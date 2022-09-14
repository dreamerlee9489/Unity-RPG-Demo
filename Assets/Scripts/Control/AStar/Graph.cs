using System.Collections.Generic;
using UnityEngine;

namespace Control.AStar
{
    public enum HeuristicType
    {
        None,
        Euclidean,
        Manhattan
    }

    public class Graph
    {
        private readonly List<Node> _nodes = new List<Node>();
        private readonly List<Edge> _edges = new List<Edge>();
        public readonly List<Node> path = new List<Node>();

        public void AddNode(Transform node) => _nodes.Add(new Node(node));

        public void AddEdge(Transform fromNode, Transform toNode)
        {
            Node from = FindNode(fromNode);
            Node to = FindNode(toNode);
            if (from != null && to != null)
            {
                Edge edge = new Edge(from, to);
                _edges.Add(edge);
                from.edges.Add(edge);
            }
        }

        Node FindNode(Transform node)
        {
            foreach (Node n in _nodes)
                if (n.GetNode() == node)
                    return n;
            return null;
        }

        public bool FindPath(Transform startNode, Transform endNode)
        {
            Node start = FindNode(startNode);
            Node end = FindNode(endNode);
            if (start == null || end == null)
                return false;

            List<Node> openList = new List<Node>();
            List<Node> closeList = new List<Node>();

            start.g = 0;
            start.h = Heuristic(start, end, HeuristicType.None);
            start.f = start.h;
            openList.Add(start);

            while (openList.Count > 0)
            {
                Node currentNode = MinFNode(openList);
                if (currentNode.node == endNode)
                {
                    ConstructPath(start, end);
                    return true;
                }

                openList.Remove(currentNode);
                closeList.Add(currentNode);
                Node neighbor;
                foreach (Edge edge in currentNode.edges)
                {
                    neighbor = edge.to;
                    if (closeList.IndexOf(neighbor) != -1)
                        continue;
                    if (openList.IndexOf(neighbor) == -1)
                    {
                        openList.Add(neighbor);
                        neighbor.last = currentNode;
                        neighbor.g = currentNode.g +
                                     Vector3.Distance(neighbor.node.position, currentNode.node.position);
                        neighbor.h = Heuristic(neighbor, end, HeuristicType.None);
                        neighbor.f = neighbor.g + neighbor.h;
                    }
                }
            }

            return false;
        }

        void ConstructPath(Node start, Node end)
        {
            path.Clear();
            path.Add(end);
            Node last = end.last;
            while (last != null)
            {
                path.Insert(0, last);
                last = last.last;
            }
        }

        Node MinFNode(List<Node> openList)
        {
            float minF = openList[0].f;
            int index = 0;
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].f < minF)
                {
                    minF = openList[i].f;
                    index = i;
                }
            }

            return openList[index];
        }

        float Heuristic(Node from, Node to, HeuristicType type)
        {
            float distance = 0;
            switch (type)
            {
                case HeuristicType.None:
                    distance = Vector3.Distance(from.node.position, to.node.position);
                    break;
                case HeuristicType.Euclidean:
                    distance = Mathf.Pow(from.node.transform.position.x - to.node.transform.position.x, 2) +
                               Mathf.Pow(from.node.transform.position.z - to.node.transform.position.z, 2);
                    break;
                case HeuristicType.Manhattan:
                    distance = Mathf.Abs(from.node.transform.position.x - to.node.transform.position.x) +
                               Mathf.Abs(from.node.transform.position.z - to.node.transform.position.z);
                    break;
            }

            return distance;
        }
    }
}