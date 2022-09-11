namespace Control.BT
{
    public class Action : Node
    {
        public delegate Status Task();
        public event Task task = null;
        public Action(Task task, string name = "Action") : base(name) => this.task = task;

        public override Status Execute()
        {
            if (task != null)
                return status = task();
            return status = Status.Failure;
        }
    }
}