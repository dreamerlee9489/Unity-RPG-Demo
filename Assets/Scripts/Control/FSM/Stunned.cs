using Manager;
using UnityEngine;

namespace Control.FSM
{
    public class Stunned : State
    {
        private readonly float _duration = 0;
        private float _timer = 0;
        private static readonly int stunned = Animator.StringToHash("stunned");

        public Stunned(Entity owner, Entity target, float duration) : base(owner, target)
        {
            this._duration = duration;
        }

        public override void Enter()
        {
            owner.audioSource.clip =
                Resources.LoadAsync("Audio/SFX_Take Damage Ouch " + Random.Range(1, 6)).asset as AudioClip;
            owner.audioSource.Play();
            animator.SetBool(stunned, true);
            agent.isStopped = true;
            UIManager.Instance.messagePanel.Print(owner.entityConfig.nickName + "被眩晕。", Color.green);
        }

        public override void Execute()
        {
            _timer += Time.deltaTime;
            if (_timer >= _duration)
                owner.GetComponent<StateController>().ChangeState(new Idle(owner, target));
        }

        public override void Exit()
        {
            animator.SetBool(stunned, false);
            agent.isStopped = false;
            UIManager.Instance.messagePanel.Print(owner.entityConfig.nickName + "解除了眩晕。", Color.green);
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}