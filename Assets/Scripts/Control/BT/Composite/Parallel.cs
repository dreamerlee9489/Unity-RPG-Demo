using Control.MSG;
using Manager;

namespace Control.BT.Composite
{
    public class Parallel : Composite
    {
        public override Status Execute()
        {
            int successCount = 0;
            foreach (var child in children)
            {
                switch (child.Execute())
                {
                    case Status.Success:
                        successCount++;
                        if (successCount == children.Count)
                            return status = Status.Success;
                        break;
                    case Status.Running:
                        break;
                    case Status.Failure:
                        return status = Status.Failure;
                }
            }

            return status = Status.Running;
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new System.NotImplementedException();
        }

        protected override void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}