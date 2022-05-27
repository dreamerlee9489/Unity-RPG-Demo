using System.Collections.Generic;

namespace App.Data
{
    [System.Serializable]
    public class MapData
    {
        public Dictionary<string, ItemData> pickupItems = new Dictionary<string, ItemData>();
        public Dictionary<string, EntityData> deadEnemies = new Dictionary<string, EntityData>();
        public MapData() {}
    }
}