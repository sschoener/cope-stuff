#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace cope.Graphs
{
    /// <summary>
    /// Represents a weighted graph.
    /// </summary>
    public class WeightedGraph : IGraph<int, int>
    {
        private const int INVALID_ID = -1;
        private const float INVALID_WEIGHT = float.NaN;

        /// <summary>
        /// In the undirected case, the same edge is used for both directions.
        /// </summary>
        private readonly Dictionary<int, Edge> m_edges;

        private readonly Dictionary<int, Node> m_nodes;
        private int m_iNextEdgeId;
        private int m_iNextNodeId;

        /// <summary>
        /// Constructs a new directed graph which.
        /// </summary>
        public WeightedGraph()
        {
            m_nodes = new Dictionary<int, Node>();
            m_edges = new Dictionary<int, Edge>();
        }

        /// <summary>
        /// Copy constructor. Does not like undirected graphs.
        /// </summary>
        /// <param name="graph"></param>
        /// <exception cref="Exception">Can't copy an undirected grap!</exception>
        public WeightedGraph(IGraph<int, int> graph)
        {
            if (!graph.IsDirected)
                throw new Exception("Can't copy an undirected grap!");

            // convert their ids to our ids
            var idConverter = new Dictionary<int, int>(graph.GetNumNodes());
            var nodeIds = graph.GetAllNodes();
            foreach (int id in nodeIds)
                idConverter.Add(id, AddNode());

            var edges = graph.GetAllWeightedEdges();
            foreach (var edge in edges)
            {
                int from = idConverter[edge.FromNodeId];
                int to = idConverter[edge.ToNodeId];
                AddEdgeInternal(from, to, edge.Weight);
            }
        }

        #region IGraph<int,int> Members

        /// <summary>
        /// Gets whether or not this graph is directed.
        /// </summary>
        public bool IsDirected
        {
            get { return true; }
        }

        /// <summary>
        /// Adds a node to the graph and returns the id of the newly created node.
        /// </summary>
        /// <returns></returns>
        public int AddNode()
        {
            m_nodes.Add(m_iNextNodeId, new Node(m_iNextNodeId));
            int id = GetNewNodeId();
            return id;
        }

        /// <summary>
        /// Returns whether or not there is a node with the specified id.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool HasNode(int nodeId)
        {
            return m_nodes.ContainsKey(nodeId);
        }

        /// <summary>
        /// Removes the node with the given id and returns whether or not there was such a node.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool RemoveNode(int nodeId)
        {
            Node node;
            if (!m_nodes.TryGetValue(nodeId, out node))
            {
                foreach (Edge e in node.IncomingEdges)
                    RemoveEdge(e);
                foreach (Edge e in node.OutgoingEdges)
                    RemoveEdge(e);
                return m_nodes.Remove(nodeId);
            }
            return false;
        }

        /// <summary>
        /// Adds an edge to the graph connecting the two specified nodes and returns the id of the newly created edge.
        /// If there already is such an edge, it will return the id of that edge.
        /// If one of the specified nodes does not exist, it will return InvalidEdgeId.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public int AddEdge(int fromNodeId, int toNodeId)
        {
            var edge = GetEdgeInternal(fromNodeId, toNodeId);
            if (edge != null)
                return edge.Id;
            return AddEdge(fromNodeId, toNodeId, 1f);
        }

        /// <summary>
        /// Returns whether or not there is an edge connecting the two specified nodes.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public bool HasEdge(int fromNodeId, int toNodeId)
        {
            return GetEdgeInternal(fromNodeId, toNodeId) != null;
        }

        /// <summary>
        /// Removes the edge connecting the two specified nodes and returns whether or not such an edge existed.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public bool RemoveEdge(int fromNodeId, int toNodeId)
        {
            var edge = GetEdgeInternal(fromNodeId, toNodeId);
            if (edge == null)
                return false;
            RemoveEdge(edge);
            return true;
        }

        /// <summary>
        /// Removes the edge with the specified id from the graph and returns whether or not such an edge existed.
        /// </summary>
        /// <param name="edgeIdId"></param>
        /// <returns></returns>
        public bool RemoveEdge(int edgeIdId)
        {
            Edge edge;
            if (!m_edges.TryGetValue(edgeIdId, out edge))
                return false;
            RemoveEdge(edge);
            return true;
        }

        /// <summary>
        /// Returns the edge connecting the two specified nodes or InvalidEdgeId if there is no such edge.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public int GetEdge(int fromNodeId, int toNodeId)
        {
            var edge = GetEdgeInternal(fromNodeId, toNodeId);
            if (edge == null)
                return InvalidEdgeId;
            return edge.Id;
        }

        /// <summary>
        /// Sets the weight of the specified edge to the specified weight. Returns whether or not the operation succeeded.
        /// </summary>
        /// <param name="edgeId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public bool SetWeight(int edgeId, float weight)
        {
            if (!IsValidWeight(weight))
                return false;
            Edge e;
            if (m_edges.TryGetValue(edgeId, out e))
            {
                e.Weight = weight;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns all the id of all edges originating from a given Node. If there is no such node, it will return null.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetEdgesFromNode(int fromNodeId)
        {
            Node from;
            if (!m_nodes.TryGetValue(fromNodeId, out from))
                return null;
            int[] ids = new int[from.OutgoingEdges.Count];
            int idx = 0;
            foreach (Edge e in from.OutgoingEdges)
                ids[idx++] = e.Id;
            return ids;
        }

        /// <summary>
        /// Returns the ids of all edges ending at the specified node. If there is no such node, it will return null.
        /// </summary>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetEdgesToNode(int toNodeId)
        {
            Node to;
            if (!m_nodes.TryGetValue(toNodeId, out to))
                return null;
            int[] ids = new int[to.IncomingEdges.Count];
            int idx = 0;
            foreach (Edge e in to.IncomingEdges)
                ids[idx++] = e.Id;
            return ids;
        }

        /// <summary>
        /// Returns all node ids.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetAllNodes()
        {
            return m_nodes.Keys;
        }

        /// <summary>
        /// Returns the target node of the specified edge or InvalidNodeId if there is no such edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public int GetEdgeTarget(int edge)
        {
            Edge edgeInfo;
            if (!m_edges.TryGetValue(edge, out edgeInfo))
                return InvalidNodeId;
            return edgeInfo.To.Id;
        }

        /// <summary>
        /// Returns the origin/source node of the specified edge of InvalidNodeId if there is no such edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public int GetEdgeOrigin(int edge)
        {
            Edge edgeInfo;
            if (!m_edges.TryGetValue(edge, out edgeInfo))
                return InvalidNodeId;
            return edgeInfo.From.Id;
        }

        /// <summary>
        /// Returns the number of nodes in the graph.
        /// </summary>
        /// <returns></returns>
        public int GetNumNodes()
        {
            return m_nodes.Count;
        }

        /// <summary>
        /// Returns the number of edges in the graph.
        /// </summary>
        /// <returns></returns>
        public int GetNumEdges()
        {
            return m_edges.Count;
        }

        /// <summary>
        /// Returns whether or not a specified weight is considered valid.
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public bool IsValidWeight(float weight)
        {
            return !float.IsNaN(weight);
        }

        /// <summary>
        /// Gets the id representing an invalid node.
        /// </summary>
        public int InvalidNodeId
        {
            get { return INVALID_ID; }
        }

        /// <summary>
        /// Gets the id representing an invalid edge.
        /// </summary>
        public int InvalidEdgeId
        {
            get { return INVALID_ID; }
        }

        /// <summary>
        /// Adds an edge to the graph connecting the two specified nodes and returns the id of the newly created edge.
        /// Returns InvalidEdgeId if one of the specified nodes was invalid or if the weight was invalid.
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
        /// Returns the weight of the edge connecting the two specified edges.
        /// If there is no such edge, it will return float.NaN, which is an invalid weight.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public virtual float GetWeight(int fromNodeId, int toNodeId)
        {
            Node from;
            if (!m_nodes.TryGetValue(fromNodeId, out from))
                return INVALID_WEIGHT;
            var edge = from.OutgoingEdges.First(e => e.To.Id == toNodeId);
            if (edge == null)
                return INVALID_WEIGHT;
            return edge.Weight;
        }

        /// <summary>
        /// Returns the weight of the specified edge. If there is no such edge, it will return float.NaN, which is an invalid weight.
        /// </summary>
        /// <param name="edgeId"></param>
        /// <returns></returns>
        public float GetWeight(int edgeId)
        {
            Edge e;
            if (m_edges.TryGetValue(edgeId, out e))
                return e.Weight;
            return INVALID_WEIGHT;
        }

        /// <summary>
        /// Sets the weight of the edge connecting the two specified edges. Returns whether or not the operation succeeded.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public virtual bool SetWeight(int fromNodeId, int toNodeId, float weight)
        {
            if (!IsValidWeight(weight))
                return false;
            Node from;
            if (!m_nodes.TryGetValue(fromNodeId, out from))
                return false;
            var edge = from.OutgoingEdges.First(e => e.To.Id == toNodeId);
            if (edge == null)
                return false;
            edge.Weight = weight;
            return true;
        }

        #endregion

        protected int AddEdgeInternal(int fromNodeId, int toNodeId, float weight)
        {
            if (!IsValidWeight(weight))
                return InvalidEdgeId;
            Node from;
            if (!m_nodes.TryGetValue(fromNodeId, out from))
                return InvalidEdgeId;
            Node to;
            if (!m_nodes.TryGetValue(toNodeId, out to))
                return InvalidEdgeId;
            int edgeId = GetNewEdgeId();
            Edge e = new Edge(edgeId, from, to, weight);
            from.OutgoingEdges.Add(e);
            to.IncomingEdges.Add(e);
            m_edges.Add(edgeId, e);
            return edgeId;
        }

        private void RemoveEdge(Edge e)
        {
            e.From.OutgoingEdges.Remove(e);
            e.To.IncomingEdges.Remove(e);
            m_edges.Remove(e.Id);
        }

        private Edge GetEdgeInternal(int fromNodeId, int toNodeId)
        {
            Node from;
            if (!m_nodes.TryGetValue(fromNodeId, out from))
                return null;
            var edge = from.OutgoingEdges.FirstOrDefault(e => e.To.Id == toNodeId);
            return edge;
        }

        private int GetNewEdgeId()
        {
            return m_iNextEdgeId++;
        }

        private int GetNewNodeId()
        {
            return m_iNextNodeId++;
        }

        #region Nested type: Edge

        protected class Edge
        {
            public readonly Node From;
            public readonly Node To;
            public int Id;
            public float Weight;

            public Edge(int id, Node from, Node to, float weight)
            {
                Id = id;
                From = from;
                To = to;
                Weight = weight;
            }

            public override string ToString()
            {
                return "From " + From.Id + " to " + To.Id + " with id " + Id + ", weight " + Weight;
            }
        }

        #endregion

        #region Nested type: Node

        protected class Node
        {
            public readonly int Id;
            public readonly HashSet<Edge> IncomingEdges;
            public readonly HashSet<Edge> OutgoingEdges;

            public Node(int id)
            {
                Id = id;
                OutgoingEdges = new HashSet<Edge>();
                IncomingEdges = new HashSet<Edge>();
            }

            public override string ToString()
            {
                return "Id: " + Id + ", |Incoming| = " + IncomingEdges.Count + ", |Outgoing| = " + OutgoingEdges.Count;
            }
        }

        #endregion
    }
}