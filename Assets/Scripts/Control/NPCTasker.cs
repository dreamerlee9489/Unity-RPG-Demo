using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using App.SO;
using App.Manager;
using App.Items;
using App.Data;

namespace App.Control
{
    [System.Serializable]
    public class Task
    {
        public string name = "", chName = "";
        public string description = "";
        public string npcName = "";
        public string targetPath = "";
        public int bounty = 0, exp = 0;
        public int count = 0, number = 1;
        public bool accepted = false;
        public Dictionary<string, int> rewards = null;
        GameObject target = null;

        public Task() {}
        public Task(string name, string chName, string npcName, string targetPath, int bounty, int exp, int number, Dictionary<string, int> rewards = null)
        {
            this.name = name;
            this.chName = chName;
            this.npcName = npcName;
            this.targetPath = targetPath;
            this.bounty = bounty;
            this.exp = exp;
            this.number = number;
            this.rewards = rewards;
            target = Resources.LoadAsync(targetPath).asset as GameObject;
        }

        public GameObject GetTarget() 
        { 
            if(target == null)
                target = Resources.LoadAsync(targetPath).asset as GameObject;
            return target; 
        }
        public void UpdateProgress(int count)
        {
            this.count += count;
            UIManager.Instance.taskPanel.UpdatePanel(this);
        }
    }

    public class NPCTasker : NPCController
    {
        public int index = 0;
        public List<Task> tasks = new List<Task>();

        protected override void Awake()
        {
            base.Awake();
            tasks.Add(new Task("KillUndeadKnight", "消灭不死骑士", nickName, "Entity/Enemy/Enemy_UndeadKnight_01", 500, 100, 1, new Dictionary<string, int>(){
                    { "Weapon/Weapon_Sword_Broad", 1 }, { "Potion/Potion_Meat_01", 10 }
            }));
            tasks.Add(new Task("CollectMeat", "收集烤牛排", nickName, "Items/Potion/Potion_Meat_01", 500, 200, 12, new Dictionary<string, int>() {
                    { "Weapon/Weapon_Axe_Large_01", 1 }
            }));
            actions.Add("GiveTask_KillUndeadKnight", () =>
            {
                GiveTask(tasks[0]);
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward();
            });
            actions.Add("GiveTask_CollectMeat", () =>
            {
                GiveTask(tasks[1]);
            });
            actions.Add("GiveReward_CollectMeat", () =>
            {
                GiveReward();
            });
            NPCData data = JsonManager.Instance.LoadData<NPCData>(InventoryManager.Instance.playerData.nickName + "_NPCData_" + name);
            index = data == null ? 0 : data.index;
            for (int i = 0; i < InventoryManager.Instance.ongoingTasks.Count; i++)
            {
                if (InventoryManager.Instance.ongoingTasks[i].npcName == nickName)
                {
                    tasks[index] = InventoryManager.Instance.ongoingTasks[i];
                    break;
                }
            }
        }

        protected void GiveTask(Task task)
        {
            task.accepted = true;
            InventoryManager.Instance.ongoingTasks.Add(task);
            UIManager.Instance.taskPanel.Add(task);
            if (task.GetTarget().GetComponent<Item>() != null)
                task.UpdateProgress(InventoryManager.Instance.Count(task.GetTarget().GetComponent<Item>()));
        }

        protected void GiveReward()
        {
            InventoryManager.Instance.ongoingTasks.Remove(tasks[index]);
            UIManager.Instance.taskPanel.Remove(tasks[index]);
            if (tasks[index].GetTarget().GetComponent<Item>() != null)
                for (int i = 0; i < tasks[index].number; i++)
                    InventoryManager.Instance.GetItem(tasks[index].GetTarget().GetComponent<Item>()).RemoveFromInventory();
            foreach (var pair in tasks[index].rewards)
            {
                Item item = null;
                for (int i = 0; i < pair.Value; i++)
                {
                    item = Resources.Load<Item>("Items/" + pair.Key);
                    item.AddToInventory();
                }
                UIManager.Instance.messagePanel.Print("[系统]  获得奖励：" + item.itemConfig.itemName + " * " + pair.Value, Color.yellow);
            }
            GameManager.Instance.player.GetExprience(tasks[index].exp);
            InventoryManager.Instance.playerData.golds += tasks[index].bounty;
            UIManager.Instance.goldPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
            NPCData data = new NPCData();
            data.index = ++index;
            data.currentHP = GetComponent<CombatEntity>().currentHP;
            data.currentMP = GetComponent<CombatEntity>().currentMP;
            data.position = new Vector(transform.position);
            JsonManager.Instance.SaveData(data, InventoryManager.Instance.playerData.nickName + "_NPCData_" + name);
        }

        public void CheckTaskProgress()
        {
            if (index == tasks.Count)
                dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + name).asset as DialogueConfig;
            else
            {
                if (!tasks[index].accepted)
                    dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + tasks[index].name + "_Pending").asset as DialogueConfig;
                else
                {
                    if (tasks[index].count < tasks[index].number)
                        dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + tasks[index].name + "_Undone").asset as DialogueConfig;
                    else
                        dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + tasks[index].name + "_Completed").asset as DialogueConfig;
                }
            }
        }
    }
}
