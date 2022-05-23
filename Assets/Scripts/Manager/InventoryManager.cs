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
        public static InventoryManager Instance => instance;
        public List<Item> items = new List<Item>();
        public List<ItemUI> itemUIs = new List<ItemUI>();
        public PlayerData playerData = new PlayerData();
        public Transform bag { get; set; }
        public Transform skills { get; set; }

        InventoryManager()
        {
            bag = GameManager.Instance.player.GetComponent<PlayerController>().bag;
            skills = GameManager.Instance.player.GetComponent<PlayerController>().skills;
        }

        public void Add(Item item, ItemUI itemUI)
        {
            items.Add(item);
            itemUIs.Add(itemUI);
            item.itemUI = itemUI;
            itemUI.item = item;
            ItemSlot itemSlot = itemUI.transform.parent.parent.GetComponent<ItemSlot>();
            itemSlot.itemUI = itemUI;
            itemSlot.itemType = item.itemConfig.itemType;
            item.gameObject.SetActive(false);
            item.containerType = ContainerType.BAG;
            item.collider.enabled = false;
            item.rigidbody.useGravity = false;
            item.rigidbody.isKinematic = true;
        }

        public void Remove(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == item)
                {
                    items.RemoveAt(i);
                    itemUIs.RemoveAt(i);
                }
            }
        }

        public int Count(Item item)
        {
            int count = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Equals(item))
                    count++;
            }
            return count;
        }

        public Item GetItem(Item item)
        {
            int index = HasItem(item);
            return index != -1 ? items[index] : null;
        }

        public int HasItem(Item item)
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].Equals(item) && items[i].containerType != ContainerType.EQUIPMENT)
                    return i;
            return -1;
        }

        public int HasSkill(Skill skill)
        {
            for (int i = 0; i < skills.childCount; i++)
                if ((skills.GetChild(i).GetComponent<Skill>()).Equals(skill))
                    return i;
            return -1;
        }

        public Skill GetSkill(Skill skill)
        {
            int index = HasSkill(skill);
            return index != -1 ? skills.GetChild(index).GetComponent<Skill>() : null;
        }
    }
}