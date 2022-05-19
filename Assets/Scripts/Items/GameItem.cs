using UnityEngine;
using App.Config;
using App.Control;
using App.UI;

namespace App.Items
{
    public enum ItemType { NONE, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, HAND, WEAPON, PANTS, POTION, SKILL }
    public enum EquipmentType { WEAPON, ARMOR, JEWELRY }
    public enum ContainerType { WORLD, ENTITY, BAG, EQUIPMENT, ACTION }

    [RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
    public abstract class GameItem : MonoBehaviour
    {
        public ItemConfig config = null;
        public ItemUI itemUI { get; set; }
        public new Collider collider { get; set; }
        public new Rigidbody rigidbody { get; set; }
        public ContainerType containerType = ContainerType.WORLD;
        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AddToInventory();
                Destroy(gameObject);
            }
        }
        public abstract void Use(CombatEntity user);
        public abstract void AddToInventory();
        public override bool Equals(object other) => config == (other as GameItem).config;
        public override int GetHashCode() => config.itemName.GetHashCode();

        protected void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            collider.enabled = containerType == ContainerType.WORLD ? true : false;
            rigidbody.useGravity = containerType == ContainerType.WORLD ? true : false;
        }
    }
}
