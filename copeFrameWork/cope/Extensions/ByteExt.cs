#region

using System.Text;

#endregion

namespace cope.Extensions
{
    public static class ByteExt
    {
        /// <summary>
        /// Reverses the bytes in a byte array.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="returnCopy">Set to true to receive a copy of the array and leave the original array unmodified.</param>
        /// <param name="index">Index of the first element in the array to reverse.</param>
        /// <param name="length">Number of bytes to reverse.</param>
        /// <returns></returns>
        public static byte[] ReverseBytes(this byte[] b1, bool returnCopy, int index, int length)
        {
            var tmp = new byte[length];
            int end = index + length;
            for (int i = index; i < end; i++)
            {
                tmp[i - index] = b1[i];
            }
            int j = 0;
            for (int i = end - 1; i >= index; i--, j++)
            {
                if (!returnCopy)
                    b1[j] = tmp[i];
                else
                    tmp[j] = b1[i];
            }
            if (returnCopy)
            {
                return tmp;
            }
            return b1;
        }

        /// <summary>
        /// Reverses the bytes in a byte array.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="returnCopy">Set to true to receive a copy of the array and leave the original array unmodified.</param>
        /// <returns></returns>
        public static byte[] ReverseBytes(this byte[] b1, bool returnCopy)
        {
            return b1.ReverseBytes(returnCopy, 0, b1.Length);
        }

        /// <summary>
        /// Reverses the bytes in a byte array.
        /// </summary>
        /// <param name="b1"></param>
        /// <returns></returns>
        public static byte[] ReverseBytes(this byte[] b1)
        {
            return b1.ReverseBytes(false);
        }

        /// <summary>
        /// Converts this byte array to a string containing its hexadecimal representation.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="addSpaces">Set to false to disable spaces between the single bytes.</param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes, bool addSpaces = true)
        {
            if (bytes.Length == 0)
                return string.Empty;
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
                if (addSpaces)
                    sb.Append(' ');
            }
            if (addSpaces)
                return sb.ToString(0, sb.Length - 1);
            return sb.ToString();
        }
    }
}