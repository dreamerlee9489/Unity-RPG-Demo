using App.Manager;
using App.UI;
using App.Control;

namespace App.Items
{
    public class Weapon : Equipment
    {
        public override void Use(Entity user)
        {
            ItemSlot weaponSlot = UIManager.Instance.equipmentPanel.weaponSlot;
            BagPanel bagPanel = UIManager.Instance.bagPanel;
            switch (containerType)
            {
                case ContainerType.Action:
                case ContainerType.Bag:
                    ItemUI temp = weaponSlot.itemUI;
                    if (weaponSlot.itemUI != null)
                    {
                        weaponSlot.Erase();
                        bagPanel.Erase(itemUI);
                        weaponSlot.Draw(itemUI);
                        bagPanel.Draw(temp);
                        GameManager.Instance.player.DetachEquipment(temp.item as Equipment);
                        temp.item.containerType = ContainerType.Bag;
                    }
                    else
                    {
                        bagPanel.Erase(itemUI);
                        weaponSlot.Draw(itemUI);
                    }
                    GameManager.Instance.player.AttachEquipment(this);
                    containerType = ContainerType.Equipment;
                    break;
                case ContainerType.Equipment:
                    weaponSlot.Erase();
                    bagPanel.Draw(itemUI);
                    GameManager.Instance.player.DetachEquipment(this);
                    containerType = ContainerType.Bag;
                    break;
            }
        }   
    }
}