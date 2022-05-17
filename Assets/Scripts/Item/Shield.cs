using App.Manager;
using App.SO;
using App.UI;
using UnityEngine;

namespace App.Item
{
    public class Shield : GameItem
    {
        public Shield(ItemConfig itemConfig) : base(itemConfig)
        {
            itemType = ItemType.SHIELD;
        }

        public override void Use(Transform user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Shield>("Item/" + name));
                Destroy(gameObject);
            }
        }
    }
}