using Control.MSG;
using UnityEngine;
using UnityEngine.AI;
using Manager;

namespace Control.FSM
{
    public abstract class State
    {
        protected readonly Animator animator = null;
        protected readonly NavMeshAgent agent = null;
        protected readonly Entity owner = null;
        protected readonly Entity target = null;

        protected State(Entity owner, Entity target)
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
}