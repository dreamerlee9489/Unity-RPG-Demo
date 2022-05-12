using UnityEngine;
using UnityEngine.AI;

namespace Game.Control.FSM
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
    }

    public class Idle : State
    {
        public Idle(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }
    }

    public class Patrol : State
    {
        private float wanderTimer = 3f;

        public Patrol(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
        }

        public override void Execute()
        {
            if (combatEntity.CanSee(target))
                owner.ChangeState(new Pursuit(owner, target));
            else
            {
                wanderTimer += Time.deltaTime;
                if (wanderTimer >= 3f)
                {
                    moveEntity.Wander();
                    wanderTimer = 0;
                }
            }
        }

        public override void Exit()
        {
        }
    }

    public class Pursuit : State
    {
        public Pursuit(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
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
                owner.ChangeState(new Patrol(owner, target));
        }

        public override void Exit()
        {
        }
    }

    public class Attack : State
    {
        public Attack(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
            animator.SetBool("attack", true);
        }

        public override void Execute()
        {
            if (!combatEntity.CanAttack(target))
                owner.ChangeState(new Patrol(owner, target));
            owner.transform.LookAt(target);
        }

        public override void Exit()
        {
            animator.SetBool("attack", false);
        }
    }

    public class RunAway : State
    {
        public RunAway(FiniteStateMachine owner, Transform target) : base(owner, target) => Enter();

        public override void Enter()
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}
