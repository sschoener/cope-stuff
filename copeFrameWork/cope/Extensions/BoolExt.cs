#region

using System.Text;

#endregion

namespace cope.Extensions
{
    public static class BoolExt
    {
        /// <summary>
        /// Converts this bool array to a binary string.
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static string ToBinaryString(this bool[] ba)
        {
            var sb = new StringBuilder(ba.Length);
            foreach (bool b in ba)
                sb.Append(b ? '1' : '0');
            return sb.ToString();
        }

        public static int[] ToIntArray(this bool[] ba)
        {
            var output = new int[ba.Length];
            for (int i = 0; i < ba.Length; i++)
                output[i] = ba[i] ? 1 : 0;
            return output;
        }
    }
}