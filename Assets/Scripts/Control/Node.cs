using System.Collections.Generic;

namespace Game.Control.BehaviourTree
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
        public Task task = null;
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
        public Task task = null;
        public Action(Task task, string name = "Action") : base(name) => this.task = task;
        public override Status Execute()
        {
            if (task != null)
                return task();
            return Status.FAILURE;
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
