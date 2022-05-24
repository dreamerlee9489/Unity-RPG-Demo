using UnityEngine;
using App.Control;
using App.Manager;
using App.SO;

namespace App.Items
{
    public class Skill : Item
    {
        public int level = 0;

        public override void AddToInventory()
        {
            int index = InventoryManager.Instance.HasSkill(this);
            if(index != -1)
                InventoryManager.Instance.skills.GetChild(index).GetComponent<Skill>().level++;
            else
            {
                Skill skill = Instantiate(itemConfig.itemPrefab, InventoryManager.Instance.skills).GetComponent<Skill>();
                skill.level = 1;
                InventoryManager.Instance.Add(skill, Instantiate(itemConfig.itemUI, UIManager.Instance.actionPanel.GetFirstValidSlot().icons.transform));
            }
        }

        public override void RemoveFromInventory() {}

        public override void Use(CombatEntity user)
        {
            SkillConfig skillConfig = itemConfig as SkillConfig;
            WeaponType professWeaponType = user.professionConfig.weaponType;
            WeaponType currentWeaponType = (user.currentWeapon.itemConfig as WeaponConfig).weaponType;
            if (professWeaponType == currentWeaponType)
            {
                if (level > 0)
                {
                    switch (skillConfig.skillType)
                    {
                        case SkillType.A:
                            user.GetComponent<Animator>().SetBool("attack", false);
                            user.GetComponent<Animator>().SetTrigger("skillA");
                            break;
                        case SkillType.B:
                            user.GetComponent<Animator>().SetBool("attack", false);
                            user.GetComponent<Animator>().SetTrigger("skillB");
                            break;
                        case SkillType.C:
                            user.GetComponent<Animator>().SetBool("attack", false);
                            user.GetComponent<Animator>().SetTrigger("skillC");
                            break;
                        case SkillType.D:
                            user.GetComponent<Animator>().SetBool("attack", false);
                            user.GetComponent<Animator>().SetTrigger("skillD");
                            break;
                    }
                }
                else
                {
                    UIManager.Instance.messagePanel.ShowMessage("[系统]  你尚未学习该技能", Color.red);
                }
            }
            else
            {
                UIManager.Instance.messagePanel.ShowMessage("[系统]  武器类型不匹配，无法施展技能", Color.red);
            }
        }

        void LevelUp()
        {
            
        }
    }
}
