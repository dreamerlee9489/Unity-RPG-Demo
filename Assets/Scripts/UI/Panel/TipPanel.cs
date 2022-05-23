using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.SO;
using App.Items;
using App.Manager;

namespace App.UI
{
    public class TipPanel : MonoBehaviour
    {
        Text itemName = null;
        Text itemLevel = null;
        Text itemType = null;
        Text itemPrice = null;
        Text description = null;
        Text tipBarPrefab = null;
        List<Text> bars = new List<Text>();

        void Awake()
        {
            itemName = transform.GetChild(0).GetComponent<Text>();
            itemLevel = itemName.transform.GetChild(0).GetComponent<Text>();
            itemType = transform.GetChild(1).GetComponent<Text>();
            itemPrice = itemType.transform.GetChild(0).GetComponent<Text>();
            description = transform.GetChild(2).GetComponent<Text>();
            tipBarPrefab = Resources.Load<Text>("UI/Bar/TipBar");
        }

        void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        public void Draw(Item item)
        {
            if (bars.Count == 0)
            {
                ItemConfig itemConfig = item.itemConfig;
                itemName.text = itemConfig.itemName;
                description.text = itemConfig.description;
                gameObject.SetActive(true);
                switch (itemConfig.itemType)
                {
                    case ItemType.WEAPON:
                        itemType.text = "武器";
                        itemPrice.text = "价格：" + itemConfig.itemPrice.ToString();
                        itemLevel.text = "等级：" + itemConfig.itemLevel.ToString();
                        WeaponConfig weaponConfig = itemConfig as WeaponConfig;
                        bars.Add(Instantiate(tipBarPrefab, transform));
                        bars[0].text = "攻击力：" + (weaponConfig.atk > 0 ? "+" : "") + weaponConfig.atk.ToString();
                        break;
                    case ItemType.POTION:
                        itemType.text = "消耗品";
                        itemPrice.text = "价格：" + itemConfig.itemPrice.ToString();
                        itemLevel.text = "等级：" + itemConfig.itemLevel.ToString();
                        PotionConfig potionConfig = itemConfig as PotionConfig;
                        if (potionConfig.hp != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "生命值：" + (potionConfig.hp > 0 ? "+" : "") + potionConfig.hp.ToString();
                        }
                        if (potionConfig.mp != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "法力值：" + (potionConfig.mp > 0 ? "+" : "") + potionConfig.mp.ToString();
                        }
                        if (potionConfig.atk != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "攻击力：" + (potionConfig.atk > 0 ? "+" : "") + potionConfig.atk.ToString();
                        }
                        if (potionConfig.def != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "防御力：" + (potionConfig.def > 0 ? "+" : "") + potionConfig.def.ToString();
                        }
                        break;
                    case ItemType.SKILL:
                        itemType.text = "技能";
                        itemPrice.text = "不可出售";
                        itemLevel.text = "等级：" + (item.CompareTag("Player") ? (item as Skill).level : UIManager.Instance.skillShopPanel.GetPlayerSkill(item as Skill).level + 1);
                        SkillConfig skillConfig = itemConfig as SkillConfig;
                        if(skillConfig.initialHP != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "生命值：" + (skillConfig.initialHP > 0 ? "+" : "") + skillConfig.initialHP.ToString();
                        }
                        if(skillConfig.initialMP != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "法力值：" + (skillConfig.initialMP > 0 ? "+" : "") + skillConfig.initialMP.ToString();
                        }
                        if(skillConfig.initialATK != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "攻击力：" + (skillConfig.initialATK > 0 ? "+" : "") + skillConfig.initialATK.ToString();
                        }
                        if(skillConfig.initialDEF != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "防御力：" + (skillConfig.initialDEF > 0 ? "+" : "") + skillConfig.initialDEF.ToString();
                        }
                        if(skillConfig.initialCD != 0)
                        {
                            bars.Add(Instantiate(tipBarPrefab, transform));
                            bars[bars.Count - 1].text = "冷却时间：" + skillConfig.initialCD.ToString() + "秒";
                        }
                        break;
                    case ItemType.BOOTS:
                    case ItemType.BREAST:
                    case ItemType.BRACELET:
                    case ItemType.HELMET:
                    case ItemType.NECKLACE:
                    case ItemType.PANTS:
                    case ItemType.SHIELD:
                        break;
                }
            }
        }

        public void Erase()
        {
            gameObject.SetActive(false);
            foreach (var text in bars)
                Destroy(text.gameObject);
            bars.Clear();
        }
    }
}
