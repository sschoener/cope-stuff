namespace cope
{
    public abstract class Either<T1, T2>
    {
        public abstract bool IsLeft {get;}
        public abstract bool IsRight {get;}

        public abstract EitherLeft<T1, T2> Left { get; }
        public abstract EitherRight<T1, T2> Right { get; }

        // TODO: Add proper projections.
    }
}
