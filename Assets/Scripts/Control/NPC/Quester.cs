using System.Collections.Generic;
using System.Linq;
using Data;
using Items;
using Manager;
using SO;
using UnityEngine;

namespace Control.NPC
{
    public class Quester : NPCController
    {
        private int _index = 0;
        private List<Quest> _quests = new();

        private void SaveData()
        {
            NPCData data = new NPCData
            {
                index = _index,
                currentHP = GetComponent<Entity>().currentHP,
                currentMP = GetComponent<Entity>().currentMP,
                position = new Vector(transform.position)
            };
            BinaryManager.Instance.SaveData(data, InventoryManager.Instance.playerData.nickName + "_NPCData_" + name);
        }

        protected override void Awake()
        {
            base.Awake();
            _quests = new List<Quest>();
            GameManager.Instance.onSavingData += SaveData;
            _quests.Add(new Quest("KillUndeadKnight", "消灭不死骑士", nickName, "Entity/Enemy/Enemy_UndeadKnight_01", 500, 100,
                1, new Dictionary<string, int>()
                {
                    { "Weapon/Weapon_Sword_Broad", 1 }, { "Potion/Potion_Meat_01", 10 }
                }));
            _quests.Add(new Quest("CollectMeat", "收集烤牛排", nickName, "Items/Potion/Potion_Meat_01", 500, 200, 12,
                new Dictionary<string, int>()
                {
                    { "Weapon/Weapon_Axe_Large_01", 1 }
                }));
            actions.Add("GiveQuest_KillUndeadKnight", () => { GiveQuest(_quests[0]); });
            actions.Add("GiveReward_KillUndeadKnight", GiveReward);
            actions.Add("GiveQuest_CollectMeat", () => { GiveQuest(_quests[1]); });
            actions.Add("GiveReward_CollectMeat", GiveReward);
            NPCData data =
                BinaryManager.Instance.LoadData<NPCData>(InventoryManager.Instance.playerData.nickName + "_NPCData_" +
                                                         name);
            _index = data?.index ?? 0;
            foreach (var quest in InventoryManager.Instance.ongoingQuests.Where(quest => quest.npcName == nickName))
            {
                _quests[_index] = quest;
                break;
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.onSavingData -= SaveData;
        }

        private void GiveQuest(Quest task)
        {
            task.accepted = true;
            InventoryManager.Instance.ongoingQuests.Add(task);
            UIManager.Instance.questPanel.Add(task);
            if (task.Target.GetComponent<Item>() != null)
                task.UpdateProgress(InventoryManager.Instance.Count(task.Target.GetComponent<Item>()));
        }

        private void GiveReward()
        {
            InventoryManager.Instance.ongoingQuests.Remove(_quests[_index]);
            UIManager.Instance.questPanel.Remove(_quests[_index]);
            if (_quests[_index].Target.GetComponent<Item>() != null)
                for (int i = 0; i < _quests[_index].number; i++)
                    InventoryManager.Instance.GetItem(_quests[_index].Target.GetComponent<Item>()).RemoveFromInventory();
            foreach (var pair in _quests[_index].rewards)
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

            GameManager.Instance.player.GetExprience(_quests[_index].exp);
            InventoryManager.Instance.playerData.golds += _quests[_index].bounty;
            UIManager.Instance.goldPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
            _index++;
        }

        public void CheckQuestProgress()
        {
            if (_index == _quests.Count)
                dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + name).asset as DialogueConfig;
            else
            {
                if (!_quests[_index].accepted)
                    dialogueConfig =
                        Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + _quests[_index].name + "_Pending")
                            .asset as DialogueConfig;
                else
                {
                    if (_quests[_index].count < _quests[_index].number)
                        dialogueConfig =
                            Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + _quests[_index].name + "_Undone")
                                .asset as DialogueConfig;
                    else
                        dialogueConfig =
                            Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + _quests[_index].name + "_Completed")
                                .asset as DialogueConfig;
                }
            }
        }
    }
}