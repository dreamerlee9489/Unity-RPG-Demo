using UnityEngine;
using App.UI;
using App.Items;

namespace App.SO
{
    public abstract class ItemConfig : ScriptableObject
    {
        public string itemName = "";
        public string description = "";
        public int price = 999;
        public Item item = null;
        public ItemUI itemUI = null;
        public ItemType itemType = ItemType.WEAPON;
    }
}
