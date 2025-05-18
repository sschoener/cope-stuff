#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace cope.Extensions
{
    public static class ComparableExt
    {
        /// <summary>
        /// Returns true if this array contains anything identical to o2.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool ContainsComparable<T>(this IEnumerable<T> o1, T o2) where T : IComparable<T>
        {
            return o1.Any(t => t.CompareTo(t) == 0);
        }

        /// <summary>
        /// Returns whether the two arrays have equal contents by using the CompareTo method.
        /// </summary>
        /// <param name="ic1"></param>
        /// <param name="ic2"></param>
        /// <returns></returns>
        public static bool IsComparable<T>(this T[] ic1, T[] ic2) where T : IComparable<T>
        {
            if (ic1.Length != ic2.Length)
                return false;
            return !ic1.Where((t, i) => t.CompareTo(ic2[i]) != 0).Any();
        }

        /// <summary>
        /// Removes all occurences of o2 in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static T[] RemoveComparable<T>(this T[] o1, T o2) where T : IComparable<T>
        {
            var tmp = new T[o1.Length - o1.CountComparable(o2)];
            int index = 0;
            foreach (T t in o1)
            {
                if (t.CompareTo(o2) != 0)
                    tmp[index++] = t;
            }
            return tmp;
        }

        /// <summary>
        /// Counts how often o2 appears in this IEnumerable using the CompareTo method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static int CountComparable<T>(this IEnumerable<T> o1, T o2) where T : IComparable<T>
        {
            return o1.Count(p => o2.CompareTo(p) == 0);
        }

        /// <summary>
        /// Returns the index of the first occurence of anything comparably-equal to t2 or -1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static int IndexOfComparable<T>(this T[] t1, T t2) where T : IComparable<T>
        {
            for (int i = 0; i < t1.Length; i++)
                if (t1[i].CompareTo(t2) == 0)
                    return i;
            return -1;
        }
    }
}