using System;
using System.Collections.Generic;
using UnityEngine;
using App.SO;
using App.Manager;

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
        [HideInInspector] public NPCController npc = null;

        public void UpdateProgress(int count)
        {
            current += count;
            if (current >= total && !isCompleted)
                npc.CompleteQuest(this);
        }
    }

    public class NPCController : MonoBehaviour
    {
        public DialoguesConfig dialoguesConfig = null;
        public List<Quest> quests = new List<Quest>();
        public Dictionary<string, Action> events = new Dictionary<string, Action>();
        int index = 0;

        public void CompleteQuest(Quest quest)
        {
            quest.isCompleted = true;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/CompletedQuest_KillUndeadKnight").asset as DialoguesConfig;
            Debug.Log("任务已完成: " + quest.current);
        }

        public void EventTrigger(string action)
        {
            if (events.ContainsKey(action))
                events[action].Invoke();
        }

        void Awake()
        {
            events.Add("GiveQuest_KillUndeadKnight", () =>
            {
                GiveQuest(null);
            });
            events.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward();
            });
        }

        void GiveQuest(Dictionary<string, int> rewards)
        {
            quests[index].npc = this;
            quests[index].rewards = rewards;
            GameManager.Instance.player.quests.Add(quests[index]);
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/GivenQuest_KillUndeadKnight").asset as DialoguesConfig;
            Debug.Log("领取任务: " + quests[index].name);
        }

        void GiveReward()
        {
            index = 0;
            dialoguesConfig = Resources.LoadAsync("Config/Dialogue/Quest_KillUndeadKnight").asset as DialoguesConfig;
            Debug.Log("获取奖励: " + quests[index].name);
        }
    }
}
