#region

using System.Collections;
using System.Collections.Generic;
using System.Text;

#endregion

namespace cope
{
    /// <summary>
    /// Represents a circular array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CircularArray<T> : IEnumerable<T>, IGenericClonable<CircularArray<T>>
    {
        #region fields

        private readonly T[] m_internalArray;
        private int m_iIterationIndex;

        #endregion fields

        #region ctors

        /// <summary>
        /// Creates a new CircularArray of the specified size.
        /// </summary>
        /// <param name="size">Size of the array.</param>
        public CircularArray(int size)
        {
            m_internalArray = new T[size];
        }

        /// <summary>
        /// Creates a new CircularArray based on the specified array. The specified array will NOT be copied.
        /// </summary>
        /// <param name="array"></param>
        public CircularArray(T[] array)
        {
            m_internalArray = array;
        }

        #endregion ctors

        #region methods

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>) m_internalArray.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_internalArray.GetEnumerator();
        }

        #endregion

        #region IGenericClonable<CircularArray<T>> Members

        public CircularArray<T> GClone()
        {
            var copy = (T[]) m_internalArray.Clone();
            return new CircularArray<T>(copy);
        }

        #endregion

        public override string ToString()
        {
            int arrayLength = m_internalArray.Length;
            var b = new StringBuilder(arrayLength * 10);
            for (int i = 0; i < arrayLength; i++)
            {
                b.Append('[');
                b.Append(i);
                b.Append("] = ");
                b.Append(m_internalArray[i].ToString());
            }
            return b.ToString();
        }

        /*public override int GetHashCode()
        {
            return m_internalArray.Aggregate(0, (current, t) => current ^ t.GetHashCode());
        }*/

        /// <summary>
        /// Returns the next element from the CircularArray.
        /// </summary>
        /// <returns></returns>
        public T GetNext()
        {
            if (m_internalArray.Length == 0)
                return default(T);
            if (m_iIterationIndex >= m_internalArray.Length)
                m_iIterationIndex = 0;
            return m_internalArray[m_iIterationIndex];
        }

        /// <summary>
        /// Sets the current iteration index used by GetNext().
        /// </summary>
        /// <param name="index"></param>
        public void SetIterationIndex(int index)
        {
            if (index >= m_internalArray.Length)
                m_iIterationIndex = m_iIterationIndex % m_internalArray.Length;
            else
                m_iIterationIndex = index;
        }

        /// <summary>
        /// Returns 'length' values starting from index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public T[] GetValues(int index, int length)
        {
            int startIndex = index;
            int arrayLength = m_internalArray.Length;
            T[] returnValues = new T[length];
            // return an empty array if appropriate
            if (length == 0 || arrayLength == 0)
                return returnValues;

            // map the starting index to the length of the internal array
            if (startIndex >= arrayLength)
                startIndex %= arrayLength;
            for (int i = 0; i < length; i++)
            {
                if (startIndex >= arrayLength)
                    startIndex = 0;
                returnValues[length] = m_internalArray[startIndex++];
            }
            return returnValues;
        }

        /// <summary>
        /// Copies 'length' values from this array to 'target' starting at 'index'.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="length"></param>
        /// <param name="index"></param>
        public void CopyTo(T[] target, int length, int index = 0)
        {
            if (length >= target.Length)
                length = target.Length - 1;
            for (int j = 0; j < length; j++)
            {
                if (index >= m_internalArray.Length)
                    index = m_internalArray.Length - 1;
                target[j] = m_internalArray[index++];
            }
        }

        #endregion methods

        #region properties

        /// <summary>
        /// Gets the length of the circular buffer.
        /// </summary>
        public int Length
        {
            get { return m_internalArray.Length; }
        }

        /// <summary>
        /// Gets or sets the value at the specified index modulo array length.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index >= m_internalArray.Length)
                    index %= m_internalArray.Length;
                return m_internalArray[index];
            }
            set
            {
                if (index >= m_internalArray.Length)
                    index %= m_internalArray.Length;
                m_internalArray[index] = value;
            }
        }

        #endregion properties
    }
}