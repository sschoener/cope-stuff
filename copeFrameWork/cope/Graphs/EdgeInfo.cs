namespace cope.Graphs
{
    public struct EdgeInfo<TNode>
    {
        public readonly TNode FromNodeId;
        public readonly TNode ToNodeId;

        public EdgeInfo(TNode fromNodeId, TNode toNodeId)
        {
            FromNodeId = fromNodeId;
            ToNodeId = toNodeId;
        }

        public override string ToString()
        {
            return "Edge from " + FromNodeId + " to " + ToNodeId;
        }
    }

    public struct WeightedEdgeInfo<TNode>
    {
        public readonly TNode FromNodeId;
        public readonly TNode ToNodeId;
        public readonly float Weight;

        public WeightedEdgeInfo(TNode fromNodeId, TNode toNodeId, float weight)
        {
            FromNodeId = fromNodeId;
            ToNodeId = toNodeId;
            Weight = weight;
        }

        public override string ToString()
        {
            return "Edge from " + FromNodeId + " to " + ToNodeId + " with weight " + Weight;
        }
    }
}