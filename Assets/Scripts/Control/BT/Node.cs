namespace Control.BT
{
    public enum Status
    {
        None,
        Success,
        Running,
        Failure
    }

    public abstract class Node
    {
        public string name;
        public Status status = Status.None;

        public Node(string name = "Node")
        {
            this.name = name;
        }

        public abstract Status Execute();
    }
}