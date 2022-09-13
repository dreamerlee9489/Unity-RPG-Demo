using Control.MSG;
using Manager;

namespace Control.BT.Decorator
{
    public class UntilFailure : Decorator
    {
        public UntilFailure(Node child, string name = "UntilFailure") : base(child, name)
        {
        }

        public override Status Execute()
        {
            switch (child.Execute())
            {
                case Status.Success:
                    break;
                case Status.Running:
                    break;
                case Status.Failure:
                    return status = Status.Failure;
            }

            return status = Status.Running;
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }
    }
}