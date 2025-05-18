namespace cope.Graphs
{
    /// <summary>
    /// Class representing an unweighted adjacency matrix graph.
    /// In this implementation an adjacency matrix based graph has a fixed number of nodes which are all created when the graph is created;
    /// </summary>
    public class AdjacencyMatrixGraph : WeightedAdjacencyMatrixGraph
    {
        /// <summary>
        /// Constructs a new graph and allocates memory for a specific amount of nodes.
        /// It is not possible to add nodes to an adjacency matrix graph.
        /// </summary>
        /// <param name="isDirected"></param>
        /// <param name="numNodes">The number of nodes this graph will have.</param>
        public AdjacencyMatrixGraph(bool isDirected, int numNodes) : base(isDirected, numNodes)
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="graph"></param>
        public AdjacencyMatrixGraph(IGraph<int, int> graph) : base(graph)
        {
        }

        /// <summary>
        /// Adds an edge between the to specified nodes to the graph.
        /// As this class does not support weighted edges, the weight parameter will be ignored.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public override int AddEdge(int fromNodeId, int toNodeId, float weight = 1f)
        {
            return base.AddEdge(fromNodeId, toNodeId, 1f);
        }

        /// <summary>
        /// Returns the weight of an edge or -1 if there is no edge between the two specified nodes.
        /// As this class does not support weighted edges, this method will return 1 for every node.
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
        /// Not supported by this class. Won't do anything at all.
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