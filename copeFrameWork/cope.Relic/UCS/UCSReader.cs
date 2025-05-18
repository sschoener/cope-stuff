#region

using System;
using System.Collections.Generic;
using System.IO;
using cope.Extensions;

#endregion

namespace cope.Relic.UCS
{
    ///<summary>
    /// Helper class for reading UCS files.
    ///</summary>
    public static class UCSReader
    {
        ///<summary>
        /// Reads UCS strings from a Stream and converts them to a Dictionary.
        ///</summary>
        ///<param name="stream"></param>
        ///<returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream" /> is <c>null</c>.</exception>
        public static UCSStrings Read(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            return Read(new StreamReader(stream));
        }

        ///<summary>
        /// Reads UCS strings from a TextReader and converts them to a Dictionary.
        ///</summary>
        ///<param name="reader"></param>
        ///<returns></returns>
        public static UCSStrings Read(TextReader reader)
        {
            var strings = new Dictionary<uint, string>();
            uint maxIndex = 0;
            while (true)
            {
                string line = reader.ReadLine();

                if (line == null)
                    return new UCSStrings(strings, maxIndex);
                if (line == string.Empty)
                    continue;

                uint idx = AddUCSStringFromText(line, strings);
                if (idx > maxIndex)
                    maxIndex = idx;
            }
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        private static uint AddUCSStringFromText(string line, IDictionary<uint, string> strings)
        {
            uint index;
            try
            {
                index = uint.Parse(line.SubstringBeforeFirst(CharType.Whitespace));
            }
            catch (Exception)
            {
                return 0;
            }
            if (strings.ContainsKey(index))
                throw new Exception("This UCS file already has a string with the index " + index + ".");
            string ucsString = line.SubstringAfterFirst(CharType.Whitespace);
            strings.Add(index, ucsString);
            return index;
        }
    }
}