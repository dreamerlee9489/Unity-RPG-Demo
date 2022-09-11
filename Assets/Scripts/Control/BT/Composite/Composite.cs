using System.Collections.Generic;

namespace Control.BT.Composite
{
    public abstract class Composite : Node
    {
        protected int index = 0;
        protected readonly List<Node> children = new();

        protected Composite(string name = "Composite") : base(name)
        {
        }

        protected abstract void Reset();

        public void AddChild(Node node) => children.Add(node);

        public void AddChildren(params Node[] nodes)
        {
            foreach (Node node in nodes)
                children.Add(node);
        }
    }
}