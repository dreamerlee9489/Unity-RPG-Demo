using System;
using System.Collections.Generic;
using UnityEngine;
using App.SO;

namespace App.Control
{
    [System.Serializable]
    public class Quest
    {
        public string name = "";
        public string description = "";
        public int current = 0, total = 1;
        public bool isCompleted = false;
        public Dictionary<string, int> rewards = null;
        [HideInInspector] public string target = null;
        [HideInInspector] public NPCController npc = null;

        public void UpdateProgress(int count)
        {
            current += count;
            GameManager.Instance.canvas.questPanel.UpdateQuest(this);
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
        [HideInInspector] public int index = 0;

        void Awake()
        {
            actions.Add("GiveQuest_KillUndeadKnight", () =>
            {
                GiveQuest("Enemy_UndeadKnight", null);
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward();
            });
        }

        void GiveQuest(string target, Dictionary<string, int> rewards)
        {
            quests[index].npc = this;
            quests[index].target = target;
            quests[index].rewards = rewards;
            GameManager.Instance.ongoingQuests.Add(quests[index]);
            GameManager.Instance.canvas.questPanel.Add(quests[index]);
            GameManager.Instance.entities[target].GetComponent<CombatEntity>().isQuestTarget = true;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/GivenQuest_KillUndeadKnight").asset as DialoguesConfig;
            Debug.Log("领取任务: " + quests[index].name);
        }

        void GiveReward()
        {
            quests[index].current -= quests[index].total;
            GameManager.Instance.ongoingQuests.Remove(quests[index]);
            GameManager.Instance.canvas.questPanel.Remove(quests[index]);
            GameManager.Instance.entities[quests[index].target].GetComponent<CombatEntity>().isQuestTarget = false;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/Quest_KillUndeadKnight").asset as DialoguesConfig;
            Debug.Log("获取奖励: " + quests[index].name);
            index = 0;
        }

        public void CompleteQuest(Quest quest)
        {
            quest.isCompleted = true;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/CompletedQuest_KillUndeadKnight").asset as DialoguesConfig;
            Debug.Log("任务已完成: " + quest.current);
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}
