#region

using System;
using System.Collections.Generic;

#endregion

namespace cope.Graphs
{
    /// <summary>
    /// Represents a weighted, possibly directed adjacency matrix graph.
    /// In this implementation an adjacency matrix based graph has a fixed number of nodes which are all created when the graph is created;
    /// </summary>
    public class WeightedAdjacencyMatrixGraph : IGraph<int, int>
    {
        private const float INVALID_WEIGHT = float.NaN;

        /// <summary>
        /// Square-array indexed by nodes.
        /// [1,2] = Edge from 1 to 2
        /// If the value of [x,y] == float.NaN, then there's no edge between the two nodes.
        /// </summary>
        private readonly float[,] m_adjacencyMatrix;

        /// <summary>
        /// Constructs a new adjacency matrx graph with memory allocated for the specified number of nodes.
        /// You cannot change the number of nodes after constructing the graph.
        /// </summary>
        /// <param name="isDirected"></param>
        /// <param name="numNodes"></param>
        public WeightedAdjacencyMatrixGraph(bool isDirected, int numNodes)
        {
            IsDirected = isDirected;
            m_adjacencyMatrix = new float[numNodes,numNodes];
            RemoveAllEdges();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="graph"></param>
        public WeightedAdjacencyMatrixGraph(IGraph<int, int> graph)
        {
            IsDirected = graph.IsDirected;
            int numNodes = graph.GetNumNodes();
            m_adjacencyMatrix = new float[numNodes,numNodes];
            RemoveAllEdges();

            // other graph node ids don't necessarily start from 0 etc.
            // so we need to convert them
            var nodeIds = graph.GetAllNodes();
            var nodeToIndex = new Dictionary<int, int>(numNodes);
            int idx = 0;
            foreach (int nodeId in nodeIds)
                nodeToIndex.Add(nodeId, idx++);

            var edges = graph.GetAllWeightedEdges();
            foreach (var edgeInfo in edges)
            {
                int from = nodeToIndex[edgeInfo.FromNodeId];
                int to = nodeToIndex[edgeInfo.ToNodeId];
                AddEdgeInternal(from, to, edgeInfo.Weight);
            }
        }

        #region IGraph<int,int> Members

        /// <summary>
        /// Gets whether or not this graph is directed.
        /// </summary>
        public bool IsDirected { get; private set; }

        /// <summary>
        /// Not supported by adjacency graphs! Will throw an exception.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">AdjacencyMatrixGraphs don't support adding any more nodes than the number set at creation time.</exception>
        public int AddNode()
        {
            throw new InvalidOperationException(
                "AdjacencyMatrixGraphs don't support adding any more nodes than the number set at creation time.");
        }

        /// <summary>
        /// Returns whether or not there is a node with the specified id.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool HasNode(int nodeId)
        {
            if (nodeId < 0 || nodeId >= GetNumNodes())
                return false;
            return true;
        }

        /// <summary>
        /// Not supported by adjacency matrix graphs! Will throw an exception.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">AdjacencyMatrixGraphs don't support removing nodes.</exception>
        public bool RemoveNode(int nodeId)
        {
            throw new InvalidOperationException("AdjacencyMatrixGraphs don't support removing nodes.");
        }

        /// <summary>
        /// Adds an edge between the two specified nodes and returns the id of the newly created edge. If there already is an edge between the two nodes,
        /// it will return the id of that edge. If one of the given node Ids is invalid, it will return InvalidEdgeId.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public int AddEdge(int fromNodeId, int toNodeId)
        {
            return AddEdge(fromNodeId, toNodeId, 1f);
        }

        /// <summary>
        /// Returns whether or not this an edge exists between the two specified nodes.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public bool HasEdge(int fromNodeId, int toNodeId)
        {
            if (!HasNode(fromNodeId) || !HasNode(toNodeId))
                return false;
            return IsValidWeight(m_adjacencyMatrix[fromNodeId, toNodeId]);
        }

        /// <summary>
        /// Removes the edge between the two specified nodes. Returns true if an edge existed between the two nodes.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        public bool RemoveEdge(int fromNodeId, int toNodeId)
        {
            if (!HasNode(fromNodeId) || !HasNode(toNodeId))
                return false;
            float old = m_adjacencyMatrix[fromNodeId, toNodeId];
            m_adjacencyMatrix[fromNodeId, toNodeId] = INVALID_WEIGHT;
            if (!IsDirected)
                m_adjacencyMatrix[toNodeId, fromNodeId] = INVALID_WEIGHT;
            return IsValidWeight(old);
        }

        /// <summary>
        /// Removes the specified edge. Returns true if an edge existed between the two nodes.
        /// </summary>
        /// <param name="edgeId"></param>
        public bool RemoveEdge(int edgeId)
        {
            int from = edgeId / m_adjacencyMatrix.GetLength(0);
            int to = edgeId - (from * m_adjacencyMatrix.GetLength(0));
            return RemoveEdge(from, to);
        }

        /// <summary>
        /// Returns the id of the edge connecting the two specified nodes or InvalidEdgeId if there is no such edge.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public int GetEdge(int fromNodeId, int toNodeId)
        {
            if (!HasEdge(fromNodeId, toNodeId))
                return InvalidEdgeId;
            return fromNodeId * m_adjacencyMatrix.GetLength(0) + toNodeId;
        }

        /// <summary>
        /// Sets the weight of an edge.
        /// Returns whether or not the operation succeeded (as it may fail if there is no such edge or if the weight is not valid).
        /// </summary>
        /// <param name="edgeId"></param>
        /// <param name="weight"></param>
        public bool SetWeight(int edgeId, float weight)
        {
            int from = GetEdgeOrigin(edgeId);
            int to = GetEdgeTarget(edgeId);
            return SetWeight(from, to, weight);
        }

        /// <summary>
        /// Returns the ids of all edges that start at a specified node. Returns null if the specified node does not exist.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetEdgesFromNode(int fromNodeId)
        {
            if (!HasNode(fromNodeId))
                return null;
            int numNodes = GetNumNodes();
            List<int> nodes = new List<int>(numNodes);
            for (int i = 0; i < numNodes; i++)
                if (IsValidWeight(m_adjacencyMatrix[fromNodeId, i]))
                    nodes.Add(GetEdge(fromNodeId, i));
            return nodes;
        }

        /// <summary>
        /// Returns the ids of all edges that end at a specified node. Returns null if the specified node does not exist.
        /// </summary>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetEdgesToNode(int toNodeId)
        {
            if (!HasNode(toNodeId))
                return null;
            int numNodes = m_adjacencyMatrix.GetLength(0);
            List<int> nodes = new List<int>(numNodes);
            for (int i = 0; i < numNodes; i++)
                if (IsValidWeight(m_adjacencyMatrix[i, toNodeId]))
                    nodes.Add(GetEdge(i, toNodeId));
            return nodes;
        }

        /// <summary>
        /// Returns the ids of all nodes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetAllNodes()
        {
            int numNodes = GetNumNodes();
            int[] ids = new int[numNodes];
            for (int i = 0; i < numNodes; i++)
                ids[i] = i;
            return ids;
        }

        /// <summary>
        /// Returns the id of the target node of the specified edge.
        /// Returns InvalidNodeId if there is no such edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public int GetEdgeTarget(int edge)
        {
            int to = GetEdgeTargetInternal(edge);
            int from = GetEdgeOriginInternal(edge);
            if (!HasEdge(to, from))
                return InvalidNodeId;
            return to;
        }

        /// <summary>
        /// Returns the id of the origion/source node of the specified edge.
        /// Returns InvalidNodeId if there is no such edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public int GetEdgeOrigin(int edge)
        {
            int to = GetEdgeTargetInternal(edge);
            int from = GetEdgeOriginInternal(edge);
            if (!HasEdge(to, from))
                return InvalidNodeId;
            return from;
        }

        /// <summary>
        /// Returns the number of nodes in this graph.
        /// </summary>
        /// <returns></returns>
        public int GetNumNodes()
        {
            return m_adjacencyMatrix.GetLength(0);
        }

        /// <summary>
        /// Returns the number of edges in this graph.
        /// </summary>
        /// <returns></returns>
        public int GetNumEdges()
        {
            int numNodes = m_adjacencyMatrix.GetLength(0);
            int edgeCount = 0;
            for (int fromNode = 0; fromNode < numNodes; fromNode++)
                for (int toNode = 0; toNode < numNodes; toNode++)
                    if (IsValidWeight(m_adjacencyMatrix[fromNode, toNode]))
                        edgeCount++;
            if (!IsDirected)
                return edgeCount / 2;
            return edgeCount;
        }

        /// <summary>
        /// Gets the id representing an invalid node.
        /// </summary>
        public int InvalidNodeId
        {
            get { return -1; }
        }

        /// <summary>
        /// Gets the id representing an invalid edge.
        /// </summary>
        public int InvalidEdgeId
        {
            get { return -1; }
        }

        /// <summary>
        /// Adds an edge to the graph, connecting the two specified nodes. The edge will have the specified weight.
        /// Returns the id of the new edge or InvalidEdgeId if one of the node ids was invalid or if the given weight is invalid.
        /// If there already is an edge connecting the two nodes, that edge's weight will be set and its id returned.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public virtual int AddEdge(int fromNodeId, int toNodeId, float weight)
        {
            return AddEdgeInternal(fromNodeId, toNodeId, weight);
        }

        /// <summary>
        /// Returns the weight of an Edge. If there is no such edge, it will return float.NaN.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public virtual float GetWeight(int fromNodeId, int toNodeId)
        {
            if (!HasNode(fromNodeId) || !HasNode(toNodeId))
                return INVALID_WEIGHT;
            return m_adjacencyMatrix[fromNodeId, toNodeId];
        }

        /// <summary>
        /// Returns the weight of the edge with the given id or float.NaN if there is no such weight.
        /// </summary>
        /// <param name="edgeId"></param>
        /// <returns></returns>
        public float GetWeight(int edgeId)
        {
            int from = GetEdgeOriginInternal(edgeId);
            int to = GetEdgeTargetInternal(edgeId);
            return GetWeight(from, to);
        }

        /// <summary>
        /// Sets the weight of the edge connecting the two specified nodes.
        /// Returns whether or not the operation succeeded (as it may fail if there is no such edge or if the weight is not valid).
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public virtual bool SetWeight(int fromNodeId, int toNodeId, float weight)
        {
            if (!HasNode(fromNodeId) || !HasNode(toNodeId) || !IsValidWeight(weight))
                return false;
            if (!IsValidWeight(m_adjacencyMatrix[fromNodeId, toNodeId]))
                return false;
            m_adjacencyMatrix[fromNodeId, toNodeId] = weight;
            if (!IsDirected)
                m_adjacencyMatrix[toNodeId, fromNodeId] = weight;
            return true;
        }

        /// <summary>
        /// Returns whether or not the specified weight is considered a valid weight.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool IsValidWeight(float w)
        {
            return !float.IsNaN(w);
        }

        #endregion

        protected int GetEdgeTargetInternal(int edge)
        {
            int from = edge / m_adjacencyMatrix.GetLength(0);
            if (from > m_adjacencyMatrix.GetLength(0))
                return InvalidNodeId;
            return edge - (from * m_adjacencyMatrix.GetLength(0));
        }

        protected int GetEdgeOriginInternal(int edge)
        {
            int from = edge / m_adjacencyMatrix.GetLength(0);
            if (from > m_adjacencyMatrix.GetLength(0))
                return InvalidNodeId;
            return from;
        }

        protected void RemoveAllEdges()
        {
            var numNodes = GetNumNodes();
            for (int x = 0; x < numNodes; x++)
                for (int y = 0; y < numNodes; y++)
                    m_adjacencyMatrix[x, y] = INVALID_WEIGHT;
        }

        protected int AddEdgeInternal(int fromNodeId, int toNodeId, float weight)
        {
            if (!IsValidWeight(weight))
                return InvalidEdgeId;
            if (!HasNode(fromNodeId) || !HasNode(toNodeId) || !IsValidWeight(weight))
                return InvalidEdgeId;
            m_adjacencyMatrix[fromNodeId, toNodeId] = weight;
            if (!IsDirected)
                m_adjacencyMatrix[toNodeId, fromNodeId] = weight;
            return GetEdge(fromNodeId, toNodeId);
        }
    }
}