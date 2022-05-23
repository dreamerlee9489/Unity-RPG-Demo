using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Items;
using App.Manager;
using App.Control;

namespace App.UI
{
    public class ItemShopPanel : ShopPanel
    {
        Button btnShift = null;
        SelectBar selectBar = null;
        public bool isSell { get; set; }

        protected override void Awake()
        {
            base.Awake();
            shopType = ShopType.ITEM;
            selectBar = transform.GetChild(5).GetComponent<SelectBar>();
            btnShift = transform.GetChild(6).GetComponent<Button>();
            btnTrade.onClick.AddListener(() =>
            {
                if (!isSell)
                {
                    if (InventoryManager.Instance.playerData.golds >= totalPrice)
                    {
                        for (int i = 0; i < shopBars.Count; i++)
                        {
                            for (int j = 0; j < shopBars[i].count; j++)
                                goods.GetChild(i).GetComponent<Item>().AddToInventory();
                            if (shopBars[i].count > 0)
                                UIManager.Instance.messagePanel.ShowMessage("[系统]  你购买了物品：" + shopBars[i].item.itemConfig.itemName + " * " + shopBars[i].count, Color.yellow);
                            shopBars[i].count = 0;
                            shopBars[i].countText.text = "0";
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
                        for (int j = 0; j < shopBars[i].count; j++)
                            InventoryManager.Instance.GetItem(shopBars[i].item).RemoveFromInventory();
                        if (shopBars[i].count > 0)
                            UIManager.Instance.messagePanel.ShowMessage("[系统]  你出售了：" + shopBars[i].item.itemConfig.itemName + " * " + shopBars[i].count, Color.red);
                        shopBars[i].inventory -= shopBars[i].count;
                        shopBars[i].count = 0;
                        shopBars[i].countText.text = "0";
                    }
                    for (int i = 0; i < shopBars.Count; i++)
                    {
                        if (shopBars[i].inventory == 0)
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
                    BuildPanel(GameManager.Instance.player.GetComponent<PlayerController>().bag);
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

        public override void BuildPanel(Transform goods)
        {
            base.BuildPanel(goods);
            for (int i = 0; i < this.goods.childCount; i++)
            {
                Item item = this.goods.GetChild(i).GetComponent<Item>();
                int index = HasItem(item);
                if (index == -1)
                {
                    shopBars.Add(Instantiate(shopBarPrefab, content));
                    shopBars[shopBars.Count - 1].BuildBar(item, this);
                }
                else
                {
                    shopBars[index].inventory++;
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
                    shopBars.Add(Instantiate(shopBarPrefab, content));
                    shopBars[shopBars.Count - 1].BuildBar(goods.GetChild(items[i]).GetComponent<Item>(), this);
                }
                else
                {
                    shopBars[index].inventory++;
                }
            }
        }
    }
}