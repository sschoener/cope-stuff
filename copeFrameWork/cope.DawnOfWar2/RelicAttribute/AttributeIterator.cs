#region

using System.Collections.Generic;

#endregion

namespace cope.DawnOfWar2.RelicAttribute
{
    ///<summary>
    /// Provides static functions to call processor-methods on every AttributeValue in a tree of AttributeValues.
    ///</summary>
    public static class AttributeIterator
    {
        ///<summary>
        /// Calls the Process method of the specified processor on the given starting value and all its children.
        ///</summary>
        ///<param name="startingValue"></param>
        ///<param name="processor"></param>
        public static void DoForAll(AttributeValue startingValue, IAttributeProcessor processor)
        {
            AttributeCrawler crawler = new AttributeCrawler(startingValue, processor);
            crawler.Start();
        }

        ///<summary>
        /// Calls the Process method of the specified processor on the given starting values and all their children.
        ///</summary>
        ///<param name="startingValues"></param>
        ///<param name="processor"></param>
        public static void DoForAll(IEnumerable<AttributeValue> startingValues, IAttributeProcessor processor)
        {
            AttributeCrawler search = new AttributeCrawler(startingValues, processor);
            search.Start();
        }

        ///<summary>
        /// Calls the Process method of the specified processor on the given starting value and all its childs
        /// and returns the results of the calls.
        ///</summary>
        ///<param name="startingValue"></param>
        ///<param name="processor"></param>
        public static IEnumerable<T> DoForAll<T>(AttributeValue startingValue, IAttributeProcessor<T> processor)
        {
            AttributeCrawler<T> crawler = new AttributeCrawler<T>(startingValue, processor);
            return crawler.Start();
        }

        ///<summary>
        /// Calls the Process method of the specified processor on the given starting values and all their childs
        /// and returns the results of the calls.
        ///</summary>
        ///<param name="startingValues"></param>
        ///<param name="processor"></param>
        public static IEnumerable<T> DoForAll<T>(IEnumerable<AttributeValue> startingValues,
                                                 IAttributeProcessor<T> processor)
        {
            AttributeCrawler<T> search = new AttributeCrawler<T>(startingValues, processor);
            return search.Start();
        }
    }

    internal class AttributeCrawler
    {
        #region fields

        private readonly IAttributeProcessor m_processor;
        private readonly List<AttributeValue> m_startingValues;

        #endregion

        #region ctors

        private AttributeCrawler(IAttributeProcessor processor)
        {
            m_processor = processor;
            m_startingValues = new List<AttributeValue>();
        }

        public AttributeCrawler(AttributeValue startingValue, IAttributeProcessor processor)
            : this(processor)
        {
            m_startingValues.Add(startingValue);
        }

        public AttributeCrawler(IEnumerable<AttributeValue> startingValues, IAttributeProcessor processor)
            : this(processor)
        {
            m_startingValues.AddRange(startingValues);
        }

        #endregion

        #region methods

        public void Start()
        {
            foreach (var attributeValue in m_startingValues)
                Process(attributeValue);
        }

        private void Process(AttributeValue attribute)
        {
            m_processor.Process(attribute);
            if (attribute.DataType == AttributeDataType.Table)
            {
                foreach (var child in attribute.Data as AttributeTable)
                    Process(child);
            }
        }

        #endregion
    }

    internal class AttributeCrawler<T>
    {
        #region fields

        private readonly IAttributeProcessor<T> m_processor;
        private readonly List<AttributeValue> m_startingValues;

        #endregion

        #region ctors

        private AttributeCrawler(IAttributeProcessor<T> processor)
        {
            m_processor = processor;
            m_startingValues = new List<AttributeValue>();
        }

        public AttributeCrawler(AttributeValue startingValue, IAttributeProcessor<T> processor)
            : this(processor)
        {
            m_startingValues.Add(startingValue);
        }

        public AttributeCrawler(IEnumerable<AttributeValue> startingValues, IAttributeProcessor<T> processor)
            : this(processor)
        {
            m_startingValues.AddRange(startingValues);
        }

        #endregion

        #region methods

        public IEnumerable<T> Start()
        {
            List<T> results = new List<T>();
            foreach (var attributeValue in m_startingValues)
                Process(attributeValue, results);
            return results;
        }

        private void Process(AttributeValue attribute, ICollection<T> results)
        {
            results.Add(m_processor.Process(attribute));
            if (attribute.DataType == AttributeDataType.Table && attribute.Data != null)
            {
                foreach (var child in attribute.Data as AttributeTable)
                    Process(child, results);
            }
        }

        #endregion
    }
}