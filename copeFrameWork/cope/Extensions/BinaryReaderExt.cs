#region

using System;
using System.IO;

#endregion

namespace cope.Extensions
{
	public static class BinaryReaderExt
	{
		/// <summary>
		/// Buffer size for ReadUntil method.
		/// </summary>
		private const int BYTE_BUFFER_SIZE = 0x1000;

		/// <summary>
		/// Returns whether or not this BinaryReader has reached the end of the stream.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static bool IsAtEnd (this BinaryReader reader)
		{
			return reader.BaseStream.Position == reader.BaseStream.Length;
		}

		/// <summary>
		/// Reads bytes until the specified value is found.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="value">The value to stop at. The value itself is also read.</param>
		/// <returns></returns>
		public static byte[] ReadUntil (this BinaryReader reader, byte value)
		{
			int amount = 0;
			long baseAddress = reader.BaseStream.Position;
			var buffer = new byte[BYTE_BUFFER_SIZE];
			while (true) {
				int bytesRead = reader.Read (buffer, 0, BYTE_BUFFER_SIZE);
				if (bytesRead == 0) // end of stream
					break;
				bool done = false;
				for (int i = 0; i < bytesRead; i++) {
					if (buffer [i] == value) {
						done = true;
						amount += i + 1;
						break;
					}
				}
				if (done)
					break;
				amount += bytesRead;
			}
			reader.BaseStream.Position = baseAddress;
			return reader.ReadBytes (amount);
		}

		/// <summary>
		/// Reads a zero terminated Ascii-string (including terminating the zero) from this BinaryReader.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static string ReadCString (this BinaryReader reader)
		{
			byte[] bytes = ReadUntil (reader, 0x00);
			return bytes.ToString (true, bytes.Length - 1);
		}

		/// <summary>
		/// Reads a zero terminated Ascii-string (including the terminating zero) from this BinaryReader. Also takes a length parameter to control the maximum length of the string
		/// and ensure that a given amount of bytes are read.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string ReadCString (this BinaryReader reader, int length)
		{
			var bytes = reader.ReadBytes (length);
			int stringLength = 0;
			for (int i = 0; i < length; i++) {
				if (bytes [i] == 0)
					break;
				stringLength++;
			}
			return bytes.ToString (true, stringLength);
		}

		/// <summary>
		/// Reads an Ascii string of the specified length from this BinaryReader.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="lengthInBytes">Number of bytes to read.</param>
		/// <returns></returns>
		public static string ReadAsciiString (this BinaryReader reader, int lengthInBytes)
		{
			return reader.ReadBytes(lengthInBytes).ToString (true);
		}

		/// <summary>
		/// Reads an Unicode string of the specified length (in bytes) from this BinaryReader.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="lengthInBytes">Number of bytes to read.</param>
		/// <returns></returns>
		public static string ReadUnicodeString (this BinaryReader reader, int lengthInBytes)
		{
			return reader.ReadBytes (lengthInBytes).ToString (false);
		}

		/// <summary>
		/// Reads an array of the specified length of Integers from this BinaryReader.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="count">Number of elements in the array.</param>
		/// <returns></returns>
		public static int[] ReadInt32Array (this BinaryReader reader, int count)
		{
			if (count < 0)
				return null;
			byte[] bytes = reader.ReadBytes (count * 4);
			int[] intArray = new int[count];
			int idx = 0;
			for (int byteIdx = 0; byteIdx < count * 4; byteIdx += 4)
				intArray [idx++] = BitConverter.ToInt32 (bytes, byteIdx);
			return intArray;
		}

		/// <summary>
		/// Reads an array of the specified length of unsigned Integers from this BinaryReader.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="count">Number of elements in the array.</param>
		/// <returns></returns>
		public static uint[] ReadUInt32Array (this BinaryReader reader, int count)
		{
			if (count < 0)
				return null;
			byte[] bytes = reader.ReadBytes (count * 4);
			uint[] intArray = new uint[count];
			int idx = 0;
			for (int byteIdx = 0; byteIdx < count * 4; byteIdx += 4) {
				intArray [idx++] = BitConverter.ToUInt32 (bytes, byteIdx);
			}
			return intArray;
		}

		/// <summary>
		/// Reads an array of the specified length of Floats from this BinaryReader.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="count">Number of elements in the array.</param>
		/// <returns></returns>
		public static float[] ReadFloatArray (this BinaryReader reader, int count)
		{
			if (count < 0)
				return null;
			byte[] bytes = reader.ReadBytes (count * 4);
			float[] floatArray = new float[count];
			int idx = 0;
			for (int byteIdx = 0; byteIdx < count * 4; byteIdx += 4) {
				floatArray [idx++] = BitConverter.ToSingle (bytes, byteIdx);
			}
			return floatArray;
		}

		/// <summary>
		/// Reads an array of the specified length of 1-byte booleans from this BinaryReader.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="count">Number of elements in the array.</param>
		/// <returns></returns>
		public static bool[] ReadBoolArray (this BinaryReader reader, int count)
		{
			if (count < 0)
				return null;
			byte[] bytes = reader.ReadBytes (count);
			bool[] boolArray = new bool[count];
			for (int i = 0; i < bytes.Length; i++)
				boolArray [i] = bytes [i] > 0;
			return boolArray;
		}
	}
}