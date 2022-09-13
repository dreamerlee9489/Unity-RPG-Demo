using Control.MSG;
using Manager;

namespace Control.BT.Decorator
{
    public class Limit : Decorator
    {
        private readonly int _time = 0;
        private int _count = 0;

        public Limit(int time, Node child, string name = "Limit") : base(child, name)
        {
            this._time = time;
        }

        public override Status Execute()
        {
            switch (child.Execute())
            {
                case Status.Success:
                    _count++;
                    if (_count >= _time)
                    {
                        _count = 0;
                        return status = Status.Success;
                    }

                    break;
                case Status.Running:
                    break;
                case Status.Failure:
                    _count++;
                    if (_count >= _time)
                    {
                        _count = 0;
                        return status = Status.Success;
                    }

                    break;
            }

            return status = Status.Running;
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}