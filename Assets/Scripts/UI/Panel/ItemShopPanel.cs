using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Items;
using App.Enviorment;
using App.AI;

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
            shopBarPrefab = Resources.Load<ShopBar>("UI/Bar/ItemShopBar");
            shopType = ShopType.ITEM;
            selectBar = transform.GetChild(4).GetComponent<SelectBar>();
            btnShift = transform.GetChild(5).GetComponent<Button>();
            btnTrade.onClick.AddListener(() =>
            {
                if (!isSell)
                {
                    if (InventoryManager.Instance.playerData.golds < total)
                        hint.text = "金币不足，无法购买";
                    else
                    {
                        for (int i = 0; i < shopBars.Count; i++)
                        {
                            for (int j = 0; j < shopBars[i].count; j++)
                                goods.GetChild(i).GetComponent<Item>().AddToInventory();
                            if (shopBars[i].count > 0)
                                UIManager.Instance.messagePanel.Print("[系统]  你购买了物品：" + shopBars[i].shopItem.itemConfig.itemName + " * " + shopBars[i].count, Color.yellow);
                            shopBars[i].count = 0;
                            shopBars[i].countText.text = "0";
                        }
                        InventoryManager.Instance.playerData.golds -= total;
                        UIManager.Instance.goldPanel.UpdatePanel();
                        total = 0;
                        txtTotal.text = "0";
                    }
                }
                else
                {
                    for (int i = 0; i < shopBars.Count; i++)
                    {
                        for (int j = 0; j < shopBars[i].count; j++)
                            InventoryManager.Instance.GetItem(shopBars[i].shopItem).RemoveFromInventory();
                        if (shopBars[i].count > 0)
                            UIManager.Instance.messagePanel.Print("[系统]  你出售了物品：" + shopBars[i].shopItem.itemConfig.itemName + " * " + shopBars[i].count, Color.yellow);
                        (shopBars[i] as ItemShopBar).inventory -= shopBars[i].count;
                        shopBars[i].count = 0;
                        shopBars[i].countText.text = "0";
                    }
                    for (int i = 0; i < shopBars.Count; i++)
                    {
                        if ((shopBars[i] as ItemShopBar).inventory == 0)
                        {
                            Destroy(shopBars[i].gameObject);
                            shopBars[i] = null;
                        }
                    }
                    InventoryManager.Instance.playerData.golds += total;
                    UIManager.Instance.goldPanel.UpdatePanel();
                    total = 0;
                    txtTotal.text = "0";
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
                    total = 0;
                    txtTotal.text = "0";
                }
                else
                {
                    isSell = false;
                    BuildPanel(UIManager.Instance.dialoguePanel.npc.goods);
                    btnShift.transform.GetChild(0).GetComponent<Text>().text = "切换到出售";
                    btnTrade.transform.GetChild(0).GetComponent<Text>().text = "购买";
                    total = 0;
                    txtTotal.text = "0";
                }
            });
        }

        void OnEnable()
        {
            isSell = false;
            btnShift.transform.GetChild(0).GetComponent<Text>().text = "切换到出售";
            btnTrade.transform.GetChild(0).GetComponent<Text>().text = "购买";
            total = 0;
            txtTotal.text = "0";
            hint.text = "";
        }

        int HasItem(Item item)
        {
            for (int i = 0; i < shopBars.Count; i++)
                if (shopBars[i].shopItem.Equals(item))
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
                    (shopBars[index] as ItemShopBar).inventory++;
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
                    (shopBars[index] as ItemShopBar).inventory++;
                }
            }
        }
    }
}