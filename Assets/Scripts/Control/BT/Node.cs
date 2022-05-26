namespace App.Control.BT
{
    public enum Status { SUCCESS, RUNNING, FAILURE }

    public abstract class Node
    {
        public Status status;
        public int index = 0;
        public string name;
        public Node(string name = "Node") { this.name = name; }
        public abstract Status Execute();
    }

    public class Condition : Node
    {
        public delegate bool Task();
        public event Task task = null;
        public Condition(Task task, string name = "Condition") : base(name) => this.task = task;
        public override Status Execute()
        {
            if (task != null && task())
                return status = Status.SUCCESS;
            return status = Status.FAILURE;
        }
    }

    public class Action : Node
    {
        public delegate Status Task();
        public event Task task = null;
        public Action(Task task, string name = "Action") : base(name) => this.task = task;
        public override Status Execute()
        {
            if (task != null)
                return status = task();
            return status = Status.FAILURE;
        }
    }
}
