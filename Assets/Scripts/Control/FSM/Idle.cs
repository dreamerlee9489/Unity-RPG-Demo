using Manager;
using UnityEngine;

namespace Control.FSM
{
    public sealed class Idle : State
    {
        private float _idleTimer = 0;

        public Idle(Entity owner, Entity target) : base(owner, target) => Enter();

        public override void Enter()
        {
            agent.speed = owner.entityConfig.walkSpeed * owner.entityConfig.walkFactor;
        }

        public override void Execute()
        {
            _idleTimer += Time.deltaTime;
            if (_idleTimer > 4)
                owner.GetComponent<StateController>().ChangeState(new Patrol(owner, target));
            else if (owner.CanSee(target.transform))
                owner.GetComponent<StateController>().ChangeState(new Pursuit(owner, target));
        }

        public override void Exit()
        {
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}