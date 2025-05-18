#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace cope.Graphs
{
    /// <summary>
    /// Provides extension methods for the IGraph-interface.
    /// </summary>
    public static class GraphExt
    {
        /// <summary>
        /// Adds a node to the graph and adds edges from the node to the specified target nodes.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="toNodeIds">The nodes to add edges to.</param>
        /// <returns></returns>
        public static TNode AddNodeWithEdges<TNode, TEdge>(this IGraph<TNode, TEdge> graph, params TNode[] toNodeIds)
        {
            TNode nodeId = graph.AddNode();
            graph.AddEdge(nodeId, toNodeIds);
            return nodeId;
        }

        /// <summary>
        /// Adds a node to the graph and adds edges from the node to the specified target nodes.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="toNodeIds">IEnumerable of the nodes to add edges to.</param>
        /// <returns></returns>
        public static TNode AddNodeWithEdges<TNode, TEdge>(this IGraph<TNode, TEdge> graph, IEnumerable<TNode> toNodeIds)
        {
            TNode nodeId = graph.AddNode();
            graph.AddEdge(nodeId, toNodeIds);
            return nodeId;
        }

        /// <summary>
        /// Takes a source node and a collection of target nodes and constructs edges from the source to the target nodes.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="fromNodeId">The source node.</param>
        /// <param name="toNodeIds">The target nodes.</param>
        public static void AddEdge<TNode, TEdge>(this IGraph<TNode, TEdge> graph, TNode fromNodeId,
                                                 params TNode[] toNodeIds)
        {
            foreach (TNode t in toNodeIds)
                graph.AddEdge(fromNodeId, t);
        }

        /// <summary>
        /// Takes a source node and a collection of target nodes and constructs edges from the source to the target nodes.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="fromNodeId">The source node.</param>
        /// <param name="toNodeIds">The target nodes.</param>
        public static void AddEdge<TNode, TEdge>(this IGraph<TNode, TEdge> graph, TNode fromNodeId,
                                                 IEnumerable<TNode> toNodeIds)
        {
            foreach (var nodeId in toNodeIds)
                graph.AddEdge(fromNodeId, nodeId);
        }

        /// <summary>
        /// Takes a source node and a collection of target nodes and removs all edges from the source to the target nodes.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="fromNodeId">The source node.</param>
        /// <param name="toNodeIds">The target nodes.</param>
        public static void RemoveEdge<TNode, TEdge>(this IGraph<TNode, TEdge> graph, TNode fromNodeId,
                                                    IEnumerable<TNode> toNodeIds)
        {
            foreach (var nodeId in toNodeIds)
                graph.RemoveEdge(fromNodeId, nodeId);
        }

        /// <summary>
        /// Returns all neighbors of a given node. Each cell this cell has a edge to is considered a neighbor.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IEnumerable<TNode> GetNeighbors<TNode, TEdge>(this IGraph<TNode, TEdge> graph, TNode node)
        {
            return graph.GetEdgesFromNode(node).Select(graph.GetEdgeTarget);
        }

        /// <summary>
        /// Returns alls edges of the graph.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<EdgeInfo<TNode>> GetAllEdges<TNode, TEdge>(this IGraph<TNode, TEdge> graph)
        {
            var nodeIds = graph.GetAllNodes();
            var graphEdges = new EdgeInfo<TNode>[graph.GetNumEdges()];
            int idx = 0;
            foreach (var fromNodeId in nodeIds)
            {
                var edges = graph.GetEdgesFromNode(fromNodeId);
                foreach (var edge in edges)
                {
                    var edgeInfo = new EdgeInfo<TNode>(fromNodeId, graph.GetEdgeTarget(edge));
                    if (!graph.IsDirected &&
                        graphEdges.Any(
                            ei => edgeInfo.FromNodeId.Equals(ei.ToNodeId) && edgeInfo.ToNodeId.Equals(ei.FromNodeId)))
                        continue;
                    graphEdges[idx++] = edgeInfo;
                }
            }
            return graphEdges;
        }

        /// <summary>
        /// Returns alls edges (including their weight) of the graph.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<WeightedEdgeInfo<TNode>> GetAllWeightedEdges<TNode, TEdge>(
            this IGraph<TNode, TEdge> graph)
        {
            var nodeIds = graph.GetAllNodes();
            var graphEdges = new WeightedEdgeInfo<TNode>[graph.GetNumEdges()];
            int idx = 0;
            foreach (var fromNodeId in nodeIds)
            {
                var edges = graph.GetEdgesFromNode(fromNodeId);
                foreach (var edge in edges)
                {
                    TNode toNode = graph.GetEdgeTarget(edge);
                    float weight = graph.GetWeight(fromNodeId, toNode);
                    var edgeInfo = new WeightedEdgeInfo<TNode>(fromNodeId, toNode, weight);
                    if (!graph.IsDirected &&
                        graphEdges.Any(
                            ei => edgeInfo.FromNodeId.Equals(ei.ToNodeId) && edgeInfo.ToNodeId.Equals(ei.FromNodeId)))
                        continue;
                    graphEdges[idx++] = edgeInfo;
                }
            }
            return graphEdges;
        }

        /// <summary>
        /// Returns whether or not the graph is regular.
        /// A regular graph has the same degree of edges on every node.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static bool IsRegular<TNode, TEdge>(this IGraph<TNode, TEdge> graph)
        {
            var nodes = graph.GetAllNodes();
            int c = -1;
            foreach (var n in nodes)
                if (c < 0)
                    c = graph.GetEdgesFromNode(n).Count();
                else if (c != graph.GetEdgesFromNode(n).Count())
                    return false;
            return true;
        }

        /// <summary>
        /// Returns the shortest path between the two specified nodes using Dijkstra's algorithm.
        /// If there is no path connecting the two nodes, the function will return null.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static GraphPath<TEdge> ShortestPath<TNode, TEdge>(this IGraph<TNode, TEdge> graph, TNode from, TNode to)
        {
            var edgeToNode = Dijkstra(graph, from);
            return ShortestPath(graph, edgeToNode, from, to);
        }

        /// <summary>
        /// Returns the shortest path between the two specified nodes using the output of Dijkstra's algorithm.
        /// If there is no path connecting the two nodes, the function will return null.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="dijkstraMap">The Dijkstra map is the output of Dijkstra's algorithm: It's a dictionary indexed by the nodes which stores both the distance of
        /// and the edge leading to the node for every reachable node of the graph.</param>
        /// <param name="from">The node Dijkstra's algorithm started with. This is required in order to determine where a path needs to start.</param>
        /// <param name="to">The node to find a path to.</param>
        /// <returns></returns>
        public static GraphPath<TEdge> ShortestPath<TNode, TEdge>(this IGraph<TNode, TEdge> graph,
                                                                  Dictionary<TNode, MutableTuple<float, TEdge>>
                                                                      dijkstraMap, TNode from, TNode to)
        {
            var edgeToNode = dijkstraMap;

            if (!edgeToNode.ContainsKey(to))
                return null;
            // build path
            float totalLength = 0;
            var path = new List<TEdge>();
            var currentNode = to;
            while (!currentNode.Equals(from))
            {
                var toNode = edgeToNode[currentNode];
                totalLength += graph.GetWeight(toNode.Item2);
                currentNode = graph.GetEdgeOrigin(toNode.Item2);
                path.Add(toNode.Item2);
            }
            path.Reverse();
            return new GraphPath<TEdge>(totalLength, path.ToArray());
        }

        /// <summary>
        /// Runs Dijkstra's algorithm on the graph for the given starting node. It returns a dictionary containing information about the graph:
        /// It is indexed by the nodes and for each node it contains a Tuple consisting of the distance to the node and the edge leading to the node.
        /// Using this information, a path to every reachable node can be rebuild. If a node is not part of the dictionary, then it is not reachable
        /// from the specified starting node.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="from">The node to start Dijkstra's algorithm with.</param>
        /// <returns></returns>
        public static Dictionary<TNode, MutableTuple<float, TEdge>> Dijkstra<TNode, TEdge>(
            this IGraph<TNode, TEdge> graph, TNode from)
        {
            // for each node this dictionary contains the edge leading to this node and the distance to the node
            var edgeToNode = new Dictionary<TNode, MutableTuple<float, TEdge>>
                                 {{from, new MutableTuple<float, TEdge>(0, default(TEdge))}};

            // heap stores a tuple containing the total distance of a node and the node itself for each node
            var heap = new DelegateHeap<MutableTuple<float, TNode>>((t1, t2) => -t1.Item1.CompareTo(t2.Item1));
            heap.Insert(new MutableTuple<float, TNode>(0f, from));

            while (heap.Count > 0)
            {
                // the heap's head is the node with the lowest distance
                var node = heap.GetHead();
                heap.RemoveHead();

                foreach (var e in graph.GetEdgesFromNode(node.Item2))
                {
                    var target = graph.GetEdgeTarget(e);
                    float currentDist = node.Item1 + graph.GetWeight(e);

                    MutableTuple<float, TEdge> nodeInfo;
                    if (!edgeToNode.TryGetValue(target, out nodeInfo)) // does the node already have a distance?
                    {
                        edgeToNode.Add(target, new MutableTuple<float, TEdge>(currentDist, e));
                        heap.Insert(new MutableTuple<float, TNode>(currentDist, target));
                    }

                    else if (currentDist < nodeInfo.Item1) // is the current path shorter?
                    {
                        nodeInfo.Item1 = currentDist;
                        nodeInfo.Item2 = e;
                        heap.Update(t => t.Item2.Equals(target), t =>
                                                                     {
                                                                         t.Item1 = currentDist;
                                                                         return;
                                                                     });
                    }
                }
            }
            return edgeToNode;
        }

        /// <summary>
        /// Implements Floyd's algorithm which calculates the shortest path for each pair of nodes.
        /// It returns a dictionary keyed by NodeCombinations which contains the length of the shortest paths.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static Dictionary<NodeCombination<TNode>, float> Floyd<TNode, TEdge>(this IGraph<TNode, TEdge> graph)
        {
            var nodes = graph.GetAllNodes();
            int numNodes = graph.GetNumNodes();
            var distances = new Dictionary<NodeCombination<TNode>, float>(numNodes * numNodes);
            foreach (var n1 in nodes)
                foreach (var n2 in nodes)
                {
                    float dist = graph.GetWeight(n1, n2);
                    if (!graph.IsValidWeight(dist))
                        dist = float.PositiveInfinity;
                    distances.Add(new NodeCombination<TNode>(n1, n2), dist);
                }
            foreach (var k in nodes)
                foreach (var i in nodes)
                    foreach (var j in nodes)
                    {
                        var key1 = new NodeCombination<TNode>(i, j);
                        var key2 = new NodeCombination<TNode>(i, k);
                        var key3 = new NodeCombination<TNode>(k, j);
                        distances[key1] = Math.Min(distances[key1], distances[key2] + distances[key3]);
                    }
            return distances;
        }

        /// <summary>
        /// Calculates the diameter of this graph. The diameter is the maximum length of the shortest paths connecting each pair of nodes.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static float Diameter<TNode, TEdge>(this IGraph<TNode, TEdge> graph)
        {
            return Diameter(graph, Floyd(graph));
        }

        /// <summary>
        /// Calculates the diameter of this graph. The diameter is the maximum length of the shortest paths connecting each pair of nodes.
        /// Takes a Floyd Map as input which is a Dictionary mapping NodeCombinations onto their length.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="floydMap"></param>
        /// <returns></returns>
        public static float Diameter<TNode, TEdge>(this IGraph<TNode, TEdge> graph,
                                                   Dictionary<NodeCombination<TNode>, float> floydMap)
        {
            return floydMap.Values.Where(f => !float.IsPositiveInfinity(f)).Max();
        }

        /// <summary>
        /// Returns the global clustering coefficient of the graph which is the average of all local clustering coefficients.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static double ClusteringCoefficient<TNode, TEdge>(this IGraph<TNode, TEdge> graph)
        {
            double total = graph.GetAllNodes().Sum(node => LocalClusteringCoefficient(graph, node));
            return total / graph.GetNumNodes();
        }

        /// <summary>
        /// Calculates the local clustering coefficient for a given node.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static double LocalClusteringCoefficient<TNode, TEdge>(this IGraph<TNode, TEdge> graph, TNode node)
        {
            var neighbors = graph.GetNeighbors(node);
            int numNeighbors = neighbors.Count();
            if (numNeighbors <= 1)
                return 1;
            int maxEdges = numNeighbors * (numNeighbors - 1);
            if (!graph.IsDirected)
                maxEdges /= 2;

            int actualNumberOfEdges = 0;
            foreach (var n in neighbors)
            {
                var nneighbors = graph.GetNeighbors(n);
                actualNumberOfEdges = nneighbors.Count(nn => neighbors.Contains(nn));
            }
            return actualNumberOfEdges / (double) maxEdges;
        }

        /// <summary>
        /// Calculates the average path length of this graph by using Floyd's algorithm.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static float AveragePathLength<TNode, TEdge>(this IGraph<TNode, TEdge> graph)
        {
            return AveragePathLength(graph, Floyd(graph));
        }

        /// <summary>
        /// Calculates the average path length of this graph. Takes a Floyd map as a parameter. A Floyd map maps NodeCombination onto the length of the path between them.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="floydMap"></param>
        /// <returns></returns>
        public static float AveragePathLength<TNode, TEdge>(this IGraph<TNode, TEdge> graph,
                                                            Dictionary<NodeCombination<TNode>, float> floydMap)
        {
            float total = floydMap.Values.Sum();
            float average = total / floydMap.Count;
            if (!graph.IsDirected)
                return average / 2f;
            return average;
        }
    }
}