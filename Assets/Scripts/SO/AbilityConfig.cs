using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "Project/AbilityConfig", order = 0)]
    public class AbilityConfig : ScriptableObject
    {
        public float walkSpeed = 1.558401f, runSpeed = 5.662316f;
        public float viewRadius = 6f, attackRadius = 1.5f;
    }
}
