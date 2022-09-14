namespace Control.AStar
{
    public enum EdgeDirect
    {
        Bi,
        Uni
    }
    
    public class Edge
    {
        public Node from, to;
        public EdgeDirect direct = EdgeDirect.Uni;

        public Edge(Node from, Node to)
        {
            this.from = from;
            this.to = to;
        }
    }
}