using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "Project/AbilityConfig", order = 0)]
    public class AbilityConfig : ScriptableObject
    {
        public float walkSpeed = 1.558401f, runSpeed = 5.662316f, speedFactor = 0.8f;
        public float viewRadius = 6f, armRadius = 1f, weaponRadius = 1f, fleeRadius = 6f;
    }
}
