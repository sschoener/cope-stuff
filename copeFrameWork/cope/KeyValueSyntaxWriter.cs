#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using cope.Extensions;

#endregion

namespace cope
{
    /// <summary>
    /// This class allows to convert <c>KeyedValue</c> objects to their string representation in Key-Value-Syntax.
    /// See <see cref="KeyValueSyntaxParser"/> for a description of the Key-Value-Syntax.
    /// </summary>
    public class KeyValueSyntaxWriter
    {
        #region statics

        /// <summary>
        /// Converts the given <c>KeyedValue</c> to its string representation in Key-Value-Syntax. You may specify some options
        /// via a <c>KeyValueSyntaxWriterOptions</c> object.
        /// </summary>
        /// <param name="kv">The value to convert.</param>
        /// <param name="options"></param>
        /// <returns>Returns the string representation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="kv" /> is <c>null</c>.</exception>
        /// <exception cref="CopeException">Trying to get string representation of an KeyedValue with an invalid type.</exception>
        public static string[] GetString(KeyedValue kv, KeyValueSyntaxWriterOptions options = null)
        {
            if (kv == null) throw new ArgumentNullException("kv");
            try
            {
                if (options == null)
                    options = new KeyValueSyntaxWriterOptions();
                var writer = new KeyValueSyntaxWriter(options);
                return writer.GetStringToplevel(kv);
            }
            catch (Exception ex)
            {
                var excep = new CopeException(ex, "Error while converting KeyedValue to Key-Value-Syntax.");
                excep.Data["KeyedValue"] = kv;
                throw excep;
            }
        }

        /// <summary>
        /// Converts the given IEnumerable of <c>KeyedValue</c> to its string representation in Key-Value-Syntax. You may specify some options
        /// via a <c>KeyValueSyntaxWriterOptions</c> object.
        /// </summary>
        /// <param name="kvs">The value to convert.</param>
        /// <param name="options"></param>
        /// <returns>Returns the string representation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="kvs" /> is <c>null</c>.</exception>
        /// <exception cref="CopeException">Trying to get string representation of an KeyedValue with an invalid type.</exception>
        public static string[] GetStrings(IEnumerable<KeyedValue> kvs, KeyValueSyntaxWriterOptions options = null)
        {
            if (kvs == null) throw new ArgumentNullException("kvs");
            try
            {
                if (options == null)
                    options = new KeyValueSyntaxWriterOptions();
                var writer = new KeyValueSyntaxWriter(options);
                string[][] output = new string[kvs.Count()][];
                int i = 0;
                foreach (var kv in kvs)
                {
                    output[i] = writer.GetStringToplevel(kv);
                    i++;
                }
                return output.Flatten();
            }
            catch (Exception ex)
            {
                var excep = new CopeException(ex, "Error while converting KeyedValue to Key-Value-Syntax.");
                excep.Data["KeyedValue"] = kvs;
                throw excep;
            }
        }

        #endregion

        #region fields

        private readonly string m_sIndentString;
        private readonly bool m_bSortEntries;

        #endregion

        private KeyValueSyntaxWriter(KeyValueSyntaxWriterOptions options)
        {
            m_sIndentString = new String(options.UseTabIndentation ? '\t' : ' ', options.IndentWidth);
            if (options.UsePipes)
                m_sIndentString = '|' + m_sIndentString;
            m_bSortEntries = options.SortEntries;
        }

        #region methods

        /// <exception cref="CopeException">Trying to get string representation of an KeyedValue with an invalid type.</exception>
        private string[] GetStringToplevel(KeyedValue kv)
        {
            switch (kv.Type)
            {
                case KeyValueType.Table:
                    return GetStringTable(kv, true);
                case KeyValueType.Invalid:
                    throw new CopeException("Trying to get string representation of an KeyedValue with an invalid type.");
                default:
                    return new[] {GetStringOther(kv)};
            }
        }

        /// <exception cref="CopeException">Trying to get string representation of an KeyedValue with an invalid type.</exception>
        private string[] GetStringIntern(KeyedValue kv)
        {
            switch (kv.Type)
            {
                case KeyValueType.Table:
                    return GetStringTable(kv);
                case KeyValueType.Invalid:
                    throw new CopeException("Trying to get string representation of an KeyedValue with an invalid type.");
                default:
                    return new[] {GetStringOther(kv)};
            }
        }

        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        private string[] GetStringTable(KeyedValue kv, bool toplevel = false)
        {
            KeyValueTable kvt = (KeyValueTable) kv.Value;
            var strings = new string[kvt.ChildCount][];
            int idx = 0;

            IEnumerable<KeyedValue> tableEntries = kvt;
            if (m_bSortEntries)
            {
                var list = kvt.ToList();
                list.Sort();
                tableEntries = list;
            }
                
            foreach (var v in tableEntries)
                strings[idx++] = GetStringIntern(v).MapInplace(s => m_sIndentString + s);
            int numLines = strings.Aggregate(0, (c, ss) => c + ss.Length);
            var output = new string[numLines + 2];
            strings.Flatten(output, 1);
            if (toplevel && string.IsNullOrWhiteSpace(kv.Key))
                output[0] = "{";
            else
                output[0] = kv.Key + ": {";
            if (!string.IsNullOrWhiteSpace(kv.MetaData))
                output[0] += " -$ " + kv.MetaData + " $-";
            if (!string.IsNullOrWhiteSpace(kv.AutoComment))
                output[0] += " -- " + kv.AutoComment;

            output[numLines + 1] = "};";
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kv"></param>
        /// <returns></returns>
        /// <exception cref="CopeException">Invalid type in GetStringOther, should never happen.</exception>
        private string GetStringOther(KeyedValue kv)
        {
            string s = kv.Key + ": ";
            switch (kv.Type)
            {
                case KeyValueType.Boolean:
                    s += kv.Value.ToString().ToLowerInvariant();
                    break;
                case KeyValueType.Integer:
                    s += ((int) kv.Value).ToString(CultureInfo.InvariantCulture);
                    break;
                case KeyValueType.String:
                    s += '"' + kv.Value.ToString() + '"';
                    break;
                case KeyValueType.Float:
                    s += ((float) kv.Value).ToString(CultureInfo.InvariantCulture) + 'f';
                    break;
                default:
                    throw new CopeException("Invalid type in GetStringOther, should never happen.");
            }
            s += ';';
            if (!string.IsNullOrWhiteSpace(kv.MetaData))
                s += " -$ " + kv.MetaData + " $-";
            if (!string.IsNullOrWhiteSpace(kv.AutoComment))
                s += " -- " + kv.AutoComment;
            return s;
        }

        #endregion
    }
}