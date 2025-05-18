#region

using System;

#endregion

namespace cope
{
    public class CopeException : Exception
    {
        public CopeException()
        {
        }

        public CopeException(string msg)
            : base(msg)
        {
        }

        public CopeException(string msg, params object[] args)
            : base(string.Format(msg, args))
        {
        }

        public CopeException(Exception innerException, string msg, params object[] args)
            : base(string.Format(msg, args), innerException)
        {
        }
    }
}