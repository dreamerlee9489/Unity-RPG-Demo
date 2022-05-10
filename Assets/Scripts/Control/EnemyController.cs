using UnityEngine;
using UnityEngine.AI;
using Game.Control.BehaviourTree;

namespace Game.Control
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public class EnemyController : MonoBehaviour
    {
        float wanderTimer = 6f;
        NavMeshAgent agent = null;
        Transform target = null;
        MoveEntity moveEntity = null;
        CombatEntity combatEntity = null;
        Command command = null;
        Selector root = new Selector();

        void ExecuteCommand(Command command, RaycastHit hit)
        {
            this.command = command;
            command.Execute(hit);
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
            target = GameObject.FindWithTag("Player").transform;
            Sequence retreat = new Sequence();
            Parallel patrol = new Parallel();
            Parallel chase = new Parallel();
            Condition needFlee = new Condition(() =>
            {
                print("不需要逃跑");
                return false;
            });
            Condition canSee = new Condition(() =>
            {
                if (combatEntity.CanSee(target))
                    return true;
                return false;
            });
            Action flee = new Action(() =>
            {
                if (moveEntity.Flee(target.position))
                    return Status.SUCCESS;
                return Status.RUNNING;
            });
            Action seekSafety = new Action(() =>
            {
                print("已找到安全位置");
                return Status.SUCCESS;
            });
            Action wandering = new Action(() =>
            {
                wanderTimer += Time.deltaTime;
                if (wanderTimer >= 6f)
                {
                    moveEntity.Wander();
                    wanderTimer = 0;
                }
                return Status.RUNNING;
            });
            Action moving = new Action(() =>
            {
                if (moveEntity.Seek(target.position) <= agent.stoppingDistance)
                    return Status.SUCCESS;
                return Status.RUNNING;
            });
            Action attacking = new Action(() =>
            {
                if (combatEntity.CanAttack(target))
                {
                    combatEntity.StartAttack();
                    return Status.SUCCESS;
                }
                combatEntity.StopAttack();
                return Status.RUNNING;
            });
            UntilSuccess untilSucc = new UntilSuccess(canSee);
            UntilFailure untilFail = new UntilFailure(canSee);
            root.AddChild(retreat);
            root.AddChild(patrol);
            root.AddChild(chase);
            retreat.AddChild(needFlee);
            retreat.AddChild(flee);
            retreat.AddChild(seekSafety);
            patrol.AddChild(untilSucc);
            patrol.AddChild(wandering);
            chase.AddChild(untilFail);
            chase.AddChild(moving);
            chase.AddChild(attacking);
        }

        private void Update()
        {
            // root.Execute();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, GetComponent<MoveEntity>().abilityConfig.viewRadius);
        }
    }
}
