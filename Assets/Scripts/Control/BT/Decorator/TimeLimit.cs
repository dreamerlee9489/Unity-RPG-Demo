using UnityEngine;

namespace Control.BT.Decorator
{
    public class TimeLimit : Decorator
    {
        float runtime = 0, timer = 0;

        public TimeLimit(float runtime, Node child, string name = "TimeLimit") : base(child, name)
        {
            this.runtime = runtime;
        }

        public override Status Execute()
        {
            timer += Time.deltaTime;
            if (timer >= runtime)
            {
                timer = 0;
                return status = Status.Success;
            }

            return status = Status.Running;
        }
    }
}