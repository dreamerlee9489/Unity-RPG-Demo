using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Items;
using App.Manager;
using App.Control;

namespace App.UI
{
    public class ShopPanel : BasePanel
    {
        int totalPrice = 0;
        Transform content = null;
        Button btnQuit = null;
        Button btnTrade = null;
        Button btnShift = null;
        SelectBar selectBar = null;
        List<ItemBar> itemBars = new List<ItemBar>();
        public bool isSell = false;
        public ItemBar itemBar = null;
        public Text total { get; set; }
        public Transform goods { get; set; }

        void Awake()
        {
            btnQuit = transform.GetChild(1).GetComponent<Button>();
            selectBar = transform.GetChild(2).GetComponent<SelectBar>();
            content = transform.GetChild(3).GetChild(0).GetChild(0);
            total = transform.GetChild(4).GetChild(0).GetComponent<Text>();
            btnTrade = transform.GetChild(5).GetComponent<Button>();
            btnShift = transform.GetChild(6).GetComponent<Button>();
            btnQuit.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
            btnTrade.onClick.AddListener(() =>
            {
                if (!isSell)
                {
                    if(InventoryManager.Instance.playerData.golds >= totalPrice)
                    {
                        for (int i = 0; i < itemBars.Count; i++)
                        {
                            for (int j = 0; j < itemBars[i].quantity; j++)
                                goods.GetChild(i).GetComponent<Item>().AddToInventory();
                            if(itemBars[i].quantity > 0)
                                UIManager.Instance.messagePanel.ShowMessage("[系统]  你购买了：" + itemBars[i].item.itemConfig.itemName + " * " + itemBars[i].quantity, Color.yellow);
                            itemBars[i].quantity = 0;
			                itemBars[i].quantityText.text = "0";
                        }
                        InventoryManager.Instance.playerData.golds -= totalPrice;
                        UIManager.Instance.goldPanel.UpdatePanel();
                        totalPrice = 0;
                        total.text = "0";
                    }
                    else
                    {
                        UIManager.Instance.messagePanel.ShowMessage("[系统]  余额不足，无法购买。", Color.red);
                    }
                }
                else
                {
                    for (int i = 0; i < itemBars.Count; i++)
                    {
                        for (int j = 0; j < itemBars[i].quantity; j++)
                            InventoryManager.Instance.Get(itemBars[i].item).RemoveFromInventory();
                        if(itemBars[i].quantity > 0)
                            UIManager.Instance.messagePanel.ShowMessage("[系统]  你出售了：" + itemBars[i].item.itemConfig.itemName + " * " + itemBars[i].quantity, Color.red);
                        itemBars[i].count -= itemBars[i].quantity;
                        itemBars[i].quantity = 0;
                        itemBars[i].quantityText.text = "0";
                        if(itemBars[i].count == 0)
                        {
                            Destroy(itemBars[i].gameObject);
                        }
                    }
                    InventoryManager.Instance.playerData.golds += totalPrice;
                    UIManager.Instance.goldPanel.UpdatePanel();
                    totalPrice = 0;
                    total.text = "0";
                }
            });
            btnShift.onClick.AddListener(() =>
            {
                if (!isSell)
                {
                    isSell = true;
                    BuildPanel(GameManager.Instance.player.GetComponent<PlayerController>().inventory);
                    btnShift.transform.GetChild(0).GetComponent<Text>().text = "切换到购买";
                    btnTrade.transform.GetChild(0).GetComponent<Text>().text = "出售";
                    totalPrice = 0;
                    total.text = "0";
                }
                else
                {
                    isSell = false;
                    BuildPanel(UIManager.Instance.dialoguePanel.npc.goods);
                    btnShift.transform.GetChild(0).GetComponent<Text>().text = "切换到出售";
                    btnTrade.transform.GetChild(0).GetComponent<Text>().text = "购买";
                    totalPrice = 0;
                    total.text = "0";
                }
            });
        }

        void OnEnable()
        {
            isSell = false;
            btnShift.transform.GetChild(0).GetComponent<Text>().text = "切换到出售";
            btnTrade.transform.GetChild(0).GetComponent<Text>().text = "购买";
            totalPrice = 0;
            total.text = "0";
        }

        int HasItem(Item item)
        {
            for (int i = 0; i < itemBars.Count; i++)
                if (itemBars[i].item.Equals(item))
                    return i;
            return -1;
        }

        void ClearPanel()
        {
            if (itemBars.Count > 0)
            {
                foreach (var bar in itemBars)
                    Destroy(bar.gameObject);
                itemBars.Clear();
            }
        }

        public void BuildPanel(Transform shop)
        {
            ClearPanel();
            goods = shop;
            gameObject.SetActive(true);
            for (int i = 0; i < goods.childCount; i++)
            {
                Item item = goods.GetChild(i).GetComponent<Item>();
                int index = HasItem(item);
                if (index == -1)
                {
                    itemBars.Add(Instantiate(itemBar, content));
                    itemBars[itemBars.Count - 1].BuildBar(item);
                }
                else
                {
                    itemBars[index].count++;
                }
            }
        }

        public void ShowFiltered(List<int> items)
        {
            ClearPanel();
            for (int i = 0; i < items.Count; i++)
            {
                Item item = goods.GetChild(items[i]).GetComponent<Item>();
                int index = HasItem(item);
                if (index == -1)
                {
                    itemBars.Add(Instantiate(itemBar, content));
                    itemBars[itemBars.Count - 1].BuildBar(goods.GetChild(items[i]).GetComponent<Item>());
                }
                else
                {
                    itemBars[index].count++;
                }
            }
        }

        public void CountTotalPrice()
        {
            totalPrice = 0;
            for (int i = 0; i < itemBars.Count; i++)
                totalPrice += itemBars[i].totalPrice;
            total.text = totalPrice.ToString();
        }
    }
}
