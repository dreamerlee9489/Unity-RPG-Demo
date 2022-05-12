using UnityEngine;

namespace Game.Control
{
    public interface ICmdReceiver
    {
        void ExecuteAction(RaycastHit hit);
        void CancelAction();
    }

    public abstract class Command
    {
        protected ICmdReceiver receiver = null;
        protected Command(ICmdReceiver receiver) => this.receiver = receiver;
        public abstract void Execute(RaycastHit hit);
        public abstract void Cancel();
    }

    public class MoveCommand : Command
    {
        public MoveCommand(ICmdReceiver receiver) : base(receiver) { }
        public override void Execute(RaycastHit hit) => receiver.ExecuteAction(hit);
        public override void Cancel() => receiver.CancelAction();
    }

    public class CombatCommand : Command
    {
        public CombatCommand(ICmdReceiver receiver) : base(receiver) { }
        public override void Execute(RaycastHit hit) => receiver.ExecuteAction(hit);
        public override void Cancel() => receiver.CancelAction();
    }
}