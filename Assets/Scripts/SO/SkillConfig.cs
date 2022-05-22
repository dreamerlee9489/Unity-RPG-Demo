using UnityEngine;

namespace App.SO
{
    public enum SkillType { A, B, C, D }

    public class SkillAttribute
    {
        public float thisLevelHP = 100, thisLevelMP = 100, thisLevelATK = 10, thisLevelDEF = 2, upLevelLMT = 1;
    }

    [CreateAssetMenu(fileName = "SkillConfig_", menuName = "Unity RPG Project/SkillConfig", order = 6)]
    public class SkillConfig : ItemConfig
    {
        public SkillType skillType = SkillType.A;
        public float initialHP = 0, initialMP = 0, initialATK = 0, initialDEF = 0, initialLV = 1;
        public float factorHP = 0.25f, factorMP = 0.25f, factorATK = 0.5f, factorDEF = 0.5f, factorLV = 0.5f;
        public float initialCD = 0;
        public int maxLV = 5;

        public SkillAttribute GetAttributeBySkillLevel(int skillLevel)
        {
            SkillAttribute attribute = new SkillAttribute();
            attribute.thisLevelHP = skillLevel == 1 ? initialHP : initialHP * Mathf.Pow(1 + factorHP, skillLevel - 1);
            attribute.thisLevelMP = skillLevel == 1 ? initialMP : initialMP * Mathf.Pow(1 + factorMP, skillLevel - 1);
            attribute.thisLevelATK = skillLevel == 1 ? initialATK : initialATK * Mathf.Pow(1 + factorATK, skillLevel - 1);
            attribute.thisLevelDEF = skillLevel == 1 ? initialDEF : initialDEF * Mathf.Pow(1 + factorDEF, skillLevel - 1);
            attribute.upLevelLMT = skillLevel == 1 ? initialLV : initialLV * Mathf.Pow(1 + factorLV, skillLevel - 1);
            return attribute;
        }

        public bool CheckUserLevel(int userLevel)
        {
            switch(skillType)
            {
                case SkillType.A:
                break;
                case SkillType.B:
                break;
                case SkillType.C:
                break;
                case SkillType.D:
                break;
            }
            return false;
        }
    }
}