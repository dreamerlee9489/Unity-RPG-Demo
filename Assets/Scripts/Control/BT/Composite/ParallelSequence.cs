using System.Collections.Generic;

namespace Control.BT.Composite
{
    public class ParallelSequence : Composite
    {
        private List<Node> _waitNodes = new();
        private bool _isSuccess = false;

        public ParallelSequence(string name = "ParallelSequence") : base(name)
        {
        }

        protected override void Reset()
        {
            _waitNodes.Clear();
            _isSuccess = false;
        }

        public override Status Execute()
        {
            if (children.Count == 0)
                return Status.Success;
            List<Node> waitNode = new(), mainNodes = null;
            mainNodes = _waitNodes.Count == 0 ? children : _waitNodes;
            foreach (var node in mainNodes)
            {
                status = node.Execute();
                switch (status)
                {
                    case Status.Success:
                        _isSuccess = true;
                        break;
                    case Status.Running:
                        waitNode.Add(node);
                        break;
                    default:
                        break;
                }
            }

            if (waitNode.Count > 0)
            {
                _waitNodes = waitNode;
                return Status.Running;
            }

            status = _isSuccess ? Status.Success : Status.Failure;
            Reset();
            return status;
        }
    }
}