using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Unity RPG Project/WeaponConfig", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        public float weaponLength = 1f;
        public int weaponAtk = 10;
    }
}