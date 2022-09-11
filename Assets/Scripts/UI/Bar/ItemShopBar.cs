using UnityEngine;
using Items;
using Manager;

namespace UI
{
    public class ItemShopBar : ShopBar
    {
        public int inventory { get; set; }

        protected override void Awake()
        {
            base.Awake();
            inventory = UIManager.Instance.itemShopPanel.isSell ? 1 : 10;
            btnMinus.onClick.AddListener(() =>
            {
                if (count != 0)
                {
                    count = Mathf.Max(--count, 0);
                    total = count * price;
                    countText.text = count.ToString();
                    shopPanel.CountTotalPrice();
                    shopPanel.hint.text = "";
                }
            });
            btnPlus.onClick.AddListener(() =>
            {
                if (count == inventory)
                    shopPanel.hint.text = (shopPanel as ItemShopPanel).isSell ? "已达到你的库存上限" : "已达到单次购买数量最大限制";
                else
                {
                    count = Mathf.Min(++count, inventory);
                    total = count * price;
                    countText.text = count.ToString();
                    shopPanel.CountTotalPrice();
                    shopPanel.hint.text = "";
                }
            });
        }

        public override void BuildBar(Item item, ShopPanel shopPanel)
        {
            base.BuildBar(item, shopPanel);
            price = (int)(item.itemConfig.itemPrice * (UIManager.Instance.itemShopPanel.isSell ? 1f : 1.5f));
            priceText.text = price.ToString();
        }
    }
}