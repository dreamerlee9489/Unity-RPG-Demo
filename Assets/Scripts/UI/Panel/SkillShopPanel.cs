using UnityEngine;
using UnityEngine.UI;
using App.Items;
using App.Manager;

namespace App.UI
{
    public class SkillShopPanel : ShopPanel
    {
        protected override void Awake()
        {
            base.Awake();
            shopType = ShopType.SKILL;
            content = gameObject.GetComponentInChildren<ScrollRect>().content;
            btnTrade.onClick.AddListener(() =>
            {
                if (InventoryManager.Instance.playerData.golds >= totalPrice)
                {
                    for (int i = 0; i < shopBars.Count; i++)
                    {
                        if (shopBars[i].count > 0)
                        {
                            goods.GetChild(i).GetComponent<Skill>().AddToInventory();
                            UIManager.Instance.messagePanel.ShowMessage("[系统]  你学习了技能：" + shopBars[i].item.itemConfig.itemName + " * " + shopBars[i].count, Color.yellow);
                        }
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
            });
        }

        public override void BuildPanel(Transform goods)
        {
            base.BuildPanel(goods);
            for (int i = 0; i < this.goods.childCount; i++)
            {
                Item item = this.goods.GetChild(i).GetComponent<Item>();
                shopBars.Add(Instantiate(shopBarPrefab, content));
                shopBars[shopBars.Count - 1].BuildBar(item, this);
            }
        }
    }
}
