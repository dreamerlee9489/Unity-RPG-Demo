using UnityEngine;

namespace Control.BT.Decorator
{
    public class Timer : Decorator
    {
        float cd = 0, timer = 0;

        public Timer(float cd, Node child, string name = "Timer") : base(child, name)
        {
            this.cd = cd;
        }

        public override Status Execute()
        {
            timer += Time.deltaTime;
            if (timer >= cd)
            {
                switch (child.Execute())
                {
                    case Status.Success:
                        timer = 0;
                        return status = Status.Success;
                    case Status.Running:
                        break;
                    case Status.Failure:
                        timer = 0;
                        return status = Status.Failure;
                }
            }

            return status = Status.Running;
        }
    }
}