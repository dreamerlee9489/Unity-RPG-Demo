using UnityEngine;
using App.Manager;
using App.UI;
using App.Control;

namespace App.Items
{
    public class Weapon : Equipment
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Instantiate(config.item, InventoryManager.Instance.inventory), Instantiate(config.itemUI, GameManager.Instance.canvas.bagPanel.GetFirstValidSlot().icons.transform));
                Destroy(gameObject);
            }
        }
        
        public override void Use(CombatEntity user)
        {
            ItemSlot weaponSlot = GameManager.Instance.canvas.equipmentPanel.weaponSlot;
            BagPanel bagPanel = GameManager.Instance.canvas.bagPanel;
            switch (containerType)
            {
                case ContainerType.ACTION:
                case ContainerType.BAG:
                    ItemUI temp = weaponSlot.itemUI;
                    if (weaponSlot.itemUI != null)
                    {
                        weaponSlot.Close();
                        bagPanel.Close(itemUI);
                        weaponSlot.Open(itemUI);
                        bagPanel.Open(temp);
                        GameManager.Instance.player.DetachEquipment(temp.item as Equipment);
                        temp.item.containerType = ContainerType.BAG;
                    }
                    else
                    {
                        bagPanel.Close(itemUI);
                        weaponSlot.Open(itemUI);
                    }
                    GameManager.Instance.player.AttachEquipment(this);
                    containerType = ContainerType.EQUIPMENT;
                    break;
                case ContainerType.EQUIPMENT:
                    weaponSlot.Close();
                    bagPanel.Open(itemUI);
                    GameManager.Instance.player.DetachEquipment(this);
                    containerType = ContainerType.BAG;
                    break;
            }
        }
    }
}