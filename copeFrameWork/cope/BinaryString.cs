#region

using System;
using cope.Extensions;

#endregion

namespace cope
{
    public static class BinaryString
    {
        public static string ToDecimalInverse(string binary)
        {
            double v = 0f;
            for (int p = 0; p < binary.Length; p++)
            {
                if (binary[p] != '0')
                    v += 1.0 / Math.Pow(2, p + 1);
            }
            return v.ToString().SubstringAfterFirst(new[] {',', '.'});
        }
    }
}