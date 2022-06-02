using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using App.Items;
using App.UI;
using App.Data;
using App.SO;
using App.Control;

namespace App.Manager
{
    public class InventoryManager
    {
        public static string PLAYER_ACCOUNT_PATH = Application.persistentDataPath + "/PlayerAccount.txt";
        static InventoryManager instance = new InventoryManager();
        public static InventoryManager Instance => instance;
        public Transform bag = null;
        public Transform skills = null;
        public PlayerData playerData = null;
        public List<Item> items = new List<Item>();
        public List<ItemUI> itemUIs = new List<ItemUI>();
        public List<Quest> ongoingQuests = new List<Quest>();
        InventoryManager() { GameManager.Instance.onSavingData += SaveData; }

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
            item.GetComponent<MeshRenderer>().enabled = false;
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

        public void SaveData()
        {
            playerData.itemDatas.Clear();
            playerData.sceneName = SceneManager.GetActiveScene().name;
            playerData.professionPath = "Config/Profession/" + GameManager.Instance.player.professionConfig.name;
            playerData.level = GameManager.Instance.player.level;
            playerData.currentHP = GameManager.Instance.player.currentHP;
            playerData.currentMP = GameManager.Instance.player.currentMP;
            playerData.currentEXP = GameManager.Instance.player.currentEXP;
            playerData.position = new Vector(GameManager.Instance.player.transform.position);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].itemData = new ItemData();
                items[i].itemData.level = items[i].level;
                items[i].itemData.containerType = items[i].containerType;
                items[i].itemData.path = "Items/" + items[i].GetType().Name + "/" + items[i].itemConfig.item.name;
                playerData.itemDatas.Add(items[i].itemData);
            }
            playerData.ongoingQuests.Clear();
            playerData.ongoingQuests.AddRange(ongoingQuests);
            BinaryManager.Instance.SaveData(playerData, playerData.nickName + "_PlayerData");
            BinaryManager.Instance.SaveData(playerData, "CurrentPlayerData");
            if(File.Exists(PLAYER_ACCOUNT_PATH))
            {
                using(StreamReader reader = File.OpenText(PLAYER_ACCOUNT_PATH))
                {
                    string name = "";
                    while((name = reader.ReadLine()) != null)
                    {
                        if(name == playerData.nickName)
                            return;
                    }
                }
            } 
            using (StreamWriter sw = File.AppendText(PLAYER_ACCOUNT_PATH))
                sw.WriteLine(playerData.nickName);
        }

        public void LoadData(PlayerData playerData)
        {
            for (int i = 0; i < playerData.itemDatas.Count; i++)
                Resources.Load<Item>(playerData.itemDatas[i].path).LoadToContainer(playerData.itemDatas[i]);
            for (int i = 0; i < playerData.ongoingQuests.Count; i++)
            {
                ongoingQuests.Add(playerData.ongoingQuests[i]);
                UIManager.Instance.questPanel.Add(playerData.ongoingQuests[i]);
            }
            GameManager.Instance.player.professionConfig = Resources.Load<ProfessionConfig>(playerData.professionPath);
            UIManager.Instance.goldPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
            UIManager.Instance.hudPanel.UpdatePanel();
        }
    }
}