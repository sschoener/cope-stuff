#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Graphs
{
    /// <summary>
    /// Implements a depth first enumerator
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    internal class DepthFirstEnumerator<TNode, TEdge> : IEnumerator<TNode>
    {
        private readonly IGraph<TNode, TEdge> m_graph;
        private readonly Stack<TNode> m_nextNodes;
        private readonly TNode m_startNode;
        private readonly HashSet<TNode> m_visitedNodes;

        /// <summary>
        /// Constructs a new enumerator given a graph to operate on and a start node.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="start">The node to start the enumeration at.</param>
        /// <param name="includeStartNode">If set to true, the enumerator will output the start node as the first item.</param>
        public DepthFirstEnumerator(IGraph<TNode, TEdge> graph, TNode start, bool includeStartNode = true)
        {
            m_graph = graph;
            m_startNode = start;
            m_nextNodes = new Stack<TNode>();
            m_visitedNodes = new HashSet<TNode>();
            IncludeStartNode = includeStartNode;
            Reset();
        }

        /// <summary>
        /// Gets whether or not the start node of the enumeration is returned during the enumeration.
        /// </summary>
        public bool IncludeStartNode { get; private set; }

        /// <summary>
        /// Gets the total number of nodes that have been visited.
        /// </summary>
        public int NodesVisited { get; private set; }

        #region IEnumerator<TNode> Members

        public void Dispose()
        {
            return;
        }

        /// <summary>
        /// Moves to the next node. Returns false if there are no nodes left.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            // DFS code
            if (!Current.Equals(m_graph.InvalidNodeId))
            {
                var edges = m_graph.GetEdgesFromNode(Current);
                foreach (var edge in edges)
                {
                    TNode target = m_graph.GetEdgeTarget(edge);
                    if (!m_visitedNodes.Contains(target))
                    {
                        m_nextNodes.Push(target);
                        m_visitedNodes.Add(target);
                    }
                }
            }
            if (m_nextNodes.Count == 0)
                return false;
            Current = m_nextNodes.Pop();
            NodesVisited++;
            return true;
        }

        public void Reset()
        {
            m_visitedNodes.Clear();
            m_nextNodes.Clear();
            if (IncludeStartNode)
            {
                Current = m_graph.InvalidNodeId;
                m_nextNodes.Push(m_startNode);
                m_visitedNodes.Add(m_startNode);
            }
            else
                Current = m_startNode;
            NodesVisited = 0;
        }

        /// <summary>
        /// Gets the current node.
        /// </summary>
        public TNode Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }
}