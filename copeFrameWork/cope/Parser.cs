#region

using System;
using System.Globalization;

#endregion

namespace cope
{
    /// <summary>
    /// Parsing numbers the safe way!
    /// </summary>
    public static class Parser
    {
        private static readonly NumberFormatInfo s_numUs = new CultureInfo("en-US", false).NumberFormat;
        private static readonly NumberFormatInfo s_numDe = new CultureInfo("de-DE", false).NumberFormat;
        private static readonly NumberFormatInfo s_numtmp = new CultureInfo("en-US", false).NumberFormat;

        /// <summary>
        /// Parses a string to a float by first trying to use the American decimal seperator ('.') and then the German (',').
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns></returns>
        public static float ParseFloatSave(string str)
        {
            if (str.Length > 0)
            {
                try
                {
                    return float.Parse(str, s_numUs);
                }
                catch (FormatException)
                {
                    return float.Parse(str, s_numDe);
                }
                catch (OverflowException)
                {
                    return float.Parse(str, s_numDe);
                }
            }
            throw (new ArgumentNullException());
        }

        /// <summary>
        /// Parses a string to a float by using the specified decimal seperator.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="decimalSeperator">Decimal seperator to use.</param>
        /// <returns></returns>
        public static float ParseFloat(string str, string decimalSeperator)
        {
            s_numtmp.NumberDecimalSeparator = decimalSeperator;
            s_numtmp.PercentDecimalSeparator = decimalSeperator;
            s_numtmp.CurrencyDecimalSeparator = decimalSeperator;

            return float.Parse(str, s_numtmp);
        }

        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        public static float ParseFloatInvariant(string str)
        {
            float result;
            if (float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return result;
            throw new CopeException("Failed to parse " + str +
                                    " as float using NumberStyles.Float and the invariant culture.");
        }

        /// <summary>
        /// Parses a string to a decimal by first trying to use the American decimal seperator ('.') and then the German (',').
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns></returns>
        public static decimal ParseDecimalSave(string str)
        {
            if (str.Length > 0)
            {
                try
                {
                    return decimal.Parse(str, s_numUs);
                }
                catch (FormatException)
                {
                    return decimal.Parse(str, s_numDe);
                }
            }
            throw (new ArgumentNullException());
        }

        /// <summary>
        /// Parses a string to a decimal by using the specified decimal seperator.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="decimalSeperator">Decimal seperator to use.</param>
        /// <returns></returns>
        public static decimal ParseDecimal(string str, string decimalSeperator)
        {
            s_numtmp.NumberDecimalSeparator = decimalSeperator;
            s_numtmp.PercentDecimalSeparator = decimalSeperator;
            s_numtmp.CurrencyDecimalSeparator = decimalSeperator;

            return decimal.Parse(str, s_numtmp);
        }

        /// <summary>
        /// Parses a string to a double by first trying to use the American decimal seperator ('.') and then the German (',').
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns></returns>
        public static double ParseDoubleSave(string str)
        {
            if (str.Length > 0)
            {
                try
                {
                    return double.Parse(str, s_numUs);
                }
                catch (FormatException)
                {
                    return double.Parse(str, s_numDe);
                }
            }
            throw (new ArgumentNullException());
        }

        /// <summary>
        /// Parses a string to a double by using the specified decimal seperator.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="decimalSeperator">Decimal seperator to use.</param>
        /// <returns></returns>
        public static double ParseDouble(string str, string decimalSeperator)
        {
            s_numtmp.NumberDecimalSeparator = decimalSeperator;
            s_numtmp.PercentDecimalSeparator = decimalSeperator;
            s_numtmp.CurrencyDecimalSeparator = decimalSeperator;

            return double.Parse(str, s_numtmp);
        }
    }
}