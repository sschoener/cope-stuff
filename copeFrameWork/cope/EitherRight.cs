using System;

namespace cope
{
    public sealed class EitherRight<T1, T2> : Either<T1, T2>
    {
        #region Overrides of Either<T1,T2>

        public override bool IsLeft
        {
            get { return false; }
        }

        public override bool IsRight
        {
            get { return true; }
        }

        /// <exception cref="InvalidOperationException">This is an Either-Right instance!</exception>
        public override EitherLeft<T1, T2> Left
        {
            get { throw new InvalidOperationException("This is an Either-Right instance!"); }
        }

        public override EitherRight<T1, T2> Right
        {
            get { return this; }
        }

        #endregion

        public EitherRight(T2 value)
        {
            Value = value;
        }

        public T2 Value { get; private set; }
    }
}