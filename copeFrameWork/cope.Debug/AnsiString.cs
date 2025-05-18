using System.Runtime.InteropServices;

namespace cope.Debug
{
    /// <summary>
    /// http://www.codeproject.com/KB/winsdk/NTNativeAPIWrapperLibrary.aspx
    /// </summary>
    internal struct AnsiString
    {
        public AnsiString(string str)
        {
            ByteLength = (ushort)(str.Length * sizeof(char));
            MaximumLength = ByteLength;
            String = str;
        }

        public readonly ushort ByteLength;
        public readonly ushort MaximumLength;
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string String;
    }
}