using Game.SO;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Control
{
    public class MoveEntity : MonoBehaviour, IReceiver
    {
        public float walkSpeed = 1.558401f, runSpeed = 5.662316f, stopDistance = 1.5f;
        Animator animator = null;
        NavMeshAgent agent = null;

        public void ExecuteAction(RaycastHit hit)
        {
            if (GetComponent<CombatEntity>().target != null)
                GetComponent<CombatEntity>().CancelAction();
            agent.speed = runSpeed;
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
            agent.speed = runSpeed;
            agent.isStopped = false;
            agent.autoBraking = false;
            agent.stoppingDistance = stopDistance;
        }

        void Update()
        {
            animator.SetFloat("moveSpeed", transform.InverseTransformVector(agent.velocity).z);
        }
    }
}
