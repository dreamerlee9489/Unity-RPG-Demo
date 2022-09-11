namespace Control.BT.Composite
{
    public class RandomSelector : Composite
    {
        private bool _shuffled = false;

        public RandomSelector(string name = "RandomSelector") : base(name)
        {
        }

        public override Status Execute()
        {
            if (!_shuffled)
            {
                children.Shuffle();
                _shuffled = true;
            }

            switch (children[index].Execute())
            {
                case Status.Success:
                    index = 0;
                    _shuffled = false;
                    return status = Status.Success;
                case Status.Running:
                    break;
                case Status.Failure:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        _shuffled = false;
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