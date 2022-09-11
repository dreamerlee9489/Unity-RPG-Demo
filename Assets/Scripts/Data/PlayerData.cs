using System.Collections.Generic;
using Control;
using Control.NPC;

namespace Data
{
    [System.Serializable]
    public class PlayerData
    {
        public string nickName = "冒险家";
        public string sceneName = "";
        public string professionPath = "";
        public int level = 1;
        public int golds = 5000;
        public float currentHP = 0;
        public float currentMP = 0;
        public float currentEXP = 0;
        public Vector position;
        public List<ItemData> itemDatas = new List<ItemData>();
        public List<Quest> ongoingQuests = new List<Quest>();

        public PlayerData()
        {
        }

        public PlayerData(string nickName, int golds)
        {
            this.nickName = nickName;
            this.golds = golds;
        }
    }
}