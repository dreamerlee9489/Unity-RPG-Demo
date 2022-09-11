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
        private string _name;
        protected Status status = Status.None;

        protected Node(string name = "Node") => this._name = name;

        public abstract Status Execute();
    }
}