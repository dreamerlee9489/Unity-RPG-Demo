using System.Collections.Generic;

namespace App.Data
{
    [System.Serializable]
    public class MapData
    {
        public List<ItemData> newDropItemDatas = new List<ItemData>();
        public List<ItemData> pickedupItemDatas = new List<ItemData>();
        public MapData() {}
    }
}