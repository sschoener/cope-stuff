#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// An Enumerable type which always returns the same value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConstantStream<T> : IEnumerable<T>
    {
        /// <summary>
        /// Constructs a new ConstantStream repeating the given value.
        /// </summary>
        /// <param name="t"></param>
        public ConstantStream(T t)
        {
            Value = t;
        }

        /// <summary>
        /// Gets the value that will be repeated ad infinitum.
        /// </summary>
        public T Value { get; private set; }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new ConstantEnumerator<T>(Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}