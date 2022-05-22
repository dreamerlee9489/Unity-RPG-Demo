using System.Collections.Generic;
using UnityEngine;
using App.Items;

namespace App.SO
{
    public enum ProfessionType { NONE, WARRIOR, ARCHER, MEGA, SHAMAN }

    public class Attribute
    {
        public float thisLevelHP = 100, thisLevelMP = 100, thisLevelATK = 10, thisLevelDEF = 2, upLevelEXP = 100;
    }

    [CreateAssetMenu(fileName = "ProfessionConfig_", menuName = "Unity RPG Project/ProfessionConfig", order = 7)]
    public class ProfessionConfig : ScriptableObject
    {
        public ProfessionType professionType = ProfessionType.NONE;
        public float initialHP = 100, initialMP = 100, initialATK = 10, initialDEF = 2, initialEXP = 100;
        public float factorHP = 0.25f, factorMP = 0.25f, factorATK = 0.5f, factorDEF = 0.5f, factorEXP = 1f;
        public List<Skill> skillTree = new List<Skill>();

        public Attribute GetAttributeByLevel(int level)
        {
            Attribute attribute = new Attribute();
            attribute.thisLevelHP = level == 1 ? initialHP : initialHP * Mathf.Pow(1 + factorHP, level - 1);
            attribute.thisLevelMP = level == 1 ? initialMP : initialMP * Mathf.Pow(1 + factorMP, level - 1);
            attribute.thisLevelATK = level == 1 ? initialATK : initialATK * Mathf.Pow(1 + factorATK, level - 1);
            attribute.thisLevelDEF = level == 1 ? initialDEF : initialDEF * Mathf.Pow(1 + factorDEF, level - 1);
            attribute.upLevelEXP = level == 1 ? initialEXP : initialEXP * Mathf.Pow(1 + factorEXP, level - 1);
            return attribute;
        }
    }
}
