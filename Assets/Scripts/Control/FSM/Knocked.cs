using Control.MSG;
using Manager;
using UnityEngine;

namespace Control.FSM
{
    public class Knocked : State
    {
        private const float Duration = 5.533f;
        private float _timer = 0;
        private static readonly int knocked = Animator.StringToHash("knocked");

        public Knocked(Entity owner, Entity target) : base(owner, target)
        {
        }

        public override void Enter()
        {
            owner.audioSource.clip =
                Resources.LoadAsync("Audio/SFX_Take Damage Ouch " + Random.Range(1, 6)).asset as AudioClip;
            owner.audioSource.Play();
            animator.SetTrigger(knocked);
            agent.isStopped = true;
            UIManager.Instance.messagePanel.Print(owner.entityConfig.nickName + "被击倒。", Color.green);
        }

        public override void Execute()
        {
            _timer += Time.deltaTime;
            if (_timer >= Duration)
                owner.GetComponent<StateController>().ChangeState(new Idle(owner, target));
        }

        public override void Exit()
        {
            animator.ResetTrigger(knocked);
            agent.isStopped = false;
            UIManager.Instance.messagePanel.Print(owner.entityConfig.nickName + "站起来了。", Color.green);
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}