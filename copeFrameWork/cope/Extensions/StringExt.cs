#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

#endregion

namespace cope.Extensions
{
    /// <summary>
    /// Static class offering lots of useful functions for strings.
    /// </summary>
    public static class StringExt
    {
        /// <summary>
        /// Determines whether the content of this string and the content of a specified StringBuilder are the same.
        /// Mind that the content's equality is independent of the StringBuilder's capacity-property.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="sb">StringBuilder to compare to.</param>
        /// <returns>True if the contents of both the <c>string</c> and the <c>StringBuilder</c> are equal.</returns>
        public static bool Equals(this string str, StringBuilder sb)
        {
            if (str.Length != sb.Length)
                return false;
            return !str.Where((character, strIdx) => character != sb[strIdx]).Any();
        }

        /// <summary>
        /// Returns whether this string is an Ascii string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsAscii(this string value)
        {
            return Encoding.UTF8.GetByteCount(value) == value.Length;
        }

        /// <summary>
        /// Tries to convert the content of this string to a given type using the TypeConverters available
        /// via the <c>TypeDescriptor</c> class. May throw an <c>CopeException</c> if there is no TypeConverter
        /// available for the given type.
        /// </summary>
        /// <typeparam name="T">The type to convert the string to.</typeparam>
        /// <param name="str">The string to operate on.</param>
        /// <returns>Returns the converted value.</returns>
        /// <exception cref="CopeException">Thrown when there is no appropriate converter available.</exception>
        public static T ConvertTo<T>(this string str)
        {
            TypeConverter c = TypeDescriptor.GetConverter(typeof (T));
            if (c == null)
                throw new CopeException("Unable to convert '" + str + "' to type " + typeof (T).FullName +
                                        ". No converter available!");
            return (T) c.ConvertFromString(str);
        }

        /// <summary>
        /// Tries to convert the content of this string to a given type using the TypeConverters available
        /// via the <c>TypeDescriptor</c> class. You may specify an alternative value to be used when the 
        /// conversion fails or if there is no converter available.
        /// </summary>
        /// <typeparam name="T">The type to convert the string to.</typeparam>
        /// <param name="str">The string to operate on.</param>
        /// <param name="alternative">The alternative value to return if the operation should fail.</param>
        /// <returns>Returns the converted value or the alternative.</returns>
        public static T ConvertTo<T>(this string str, T alternative)
        {
            TypeConverter c = TypeDescriptor.GetConverter(typeof (T));
            if (c == null)
                return alternative;
            try
            {
                return (T) c.ConvertFromString(str);
            }
            catch
            {
                return alternative;
            }
        }

        #region String Information

        #region Position

        /// <summary>
        /// Returns the zero-based index of the first occurence of a char of CharType. Returns -1 if CharType  couldn't be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <returns></returns>
        public static int IndexOf(this string str, CharType charType)
        {
            for (int idx = 0; idx < str.Length; idx++)
            {
                if (str[idx].IsCharOfType(charType))
                {
                    return idx;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based position of the first instance of searchString. Returns -1 if searchString couldn't be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">StringBuilder to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int IndexOf(this string str, StringBuilder searchString, bool ignoreCase = true)
        {
            int strLength = str.Length;
            int searchStringLength = searchString.Length;
            if (searchStringLength == 0 || strLength == 0 || searchStringLength > strLength)
                return -1;
            if (ignoreCase)
                str = str.ToLowerInvariant();
            for (int strIdx = 0; strIdx < strLength - searchStringLength; strIdx++)
            {
                if (ignoreCase)
                {
                    if (str[strIdx] != char.ToLowerInvariant(searchString[0]))
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
                        if (str[strIdx + searchIdx] != char.ToLower(searchString[searchIdx]))
                            break;
                    }
                    else if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based index of the last occurence of a char of CharType. Returns -1 if CharType  couldn't be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="ct">CharType  to search for.</param>
        /// <returns></returns>
        public static int LastIndexOf(this string str, CharType ct)
        {
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i].IsCharOfType(ct))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the zero-based position of the last instance of searchString. Returns -1 if searchString couldn't be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">StringBuilder to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int LastIndexOf(this string str, StringBuilder searchString, bool ignoreCase = true)
        {
            int searchStringLength = searchString.Length;
            int strLength = str.Length;
            if (searchStringLength > strLength || strLength == 0 || searchStringLength == 0)
                return -1;
            if (ignoreCase)
                str = str.ToLowerInvariant();

            for (int strIdx = strLength - searchStringLength - 1; strIdx >= 0; strIdx--)
            {
                if (ignoreCase)
                    if (str[strIdx] != char.ToLowerInvariant(searchString[0]))
                        continue;
                    else if (str[strIdx] != searchString[0])
                        continue;
                for (int searchIdx = 0;; searchIdx++)
                {
                    if (searchIdx == searchStringLength)
                        return strIdx;
                    if (ignoreCase)
                    {
                        if (str[strIdx + searchIdx] != char.ToLowerInvariant(searchString[searchIdx]))
                            break;
                    }
                    else if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns an array containing each zero-based position of the searchChar in this instance. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <returns></returns>
        public static int[] IndicesOfAll(this string str, char searchChar)
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
        /// <param name="str">String to operate on.</param>
        /// <param name="ct">CharType  to search for.</param>
        /// <returns></returns>
        public static int[] IndicesOfAll(this string str, CharType ct)
        {
            var positions = new int[Count(str, ct)];
            int p = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].IsCharOfType(ct))
                {
                    positions[p] = i;
                    p++;
                }
            }
            return positions;
        }

        public static int[] IndicesOfAll(this string str, string searchString, bool ignoreCase = true)
        {
            if (searchString.Length > str.Length || str.Length == 0 || searchString.Length == 0)
                return new int[0];
            var indices = new int[str.Count(searchString, ignoreCase)];
            if (ignoreCase)
            {
                str = str.ToLowerInvariant();
                searchString = searchString.ToLowerInvariant();
            }
            int p = 0;
            for (int strIdx = 0; strIdx < str.Length - searchString.Length; strIdx++)
            {
                if (str[strIdx] != searchString[0])
                    continue;
                for (int searchIdx = 1;; searchIdx++)
                {
                    if (searchIdx == searchString.Length)
                    {
                        indices[p++] = strIdx;
                        break;
                    }
                    if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return indices;
        }

        /// <summary>
        /// Returns the zero-based index of the first occurence of a char of any of the specified CharTypes. Returns -1 if nothing could be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charTypes">CharTypes to search for.</param>
        /// <returns></returns>
        public static int IndexOfAny(this string str, params CharType[] charTypes)
        {
            int strLength = str.Length;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
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

        /// <summary>
        /// Returns the zero-based index of the last occurence of a char of any of the specified CharTypes. Returns -1 if nothing could be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charTypes">CharTypes to search for.</param>
        /// <returns></returns>
        public static int LastIndexOfAny(this string str, params CharType[] charTypes)
        {
            int length = str.Length - 1;
            for (int strIdx = length; strIdx >= 0; strIdx--)
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

        #region checking parts of the string

        /// <summary>
        /// Checks whether or not the character at position 'idx' and the specified character are equal.
        /// If 'idx' is greater or equal to the string's length, the method returns false.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="idx"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool HasCharAt(this string str, int idx, char c)
        {
            if (idx > str.Length)
                return false;
            return str[idx] == c;
        }

        /// <summary>
        /// Checks whether or not the character at position 'idx' is of the specified type.
        /// If 'idx' is greater or equal to the string's length, the method returns false.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="idx"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static bool HasCharAt(this string str, int idx, CharType ct)
        {
            if (idx > str.Length)
                return false;
            return str[idx].IsCharOfType(ct);
        }

        /// <summary>
        /// Checks whether or not the character at position 'idx' satisfies the given predicate.
        /// If 'idx' is greater or equal to the string's length, the method returns false.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="idx"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool HasCharAt(this string str, int idx, Func<char, bool> predicate)
        {
            if (idx > str.Length)
                return false;
            return predicate(str[idx]);
        }

        /// <summary>
        /// Searches for the first character which is of the specified type starting at the specified index.
        /// Returns true if there is a character of the specified CharType, otherwise false. The character
        /// which has been found is available via an out parameter.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="idx"></param>
        /// <param name="ct"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool FirstCharFrom(this string str, int idx, CharType ct, out char c)
        {
            for (; idx < str.Length; idx++)
            {
                if (str[idx].IsCharOfType(ct))
                {
                    c = str[idx];
                    return true;
                }
            }
            c = '\0';
            return false;
        }

        /// <summary>
        /// Returns true if this instance ends with the specified value. Returns false if the value to check against is longer than the string to check itself.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="end">StringBuilder which the end is compared to.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static bool EndsWith(this string str, StringBuilder end, bool ignoreCase = true)
        {
            int endLength = end.Length;
            int strLength = str.Length;
            if (strLength == 0 || endLength == 0 || endLength > strLength)
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
        public static bool EndsWith(this string str, char end)
        {
            return str.Length != 0 && str[str.Length - 1] == end;
        }

        /// <summary>
        /// Returns true if this instance ends with the specified value. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charType">CharacterType which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWith(this string str, CharType charType)
        {
            return str.Length != 0 && str[str.Length - 1].IsCharOfType(charType);
        }

        /// <summary>
        /// Returns true if this instance ends with any of the specified chars. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ends">Characters which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWithAny(this string str, IEnumerable<char> ends)
        {
            if (str.Length == 0)
                return false;
            return ends.Any(chr => str[str.Length - 1] == chr);
        }

        /// <summary>
        /// Returns true if this instance ends with any of the specified chars. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ends">Characters which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWithAny(this string str, params char[] ends)
        {
            if (str.Length == 0)
                return false;
            return ends.Any(chr => str[str.Length - 1] == chr);
        }

        /// <summary>
        /// Returns true if this instance ends with a char of any of the specified CharTypes. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charTypes">CharTypes which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWithAny(this string str, IEnumerable<CharType> charTypes)
        {
            if (str.Length == 0)
                return false;
            return charTypes.Any(charType => str[str.Length - 1].IsCharOfType(charType));
        }

        /// <summary>
        /// Returns true if this instance ends with a char of any of the specified CharTypes. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charTypes">CharTypes which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWithAny(this string str, params CharType[] charTypes)
        {
            if (str.Length == 0)
                return false;
            return charTypes.Any(charType => str[str.Length - 1].IsCharOfType(charType));
        }

        /// <summary>
        /// Returns true if this instance ends with any of the specified strings. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strings">Strings which the end is compared to.</param>
        /// <returns></returns>
        public static bool EndsWithAny(this string str, params string[] strings)
        {
            if (str.Length == 0)
                return false;
            return strings.Any(s => str.EndsWith(str));
        }

        /// <summary>
        /// Returns true if this instance begins with the specified value. Returns false if the value to check against is longer than the string to check itself.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginning">StringBuilder which the beginning is compared to.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static bool StartsWith(this string str, StringBuilder beginning, bool ignoreCase = true)
        {
            int beginningLength = beginning.Length;
            if (str.Length == 0 || beginningLength > str.Length)
                return false;
            for (int strIdx = 0; strIdx < beginningLength; strIdx++)
            {
                if (ignoreCase
                        ? char.ToLower(str[strIdx]) != char.ToLower(beginning[strIdx])
                        : str[strIdx] != beginning[strIdx])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns true if this instance begins with the specified value. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginning">Character which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWith(this string str, char beginning)
        {
            if (str.Length != 0 && str[0] == beginning)
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if this instance begins with the specified value. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charType">CharacterType which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWith(this string str, CharType charType)
        {
            if (str.Length != 0 && str[str.Length - 1].IsCharOfType(charType))
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if this instance begins with any of the specified strings. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginnings">Strings which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWithAny(this string str, IEnumerable<String> beginnings)
        {
            if (str.Length == 0)
                return false;
            return beginnings.Any(str.StartsWith);
        }

        /// <summary>
        /// Returns true if this instance begins with any of the specified chars. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginnings">Characters which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWithAny(this string str, IEnumerable<char> beginnings)
        {
            if (str.Length == 0)
                return false;
            return beginnings.Any(chr => str[0] == chr);
        }

        /// <summary>
        /// Returns true if this instance begins with any of the specified chars. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="beginnings">Characters which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWithAny(this string str, params char[] beginnings)
        {
            if (str.Length == 0)
                return false;
            return beginnings.Any(chr => str[0] == chr);
        }

        /// <summary>
        /// Returns true if this instance begins with a char of any of the specified CharTypes. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charTypes">CharTypes which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWithAny(this string str, IEnumerable<CharType> charTypes)
        {
            if (str.Length == 0)
                return false;
            return charTypes.Any(ctc => str[str.Length - 1].IsCharOfType(ctc));
        }

        /// <summary>
        /// Returns true if this instance begins with a char of any of the specified CharTypes. Returns false if the value to check against is longer than the string to check itself. Not case-sensitve.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charTypes">CharTypes which the beginning is compared to.</param>
        /// <returns></returns>
        public static bool StartsWithAny(this string str, params CharType[] charTypes)
        {
            if (str.Length == 0)
                return false;
            return charTypes.Any(ctc => str[str.Length - 1].IsCharOfType(ctc));
        }

        #endregion

        #region Searching

        /// <summary>
        /// Returns true if the string contains the specified character.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="c">Value to search for.</param>
        /// <returns></returns>
        public static bool Contains(this string str, char c)
        {
            return str.IndexOf(c) != -1;
        }

        /// <summary>
        /// Returns true if the string contains a character of the specified CharType.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">Value to search for.</param>
        /// <returns></returns>
        public static bool Contains(this string str, CharType charType)
        {
            return str.IndexOf(charType) != -1;
        }

        /// <summary>
        /// Returns true if the string contains the string in the specified StringBuilder.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">Value to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static bool Contains(this string str, StringBuilder searchString, bool ignoreCase = true)
        {
            return str.IndexOf(searchString, ignoreCase) != -1;
        }

        /// <summary>
        /// Returns true if the string contains any of the specified characters.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="c">Chars to search for.</param>
        /// <returns></returns>
        public static bool ContainsAny(this string str, params char[] c)
        {
            return str.IndexOfAny(c) != -1;
        }

        /// <summary>
        /// Returns true if the string contains a character of any of the specified CharTypes.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charTypes">CharTypes to search for.</param>
        /// <returns></returns>
        public static bool ContainsAny(this string str, params CharType[] charTypes)
        {
            return str.IndexOfAny(charTypes) != -1;
        }

        #endregion Searching

        #region Counting

        /// <summary>
        /// Returns the number of occurences of searchChar. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <returns></returns>
        public static int Count(this string str, char searchChar)
        {
            return str.Count(c => c == searchChar);
        }

        /// <summary>
        /// Returns the number of occurences of any of the searchChars. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <returns></returns>
        public static int Count(this string str, params char[] searchChars)
        {
            return str.Count(c => searchChars.Any(cs => c == cs));
        }

        /// <summary>
        /// Returns the number of occurences of chars of CharType.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <returns></returns>
        public static int Count(this string str, CharType charType)
        {
            return str.Count(c => c.IsCharOfType(charType));
        }

        /// <summary>
        /// Returns the number of occurences of chars of any of the given CharTypes.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charTypes">CharTypes to search for.</param>
        /// <returns></returns>
        public static int Count(this string str, params CharType[] charTypes)
        {
            return str.Count(c => charTypes.Any(charType => c.IsCharOfType(charType)));
        }

        /// <summary>
        /// Returns the number of occurences of searchString.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">String to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int Count(this string str, string searchString, bool ignoreCase = true)
        {
            if (searchString.Length > str.Length || searchString.Length == 0 || str.Length == 0)
                return 0;
            int count = 0;
            if (ignoreCase)
            {
                searchString = searchString.ToLowerInvariant();
                str = str.ToLowerInvariant();
            }

            for (int strIdx = 0; strIdx < str.Length - searchString.Length; strIdx++)
            {
                if (str[strIdx] != searchString[0])
                    continue;
                for (int searchIdx = 1;; searchIdx++)
                {
                    // searched the whole search string without breaking? => found
                    if (searchIdx == searchString.Length)
                    {
                        count++;
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
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">StringBuilder to search for.</param>
        /// <param name="ignoreCase">Set to true to ignore case.</param>
        /// <returns></returns>
        public static int Count(this string str, StringBuilder searchString, bool ignoreCase = true)
        {
            if (searchString.Length > str.Length || searchString.Length == 0 || str.Length == 0)
                return 0;
            int count = 0;
            if (ignoreCase)
                str = str.ToLowerInvariant();
            for (int strIdx = 0; strIdx < str.Length - searchString.Length; strIdx++)
            {
                if (ignoreCase)
                {
                    if (str[strIdx] != char.ToLowerInvariant(searchString[0]))
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
                        if (str[strIdx + searchIdx] != char.ToLowerInvariant(searchString[searchIdx]))
                            break;
                    }
                    else if (str[strIdx + searchIdx] != searchString[searchIdx])
                        break;
                }
            }
            return count;
        }

        /// <summary>
        /// Counts characters starting at 'startIndex' while the given predicate is satisfied. It will stop
        /// as soon as it hits a character which does not satisfy the predicate.
        /// </summary>
        /// <param name="str">The string to count in.</param>
        /// <param name="predicate">The predicate which the characters to count need to satisfy.</param>
        /// <param name="startIndex">The index to start counting at.</param>
        /// <returns></returns>
        public static int CountWhile(this string str, Func<char, bool> predicate, int startIndex = 0)
        {
            int ctr = 0;
            for (int i = startIndex; i < str.Length; i++)
            {
                if (predicate(str[i]))
                    ctr++;
                else
                    return ctr;
            }
            return ctr;
        }

        /// <summary>
        /// Counts characters starting at 'startIndex' while the characters are of the given CharType. It will stop
        /// as soon as it hits a character which is not of the specified CharType.
        /// </summary>
        /// <param name="str">The string to count in.</param>
        /// <param name="ct">The predicate which the characters to count need to satisfy.</param>
        /// <param name="startIndex">The index to start counting at.</param>
        /// <returns></returns>
        public static int CountWhile(this string str, CharType ct, int startIndex = 0)
        {
            int ctr = 0;
            for (int i = startIndex; i < str.Length; i++)
            {
                if (str[i].IsCharOfType(ct))
                    ctr++;
                else
                    return ctr;
            }
            return ctr;
        }

        #endregion Counting

        #endregion String Information

        #region String Splitting

        /// <summary>
        /// Takes characters from string starting at the specified index for as long as the characters satisfy
        /// the provided predicate.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="predicate"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string TakeWhile(this string str, Func<char, bool> predicate, int startIndex)
        {
            int i = startIndex;
            for (; i < str.Length; i++)
                if (!predicate(str[i]))
                    break;
            return str.Substring(startIndex, i - startIndex);
        }

        /// <summary>
        /// Returns the substring starting at 'startIndex' going for 'length'. Unlike the Substring method, this method does not throw
        /// any exceptions. Should startIndex + length exceed the string's length, this method will return the substring starting at
        /// 'startIndex' to the end of the string. In any other case where Substring would throw an exception, this method will return
        /// the empty string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubstringSafe(this string str, int startIndex, int length)
        {
            if (startIndex + length < 0)
                return string.Empty;
            if (startIndex > str.Length)
                return string.Empty;
            if (startIndex + length > str.Length)
                return str.Substring(startIndex);
            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// Returns the substring starting at 'startIndex' going for 'length'. Unlike the Substring method, this method does not throw
        /// any exceptions. Should startIndex + length exceed the string's length, this method will return the substring starting at
        /// 'startIndex' to the end of the string. In any other case where Substring would throw an exception, this method will return
        /// the empty string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string SubstringSafe(this string str, int startIndex)
        {
            if (startIndex < 0)
                return string.Empty;
            if (startIndex > str.Length)
                return string.Empty;
            return str.Substring(startIndex);
        }

        /// <summary>
        /// Returns the substring between the Nth and the Mth occurence of the search char. If there are less than M occurrences
        /// in the string it returns the unmodified string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nthOcc">Nth occurence.</param>
        /// <param name="mthOcc">Mth occurence.</param>
        /// <param name="searchChar">The char to search for.</param>
        /// <returns></returns>
        /// <exception cref="CopeException">The first occurrence must obviously be a smaller number than the second occurrence!</exception>
        public static string SubstringBetweenOccurrencs(this string str, int nthOcc, int mthOcc, char searchChar)
        {
            if (nthOcc >= mthOcc)
                throw new CopeException(
                    "The first occurrence must obviously be a smaller number than the second occurrence!");
            if (str.Length < mthOcc)
                return str;
            int numOccurences = 0;
            int split1 = -1, split2 = -1;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == searchChar)
                {
                    numOccurences++;
                    if (numOccurences == nthOcc)
                    {
                        split1 = i;
                        continue;
                    }
                    if (numOccurences == mthOcc)
                    {
                        split2 = i;
                        break;
                    }
                }
            }
            if (split1 < 0 || split2 < 0)
                return str;
            return str.Substring(split1 + 1, split2 - split1 - 1);
        }

        /// <summary>
        /// Performs splitting of a string just as the original split method. In contrast to the original, this method
        /// takes the StringSplitOptions as a first parameter which allows for the split-characters to be passed in
        /// 'params'-style.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="options"></param>
        /// <param name="cs"></param>
        /// <returns></returns>
        public static string[] Split(this string str, StringSplitOptions options, params char[] cs)
        {
            return str.Split(cs, options);
        }

        /// <summary>
        /// Performs splitting of a string just as the original split method. In contrast to the original, this method
        /// takes the StringSplitOptions as a first parameter which allows for the split-strings to be passed in
        /// 'params'-style.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="options"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] Split(this string str, StringSplitOptions options, params string[] s)
        {
            return str.Split(s, options);
        }

        /// <summary>
        /// Performs splitting of a string just as the original split method. In contrast to the original, this method
        /// allows for the split-strings to be passed in 'params'-style.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] Split(this string str, params string[] s)
        {
            return str.Split(s, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits this string into chunks of a given length.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lengthOfParts">Length of the parts to split the string into.</param>
        /// <returns></returns>
        public static string[] Split(this string str, int lengthOfParts)
        {
            int length = (str.Length + 1) / lengthOfParts;
            var parts = new string[length];
            int idx = 0;
            for (int i = 0; i < length; i++)
            {
                parts[i] = str.Substring(idx, lengthOfParts);
                idx += lengthOfParts;
            }
            return parts;
        }

        #region SplitAt

        /// <summary>
        /// Splits a string into two parts using the last occurence of the given splitter. The splitter will not be part of any of the resulting strings.
        /// Returns true if the splitter was part of the string, otherwise false. If the splitter did not occur in the string, firstPath will hold the input string
        /// and secondPart will hold the empty string.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="splitter">The splitter string.</param>
        /// <param name="firstPart">Part of the string before the splitter.</param>
        /// <param name="secondPart">Part of the string after the splitter.</param>
        /// <returns></returns>
        public static bool SplitAtLast(this string str, string splitter, out string firstPart, out string secondPart)
        {
            int idx = str.LastIndexOf(splitter);
            if (idx < 0)
            {
                firstPart = str;
                secondPart = string.Empty;
                return false;
            }
            firstPart = SplitAt(str, idx, splitter.Length, out secondPart);
            return true;
        }

        /// <summary>
        /// Splits a string into two parts using the last occurence of the given splitter. The splitter will not be part of any of the resulting strings.
        /// Returns true if the splitter was part of the string, otherwise false. If the splitter did not occur in the string, firstPath will hold the input string
        /// and secondPart will hold the empty string.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="splitter">The splitter character.</param>
        /// <param name="firstPart">Part of the string before the splitter.</param>
        /// <param name="secondPart">Part of the string after the splitter.</param>
        /// <returns></returns>
        public static bool SplitAtLast(this string str, char splitter, out string firstPart, out string secondPart)
        {
            int idx = str.LastIndexOf(splitter);
            if (idx < 0)
            {
                firstPart = str;
                secondPart = string.Empty;
                return false;
            }
            firstPart = SplitAt(str, idx, 1, out secondPart);
            return true;
        }

        /// <summary>
        /// Splits a string into two parts using the last occurence of the given splitter. The splitter will not be part of any of the resulting strings.
        /// Returns true if the splitter was part of the string, otherwise false. If the splitter did not occur in the string, firstPath will hold the input string
        /// and secondPart will hold the empty string.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="splitter">The splitter CharType.</param>
        /// <param name="firstPart">Part of the string before the splitter.</param>
        /// <param name="secondPart">Part of the string after the splitter.</param>
        /// <returns></returns>
        public static bool SplitAtLast(this string str, CharType splitter, out string firstPart, out string secondPart)
        {
            int idx = str.LastIndexOf(splitter);
            if (idx < 0)
            {
                firstPart = str;
                secondPart = string.Empty;
                return false;
            }
            firstPart = SplitAt(str, idx, 1, out secondPart);
            return true;
        }

        /// <summary>
        /// Splits a string into two parts using the first occurence of the given splitter. The splitter will not be part of any of the resulting strings.
        /// Returns true if the splitter was part of the string, otherwise false. If the splitter did not occur in the string, firstPath will hold the input string
        /// and secondPart will hold the empty string.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="splitter">The splitter string.</param>
        /// <param name="firstPart">Part of the string before the splitter.</param>
        /// <param name="secondPart">Part of the string after the splitter.</param>
        /// <returns></returns>
        public static bool SplitAtFirst(this string str, string splitter, out string firstPart, out string secondPart)
        {
            int idx = str.LastIndexOf(splitter);
            if (idx < 0)
            {
                firstPart = str;
                secondPart = string.Empty;
                return false;
            }
            firstPart = SplitAt(str, idx, splitter.Length, out secondPart);
            return true;
        }

        /// <summary>
        /// Splits a string into two parts using the first occurence of the given splitter. The splitter will not be part of any of the resulting strings.
        /// Returns true if the splitter was part of the string, otherwise false. If the splitter did not occur in the string, firstPath will hold the input string
        /// and secondPart will hold the empty string.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="splitter">The splitter character.</param>
        /// <param name="firstPart">Part of the string before the splitter.</param>
        /// <param name="secondPart">Part of the string after the splitter.</param>
        /// <returns></returns>
        public static bool SplitAtFirst(this string str, char splitter, out string firstPart, out string secondPart)
        {
            int idx = str.LastIndexOf(splitter);
            if (idx < 0)
            {
                firstPart = str;
                secondPart = string.Empty;
                return false;
            }
            firstPart = SplitAt(str, idx, 1, out secondPart);
            return true;
        }

        /// <summary>
        /// Splits a string into two parts using the first occurence of the given splitter. The splitter will not be part of any of the resulting strings.
        /// Returns true if the splitter was part of the string, otherwise false. If the splitter did not occur in the string, firstPath will hold the input string
        /// and secondPart will hold the empty string.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="splitter">The splitter CharType.</param>
        /// <param name="firstPart">Part of the string before the splitter.</param>
        /// <param name="secondPart">Part of the string after the splitter.</param>
        /// <returns></returns>
        public static bool SplitAtFirst(this string str, CharType splitter, out string firstPart, out string secondPart)
        {
            int idx = str.LastIndexOf(splitter);
            if (idx < 0)
            {
                firstPart = str;
                secondPart = string.Empty;
                return false;
            }
            firstPart = SplitAt(str, idx, 1, out secondPart);
            return true;
        }

        /// <summary>
        /// Splits a string into two parts. The split index determines where the first part of the string ends, the splitter length determines how many characters
        /// to ignore before the second part of the string begins. If split index is great than the maximum index, it will return the original string and the out-parameter
        /// will be the empty string.
        /// Return the first part of the split, the second part is available as an out-parameter.
        /// </summary>
        /// <param name="str">The string to split.</param>
        /// <param name="splitIdx">The index of the splitter, this is where the first part ends.</param>
        /// <param name="splitterLength">The length of the splitter, add this to the split index to get where the second part starts.</param>
        /// <param name="secondPart">Part of the string after the splitter.</param>
        /// <returns>Part of the string before the splitter.</returns>
        public static string SplitAt(this string str, int splitIdx, int splitterLength, out string secondPart)
        {
            secondPart = str.SubstringSafe(splitIdx + splitterLength);
            return str.Substring(0, splitIdx);
        }

        #endregion

        #region SubstringAfterLast

        /// <summary>
        /// Returns the string after the last occurence of searchString or the string itself if searchString couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">String to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringAfterLast(this string str, string searchString, bool including = false)
        {
            if (searchString.Length == 0 || str.Length == 0)
                return str;
            int removeCount = str.LastIndexOf(searchString);
            if (removeCount == -1)
                return str;
            return including ? str.Remove(0, removeCount) : str.Remove(0, removeCount + searchString.Length);
        }

        /// <summary>
        /// Returns the string after the last occurence of searchChar or the string itself if searchChar couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchString.</param>
        /// <returns></returns>
        public static string SubstringAfterLast(this string str, char searchChar, bool including = false)
        {
            if (str.Length == 0)
                return str;
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
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringAfterLast(this string str, char[] searchChars, bool including = false)
        {
            if (str.Length == 0)
                return str;
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
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringAfterLast(this string str, CharType charType, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int removeCount = str.LastIndexOf(charType);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount);
            return str.Remove(0, removeCount + 1);
        }

        #endregion SubstringAfterLast

        #region SubstringAfterFirst

        /// <summary>
        /// Returns the string after the first occurence of searchString or the string itself if searchString couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">String to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchString.</param>
        /// <returns></returns>
        public static string SubstringAfterFirst(this string str, string searchString, bool including = false)
        {
            if (searchString.Length == 0 || str.Length == 0)
                return str;
            int removeCount = str.IndexOf(searchString);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount);
            return str.Remove(0, removeCount + searchString.Length);
        }

        /// <summary>
        /// Returns the string after the first occurence of searchChar or the string itself if searchChar couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringAfterFirst(this string str, char searchChar, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int removeCount = str.IndexOf(searchChar);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount);
            return str.Remove(0, removeCount + 1);
        }

        /// <summary>
        /// Returns the string after the first occurence of any of the searchChars or the string itself if no searchChar could be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringAfterFirst(this string str, char[] searchChars, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int removeCount = str.IndexOfAny(searchChars);
            if (removeCount == -1)
                return str;
            if (including)
                return str.Remove(0, removeCount);
            return str.Remove(0, removeCount + 1);
        }

        /// <summary>
        /// Returns the string after the first occurence of searchChar or the string itself if searchChar couldn't be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringAfterFirst(this string str, CharType charType, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int removecount = str.IndexOf(charType);
            if (removecount == -1)
                return str;
            if (including)
                return str.Remove(0, removecount);
            return str.Remove(0, removecount + 1);
        }

        #endregion SubstringAfterFirst

        #region SubstringBeforeLast

        /// <summary>
        /// Returns the string before the last occurence of searchString or the string itself if searchString couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">String to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchString.</param>
        /// <returns></returns>
        public static string SubstringBeforeLast(this string str, string searchString, bool including = false)
        {
            if (searchString.Length == 0 || str.Length == 0)
                return str;
            int newEnd = str.LastIndexOf(searchString);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + str.Length);
            return str.Remove(newEnd);
        }

        /// <summary>
        /// Returns the string before the last occurence of searchChar or the string itself if searchChar couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringBeforeLast(this string str, char searchChar, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int newEnd = str.LastIndexOf(searchChar);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1);
            return str.Remove(newEnd);
        }

        /// <summary>
        /// Returns the string before the last occurence of any of the searchChars or the string itself if no searchChar could be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringBeforeLast(this string str, char[] searchChars, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int newEnd = str.LastIndexOfAny(searchChars);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1);
            return str.Remove(newEnd);
        }

        /// <summary>
        /// Returns the string before the last occurence of searchChar or the string itself if searchChar couldn't be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringBeforeLast(this string str, CharType charType, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int newEnd = str.LastIndexOf(charType);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1);
            return str.Remove(newEnd);
        }

        #endregion SubstringBeforeLast

        #region SubstringBeforeFirst

        /// <summary>
        /// Returns the string before the first occurence of searchString or the string itself if searchString couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchString">String to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchString.</param>
        /// <returns></returns>
        public static string SubstringBeforeFirst(this string str, string searchString, bool including = false)
        {
            if (searchString.Length == 0 || str.Length == 0)
                return str;
            int newEnd = str.IndexOf(searchString);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + str.Length);
            return str.Remove(newEnd);
        }

        /// <summary>
        /// Returns the string before the first occurence of searchChar or the string itself if searchChar couldn't be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChar">Char to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringBeforeFirst(this string str, char searchChar, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int newEnd = str.IndexOf(searchChar);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1);
            return str.Remove(newEnd);
        }

        /// <summary>
        /// Returns the string before the first occurence of any of the searchChars or the string itself if no searchChar could be found. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="searchChars">Chars to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringBeforeFirst(this string str, char[] searchChars, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int newEnd = str.IndexOfAny(searchChars);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1);
            return str.Remove(newEnd, str.Length - newEnd);
        }

        /// <summary>
        /// Returns the string before the first occurence of searchChar or the string itself if searchChar couldn't be found.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">CharType  to search for.</param>
        /// <param name="including">Set to true to return the string including the found occurence of the searchChar.</param>
        /// <returns></returns>
        public static string SubstringBeforeFirst(this string str, CharType charType, bool including = false)
        {
            if (str.Length == 0)
                return str;
            int newEnd = str.IndexOf(charType);
            if (newEnd == -1)
                return str;
            if (including)
                return str.Remove(newEnd + 1);
            return str.Remove(newEnd, str.Length - newEnd);
        }

        #endregion SubstringBeforeFirst

        #endregion String Splitting

        #region String Modification

        /// <summary>
        /// Replaces all occurences of oldChars with newChar. Case-sensitive.
        /// </summary>
        /// <param name="str">The string to operate on.</param>
        /// <param name="oldChars">The chars to replace.</param>
        /// <param name="newChar">The new char.</param>
        /// <returns></returns>
        public static string Replace(this string str, char newChar, params char[] oldChars)
        {
            char[] chrTmp = str.ToCharArray();
            int chrTmpLength = chrTmp.Length;
            for (int strIdx = 0; strIdx < chrTmpLength; strIdx++)
            {
                foreach (char c in oldChars)
                {
                    if (chrTmp[strIdx].Equals(c))
                    {
                        chrTmp[strIdx] = newChar;
                        break;
                    }
                }
            }
            return new string(chrTmp);
        }

        /// <summary>
        /// Replaces all occurences of oldChars with newChars (so if oldChars[0] is found it's replaced with newChars[0]).
        /// If oldChars is bigger than newChars for every [i] > newChars.Length newChars[length-1] is used. Case-sensitive.
        /// </summary>
        /// <param name="str">The string to operate on.</param>
        /// <param name="oldChars">The chars to replace.</param>
        /// <param name="newChars">The new chars.</param>
        /// <returns></returns>
        public static string Replace(this string str, char[] oldChars, char[] newChars)
        {
            char[] chrTmp = str.ToCharArray();
            int chrTmpLength = chrTmp.Length;
            int oldCharsLength = oldChars.Length;
            int newCharsLength = newChars.Length;
            for (int strIdx = 0; strIdx < chrTmpLength; strIdx++)
            {
                for (int oldIdx = 0; oldIdx < oldCharsLength; oldIdx++)
                {
                    if (chrTmp[strIdx].Equals(oldChars[oldIdx]))
                    {
                        if (oldIdx >= newCharsLength)
                            chrTmp[strIdx] = newChars[newCharsLength - 1];
                        else
                            chrTmp[strIdx] = newChars[oldIdx];
                        break;
                    }
                }
            }
            return new string(chrTmp);
        }

        /// <summary>
        /// Replaces all occurences of Chars of a special CharType  with newChar.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">CharType  to replace.</param>
        /// <param name="newChar">Char to insert.</param>
        public static string Replace(this string str, CharType charType, char newChar)
        {
            char[] chrTmp = str.ToCharArray();
            int chrTmpLength = chrTmp.Length;
            for (int strIdx = 0; strIdx < chrTmpLength; strIdx++)
            {
                if (str[strIdx].IsCharOfType(charType))
                {
                    chrTmp[strIdx] = newChar;
                }
            }
            return new string(chrTmp);
        }

        /// <summary>
        /// Replaces all occurences of Chars of a any of the specified CharTypes with newChar.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charTypes">CharTypes to replace.</param>
        /// <param name="newChar">Char to insert.</param>
        public static string Replace(this string str, char newChar, params CharType[] charTypes)
        {
            char[] chrTmp = str.ToCharArray();
            int chrTmpLength = chrTmp.Length;
            for (int strIdx = 0; strIdx < chrTmpLength; strIdx++)
            {
                foreach (CharType charType in charTypes)
                {
                    if (str[strIdx].IsCharOfType(charType))
                    {
                        chrTmp[strIdx] = newChar;
                        break;
                    }
                }
            }
            return new string(chrTmp);
        }

        /// <summary>
        /// Removes all chars with the specified value c from str. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="c">Char to remove.</param>
        /// <returns></returns>
        public static string RemoveAll(this string str, char c)
        {
            int strLength = str.Length;
            var chrTmp = new char[strLength];
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx] != c)
                    continue;
                chrTmp[newIdx] = str[strIdx];
                newIdx++;
            }
            return new string(chrTmp, 0, newIdx);
        }

        /// <summary>
        /// Removes all chars with any of the specified values c from str. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="chars">Chars to remove.</param>
        /// <returns></returns>
        public static string RemoveAll(this string str, params char[] chars)
        {
            int strLength = str.Length;
            var chrTmp = new char[strLength];
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
                chrTmp[newIdx] = str[strIdx];
                newIdx++;
            }
            return new string(chrTmp, 0, newIdx);
        }

        /// <summary>
        /// Removes all chars of the specified CharType  from str.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charType">CharType  to remove.</param>
        /// <returns></returns>
        public static string RemoveAll(this string str, CharType charType)
        {
            int strLength = str.Length;
            var chrTmp = new char[strLength];
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (!str[strIdx].IsCharOfType(charType))
                    continue;
                chrTmp[newIdx] = str[strIdx];
                newIdx++;
            }
            return new string(chrTmp, 0, newIdx);
        }

        /// <summary>
        /// Removes all chars of the specified CharTypes from str.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charTypes">CharTypes to remove.</param>
        /// <returns></returns>
        public static string RemoveAll(this string str, params CharType[] charTypes)
        {
            int strLength = str.Length;
            var chrTmp = new char[strLength];
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
                chrTmp[newIdx] = str[strIdx];
                newIdx++;
            }
            return new string(chrTmp, 0, newIdx);
        }

        /// <summary>
        /// Removes everything but the specified value. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="c">Char to keep.</param>
        /// <returns></returns>
        public static string RemoveAllBut(this string str, char c)
        {
            var chrTmp = new char[str.Length];
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx] == c)
                {
                    chrTmp[newIdx] = str[strIdx];
                    newIdx++;
                }
            }
            return new string(chrTmp, 0, newIdx);
        }

        /// <summary>
        /// Removes everything but the specified value. Case-sensitive.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="chars">Chars to keep.</param>
        /// <returns></returns>
        public static string RemoveAllBut(this string str, params char[] chars)
        {
            var chrTmp = new char[str.Length];
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                foreach (char cs in chars)
                {
                    if (str[strIdx] == cs)
                    {
                        chrTmp[newIdx] = str[strIdx];
                        newIdx++;
                        break;
                    }
                }
            }
            return new string(chrTmp, 0, newIdx);
        }

        /// <summary>
        /// Removes everything but the specified value.
        /// </summary>
        /// <param name="str">String to operate on</param>
        /// <param name="charType">CharType  to keep.</param>
        /// <returns></returns>
        public static string RemoveAllBut(this string str, CharType charType)
        {
            var chrTmp = new char[str.Length];
            int strLength = str.Length;
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                if (str[strIdx].IsCharOfType(charType))
                {
                    chrTmp[newIdx] = str[strIdx];
                    newIdx++;
                }
            }
            return new string(chrTmp, 0, newIdx);
        }

        /// <summary>
        /// Removes everything but the specified value.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <param name="charTypes">CharTypes to keep.</param>
        /// <returns></returns>
        public static string RemoveAllBut(this string str, params CharType[] charTypes)
        {
            int strLength = str.Length;
            var chrTmp = new char[strLength];
            int newIdx = 0;
            for (int strIdx = 0; strIdx < strLength; strIdx++)
            {
                foreach (CharType charType in charTypes)
                {
                    if (str[strIdx].IsCharOfType(charType))
                    {
                        chrTmp[newIdx] = str[strIdx];
                        newIdx++;
                        break;
                    }
                }
            }
            return new string(chrTmp, 0, newIdx);
        }

        /// <summary>
        /// Removes the last 'count' characters from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RemoveLast(this string str, int count)
        {
            return str.Remove(str.Length - count);
        }

        /// <summary>
        /// Removes the first 'count' characters from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RemoveFirst(this string str, int count)
        {
            return str.Remove(0, count);
        }

        /// <summary>
        /// Reverses a string.
        /// </summary>
        /// <param name="str">String to operate on.</param>
        /// <returns></returns>
        public static string Reverse(this string str)
        {
            int strLength = str.Length;
            var dst = new char[strLength];
            for (int strIdx = 1; strIdx <= strLength; strIdx++)
            {
                dst[strLength - strIdx] = str[strIdx - 1];
            }
            return new string(dst);
        }

        /// <summary>
        /// Inserts the given string at the specified index 'count'-times.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str2">The string to be inserted.</param>
        /// <param name="startIndex">The index where to insert.</param>
        /// <param name="count">The number of times to insert the string</param>
        /// <returns></returns>
        public static string Insert(this string str, string str2, int startIndex = 0, int count = 1)
        {
            var strb = new StringBuilder(str);
            strb.Insert(startIndex, str2, count);
            return strb.ToString();
        }

        /// <summary>
        /// Appends the specified string 'count'-times.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str2">The string to be appended.</param>
        /// <param name="count">The number of times it shall be appended.</param>
        /// <returns></returns>
        public static string Append(this string str, string str2, int count = 1)
        {
            return str.Insert(str2, str.Length, count);
        }

        /// <summary>
        /// Prepends the specified string 'count'-times.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str2">The string to be prepended.</param>
        /// <param name="count">The number of times it shall be prepended.</param>
        /// <returns></returns>
        public static string Prepend(this string str, string str2, int count = 1)
        {
            return str.Insert(str2, 0, count);
        }

        /// <summary>
        /// Limits the length of a string returning either the string if its length is below the limit or a substring of the original string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Limit(this string str, int maxLength)
        {
            if (str.Length <= maxLength)
                return str;
            return str.Substring(0, maxLength);
        }

        /// <summary>
        /// Removes a prefix from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="prefix">The prefix to remove from the string.</param>
        /// <returns></returns>
        public static string RemovePrefix(this string str, string prefix)
        {
            return str.Remove(0, prefix.Length);
        }

        /// <summary>
        /// Removes the first occurence of any of the given prefixes from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="prefixes">The prefixes to check for (and remove the first found).</param>
        /// <returns></returns>
        public static string RemovePrefix(this string str, params string[] prefixes)
        {
            foreach (string p in prefixes)
                if (str.StartsWith(p))
                    return str.Remove(0, p.Length);
            return str;
        }

        /// <summary>
        /// Removes the first occurence of any of the given prefixes from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="prefixes">The prefixes to check for (and remove the first found).</param>
        /// <returns></returns>
        public static string RemovePrefix(this string str, IEnumerable<string> prefixes)
        {
            foreach (string p in prefixes)
                if (str.StartsWith(p))
                    return str.Remove(0, p.Length);
            return str;
        }

        #endregion String Modification
    }
}