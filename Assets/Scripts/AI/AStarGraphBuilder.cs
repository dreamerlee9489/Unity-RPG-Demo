using System.Collections.Generic;
using UnityEngine;

namespace App.AI
{
	public enum EdgeDirect { BI, UNI }
	public enum HeuristicType { None, Euclidean, Manhattan }

	public class AStarNode
	{
		public Transform node;
		public List<AStarEdge> edges = new List<AStarEdge>();
		public AStarNode last;
		public float g, h, f;
		public AStarNode(Transform node) => this.node = node;
		public Transform GetNode() => node;
	}

	public class AStarEdge
	{
		public AStarNode from, to;
        public EdgeDirect direct = EdgeDirect.UNI;

		public AStarEdge(AStarNode from, AStarNode to)
		{ 
			this.from = from;
			this.to = to;
		}
    }

    public class AStarGraph 
	{
		List<AStarNode> nodes = new List<AStarNode>();
		List<AStarEdge> edges = new List<AStarEdge>();
		public List<AStarNode> path = new List<AStarNode>();

		public void AddNode(Transform node) => nodes.Add(new AStarNode(node));
		public void AddEdge(Transform fromNode, Transform toNode)
		{
			AStarNode from = FindNode(fromNode);
			AStarNode to = FindNode(toNode);
			if (from != null && to != null)
			{
				AStarEdge edge = new AStarEdge(from, to);
				edges.Add(edge);
				from.edges.Add(edge);
			}		
		}

		AStarNode FindNode(Transform node)
		{
			foreach(AStarNode n in nodes)
				if(n.GetNode() == node)
					return n;
			return null;
		}

		public bool FindPath(Transform startNode, Transform endNode)
		{
			AStarNode start = FindNode(startNode);
			AStarNode end = FindNode(endNode);
			if(start == null || end == null)
				return false;

            List<AStarNode> openList = new List<AStarNode>();
            List<AStarNode> closeList = new List<AStarNode>();

			start.g = 0;
			start.h = Heuristic(start, end, HeuristicType.None);
			start.f = start.h;
			openList.Add(start);

			while (openList.Count > 0)
			{
				AStarNode currentNode = MinFNode(openList);
				if (currentNode.node == endNode)
				{
					ConstructPath(start, end);
					return true;
				}
				openList.Remove(currentNode);
				closeList.Add(currentNode);
				AStarNode neighbor;
				foreach (AStarEdge edge in currentNode.edges)
				{
					neighbor = edge.to;
					if (closeList.IndexOf(neighbor) != -1)
						continue;
					if (openList.IndexOf(neighbor) == -1)
					{
						openList.Add(neighbor);
                        neighbor.last = currentNode;
                        neighbor.g = currentNode.g + Vector3.Distance(neighbor.node.position, currentNode.node.position);
                        neighbor.h = Heuristic(neighbor, end, HeuristicType.None);
                        neighbor.f = neighbor.g + neighbor.h;
                    }
				}
			}
            return false;
		}

        void ConstructPath(AStarNode start, AStarNode end)
        {
            path.Clear();
			path.Add(end);
			AStarNode last = end.last;
			while (last != null)
			{
				path.Insert(0, last);
				last = last.last;
			}
        }

        AStarNode MinFNode(List<AStarNode> openList)
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

        float Heuristic(AStarNode from, AStarNode to, HeuristicType type)
		{
			float distance = 0;
            switch (type)
            {
				case HeuristicType.None:
					distance = Vector3.Distance(from.node.position, to.node.position);
					break;
                case HeuristicType.Euclidean:
					distance = Mathf.Pow(from.node.transform.position.x - to.node.transform.position.x, 2) + Mathf.Pow(from.node.transform.position.z - to.node.transform.position.z, 2);
					break;
                case HeuristicType.Manhattan:
					distance = Mathf.Abs(from.node.transform.position.x - to.node.transform.position.x) + Mathf.Abs(from.node.transform.position.z - to.node.transform.position.z);
                    break;
            }
			return distance;
		}
	}

	public class AStarGraphBuildler : MonoBehaviour
    {
        [System.Serializable]
        public struct Edge
        {
            public Transform fromNode, toNode;
            public EdgeDirect direct;
        }

        public List<Transform> waypoints = new List<Transform>();
        public List<Edge> edges = new List<Edge>();
        public AStarGraph graph = new AStarGraph();

        void Awake()
        {
            BuildGraph();
        }

        public void BuildGraph()
        {
            foreach (var waypoint in waypoints)
                graph.AddNode(waypoint);
            foreach (var edge in edges)
            {
                graph.AddEdge(edge.fromNode, edge.toNode);
                if (edge.direct == EdgeDirect.BI)
                    graph.AddEdge(edge.toNode, edge.fromNode);
            }
        }
    }
}
