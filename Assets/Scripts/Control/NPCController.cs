using System;
using System.Collections.Generic;
using UnityEngine;
using App.SO;
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
    public abstract class NPCController : MonoBehaviour
    {
        protected int index = 0;
        protected List<Quest> quests = new List<Quest>();
        public Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public DialogueConfig dialogueConfig { get; set; }

        protected virtual void Awake()
        {
            index = 0;
            GetComponent<CombatEntity>().healthBar.gameObject.SetActive(false);
        }

        protected void GiveQuest(string thisName, string chName, int bounty, int exp, int number, GameObject target, Dictionary<string, int> rewards)
        {
            quests.Add(new Quest(thisName, chName, bounty, exp, number, target, this, rewards));
            GameManager.Instance.registeredQuests.Add(quests[index]);
            UIManager.Instance.questPanel.Add(quests[index]);
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + thisName + "_Accept").asset as DialogueConfig;
            if (target.GetComponent<Item>() != null)
                quests[index].UpdateProgress(InventoryManager.Instance.Count(target.GetComponent<Item>()));
        }

        protected void GiveReward(string nextName = null)
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
            GameManager.Instance.player.GetExprience(quests[index].exp);
            InventoryManager.Instance.playerData.golds += quests[index].bounty;
            UIManager.Instance.goldPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
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
