#region

using System;

#endregion

namespace cope.Extensions
{
    /// <summary>
    /// Extension for DateTime.
    /// </summary>
    public static class DateTimeExt
    {
        private static readonly DateTime s_unix = new DateTime(1970, 1, 1);

        /// <summary>
        /// Converts the specified unixTimeStamp to a DateTime object.
        /// </summary>
        /// <param name="unixTimeStamp">UnixTimeStamp to get the time from.</param>
        /// <returns></returns>
        public static DateTime GetFromUnixTimeStamp(uint unixTimeStamp)
        {
            // Unix Time Stamps count the seconds from 1.1.1970
            var unix1970 = new DateTime(1970, 1, 1);
            return unix1970.AddSeconds(unixTimeStamp);
        }

        /// <summary>
        /// Returns the seconds passed since 1.1.1970.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static uint GetUnixTimeStamp(this DateTime dt)
        {
            return (uint) (dt - s_unix).TotalSeconds;
        }

        /// <summary>
        /// Returns a string of format YYYY-MM-DD-hh-mm-ss.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToProperString(this DateTime dt)
        {
            return dt.ToString("yyyy'-'MM'-'dd'-'HH'-'mm'-'ss");
        }

        /// <summary>
        /// Returns a string of format YYYY-MM-DD-hh-mm-ss using a custom separator.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="separator">Separator character to be inserted between the time-values.</param>
        /// <returns></returns>
        public static string ToProperString(this DateTime dt, char separator)
        {
            return
                dt.ToString("yyyy'" + separator + "'MM'" + separator + "'dd'" + separator + "'HH'" + separator + "'mm'" +
                            separator + "'ss");
        }
    }
}