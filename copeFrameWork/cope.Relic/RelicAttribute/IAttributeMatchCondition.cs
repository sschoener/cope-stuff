namespace cope.Relic.RelicAttribute
{
    public interface IAttributeMatchCondition
    {
        bool SatisfiesCondition(AttributeValue attribute);
    }
}