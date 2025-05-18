namespace cope.Graphs
{
    public class GraphPath<TEdge>
    {
        public readonly TEdge[] Edges;
        public readonly float Length;

        public GraphPath(float length, TEdge[] edges)
        {
            Length = length;
            Edges = edges;
        }
    }
}