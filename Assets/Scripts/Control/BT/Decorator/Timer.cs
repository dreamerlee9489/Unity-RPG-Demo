using Control.MSG;
using Manager;
using UnityEngine;

namespace Control.BT.Decorator
{
    public class Timer : Decorator
    {
        private readonly float _cd = 0;
        private float _timer = 0;

        public Timer(float cd, Node child, string name = "Timer") : base(child, name)
        {
            this._cd = cd;
        }

        public override Status Execute()
        {
            _timer += Time.deltaTime;
            if (_timer >= _cd)
            {
                switch (child.Execute())
                {
                    case Status.Success:
                        _timer = 0;
                        return status = Status.Success;
                    case Status.Running:
                        break;
                    case Status.Failure:
                        _timer = 0;
                        return status = Status.Failure;
                }
            }

            return status = Status.Running;
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}