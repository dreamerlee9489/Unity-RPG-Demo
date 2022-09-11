using Manager;
using UnityEngine;

namespace Control.FSM
{
    public sealed class Attack : State
    {
        private static readonly int attack = Animator.StringToHash("attack");

        public Attack(Entity owner, Entity target) : base(owner, target) => Enter();

        public override void Enter()
        {
            animator.SetBool(attack, true);
        }

        public override void Execute()
        {
            owner.transform.LookAt(target.transform);
            if (!owner.CanAttack(target.transform))
                owner.GetComponent<StateController>().ChangeState(new Idle(owner, target));
        }

        public override void Exit()
        {
            animator.SetBool(attack, false);
            owner.target = null;
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}