using UnityEngine;

namespace App.SO
{
    public enum SkillType { A, B, C, D }

    [CreateAssetMenu(fileName = "SkillConfig_", menuName = "Unity RPG Project/SkillConfig", order = 6)]
    public class SkillConfig : ItemConfig
    {
        public SkillType skillType = SkillType.A;
        public float initialHP = 0, initialMP = 0, initialATK = 0, initialDEF = 0, initialCD = 0;
        public float factorHP = 0.25f, factorMP = 0.25f, factorATK = 0.25f, factorDEF = 0.25f, factorCD = -0.25f;
    }
}