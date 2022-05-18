﻿using UnityEngine;
using App.Manager;
using App.UI;

namespace App.Items
{
    public class Weapon : Equipment
    {
        public override void Use(Transform user)
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