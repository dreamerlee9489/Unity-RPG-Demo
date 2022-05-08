using System.Collections.Generic;
using UnityEngine;

namespace Game.Control.BehaviourTree
{
    public abstract class Node
    {
        public enum Status { SUCCESS, RUNNING, FAILURE }
        public Status status;
        public List<Node> children = new List<Node>();
        public double priority = 0;
        public int index = 0;
        public string name;
        public Node(string name = "Node") { this.name = name; }
        public abstract Status Execute();
    }

    public class Condition : Node
    {
        public delegate bool Task();
        public Task task;
        public Condition(Task task, string name = "Condition") : base(name) => this.task = task;
        public override Status Execute()
        {
            if (task != null && task())
                return Status.SUCCESS;
            return Status.FAILURE;
        }
    }

    public class Action : Node
    {
        public delegate Status Task();
        public Task task;
        public Action(Task task, string name = "Action") : base(name) => this.task = task;
        public override Status Execute()
        {
            if (task != null)
                return task();
            return Status.FAILURE;
        }
    }

    #region Composite
    /// <summary>
    /// 选择结点
    /// 子节点返回成功:退出
    /// 子节点返回失败:执行下一个子节点
    /// </summary>
    public class Selector : Node
    {
        public Selector(string name = "Selector") : base(name) { }
        public override Status Execute()
        {
            switch (children[index].Execute())
            {
                case Status.SUCCESS:
                    index = 0;
                    return Status.SUCCESS;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        return Status.FAILURE;
                    }
                    break;
            }
            return Status.RUNNING;
        }
    }

    public class PrioritySelector : Node
    {
        bool ordered = false;
        public PrioritySelector(string name = "PrioritySelector") : base(name) { }
        public override Status Execute()
        {
            if (!ordered)
            {
                children.Sort((a, b) =>
                {
                    if (a.priority < b.priority)
                        return -1;
                    else if (a.priority > b.priority)
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
                    return Status.SUCCESS;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        ordered = false;
                        return Status.FAILURE;
                    }
                    break;
            }
            return Status.RUNNING;
        }
    }

    public class RandomSelector : Node
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
                    return Status.SUCCESS;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    index++;
                    if (index >= children.Count)
                    {
                        index = 0;
                        shuffled = false;
                        return Status.FAILURE;
                    }
                    break;
            }
            return Status.RUNNING;
        }
    }

    public class Sequence : Node
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
                        return Status.SUCCESS;
                    }
                    break;
                case Status.RUNNING:
                    break;
                case Status.FAILURE:
                    index = 0;
                    return Status.FAILURE;
            }
            return Status.RUNNING;
        }
    }
    /// <summary>
    /// 并行结点
    /// 所有子节点都返回成功则退出
    /// 某个子节点返回失败则返回失败
    /// </summary>
    public class Parallel : Node
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
                            return Status.SUCCESS;
                        break;
                    case Status.RUNNING:
                        break;
                    case Status.FAILURE:
                        return Status.FAILURE;
                }
            }
            return Status.RUNNING;
        }
    }
    #endregion

    #region Decorator
    public class Inverter : Node
    {
        public Inverter(string name = "Inverter") : base(name) { }
        public override Status Execute()
        {
            switch (children[0].Execute())
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

    public class UntilSuccess : Node
    {
        public UntilSuccess(string name = "UntilSuccess") : base(name) { }
        public override Status Execute()
        {
            switch (children[0].Execute())
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

    public class UntilFailure : Node
    {
        public UntilFailure(string name = "UntilFailure") : base(name) { }
        public override Status Execute()
        {
            switch (children[0].Execute())
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

    public class Limit : Node
    {
        int time = 0, count = 0;
        public Limit(int time, string name = "Limit") : base(name) { this.time = time; }
        public override Status Execute()
        {
            switch (children[0].Execute())
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

    public class Timer : Node
    {
        float cd = 0, timer = 0;
        public Timer(float cd, string name = "Timer") : base(name) { this.cd = cd; }
        public override Status Execute()
        {
            timer += Time.deltaTime;
            if (timer >= cd)
            {
                switch (children[0].Execute())
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

    public class TimeLimit : Node
    {
        float runtime = 0, timer = 0;
        public TimeLimit(float runtime, string name = "TimeLimit") : base(name) { this.runtime = runtime; }
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
    #endregion

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
