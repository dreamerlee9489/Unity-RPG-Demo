using App.Manager;
using App.SO;
using UnityEngine;

namespace App.Item
{
    public class Armor : GameItem
    {
        public Armor(ItemConfig itemConfig) : base(itemConfig)
        {
            itemType = UI.ItemType.ARMOR;
        }

        public override void Use(Transform user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Armor>("Item/" + name));
                Destroy(gameObject);
            }
        }
    }
}