using UnityEngine;
using App.UI;

namespace App.SO
{
    [CreateAssetMenu(fileName = "EquipmentConfig_", menuName = "Unity RPG Project/EquipmentConfig", order = 3)]
    public class EquipmentConfig : ItemConfig
    {
        public float hp = 20f, def = 1f;
    }
}
