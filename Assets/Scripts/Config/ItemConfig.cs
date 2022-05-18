using UnityEngine;
using App.UI;
using App.Items;

namespace App.Config
{
    public abstract class ItemConfig : ScriptableObject
    {
        public string itemName = "";
        public string description = "";
        public GameItem item = null;
        public ItemUI itemUI = null;
        public ItemType itemType = ItemType.WEAPON;
    }
}
