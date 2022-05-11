using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "Unity RPG Project/AbilityConfig", order = 0)]
    public class AbilityConfig : ScriptableObject
    {
        public float walkSpeed = 1.558401f, runSpeed = 5.662316f, speedFactor = 0.75f;
        public float viewRadius = 6f, fleeRadius = 6f, stopDistance = 1f;
        public int level = 1, maxHP = 100, maxMP = 100, unarmAtk = 10;
    }
}
