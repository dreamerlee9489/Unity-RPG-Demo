using System;
using System.Collections.Generic;
using UnityEngine;
using App.Config;
using App.Manager;
using App.Items;
using App.UI;

namespace App.Control
{
    [System.Serializable]
    public class Quest
    {
        public string name = "";
        public string description = "";
        public string target { get; set; }
        public int current = 0, total = 1;
        public bool isCompleted = false;
        public int bounty = 0;
        public Dictionary<string, int> rewards { get; set; }
        public NPCController npc { get; set; }

        public void UpdateProgress(int count)
        {
            current += count;
            UIManager.Instance.questPanel.UpdateQuest(this);
            if (current >= total && !isCompleted)
                npc.CompleteQuest(this);
        }
    }

    [RequireComponent(typeof(MoveEntity))]
    public class NPCController : MonoBehaviour
    {
        public DialoguesConfig dialoguesConfig = null;
        public List<Quest> quests = new List<Quest>();
        public Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public int index { get; set; }

        void Awake()
        {
            actions.Add("GiveQuest_KillUndeadKnight", () =>
            {
                GiveQuest("不死骑士", new Dictionary<string, int>(){
                    { "Weapon_Sword_Broad", 1 }, { "Potion_Meat_01", 10 }
                }, 500);
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward();
            });
        }

        void GiveQuest(string target, Dictionary<string, int> rewards, int bounty = 0)
        {
            quests[index].npc = this;
            quests[index].target = target;
            quests[index].rewards = rewards;
            quests[index].bounty = bounty;
            GameManager.Instance.ongoingQuests.Add(quests[index]);
            UIManager.Instance.questPanel.Add(quests[index]);
            GameManager.Instance.entities[target].isQuestTarget = true;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_KillUndeadKnight_Accept").asset as DialoguesConfig;
            Debug.Log("领取任务: " + quests[index].name);
        }

        void GiveReward()
        {
            quests[index].current -= quests[index].total;
            GameManager.Instance.ongoingQuests.Remove(quests[index]);
            UIManager.Instance.questPanel.Remove(quests[index]);
            GameManager.Instance.entities[quests[index].target].isQuestTarget = false;
            foreach (var pair in quests[index].rewards)
            {
                for (int i = 0; i < pair.Value; i++)
                    (Resources.Load<GameItem>("Items/" + pair.Key)).AddToInventory();
            }
            InventoryManager.Instance.playerData.golds += quests[index].bounty;
            UIManager.Instance.goldPanel.UpdatePanel();
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_KillUndeadKnight_Start").asset as DialoguesConfig;
            Debug.Log("获取奖励: " + quests[index].name);
            index = 0;
        }

        public void CompleteQuest(Quest quest)
        {
            quest.isCompleted = true;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_KillUndeadKnight_Submit").asset as DialoguesConfig;
            Debug.Log("任务已完成: " + quest.current);
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}
