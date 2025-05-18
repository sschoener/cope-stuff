namespace cope.Relic.RelicAttribute
{
    /// <summary>
    /// Interface used by the AttributeIterator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAttributeProcessor<out T>
    {
        ///<summary>
        /// This method is called by AttributeIterator on every single AttributeValue.
        /// Its return value is then added to the collection of return values.
        ///</summary>
        ///<param name="data"></param>
        ///<returns></returns>
        T Process(AttributeValue data);
    }

    /// <summary>
    /// Interface used by the AttributeIterator.
    /// </summary>
    public interface IAttributeProcessor
    {
        ///<summary>
        /// This method is called by AttributeIterator on every single AttributeValue.
        ///</summary>
        ///<param name="data"></param>
        void Process(AttributeValue data);
    }
}