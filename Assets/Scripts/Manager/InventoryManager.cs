using System.Collections.Generic;
using UnityEngine;
using App.Items;
using App.UI;
using App.Data;
using App.Control;

namespace App.Manager
{
    public class InventoryManager
    {
        static InventoryManager instance = new InventoryManager();
        InventoryManager() { inventory = GameManager.Instance.player.GetComponent<PlayerController>().inventory; }
        public static InventoryManager Instance => instance;
        public List<GameItem> items = new List<GameItem>();
        public List<ItemUI> itemUIs = new List<ItemUI>();
        public PlayerData playerData = new PlayerData();
        public Transform inventory { get; set; }

        public void Add(GameItem item, ItemUI itemUI)
        {
            items.Add(item);
            itemUIs.Add(itemUI);
            item.itemUI = itemUI;
            itemUI.item = item;
            ItemSlot itemSlot = itemUI.transform.parent.parent.GetComponent<ItemSlot>();
            itemSlot.itemUI = itemUI;
            itemSlot.itemType = item.config.itemType;
            item.gameObject.SetActive(false);
            item.containerType = ContainerType.BAG;
            item.collider.enabled = false;
            item.rigidbody.useGravity = false;
            item.rigidbody.isKinematic = true;
        }

        public void Remove(GameItem item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i] == item)
                {
                    items.RemoveAt(i);
                    itemUIs.RemoveAt(i);
                }
            }
        }
    }
}