namespace Control.BT.Composite
{
    public class PrioritySelector : Composite
    {
        bool ordered = false;
        public double priority = 0;

        public PrioritySelector(double priority, string name = "PrioritySelector") : base(name) =>
            this.priority = priority;

        public override Status Execute()
        {
            if (!ordered)
            {
                children.Sort((a, b) =>
                {
                    if (((PrioritySelector)a).priority < ((PrioritySelector)b).priority)
                        return -1;
                    else if (((PrioritySelector)a).priority > ((PrioritySelector)b).priority)
                        return 1;
                    return 0;
                });
                ordered = true;
            }

            switch (children[index].Execute())
            {
                case Status.Success:
                    index = 0;
                    ordered = false;
                    return status = Status.Success;
                case Status.Running:
                    break;
                case Status.Failure:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        ordered = false;
                        return status = Status.Failure;
                    }

                    break;
            }

            return status = Status.Running;
        }

        protected override void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}