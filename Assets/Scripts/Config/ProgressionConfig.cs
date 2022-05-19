using UnityEngine;

namespace App.Config
{
    [System.Serializable]
    public class Progression
    {
        public float thisLevelHp = 100, thisLevelMp = 100, thisLevelAtk = 10, thisLevelDef = 2, nextLevelExp = 100;
    }

    [CreateAssetMenu(fileName = "ProgressionConfig_", menuName = "Unity RPG Project/ProgressionConfig", order = 6)]
    public class ProgressionConfig : ScriptableObject
    {
        public float initialHp = 100, initialMp = 100, initialAtk = 10, initialDef = 2, initialExp = 100;
        public float hpFactor = 0.25f, mpFactor = 0.25f, atkFactor = 0.5f, defFactor = 0.5f, expFactor = 1f;

        public Progression GetProgressionByLevel(int level)
        {
            Progression progression = new Progression();
            progression.thisLevelHp = level == 1 ? initialHp : initialHp * Mathf.Pow(1 + hpFactor, level - 1);
            progression.thisLevelMp = level == 1 ? initialMp : initialMp * Mathf.Pow(1 + mpFactor, level - 1);
            progression.thisLevelAtk = level == 1 ? initialAtk : initialAtk * Mathf.Pow(1 + atkFactor, level - 1);
            progression.thisLevelDef = level == 1 ? initialDef : initialDef * Mathf.Pow(1 + defFactor, level - 1);
            progression.nextLevelExp = level == 1 ? initialExp : initialExp * Mathf.Pow(1 + expFactor, level - 1);
            return progression;
        }
    }
}
