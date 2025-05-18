namespace cope.DawnOfWar2.RelicAttribute
{
    public interface IAttributeMatchCondition
    {
        bool SatisfiesCondition(AttributeValue attribute);
    }
}