using UnityEngine;
using UnityEngine.AI;

namespace Game.Control
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveEntity : MonoBehaviour, IReceiver
    {
        public float walkSpeed = 1.558401f, runSpeed = 5.662316f;
        Animator animator = null;
        NavMeshAgent agent = null;

        public void ExecuteAction(Vector3 position)
        {
            agent.isStopped = false;
            agent.destination = position;
        }

        public void CancelAction()
        {
            agent.isStopped = true;
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = runSpeed;
        }

        void Update()
        {
            animator.SetFloat("moveSpeed", transform.InverseTransformVector(agent.velocity).z);
        }
    }
}
