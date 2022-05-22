using App.Manager;
using UnityEngine;

namespace App.Items
{
    public abstract class Equipment : Item
    {
        public EquipmentType equipmentType = EquipmentType.WEAPON;

        public override void AddToInventory()
        {
            InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.inventory), Instantiate(itemConfig.itemUI, UIManager.Instance.bagPanel.GetFirstValidSlot().icons.transform));
        }
        
        public override void RemoveFromInventory()
        {
            InventoryManager.Instance.Remove(this);
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
        }
    }
}
