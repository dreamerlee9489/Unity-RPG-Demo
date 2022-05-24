using UnityEngine;
using App.SO;
using App.Control;
using App.UI;
using UnityEngine.UI;

namespace App.Items
{
    public enum ItemType { NONE, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, BRACELET, WEAPON, PANTS, POTION, SKILL }
    public enum EquipmentType { WEAPON, ARMOR, JEWELRY }
    public enum ContainerType { WORLD, BAG, EQUIPMENT, ACTION }

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour
    {
        public float cdTimer = 0;
        public ItemConfig itemConfig = null;
        public ContainerType containerType = ContainerType.WORLD;
        public new Collider collider { get; set; }
        public new Rigidbody rigidbody { get; set; }
        public ItemUI itemUI { get; set; }
        public ItemSlot itemSlot { get; set; }
        public NameBar nameBar { get; set; }        
        public abstract void AddToInventory();
        public abstract void RemoveFromInventory();
        public abstract void Use(CombatEntity user);
        public override bool Equals(object other) => itemConfig == (other as Item).itemConfig;
        public override int GetHashCode() => itemConfig.itemName.GetHashCode();

        protected virtual void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.constraints = containerType == ContainerType.WORLD ? RigidbodyConstraints.None : RigidbodyConstraints.FreezePositionY;
            collider.enabled = containerType == ContainerType.WORLD ? true : false;
            cdTimer = itemConfig.cd;
       }

        protected virtual void Update()
        {
            if (containerType == ContainerType.WORLD)
            {
                if(nameBar == null)
                {
                    nameBar = Instantiate(Resources.Load<NameBar>("UI/Bar/NameBar"));
                    nameBar.chName.text = itemConfig.itemName;
                }
                nameBar.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
        }
    }
}
