using App.Manager;
using App.SO;
using UnityEngine;
using App.UI;

namespace App.Item
{
    public class Boots : GameItem
    {
        public Boots(ItemConfig itemConfig) : base(itemConfig)
        {
            itemType = ItemType.BOOTS;
        }

        public override void Use(Transform user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Boots>("Item/" + name));
                Destroy(gameObject);
            }
        }
    }
}