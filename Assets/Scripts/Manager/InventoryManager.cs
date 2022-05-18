using System.Collections.Generic;
using UnityEngine;
using App.Items;
using App.UI;

namespace App.Manager
{
    public class InventoryManager
    {
        static InventoryManager instance = new InventoryManager();
        public static InventoryManager Instance => instance;
        public List<GameItem> items = new List<GameItem>();
        public List<ItemUI> itemUIs = new List<ItemUI>();

        public void Add(GameItem item, ItemUI itemUI)
        {
            items.Add(item);
            itemUIs.Add(itemUI);
            item.itemUI = itemUI;
            itemUI.item = item;
            ItemSlot itemSlot = itemUI.transform.parent.GetComponent<ItemSlot>();
            itemSlot.itemUI = itemUI;
            itemSlot.itemType = item.config.itemType;
            item.gameObject.SetActive(false);
            item.containerType = ContainerType.BAG;
            item.GetComponent<Collider>().enabled = false;
        }
    }
}
