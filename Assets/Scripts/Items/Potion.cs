using App.Manager;
using UnityEngine;

namespace App.Items
{
    public class Potion : GameItem
    {
        public override void Use(Transform user)
        {
            print("Eat: " + config.itemName);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Instantiate(config.item, GameManager.Instance.player.transform), Instantiate(config.itemUI, GameManager.Instance.canvas.bagPanel.GetFirstValidSlot().transform));
                Destroy(gameObject);
            }
        }
    }
}