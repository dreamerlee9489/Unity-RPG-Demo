using System.Collections.Generic;

namespace Data
{
    [System.Serializable]
    public class MapData
    {
        public List<ItemData> mapItemDatas = new List<ItemData>();
        public Dictionary<string, EnemyData> mapEnemyDatas = new Dictionary<string, EnemyData>();

        public MapData()
        {
        }
    }
}