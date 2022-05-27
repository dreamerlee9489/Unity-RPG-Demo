using System.Collections.Generic;

namespace App.Data
{   
    [System.Serializable]
    public class PlayerData
    {
        public string nickName = "冒险家";
        public int golds = 5000;
        public string sceneName = "";
        public Vector position;
        public Vector rotation;
        public List<ItemData> itemDatas = new List<ItemData>();
        public PlayerData() {}
    }
}
