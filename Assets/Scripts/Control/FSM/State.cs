using UnityEngine;
using UnityEngine.AI;
using App.Manager;

namespace App.Control.FSM
{
    public abstract class State
    {
        protected Animator animator = null;
        protected NavMeshAgent agent = null;
        protected Entity owner = null;
        protected Entity target = null;

        public State(Entity owner, Entity target)
        {
            animator = owner.GetComponent<Animator>();
            agent = owner.GetComponent<NavMeshAgent>();
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

        public Idle(Entity owner, Entity target) : base(owner, target) => Enter();

        public override void Enter()
        {
            agent.speed = owner.entityConfig.walkSpeed * owner.entityConfig.walkFactor;
        }

        public override void Execute()
        {
            idleTimer += Time.deltaTime;
            if(idleTimer > 4)
                owner.GetComponent<FiniteStateMachine>().ChangeState(new Patrol(owner, target));
            else if (owner.CanSee(target.transform))
                owner.GetComponent<FiniteStateMachine>().ChangeState(new Pursuit(owner, target));
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

        public Patrol(Entity owner, Entity target) : base(owner, target) => Enter();

        public override void Enter()
        {
            agent.speed = owner.entityConfig.walkSpeed * owner.entityConfig.walkFactor;
        }

        public override void Execute()
        {
            if (owner.CanSee(target.transform))
                owner.GetComponent<FiniteStateMachine>().ChangeState(new Pursuit(owner, target));
            else
            {
                wanderTimer += Time.deltaTime;
                if (wanderTimer >= 6f)
                {
                    owner.Wander();
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
        public Pursuit(Entity owner, Entity target) : base(owner, target) => Enter();

        public override void Enter()
        {
            owner.target = target.transform;
            agent.speed = owner.entityConfig.runSpeed * owner.entityConfig.runFactor * owner.speedRate;
        }

        public override void Execute()
        {
            if (!owner.CanSee(target.transform))
                owner.GetComponent<FiniteStateMachine>().ChangeState(new Idle(owner, target));
            else
            {
                if (owner.CanAttack(target.transform))
                    owner.GetComponent<FiniteStateMachine>().ChangeState(new Attack(owner, target));
                owner.Seek(target.transform.position);
                owner.transform.LookAt(target.transform);
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

    public class Attack : State
    {
        public Attack(Entity owner, Entity target) : base(owner, target) => Enter();

        public override void Enter()
        {
            animator.SetBool("attack", true);
        }

        public override void Execute()
        {
            owner.transform.LookAt(target.transform);
            if (!owner.CanAttack(target.transform))
                owner.GetComponent<FiniteStateMachine>().ChangeState(new Idle(owner, target));
        }

        public override void Exit()
        {
            animator.SetBool("attack", false);
            owner.target = null;
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Stunned : State
    {
        public float duration = 0, timer = 0;

        public Stunned(Entity owner, Entity target, float duration) : base(owner, target)
        {
            this.duration = duration;
        }

        public override void Enter()
        {
            owner.audioSource.clip = Resources.LoadAsync("Audio/SFX_Take Damage Ouch " + Random.Range(1, 6)).asset as AudioClip;
            owner.audioSource.Play();
            animator.SetBool("stunned", true);
            agent.isStopped = true;
            UIManager.Instance.messagePanel.Print(owner.entityConfig.nickName + "被眩晕。", Color.green);
        }

        public override void Execute()
        {
            timer += Time.deltaTime;
            if(timer >= duration)
                owner.GetComponent<FiniteStateMachine>().ChangeState(new Idle(owner, target));
        }

        public override void Exit()
        {
            animator.SetBool("stunned", false);
            agent.isStopped = false;
            UIManager.Instance.messagePanel.Print(owner.entityConfig.nickName + "解除了眩晕。", Color.green);
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Knocked : State
    {
        float duration = 5.533f, timer = 0;

        public Knocked(Entity owner, Entity target) : base(owner, target)
        {
        }

        public override void Enter()
        {
            owner.audioSource.clip = Resources.LoadAsync("Audio/SFX_Take Damage Ouch " + Random.Range(1, 6)).asset as AudioClip;
            owner.audioSource.Play();
            animator.SetTrigger("knocked");
            agent.isStopped = true;
            UIManager.Instance.messagePanel.Print(owner.entityConfig.nickName + "被击倒。", Color.green);
        }

        public override void Execute()
        {
            timer += Time.deltaTime;
            if(timer >= duration)
                owner.GetComponent<FiniteStateMachine>().ChangeState(new Idle(owner, target));
        }

        public override void Exit()
        {
            animator.ResetTrigger("knocked");
            agent.isStopped = false;
            UIManager.Instance.messagePanel.Print(owner.entityConfig.nickName + "站起来了。", Color.green);
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}
