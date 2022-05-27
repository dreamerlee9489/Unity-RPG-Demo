using App.Manager;

namespace App.Items
{
    public abstract class Equipment : Item
    {
        public EquipmentType equipmentType = EquipmentType.WEAPON;

        public override void AddToInventory()
        {
            InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.bag), Instantiate(itemConfig.itemUI, UIManager.Instance.bagPanel.GetFirstValidSlot().icons.transform));
        }
        
        public override void RemoveFromInventory()
        {
            InventoryManager.Instance.Remove(this);
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
        }

        public override void LoadToContainer(int level, ContainerType containerType)
        {
            switch (containerType)
            {
                case ContainerType.WORLD:
                    break;
                case ContainerType.BAG:
                    InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.bag), Instantiate(itemConfig.itemUI, UIManager.Instance.bagPanel.GetFirstValidSlot().icons.transform));
                    break;
                case ContainerType.EQUIPMENT:
                    Item item = Instantiate(itemConfig.item, InventoryManager.Instance.bag);
                    item.level = level;
                    InventoryManager.Instance.Add(item, Instantiate(itemConfig.itemUI, UIManager.Instance.bagPanel.GetFirstValidSlot().icons.transform));
                    item.Use(GameManager.Instance.player);
                    break;
                case ContainerType.ACTION:
                    break;
                case ContainerType.SKILL:
                    break;
            }
        }
    }
}
