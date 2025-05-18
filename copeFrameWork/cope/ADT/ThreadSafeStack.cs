namespace cope.ADT
{
    public class ThreadSafeStack<T>
    {
        #region fields

        private readonly object m_topLock = new object();
        private Container<T> m_top;

        #endregion fields

        #region ctors

        #endregion ctors

        #region methods

        public void Push(T t)
        {
            lock (m_topLock)
            {
                m_top = new Container<T>(t, m_top);
            }
        }

        public T Pop()
        {
            lock (m_topLock)
            {
                T t = m_top.Data;
                if (m_top != null)
                    m_top = m_top.Child;
                return t;
            }
        }

        public void Clear()
        {
            lock (m_topLock)
            {
                m_top = null;
            }
        }

        #endregion methods

        #region properties

        public bool IsEmpty
        {
            get { return m_top == null; }
        }

        #endregion properties

        #region Nested type: Container

        private class Container<TK>
        {
            public readonly Container<TK> Child;
            public readonly TK Data;

            public Container(TK t, Container<TK> child)
            {
                Data = t;
                Child = child;
            }
        }

        #endregion
    }
}