using UnityEngine;
using UnityEngine.AI;
using App.SO;

namespace App.Control
{
    public class CombatEntity : MonoBehaviour, ICmdReceiver, IMsgReceiver
    {
        Animator animator = null;
        NavMeshAgent agent = null;
        AbilityConfig abilityConfig = null;
        public float sqrViewRadius = 36f, sqrAttackRadius = 2.25f;
        public float currHp = 100f, currDef = 1f, currAtk = 10f;        
        public EquipmentConfig weaponConfig = null;
        public HealthBar healthBar = null;
        [HideInInspector] public bool isDead = false;
        [HideInInspector] public bool isQuestTarget = false;
        [HideInInspector] public Transform target = null;

        void Awake()
        {
            if(CompareTag("Enemy"))
                healthBar = transform.GetChild(0).GetComponent<HealthBar>();            
            GetComponent<Collider>().isTrigger = true;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            abilityConfig = GetComponent<MoveEntity>().abilityConfig;
            agent.stoppingDistance = abilityConfig.stopDistance;
            agent.radius = 0.5f;
            currHp = abilityConfig.hp;
            currDef = abilityConfig.def;
            currAtk = abilityConfig.atk + weaponConfig.atk;
            sqrViewRadius = Mathf.Pow(abilityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
        }

        void AttackL() => TakeDamage(target);
        void AttackR() => TakeDamage(target);

        void TakeDamage(Transform target, float atkFactor = 1)
        {
            if(target != null)
            {
                CombatEntity defender = target.GetComponent<CombatEntity>();
                defender.currHp = Mathf.Max(defender.currHp + defender.currDef - currAtk * atkFactor, 0);
                defender.healthBar.UpdateBar(new Vector3(defender.currHp / defender.abilityConfig.hp, 1, 1));
                if (defender.currHp <= 0)
                {
                    defender.Death();
                    CancelAction();
                }
            }
        }

        void Death()
        {
            isDead = true;
            animator.SetBool("attack", false);
            animator.SetBool("death", true);
            agent.radius = 0;
            GetComponent<Collider>().enabled = false;
            if(isQuestTarget)
            {
                for (int i = 0; i < GameManager.Instance.ongoingQuests.Count; i++)
                {
                    Quest quest = GameManager.Instance.ongoingQuests[i];
                    if(quest.target == name)
                        quest.UpdateProgress(1);
                }
            }
        }

        public void ExecuteAction(Vector3 point) {}
        public void ExecuteAction(Transform target)
        {
            if (!target.GetComponent<CombatEntity>().isDead)
            {
                this.target = target;
                transform.LookAt(target);
                if (!CanAttack(target))
                {
                    agent.destination = target.position;
                    animator.SetBool("attack", false);
                }
                else
                    animator.SetBool("attack", true);
            }
        }

        public void CancelAction()
        {
            target = null;
            animator.SetBool("attack", false);
        }

        public bool CanSee(Transform target)
        {
            if(!target.GetComponent<CombatEntity>().isDead)
            {
                Vector3 direction = target.position - transform.position;
                if (direction.sqrMagnitude <= sqrViewRadius)
                return true;
            }
            return false;
        }

        public bool CanAttack(Transform target)
        {
            if(!target.GetComponent<CombatEntity>().isDead)
            {
                Vector3 direction = target.position - transform.position;
                if (direction.sqrMagnitude <= sqrAttackRadius && Vector3.Dot(transform.forward, direction.normalized) > 0.5f)
                    return true;
            }
            return false;
        }

        public bool HandleMessage(Telegram telegram)
        {
            print(Time.unscaledTime + "s: " + gameObject.name + " recv: " + telegram.ToString());
            return GetComponent<FSM.FiniteStateMachine>().HandleMessage(telegram);
        }
    }
}
