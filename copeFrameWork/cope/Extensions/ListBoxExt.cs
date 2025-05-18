#region

using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace cope.Extensions
{
    public static class ListBoxExt
    {
        /// <summary>
        /// Adds each element from the specified IEnumerable to the ObjectCollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="range"></param>
        public static void AddRange<T>(this ListBox.ObjectCollection coll, IEnumerable<T> range)
        {
            foreach (T t in range)
                coll.Add(t);
        }
    }
}