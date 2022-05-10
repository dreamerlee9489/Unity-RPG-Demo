using UnityEngine;

namespace Game.Control.BehaviourTree
{
    public abstract class Decorator : Node
    {
        protected Node node = null;
        public Decorator(Node node = null, string name = "Decorator") : base(name) => this.node = node;
        public void SetNode(Node node) => this.node = node;
    }

    public class Inverter : Decorator
    {
        public Inverter(Node node, string name = "Inverter") : base(node, name) { }
        public override Status Execute()
        {
            switch (node.Execute())
            {
                case Status.SUCCESS:
                    return Status.FAILURE;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    return Status.SUCCESS;
            }
            return Status.RUNNING;
        }
    }

    public class UntilSuccess : Decorator
    {
        public UntilSuccess(Node node, string name = "UntilSuccess") : base(node, name) { }
        public override Status Execute()
        {
            switch (node.Execute())
            {
                case Status.SUCCESS:
                    return Status.FAILURE;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    break;
            }
            return Status.RUNNING;
        }
    }

    public class UntilFailure : Decorator
    {
        public UntilFailure(Node node, string name = "UntilFailure") : base(node, name) { }
        public override Status Execute()
        {
            switch (node.Execute())
            {
                case Status.SUCCESS:
                    break;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    return Status.FAILURE;
            }
            return Status.RUNNING;
        }
    }

    public class Limit : Decorator
    {
        int time = 0, count = 0;
        public Limit(int time, Node node, string name = "Limit") : base(node, name) { this.time = time; }
        public override Status Execute()
        {
            switch (node.Execute())
            {
                case Status.SUCCESS:
                    count++;
                    if (count >= time)
                    {
                        count = 0;
                        return Status.SUCCESS;
                    }
                    break;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    count++;
                    if (count >= time)
                    {
                        count = 0;
                        return Status.SUCCESS;
                    }
                    break;
            }
            return Status.RUNNING;
        }
    }

    public class Timer : Decorator
    {
        float cd = 0, timer = 0;
        public Timer(float cd, Node node, string name = "Timer") : base(node, name) { this.cd = cd; }
        public override Status Execute()
        {
            timer += Time.deltaTime;
            if (timer >= cd)
            {
                switch (node.Execute())
                {
                    case Status.SUCCESS:
                        timer = 0;
                        return Status.SUCCESS;
                    case Status.RUNNING:
                        break;
                    case Status.FAILURE:
                        timer = 0;
                        return Status.FAILURE;
                }
            }
            return Status.RUNNING;
        }
    }

    public class TimeLimit : Decorator
    {
        float runtime = 0, timer = 0;
        public TimeLimit(float runtime, Node node, string name = "TimeLimit") : base(node, name) { this.runtime = runtime; }
        public override Status Execute()
        {
            timer += Time.deltaTime;
            if (timer >= runtime)
            {
                timer = 0;
                return Status.SUCCESS;
            }
            return Status.RUNNING;
        }
    }
}