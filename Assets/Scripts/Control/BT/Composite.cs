using System.Collections.Generic;

namespace Game.Control.BT
{
    public abstract class Composite : Node
    {
        protected List<Node> children = new List<Node>();
        protected Composite(string name = "Composite") : base(name) { }
        public void AddChild(Node node) => children.Add(node);
        public void AddChildren(params Node[] nodes)
        {
            foreach(Node node in nodes)
                children.Add(node);
        }
    }

    public class Selector : Composite
    {
        public Selector(string name = "Selector") : base(name) { }
        public override Status Execute()
        {
            switch (children[index].Execute())
            {
                case Status.SUCCESS:
                    index = 0;
                    return status = Status.SUCCESS;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        return status = Status.FAILURE;
                    }
                    break;
            }
            return status = Status.RUNNING;
        }
    }

    public class PrioritySelector : Composite
    {
        bool ordered = false;
        public double priority = 0;
        public PrioritySelector(double priority, string name = "PrioritySelector") : base(name) => this.priority = priority;
        public override Status Execute()
        {
            if (!ordered)
            {
                children.Sort((a, b) =>
                {
                    if ((a as PrioritySelector).priority < (b as PrioritySelector).priority)
                        return -1;
                    else if ((a as PrioritySelector).priority > (b as PrioritySelector).priority)
                        return 1;
                    return 0;
                });
                ordered = true;
            }
            switch (children[index].Execute())
            {
                case Status.SUCCESS:
                    index = 0;
                    ordered = false;
                    return status = Status.SUCCESS;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        ordered = false;
                        return status = Status.FAILURE;
                    }
                    break;
            }
            return status = Status.RUNNING;
        }
    }

    public class RandomSelector : Composite
    {
        bool shuffled = false;
        public RandomSelector(string name = "RandomSelector") : base(name) { }
        public override Status Execute()
        {
            if (!shuffled)
            {
                children.Shuffle();
                shuffled = true;
            }
            switch (children[index].Execute())
            {
                case Status.SUCCESS:
                    index = 0;
                    shuffled = false;
                    return status = Status.SUCCESS;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        shuffled = false;
                        return status = Status.FAILURE;
                    }
                    break;
            }
            return status = Status.RUNNING;
        }
    }

    public class Sequence : Composite
    {
        public Sequence(string name = "Sequence") : base(name) { }
        public override Status Execute()
        {
            switch (children[index].Execute())
            {
                case Status.SUCCESS:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        return status = Status.SUCCESS;
                    }
                    break;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    index = 0;
                    return status = Status.FAILURE;
            }
            return status = Status.RUNNING;
        }
    }

    public class Parallel : Composite
    {
        public Parallel(string name = "Parallel") : base(name) { }
        public override Status Execute()
        {
            int succ_count = 0;
            for (int i = 0; i < children.Count; i++)
            {
                switch (children[i].Execute())
                {
                    case Status.SUCCESS:
                        succ_count++;
                        if (succ_count == children.Count)
                            return status = Status.SUCCESS;
                        break;
                    case Status.RUNNING:
                        break;
                    case Status.FAILURE:
                        return status = Status.FAILURE;
                }
            }
            return status = Status.RUNNING;
        }
    }

    public static class Utils
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            System.Random random = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
                n--;
            }
        }
    }
}