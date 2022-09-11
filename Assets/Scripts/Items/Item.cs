using UnityEngine;
using SO;
using UI;
using Control;
using Data;

namespace Items
{
    public enum ItemType
    {
        None,
        Helmet,
        Breast,
        Shield,
        Boots,
        Necklace,
        Bracelet,
        Weapon,
        Pants,
        Potion,
        Skill
    }

    public enum EquipmentType
    {
        Weapon,
        Armor,
        Jewelry
    }

    public enum ContainerType
    {
        World,
        Bag,
        Equipment,
        Action
    }

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour
    {
        public ItemConfig itemConfig = null;
        public ContainerType containerType = ContainerType.World;
        public int level { get; set; }
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
            itemData = new ItemData
            {
                id = System.Guid.NewGuid().ToString(),
                path = "Items/" + GetType().Name + "/" + itemConfig.item.name,
                position = new Vector(transform.position),
                containerType = containerType
            };
        }

        protected virtual void Update()
        {
            if (containerType == ContainerType.World)
            {
                if (nameBar == null)
                {
                    nameBar = Instantiate(Resources.Load<NameBar>("UI/Bar/NameBar"));
                    nameBar.chName.text = itemConfig.itemName;
                }

                nameBar.transform.position =
                    new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
        }
    }
}