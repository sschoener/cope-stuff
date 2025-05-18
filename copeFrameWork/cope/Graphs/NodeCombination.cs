namespace cope.Graphs
{
    /// <summary>
    /// Represents a read-only combination of nodes suitable as a key in arrays and for all other purposes.
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public struct NodeCombination<TNode>
    {
        public NodeCombination(TNode n1, TNode n2)
            : this()
        {
            Node1 = n1;
            Node2 = n2;
        }

        public TNode Node1 { get; private set; }
        public TNode Node2 { get; private set; }

        public override int GetHashCode()
        {
            return Node1.GetHashCode() ^ Node2.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            var other = (NodeCombination<TNode>) obj;
            return other.Node1.Equals(Node1) && other.Node2.Equals(Node2);
        }

        public override string ToString()
        {
            return "Node 1: " + Node1 + ", Node 2:" + Node2;
        }
    }
}