namespace Control.BT.Composite
{
    public class PrioritySelector : Composite
    {
        private bool _ordered = false;
        private readonly double _priority = 0;

        public PrioritySelector(double priority, string name = "PrioritySelector") : base(name) =>
            this._priority = priority;

        public override Status Execute()
        {
            if (!_ordered)
            {
                children.Sort((a, b) =>
                {
                    if (((PrioritySelector)a)._priority < ((PrioritySelector)b)._priority)
                        return -1;
                    else if (((PrioritySelector)a)._priority > ((PrioritySelector)b)._priority)
                        return 1;
                    return 0;
                });
                _ordered = true;
            }

            switch (children[index].Execute())
            {
                case Status.Success:
                    index = 0;
                    _ordered = false;
                    return status = Status.Success;
                case Status.Running:
                    break;
                case Status.Failure:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        _ordered = false;
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