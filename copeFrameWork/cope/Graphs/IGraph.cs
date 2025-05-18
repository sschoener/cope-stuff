#region

using System.Collections.Generic;

#endregion

namespace cope.Graphs
{
    public interface IGraph<TNode, TEdge>
    {
        /// <summary>
        /// Gets whether the graph is directed.
        /// </summary>
        bool IsDirected { get; }

        /// <summary>
        /// Gets the Id denoting an invalid node.
        /// </summary>
        TNode InvalidNodeId { get; }

        /// <summary>
        /// Gets the Id doniting an invalid edge.
        /// </summary>
        TEdge InvalidEdgeId { get; }

        /// <summary>
        /// Adds a node to the graph and returns the node's id or -1 if it failed.
        /// </summary>
        /// <returns>Returns the new node's Id or InvalidNodeId if it failed.</returns>
        TNode AddNode();

        /// <summary>
        /// Returns whether a node with the specified node Id exists.
        /// </summary>
        /// <param name="nodeId">The node Id to check.</param>
        /// <returns></returns>
        bool HasNode(TNode nodeId);

        /// <summary>
        /// Removes the specified node; returns false if there is no node with the specified Id, otherwise true.
        /// </summary>
        /// <param name="nodeId">The Id of the node to remove.</param>
        /// <returns></returns>
        bool RemoveNode(TNode nodeId);

        /// <summary>
        /// Adds an edge between the two specified nodes to the graph.
        /// </summary>
        /// <param name="fromNodeId">The id of the source-node of the edge.</param>
        /// <param name="toNodeId">The id of the target-node of the edge.</param>
        TEdge AddEdge(TNode fromNodeId, TNode toNodeId);

        /// <summary>
        /// Adds an edge with a specified weight between the two specified nodes to the graph.
        /// </summary>
        /// <param name="fromNodeId">The id of the source-node of the edge.</param>
        /// <param name="toNodeId">The id of the target-node of the edge.</param>
        /// <param name="weight">The weight of the edge.</param>
        TEdge AddEdge(TNode fromNodeId, TNode toNodeId, float weight);

        /// <summary>
        /// Returns whether an edge exists between the two specified nodes.
        /// </summary>
        /// <param name="fromNodeId">The id of the source-node of the edge.</param>
        /// <param name="toNodeId">The id of the target-node of the edge.</param>
        /// <returns></returns>
        bool HasEdge(TNode fromNodeId, TNode toNodeId);

        /// <summary>
        /// Removes the edge between the two specified nodes. Returns true if there was a an edge connecting the nodes.
        /// </summary>
        /// <param name="fromNodeId">The id of the source-node of the edge.</param>
        /// <param name="toNodeId">The id of the target-node of the edge.</param>
        /// <returns></returns>
        bool RemoveEdge(TNode fromNodeId, TNode toNodeId);

        /// <summary>
        /// Removes the specifed edge. Returns true if there was a an edge connecting the nodes.
        /// </summary>
        /// <param name="edgeId">The edge to remove.</param>
        /// <returns></returns>
        bool RemoveEdge(TEdge edgeId);

        /// <summary>
        /// Returns the edge between the specified nodes.
        /// </summary>
        /// <param name="fromNodeId">The id of the source-node of the edge.</param>
        /// <param name="toNodeId">The id of the target-node of the edge.</param>
        /// <returns></returns>
        TEdge GetEdge(TNode fromNodeId, TNode toNodeId);

        /// <summary>
        /// Returns the weight of the edge between the two specified nodes.
        /// </summary>
        /// <param name="fromNodeId">The id of the source-node of the edge.</param>
        /// <param name="toNodeId">The id of the target-node of the edge.</param>
        /// <returns></returns>
        float GetWeight(TNode fromNodeId, TNode toNodeId);

        /// <summary>
        /// Returns the weight of the specified node.
        /// </summary>
        /// <param name="edgeId">The id of the edge to get the weight of.</param>
        /// <returns></returns>
        float GetWeight(TEdge edgeId);

        /// <summary>
        /// Sets the weight of the edge between the two specified nodes. Returns whether or not the operation succeeded (as it may fail if there is no such edge).
        /// </summary>
        /// <param name="fromNodeId">The id of the source-node of the edge.</param>
        /// <param name="toNodeId">The id of the target-node of the edge.</param>
        /// <param name="weight">The new weight of the edge.</param>
        /// <returns></returns>
        bool SetWeight(TNode fromNodeId, TNode toNodeId, float weight);

        /// <summary>
        /// Sets the weight of the edge with the given id. Returns whether or not the operation succeeded (as it may fail if there is no such edge).
        /// </summary>
        /// <param name="edgeId">The id of the edge to set the weight of.</param>
        /// <param name="weight">The new weight of the edge.</param>
        /// <returns></returns>
        bool SetWeight(TEdge edgeId, float weight);

        /// <summary>
        /// Gets the ids of all nodes the specified node has an edge to.
        /// </summary>
        /// <param name="fromNodeId">The node id to get edges from.</param>
        /// <returns></returns>
        IEnumerable<TEdge> GetEdgesFromNode(TNode fromNodeId);

        /// <summary>
        /// Gets the ids of all nodes that have an edge to the specified node.
        /// </summary>
        /// <param name="toNodeId">The node id to get edges to.</param>
        /// <returns></returns>
        IEnumerable<TEdge> GetEdgesToNode(TNode toNodeId);

        /// <summary>
        /// Returns all node ids in the graph.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TNode> GetAllNodes();

        /// <summary>
        /// Returns the target of an edge.
        /// </summary>
        /// <param name="edge">The edge to get the target of.</param>
        /// <returns></returns>
        TNode GetEdgeTarget(TEdge edge);

        /// <summary>
        /// Returns the origin of an edge.
        /// </summary>
        /// <param name="edge">The edge to get the target of.</param>
        /// <returns></returns>
        TNode GetEdgeOrigin(TEdge edge);

        /// <summary>
        /// Returns the number of nodes in the graph.
        /// </summary>
        /// <returns></returns>
        int GetNumNodes();

        /// <summary>
        /// Returns the number of edges in the graph.
        /// </summary>
        /// <returns></returns>
        int GetNumEdges();

        /// <summary>
        /// Returns whether or not the specified weight is considered valid.
        /// </summary>
        /// <returns></returns>
        bool IsValidWeight(float weight);
    }
}