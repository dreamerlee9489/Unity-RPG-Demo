using UnityEngine;
using UnityEngine.AI;

namespace Game.Control
{
    public class CombatEntity : MonoBehaviour, IReceiver
    {
        float sqrViewRadius = 36f, sqrAttackRadius = 2.25f;
        NavMeshAgent agent = null;
        Animator animator = null;
        [HideInInspector] public Transform target = null;


        public bool CanSee(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude <= sqrViewRadius && Vector3.Dot(transform.forward, direction.normalized) > 0f)
                return true;
            return false;
        }

        public bool CanAttack(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude <= sqrAttackRadius && Vector3.Dot(transform.forward, direction.normalized) > 0.5f)
                return true;
            return false;
        }

        public void ExecuteAction(RaycastHit hit)
        {
            target = hit.transform;
            agent.destination = target.position;
            transform.LookAt(target);
        }

        public void CancelAction()
        {
            if (target != null)
            {
                target = null;
                agent.destination = transform.position;
            }
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (target != null && !CanAttack(target))
            {
                agent.destination = target.position;
                transform.LookAt(target);
            }
        }
    }
}
