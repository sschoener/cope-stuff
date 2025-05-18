using System.Collections;
using System.Collections.Generic;

namespace cope
{
    /// <summary>
    /// Enumerator used by Slices over arrays.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    sealed class SliceArrayEnumerator<T> : IEnumerator<T>
    {
        private int m_currentIdx;
        private readonly int m_startIdx;
        private readonly int m_endIdx;
        private readonly IList<T> m_indexedList;

        public SliceArrayEnumerator(IList<T> indexedList, int startIndex, int length)
        {
            m_indexedList = indexedList;
            m_startIdx = startIndex;
            if (startIndex + length > indexedList.Count)
            {
                m_endIdx = indexedList.Count;
            }
            else
            {
                m_endIdx = startIndex + length;
            }
            m_currentIdx = m_startIdx - 1;
        }

        public T Current
        {
            get { return m_indexedList[m_currentIdx]; }
        }

        public bool MoveNext()
        {
            m_currentIdx++;
            return m_currentIdx < m_endIdx;
        }

        public void Reset()
        {
            m_currentIdx = m_startIdx - 1;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
        }
    }
}