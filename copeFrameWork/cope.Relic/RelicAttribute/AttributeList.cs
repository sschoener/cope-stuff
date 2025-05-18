namespace cope.Relic.RelicAttribute
{
    /// <summary>
    /// List of attributes that may share a common key. Used in CoH2's RGDs to dinstinguish it from tables.
    /// </summary>
    public class AttributeList : AttributeTable
    {

        public override AttributeValueType Value
        {
            get { return AttributeValueType.List; }
        }

        public override string ToString()
        {
            return "Attrib List, " + ChildCount + " entries";
        }

        public override AttributeTable GClone()
        {
            AttributeList table = new AttributeList();
            foreach (var attributeValue in m_values)
                table.AddValue(attributeValue.GClone());
            return table;
        }
    }
}