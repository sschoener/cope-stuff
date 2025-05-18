#region

using System;
using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// Static helper class which offers sorting of IEnumerables using HeapSort.
    /// </summary>
    public static class HeapSort
    {
        /// <summary>
        /// Sorts items using the specified function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static T[] Sort<T>(IEnumerable<T> items, Func<T, T, int> compare)
        {
            return SortHeap(new DelegateHeap<T>(items, compare));
        }

        /// <summary>
        /// Sorts items using the specified IComparer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static T[] Sort<T>(IEnumerable<T> items, IComparer<T> compare)
        {
            return SortHeap(new CustomHeap<T>(items, compare));
        }

        /// <summary>
        /// Sorts items which implement the generic IComparable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T[] Sort<T>(IEnumerable<T> items) where T : IComparable<T>
        {
            return SortHeap(new Heap<T>(items));
        }

        /// <summary>
        /// Sorts values from a heap. The heap will be destroyed by this operation!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="heap"></param>
        /// <returns></returns>
        private static T[] SortHeap<T>(DelegateHeap<T> heap)
        {
            var ret = new T[heap.Count];
            int counter = heap.Count - 1;
            while (heap.Count > 0)
            {
                ret[counter--] = heap.GetHead();
                heap.RemoveHead();
            }
            return ret;
        }
    }
}