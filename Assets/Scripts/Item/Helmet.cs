using UnityEngine;
using App.SO;
using App.Manager;
using App.UI;

namespace App.Item
{
    public class Helmet : GameItem
    {
        public Helmet(ItemConfig itemConfig) : base(itemConfig)
        {
            itemType = ItemType.HELMET;
        }

        public override void Use(Transform user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Helmet>("Item/" + name));
                Destroy(gameObject);
            }
        }
    }
}