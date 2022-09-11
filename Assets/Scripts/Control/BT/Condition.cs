namespace Control.BT
{
    public class Condition : Node
    {
        public delegate bool Task();
        public event Task task = null;
        public Condition(Task task, string name = "Condition") : base(name) => this.task = task;

        public override Status Execute()
        {
            if (task != null && task())
                return status = Status.Success;
            return status = Status.Failure;
        }
    }
}