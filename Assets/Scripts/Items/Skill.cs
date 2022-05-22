using UnityEngine;
using App.Control;
using App.Manager;
using App.SO;

namespace App.Items
{
    public class Skill : Item
    {
        public int level = 1;
        
        public override void AddToInventory()
        {
            InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.inventory), Instantiate(itemConfig.itemUI, UIManager.Instance.actionPanel.GetFirstValidSlot().icons.transform));
        }

        public override void RemoveFromInventory()
        {
            throw new System.NotImplementedException();
        }

        public override void Use(CombatEntity user)
        {
            SkillConfig skillConfig = itemConfig as SkillConfig;
            switch (skillConfig.skillType)
            {
                case SkillType.A:
                user.GetComponent<Animator>().SetTrigger("skillA");
                break;
                case SkillType.B:
                user.GetComponent<Animator>().SetTrigger("skillB");
                break;
                case SkillType.C:
                user.GetComponent<Animator>().SetTrigger("skillC");
                break;
                case SkillType.D:
                user.GetComponent<Animator>().SetTrigger("skillD");
                break;
            }
        }
    }
}
