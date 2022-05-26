using UnityEngine;

namespace App.Control
{
    public interface ICmdReceiver
    {
        void ExecuteAction(Vector3 point);
        void ExecuteAction(Transform target);
        void CancelAction();
    }

    public abstract class Command
    {
        protected ICmdReceiver receiver = null;
        protected Command(ICmdReceiver receiver) => this.receiver = receiver;
        public abstract void Execute(Vector3 point);
        public abstract void Execute(Transform target);
        public abstract void Cancel();
    }

    public class MoveCommand : Command
    {
        public MoveCommand(ICmdReceiver receiver) : base(receiver) { }
        public override void Execute(Vector3 point) => receiver.ExecuteAction(point);
        public override void Execute(Transform target) => receiver.ExecuteAction(target);
        public override void Cancel() => receiver.CancelAction();
    }

    public class CombatCommand : Command
    {
        public CombatCommand(ICmdReceiver receiver) : base(receiver) { }
        public override void Execute(Vector3 point) => receiver.ExecuteAction(point);
        public override void Execute(Transform target) => receiver.ExecuteAction(target);
        public override void Cancel() => receiver.CancelAction();
    }

    public class DialogueCommand : Command
    {
        public DialogueCommand(ICmdReceiver receiver) : base(receiver) { }
        public override void Execute(Vector3 point) => receiver.ExecuteAction(point);
        public override void Execute(Transform target) => receiver.ExecuteAction(target);
        public override void Cancel() => receiver.CancelAction();
    }
}