using UnityEngine;

namespace App.SO
{
    [CreateAssetMenu(fileName = "ArmorConfig_", menuName = "Unity RPG Project/ArmorConfig", order = 3)]
    public class ArmorConfig : ItemConfig
    {
        public float hp = 20f, def = 1f;
    }
}
