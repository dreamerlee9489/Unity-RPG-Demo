using System;
using Control.MSG;
using Manager;

namespace Control.BT.Composite
{
    public class Sequence : Composite
    {
        public Sequence(string name = "Sequence") : base(name)
        {
        }
        
        protected override void Reset() => index = 0;

        public override Status Execute()
        {
            if (children.Count == 0)
                return Status.Success;
            if(index >= children.Count)
                Reset();
            while (index < children.Count)
            {
                status = children[index++].Execute();
                switch (status)
                {
                    case Status.Running:
                        return status;
                    case Status.Failure:
                        Reset();
                        return status;
                    default:
                        break;
                }
            }
            Reset();
            return Status.Success;
        }

        public override bool OnMessage(Telegram telegram)
        {
            throw new NotImplementedException();
        }
    }
}