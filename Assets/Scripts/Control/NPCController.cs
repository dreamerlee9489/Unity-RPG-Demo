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
        public string name = "", chName = "";
        public string description = "";
        public int count = 0, number = 1;
        public bool isCompleted = false;
        public int bounty = 0;
        public float exp = 0;
        public GameObject target { get; set; }
        public NPCController npc { get; set; }
        public Dictionary<string, int> rewards { get; set; }

        public Quest(string name, string chName, int bounty, float exp, int number, GameObject target, NPCController npc, Dictionary<string, int> rewards = null)
        {
            this.name = name;
            this.chName = chName;
            this.bounty = bounty;
            this.exp = exp;
            this.number = number;
            this.target = target;
            this.npc = npc;
            this.rewards = rewards;
        }

        public void UpdateProgress(int count)
        {
            this.count += count;
            UIManager.Instance.questPanel.UpdateQuest(this);
            if (this.count >= this.number && !isCompleted)
                npc.CompleteQuest();
        }
    }

    [RequireComponent(typeof(MoveEntity))]
    public class NPCController : MonoBehaviour
    {
        List<Quest> quests = new List<Quest>();
        public DialogueConfig dialogueConfig = null;
        public Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public int index { get; set; }

        void Awake()
        {
            actions.Add("GiveQuest_KillUndeadKnight", () =>
            {
                GiveQuest("KillUndeadKnight", "消灭不死骑士", 500, 100, 1, GameManager.Instance.objects["不死骑士"], new Dictionary<string, int>(){
                    { "Weapon_Sword_Broad", 1 }, { "Potion_Meat_01", 10 }
                });
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward("CollectMeat");
            });
            actions.Add("GiveQuest_CollectMeat", () =>
            {
                GiveQuest("CollectMeat", "收集烤牛排",  500, 200, 12, GameManager.Instance.objects["香喷喷的烤牛排"], new Dictionary<string, int>() {
                    { "Weapon_Axe_Large_01", 1 }
                });
            });
            actions.Add("GiveReward_CollectMeat", () =>
            {
                GiveReward();
            });
            index = 0;
            dialogueConfig = index == 0 ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_KillUndeadKnight_Start").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + quests[index].name + "_Start").asset as DialogueConfig;
        }

        void GiveQuest(string thisName, string chName, int bounty, int exp, int number, GameObject target, Dictionary<string, int> rewards)
        {
            quests.Add(new Quest(thisName, chName, bounty, exp, number, target, this, rewards));
            GameManager.Instance.registeredQuests.Add(quests[index]);
            UIManager.Instance.questPanel.Add(quests[index]);
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + thisName + "_Accept").asset as DialogueConfig;
            if (target.GetComponent<Item>() != null)
                quests[index].UpdateProgress(InventoryManager.Instance.Count(target.GetComponent<Item>()));
        }

        void GiveReward(string nextName = null)
        {
            GameManager.Instance.registeredQuests.Remove(quests[index]);
            UIManager.Instance.questPanel.Remove(quests[index]);
            dialogueConfig = nextName != null ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + nextName + "_Start").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + name).asset as DialogueConfig;
            if(quests[index].target.GetComponent<Item>() != null)
                for (int i = 0; i < quests[index].number; i++)
                    InventoryManager.Instance.Get(quests[index].target.GetComponent<Item>()).Use(GetComponent<CombatEntity>());
            foreach (var pair in quests[index].rewards)
            {
                Item item = null;
                for (int i = 0; i < pair.Value; i++)
                {
                    item = Resources.Load<Item>("Items/" + pair.Key);
                    item.AddToInventory();
                }
                UIManager.Instance.messagePanel.ShowMessage("[系统]  获得奖励：" + item.itemConfig.itemName + " * " + pair.Value);
            }
            InventoryManager.Instance.playerData.golds += quests[index].bounty;
            UIManager.Instance.goldPanel.UpdatePanel();
            index++;
        }

        public void CompleteQuest()
        {
            quests[index].isCompleted = true;
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + quests[index].name + "_Submit").asset as DialogueConfig;
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}
