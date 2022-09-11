using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Control.NPC
{
    [System.Serializable]
    public class Quest
    {
        public string name = "", chName = "";
        public string description = "";
        public string npcName = "";
        public string targetPath = "";
        public int bounty = 0, exp = 0;
        public int count = 0, number = 1;
        public bool accepted = false;
        public Dictionary<string, int> rewards = null;
        [System.NonSerialized] private GameObject _target = null;

        public GameObject Target
        {
            get
            {
                if (_target == null)
                    _target = Resources.LoadAsync(targetPath).asset as GameObject;
                return _target;
            }
        }

        public Quest()
        {
        }

        public Quest(string name, string chName, string npcName, string targetPath, int bounty, int exp, int number,
            Dictionary<string, int> rewards = null)
        {
            this.name = name;
            this.chName = chName;
            this.npcName = npcName;
            this.targetPath = targetPath;
            this.bounty = bounty;
            this.exp = exp;
            this.number = number;
            this.rewards = rewards;
            _target = Resources.LoadAsync(targetPath).asset as GameObject;
        }

        public void UpdateProgress(int count)
        {
            this.count += count;
            UIManager.Instance.questPanel.UpdatePanel(this);
        }
    }
}