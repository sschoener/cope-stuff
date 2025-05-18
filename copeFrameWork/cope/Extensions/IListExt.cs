using System.Collections.Generic;

namespace cope.Extensions
{
    /// <summary>
    /// Static class offering extensions for ILists, indexed lists.
    /// </summary>
    public static class IListExt
    {
        /// <summary>
        /// Gets a slice of an indexed list (IList) given a start index and an end index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexedList"></param>
        /// <param name="start">The start index of the slice.</param>
        /// <param name="end">The end index of the slice.</param>
        /// <returns></returns>
        public static Slice<T> Slice<T>(this IList<T> indexedList, int start, int end)
        {
            return new Slice<T>(indexedList, start, end - start);
        }

        /// <summary>
        /// Gets a slice of an indexed list (IList) given a start index, running all to the end of the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexedList"></param>
        /// <param name="start">The start index of the slice.</param>
        /// <returns></returns>
        public static Slice<T> Slice<T>(this IList<T> indexedList, int start)
        {
            return new Slice<T>(indexedList, start, indexedList.Count - start);
        }

        /// <summary>
        /// Gets a slice of an indexed list (IList) given a start index and a length.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexedList"></param>
        /// <param name="start">The start index of the slice.</param>
        /// <param name="length">The length of the slice.</param>
        /// <returns></returns>
        public static Slice<T> SliceLength<T>(this IList<T> indexedList, int start, int length)
        {
            return new Slice<T>(indexedList, start, length);
        }
    }
}