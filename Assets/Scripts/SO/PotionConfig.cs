using UnityEngine;
using App.UI;

namespace App.SO
{
    [CreateAssetMenu(fileName = "PotionConfig_", menuName = "Unity RPG Project/PotionConfig", order = 4)]
    public class PotionConfig : ItemConfig
    {
        public float hp = 10f, mp = 10f, atk = 10f, def = 5f;
    }
}
