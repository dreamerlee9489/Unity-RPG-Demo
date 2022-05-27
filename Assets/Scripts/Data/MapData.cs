using System.Collections.Generic;

namespace App.Data
{
    [System.Serializable]
    public class MapData
    {
        public List<ItemData> mapItemDatas = new List<ItemData>();
        public Dictionary<string, EntityData> mapEntityDatas = new Dictionary<string, EntityData>();
        public MapData() {}
    }
}