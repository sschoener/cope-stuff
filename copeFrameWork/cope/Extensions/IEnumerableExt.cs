#region

using System;
using System.Collections.Generic;

#endregion

namespace cope.Extensions
{
    public static class IEnumerableExt
    {
        /// <summary>
        /// Invokes the specified action on each element of the IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c>.</exception>
        public static void ForEach<T>(this IEnumerable<T> ienum, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            foreach (T t in ienum)
                action(t);
        }

        /// <summary>
        /// Invokes the specified action on each element of the IEnumerable. The action also
        /// gets the elements index within the IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c>.</exception>
        public static void ForEach<T>(this IEnumerable<T> ienum, Action<T, int> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            int idx = 0;
            foreach (T t in ienum)
            {
                action(t, idx);
                idx++;
            }
        }

        /// <summary>
        /// Invokes the specified action on each element of the IEnumerable. The iteration
        /// ends as soon as the action returns 'false'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c>.</exception>
        public static void ForEach<T>(this IEnumerable<T> ienum, Func<T, bool> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            foreach (T t in ienum)
            {
                if (action(t))
                    return;
            }
        }

        /// <summary>
        /// Invokes the specified action on each element of the IEnumerable. The action also
        /// gets the index of the element in the IEnumerable as a parameter. The iteration
        /// ends as soon as the action returns 'false'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c>.</exception>
        public static void ForEach<T>(this IEnumerable<T> ienum, Func<T, int, bool> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            int idx = 0;
            foreach (T t in ienum)
            {
                if (!action(t, idx))
                    return;
                idx++;
            }
        }

        /// <summary>
        /// Returns the index of the first item satisfying the given predicate or -1 if there is no such item in the IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="predicate">The predicate to check the items against.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate" /> is <c>null</c>.</exception>
        public static int IndexOf<T>(this IEnumerable<T> ienum, Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            int i = 0;
            foreach (T t in ienum)
            {
                if (predicate(t))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the first occurrence of a specified object in the IEnumerable.
        /// Equality is checked using the default equality comparer.
        /// Should the IEnumerable not contain the specified object, this method returns -1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> ienum, T obj)
        {
            int i = 0;
            foreach (T t in ienum)
            {
                if (t.Equals(obj))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the first occurrence of a specified object in the IEnumerable.
        /// Equality is checked using the specified function.
        /// Should the IEnumerable not contain the specified object, this method returns -1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="equalityComparer"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="equalityComparer" /> is <c>null</c>.</exception>
        public static int IndexOf<T>(this IEnumerable<T> ienum, Func<T, T, bool> equalityComparer, T obj)
        {
            if (equalityComparer == null) throw new ArgumentNullException("equalityComparer");
            int i = 0;
            foreach (T t in ienum)
            {
                if (equalityComparer(t, obj))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the last occurrence of a specified object in the IEnumerable.
        /// Equality is checked using the default equality comparer.
        /// Should the IEnumerable not contain the specified object, this method returns -1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int LastIndexOf<T>(this IEnumerable<T> ienum, T obj)
        {
            int i = 0;
            int idx = -1;
            foreach (T t in ienum)
            {
                if (t.Equals(obj))
                    idx = i;
                i++;
            }
            return idx;
        }

        /// <summary>
        /// Returns the index of the last occurrence of a specified object in the IEnumerable.
        /// Equality is checked using the specified function.
        /// Should the IEnumerable not contain the specified object, this method returns -1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="equalityComparer"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="equalityComparer" /> is <c>null</c>.</exception>
        public static int LastIndexOf<T>(this IEnumerable<T> ienum, Func<T, T, bool> equalityComparer, T obj)
        {
            if (equalityComparer == null) throw new ArgumentNullException("equalityComparer");
            int i = 0;
            int idx = -1;
            foreach (T t in ienum)
            {
                if (equalityComparer(t, obj))
                    idx = i;
                i++;
            }
            return idx;
        }

        /// <summary>
        /// Returns the index of the last element in an IEnumerable that satisfies a certain predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="predicate">The predicate to check items against.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate" /> is <c>null</c>.</exception>
        public static int LastIndex<T>(this IEnumerable<T> ienum, Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            int i = 0;
            int idx = -1;
            foreach (T t in ienum)
            {
                if (predicate(t))
                    idx = i;
                i++;
            }
            return idx;
        }

        /// <summary>
        /// Splits this IEnumeration into two halfs using the selector function.
        /// Items for which the selector function returns true are added to the first ICollection,
        /// all the others are added to the second ICollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum"></param>
        /// <param name="selector">This function determines which collection an item belongs to.</param>
        /// <param name="t1">All items for which the selector returns true are added to this collection.</param>
        /// <param name="t2">All items for which the selector returns false are added to this collection.</param>
        /// <exception cref="ArgumentNullException"><paramref name="selector" /> is <c>null</c>.</exception>
        public static void Split<T>(this IEnumerable<T> ienum, Func<T, bool> selector, ref ICollection<T> t1,
                                    ref ICollection<T> t2)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (t1 == null) throw new ArgumentNullException("t1");
            if (t2 == null) throw new ArgumentNullException("t2");
            foreach (T t in ienum)
                if (selector(t))
                    t1.Add(t);
                else
                    t2.Add(t);
        }
    }
}