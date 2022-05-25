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
            UIManager.Instance.taskPanel.UpdatePanel(this);
            npc.dialogueConfig = this.count >= number ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + npc.tasks[npc.index].name + "_Submit").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + npc.tasks[npc.index].name + "_Accept").asset as DialogueConfig;
        }
    }
    
	public class NPCTasker : NPCController
	{
		protected override void Awake()
		{
            base.Awake();
			actions.Add("GiveTask_KillUndeadKnight", () =>
            {
                GiveTask("KillUndeadKnight", "消灭不死骑士", 500, 100, 1, Resources.LoadAsync("Entity/Enemy/Enemy_UndeadKnight_01").asset as GameObject, new Dictionary<string, int>(){
                    { "Weapon/Weapon_Sword_Broad", 1 }, { "Potion/Potion_Meat_01", 10 }
                });
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward("CollectMeat");
            });
            actions.Add("GiveTask_CollectMeat", () =>
            {
                GiveTask("CollectMeat", "收集烤牛排",  500, 200, 12, Resources.LoadAsync("Items/Potion/Potion_Meat_01").asset as GameObject, new Dictionary<string, int>() {
                    { "Weapon/Weapon_Axe_Large_01", 1 }
                });
            });
            actions.Add("GiveReward_CollectMeat", () =>
            {
                GiveReward();
            });
            dialogueConfig = index == 0 ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_KillUndeadKnight_Start").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + tasks[index].name + "_Start").asset as DialogueConfig;
		}

        protected void GiveTask(string thisName, string chName, int bounty, int exp, int number, GameObject target, Dictionary<string, int> rewards)
        {
            tasks.Add(new Task(thisName, chName, bounty, exp, number, target, this, rewards));
            GameManager.Instance.ongoingTasks.Add(tasks[index]);
            UIManager.Instance.taskPanel.Add(tasks[index]);
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + thisName + "_Accept").asset as DialogueConfig;
            if (target.GetComponent<Item>() != null)
                tasks[index].UpdateProgress(InventoryManager.Instance.Count(target.GetComponent<Item>()));
        }

        protected void GiveReward(string nextName = null)
        {
            GameManager.Instance.ongoingTasks.Remove(tasks[index]);
            UIManager.Instance.taskPanel.Remove(tasks[index]);
            dialogueConfig = nextName != null ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + nextName + "_Start").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + name).asset as DialogueConfig;
            if(tasks[index].target.GetComponent<Item>() != null)
                for (int i = 0; i < tasks[index].number; i++)
                    InventoryManager.Instance.GetItem(tasks[index].target.GetComponent<Item>()).RemoveFromInventory();
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
            index++;
        }
	}
}
