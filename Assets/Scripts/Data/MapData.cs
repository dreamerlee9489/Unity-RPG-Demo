using System.Collections.Generic;

namespace App.Data
{
    [System.Serializable]
    public class MapData
    {
        public List<ItemData> mapItemDatas = null;
        public Dictionary<string, EntityData> mapEntityDatas = null;
        public MapData() {}
    }
}