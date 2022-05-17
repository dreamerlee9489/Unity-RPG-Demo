using App.UI;
using UnityEngine;

namespace App.SO
{
    [CreateAssetMenu(fileName = "WeaponConfig_", menuName = "Unity RPG Project/WeaponConfig", order = 2)]
    public class WeaponConfig : ItemConfig
    {
        public float atk = 10f;
        public RuntimeAnimatorController animatorController = null;
    }
}