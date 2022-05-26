using UnityEngine;
using UnityEngine.AI;
using App.Control.BT;
using App.Manager;

namespace App.Control
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public class BehaviorController : MonoBehaviour
    {
        float wanderTimer = 6f;
        Animator animator = null;
        NavMeshAgent agent = null;
        Transform player = null;
        MoveEntity moveEntity = null;
        CombatEntity combatEntity = null;
        Selector root = new Selector();

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
        }

        void Start()
        {
            player = GameManager.Instance.player.transform;
            Sequence retreat = new Sequence();
            Parallel wander = new Parallel();
            Parallel chase = new Parallel();
            Condition canSeePlayer = new Condition(() =>
            {
                if (combatEntity.CanSee(player))
                {
                    agent.speed = combatEntity.maxSpeed;
                    return true;
                }
                agent.speed = combatEntity.entityConfig.walkSpeed * combatEntity.entityConfig.walkFactor;
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
                if (wanderTimer >= 6f)
                {
                    moveEntity.Wander();
                    wanderTimer = 0;
                }
                return Status.RUNNING;
            }));
            chase.AddChildren(new UntilFailure(canSeePlayer), new Action(() =>
            {
                if(!combatEntity.immovable)
                {
                    combatEntity.ExecuteAction(player);
                    return Status.RUNNING;
                }
                else
                {
                    combatEntity.CancelAction();
                    return Status.FAILURE;
                }
            }));
        }

        void Update()
        {
            if (!combatEntity.isDead)
                root.Execute();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, GetComponent<CombatEntity>().entityConfig.viewRadius);
        }
    }
}
