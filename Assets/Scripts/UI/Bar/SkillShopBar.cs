using App.Items;
using App.Manager;
using App.SO;
using UnityEngine;

namespace App.UI
{
	public class SkillShopBar : ShopBar
	{
		int minLevel = 0, maxLevel = 0;
		public Skill playerSkill { get; set; }

		protected override void Awake()
        {
            base.Awake();
            btnMinus.onClick.AddListener(() =>
            {
                count = Mathf.Max(--count, minLevel);
                total = count * price;
                countText.text = count.ToString();
                shopPanel.CountTotalPrice();
                if(count == minLevel)
					UIManager.Instance.messagePanel.ShowMessage("已达到当前等级最低限制", Color.red);
            });
            btnPlus.onClick.AddListener(() =>
            {
                count = Mathf.Min(++count, maxLevel);
                total = count * price;
                countText.text = count.ToString();
                shopPanel.CountTotalPrice();
                if(count == maxLevel)
					UIManager.Instance.messagePanel.ShowMessage("已达到当前等级最高限制", Color.red);
            });
        }

		void Start()
		{
			for (int i = 0; i < InventoryManager.Instance.skills.childCount; i++)
            {
                Skill skill = InventoryManager.Instance.skills.GetChild(i).GetComponent<Skill>();
                if (skill.Equals(shopItem))
                    this.playerSkill = skill;
            }
			for (int i = 0; i < (shopItem.itemConfig as SkillConfig).playerLevelRequires.Count; i++)
			{
				if((shopItem.itemConfig as SkillConfig).playerLevelRequires[i] > GameManager.Instance.player.level)
                    break;
                else 
                    maxLevel = i + 1;
			}
            count = minLevel = (shopItem.itemConfig as SkillConfig).playerLevelRequires[playerSkill.level] > GameManager.Instance.player.level ? playerSkill.level : playerSkill.level + 1;
			countText.text = minLevel.ToString();
            total = count * price;
            UIManager.Instance.skillShopPanel.CountTotalPrice();
            Debug.Log(playerSkill.level + "-" + minLevel + "-" + minLevel);
		}
		
		public override void BuildBar(Item item, ShopPanel shopPanel)
        {
            base.BuildBar(item, shopPanel);
            price = item.itemConfig.itemPrice;
            priceText.text = price.ToString();
        }	
	}
}
