using UnityEngine;
using UnityEngine.AI;
using Game.SO;

namespace Game.Control
{
    public class MoveEntity : MonoBehaviour, IReceiver
    {
        Animator animator = null;
        NavMeshAgent agent = null;
        public AbilityConfig abilityConfig = null;

        public void ExecuteAction(RaycastHit hit)
        {
            if (GetComponent<CombatEntity>().target != null)
                GetComponent<CombatEntity>().CancelAction();
            agent.destination = hit.point;
        }

        public void CancelAction()
        {
            agent.destination = transform.position;
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = false;
            agent.autoBraking = false;
            agent.speed = abilityConfig.runSpeed;
            agent.stoppingDistance = abilityConfig.attackRadius;
        }

        void Update()
        {
            animator.SetFloat("moveSpeed", transform.InverseTransformVector(agent.velocity).z);
        }
    }
}
