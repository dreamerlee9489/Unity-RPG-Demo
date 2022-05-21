using System;
using System.Collections.Generic;
using UnityEngine;
using App.SO;
using App.Manager;
using App.Items;

namespace App.Control
{
    [System.Serializable]
    public class Task
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

        public Task(string name, string chName, int bounty, float exp, int number, GameObject target, NPCController npc, Dictionary<string, int> rewards = null)
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
            UIManager.Instance.taskPanel.UpdateTask(this);
            if (this.count >= this.number && !isCompleted)
                npc.CompleteTask();
        }
    }

    [RequireComponent(typeof(MoveEntity))]
    public abstract class NPCController : MonoBehaviour
    {
        protected int index = 0;
        protected List<Task> tasks = new List<Task>();
        public Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public Transform goods { get; set; }
        public DialogueConfig dialogueConfig { get; set; }

        protected virtual void Awake()
        {
            index = 0;
            goods = transform.GetChild(1);
            GetComponent<CombatEntity>().healthBar.gameObject.SetActive(false);
        }

        protected void GiveTask(string thisName, string chName, int bounty, int exp, int number, GameObject target, Dictionary<string, int> rewards)
        {
            tasks.Add(new Task(thisName, chName, bounty, exp, number, target, this, rewards));
            GameManager.Instance.registeredTasks.Add(tasks[index]);
            UIManager.Instance.taskPanel.Add(tasks[index]);
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + thisName + "_Accept").asset as DialogueConfig;
            if (target.GetComponent<Item>() != null)
                tasks[index].UpdateProgress(InventoryManager.Instance.Count(target.GetComponent<Item>()));
        }

        protected void GiveReward(string nextName = null)
        {
            GameManager.Instance.registeredTasks.Remove(tasks[index]);
            UIManager.Instance.taskPanel.Remove(tasks[index]);
            dialogueConfig = nextName != null ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + nextName + "_Start").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + name).asset as DialogueConfig;
            if(tasks[index].target.GetComponent<Item>() != null)
                for (int i = 0; i < tasks[index].number; i++)
                    InventoryManager.Instance.Get(tasks[index].target.GetComponent<Item>()).RemoveFromInventory();
            foreach (var pair in tasks[index].rewards)
            {
                Item item = null;
                for (int i = 0; i < pair.Value; i++)
                {
                    item = Resources.Load<Item>("Items/" + pair.Key);
                    item.AddToInventory();
                }
                UIManager.Instance.messagePanel.ShowMessage("[系统]  获得奖励：" + item.itemConfig.itemName + " * " + pair.Value, Color.yellow);
            }
            GameManager.Instance.player.GetExprience(tasks[index].exp);
            InventoryManager.Instance.playerData.golds += tasks[index].bounty;
            UIManager.Instance.goldPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
            index++;
        }

        public void CompleteTask()
        {
            tasks[index].isCompleted = true;
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + tasks[index].name + "_Submit").asset as DialogueConfig;
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}
