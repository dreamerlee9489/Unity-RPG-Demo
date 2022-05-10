using UnityEngine;
using UnityEngine.AI;
using Game.Control.BehaviourTree;

namespace Game.Control
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public class EnemyController : MonoBehaviour
    {
        float wanderTimer = 6f;
        Animator animator = null;
        NavMeshAgent agent = null;
        Transform player = null;
        MoveEntity moveEntity = null;
        CombatEntity combatEntity = null;
        Selector root = new Selector();

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
            player = GameObject.FindWithTag("Player").transform;
            Sequence retreat = new Sequence();
            Parallel wander = new Parallel();
            Parallel chase = new Parallel();
            Condition canSeePlayer = new Condition(() =>
            {
                if (combatEntity.CanSee(player))
                    return true;
                return false;
            });
            root.AddChildren(retreat, wander, chase);
            retreat.AddChildren(new Condition(() =>
            {
                print("不需要逃跑");
                return false;
            }), new Action(() =>
            {
                if (moveEntity.Flee(player.position))
                    return Status.SUCCESS;
                return Status.RUNNING;
            }), new Action(() =>
            {
                print("已找到安全位置");
                return Status.SUCCESS;
            }));
            wander.AddChildren(new UntilSuccess(canSeePlayer), new Action(() =>
            {
                wanderTimer += Time.deltaTime;
                if(wanderTimer >= 2f)
                {
                    moveEntity.Wander();
                    wanderTimer = 0;
                }
                return Status.RUNNING;
            }));
            chase.AddChildren(new UntilFailure(canSeePlayer), new Action(() =>
            {
                if (moveEntity.Seek(player.position) <= agent.stoppingDistance)
                    return Status.SUCCESS;
                return Status.RUNNING;
            }), new Action(() =>
            {
                if (combatEntity.CanAttack(player))
                {
                    animator.SetBool("attack", true);
                    return Status.SUCCESS;
                }
                transform.LookAt(player);
                animator.SetBool("attack", false);
                return Status.RUNNING;
            }));
        }

        private void Update()
        {
            root.Execute();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, GetComponent<MoveEntity>().abilityConfig.viewRadius);
        }
    }
}
