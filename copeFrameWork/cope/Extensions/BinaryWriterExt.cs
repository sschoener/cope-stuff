#region

using System.IO;

#endregion

namespace cope.Extensions
{
    public static class BinaryWriterExt
    {
        public static int WriteCString(this BinaryWriter bw, string str)
        {
            bw.Write(str.ToCharArray());
            bw.Write(false);
            return str.Length + 1;
        }
    }
}