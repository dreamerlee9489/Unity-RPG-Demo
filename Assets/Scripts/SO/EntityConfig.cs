using UnityEngine;
using Control;
using Items;

namespace SO
{
    [CreateAssetMenu(fileName = "EntityConfig_", menuName = "Unity RPG Project/EntityConfig", order = 0)]
    public class EntityConfig : ScriptableObject
    {
        public string nickName = "";
        public string description = "";
        public int level = 1;
        public Entity entity = null;
        public Weapon weapon = null;
        public float walkSpeed = 1.558401f, runSpeed = 5.662316f, walkFactor = 1f, runFactor = 1f;
        public float viewRadius = 6f, fleeRadius = 6f, stopDistance = 1f;
    }
}