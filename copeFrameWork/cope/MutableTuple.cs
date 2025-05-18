#region

using System;

#endregion

namespace cope
{
    /// <summary>
    /// Represents a tuple with mutable members, thus probably not suitable as a key into hashtables.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public sealed class MutableTuple<T1, T2>
    {
        private T1 m_item1;
        private T2 m_item2;

        /// <summary>
        /// Constructs a new mutable tuple from the two given values.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        public MutableTuple(T1 item1, T2 item2)
        {
            m_item1 = item1;
            m_item2 = item2;
        }

        /// <summary>
        /// Gets or sets the first item of the tuple.
        /// </summary>
        public T1 Item1
        {
            get { return m_item1; }
            set
            {
                T1 old = m_item1;
                m_item1 = value;
                if (OnItem1Changed != null)
                    OnItem1Changed(this, new ValueChangedEventArgs<T1>(old, value));
            }
        }

        /// <summary>
        /// Gets or sets the second item of the tuple.
        /// </summary>
        public T2 Item2
        {
            get { return m_item2; }
            set
            {
                T2 old = m_item2;
                m_item2 = value;
                if (OnItem2Changed != null)
                    OnItem2Changed(this, new ValueChangedEventArgs<T2>(old, value));
            }
        }

        /// <summary>
        /// This event fires when the first item has been changed.
        /// </summary>
        public event EventHandler<ValueChangedEventArgs<T1>> OnItem1Changed;

        /// <summary>
        /// This event fires when the second item has been changed.
        /// </summary>
        public event EventHandler<ValueChangedEventArgs<T2>> OnItem2Changed;

        public override int GetHashCode()
        {
            return Item1.GetHashCode() ^ Item2.GetHashCode();
        }

        public override string ToString()
        {
            return "Item 1: " + Item1 + ", Item 2: " + Item2;
        }
    }
}