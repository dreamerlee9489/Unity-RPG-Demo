using UnityEngine;
using UnityEngine.AI;
using App.Control.BT;
using App.Manager;

namespace App.Control
{
    [RequireComponent(typeof(Entity))]
    public class BehaviorController : MonoBehaviour
    {
        float wanderTimer = 6f;
        Animator animator = null;
        NavMeshAgent agent = null;
        Transform player = null;
        Entity entity = null;
        Selector root = new Selector();

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            entity = GetComponent<Entity>();
        }

        void Start()
        {
            player = GameManager.Instance.player.transform;
            Sequence retreat = new Sequence();
            Parallel wander = new Parallel();
            Parallel chase = new Parallel();
            Condition canSeePlayer = new Condition(() =>
            {
                if (entity.CanSee(player))
                {
                    agent.speed = entity.entityConfig.runSpeed * entity.entityConfig.runFactor * entity.speedRate;
                    return true;
                }
                agent.speed = entity.entityConfig.walkSpeed * entity.entityConfig.walkFactor;
                return false;
            });
            root.AddChildren(retreat, wander, chase);
            retreat.AddChildren(new Condition(() =>
            {
                return false;
            }), new Action(() =>
            {
                if (entity.Flee(player.position))
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
                    entity.Wander();
                    wanderTimer = 0;
                }
                return Status.RUNNING;
            }));
            chase.AddChildren(new UntilFailure(canSeePlayer), new Action(() =>
            {
                entity.ExecuteAction(player);
                    return Status.RUNNING;
            }));
        }

        void Update()
        {
            if (!entity.isDead)
                root.Execute();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, GetComponent<Entity>().entityConfig.viewRadius);
        }
    }
}
