using System.Diagnostics;
using App.Items;
using App.Manager;
using App.SO;
using UnityEngine;

namespace App.UI
{
	public class SkillShopBar : ShopBar
	{
		int minLevel = 0, maxLevel = 0;
		public Skill skill { get; set; }

		protected override void Awake()
        {
            base.Awake();
            btnMinus.onClick.AddListener(() =>
            {
                count = Mathf.Max(--count, minLevel);
                total = (count - minLevel) * price;
                countText.text = count.ToString();
                shopPanel.CountTotalPrice();
                if(count == minLevel)
					UIManager.Instance.messagePanel.ShowMessage("已到达当前技能等级", Color.red);
            });
            btnPlus.onClick.AddListener(() =>
            {
                count = Mathf.Min(++count, maxLevel);
                total = (count - minLevel) * price;
                countText.text = count.ToString();
                shopPanel.CountTotalPrice();
                if(count == maxLevel)
					UIManager.Instance.messagePanel.ShowMessage("当前等级无法学习更高级的技能", Color.red);
            });
        }

		void Start()
		{
			for (int i = 0; i < InventoryManager.Instance.skills.childCount; i++)
            {
                Skill skill = InventoryManager.Instance.skills.GetChild(i).GetComponent<Skill>();
                if (skill.Equals(shopItem))
                    this.skill = skill;
            }
			for (int i = 0; i < (shopItem.itemConfig as SkillConfig).levelRequires.Count; i++)
			{
				if((shopItem.itemConfig as SkillConfig).levelRequires[i] <= GameManager.Instance.player.level)
                {
                    maxLevel = i + 1;
                }
                else 
                    break;
			}
            count = minLevel = skill == null ? 0 : skill.level;
            total = (count - minLevel) * price;
			countText.text = count.ToString();
            UIManager.Instance.skillShopPanel.CountTotalPrice();
		}
		
		public override void BuildBar(Item item, ShopPanel shopPanel)
        {
            base.BuildBar(item, shopPanel);
            price = item.itemConfig.itemPrice;
            priceText.text = price.ToString();
        }	
	}
}
