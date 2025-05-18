#region

using System;
using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// Implements a heap which will only work for types which implement the generic IComparable.
    /// If you want to use a custom IComparer, use CustomHeap.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Heap<T> : DelegateHeap<T> where T : IComparable<T>
    {
        public Heap(IEnumerable<T> t) : base(t, (t1, t2) => t1.CompareTo(t2))
        {
        }

        public Heap(Heap<T> heap) : base(heap)
        {
        }
    }
}