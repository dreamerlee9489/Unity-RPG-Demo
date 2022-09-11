namespace Control.BT.Composite
{
    public class Parallel : Composite
    {
        

        public override Status Execute()
        {
            int successCount = 0;
            for (int i = 0; i < children.Count; i++)
            {
                switch (children[i].Execute())
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

        protected override void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}