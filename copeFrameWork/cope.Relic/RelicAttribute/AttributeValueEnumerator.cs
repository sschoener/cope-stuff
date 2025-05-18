#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Relic.RelicAttribute
{
    /// <summary>
    /// Enumerator for AttributeValues. Will traverse whole trees of AttributeValues,
    /// but as always with enumerators: Don't change the Enumeration while enumerating!
    /// This implementation follows the principles of DepthFirstSearching.
    /// </summary>
    internal class AttributeValueEnumerator : IEnumerator<AttributeValue>
    {
        private readonly Stack<IEnumerator<AttributeValue>> m_parentStack;
        private readonly AttributeValue m_startValue;

        public AttributeValueEnumerator(AttributeValue value)
        {
            m_startValue = value;
            m_parentStack = new Stack<IEnumerator<AttributeValue>>();
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }

        #endregion

        #region Implementation of IEnumerator

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public bool MoveNext()
        {
            // iteration begin
            if (Current == null)
            {
                Current = m_startValue;
                return true;
            }

            // is it something else than table?
            if (ShouldReturnToParent())
            {
                // try to go to our parent
                while (m_parentStack.Count > 0)
                {
                    // parent available, get it from the stack
                    var parent = m_parentStack.Peek();

                    // does the parent still have any elements left?
                    if (parent.MoveNext())
                    {
                        // yes, it has, return the next element from the parent
                        Current = parent.Current;
                        return true;
                    }
                    // no, it doesn't have any items left
                    m_parentStack.Pop();
                }
                // no parent available!
                return false;
            }

            var table = Current.Data as AttributeTable;
            var iter = table.GetEnumerator();
            m_parentStack.Push(iter);
            iter.MoveNext();
            Current = iter.Current;
            return true;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public void Reset()
        {
            Current = null;
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>
        /// The element in the collection at the current position of the enumerator.
        /// </returns>
        public AttributeValue Current { get; private set; }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>
        /// The current element in the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception><filterpriority>2</filterpriority>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        private bool ShouldReturnToParent()
        {
            if (Current.DataType != AttributeValueType.Table)
                return true;
            var table = Current.Data as AttributeTable;
            if (table.ChildCount == 0)
                return true;
            return false;
        }

        #endregion
    }
}