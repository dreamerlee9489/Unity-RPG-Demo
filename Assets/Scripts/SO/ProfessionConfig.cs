using System.Collections.Generic;
using UnityEngine;
using Items;

namespace SO
{
    public enum ProfessionType
    {
        NONE,
        WARRIOR,
        ARCHER,
        MEGA,
        SHAMAN
    }

    public class ProfessionAttribute
    {
        public float hp = 100, mp = 100, atk = 10, def = 2, exp = 100;
    }

    [CreateAssetMenu(fileName = "ProfessionConfig_", menuName = "Unity RPG Project/ProfessionConfig", order = 7)]
    public class ProfessionConfig : ScriptableObject
    {
        public string professionName = "";
        public ProfessionType professionType = ProfessionType.NONE;
        public WeaponType weaponType = WeaponType.NONE;
        public float initialHP = 100, initialMP = 100, initialATK = 10, initialDEF = 2, initialEXP = 100;
        public float factorHP = 0.25f, factorMP = 0.25f, factorATK = 0.5f, factorDEF = 0.5f, factorEXP = 1f;
        public List<Skill> skillTree = new List<Skill>();

        public ProfessionAttribute GetProfessionAttribute(int level)
        {
            ProfessionAttribute attribute = new ProfessionAttribute();
            attribute.hp = level == 1 ? initialHP : initialHP * Mathf.Pow(1 + factorHP, level - 1);
            attribute.mp = level == 1 ? initialMP : initialMP * Mathf.Pow(1 + factorMP, level - 1);
            attribute.atk = level == 1 ? initialATK : initialATK * Mathf.Pow(1 + factorATK, level - 1);
            attribute.def = level == 1 ? initialDEF : initialDEF * Mathf.Pow(1 + factorDEF, level - 1);
            attribute.exp = level == 1 ? initialEXP : initialEXP * Mathf.Pow(1 + factorEXP, level - 1);
            return attribute;
        }
    }
}