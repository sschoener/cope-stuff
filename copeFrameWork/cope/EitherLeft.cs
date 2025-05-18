using System;

namespace cope
{
    public sealed class EitherLeft<T1, T2> : Either<T1, T2>
    {
        #region Overrides of Either<T1,T2>

        public override bool IsLeft
        {
            get { return true; }
        }

        public override bool IsRight
        {
            get { return false; }
        }

        public override EitherLeft<T1, T2> Left
        {
            get { return this; }
        }

        /// <exception cref="InvalidOperationException">This is an Either-Left instance!</exception>
        public override EitherRight<T1, T2> Right
        {
            get { throw new InvalidOperationException("This is an Either-Left instance!"); }
        }

        #endregion

        public EitherLeft(T1 value)
        {
            Value = value;
        }

        public T1 Value { get; private set; }
    }
}