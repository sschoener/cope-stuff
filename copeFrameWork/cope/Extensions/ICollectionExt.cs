#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace cope.Extensions
{
    public static class ICollectionExt
    {
        /// <summary>
        /// Adds a range of items to this instancen of ICollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="icoll"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this ICollection<T> icoll, IEnumerable<T> items)
        {
            foreach (T t in items)
                icoll.Add(t);
        }

        /// <summary>
        /// Adds a range of items to this instancen of ICollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="icoll"></param>
        /// <param name="items"></param>
        public static void AddMultiple<T>(this ICollection<T> icoll, params T[] items)
        {
            foreach (T t in items)
                icoll.Add(t);
        }

        /// <summary>
        /// Removes all elements matched by the selector from this ICollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="icoll"></param>
        /// <param name="selector"></param>
        /// <exception cref="ArgumentNullException"><paramref name="selector" /> is <c>null</c>.</exception>
        public static void Remove<T>(this ICollection<T> icoll, Func<T, bool> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            var remove = icoll.Where(selector);
            foreach (T t in remove)
                icoll.Remove(t);
        }

        /// <summary>
        /// Removes all elements matched by the selector from this ICollection. The selector also
        /// gets the index of the current element as a parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="icoll"></param>
        /// <param name="selector"></param>
        /// <exception cref="ArgumentNullException"><paramref name="selector" /> is <c>null</c>.</exception>
        public static void Remove<T>(this ICollection<T> icoll, Func<T, int, bool> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            var remove = icoll.Where(selector);
            foreach (T t in remove)
                icoll.Remove(t);
        }
    }
}