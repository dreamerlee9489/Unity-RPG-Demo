using UnityEngine;
using App.SO;
using App.UI;
using App.Control;
using App.Data;

namespace App.Items
{
    public enum ItemType { NONE, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, BRACELET, WEAPON, PANTS, POTION, SKILL }
    public enum EquipmentType { WEAPON, ARMOR, JEWELRY }
    public enum ContainerType { WORLD, BAG, EQUIPMENT, ACTION }

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour
    {
        public ItemConfig itemConfig = null;
        public ContainerType containerType = ContainerType.WORLD;
        public int level = 0;
        public float cdTimer { get; set; }
        public new Collider collider { get; set; }
        public new Rigidbody rigidbody { get; set; }
        public NameBar nameBar { get; set; }
        public ItemUI itemUI { get; set; }
        public ItemSlot itemSlot { get; set; }
        public ItemData itemData { get; set; }
        public abstract void Use(Entity user);
        public abstract void AddToInventory();
        public abstract void RemoveFromInventory();
        public abstract void LoadToContainer(ItemData itemData);
        public override bool Equals(object other) => itemConfig == (other as Item).itemConfig;
        public override int GetHashCode() => name.GetHashCode() ^ transform.GetHashCode();

        protected virtual void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            itemData = new ItemData();
            itemData.id = System.Guid.NewGuid().ToString();
            itemData.path = "Items/" + GetType().Name + "/" + itemConfig.item.name;
            itemData.position = new Vector(transform.position);
            itemData.containerType = containerType;
        }

        protected virtual void Update()
        {
            if (containerType == ContainerType.WORLD)
            {
                if (nameBar == null)
                {
                    nameBar = Instantiate(Resources.Load<NameBar>("UI/Bar/NameBar"));
                    nameBar.chName.text = itemConfig.itemName;
                }
                nameBar.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
        }
    }
}
