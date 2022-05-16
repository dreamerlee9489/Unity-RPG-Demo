using UnityEngine;
using App.UI;

namespace App.SO
{
    [CreateAssetMenu(fileName = "NewPotionConfig", menuName = "Unity RPG Project/PotionConfig", order = 0)]
    public class PotionConfig : ScriptableObject
    {
        public string itemName = "";
        public string description = "";
        public ItemUI itemUI = null;
    }
}
