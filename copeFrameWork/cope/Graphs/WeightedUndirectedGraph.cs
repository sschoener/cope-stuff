#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace cope.Graphs
{
    /// <summary>
    /// Represents an undirected weighted Graph.
    /// </summary>
    public class WeightedUndirectedGraph : IGraph<int, int>
    {
        private const int INVALID_ID = -1;
        private const float INVALID_WEIGHT = float.NaN;
        private readonly Dictionary<int, Edge> m_edges;
        private readonly Dictionary<int, Node> m_nodes;
        private int m_iNextEdgeId;
        private int m_iNextNodeId;

        /// <summary>
        /// Constructs an empty graph.
        /// </summary>
        public WeightedUndirectedGraph()
        {
            m_nodes = new Dictionary<int, Node>();
            m_edges = new Dictionary<int, Edge>();
        }

        /// <summary>
        /// Copy constructor. Will throw an exception if the graph to copy is directed.
        /// </summary>
        /// <param name="graph"></param>
        /// <exception cref="Exception">Cannot copy a directed graph!</exception>
        public WeightedUndirectedGraph(IGraph<int, int> graph)
        {
            if (graph.IsDirected)
                throw new Exception("Cannot copy a directed graph!");

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
        /// Gets whether or not this graph is directed. In this case it will always return false.
        /// </summary>
        public bool IsDirected
        {
            get { return false; }
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
        /// Removes the node with the specified id from the graph.
        /// Returns whether or not there was such a node.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool RemoveNode(int nodeId)
        {
            Node node;
            if (!m_nodes.TryGetValue(nodeId, out node))
            {
                var edges = node.Edges.ToArray();
                foreach (var e in edges)
                    RemoveEdge(e);
                m_nodes.Remove(nodeId);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an edge to the graph. Returns the id of the newly created edge.
        /// If there already is such an edge, it returns the id of that edge.
        /// If one of the specified nodes does not exist, it will return InvalidEdgeId.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public int AddEdge(int fromNodeId, int toNodeId)
        {
            if (HasEdge(fromNodeId, toNodeId))
                return GetEdge(fromNodeId, toNodeId);
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
        /// Removes the edge connecting the two specified nodes.
        /// Returns whether or not there was such an edge.
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
        /// Removes the edge with the given id.
        /// Returns whether or not there was such an edge.
        /// </summary>
        /// <param name="edgeId"></param>
        /// <returns></returns>
        public bool RemoveEdge(int edgeId)
        {
            Edge edgeInfo;
            if (!m_edges.TryGetValue(edgeId, out edgeInfo))
                return false;
            RemoveEdge(edgeInfo);
            return true;
        }

        /// <summary>
        /// Returns the id of the edge between the two specified nodes.
        /// If there is no such edge, it will return InvalidEdgeId.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public int GetEdge(int fromNodeId, int toNodeId)
        {
            var edge = GetEdgeInternal(fromNodeId, toNodeId);
            if (edge == null)
                return InvalidEdgeId;
            if (edge.Node1.Id == fromNodeId)
                return edge.IdToNode2;
            return edge.IdToNode1;
        }

        /// <summary>
        /// Sets the weight of the specified edge.
        /// Returns whether or not the operation succeeded (as it may fail if there is no such edge or if the weight is not valid).
        /// </summary>
        /// <param name="edgeId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public virtual bool SetWeight(int edgeId, float weight)
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
        /// Returns all edges originating from the specified node.
        /// If the specified node does not exist, it return null.
        /// </summary>
        /// <param name="fromNodeId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetEdgesFromNode(int fromNodeId)
        {
            Node from;
            if (!m_nodes.TryGetValue(fromNodeId, out from))
                return null;
            int[] ids = new int[from.Edges.Count];
            int idx = 0;
            foreach (Edge e in from.Edges)
            {
                if (e.Node1 == from)
                    ids[idx++] = e.IdToNode2;
                else
                    ids[idx++] = e.IdToNode1;
            }

            return ids;
        }

        /// <summary>
        /// Returns all edges ending at the specified node.
        /// If the specified node does not exist, it returns null.
        /// </summary>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetEdgesToNode(int toNodeId)
        {
            Node to;
            if (!m_nodes.TryGetValue(toNodeId, out to))
                return null;
            int[] ids = new int[to.Edges.Count];
            int idx = 0;
            foreach (Edge e in to.Edges)
            {
                if (e.Node1 == to)
                    ids[idx++] = e.IdToNode1;
                else
                    ids[idx++] = e.IdToNode2;
            }
            return ids;
        }

        /// <summary>
        /// Returns the ids of all nodes.
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
            if (edge == edgeInfo.IdToNode1)
                return edgeInfo.Node1.Id;
            return edgeInfo.Node2.Id;
        }

        /// <summary>
        /// Returns the origin/source node of the specified edge or InvalidNodeId if there is no such edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public int GetEdgeOrigin(int edge)
        {
            Edge edgeInfo;
            if (!m_edges.TryGetValue(edge, out edgeInfo))
                return InvalidNodeId;
            if (edge == edgeInfo.IdToNode1)
                return edgeInfo.Node2.Id;
            return edgeInfo.Node1.Id;
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
        /// Returns the total number of edges in the graph.
        /// </summary>
        /// <returns></returns>
        public int GetNumEdges()
        {
            return m_edges.Count / 2;
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
        /// Adds an edge to the graph and returns the id of the newly created edge.
        /// If there already is an edge connecting the two specified node, that edge's weight will be set to the specified value and its id will be returned
        /// Returns InvalidEdgeId if one of the specified nodes does not exist or if the weight is invalid.
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
        /// Returns the weight of the edge connecting the two specified nodes.
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
            Node to;
            if (!m_nodes.TryGetValue(toNodeId, out to))
                return INVALID_WEIGHT;
            var edge = from.Edges.First(e => e.Node2 == to);
            if (edge == null)
                return INVALID_WEIGHT;
            return edge.Weight;
        }

        /// <summary>
        /// Returns the weight of the specified edge. If there is no such edge, it will return float.NaN.
        /// </summary>
        /// <param name="edgeId"></param>
        /// <returns></returns>
        public float GetWeight(int edgeId)
        {
            var from = GetEdgeOrigin(edgeId);
            var to = GetEdgeTarget(edgeId);
            return GetWeight(from, to);
        }

        /// <summary>
        /// Sets the weight of the edge between the two specified nodes.
        /// Returns whether or not the operation succeeded (as it may fail if there is no such edge).
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
            Node to;
            if (!m_nodes.TryGetValue(toNodeId, out to))
                return false;
            var edge = from.Edges.First(e => e.Node2 == to);
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
            Node node1;
            if (!m_nodes.TryGetValue(fromNodeId, out node1))
                return InvalidEdgeId;
            Node node2;
            if (!m_nodes.TryGetValue(toNodeId, out node2))
                return InvalidEdgeId;
            int edgeIdToNode1 = GetNewEdgeId();
            int edgeIdToNode2 = GetNewEdgeId();
            Edge e = new Edge(edgeIdToNode1, edgeIdToNode2, node1, node2, weight);
            node1.Edges.Add(e);
            node2.Edges.Add(e);
            m_edges.Add(edgeIdToNode1, e);
            m_edges.Add(edgeIdToNode2, e);
            return edgeIdToNode1;
        }

        protected IEnumerable<int> GetAllEdges()
        {
            return m_edges.Keys;
        }

        private void RemoveEdge(Edge e)
        {
            e.Node1.Edges.Remove(e);
            e.Node2.Edges.Remove(e);
            m_edges.Remove(e.IdToNode1);
            m_edges.Remove(e.IdToNode2);
        }

        private Edge GetEdgeInternal(int fromNodeId, int toNodeId)
        {
            Node from;
            if (!m_nodes.TryGetValue(fromNodeId, out from))
                return null;
            Node to;
            if (!m_nodes.TryGetValue(toNodeId, out to))
                return null;
            var edge = from.Edges.FirstOrDefault(e => e.Connects(to));
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
            public readonly Node Node1;
            public readonly Node Node2;
            public int IdToNode1;
            public int IdToNode2;
            public float Weight;

            public Edge(int idToNode1, int idToNode2, Node node1, Node node2, float weight)
            {
                IdToNode1 = idToNode1;
                IdToNode2 = idToNode2;
                Node1 = node1;
                Node2 = node2;
                Weight = weight;
            }

            public Node GetTarget(int id)
            {
                if (id == IdToNode1)
                    return Node1;
                return Node2;
            }

            public Node GetOrigin(int id)
            {
                if (id == IdToNode1)
                    return Node2;
                return Node1;
            }

            public bool Connects(Node n)
            {
                return Node1 == n || Node2 == n;
            }

            public override string ToString()
            {
                return "From " + Node1.Id + " to " + Node2.Id + " with ids " + IdToNode1 + '|' + IdToNode2 + ", weight " +
                       Weight;
            }
        }

        #endregion

        #region Nested type: Node

        protected class Node
        {
            public readonly HashSet<Edge> Edges;
            public readonly int Id;

            public Node(int id)
            {
                Id = id;
                Edges = new HashSet<Edge>();
            }

            public override string ToString()
            {
                return "Id: " + Id;
            }
        }

        #endregion
    }
}