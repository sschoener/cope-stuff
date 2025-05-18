#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// Enumerator which repeats the enumeration of elements from a given IEnumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class RepeatingEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerable<T> m_values;
        private IEnumerator<T> m_currentState;

        public RepeatingEnumerator(IEnumerable<T> t)
        {
            m_values = t;
        }

        #region IEnumerator<T> Members

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (m_currentState == null || !m_currentState.MoveNext())
                m_currentState = m_values.GetEnumerator();
            Current = m_currentState.Current;
            return true;
        }

        public void Reset()
        {
            m_currentState = null;
            Current = default(T);
        }

        public T Current { get; protected set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }
}