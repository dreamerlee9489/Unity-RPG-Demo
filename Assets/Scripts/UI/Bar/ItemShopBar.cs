using UnityEngine;
using App.Items;
using App.Manager;
using UnityEngine.UI;

namespace App.UI
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
                count = Mathf.Max(--count, 0);
                total = count * price;
                countText.text = count.ToString();
                shopPanel.CountTotalPrice();
                shopPanel.hint.text = "";
            });
            btnPlus.onClick.AddListener(() =>
            {
                count = Mathf.Min(++count, inventory);
                total = count * price;
                countText.text = count.ToString();
                shopPanel.CountTotalPrice();
                shopPanel.hint.text = "";
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
