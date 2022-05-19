using System;
using System.Collections.Generic;
using UnityEngine;
using App.Config;
using App.Manager;
using App.Items;

namespace App.Control
{
    [System.Serializable]
    public class Quest
    {
        public string questName = "", chName = "";
        public string description = "";
        public int current = 0, total = 1;
        public bool isCompleted = false;
        public int bounty = 0;
        public float exp = 0;
        public GameObject target = null;
        public NPCController npc { get; set; }
        public Dictionary<string, int> rewards { get; set; }

        public Quest(string questName, string chName, int total, int bounty, float exp, GameObject target, NPCController npc, Dictionary<string, int> rewards = null)
        {
            this.questName = questName;
            this.chName = chName;
            this.total = total;
            this.bounty = bounty;
            this.exp = exp;
            this.target = target;
            this.npc = npc;
            this.rewards = rewards;
        }

        public void UpdateProgress(int count)
        {
            current += count;
            UIManager.Instance.questPanel.UpdateQuest(this);
            if (current >= total && !isCompleted)
                npc.CompleteQuest();
        }
    }

    [RequireComponent(typeof(MoveEntity))]
    public class NPCController : MonoBehaviour
    {
        public DialogueConfig dialoguesConfig = null;
        public Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public List<Quest> quests = new List<Quest>();
        public int index { get; set; }

        void Awake()
        {
            actions.Add("GiveQuest_KillUndeadKnight", () =>
            {
                GiveQuest("KillUndeadKnight", "消灭不死骑士", Resources.Load<GameObject>("Character/Enemy_UndeadKnight_01"), new Dictionary<string, int>(){
                    { "Weapon_Sword_Broad", 1 }, { "Potion_Meat_01", 10 }
                }, 1, 500, 100);
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward("CollectMeat");
            });
            actions.Add("GiveQuest_CollectMeat", () =>
            {
                GiveQuest("CollectMeat", "收集烤牛排", Resources.Load<GameObject>("Items/Potion_Meat_01"), new Dictionary<string, int>() {
                    { "Weapon_Axe_Large_01", 1 }
                }, 2, 500, 200);
            });
            actions.Add("GiveReward_CollectMeat", () =>
            {
                GiveReward("");
            });
        }

        void GiveQuest(string questName, string chName, GameObject target, Dictionary<string, int> rewards, int total = 0, int bounty = 0, int exp = 0)
        {
            quests.Add(new Quest(questName, chName, total, bounty, exp, target, this, rewards));
            GameManager.Instance.ongoingQuests.Add(quests[index]);
            UIManager.Instance.questPanel.Add(quests[index]);
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + questName + "_Accept").asset as DialogueConfig;
            if (target.GetComponent<CombatEntity>() != null)
                GameManager.Instance.entities[target.GetComponent<CombatEntity>().nickName].isQuestTarget = true;
            else
            {
                quests[index].target.GetComponent<GameItem>().isQuestTarget = true;
                quests[index].UpdateProgress(InventoryManager.Instance.CountItem(target.GetComponent<GameItem>()));
            }
        }

        void GiveReward(string questName)
        {
            quests[index].current -= quests[index].total;
            GameManager.Instance.ongoingQuests.Remove(quests[index]);
            UIManager.Instance.questPanel.Remove(quests[index]);
            dialoguesConfig = questName != "" ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + questName + "_Start").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + name).asset as DialogueConfig;
            foreach (var pair in quests[index].rewards)
            {
                GameItem item = null;
                for (int i = 0; i < pair.Value; i++)
                {
                    item = Resources.Load<GameItem>("Items/" + pair.Key);
                    item.AddToInventory();
                }
                UIManager.Instance.messagePanel.ShowMessage("[系统]  获得奖励：" + item.config.itemName + " * " + pair.Value);
            }
            InventoryManager.Instance.playerData.golds += quests[index].bounty;
            UIManager.Instance.goldPanel.UpdatePanel();
            index++;
        }

        public void CompleteQuest()
        {
            quests[index].isCompleted = true;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + quests[index].questName + "_Submit").asset as DialogueConfig;
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}
