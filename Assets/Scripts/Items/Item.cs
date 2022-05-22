using UnityEngine;
using App.SO;
using App.Control;
using App.UI;

namespace App.Items
{
    public enum ItemType { NONE, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, BRACELET, WEAPON, PANTS, POTION, SKILL }
    public enum EquipmentType { WEAPON, ARMOR, JEWELRY }
    public enum ContainerType { WORLD, BAG, EQUIPMENT, ACTION }

    [RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour
    {
        public ItemConfig itemConfig = null;
        public ContainerType containerType = ContainerType.WORLD;
        public new Collider collider { get; set; }
        public new Rigidbody rigidbody { get; set; }
        public ItemUI itemUI { get; set; }
        public NameBar nameBar { get; set; }        
        public abstract void Use(CombatEntity user);
        public abstract void AddToInventory();
        public abstract void RemoveFromInventory();
        public override bool Equals(object other) => itemConfig == (other as Item).itemConfig;
        public override int GetHashCode() => itemConfig.itemName.GetHashCode();

        protected void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            collider.enabled = containerType == ContainerType.WORLD ? true : false;
            rigidbody.useGravity = containerType == ContainerType.WORLD ? true : false;
            rigidbody.isKinematic = containerType == ContainerType.WORLD ? false : true;
        }

        void Update()
        {
            if (containerType == ContainerType.WORLD)
            {
                if(nameBar == null)
                {
                    nameBar = Instantiate(Resources.Load<NameBar>("UI/NameBar"));
                    nameBar.chName.text = itemConfig.itemName;
                }
                nameBar.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
        }
    }
}
