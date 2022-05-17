using App.Manager;
using App.SO;
using App.UI;
using UnityEngine;

namespace App.Item
{
    public class Potion : GameItem
    {
        public Potion(ItemConfig itemConfig) : base(itemConfig)
        {
            itemType = ItemType.POTION;
        }

        public override void Use(Transform user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Potion>("Item/" + name));
                Destroy(gameObject);
            }
        }
    }
}