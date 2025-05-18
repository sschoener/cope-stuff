#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// An Enumerable type which keeps cycling through and returning values from another Enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepeatingStream<T> : IEnumerable<T>
    {
        /// <summary>
        /// Constructs a new InfiniteRepeatingStream which will return the items from the given IEnumerable.
        /// </summary>
        /// <param name="t"></param>
        public RepeatingStream(IEnumerable<T> t)
        {
            Values = t;
        }

        /// <summary>
        /// Get the values that will be returned.
        /// </summary>
        public IEnumerable<T> Values { get; private set; }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new RepeatingEnumerator<T>(Values);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}