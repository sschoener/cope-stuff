#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Graphs
{
    /// <summary>
    /// This class enables you to do a BFS-traversal over a graph
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public class BreadthFirstWalker<TNode, TEdge> : IEnumerable<TNode>
    {
        /// <summary>
        /// Constructs a new walker given a graph to operate on and a start node.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="start">The node to start the enumeration at.</param>
        /// <param name="includeStartNode">If set to true, the enumerator will output the start node as the first item.</param>
        public BreadthFirstWalker(IGraph<TNode, TEdge> graph, TNode start, bool includeStartNode = true)
        {
            Graph = graph;
            StartNode = start;
            IncludeStartNode = includeStartNode;
        }

        /// <summary>
        /// Gets the graph this walker is operating on.
        /// </summary>
        public IGraph<TNode, TEdge> Graph { get; private set; }

        /// <summary>
        /// Gets the starting node for this walker.
        /// </summary>
        public TNode StartNode { get; private set; }

        /// <summary>
        /// Gets whether or not the starting node will be part of the enumeration.
        /// </summary>
        public bool IncludeStartNode { get; private set; }

        #region IEnumerable<TNode> Members

        public IEnumerator<TNode> GetEnumerator()
        {
            return new BreadthFirstEnumerator<TNode, TEdge>(Graph, StartNode, IncludeStartNode);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}