namespace cope.Graphs
{
    /// <summary>
    /// Class representing an unweigthed undirected graph.
    /// </summary>
    public class UndirectedGraph : WeightedUndirectedGraph
    {
        /// <summary>
        /// Constructs an empty graph.
        /// </summary>
        public UndirectedGraph()
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="graph"></param>
        public UndirectedGraph(IGraph<int, int> graph) : base(graph)
        {
            foreach (var edge in GetAllEdges())
                base.SetWeight(edge, 1f);
        }

        /// <summary>
        /// Adds an edge to the graph, ignoring the weight parameter.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public override int AddEdge(int fromNodeId, int toNodeId, float weight)
        {
            return base.AddEdge(fromNodeId, toNodeId, 1f);
        }

        /// <summary>
        /// Returns the weight of the edge between the two specified nodes or -1 if there is no edge.
        /// As this graph is unweigthed it will return 1 for every edge.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public override float GetWeight(int fromNodeId, int toNodeId)
        {
            if (HasEdge(fromNodeId, toNodeId))
                return 1f;
            return -1f;
        }

        /// <summary>
        /// Not supported for this kind of graph. It won't do anything at all.
        /// As SetWeight shall return whether or not the operation succeeded, it will always return false.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <param name="weight"></param>
        public override bool SetWeight(int fromNodeId, int toNodeId, float weight)
        {
            return false;
        }
    }
}