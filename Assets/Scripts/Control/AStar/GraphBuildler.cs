using System.Collections.Generic;
using UnityEngine;

namespace Control.AStar
{
    public class GraphBuildler : MonoBehaviour
    {
        [System.Serializable]
        public struct Edge
        {
            public Transform fromNode, toNode;
            public EdgeDirect direct;
        }

        public List<Transform> waypoints = new List<Transform>();
        public List<Edge> edges = new List<Edge>();
        public Graph graph = new Graph();

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
                if (edge.direct == EdgeDirect.Bi)
                    graph.AddEdge(edge.toNode, edge.fromNode);
            }
        }
    }
}