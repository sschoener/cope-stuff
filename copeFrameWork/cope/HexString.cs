#region

using System;

#endregion

namespace cope
{
    public static class HexString
    {
        /// <summary>
        /// Converts the hexadecimal representation of a number to its binary equivalent.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexToBinary(string hex)
        {
            int c = hex.Length / 8;
            if (hex.Length % 8 != 0)
                ++c;
            string binary = string.Empty;
            for (int i = 0; i < c; i++)
            {
                string s = i * 8 + 8 > hex.Length ? hex.Substring(i * 8) : hex.Substring(i * 8, 8);
                int num = Convert.ToInt32(s, 16);
                s = Convert.ToString(num, 2);
                binary += s.PadLeft(32, '0');
            }
            return binary;
        }
    }
}