using UnityEngine;
using UnityEngine.AI;
using Game.SO;

namespace Game.Control
{
    public class CombatEntity : MonoBehaviour, IReceiver
    {
        float sqrViewRadius = 36f, sqrAttackRadius = 2.25f;
        Animator animator = null;
        NavMeshAgent agent = null;
        AbilityConfig abilityConfig = null;
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

        public void StartAttack()
        {
            animator.SetBool("attack", true);
        }

        public void StopAttack()
        {
            animator.SetBool("attack", false);
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            abilityConfig = GetComponent<MoveEntity>().abilityConfig;
            sqrViewRadius = Mathf.Pow(abilityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(abilityConfig.armRadius + abilityConfig.weaponRadius, 2);
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

        void AttackL()
        {
            print("左勾拳");
        }

        void AttackR()
        {
            print("右勾拳");
        }
    }
}
