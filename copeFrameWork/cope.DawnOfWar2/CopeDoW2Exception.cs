#region

using System;

#endregion

namespace cope.DawnOfWar2
{
    public class CopeDoW2Exception : CopeException
    {
        public CopeDoW2Exception()
        {
        }

        public CopeDoW2Exception(string msg, params object[] args)
            : base(msg, args)
        {
        }

        public CopeDoW2Exception(Exception innerException, string msg, params object[] args)
            : base(innerException, msg, args)
        {
        }
    }
}