using UnityEngine;
using App.Control;

namespace App.Items
{
    public class Skill : Item
    {
        public override void AddToInventory()
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveFromInventory()
        {
            throw new System.NotImplementedException();
        }

        public override void Use(CombatEntity user)
        {
            Debug.Log("Use: " + itemConfig.itemName);
        }
    }
}
