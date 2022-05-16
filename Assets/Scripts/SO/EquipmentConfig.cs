using UnityEngine;
using App.UI;

namespace App.SO
{
    [CreateAssetMenu(fileName = "NewEquipmentConfig", menuName = "Unity RPG Project/EquipmentConfig", order = 0)]
    public class EquipmentConfig : ScriptableObject
    {
        public string itemName = "";
        public string description = "";
        public ItemUI itemUI = null;
        public float hp = 20f, atk = 10f, def = 1f;
    }
}
