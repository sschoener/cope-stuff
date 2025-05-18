#region

using System.Collections;
using System.Linq;
using System.Text;

#endregion

namespace cope.Extensions
{
    public static class BitArrayExt
    {
        /// <summary>
        /// Converts this instance of BitArray to its representation as a binary string.
        /// The lowest index-bit is the left-most in the string.
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static string ToBinaryString(this BitArray ba)
        {
            var sb = new StringBuilder(ba.Length);
            for (int i = 0; i < ba.Length; i++)
                sb.Append(ba[i] ? '1' : '0');
            return sb.ToString();
        }

        public static bool[] ToBoolArray(this BitArray ba)
        {
            var b = new bool[ba.Length];
            for (int i = 0; i < ba.Length; i++)
                b[i] = ba[i];
            return b;
        }

        public static int[] ToIntArray(this BitArray ba)
        {
            var b = new int[ba.Length];
            for (int i = 0; i < ba.Length; i++)
                b[i] = ba[i] ? 1 : 0;
            return b;
        }

        public static void FromBinaryString(this BitArray ba, string binary)
        {
            for (int i = 0; i < binary.Length; i++)
                ba[i] = binary[i] != '0';
        }

        public static void FromHexString(this BitArray ba, string hex)
        {
            FromBinaryString(ba, HexString.HexToBinary(hex));
        }

        /// <summary>
        /// Returns whether or not all of the bits from this instance of BitArray are all set to 0.
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static bool AllZero(this BitArray ba)
        {
            return ba.Cast<bool>().All(i => !i);
        }

        /// <summary>
        /// Returns whether or not all of the bits from this instance of BitArray are all set to 1.
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static bool AllOne(this BitArray ba)
        {
            return ba.Cast<bool>().All(i => i);
        }
    }
}