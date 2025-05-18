#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope
{
    public delegate TOut GeneratorFunc<TState, out TOut>(ref TState state);

    /// <summary>
    /// A GeneratorStream takes a generator function which modifies a state and returns an output value.
    /// This process is wrapped as an IEnumerable so that the Generator is called with the updated state in every step.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class GeneratorStream<TState, TOut> : IEnumerable<TOut>
    {
        protected TState m_initialState;

        /// <summary>
        /// Creates a new GeneratorStream using the given generator and initial state.
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="initialState"></param>
        /// <exception cref="ArgumentNullException"><paramref name="generator" /> is <c>null</c>.</exception>
        public GeneratorStream(GeneratorFunc<TState, TOut> generator, TState initialState)
        {
            if (generator == null) throw new ArgumentNullException("generator");
            Generator = generator;
            m_initialState = initialState;
        }

        /// <summary>
        /// Gets the generator function.
        /// </summary>
        public GeneratorFunc<TState, TOut> Generator { get; protected set; }

        #region IEnumerable<TOut> Members

        public IEnumerator<TOut> GetEnumerator()
        {
            return new GeneratorEnumerator<TState, TOut>(Generator, m_initialState);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}