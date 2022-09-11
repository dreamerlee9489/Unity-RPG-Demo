using UnityEngine;

namespace Control.CMD
{
    public abstract class Command
    {
        protected ICmdReceiver receiver = null;
        protected Command(ICmdReceiver receiver) => this.receiver = receiver;
        public abstract void Execute(Vector3 point);
        public abstract void Execute(Transform target);
        public abstract void Cancel();
    }
}