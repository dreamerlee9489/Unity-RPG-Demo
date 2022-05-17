using App.Manager;
using App.SO;
using App.UI;
using UnityEngine;

namespace App.Item
{
    public class Pants : GameItem
    {
        public Pants(ItemConfig itemConfig) : base(itemConfig)
        {
            itemType = ItemType.PANTS;
        }

        public override void Use(Transform user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Pants>("Item/" + name));
                Destroy(gameObject);
            }
        }
    }
}