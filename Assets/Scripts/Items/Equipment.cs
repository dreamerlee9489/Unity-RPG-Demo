using System;
using UnityEngine;
using Data;
using Manager;

namespace Items
{
    public abstract class Equipment : Item
    {
        public EquipmentType equipmentType = EquipmentType.Weapon;

        protected override void Awake()
        {
            base.Awake();
            level = itemData.level = itemConfig.itemLevel;
        }

        void Start()
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.constraints = containerType == ContainerType.World
                ? RigidbodyConstraints.None
                : RigidbodyConstraints.FreezeAll;
            collider.enabled = containerType == ContainerType.World ? true : false;
            tag = containerType == ContainerType.World ? "DropItem" : "Untagged";
        }

        public override void AddToInventory()
        {
            InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.bag),
                Instantiate(itemConfig.itemUI, UIManager.Instance.bagPanel.GetFirstValidSlot().icons.transform));
            for (int i = 0; i < InventoryManager.Instance.ongoingQuests.Count; i++)
            {
                Item temp = InventoryManager.Instance.ongoingQuests[i].Target.GetComponent<Item>();
                if (temp != null && Equals(temp))
                    InventoryManager.Instance.ongoingQuests[i].UpdateProgress(1);
            }
        }

        public override void RemoveFromInventory()
        {
            InventoryManager.Instance.Remove(this);
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
        }

        public override void LoadToContainer(ItemData itemData)
        {
            switch (itemData.containerType)
            {
                case ContainerType.World:
                    break;
                case ContainerType.Bag:
                    InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.bag),
                        Instantiate(itemConfig.itemUI,
                            UIManager.Instance.bagPanel.GetFirstValidSlot().icons.transform));
                    break;
                case ContainerType.Equipment:
                    Item item = Instantiate(itemConfig.item, InventoryManager.Instance.bag);
                    item.level = level;
                    InventoryManager.Instance.Add(item,
                        Instantiate(itemConfig.itemUI,
                            UIManager.Instance.bagPanel.GetFirstValidSlot().icons.transform));
                    item.Use(GameManager.Instance.player);
                    break;
                case ContainerType.Action:
                    break;
            }
        }
    }
}