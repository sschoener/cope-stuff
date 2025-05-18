#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// Enumerator which uses a generator function which takes a state, modifies it and returns an output.
    /// In each step of the enumeration the generator function will be called with the state produced by the previous
    /// application of the generator function. The first state is passed as an argument to the ctor.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    internal class GeneratorEnumerator<TState, TOut> : IEnumerator<TOut>
    {
        protected TState m_currentState;
        protected GeneratorFunc<TState, TOut> m_generator;

        public GeneratorEnumerator(GeneratorFunc<TState, TOut> generator, TState initialState)
        {
            m_currentState = initialState;
            m_generator = generator;
        }

        #region IEnumerator<TOut> Members

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            Current = m_generator(ref m_currentState);
            return true;
        }

        /// <exception cref="InvalidOperationException">Not available for objects of type GeneratorEnumerator.</exception>
        public void Reset()
        {
            throw new InvalidOperationException("Not available for objects of type GeneratorEnumerator.");
        }

        public TOut Current { get; protected set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }
}