using System;

namespace cope.Relic
{
    class RelicException : Exception
    {
        public RelicException()
        {
        }

        public RelicException(string msg, params object[] args)
            : base(string.Format(msg, args))
        {
        }

        public RelicException(Exception innerException, string msg, params object[] args)
            : base(string.Format(msg, args), innerException)
        {
        }
    }
}
