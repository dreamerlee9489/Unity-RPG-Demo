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
        public string name = "";
        public string description = "";
        public int current = 0, total = 1;
        public bool isCompleted = false;
        public int bounty = 0;
        public float exp = 0;
        public GameObject target = null;
        public NPCController npc { get; set; }
        public Dictionary<string, int> rewards { get; set; }

        public Quest(string name, int total, int bounty, float exp, GameObject target, NPCController npc, Dictionary<string, int> rewards = null)
        {
            this.name = name;
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
                npc.CompleteQuest(this);
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
                GiveQuest("消灭不死骑士", "DialogueConfig_KillUndeadKnight_Accept", Resources.LoadAsync("Character/Enemy_UndeadKnight").asset as GameObject, new Dictionary<string, int>(){
                    { "Weapon_Sword_Broad", 1 }, { "Potion_Meat_01", 10 }
                }, 500, 100);
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward("DialogueConfig_CollectMeat_Start");
            });
            actions.Add("GiveQuest_CollectMeat", () =>
            {
                GiveQuest("收集烤牛排", "DialogueConfig_CollectMeat_Accept", Resources.LoadAsync("Items/Potion_Meat_01").asset as GameObject, new Dictionary<string, int>() {
                    { "Weapon_Axe_Large", 1 }
                }, 500, 200);
            });
            actions.Add("GiveReward_CollectMeat", () =>
            {
                GiveReward("");
            });
        }

        void GiveQuest(string name, string nextPath, GameObject target, Dictionary<string, int> rewards, int total = 0, int bounty = 0, int exp = 0)
        {
            quests.Add(new Quest(name, total, bounty, exp, target, this, null));
            GameManager.Instance.ongoingQuests.Add(quests[index]);
            UIManager.Instance.questPanel.Add(quests[index]);
            //GameManager.Instance.entities[target].isQuestTarget = true;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/" + nextPath).asset as DialogueConfig;
        }

        void GiveReward(string nextPath)
        {
            quests[index].current -= quests[index].total;
            GameManager.Instance.ongoingQuests.Remove(quests[index]);
            UIManager.Instance.questPanel.Remove(quests[index]);
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
            dialoguesConfig = nextPath != "" ? Resources.LoadAsync("Config/Dialogue/" + nextPath).asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/" + GetComponent<CombatEntity>().nickName).asset as DialogueConfig;
            index++;
        }

        public void CompleteQuest(Quest quest)
        {
            quest.isCompleted = true;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_KillUndeadKnight_Submit").asset as DialogueConfig;
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}
