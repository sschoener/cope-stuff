#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cope.Extensions;

#endregion

namespace cope
{
    /// <summary>
    /// Helper class to quickly pretty-print values annotated with the PrintValueAttribute.
    /// </summary>
    public static class PrintValueExtension
    {
        /// <summary>
        /// Gets the print value of the specified object as a string. You may provide a level of indentation.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="indentLevel"></param>
        /// <returns></returns>
        public static string GetPrintValues(this object o, int indentLevel = 0)
        {
            return GetPrintValues(o, indentLevel, null);
        }

        /// <summary>
        /// Gets all values with the PrintValueAttribute whose filter-property is in the specified filters array.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="indentLevel"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static string GetPrintValues(this object o, int indentLevel, params int[] filters)
        {
            string indent = new String('\t', indentLevel);
            StringBuilder sb = new StringBuilder();

            // print properties
            ProcessProperties(sb, indent, o, filters);

            // print fields
            ProcessFields(sb, indent, o, filters);
            return sb.ToString();
        }

        /// <summary>
        /// Processes all properties of a given object and checks whether they possess the PrintValueAttribute.
        /// If they do, it is checked that their filter is in the list of filters to be included. Also takes care
        /// of indentation.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="indent"></param>
        /// <param name="o"></param>
        /// <param name="filters"></param>
        private static void ProcessProperties(StringBuilder sb, string indent, object o, IEnumerable<int> filters)
        {
            var properties = o.GetType().GetProperties();
            foreach (var p in properties)
            {
                var a = Attribute.GetCustomAttribute(p, typeof (PrintValueAttribute), true) as PrintValueAttribute;
                if (a != null)
                {
                    if (filters != null && !filters.Contains(a.Filter))
                        continue;

                    // exclude any property using indexers, e.g this[int index]
                    if (p.GetIndexParameters().Length == 0)
                    {
                        string name = p.Name;
                        object value;
                        if (a.FormatString != null)
                        {
                            // need dynamic to use the ToString with a format string.
                            dynamic dynValue = p.GetValue(o, null);
                            value = dynValue.ToString(a.FormatString);
                        }
                        else
                            value = p.GetValue(o, null);
                        sb.AppendLine(indent, name, " = ", value);
                    }
                }
            }
        }

        private static void ProcessFields(StringBuilder sb, string indent, object o, IEnumerable<int> filters)
        {
            var fields = o.GetType().GetFields();
            foreach (var f in fields)
            {
                var a = Attribute.GetCustomAttribute(f, typeof (PrintValueAttribute), true) as PrintValueAttribute;
                if (a != null)
                {
                    if (filters != null && !filters.Contains(a.Filter))
                        continue;
                    string name = f.Name;
                    object value;
                    if (a.FormatString != null)
                    {
                        dynamic dynValue = f.GetValue(o);
                        value = dynValue.ToString(a.FormatString);
                    }
                    else
                        value = f.GetValue(o);
                    sb.AppendLine(indent, name, " = ", value);
                }
            }
        }
    }
}