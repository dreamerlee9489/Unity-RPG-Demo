using UnityEngine;
using UnityEngine.AI;
using Game.SO;

namespace Game.Control
{
    public class CombatEntity : MonoBehaviour, IReceiver
    {
        float sqrViewRadius = 36f, sqrAttackRadius = 2.25f;
        int currHP = 100, currMP = 100, currAtk = 10;
        Animator animator = null;
        NavMeshAgent agent = null;
        AbilityConfig abilityConfig = null;
        public Transform target { get; private set; }
        public WeaponConfig weaponConfig = null;

        public void ExecuteAction(RaycastHit hit)
        {
            agent.destination = hit.point;
            agent.stoppingDistance = abilityConfig.stopDistance + hit.transform.GetComponent<CombatEntity>().abilityConfig.stopDistance;
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
            target = hit.transform;
            transform.LookAt(target);
        }

        public void CancelAction()
        {
            target = null;
            agent.destination = transform.position;
            animator.SetBool("attack", false);
        }

        public bool CanSee(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude <= sqrViewRadius)
                return true;
            return false;
        }

        public bool CanAttack(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude <= sqrAttackRadius && Vector3.Dot(transform.forward, direction.normalized) > 0f)
                return true;
            return false;
        }

        void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            abilityConfig = GetComponent<MoveEntity>().abilityConfig;
            agent.stoppingDistance = abilityConfig.stopDistance;
            currHP = abilityConfig.maxHP;
            currMP = abilityConfig.maxMP;
            currAtk = abilityConfig.unarmAtk + weaponConfig.weaponAtk;
            sqrViewRadius = Mathf.Pow(abilityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
        }

        void AttackL()
        {
        }

        void AttackR()
        {
        }
    }
}
