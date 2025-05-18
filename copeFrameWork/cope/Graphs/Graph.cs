namespace cope.Graphs
{
    /// <summary>
    /// Class representing an unweigthed directed graph.
    /// </summary>
    public class Graph : WeightedGraph
    {
        public Graph()
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="graph"></param>
        public Graph(IGraph<int, int> graph) : base(graph)
        {
        }

        /// <summary>
        /// Returns the weight of an edge or -1 if the edge does not exist.
        /// As this graph is unweighted, the weight is always 1f.
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
        /// Not supported by this Graph class as it is unweighted. It won't do anything.
        /// As this graph is unweigthed, this method will always return false.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <param name="weight"></param>
        public override bool SetWeight(int fromNodeId, int toNodeId, float weight)
        {
            return false;
        }

        /// <summary>
        /// Adds an edge to the graph. As this graph is unweighted, the weight parameter will be ignored.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public override int AddEdge(int fromNodeId, int toNodeId, float weight = 1f)
        {
            return base.AddEdge(fromNodeId, toNodeId, 1f);
        }
    }
}