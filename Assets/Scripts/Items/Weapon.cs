﻿using App.Manager;
using App.UI;
using App.Control;

namespace App.Items
{
    public class Weapon : Equipment
    {
        public override void AddToInventory()
        {
            InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.inventory), Instantiate(itemConfig.itemUI, UIManager.Instance.bagPanel.GetFirstValidSlot().icons.transform));
        }   

        public override void Use(CombatEntity user)
        {
            ItemSlot weaponSlot = UIManager.Instance.equipmentPanel.weaponSlot;
            BagPanel bagPanel = UIManager.Instance.bagPanel;
            switch (containerType)
            {
                case ContainerType.ACTION:
                case ContainerType.BAG:
                    ItemUI temp = weaponSlot.itemUI;
                    if (weaponSlot.itemUI != null)
                    {
                        weaponSlot.Erase();
                        bagPanel.Erase(itemUI);
                        weaponSlot.Draw(itemUI);
                        bagPanel.Draw(temp);
                        GameManager.Instance.player.DetachEquipment(temp.item as Equipment);
                        temp.item.containerType = ContainerType.BAG;
                    }
                    else
                    {
                        bagPanel.Erase(itemUI);
                        weaponSlot.Draw(itemUI);
                    }
                    GameManager.Instance.player.AttachEquipment(this);
                    containerType = ContainerType.EQUIPMENT;
                    break;
                case ContainerType.EQUIPMENT:
                    weaponSlot.Erase();
                    bagPanel.Draw(itemUI);
                    GameManager.Instance.player.DetachEquipment(this);
                    containerType = ContainerType.BAG;
                    break;
            }
        }   
    }
}