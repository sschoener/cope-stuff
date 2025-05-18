#region

using System;
using System.Linq;

#endregion

namespace cope.Graphs
{
    /// <summary>
    /// Static helper class for creating random graphs.
    /// </summary>
    public static class RandomGraph
    {
        /// <summary>
        /// Generates a graph with a random set of nodes. The Erdos-Renyi model uses two parameters, one to determine the number of nodes in the graph
        /// and another representing the probability for two nodes to be connected.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph">The graph to operate on. The function assumes that the graph does not yet contain any nodes or edges.</param>
        /// <param name="n">The number of nodes the graph will have.</param>
        /// <param name="p">The probability that two nodes are connected.</param>
        /// <param name="rng">Custom random number generator for the proability. If this is null, a new rng will be created.</param>
        public static void ErdosRenyiModel<TNode, TEdge>(IGraph<TNode, TEdge> graph, int n, double p, Random rng = null)
        {
            for (int i = 0; i < n; i++)
                graph.AddNode();
            AddRandomEdges(graph, p, rng);
        }

        /// <summary>
        /// Adds a bunch of random edges to a graph assuming it already has nodes in it. This function assumes that the graph does not yet have any edges.
        /// The generation of edges is controlled given a single parameter which defines the probability of two nodes being connected by an edge.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph">The graph to operate on.</param>
        /// <param name="p">The probability that two nodes are connected.</param>
        /// <param name="rng">Custom random number generator for the proability. If this is null, a new rng will be created.</param>
        public static void AddRandomEdges<TNode, TEdge>(IGraph<TNode, TEdge> graph, double p, Random rng = null)
        {
            if (rng == null)
                rng = new Random();
            p = MathUtil.Limit(p, 0f, 1f);
            var nodes = graph.GetAllNodes();
            if (graph.IsDirected)
            {
                foreach (var n1 in nodes)
                {
                    foreach (var n2 in nodes)
                    {
                        if (n1.Equals(n2))
                            continue;
                        if (rng.NextDouble() <= p)
                            graph.AddEdge(n1, n2);
                    }
                }
            }
            else
            {
                var nodeArray = nodes.ToArray();
                for (int i = 0; i < nodeArray.Length; i++)
                {
                    for (int j = i + 1; j < nodeArray.Length; j++)
                    {
                        if (rng.NextDouble() <= p)
                            graph.AddEdge(nodeArray[i], nodeArray[j]);
                    }
                }
            }
        }
    }
}