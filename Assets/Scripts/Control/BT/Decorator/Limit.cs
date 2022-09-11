namespace Control.BT.Decorator
{
    public class Limit : Decorator
    {
        int time = 0, count = 0;

        public Limit(int time, Node child, string name = "Limit") : base(child, name)
        {
            this.time = time;
        }

        public override Status Execute()
        {
            switch (child.Execute())
            {
                case Status.Success:
                    count++;
                    if (count >= time)
                    {
                        count = 0;
                        return status = Status.Success;
                    }

                    break;
                case Status.Running:
                    break;
                case Status.Failure:
                    count++;
                    if (count >= time)
                    {
                        count = 0;
                        return status = Status.Success;
                    }

                    break;
            }

            return status = Status.Running;
        }
    }
}