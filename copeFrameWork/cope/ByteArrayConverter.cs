#region

using System;
using System.Text;

#endregion

namespace cope
{
	/// <summary>
	/// Static class offering extensions for conversions involving byte[];
	/// </summary>
	public static class ByteArrayConverter
	{
		private static readonly ASCIIEncoding s_ascii = new ASCIIEncoding ();
		private static readonly UnicodeEncoding s_unicode = new UnicodeEncoding ();

        #region ToByte[]

		/// <summary>
		/// Converts a string to byte[].
		/// </summary>
		/// <param name="str">String to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII enconding instead of Unicode.</param>
		/// <param name="index">Index of the first char in the string to convert (zero-based).</param>
		/// <param name="length">Number of chars to convert.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this string str, bool asciiEncoding, int index, int length)
		{
			if (asciiEncoding) {
				var tmp = new byte[length];
				s_ascii.GetBytes (str, index, length, tmp, 0);
				return tmp;
			} else {
				var tmp = new byte[length * 2];
				s_unicode.GetBytes (str, index, length, tmp, 0);
				return tmp;
			}
		}

		/// <summary>
		/// Converts a string to byte[].
		/// </summary>
		/// <param name="str">String to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII enconding instead of Unicode.</param>
		/// <param name="length">Number of chars to convert.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this string str, bool asciiEncoding, int length)
		{
			return ToByteArray (str, asciiEncoding, 0, length);
		}

		/// <summary>
		/// Converts a string to byte[].
		/// </summary>
		/// <param name="str">String to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII enconding instead of Unicode.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this string str, bool asciiEncoding = false)
		{
			return ToByteArray (str, asciiEncoding, 0, str.Length);
		}

		/// <summary>
		/// Converts a StringBuilder to byte[].
		/// </summary>
		/// <param name="str">String to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <param name="index">Index of the first char in the string to convert (zero-based).</param>
		/// <param name="length">Number of chars to convert.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this StringBuilder str, bool asciiEncoding, int index, int length)
		{
			if (asciiEncoding)
				return s_ascii.GetBytes (str.ToString ().ToCharArray (), index, length);
			return s_unicode.GetBytes (str.ToString ().ToCharArray (), index, length);
		}

		/// <summary>
		/// Converts a StringBuilder to byte[].
		/// </summary>
		/// <param name="str">String to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <param name="length">Number of chars to convert.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this StringBuilder str, bool asciiEncoding, int length)
		{
			return ToByteArray (str, asciiEncoding, 0, length);
		}

		/// <summary>
		/// Converts a StringBuilder to byte[].
		/// </summary>
		/// <param name="str">String to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this StringBuilder str, bool asciiEncoding = false)
		{
			return ToByteArray (str, asciiEncoding, 0, str.Length);
		}

		/// <summary>
		/// Converts a char[] to byte[].
		/// </summary>
		/// <param name="ca">Char[] to convert.</param>
		/// <param name="index">Index in ca from which to start conversion.</param>
		/// <param name="length">Number of chars to convert.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this char[] ca, int index, int length)
		{
			var retval = new byte[length];
			for (int i = 0; i < ca.Length; i++)
				retval [i] = (byte)ca [i];
			return retval;
		}

		/// <summary>
		/// Converts a char[] to byte[].
		/// </summary>
		/// <param name="ca">Char[] to convert.</param>
		/// <param name="length">Number of chars to convert.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this char[] ca, int length)
		{
			return ca.ToByteArray (0, length);
		}

		/// <summary>
		/// Converts a char[] to byte[].
		/// </summary>
		/// <param name="ca">Char[] to convert.</param>
		/// <returns></returns>
		public static byte[] ToByteArray (this char[] ca)
		{
			return ca.ToByteArray (0, ca.Length);
		}

        #endregion ToByte[]

        #region ToString

		/// <summary>
		/// Converts a byte[] to a string.
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <param name="index">Index of the first byte in the array to convert (zero-based).</param>
		/// <param name="length">Number of bytes to convert.</param>
		/// <returns></returns>
		public static string ToString (this byte[] bytes, bool asciiEncoding, int index, int length)
		{
			if (asciiEncoding) {
				if (length >= bytes.Length)
					length = bytes.Length;
				return s_ascii.GetString (bytes, index, length);
			}
			return s_unicode.GetString (bytes, index, length);
		}

		/// <summary>
		/// Converts a byte[] to a string.
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <param name="length">Number of bytes to convert.</param>
		/// <returns></returns>
		public static string ToString (this byte[] bytes, bool asciiEncoding, int length)
		{
			return ToString (bytes, asciiEncoding, 0, length);
		}

		/// <summary>
		/// Converts a byte[] to a string.
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <returns></returns>
		public static string ToString (this byte[] bytes, bool asciiEncoding = false)
		{
			if (asciiEncoding)
				return s_ascii.GetString (bytes);
			return s_unicode.GetString (bytes);
		}

		/// <summary>
		/// Converts a byte[] to a StringBuilder.
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <param name="index">Index of the first byte in the array to convert (zero-based).</param>
		/// <param name="length">Number of bytes to convert.</param>
		/// <returns></returns>
		public static StringBuilder ToStringBuilder (this byte[] bytes, bool asciiEncoding, int index, int length)
		{
			int limit = index + length;
			var tmp = new StringBuilder ();
			if (asciiEncoding) {
				for (int i = index; i < limit; i++) {
					if (bytes [i] >= 0x20)
						tmp.Append ((char)bytes [i]);
					else
						tmp.Append ('\\' + bytes [i].ToString ());
				}
			} else {
				for (int i = index; i < limit; i++)
					tmp.Append (Convert.ToChar (bytes [i]));
			}
			return tmp;
		}

		/// <summary>
		/// Converts a byte[] to a StringBuilder.
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <param name="length">Number of bytes to convert.</param>
		/// <returns></returns>
		public static StringBuilder ToStringBuilder (this byte[] bytes, bool asciiEncoding, int length)
		{
			return ToStringBuilder (bytes, asciiEncoding, 0, length);
		}

		/// <summary>
		/// Converts a byte[] to a StringBuilder.
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <returns></returns>
		public static StringBuilder ToStringBuilder (this byte[] bytes, bool asciiEncoding)
		{
			return ToStringBuilder (bytes, asciiEncoding, 0, bytes.Length);
		}

		/// <summary>
		/// Converts a byte[] to a StringBuilder using Unicode encoding.
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <returns></returns>
		public static StringBuilder ToStringBuilder (this byte[] bytes)
		{
			return ToStringBuilder (bytes, false, 0, bytes.Length);
		}

        #endregion ToString

        #region ToChar[]

		/// <summary>
		/// Converts a byte[] to a char[].
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <param name="index">Index of the first byte in the array to convert (zero-based).</param>
		/// <param name="length">Number of bytes to convert.</param>
		/// <returns></returns>
		public static char[] ToCharArray (this byte[] bytes, bool asciiEncoding, int index, int length)
		{
			if (asciiEncoding)
				return s_ascii.GetChars (bytes, index, length);
			return s_unicode.GetChars (bytes, index, length);
		}

		/// <summary>
		/// Converts a byte[] to a char[].
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <param name="length">Number of bytes to convert.</param>
		/// <returns></returns>
		public static char[] ToCharArray (this byte[] bytes, bool asciiEncoding, int length)
		{
			if (asciiEncoding)
				return s_ascii.GetChars (bytes, 0, length);
			return s_unicode.GetChars (bytes, 0, length);
		}

		/// <summary>
		/// Converts a byte[] to a char[].
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <param name="asciiEncoding">Set to true to use ASCII encoding instead of Unicode.</param>
		/// <returns></returns>
		public static char[] ToCharArray (this byte[] bytes, bool asciiEncoding)
		{
			if (asciiEncoding)
				return s_ascii.GetChars (bytes);
			return s_unicode.GetChars (bytes);
		}

		/// <summary>
		/// Converts a byte[] to a char[].
		/// </summary>
		/// <param name="bytes">Byte[] to convert.</param>
		/// <returns></returns>
		public static char[] ToCharArray (this byte[] bytes)
		{
			return s_unicode.GetChars (bytes);
		}

        #endregion ToChar[]

        #region ToUInt32

		/// <summary>
		/// Returns the UInt32 represented by the first 4 bytes of this byte array.
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="startIndex"></param>
		/// <returns></returns>
		public static uint ToUInt32 (this byte[] bytes, int startIndex = 0)
		{
			return BitConverter.ToUInt32 (bytes, startIndex);
		}

        #endregion ToUInt32
	}
}