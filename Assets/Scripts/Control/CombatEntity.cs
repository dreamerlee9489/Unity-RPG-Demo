using UnityEngine;
using UnityEngine.AI;

namespace Game.Control
{
    public class CombatEntity : MonoBehaviour, IReceiver
    {
        float sqrViewRadius = 36f, sqrAttackRadius = 2.25f;
        NavMeshAgent agent = null;
        Animator animator = null;
        public Transform target { get; private set; }

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
            transform.LookAt(target);
            agent.destination = target.position;
        }

        public void CancelAction()
        {
            target = null;
            agent.destination = transform.position;
            animator.SetBool("attack", false);
        }

        void AttackL()
        {
            print("左勾拳");
        }

        void AttackR()
        {
            print("右勾拳");
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (target != null)
            {
                if (CanAttack(target))
                {
                    animator.SetBool("attack", true);
                }
                else
                {
                    agent.destination = target.position;
                    transform.LookAt(target);
                    animator.SetBool("attack", false);
                }
            }
        }
    }
}
