#region

using System.Collections.Generic;

#endregion

namespace cope.Relic.RelicAttribute
{
    ///<summary>
    /// Helper class for searching through attribute structures.
    ///</summary>
    public static class AttributeSearch
    {
        ///<summary>
        /// Searches for AttributeValues with the specified key.
        ///</summary>
        ///<param name="startingValue">This is the starting point of the search.</param>
        ///<param name="key">The key to search for.</param>
        ///<param name="fullText">Set to true to enable full-text search.</param>
        ///<returns></returns>
        public static IEnumerable<AttributeValue> SearchForKey(AttributeValue startingValue, string key, bool fullText)
        {
            AttributeKeySearcher keySearcher = new AttributeKeySearcher(fullText, key);
            return Search(startingValue, keySearcher);
        }

        ///<summary>
        /// Searches for AttributeValues with the specified key.
        ///</summary>
        ///<param name="startingValues">These are the starting points of the search.</param>
        ///<param name="key">The key to search for.</param>
        ///<param name="fullText">Set to true to enable full-text search.</param>
        ///<returns></returns>
        public static IEnumerable<AttributeValue> SearchForKey(IEnumerable<AttributeValue> startingValues, string key,
                                                               bool fullText)
        {
            AttributeKeySearcher keySearcher = new AttributeKeySearcher(fullText, key);
            return Search(startingValues, keySearcher);
        }

        ///<summary>
        /// Searches for AttributeValues with the specified value (strings only).
        ///</summary>
        ///<param name="startingValue">This is the starting point of the search.</param>
        ///<param name="value">The key to search for.</param>
        ///<param name="fullText">Set to true to enable full-text search.</param>
        ///<returns></returns>
        public static IEnumerable<AttributeValue> SearchForValue(AttributeValue startingValue, string value,
                                                                 bool fullText)
        {
            AttributeValueSearcher valueSearcher = new AttributeValueSearcher(fullText, value);
            return Search(startingValue, valueSearcher);
        }

        ///<summary>
        /// Searches for AttributeValues with the specified value (strings only).
        ///</summary>
        ///<param name="startingValues">These are the starting points of the search.</param>
        ///<param name="value">The key to search for.</param>
        ///<param name="fullText">Set to true to enable full-text search.</param>
        ///<returns></returns>
        public static IEnumerable<AttributeValue> SearchForValue(IEnumerable<AttributeValue> startingValues,
                                                                 string value, bool fullText)
        {
            AttributeValueSearcher valueSearcher = new AttributeValueSearcher(fullText, value);
            return Search(startingValues, valueSearcher);
        }

        /// <summary>
        /// Searches for AttributeValues that the satisfy the given condition.
        /// </summary>
        /// <param name="startingValue">The starting point of the search.</param>
        /// <param name="condition">The condition that must be satified.</param>
        /// <returns></returns>
        public static IEnumerable<AttributeValue> Search(AttributeValue startingValue,
                                                         IAttributeMatchCondition condition)
        {
            AttributeSearcher searcher = new AttributeSearcher(condition);
            AttributeIterator.DoForAll(startingValue, searcher);
            return searcher.Results;
        }

        /// <summary>
        /// Searches for AttributeValues that the satisfy the given condition.
        /// </summary>
        /// <param name="startingValues">The starting point of the search.</param>
        /// <param name="condition">The condition that must be satified.</param>
        /// <returns></returns>
        public static IEnumerable<AttributeValue> Search(IEnumerable<AttributeValue> startingValues,
                                                         IAttributeMatchCondition condition)
        {
            AttributeSearcher searcher = new AttributeSearcher(condition);
            AttributeIterator.DoForAll(startingValues, searcher);
            return searcher.Results;
        }
    }

    internal class AttributeKeySearcher : IAttributeMatchCondition
    {
        private readonly bool m_bFullText;
        private readonly string m_sSearchKey;

        public AttributeKeySearcher(bool fullText, string searchKey)
        {
            m_bFullText = fullText;
            m_sSearchKey = searchKey;
        }

        #region IAttributeMatchCondition Members

        public bool SatisfiesCondition(AttributeValue attribute)
        {
            return attribute.Key == m_sSearchKey || (m_bFullText && attribute.Key.Contains(m_sSearchKey));
        }

        #endregion
    }

    internal class AttributeValueSearcher : IAttributeMatchCondition
    {
        private readonly bool m_bFullText;
        private readonly string m_sSearchValue;

        public AttributeValueSearcher(bool fullText, string searchValue)
        {
            m_bFullText = fullText;
            m_sSearchValue = searchValue;
        }

        #region IAttributeMatchCondition Members

        public bool SatisfiesCondition(AttributeValue attribute)
        {
            if (attribute.DataType == AttributeValueType.String)
            {
                string value = attribute.Data as string;
                return value == m_sSearchValue || (m_bFullText && value.Contains(m_sSearchValue));
            }
            return false;
        }

        #endregion
    }

    internal class AttributeSearcher : IAttributeProcessor
    {
        private readonly IAttributeMatchCondition m_attributeMatchCondition;
        private readonly ICollection<AttributeValue> m_results;

        public AttributeSearcher(IAttributeMatchCondition attributeMatchCondition)
        {
            m_attributeMatchCondition = attributeMatchCondition;
            m_results = new HashSet<AttributeValue>();
        }

        public ICollection<AttributeValue> Results
        {
            get { return m_results; }
        }

        #region IAttributeProcessor Members

        public void Process(AttributeValue data)
        {
            if (m_attributeMatchCondition.SatisfiesCondition(data))
                m_results.Add(data);
        }

        #endregion
    }
}