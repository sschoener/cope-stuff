#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace cope.Relic.UCS
{
    ///<summary>
    /// Helper class for writing Dictionaries as UCS files.
    ///</summary>
    public static class UCSWriter
    {
        ///<summary>
        /// Writes the specified collection of UCS strings to the specified Stream in UCS style.
        ///</summary>
        ///<param name="ucsStrings">The collection of strings to be written.</param>
        ///<param name="stream"></param>
        /// <exception cref="ArgumentNullException"><paramref name="stream" /> is <c>null</c>.</exception>
        public static void Write(IEnumerable<KeyValuePair<uint, string>> ucsStrings, Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            Write(ucsStrings, new StreamWriter(stream, System.Text.Encoding.Unicode));
        }

        ///<summary>
        /// Writes the specified collection of UCS strings to the specified TextWriter in UCS style.
        ///</summary>
        ///<param name="ucsStrings">The collection of strings to be written.</param>
        ///<param name="writer"></param>
        ///<exception cref="ArgumentNullException"><paramref name="writer" /> is <c>null</c>.</exception>
        public static void Write(IEnumerable<KeyValuePair<uint, string>> ucsStrings, TextWriter writer)
        {
            if (ucsStrings == null) throw new ArgumentNullException("ucsStrings");
            if (writer == null) throw new ArgumentNullException("writer");
            foreach (KeyValuePair<uint, string> kvp in ucsStrings)
            {
                writer.Write(kvp.Key);
                writer.Write('\t');
                writer.WriteLine(kvp.Value);
            }
            writer.Flush();
        }
    }
}