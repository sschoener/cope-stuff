#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// Enumerator which constantly returns the same element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ConstantEnumerator<T> : IEnumerator<T>
    {
        private readonly T m_value;

        public ConstantEnumerator(T t)
        {
            m_value = t;
        }

        #region IEnumerator<T> Members

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            Current = m_value;
            return true;
        }

        public void Reset()
        {
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