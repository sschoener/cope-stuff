#region

using System;

#endregion

namespace cope
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PrintValueAttribute : Attribute
    {
        /// <summary>
        /// Specify a filter: When calling GetPrintValues you can specify an array of filters, only those attributes whose filter value is
        /// in the array will be printed.
        /// </summary>
        public int Filter { get; set; }

        /// <summary>
        /// Provide a string to be used for formatting the attributed value.
        /// </summary>
        public string FormatString { get; set; }
    }
}