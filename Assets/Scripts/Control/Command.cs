using UnityEngine;

namespace Game.Control
{
    public interface IReceiver
    {
        void ExecuteAction(Vector3 position);
        void CancelAction();
    }

    public abstract class Command
    {
        protected IReceiver receiver = null;

        protected Command(IReceiver receiver) 
        {
            this.receiver = receiver;
        }

        public abstract void Execute(Vector3 position); 
        public abstract void Cancel(); 
    }

    public class MoveCommand : Command
    {
        public MoveCommand(IReceiver receiver) : base(receiver) {}

        public override void Execute(Vector3 position)
        {
            receiver.ExecuteAction(position);
        }

        public override void Cancel()
        {
            receiver.CancelAction();
        }
    }
}