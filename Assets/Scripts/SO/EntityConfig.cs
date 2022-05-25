using UnityEngine;
using App.AI;

namespace App.SO
{
    [CreateAssetMenu(fileName = "EntityConfig_", menuName = "Unity RPG Project/EntityConfig", order = 0)]
    public class EntityConfig : ScriptableObject
    {
        public string nickName = "";
        public string description = "";
        public CombatEntity entity = null;
        public float walkSpeed = 1.558401f, runSpeed = 5.662316f, walkFactor = 1f, runFactor = 1f;
        public float viewRadius = 6f, fleeRadius = 6f, stopDistance = 1f;
    }
}
