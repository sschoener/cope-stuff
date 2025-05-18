using System.Collections;
using System.Collections.Generic;

namespace cope
{
    /// <summary>
    /// Enumerator used by Slices in general.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    sealed class SliceEnumerator<T> : IEnumerator<T>
    {
        private int m_currentIdx;
        private readonly int m_startIdx;
        private readonly int m_length;
        private readonly IList<T> m_indexedList;
        private IEnumerator<T> m_enum;
        private bool m_isEmpty;

        public SliceEnumerator(IList<T> indexedList, int startIndex, int length)
        {
            m_indexedList = indexedList;
            m_startIdx = startIndex;
            m_length = length;
            Reset();
        }

        public T Current
        {
            get { return m_enum.Current; }
        }

        public bool MoveNext()
        {
            if (m_isEmpty)
                return false;
            m_currentIdx++;
            if (m_currentIdx >= m_length)
                return false;
            return m_enum.MoveNext();
        }

        public void Reset()
        {
            m_enum = m_indexedList.GetEnumerator();
            int idx = 0;
            while(idx < m_startIdx - 1)
            {
                if (!m_enum.MoveNext())
                {
                    m_isEmpty = true;
                    return;
                }
                idx++;
            }
            m_currentIdx = 0;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            m_enum.Dispose();
        }
    }
}