using System.Collections.Generic;
using UnityEngine;

namespace App.SO
{
    public enum SkillType { A, B, C, D }
    public enum ControlType { NONE, SLOW, STUNN, KNOCK }
    public class SkillAttribute
    {
        public float hp = 0, mp = 0, atk = 0, def = 0, cd = 0, controlTime = 0, controlRate = 0;
    }

    [CreateAssetMenu(fileName = "SkillConfig_", menuName = "Unity RPG Project/SkillConfig", order = 6)]
    public class SkillConfig : ItemConfig
    {
        public SkillType skillType = SkillType.A;
        public ControlType controlType = ControlType.SLOW;
        public float controlRate = 0.5f;
        public float initialHP = 0, initialMP = 0, initialATK = 0, initialDEF = 0, initialCD = 0, initialControlTime = 2;
        public float factorHP = 0.25f, factorMP = 0.25f, factorATK = 0.25f, factorDEF = 0.25f, factorCD = 0.25f, factorControlTime = 0.25f;
        public List<int> levelRequires = new List<int>();

        public SkillAttribute GetSkillAttributeByLevel(int level)
        {
            SkillAttribute skillAttribute = new SkillAttribute();
            skillAttribute.hp = initialHP + factorHP * (level - 1);
            skillAttribute.mp = initialMP + factorMP * (level - 1);
            skillAttribute.cd = initialCD - factorCD * (level - 1);
            skillAttribute.atk = initialATK + factorATK * (level - 1);
            skillAttribute.def = initialDEF + factorDEF * (level - 1);
            skillAttribute.controlTime = initialControlTime + factorControlTime * (level - 1);
            return null;
        }
    }
}