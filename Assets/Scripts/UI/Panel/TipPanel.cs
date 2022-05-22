using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.SO;
using App.Items;

namespace App.UI
{
    public class TipPanel : MonoBehaviour
    {
        Text itemName = null;
        Text description = null;
        Text tipBar = null;
        List<Text> bars = new List<Text>();

        void Awake()
        {
            itemName = transform.GetChild(0).GetComponent<Text>();
            description = transform.GetChild(1).GetComponent<Text>();
            tipBar = Resources.Load<Text>("UI/TipBar");
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
                gameObject.SetActive(true);
                itemName.text = item.itemConfig.itemName;
                description.text = item.itemConfig.description;
                switch (item.itemConfig.itemType)
                {
                    case ItemType.WEAPON:
                        WeaponConfig weaponConfig = item.itemConfig as WeaponConfig;
                        bars.Add(Instantiate(tipBar, transform));
                        bars[0].text = "攻击力：" + (weaponConfig.atk > 0 ? "+" : "") + weaponConfig.atk.ToString();
                        break;
                    case ItemType.POTION:
                        PotionConfig potionConfig = item.itemConfig as PotionConfig;
                        if (potionConfig.hp != 0)
                        {
                            bars.Add(Instantiate(tipBar, transform));
                            bars[bars.Count - 1].text = "生命值：" + (potionConfig.hp > 0 ? "+" : "") + potionConfig.hp.ToString();
                        }
                        if (potionConfig.mp != 0)
                        {
                            bars.Add(Instantiate(tipBar, transform));
                            bars[bars.Count - 1].text = "法力值：" + (potionConfig.mp > 0 ? "+" : "") + potionConfig.mp.ToString();
                        }
                        if (potionConfig.atk != 0)
                        {
                            bars.Add(Instantiate(tipBar, transform));
                            bars[bars.Count - 1].text = "攻击力：" + (potionConfig.atk > 0 ? "+" : "") + potionConfig.atk.ToString();
                        }
                        if (potionConfig.def != 0)
                        {
                            bars.Add(Instantiate(tipBar, transform));
                            bars[bars.Count - 1].text = "防御力：" + (potionConfig.def > 0 ? "+" : "") + potionConfig.def.ToString();
                        }
                        break;
                    case ItemType.SKILL:
                        SkillConfig skillConfig = item.itemConfig as SkillConfig;
                        if(skillConfig.initialHP != 0)
                        {
                            bars.Add(Instantiate(tipBar, transform));
                            bars[bars.Count - 1].text = "生命值：" + (skillConfig.initialHP > 0 ? "+" : "") + skillConfig.initialHP.ToString();
                        }
                        if(skillConfig.initialMP != 0)
                        {
                            bars.Add(Instantiate(tipBar, transform));
                            bars[bars.Count - 1].text = "法力值：" + (skillConfig.initialMP > 0 ? "+" : "") + skillConfig.initialMP.ToString();
                        }
                        if(skillConfig.initialATK != 0)
                        {
                            bars.Add(Instantiate(tipBar, transform));
                            bars[bars.Count - 1].text = "攻击力：" + (skillConfig.initialATK > 0 ? "+" : "") + skillConfig.initialATK.ToString();
                        }
                        if(skillConfig.initialDEF != 0)
                        {
                            bars.Add(Instantiate(tipBar, transform));
                            bars[bars.Count - 1].text = "防御力：" + (skillConfig.initialDEF > 0 ? "+" : "") + skillConfig.initialDEF.ToString();
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
