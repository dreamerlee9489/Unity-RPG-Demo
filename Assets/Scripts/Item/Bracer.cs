using App.Manager;
using App.SO;
using App.UI;
using UnityEngine;

namespace App.Item
{
    public class Bracer : GameItem
    {
        public Bracer(ItemConfig itemConfig) : base(itemConfig)
        {
            itemType = ItemType.BRACER;
        }

        public override void Use(Transform user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Bracer>("Item/" + name));
                Destroy(gameObject);
            }
        }
    }
}