using Manager;

namespace Control.FSM
{
    public sealed class Pursuit : State
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
                owner.GetComponent<StateController>().ChangeState(new Idle(owner, target));
            else
            {
                if (owner.CanAttack(target.transform))
                    owner.GetComponent<StateController>().ChangeState(new Attack(owner, target));
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
}