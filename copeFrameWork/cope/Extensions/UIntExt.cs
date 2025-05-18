#region

using System;

#endregion

namespace cope.Extensions
{
    /// <summary>
    /// Extension class for UInt32.
    /// </summary>
    public static class UIntExt
    {
        /// <summary>
        /// Rotates the specified uint by 'count' bits right.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="count">Number of bits to rotate by.</param>/param>
        /// <returns></returns>
        public static uint RotateRight(this uint u, int count)
        {
            const int size = (sizeof (uint) * 8);
            count = count % size;
            uint add = u << (size - count);
            u = u >> count;
            u = u | add;
            return u;
        }

        /// <summary>
        /// Rotates the specified uint by 'count' bits left.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="count">Number of bits to rotate by.</param>
        /// <returns></returns>
        public static uint RotateLeft(this uint u, int count)
        {
            const int size = (sizeof (uint) * 8);
            count = count % size;
            uint add = u >> (size - count);
            u = u << count;
            u = u | add;
            return u;
        }

        /// <summary>
        /// Reverses the bytes of this UInt32.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static uint ReverseBytes(this uint u)
        {
            return ((u & 0x000000ff) << 24) | (u >> 24) | ((u & 0x00ff0000) >> 8) | ((u & 0x0000ff00) << 8);
        }

        /// <summary>
        /// Returns a Byte-Array containing the byte representation of this UInt32.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this uint u)
        {
            return BitConverter.GetBytes(u);
        }
    }
}