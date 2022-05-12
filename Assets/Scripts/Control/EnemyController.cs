using UnityEngine;
using UnityEngine.AI;
using Game.Control.BT;

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
                animator.SetBool("attack", false);
                return false;
            });
            root.AddChildren(retreat, wander, chase);
            retreat.AddChildren(new Condition(() =>
            {
                return false;
            }), new Action(() =>
            {
                if (moveEntity.Flee(player.position))
                    return Status.SUCCESS;
                return Status.RUNNING;
            }), new Action(() =>
            {
                return Status.SUCCESS;
            }));
            wander.AddChildren(new UntilSuccess(canSeePlayer), new Action(() =>
            {
                wanderTimer += Time.deltaTime;
                if(wanderTimer >= 6f)
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
                    transform.LookAt(player);
                    animator.SetBool("attack", true);
                    return Status.SUCCESS;
                }
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
