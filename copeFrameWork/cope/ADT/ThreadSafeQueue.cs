#region

using System.Threading;

#endregion

namespace cope.ADT
{
    /*
     * Allows multiple threads to de-/enqueue items in O(1)
     */

    public class ThreadSafeQueue<T>
    {
        #region fields

        private readonly EventWaitHandle m_headValid;
        private readonly object m_thisLock = new object();
        private Container<T> m_first;
        private Container<T> m_last;
        private Container<T> m_rest;

        #endregion fields

        #region ctor

        public ThreadSafeQueue()
        {
            m_headValid = new AutoResetEvent(false);
        }

        #endregion ctor

        #region methods

        public void Enqueue(T t)
        {
            lock (m_thisLock)
            {
                if (m_first == null)
                {
                    m_first = new Container<T>(t);
                    m_headValid.Set();
                    return;
                }
                if (m_rest == null)
                {
                    m_rest = new Container<T>(t);
                    m_last = m_rest;
                }
                else
                {
                    m_last.Child = new Container<T>(t);
                    m_last = m_last.Child;
                }
            }
        }

        public T Dequeue()
        {
            lock (m_thisLock)
            {
                Container<T> retval = m_first;
                m_first = m_rest;
                if (m_rest != null)
                    m_rest = m_rest.Child;
                if (m_first == m_last)
                    m_last = null;
                return retval.Data;
            }
        }

        public void Clear()
        {
            lock (m_thisLock)
            {
                m_first = null;
                m_rest = null;
                m_last = null;
            }
        }

        #endregion methods

        #region properties

        public bool IsEmpty
        {
            get
            {
                lock (m_thisLock)
                {
                    return m_first == null;
                }
            }
        }

        public EventWaitHandle HeadValidEvent
        {
            get { return m_headValid; }
        }

        #endregion properties

        #region Nested type: Container

        private class Container<TK>
        {
            public readonly TK Data;
            public Container<TK> Child;

            public Container(TK t)
            {
                Data = t;
            }
        }

        #endregion
    }
}