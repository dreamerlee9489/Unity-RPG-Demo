using System.Collections.Generic;

namespace App.Data
{   
    [System.Serializable]
    public class PlayerData
    {
        public string nickName = "冒险家";
        public string sceneName = "";
        public int level = 1;
        public int golds = 5000;
		public float currentHP = 0;
        public float currentMP = 0;
        public float currentEXP = 0;
        public Vector position;
        public List<ItemData> itemDatas = new List<ItemData>();
        public PlayerData() {}
        public PlayerData(string nickName, int golds)
        {
            this.nickName = nickName;
            this.golds = golds;
        }
    }
}
