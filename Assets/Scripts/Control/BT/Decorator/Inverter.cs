using Control.MSG;
using Manager;

namespace Control.BT.Decorator
{
    public class Inverter : Decorator
    {
        public Inverter(Node child, string name = "Inverter") : base(child, name)
        {
        }

        public override Status Execute()
        {
            switch (child.Execute())
            {
                case Status.Success:
                    return status = Status.Failure;
                case Status.Running:
                    break;
                case Status.Failure:
                    return status = Status.Success;
            }

            return status = Status.Running;
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}