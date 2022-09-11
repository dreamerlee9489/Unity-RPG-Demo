using UnityEngine;

namespace Control.BT.Decorator
{
    public abstract class Decorator : Node
    {
        protected Node child = null;
        protected Decorator(Node child = null, string name = "Decorator") : base(name) => this.child = child;
        public void SetChild(Node node) => child = node;
    }
}