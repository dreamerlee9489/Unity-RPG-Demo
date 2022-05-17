using UnityEngine;
using App.Manager;
using App.SO;
using App.UI;

namespace App.Item
{
    public class Nacklace : GameItem
    {
        public Nacklace(ItemConfig itemConfig) : base(itemConfig)
        {
            itemType = ItemType.NECKLACE;
        }

        public override void Use(Transform user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Nacklace>("Item/" + name));
                Destroy(gameObject);
            }
        }
    }
}