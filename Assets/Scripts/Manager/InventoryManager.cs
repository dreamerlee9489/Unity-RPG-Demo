using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using App.Items;
using App.UI;
using App.Data;
using App.Control;

namespace App.Manager
{
    public class InventoryManager
    {
        static InventoryManager instance = null;
        public Transform bag = null;
        public Transform skills = null;
        public PlayerData playerData = null;
        public List<Item> items = null;
        public List<ItemUI> itemUIs = null;

        InventoryManager()
        {
            items = new List<Item>();
            itemUIs = new List<ItemUI>();
            bag = GameManager.Instance.player.GetComponent<PlayerController>().bag;
            skills = GameManager.Instance.player.GetComponent<PlayerController>().skills;
            playerData = GameManager.Instance.player.GetComponent<PlayerController>().playerData;
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
            item.cdTimer = item.itemConfig.cd;
            item.gameObject.SetActive(false);
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

        public void Save()
        {
            playerData.itemDatas.Clear();
            playerData.sceneName = SceneManager.GetActiveScene().name;
            playerData.position = new Vector(GameManager.Instance.player.transform.position);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].itemData = new ItemData();
                items[i].itemData.level = items[i].level;
                items[i].itemData.containerType = items[i].containerType;
                items[i].itemData.path = "Items/" + GetType().Name + "/" + items[i].itemConfig.item.name;
                playerData.itemDatas.Add(items[i].itemData);
            }
            JsonManager.Instance.SaveData(playerData, "PlayerData_" + playerData.nickName);
            JsonManager.Instance.SaveData(playerData, "CurrentPlayerData");
        }

        public void Load(PlayerData playerData)
        {
            for (int i = 0; i < playerData.itemDatas.Count; i++)
                Resources.Load<Item>(playerData.itemDatas[i].path).LoadToContainer(playerData.itemDatas[i]);
            GameManager.Instance.player.GetComponent<PlayerController>().playerData = playerData;
            UIManager.Instance.goldPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
            GameManager.Instance.player.gameObject.SetActive(false);
            GameManager.Instance.player.transform.position = new Vector3(playerData.position.x, playerData.position.y, playerData.position.z);
            SceneManager.LoadSceneAsync(playerData.sceneName);
            GameManager.Instance.player.gameObject.SetActive(true);
        }
    }
}