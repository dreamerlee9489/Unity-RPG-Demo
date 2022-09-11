using UnityEngine;

namespace Control.BT.Decorator
{
    public class TimeLimit : Decorator
    {
        private readonly float _runtime = 0;
        private float _timer = 0;

        public TimeLimit(float runtime, Node child, string name = "TimeLimit") : base(child, name)
        {
            this._runtime = runtime;
        }

        public override Status Execute()
        {
            _timer += Time.deltaTime;
            if (_timer >= _runtime)
            {
                _timer = 0;
                return status = Status.Success;
            }

            return status = Status.Running;
        }
    }
}