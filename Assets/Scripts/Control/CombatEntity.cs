using UnityEngine;
using UnityEngine.AI;
using Game.SO;

namespace Game.Control
{
    [RequireComponent(typeof(MoveEntity))]
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
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            abilityConfig = GetComponent<MoveEntity>().abilityConfig;
            sqrViewRadius = Mathf.Pow(abilityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(abilityConfig.unarmRadius + weaponConfig.weaponRadius, 2);
            currHP = abilityConfig.maxHP;
            currMP = abilityConfig.maxMP;
            currAtk = abilityConfig.unarmAtk + weaponConfig.weaponAtk;
            agent.stoppingDistance = abilityConfig.unarmRadius + weaponConfig.weaponRadius;
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
