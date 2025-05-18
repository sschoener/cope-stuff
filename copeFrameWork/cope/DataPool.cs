#region

using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// Implements pooling for data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataPool<T>
    {
        private readonly Stack<T> m_pool;

        public DataPool(IEnumerable<T> startData)
        {
            m_pool = new Stack<T>(startData);
        }

        public DataPool(int startSize)
        {
            m_pool = new Stack<T>(startSize);
        }

        public virtual T GetOne()
        {
            if (m_pool.Count > 0)
                return m_pool.Pop();
            return default(T);
        }

        public virtual void Recycle(T t)
        {
            m_pool.Push(t);
        }

        public void Clear()
        {
            m_pool.Clear();
        }
    }
}