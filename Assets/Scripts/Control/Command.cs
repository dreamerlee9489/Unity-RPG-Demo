using UnityEngine;

namespace Game.Control
{
    public interface IReceiver
    {
        void ExecuteAction(RaycastHit hit);
        void CancelAction();
    }

    public abstract class Command
    {
        protected IReceiver receiver = null;
        protected Command(IReceiver receiver) => this.receiver = receiver;
        public abstract void Execute(RaycastHit hit);
        public abstract void Cancel();
    }

    public class MoveCommand : Command
    {
        public MoveCommand(IReceiver receiver) : base(receiver) { }
        public override void Execute(RaycastHit hit) => receiver.ExecuteAction(hit);
        public override void Cancel() => receiver.CancelAction();
    }

    public class CombatCommand : Command
    {
        public CombatCommand(IReceiver receiver) : base(receiver) { }
        public override void Execute(RaycastHit hit) => receiver.ExecuteAction(hit);
        public override void Cancel() => receiver.CancelAction();
    }
}