using System.Collections.Generic;
using UnityEngine;

namespace Control.AStar
{
    public class Node
    {
        public readonly Transform node;
        public readonly List<Edge> edges = new List<Edge>();
        public Node last;
        public float g, h, f;
        public Node(Transform node) => this.node = node;
        public Transform GetNode() => node;
    }
}