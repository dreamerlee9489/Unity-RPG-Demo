using UnityEngine;
using UnityEngine.AI;
using App.Manager;

namespace App.Control.FSM
{
    public abstract class State
    {
        protected Animator animator = null;
        protected NavMeshAgent agent = null;
        protected Transform target = null;
        protected MoveEntity moveEntity = null;
        protected CombatEntity combatEntity = null;
        protected FiniteStateMachine owner = null;

        public State(FiniteStateMachine owner, Transform target)
        {
            animator = owner.GetComponent<Animator>();
            agent = owner.GetComponent<NavMeshAgent>();
            moveEntity = owner.GetComponent<MoveEntity>();
            combatEntity = owner.GetComponent<CombatEntity>();
            this.owner = owner;
            this.target = target;
        }

        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();
        public abstract bool OnMessage(Telegram telegram);
    }

    public class Idle : State
    {
        float idleTimer = 0;

        public Idle(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
            agent.speed = combatEntity.entityConfig.walkSpeed * combatEntity.entityConfig.walkFactor;
        }

        public override void Execute()
        {
            idleTimer += Time.deltaTime;
            if(idleTimer > 4)
            {
                owner.ChangeState(new Patrol(owner, target));
                idleTimer = 0;
            }
            else if (combatEntity.CanSee(target))
            {
                owner.ChangeState(new Pursuit(owner, target));
                idleTimer = 0;
            }
        }

        public override void Exit()
        {
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Patrol : State
    {
        float wanderTimer = 6f;

        public Patrol(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
            agent.speed = combatEntity.entityConfig.walkSpeed * combatEntity.entityConfig.walkFactor;
        }

        public override void Execute()
        {
            if (combatEntity.CanSee(target))
                owner.ChangeState(new Pursuit(owner, target));
            else
            {
                wanderTimer += Time.deltaTime;
                if (wanderTimer >= 6f)
                {
                    moveEntity.Wander();
                    wanderTimer = 0;
                }
            }
        }

        public override void Exit()
        {
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Pursuit : State
    {
        public Pursuit(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
            agent.speed = combatEntity.entityConfig.runSpeed * combatEntity.entityConfig.runFactor;
        }

        public override void Execute()
        {
            if (combatEntity.CanSee(target))
            {
                if (combatEntity.CanAttack(target))
                    owner.ChangeState(new Attack(owner, target));
                moveEntity.Seek(target.position);
                owner.transform.LookAt(target);
            }
            else
                owner.ChangeState(new Idle(owner, target));
        }

        public override void Exit()
        {
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Attack : State
    {
        public Attack(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
            combatEntity.target = target;
            animator.SetBool("attack", true);
        }

        public override void Execute()
        {
            owner.transform.LookAt(target);
            if (!combatEntity.CanAttack(target))
                owner.ChangeState(new Idle(owner, target));
        }

        public override void Exit()
        {
            combatEntity.target = null;
            animator.SetBool("attack", false);
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RunAway : State
    {
        public RunAway(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
            agent.speed = combatEntity.entityConfig.runSpeed * combatEntity.entityConfig.runFactor;
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}
