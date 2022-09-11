using System;

namespace Control.BT.Composite
{
    public class Selector : Composite
    {
        public Selector(string name = "Selector") : base(name)
        {
        }

        protected override void Reset() => index = 0;

        // ReSharper disable Unity.PerformanceAnalysis
        public override Status Execute()
        {
            if (children.Count == 0)
                return Status.Success;
            if (index >= children.Count)
                Reset();
            while (index < children.Count)
            {
                status = children[index++].Execute();
                switch (status)
                {
                    case Status.Success:
                        Reset();
                        return status;
                    case Status.Running:
                        return status;
                    default:
                        break;
                }
            }
            Reset();
            return Status.Failure;
        }
    }
}