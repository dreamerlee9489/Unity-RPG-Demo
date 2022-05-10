using UnityEngine;
using UnityEngine.AI;
using Game.Control.BehaviourTree;
using Game.SO;

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
            UntilSuccess untilSucc = new UntilSuccess();
            UntilFailure untilFail = new UntilFailure();
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
                    return Node.Status.SUCCESS;
                return Node.Status.RUNNING;
            });
            Action seekSafety = new Action(() =>
            {
                print("已找到安全位置");
                return Node.Status.SUCCESS;
            });
            Action wandering = new Action(() =>
            {
                wanderTimer += Time.deltaTime;
                if (wanderTimer >= 6f)
                {
                    moveEntity.Wander();
                    wanderTimer = 0;
                }
                return Node.Status.RUNNING;
            });
            Action moving = new Action(() =>
            {
                if (moveEntity.Seek(target.position) <= agent.stoppingDistance)
                    return Node.Status.SUCCESS;
                return Node.Status.RUNNING;
            });
            Action attacking = new Action(() =>
            {
                if (combatEntity.CanAttack(target))
                {
                    combatEntity.StartAttack();
                    return Node.Status.SUCCESS;
                }
                combatEntity.StopAttack();
                return Node.Status.RUNNING;
            });
            root.children.Add(retreat);
            root.children.Add(patrol);
            root.children.Add(chase);
            retreat.children.Add(needFlee);
            retreat.children.Add(flee);
            retreat.children.Add(seekSafety);
            patrol.children.Add(untilSucc);
            patrol.children.Add(wandering);
            untilSucc.children.Add(canSee);
            chase.children.Add(untilFail);
            chase.children.Add(moving);
            chase.children.Add(attacking);
            untilFail.children.Add(canSee);
        }

        private void Update()
        {
            //root.Execute();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, GetComponent<MoveEntity>().abilityConfig.viewRadius);
        }
    }
}
