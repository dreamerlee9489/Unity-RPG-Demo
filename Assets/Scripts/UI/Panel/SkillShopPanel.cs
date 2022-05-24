using UnityEngine;
using UnityEngine.UI;
using App.Items;
using App.Manager;
using App.SO;

namespace App.UI
{
    public class SkillShopPanel : ShopPanel
    {
        protected override void Awake()
        {
            base.Awake();
            shopBarPrefab = Resources.Load<ShopBar>("UI/Bar/SkillShopBar");
            shopType = ShopType.SKILL;
            content = gameObject.GetComponentInChildren<ScrollRect>().content;
            btnTrade.onClick.AddListener(() =>
            {
                if (InventoryManager.Instance.playerData.golds >= total)
                {
                    for (int i = 0; i < shopBars.Count; i++)
                    {
                        if (shopBars[i].count > 0)
                        {
                            int levelRequire = (shopBars[i].shopItem.GetComponent<Skill>().itemConfig as SkillConfig).levelRequires[shopBars[i].count - 1];
                            Skill skill = (shopBars[i] as SkillShopBar).skill;
                            if(skill != null && shopBars[i].count <= skill.level)
                            {
                                UIManager.Instance.messagePanel.ShowMessage("你的等级尚且无法学习该技能。", Color.red);
                            }
                            else if(levelRequire <= GameManager.Instance.player.level)
                            {
                                for(int j = 0; j < shopBars[i].count; j++)
                                    goods.GetChild(i).GetComponent<Skill>().AddToInventory();
                                InventoryManager.Instance.playerData.golds -= total;
                                UIManager.Instance.goldPanel.UpdatePanel();
                                total = 0;
                                totalText.text = "0";
                                // shopBars[i].count = (shopBars[i] as SkillShopBar).skill.level;
                                // shopBars[i].countText.text = shopBars[i].count.ToString();
                                UIManager.Instance.messagePanel.ShowMessage("[系统]  " + shopBars[i].shopItem.itemConfig.itemName + "的技能等级提升到了：" + shopBars[i].count, Color.yellow);
                            }
                            else
                            {
                                UIManager.Instance.messagePanel.ShowMessage("你的等级尚且无法学习该技能。", Color.red);
                            }
                        }
                    }
                }
                else
                {
                    hint.text = "金币不足，无法学习。";
                }
            });
        }

        void OnEnable()
        {
            total = 0;
            totalText.text = "0";
            hint.text = "";
            CountTotalPrice();
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

        public Skill GetPlayerSkill(Skill skill)
        {
            for (int i = 0; i < shopBars.Count; i++)
            {
                Skill result = (shopBars[i] as SkillShopBar).skill;
                if(result != null && result.Equals(skill))
                    return result;
            }
            return null;
        }
    }
}
