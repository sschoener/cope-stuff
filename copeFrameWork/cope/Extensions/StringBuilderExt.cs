#region

using System.Linq;
using System.Text;

#endregion

namespace cope.Extensions
{
    public static class StringBuilderExt
    {
        #region StringBuilder Trivials

        /// <summary>
        /// Determines whether the value of this StringBuilder and the value of a specified String are the same.
        /// </summary>
        /// <param name="sb">StringBuilder to operate on.</param>
        /// <param name="str">String to compare to.</param>
        /// <returns></returns>
        public static bool Equals(this StringBuilder sb, string str)
        {
            int strLength = str.Length;
            if (strLength != sb.Length)
                return false;
            for (int i = 0; i < strLength; i++)
            {
                if (str[i] != sb[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Clears the StringBuilder.
        /// </summary>
        public static StringBuilder Clear(this StringBuilder str)
        {
            str.Remove(0, str.Length);
            return str;
        }

        /// <summary>
        /// Returns a char array containing the characters of the value hold by this instance of StringBuilder.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <returns></returns>
        public static char[] ToCharArray(this StringBuilder str)
        {
            int strLength = str.Length;
            var chrTmp = new char[strLength];
            for (int i = 0; i < strLength; i++)
                chrTmp[i] = str[i];
            return chrTmp;
        }

        /// <summary>
        /// Makes the value hold by this instance of StringBuilder lower case.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        public static StringBuilder ToLower(this StringBuilder str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].IsCharOfType(CharType.Upper))
                    str[i] = char.ToLower(str[i]);
            }
            return str;
        }

        /// <summary>
        /// Makes the value hold by this instance of StringBuilder upper case.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        public static StringBuilder ToUpper(this StringBuilder str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].IsCharOfType(CharType.Lower))
                    str[i] = char.ToUpper(str[i]);
            }
            return str;
        }

        /// <summary>
        /// Inverts the case of the value hold by this instance of StringBuilder.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        public static StringBuilder InvertCase(this StringBuilder str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].IsCharOfType(CharType.Lower))
                    str[i] = char.ToUpperInvariant(str[i]);
                else if (str[i].IsCharOfType(CharType.Upper))
                    str[i] = char.ToLowerInvariant(str[i]);
            }
            return str;
        }

        #region SetString

        /// <summary>
        /// Sets the string of the StringBuilder to the string representation of value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, object value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, string value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, char[] value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, int value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, long value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, short value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, byte value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, bool value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, uint value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, ulong value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, ushort value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, sbyte value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, double value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, decimal value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, float value)
        {
            str.Clear();
            str.Append(value);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        /// <param name="repeatCount">Defines how many times the value shall be appended.</param>
        public static StringBuilder SetString(this StringBuilder str, char value, int repeatCount = 1)
        {
            str.Clear();
            str.Append(value, repeatCount);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        /// <param name="startIndex">The starting position in value (zero-based.)</param>
        /// <param name="charCount">Number of characters to append</param>
        public static StringBuilder SetString(this StringBuilder str, char[] value, int charCount, int startIndex = 0)
        {
            str.Clear();
            str.Append(value, startIndex, charCount);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        /// <param name="startIndex">The starting position in value (zero-based).</param>
        /// <param name="charCount">Number of characters to append.</param>
        public static StringBuilder SetString(this StringBuilder str, string value, int charCount, int startIndex = 0)
        {
            str.Clear();
            str.Append(value, startIndex, charCount);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        /// <param name="startIndex">The starting position in value (zero-based).</param>
        /// <param name="charCount">Number of characters to append.</param>
        /// <param name="asciiEncoding">Set to true to use ASCII enconding instead of Unicode.</param>
        public static StringBuilder SetString(this StringBuilder str, byte[] value, int charCount, int startIndex = 0,
                                              bool asciiEncoding = false)
        {
            str.Clear();
            str.Append(value, startIndex, charCount, asciiEncoding);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        /// <param name="charCount">Number of characters to append</param>
        public static StringBuilder SetString(this StringBuilder str, byte[] value, int charCount)
        {
            str.Clear();
            str.Append(value, charCount);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to set to.</param>
        public static StringBuilder SetString(this StringBuilder str, byte[] value)
        {
            str.Clear();
            str.Append(value, value.Length);
            return str;
        }

        /// <summary>
        /// Sets the string of the StringBuilder to the specified format string.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">FormatString, e.g. "Floating point number: {0:f2}".</param>
        /// <param name="args">Objects to format, e.g. a,b,c.</param>
        public static StringBuilder SetString(this StringBuilder str, string value, params object[] args)
        {
            str.Clear();
            str.AppendFormat(value, args);
            return str;
        }

        #endregion SetString

        #region Append

        /// <summary>
        /// Converts the byte[] to a char[] and append these.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Byte[] to append.</param>
        /// <param name="asciiEncoding">Set to true to use ASCII enconding instead of Unicode.</param>
        /// <param name="index">Index of the first byte in the array to append (zero-based).</param>
        /// <param name="length">Number of bytes to append.</param>
        public static StringBuilder Append(this StringBuilder str, byte[] value, int length, int index = 0,
                                           bool asciiEncoding = false)
        {
            str.Append(value.ToCharArray(asciiEncoding, index, length));
            return str;
        }

        /// <summary>
        /// Converts the byte[] to a char[] and append these.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Byte[] to append.</param>
        /// <param name="asciiEncoding">Set to true to use ASCII enconding instead of Unicode.</param>
        public static StringBuilder Append(this StringBuilder str, byte[] value, bool asciiEncoding = false)
        {
            str.Append(value.ToCharArray(asciiEncoding));
            return str;
        }

        /// <summary>
        /// Appends the value to the StringBuilder.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="value">Value to append.</param>
        /// <returns></returns>
        public static StringBuilder Append(this StringBuilder str, StringBuilder value)
        {
            for (int i = 0; i < value.Length; i++)
                str.Append(value[i]);
            return str;
        }

        /// <summary>
        /// Appends the specified string a number of times.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s">String to append.</param>
        /// <param name="times">Times to append.</param>
        /// <returns></returns>
        public static StringBuilder Append(this StringBuilder str, string s, int times)
        {
            for (; times > 0; times--)
                str.Append(s);
            return str;
        }

        public static StringBuilder Append(this StringBuilder str, params string[] strings)
        {
            foreach (string s in strings)
                str.Append(s);
            return str;
        }

        public static StringBuilder Append(this StringBuilder str, params object[] objects)
        {
            foreach (var o in objects)
                str.Append(o.ToString());
            return str;
        }

        /// <summary>
        /// Appends all arguments to the StringBuilder and adds a line break.
        /// This method has been made to avoid constructs such as str.AppendLine("string" + value)
        /// which effectively defeat the whole purpose of StringBuilders.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static StringBuilder AppendLine(this StringBuilder str, params object[] args)
        {
            if (args.Length == 0)
                return str.AppendLine();
            for (int i = 0; i < args.Length - 1; i++)
                str.Append(args[i]);
            str.AppendLine(args[args.Length - 1].ToString());
            return str;
        }

        #endregion Append

        #endregion StringBuilder Trivials

        #region StringBuilder Splitting

        public static string Substring(this StringBuilder sb, int startIndex, int length)
        {
            var cs = new char[length];
            sb.CopyTo(startIndex, cs, 0, length);
            return new string(cs);
        }

        public static char[] SubArray(this StringBuilder sb, int startIndex, int length)
        {
            var cs = new char[length];
            sb.CopyTo(startIndex, cs, 0, length);
            return cs;
        }

        #region SubstringAfterLast

        /// <summary>
        /// Returns the string after the last occurence of searchChar or the string itself if searchChar couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringAfterLast(this StringBuilder str, char searchChar, bool including = false)
        {
            int removeCount = str.LastIndexOf(searchChar);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount);
            return str.Remove(0, removeCount + 1);
        }

        /// <summary>
        /// Returns the string after the last occurence of any of the searchChars or the string itself if no searchChar could be found. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringAfterLast(this StringBuilder str, char[] searchChars,
                                                       bool including = false)
        {
            int removeCount = str.LastIndexOfAny(searchChars);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount);
            return str.Remove(0, removeCount + 1);
        }

        /// <summary>
        /// Returns the string after the last occurence of searchChar or the string itself if searchChar couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringAfterLast(this StringBuilder str, CharType charType, bool including = false)
        {
            int removecount = str.LastIndexOf(charType);
            if (removecount == -1)
                return str;
            if (including)
                return str.Remove(0, removecount);
            return str.Remove(0, removecount + 1);
        }

        #endregion SubstringAfterLast

        #region SubstringAfterFirst

        /// <summary>
        /// Returns the string after the first occurence of searchChar or the string itself if searchChar couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringAfterFirst(this StringBuilder str, char searchChar, bool including = false)
        {
            int removeCount = str.IndexOf(searchChar);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount - 1);
            return str.Remove(0, removeCount);
        }

        /// <summary>
        /// Returns the string after the first occurence of any of the searchChars or the string itself if no searchChar could be found. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringAfterFirst(this StringBuilder str, char[] searchChars,
                                                        bool including = false)
        {
            int removeCount = str.IndexOfAny(searchChars);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount - 1);
            return str.Remove(0, removeCount);
        }

        /// <summary>
        /// Returns the string after the first occurence of searchChar or the string itself if searchChar couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringAfterFirst(this StringBuilder str, CharType charType,
                                                        bool including = false)
        {
            int removeCount = str.IndexOf(charType);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount - 1);
            return str.Remove(0, removeCount);
        }

        #endregion SubstringAfterFirst

        #region SubstringBeforeLast

        /// <summary>
        /// Returns the string before the last occurence of searchChar or the string itself if searchChar couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringBeforeLast(this StringBuilder str, char searchChar, bool including = false)
        {
            int newEnd = str.LastIndexOf(searchChar);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1, str.Length - (newEnd + 1));
            return str.Remove(newEnd, str.Length - newEnd);
        }

        /// <summary>
        /// Returns the string before the last occurence of any of the searchChars or the string itself if no searchChar could be found. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringBeforeLast(this StringBuilder str, char[] searchChars,
                                                        bool including = false)
        {
            int newEnd = str.LastIndexOfAny(searchChars);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1, str.Length - (newEnd + 1));
            return str.Remove(newEnd, str.Length - newEnd);
        }

        /// <summary>
        /// Returns the string before the last occurence of searchChar or the string itself if searchChar couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringBeforeLast(this StringBuilder str, CharType charType,
                                                        bool including = false)
        {
            int newEnd = str.LastIndexOf(charType);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1, str.Length - (newEnd + 1));
            return str.Remove(newEnd, str.Length - newEnd);
        }

        #endregion SubstringBeforeLast

        #region SubstringBeforeFirst

        /// <summary>
        /// Returns the string before the first occurence of searchChar or the string itself if searchChar couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringBeforeFirst(this StringBuilder str, char searchChar, bool including = false)
        {
            int newEnd = str.IndexOf(searchChar);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1, str.Length - (newEnd + 1));
            return str.Remove(newEnd, str.Length - newEnd);
        }

        /// <summary>
        /// Returns the string before the first occurence of any of the searchChars or the string itself if no searchChar could be found. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringBeforeFirst(this StringBuilder str, char[] searchChars,
                                                         bool including = false)
        {
            int newEnd = str.IndexOfAny(searchChars);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1, str.Length - (newEnd + 1));
            return str.Remove(newEnd, str.Length - newEnd);
        }

        /// <summary>
        /// Returns the string before the first occurence of searchChar or the string itself if searchChar couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static StringBuilder SubstringBeforeFirst(this StringBuilder str, CharType charType,
                                                         bool including = false)
        {
            int newEnd = str.IndexOf(charType);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1, str.Length - (newEnd + 1));
            return str.Remove(newEnd, str.Length - newEnd);
        }

        #endregion SubstringBeforeFirst

        #endregion StringBuilder Splitting

        #region StringBuilder Information

        #region Position

        /// <summary>
        /// Returns the zero-based index of the first occurence of the specified char. Returns -1 if the char couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="c">Char to search for.</param>
        /// <returns></returns>
        public static int IndexOf(this StringBuilder str, char c)
        {
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx] == c)
                {
                    return strIdx;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based index of the first occurence of a char of CharType. Returns -1 if CharType couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType to search for.</param>
        /// <returns></returns>
        public static int IndexOf(this StringBuilder str, CharType charType)
        {
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx].IsCharOfType(charType))
                {
                    return strIdx;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based position of the first instance of searchString. Returns -1 if searchString couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchString">String to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int IndexOf(this StringBuilder str, string searchString, bool ignoreCase = true)
        {
            int searchStringLength = searchString.Length;
            int strLength = str.Length;
            if (searchStringLength > strLength || strLength == 0 || searchStringLength == 0)
                return -1;
            if (ignoreCase)
                searchString = searchString.ToLowerInvariant();
            for (int strIdx = 0; strIdx < strLength - searchStringLength; strIdx++)
            {
                if (ignoreCase)
                {
                    if (char.ToLowerInvariant(str[strIdx]) != searchString[0])
                        continue;
                }
                else if (str[strIdx] != searchString[0])
                    continue;
                for (int searchIdx = 0;; searchIdx++)
                {
                    if (searchIdx == searchStringLength)
                        return strIdx;
                    if (ignoreCase)
                    {
                        if (char.ToLower(str[strIdx + searchIdx]) != searchString[searchIdx])
                            break;
                    }
                    else if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based position of the first instance of searchString. Returns -1 if searchString couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchString">StringBuilder to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int IndexOf(this StringBuilder str, StringBuilder searchString, bool ignoreCase = true)
        {
            int searchStringLength = searchString.Length;
            int strLength = str.Length;
            if (searchStringLength > strLength || strLength == 0 || searchStringLength == 0)
                return -1;

            for (int strIdx = 0; strIdx < strLength - searchStringLength; strIdx++)
            {
                if (ignoreCase)
                {
                    if (char.ToLowerInvariant(str[strIdx]) != char.ToLowerInvariant(searchString[0]))
                        continue;
                }
                else if (str[strIdx] != searchString[0])
                    continue;
                for (int searchIdx = 0;; searchIdx++)
                {
                    if (searchIdx == searchStringLength)
                        return strIdx;
                    if (ignoreCase)
                    {
                        if (char.ToLowerInvariant(str[strIdx + searchIdx]) != char.ToLower(searchString[searchIdx]))
                            break;
                    }
                    else if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based index of the last occurence of the specified char. Returns -1 if the char couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="c">Char to search for.</param>
        /// <returns></returns>
        public static int LastIndexOf(this StringBuilder str, char c)
        {
            for (int strIdx = str.Length - 1; strIdx >= 0; strIdx--)
            {
                if (str[strIdx] == c)
                {
                    return strIdx;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based index of the last occurence of a char of CharType. Returns -1 if CharType  couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <returns></returns>
        public static int LastIndexOf(this StringBuilder str, CharType charType)
        {
            for (int strIdx = str.Length - 1; strIdx >= 0; strIdx--)
            {
                if (str[strIdx].IsCharOfType(charType))
                {
                    return strIdx;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based position of the last instance of searchString. Returns -1 if searchString couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchString">StringBuilder to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int LastIndexOf(this StringBuilder str, string searchString, bool ignoreCase = true)
        {
            int searchStringLength = searchString.Length;
            int strLength = str.Length;
            if (searchStringLength > strLength || strLength == 0 || searchStringLength == 0)
                return -1;
            if (ignoreCase)
                searchString = searchString.ToLowerInvariant();

            for (int strIdx = strLength - searchStringLength - 1; strIdx >= 0; strIdx--)
            {
                if (ignoreCase)
                {
                    if (char.ToLowerInvariant(str[strIdx]) != searchString[0])
                        continue;
                }
                else if (str[strIdx] != searchString[0])
                    continue;
                for (int searchIdx = 0;; searchIdx++)
                {
                    if (searchIdx == searchStringLength)
                        return strIdx;
                    if (ignoreCase)
                    {
                        if (char.ToLowerInvariant(str[strIdx + searchIdx]) != searchString[searchIdx])
                            break;
                    }
                    else if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based position of the last instance of searchString. Returns -1 if searchString couldn't be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchString">StringBuilder to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int LastIndexOf(this StringBuilder str, StringBuilder searchString, bool ignoreCase = true)
        {
            int searchStringLength = searchString.Length;
            int strLength = str.Length;
            if (searchStringLength > strLength || strLength == 0 || searchStringLength == 0)
                return -1;

            for (int strIdx = strLength - searchStringLength - 1; strIdx >= 0; strIdx--)
            {
                if (ignoreCase)
                    if (char.ToLowerInvariant(str[strIdx]) != char.ToLowerInvariant(searchString[0]))
                        continue;
                    else if (str[strIdx] != searchString[0])
                        continue;
                for (int searchIdx = 0;; searchIdx++)
                {
                    if (searchIdx == searchStringLength)
                        return strIdx;
                    if (ignoreCase)
                        if (char.ToLowerInvariant(str[strIdx + searchIdx]) !=
                            char.ToLowerInvariant(searchString[searchIdx]))
                            break;
                        else if (str[strIdx + searchIdx] != searchString[searchIdx])
                            break;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns an array containing each zero-based position of the searchChar in this instance. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <returns></returns>
        public static int[] IndicesOfAll(this StringBuilder str, char searchChar)
        {
            var positions = new int[Count(str, searchChar)];
            int p = 0;
            int strLength = str.Length;
            for (int i = 0; i < strLength; i++)
            {
                if (str[i] == searchChar)
                {
                    positions[p] = i;
                    p++;
                }
            }
            return positions;
        }

        /// <summary>
        /// Returns an array containing each zero-based position of a char of CharType  in this instance.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <returns></returns>
        public static int[] IndicesOfAll(this StringBuilder str, CharType charType)
        {
            var positions = new int[Count(str, charType)];
            int p = 0;
            int strLength = str.Length;
            for (int i = 0; i < strLength; i++)
            {
                if (str[i].IsCharOfType(charType))
                {
                    positions[p] = i;
                    p++;
                }
            }
            return positions;
        }

        public static int[] IndicesOfAll(this StringBuilder str, string searchString, bool ignoreCase = true)
        {
            if (searchString.Length > str.Length || str.Length == 0 || searchString.Length == 0)
                return new int[0];
            var indices = new int[str.Count(searchString, ignoreCase)];
            if (ignoreCase)
                searchString = searchString.ToLowerInvariant();
            int p = 0;
            for (int strIdx = 0; strIdx < str.Length - searchString.Length; strIdx++)
            {
                if (ignoreCase)
                {
                    if (char.ToLowerInvariant(str[strIdx]) != searchString[0])
                        continue;
                }
                else if (str[strIdx] != searchString[0])
                    continue;
                for (int searchIdx = 1;; searchIdx++)
                {
                    if (searchIdx == searchString.Length)
                    {
                        indices[p++] = strIdx;
                        break;
                    }
                    if (ignoreCase)
                    {
                        if (char.ToLowerInvariant(str[strIdx + searchIdx]) != searchString[searchIdx])
                            break;
                    }
                    else if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return indices;
        }

        /// <summary>
        /// Returns the zero-based index of the first occurence of any of the specified chars. Returns -1 if nothing could be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="chars">Chars to search for.</param>
        /// <returns></returns>
        public static int IndexOfAny(this StringBuilder str, params char[] chars)
        {
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                foreach (char c in chars)
                {
                    if (str[strIdx] == c)
                    {
                        return strIdx;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based index of the first occurence of a char of any of the specified CharTypes. Returns -1 if nothing could be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charTypes">CharTypes to search for.</param>
        /// <returns></returns>
        public static int IndexOfAny(this StringBuilder str, params CharType[] charTypes)
        {
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                foreach (CharType  ctc in charTypes)
                {
                    if (str[strIdx].IsCharOfType(ctc))
                    {
                        return strIdx;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based index of the last occurence of any of the specified chars. Returns -1 if nothing could be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="chars">Chars to search for.</param>
        /// <returns></returns>
        public static int LastIndexOfAny(this StringBuilder str, params char[] chars)
        {
            for (int strIdx = str.Length - 1; strIdx >= 0; strIdx--)
            {
                foreach (char c in chars)
                {
                    if (str[strIdx] == c)
                    {
                        return strIdx;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based index of the last occurence of a char of any of the specified CharTypes. Returns -1 if nothing could be found.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charTypes">CharTypes to search for.</param>
        /// <returns></returns>
        public static int LastIndexOfAny(this StringBuilder str, params CharType[] charTypes)
        {
            for (int strIdx = str.Length - 1; strIdx >= 0; strIdx--)
            {
                foreach (CharType charType in charTypes)
                {
                    if (str[strIdx].IsCharOfType(charType))
                    {
                        return strIdx;
                    }
                }
            }
            return -1;
        }

        #endregion Position

        #region Searching

        /// <summary>
        /// Returns true if this instance ends with the specified value. Returns false if the value to check against is longer than the string to check itself.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="end">StringBuilder which the end is compared to.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static bool EndsWith(this StringBuilder str, StringBuilder end, bool ignoreCase = true)
        {
            int endLength = end.Length;
            if (str.Length == 0 || endLength == 0 || endLength > str.Length)
                return false;
            int deltaLength = str.Length - endLength;
            for (int endIdx = 0; endIdx < endLength; endIdx++)
            {
                if (ignoreCase
                        ? char.ToLower(str[deltaLength + endIdx]) != char.ToLower(end[endIdx])
                        : str[deltaLength + endIdx] != end[endIdx])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns true if this instance ends with the specified value. Returns false if the value to check against is longer than the string to check itself.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="end">String which the end is compared to.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static bool EndsWith(this StringBuilder str, string end, bool ignoreCase = true)
        {
            int endLength = end.Length;
            if (str.Length == 0 || endLength == 0 | endLength > str.Length)
                return false;
            int deltaLength = str.Length - endLength;
            for (int endIdx = 0; endIdx < endLength; endIdx++)
            {
                if (ignoreCase
                        ? char.ToLower(str[deltaLength + endIdx]) != char.ToLower(end[endIdx])
                        : str[deltaLength + endIdx] != end[endIdx])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns true if this instance ends with the specified value. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="end">Character which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWith(this StringBuilder str, char end)
        {
            return str.Length != 0 && str[str.Length - 1] == end;
        }

        /// <summary>
        /// Returns true if this instance ends with the specified value. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charType">CharacterType which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWith(this StringBuilder str, CharType charType)
        {
            return str.Length != 0 && str[str.Length - 1].IsCharOfType(charType);
        }

        /// <summary>
        /// Returns true if this instance ends with any of the specified chars. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ends">Characters which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWithAny(this StringBuilder str, params char[] ends)
        {
            if (str.Length == 0)
                return false;
            char c = str[str.Length - 1];
            return ends.Any(chr => c == chr);
        }

        /// <summary>
        /// Returns true if this instance ends with a char of any of the specified CharTypes. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charTypes">CharTypes which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWithAny(this StringBuilder str, params CharType[] charTypes)
        {
            if (str.Length == 0)
                return false;
            char c = str[str.Length - 1];
            return charTypes.Any(ctc => c.IsCharOfType(ctc));
        }

        /// <summary>
        /// Returns true if this instance begins with the specified value. Returns false if the value to check against is longer than the string to check itself.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginning">StringBuilder which the beginning is compared to.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static bool StartsWith(this StringBuilder str, StringBuilder beginning, bool ignoreCase = true)
        {
            int beginningLength = beginning.Length;
            if (str.Length == 0 || beginningLength == 0 || beginningLength > str.Length)
                return false;
            for (int i = 0; i < beginningLength; i++)
            {
                if (ignoreCase ? char.ToLower(str[i]) != char.ToLower(beginning[i]) : str[i] != beginning[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns true if this instance begins with the specified value. Returns false if the value to check against is longer than the string to check itself.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginning">String which the beginning is compared to.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static bool StartsWith(this StringBuilder str, string beginning, bool ignoreCase = true)
        {
            int beginningLength = beginning.Length;
            if (str.Length == 0 || beginningLength == 0 || beginningLength > str.Length)
                return false;
            for (int i = 0; i < beginningLength; i++)
            {
                if (ignoreCase ? char.ToLower(str[i]) != char.ToLower(beginning[i]) : str[i] != beginning[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns true if this instance begins with the specified value. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginning">Character which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWith(this StringBuilder str, char beginning)
        {
            return str.Length != 0 && str[0] == beginning;
        }

        /// <summary>
        /// Returns true if this instance begins with the specified value. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charType">CharacterType which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWith(this StringBuilder str, CharType charType)
        {
            return str.Length != 0 && str[0].IsCharOfType(charType);
        }

        /// <summary>
        /// Returns true if this instance begins with any of the specified chars. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginnings">Characters which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWithAny(this StringBuilder str, params char[] beginnings)
        {
            if (str.Length == 0)
                return false;
            char c = str[0];
            return beginnings.Any(chr => c == chr);
        }

        /// <summary>
        /// Returns true if this instance begins with a char of any of the specified CharTypes. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charTypes">CharTypes which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWithAny(this StringBuilder str, params CharType[] charTypes)
        {
            if (str.Length == 0)
                return false;
            char c = str[0];
            return charTypes.Any(ctc => c.IsCharOfType(ctc));
        }

        /// <summary>
        /// Returns true if the string contains the specified character.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="c">Value to search for.</param>
        /// <returns></returns>
        public static bool Contains(this StringBuilder str, char c)
        {
            return str.IndexOf(c) != -1;
        }

        /// <summary>
        /// Returns true if the string contains a character of the specified CharType.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">Value to search for.</param>
        /// <returns></returns>
        public static bool Contains(this StringBuilder str, CharType charType)
        {
            return str.IndexOf(charType) != -1;
        }

        /// <summary>
        /// Returns true if this instance contains the specified value.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchString">Value to search for.</param>
        /// <param name="ignoreCase">Set to true to care about case.</param>
        /// <returns></returns>
        public static bool Contains(this StringBuilder str, string searchString, bool ignoreCase = true)
        {
            return str.IndexOf(searchString, ignoreCase) != -1;
        }

        /// <summary>
        /// Returns true if the string contains the string in the specified StringBuilder.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchString">Value to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static bool Contains(this StringBuilder str, StringBuilder searchString, bool ignoreCase = true)
        {
            return str.IndexOf(searchString, ignoreCase) != -1;
        }

        /// <summary>
        /// Returns true if the string contains any of the specified characters.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="c">Chars to search for.</param>
        /// <returns></returns>
        public static bool ContainsAny(this StringBuilder str, params char[] c)
        {
            return str.IndexOfAny(c) != -1;
        }

        /// <summary>
        /// Returns true if the string contains a character of any of the specified CharTypes.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="ct">CharTypes to search for.</param>
        /// <returns></returns>
        public static bool ContainsAny(this StringBuilder str, params CharType[] ct)
        {
            return str.IndexOfAny(ct) != -1;
        }

        #endregion Searching

        #region Counting

        /// <summary>
        /// Returns the number of occurences of searchChar. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <returns></returns>
        public static int Count(this StringBuilder str, char searchChar)
        {
            int amount = 0;
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx] == searchChar)
                {
                    amount++;
                }
            }
            return amount;
        }

        /// <summary>
        /// Returns the number of occurences of any of the searchChars. Case-sensitive.
        /// </summary>
        /// <param name="str">StringBuilder to operate on</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <returns></returns>
        public static int Count(this StringBuilder str, params char[] searchChars)
        {
            int amount = 0;
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                foreach (char c in searchChars)
                {
                    if (str[strIdx] == c)
                    {
                        amount++;
                        break;
                    }
                }
            }
            return amount;
        }

        /// <summary>
        /// Returns the number of occurences of chars of CharType.
        /// </summary>
        /// <param name="str">StringBuilder to operate on</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <returns></returns>
        public static int Count(this StringBuilder str, CharType charType)
        {
            int amount = 0;
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx].IsCharOfType(charType))
                {
                    amount++;
                }
            }
            return amount;
        }

        /// <summary>
        /// Returns the number of occurences of chars of any of the given CharTypes.
        /// </summary>
        /// <param name="str">StringBuilder to operate on</param>
        /// <param name="charTypes">CharTypes to search for.</param>
        /// <returns></returns>
        public static int Count(this StringBuilder str, params CharType[] charTypes)
        {
            int amount = 0;
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                foreach (CharType charType in charTypes)
                {
                    if (str[strIdx].IsCharOfType(charType))
                    {
                        amount++;
                        break;
                    }
                }
            }
            return amount;
        }

        /// <summary>
        /// Returns the number of occurences of searchString.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchString">String to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int Count(this StringBuilder str, string searchString, bool ignoreCase = true)
        {
            if (searchString.Length > str.Length || searchString.Length == 0 || str.Length == 0)
                return 0;
            int count = 0;
            if (ignoreCase)
                searchString = searchString.ToLowerInvariant();

            for (int strIdx = 0; strIdx < str.Length - searchString.Length; strIdx++)
            {
                if (ignoreCase)
                {
                    if (char.ToLowerInvariant(str[strIdx]) != searchString[0])
                        continue;
                }
                else if (str[strIdx] != searchString[0])
                    continue;
                for (int searchIdx = 1;; searchIdx++)
                {
                    // searched the whole search string without breaking? => found
                    if (searchIdx == searchString.Length)
                    {
                        count++;
                        break;
                    }
                    if (ignoreCase)
                    {
                        if (char.ToLowerInvariant(str[strIdx + searchIdx]) != searchString[searchIdx])
                            break;
                    }
                    if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return count;
        }

        /// <summary>
        /// Returns the number of occurences of searchString.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="searchString">StringBuilder to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int Count(this StringBuilder str, StringBuilder searchString, bool ignoreCase = true)
        {
            if (searchString.Length > str.Length || searchString.Length == 0 || str.Length == 0)
                return 0;
            int count = 0;
            for (int strIdx = 0; strIdx < str.Length - searchString.Length; strIdx++)
            {
                if (ignoreCase)
                {
                    if (char.ToLowerInvariant(str[strIdx]) != char.ToLowerInvariant(searchString[0]))
                        continue;
                }
                else if (str[strIdx] != searchString[0])
                    continue;
                for (int searchIdx = 1;; searchIdx++)
                {
                    if (searchIdx == searchString.Length)
                    {
                        count++;
                        break;
                    }
                    if (ignoreCase)
                    {
                        if (char.ToLowerInvariant(str[strIdx + searchIdx]) !=
                            char.ToLowerInvariant(searchString[searchIdx]))
                            break;
                    }
                    else if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return count;
        }

        #endregion Counting

        #endregion StringBuilder Information

        #region StringBuilder Modification

        /// <summary>
        /// Replaces all occurences of oldChar with newChar.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="oldChar">The char to replace.</param>
        /// <param name="newChar">The new char.</param>
        /// <returns></returns>
        public static StringBuilder ReplaceChar(this StringBuilder str, char oldChar, char newChar)
        {
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx] == oldChar)
                {
                    str[strIdx] = newChar;
                }
            }
            return str;
        }

        /// <summary>
        /// Replaces all occurences of oldChars with newChar.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="oldChars">The chars to replace.</param>
        /// <param name="newChar">The new char.</param>
        /// <returns></returns>
        public static StringBuilder ReplaceChar(this StringBuilder str, char newChar, params char[] oldChars)
        {
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (oldChars.Any(str[strIdx].Equals))
                    str[strIdx] = newChar;
            }
            return str;
        }

        /// <summary>
        /// Replaces all occurences of oldChars with newChars (so if oldChars[0] is found it's replaced with newChars[0]).
        /// If oldChars is bigger than newChars for every [i] > newChars.Length newChars[lenght-1] is used.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="oldChars">The chars to replace.</param>
        /// <param name="newChars">The new chars.</param>
        /// <returns></returns>
        public static StringBuilder ReplaceChar(this StringBuilder str, char[] oldChars, char[] newChars)
        {
            int strLength = str.Length;
            int oldCharsLength = oldChars.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                for (int oldIdx = 0; oldIdx < oldCharsLength; oldIdx++)
                {
                    if (str[strIdx].Equals(oldChars[oldIdx]))
                    {
                        if (oldIdx >= newChars.Length)
                            str[strIdx] = newChars[newChars.Length - 1];
                        else
                            str[strIdx] = newChars[oldIdx];
                        break;
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// Replaces all occurences of Chars of a special CharType  with newChar.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to replace.</param>
        /// <param name="newChar">Char to insert.</param>
        public static StringBuilder ReplaceChar(this StringBuilder str, CharType charType, char newChar)
        {
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx].IsCharOfType(charType))
                {
                    str[strIdx] = newChar;
                }
            }
            return str;
        }

        /// <summary>
        /// Removes all chars with the specified value c.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="c">Char to remove.</param>
        /// <returns></returns>
        public static StringBuilder RemoveAll(this StringBuilder str, char c)
        {
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx] != c)
                    continue;
                str[newIdx] = str[strIdx];
                newIdx++;
            }
            str.Remove(newIdx);
            return str;
        }

        /// <summary>
        /// Removes all chars with any of the specified values c
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="chars">Chars to remove.</param>
        /// <returns></returns>
        public static StringBuilder RemoveAll(this StringBuilder str, params char[] chars)
        {
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                bool remove = false;
                foreach (char c in chars)
                {
                    if (str[strIdx] == c)
                        remove = true;
                }
                if (remove)
                    break;
                str[newIdx] = str[strIdx];
                newIdx++;
            }
            str.Remove(newIdx);
            return str;
        }

        /// <summary>
        /// Removes all chars of the specified CharType.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to remove.</param>
        /// <returns></returns>
        public static StringBuilder RemoveAll(this StringBuilder str, CharType charType)
        {
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (!str[strIdx].IsCharOfType(charType))
                    continue;
                str[newIdx] = str[strIdx];
                newIdx++;
            }
            str.Remove(newIdx);
            return str;
        }

        /// <summary>
        /// Removes all chars of the specified CharTypes.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charTypes">CharTypes to remove.</param>
        /// <returns></returns>
        public static StringBuilder RemoveAll(this StringBuilder str, params CharType[] charTypes)
        {
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                bool remove = false;
                foreach (CharType charType in charTypes)
                {
                    if (str[strIdx].IsCharOfType(charType))
                        remove = true;
                }
                if (remove)
                    continue;
                str[newIdx] = str[strIdx];
                newIdx++;
            }
            str.Remove(newIdx);
            return str;
        }

        /// <summary>
        /// Removes everything but the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="c">Char to keep.</param>
        /// <returns></returns>
        public static StringBuilder RemoveAllBut(this StringBuilder str, char c)
        {
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx] == c)
                {
                    str[newIdx] = str[strIdx];
                    newIdx++;
                }
            }
            str.Remove(newIdx);
            return str;
        }

        /// <summary>
        /// Removes everything but the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="chars">Chars to keep.</param>
        /// <returns></returns>
        public static StringBuilder RemoveAllBut(this StringBuilder str, params char[] chars)
        {
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                foreach (char c in chars)
                {
                    if (str[strIdx] == c)
                    {
                        str[newIdx] = str[strIdx];
                        newIdx++;
                        break;
                    }
                }
            }
            str.Remove(newIdx);
            return str;
        }

        /// <summary>
        /// Removes everything but the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charType">CharType  to keep.</param>
        /// <returns></returns>
        public static StringBuilder RemoveAllBut(this StringBuilder str, CharType charType)
        {
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx].IsCharOfType(charType))
                {
                    str[newIdx] = str[strIdx];
                    newIdx++;
                }
            }
            str.Remove(newIdx);
            return str;
        }

        /// <summary>
        /// Removes everything but the specified value.
        /// </summary>
        /// <param name="str">StringBuilder to operate on.</param>
        /// <param name="charTypes">CharTypes to keep.</param>
        /// <returns></returns>
        public static StringBuilder RemoveAllBut(this StringBuilder str, params CharType[] charTypes)
        {
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                foreach (CharType charType in charTypes)
                {
                    if (str[strIdx].IsCharOfType(charType))
                    {
                        str[newIdx] = str[strIdx];
                        newIdx++;
                        break;
                    }
                }
            }
            str.Remove(newIdx);
            return str;
        }

        /// <summary>
        /// Removes all characters starting at 'startIndex'.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static StringBuilder Remove(this StringBuilder str, int startIndex)
        {
            str.Remove(startIndex, str.Length - startIndex);
            return str;
        }

        /// <summary>
        /// Removes the last 'count' characters.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static StringBuilder RemoveLast(this StringBuilder str, int count)
        {
            if (str.Length <= count)
                str.Clear();
            else
                str.Remove(str.Length - count, count);
            return str;
        }

        /// <summary>
        /// Reverses a StringBuilder
        /// </summary>
        /// <param name="str">StringBuilder to operate on</param>
        public static StringBuilder Reverse(this StringBuilder str)
        {
            int strLength = str.Length;
            var tmp = new char[strLength];
            str.CopyTo(0, tmp, 0, strLength);
            for (int i = 1; i <= strLength; i++)
            {
                str[strLength - i] = tmp[i];
            }
            return str;
        }

        public static void TrimEnd(this StringBuilder str, char trimChar)
        {
            int m = str.Length;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] != trimChar)
                    break;
                m--;
            }
            if (m == str.Length)
                return;
            str.Remove(m, str.Length - m);
        }

        public static void TrimStart(this StringBuilder str, char trimChar)
        {
            int m = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != trimChar)
                    break;
                m++;
            }
            if (m == 0)
                return;
            str.Remove(0, m);
        }

        #endregion StringBuilder Modification
    }
}