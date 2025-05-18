#region

using System;
using System.Collections.Generic;
using System.Linq;
using cope.Extensions;

#endregion

namespace cope
{
    /// <summary>
    /// Implements a heap with a delegate as a comparer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateHeap<T>
    {
        private const int DEFAULT_CAPACITY = 15;
        protected T[] m_array;
        protected Func<T, T, int> m_compare;
        protected int m_count;

        /// <summary>
        /// Constructs a new heap given a delegate implementing the comparison of the items and the initial capacity of the heap.
        /// </summary>
        /// <param name="comparer"></param>
        /// <param name="capacity"></param>
        public DelegateHeap(Func<T, T, int> comparer, int capacity = DEFAULT_CAPACITY)
        {
            m_array = new T[capacity];
            m_compare = comparer;
        }

        /// <summary>
        /// Constructs a new heap from a set of items and a delegate implementing the comparison of the items.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="comparer">Function which implements the comparison of two items of type T.</param>
        public DelegateHeap(IEnumerable<T> t, Func<T, T, int> comparer)
        {
            m_compare = comparer;
            m_array = t.ToArray();
            m_count = m_array.Length;
            BuildHeap();
        }

        /// <summary>
        /// Copy constructor with the ability to override the used comparison.
        /// </summary>
        /// <param name="heap"></param>
        /// <param name="comparer"></param>
        public DelegateHeap(DelegateHeap<T> heap, Func<T, T, int> comparer) : this(heap.Items, comparer)
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="heap"></param>
        public DelegateHeap(DelegateHeap<T> heap)
        {
            m_compare = heap.m_compare;
            m_array = new T[heap.m_array.Length];
            heap.m_array.CopyTo(m_array, 0);
            m_count = heap.m_count;
        }

        /// <summary>
        /// Gets all items in the heap in an unordered fashion.
        /// </summary>
        public IEnumerable<T> Items
        {
            get { return m_array.GetValues(0, m_count); }
        }

        /// <summary>
        /// Gets the number of items in the heap.
        /// </summary>
        public int Count
        {
            get { return m_count; }
        }

        /// <summary>
        /// Returns the head of the heap. If the heap is empty, it will throw an exception.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Heap is empty!</exception>
        public T GetHead()
        {
            if (m_count >= 1)
                return m_array[0];
            throw new Exception("Heap is empty!");
        }

        /// <summary>
        /// Removes the head of the heap.
        /// </summary>
        public void RemoveHead()
        {
            if (m_count == 0)
                return;
            m_array[0] = m_array[--m_count];
            Heapify(0);
        }

        /// <summary>
        /// Inserts a value into the heap.
        /// </summary>
        /// <param name="t"></param>
        public void Insert(T t)
        {
            if (m_count == m_array.Length)
            {
                var newArray = new T[m_array.Length * 2];
                m_array.CopyTo(newArray, 0);
                m_array = newArray;
            }
            int n = m_count++;
            m_array[n] = t;
            while (n > 0 && m_compare(m_array[Parent(n)], (m_array[n])) < 0)
            {
                Swap(n, Parent(n));
                n = Parent(n);
            }
        }

        /// <summary>
        /// Updates the heap by updating one of its members.
        /// This of course only works for reference types as value types are immutable by default.
        /// Returns whether or not the specified value was part of the heap.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(T t)
        {
            int idx = IndexOf(t);
            if (idx < 0)
                return false;

            return true;
        }

        /// <summary>
        /// Takes a predicate and an action and calls the update-action on the first item in the heap that satisfies the prediate.
        /// It will then update the heap accordingly. Returns whether or not any item satisfied the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public bool Update(Func<T, bool> predicate, Action<T> update)
        {
            int idx = IndexOf(predicate);
            if (idx < 0)
                return false;
            update(m_array[idx]);
            Update(idx);
            return true;
        }

        protected void Update(int idx)
        {
            T t = m_array[idx];
            int p = Parent(idx);
            int l = Left(idx);
            int r = Right(idx);
            if (p > 0 && m_compare(t, m_array[p]) > 0)
                HeapifyParent(idx);
            else if (l < m_count && m_compare(t, m_array[l]) < 0)
                Heapify(idx);
            else if (r < m_count && m_compare(t, m_array[r]) < 0)
                Heapify(idx);
        }

        protected int IndexOf(T t)
        {
            for (int i = 0; i < m_count; i++)
            {
                if (m_array[i].Equals(t))
                    return i;
            }
            return -1;
        }

        protected int IndexOf(Func<T, bool> test)
        {
            for (int i = 0; i < m_count; i++)
            {
                if (test(m_array[i]))
                    return i;
            }
            return -1;
        }

        protected void BuildHeap()
        {
            for (int i = m_count / 2 - 1; i >= 0; i--)
                Heapify(i);
        }

        protected void HeapifyParent(int index)
        {
            int p = Parent(index);
            int idx = index;
            while (idx > 0 && m_compare(m_array[p], m_array[idx]) > 0)
            {
                Swap(idx, p);
                idx = p;
                p = Parent(idx);
            }
        }

        protected void Heapify(int index)
        {
            if (index >= m_count)
                return;
            int l = Left(index);
            int r = Right(index);
            int n = index;
            if (l < m_count && m_compare(m_array[l], (m_array[n])) >= 1)
                n = l;
            if (r < m_count && m_compare(m_array[r], (m_array[n])) >= 1)
                n = r;
            if (index != n)
            {
                Swap(index, n);
                Heapify(n);
            }
        }

        protected void Swap(int idx1, int idx2)
        {
            T tmp = m_array[idx1];
            m_array[idx1] = m_array[idx2];
            m_array[idx2] = tmp;
        }

        protected int Left(int index)
        {
            return index * 2 + 1;
        }

        protected int Right(int index)
        {
            return index * 2 + 2;
        }

        protected int Parent(int index)
        {
            return (index - 1) / 2;
        }
    }
}