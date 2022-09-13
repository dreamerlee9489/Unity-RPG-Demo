using Control.MSG;
using Manager;

namespace Control.BT.Decorator
{
    public class UntilSuccess : Decorator
    {
        public UntilSuccess(Node child, string name = "UntilSuccess") : base(child, name)
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