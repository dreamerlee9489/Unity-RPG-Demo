using Manager;
using UnityEngine;

namespace Control.FSM
{
    public sealed class Patrol : State
    {
        private float _wanderTimer = 6f;

        public Patrol(Entity owner, Entity target) : base(owner, target) => Enter();

        public override void Enter()
        {
            agent.speed = owner.entityConfig.walkSpeed * owner.entityConfig.walkFactor;
        }

        public override void Execute()
        {
            if (owner.CanSee(target.transform))
                owner.GetComponent<StateController>().ChangeState(new Pursuit(owner, target));
            else
            {
                _wanderTimer += Time.deltaTime;
                if (_wanderTimer >= 6f)
                {
                    owner.Wander();
                    _wanderTimer = 0;
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
}