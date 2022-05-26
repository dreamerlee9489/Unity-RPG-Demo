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
        static InventoryManager instance = null;
        public int golds = 5000;
        public List<Item> items = new List<Item>();
        public List<ItemUI> itemUIs = new List<ItemUI>();
        public List<ItemData> itemDatas = new List<ItemData>();
        public Transform bag { get; set; }
        public Transform skills { get; set; }

        InventoryManager()
        {
            bag = GameManager.Instance.player.GetComponent<PlayerController>().bag;
            skills = GameManager.Instance.player.GetComponent<PlayerController>().skills;
        }

        public static InventoryManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InventoryManager();
                return instance;
            }
        }

        public void SaveData()
        {
            for (int i = 0; i < items.Count; i++)
                itemDatas.Add(new ItemData(items[i].GetType().Name + "/" + items[i].itemConfig.itemPrefab.name, items[i].level, items[i].containerType));
            BinaryManager.Instance.SaveData(itemDatas, GetType().Name);
        }

        public void LoadData()
        {
            itemDatas = BinaryManager.Instance.LoadData(GetType().Name) as List<ItemData>;
            for (int i = 0; i < itemDatas.Count; i++)
            {
                switch (itemDatas[i].containerType)
                {              
                    case ContainerType.WORLD:
                        break;
                    case ContainerType.BAG:
                        Resources.Load<Item>(itemDatas[i].path).LoadToContainer(itemDatas[i].level, ContainerType.BAG);
                        break;
                    case ContainerType.EQUIPMENT:
                        Resources.Load<Item>(itemDatas[i].path).LoadToContainer(itemDatas[i].level, ContainerType.EQUIPMENT);
                        break;
                    case ContainerType.ACTION:
                        Resources.Load<Item>(itemDatas[i].path).LoadToContainer(itemDatas[i].level, ContainerType.ACTION);
                        break;
                    case ContainerType.SKILL:
                        Resources.Load<Item>(itemDatas[i].path).LoadToContainer(itemDatas[i].level, ContainerType.SKILL);
                        break;
                }
            }
        }

        public void Add(Item item, ItemUI itemUI, ContainerType containerType = ContainerType.BAG)
        {
            items.Add(item);
            itemUIs.Add(itemUI);
            item.itemUI = itemUI;
            itemUI.item = item;
            item.itemSlot = itemUI.transform.parent.parent.GetComponent<ItemSlot>();
            item.itemSlot.itemUI = itemUI;
            item.itemSlot.itemType = item.itemConfig.itemType;
            item.containerType = containerType;
            item.gameObject.SetActive(false);
            item.collider.enabled = false;
            item.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            item.cdTimer = item.itemConfig.cd;
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