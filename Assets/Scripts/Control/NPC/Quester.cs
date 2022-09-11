using System.Collections.Generic;
using Data;
using Items;
using Manager;
using SO;
using UnityEngine;

namespace Control.NPC
{
    public class Quester : NPCController
    {
        public int index { get; set; }
        public List<Quest> quests { get; set; }

        void SaveData()
        {
            NPCData data = new NPCData
            {
                index = index,
                currentHP = GetComponent<Entity>().currentHP,
                currentMP = GetComponent<Entity>().currentMP,
                position = new Vector(transform.position)
            };
            BinaryManager.Instance.SaveData(data, InventoryManager.Instance.playerData.nickName + "_NPCData_" + name);
        }

        protected override void Awake()
        {
            base.Awake();
            quests = new List<Quest>();
            GameManager.Instance.onSavingData += SaveData;
            quests.Add(new Quest("KillUndeadKnight", "消灭不死骑士", nickName, "Entity/Enemy/Enemy_UndeadKnight_01", 500, 100,
                1, new Dictionary<string, int>()
                {
                    { "Weapon/Weapon_Sword_Broad", 1 }, { "Potion/Potion_Meat_01", 10 }
                }));
            quests.Add(new Quest("CollectMeat", "收集烤牛排", nickName, "Items/Potion/Potion_Meat_01", 500, 200, 12,
                new Dictionary<string, int>()
                {
                    { "Weapon/Weapon_Axe_Large_01", 1 }
                }));
            actions.Add("GiveQuest_KillUndeadKnight", () => { GiveQuest(quests[0]); });
            actions.Add("GiveReward_KillUndeadKnight", () => { GiveReward(); });
            actions.Add("GiveQuest_CollectMeat", () => { GiveQuest(quests[1]); });
            actions.Add("GiveReward_CollectMeat", () => { GiveReward(); });
            NPCData data =
                BinaryManager.Instance.LoadData<NPCData>(InventoryManager.Instance.playerData.nickName + "_NPCData_" +
                                                         name);
            index = data == null ? 0 : data.index;
            for (int i = 0; i < InventoryManager.Instance.ongoingQuests.Count; i++)
            {
                if (InventoryManager.Instance.ongoingQuests[i].npcName == nickName)
                {
                    quests[index] = InventoryManager.Instance.ongoingQuests[i];
                    break;
                }
            }
        }

        void OnDestroy()
        {
            GameManager.Instance.onSavingData -= SaveData;
        }

        protected void GiveQuest(Quest task)
        {
            task.accepted = true;
            InventoryManager.Instance.ongoingQuests.Add(task);
            UIManager.Instance.questPanel.Add(task);
            if (task.Target.GetComponent<Item>() != null)
                task.UpdateProgress(InventoryManager.Instance.Count(task.Target.GetComponent<Item>()));
        }

        protected void GiveReward()
        {
            InventoryManager.Instance.ongoingQuests.Remove(quests[index]);
            UIManager.Instance.questPanel.Remove(quests[index]);
            if (quests[index].Target.GetComponent<Item>() != null)
                for (int i = 0; i < quests[index].number; i++)
                    InventoryManager.Instance.GetItem(quests[index].Target.GetComponent<Item>()).RemoveFromInventory();
            foreach (var pair in quests[index].rewards)
            {
                Item item = null;
                for (int i = 0; i < pair.Value; i++)
                {
                    item = Resources.Load<Item>("Items/" + pair.Key);
                    item.AddToInventory();
                }

                UIManager.Instance.messagePanel.Print("[系统]  获得奖励：" + item.itemConfig.itemName + " * " + pair.Value,
                    Color.yellow);
            }

            GameManager.Instance.player.GetExprience(quests[index].exp);
            InventoryManager.Instance.playerData.golds += quests[index].bounty;
            UIManager.Instance.goldPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
            index++;
        }

        public void CheckQuestProgress()
        {
            if (index == quests.Count)
                dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + name).asset as DialogueConfig;
            else
            {
                if (!quests[index].accepted)
                    dialogueConfig =
                        Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + quests[index].name + "_Pending")
                            .asset as DialogueConfig;
                else
                {
                    if (quests[index].count < quests[index].number)
                        dialogueConfig =
                            Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + quests[index].name + "_Undone")
                                .asset as DialogueConfig;
                    else
                        dialogueConfig =
                            Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + quests[index].name + "_Completed")
                                .asset as DialogueConfig;
                }
            }
        }
    }
}