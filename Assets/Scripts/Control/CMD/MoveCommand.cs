using UnityEngine;

namespace Control.CMD
{
    public class MoveCommand : Command
    {
        public MoveCommand(ICmdReceiver receiver) : base(receiver)
        {
        }

        public override void Execute(Vector3 point) => receiver.ExecuteAction(point);
        public override void Execute(Transform target) => receiver.ExecuteAction(target);
        public override void Cancel() => receiver.CancelAction();
    }
}