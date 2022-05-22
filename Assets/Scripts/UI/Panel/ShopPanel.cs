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
        ShopBar shopBar = null;
        List<ShopBar> shopBars = new List<ShopBar>();
        public bool isSell { get; set; }
        public Text total { get; set; }
        public Transform goods { get; set; }

        void Awake()
        {
            content = gameObject.GetComponentInChildren<ScrollRect>().content;
            shopBar = Resources.Load<ShopBar>("UI/ShopBar");
            selectBar = transform.GetChild(2).GetComponent<SelectBar>();
            total = transform.GetChild(4).GetChild(0).GetComponent<Text>();
            btnQuit = transform.GetChild(1).GetComponent<Button>();
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
                        for (int i = 0; i < shopBars.Count; i++)
                        {
                            for (int j = 0; j < shopBars[i].quantity; j++)
                                goods.GetChild(i).GetComponent<Item>().AddToInventory();
                            if(shopBars[i].quantity > 0)
                                UIManager.Instance.messagePanel.ShowMessage("[系统]  你购买了：" + shopBars[i].item.itemConfig.itemName + " * " + shopBars[i].quantity, Color.yellow);
                            shopBars[i].quantity = 0;
			                shopBars[i].quantityText.text = "0";
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
                    for (int i = 0; i < shopBars.Count; i++)
                    {
                        for (int j = 0; j < shopBars[i].quantity; j++)
                            InventoryManager.Instance.Get(shopBars[i].item).RemoveFromInventory();
                        if(shopBars[i].quantity > 0)
                            UIManager.Instance.messagePanel.ShowMessage("[系统]  你出售了：" + shopBars[i].item.itemConfig.itemName + " * " + shopBars[i].quantity, Color.red);
                        shopBars[i].total -= shopBars[i].quantity;
                        shopBars[i].quantity = 0;
                        shopBars[i].quantityText.text = "0";
                    }
                    for (int i = 0; i < shopBars.Count; i++)
                    {
                        if(shopBars[i].total == 0)
                        {
                            Destroy(shopBars[i].gameObject);
                            shopBars[i] = null;
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
            for (int i = 0; i < shopBars.Count; i++)
                if (shopBars[i].item.Equals(item))
                    return i;
            return -1;
        }

        void ClearPanel()
        {
            if (shopBars.Count > 0)
            {
                foreach (var bar in shopBars)
                    if(bar != null)
                        Destroy(bar.gameObject);
                shopBars.Clear();
            }
        }

        public void BuildPanel(Transform goods)
        {
            ClearPanel();
            this.goods = goods;
            gameObject.SetActive(true);
            for (int i = 0; i < this.goods.childCount; i++)
            {
                Item item = this.goods.GetChild(i).GetComponent<Item>();
                int index = HasItem(item);
                if (index == -1)
                {
                    shopBars.Add(Instantiate(shopBar, content));
                    shopBars[shopBars.Count - 1].BuildBar(item);
                }
                else
                {
                    shopBars[index].total++;
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
                    shopBars.Add(Instantiate(shopBar, content));
                    shopBars[shopBars.Count - 1].BuildBar(goods.GetChild(items[i]).GetComponent<Item>());
                }
                else
                {
                    shopBars[index].total++;
                }
            }
        }

        public void CountTotalPrice()
        {
            totalPrice = 0;
            for (int i = 0; i < shopBars.Count; i++)
                totalPrice += shopBars[i].totalPrice;
            total.text = totalPrice.ToString();
        }
    }
}