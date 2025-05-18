namespace cope.Extensions
{
    static public class FloatExt
    {
        /// <summary>
        /// Converts this float to its string representation using the invariant culture.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static string ToInvariantString(this float f)
        {
            return f.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
