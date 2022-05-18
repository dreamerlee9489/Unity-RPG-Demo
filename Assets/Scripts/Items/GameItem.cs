using UnityEngine;
using App.Manager;
using App.Config;
using App.UI;

namespace App.Items
{
    public enum ItemType { NONE, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, HAND, WEAPON, PANTS, POTION, SKILL }
    public enum EquipmentType { WEAPON, ARMOR, JEWELRY }
    public enum ContainerType { WORLD, ENTITY, BAG, EQUIPMENT, ACTION }

    public abstract class GameItem : MonoBehaviour
    {
        public ItemConfig config = null;
        public ItemUI itemUI { get; set; }
        public ContainerType containerType = ContainerType.WORLD;
        public abstract void Use(Transform user);

        public override bool Equals(object other)
        {
            return config == (other as GameItem).config && containerType == (other as GameItem).containerType;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected void Awake()
        {
            GetComponent<Collider>().enabled = containerType == ContainerType.WORLD ? true : false;
        }

        protected abstract void OnTriggerEnter(Collider other);
    }
}
