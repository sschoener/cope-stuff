#region

using System;

#endregion

namespace cope
{
    /// <summary>
    /// Implementation of the Cantor pairing function.
    /// http://en.wikipedia.org/wiki/Pairing_function
    /// </summary>
    public static class Cantor
    {
        /// <summary>
        /// Computes Z from X and Y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static long Compute(long x, long y)
        {
            return (x + y) * (x + y + 1) / 2 + y;
        }

        /// <summary>
        /// Computes X from Z.
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static long ComputeX(long z)
        {
            var j = (long) Math.Floor(Math.Sqrt(0.25 + 2 * z) - 0.5);
            return j - ComputeY(z);
        }

        /// <summary>
        /// Computes Y from Z.
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static long ComputeY(long z)
        {
            var j = (long) Math.Floor(Math.Sqrt(0.25 + 2 * z) - 0.5);
            return z - j * (j + 1) / 2;
        }
    }
}