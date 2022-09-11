using System;
using System.Collections.Generic;

namespace Control.BT.Composite
{
    public class ParallelSelector : Composite
    {
        private List<Node> _waitNodes = new();
        private bool _isFailure = false;

        public ParallelSelector(string name = "ParallelSelector") : base(name)
        {
        }

        protected override void Reset()
        {
            _waitNodes.Clear();
            _isFailure = false;
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
                        break;
                    case Status.Running:
                        waitNode.Add(node);
                        break;
                    default:
                        _isFailure = true;
                        break;
                }
            }

            if (waitNode.Count == 0)
            {
                _waitNodes = waitNode;
                return Status.Running;
            }

            status = _isFailure ? Status.Failure : Status.Success;
            Reset();
            return status;
        }
    }
}