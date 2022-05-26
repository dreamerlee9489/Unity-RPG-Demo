using System.Collections.Generic;
using App.Items;

namespace App.Data
{   
    public class PlayerData
    {
        public string nickName = "";
        public int golds = 5000;
        public List<ItemData> itemDatas = new List<ItemData>();
    }
}
