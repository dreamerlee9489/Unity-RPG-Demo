using UnityEngine;
using App.SO;
using App.UI;
using App.Control;
using App.Data;

namespace App.Items
{
    public enum ItemType { NONE, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, BRACELET, WEAPON, PANTS, POTION, SKILL }
    public enum EquipmentType { WEAPON, ARMOR, JEWELRY }
    public enum ContainerType { WORLD, BAG, EQUIPMENT, ACTION, SKILL }

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour
    {
        public ItemConfig itemConfig = null;
        public ContainerType containerType = ContainerType.WORLD;
        public int level { get; set; }
        public float cdTimer { get; set; }
        public new Collider collider { get; set; }
        public new Rigidbody rigidbody { get; set; }
        public NameBar nameBar { get; set; }
        public ItemUI itemUI { get; set; }
        public ItemSlot itemSlot { get; set; }
        public abstract void Use(CombatEntity user);
        public abstract void AddToInventory();
        public abstract void RemoveFromInventory();
        public abstract void LoadToContainer(int level, ContainerType containerType);
        public override bool Equals(object other) => itemConfig == (other as Item).itemConfig;
        public override int GetHashCode() => transform.GetHashCode();

        protected virtual void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            level = itemConfig.itemLevel;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.constraints = containerType == ContainerType.WORLD ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
            collider.enabled = containerType == ContainerType.WORLD ? true : false;
            tag = containerType == ContainerType.WORLD ? "Drop" : "Untagged";
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
