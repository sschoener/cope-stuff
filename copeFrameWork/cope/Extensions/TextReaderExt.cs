#region

using System.IO;
using System.Text;

#endregion

namespace cope.Extensions
{
    public static class TextReaderExt
    {
        /// <summary>
        /// Reads all the leading whitespaces and returns true if there's anything left at all.
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static bool ReadWhitespaces(this TextReader tr)
        {
            while (true)
            {
                var c = (char) tr.Peek();
                if (char.IsWhiteSpace(c))
                {
                    if (tr.Read() == -1)
                        return false;
                }
                else
                    return true;
            }
        }

        /// <summary>
        /// Reads until a specified character and returns the substring before the occurence of the character.
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="c"></param>
        /// <param name="including"></param>
        /// <returns></returns>
        public static string ReadUntil(this TextReader tr, char c, bool including = false)
        {
            var sb = new StringBuilder();
            do
            {
                int peeked = tr.Peek();
                if (peeked < 0)
                    break;

                var current = (char) peeked;
                if (current == c)
                {
                    if (including)
                        sb.Append(c);
                    break;
                }
                sb.Append(current);
            } while (tr.Read() != -1);
            return sb.ToString();
        }

        /// <summary>
        /// Reads until the first occurence of any character specified in 'limits'. Returns the character it hit on.
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="read">The string read until it hit the delimiter.</param>
        /// <param name="limits"></param>
        /// <returns></returns>
        public static char ReadUntil(this TextReader tr, out string read, params char[] limits)
        {
            return ReadUntil(tr, out read, limits);
        }

        /// <summary>
        /// Reads until the first occurence of any character specified in 'limits'. Returns the character it hit on.
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="including"></param>
        /// <param name="read">The string read until it hit the delimiter.</param>
        /// <param name="limits"></param>
        /// <returns></returns>
        public static char ReadUntil(this TextReader tr, bool including, out string read, params char[] limits)
        {
            var sb = new StringBuilder();
            char retval = '\0';
            do
            {
                var current = (char) tr.Peek();
                if (limits.ContainsComparable(current))
                {
                    if (including)
                        sb.Append(current);
                    retval = current;
                    break;
                }
                sb.Append(current);
            } while (tr.Read() == -1);
            read = sb.ToString();
            return retval;
        }

        /// <summary>
        /// Reads and returns a literal from the specified TextReader.
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static string ReadLiteral(this TextReader tr)
        {
            var sb = new StringBuilder();
            while (true)
            {
                var c = (char) tr.Peek();
                if (char.IsLetter(c))
                {
                    sb.Append(c);
                    if (tr.Read() == -1)
                        return sb.ToString();
                }
                else
                {
                    if (sb.Length == 0)
                        return null;
                    return sb.ToString();
                }
            }
        }

        /// <summary>
        /// Returns true when the StreamReader has reached the end of the file.
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static bool IsAtEnd(this TextReader tr)
        {
            return tr.Peek() == -1;
        }
    }
}