#region

using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// Implements pooling for arrays.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayPool<T>
    {
        private readonly int m_iSizeOfArrays;
        private readonly Stack<T[]> m_pool;

        public ArrayPool(int sizeOfArrays, int startSize)
        {
            m_iSizeOfArrays = sizeOfArrays;
            m_pool = new Stack<T[]>(startSize);
        }

        public ArrayPool(int sizeOfArrays, IEnumerable<T[]> startData)
        {
            m_iSizeOfArrays = sizeOfArrays;
            m_pool = new Stack<T[]>(startData);
        }

        public int SizeOfArrays
        {
            get { return m_iSizeOfArrays; }
        }

        public int Count
        {
            get { return m_pool.Count; }
        }

        public void EnsureMinAmountOfElements(int minAmount)
        {
            int diff = minAmount - m_pool.Count;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                    m_pool.Push(new T[m_iSizeOfArrays]);
            }
        }

        public virtual T[] GetOne()
        {
            if (m_pool.Count > 0)
                return m_pool.Pop();
            else
                return new T[m_iSizeOfArrays];
        }

        public virtual bool Recycle(T[] t)
        {
            if (t.Length == m_iSizeOfArrays)
            {
                m_pool.Push(t);
                return true;
            }
            else
                return false;
        }

        public void ReduceTo(int totalElements)
        {
            int difference = m_pool.Count - totalElements;
            if (difference > 0)
            {
                for (int i = 0; i < difference; i++)
                    m_pool.Pop();
            }
        }

        public void Clear()
        {
            m_pool.Clear();
        }
    }
}