#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace cope
{
    /// <summary>
    /// Static class offering various useful mathematical functions.
    /// </summary>
    public static class MathUtil
    {
        /// <summary>
        /// Returns the Greatest Common Divisor of a and b.
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static double GCD(this double a, double b)
        {
            if (a == 0 || a == b)
                return b;
            double h;
            while (b != 0)
            {
                h = a % b;
                a = b;
                b = h;
            }
            return a;
        }

        /// <summary>
        /// Returns the Least Common Multiple of a and b.
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static double LCM(this double a, double b)
        {
            return a * b / GCD(a, b);
        }

        public static bool IsPrimitiveRoot(this int n, int a)
        {
            int[] fa = DivideToFactors(n - 1);
            return fa.All(i => a.QuickExpMod((n - 1) / i, n) != 1);
        }

        public static int[] DivideToFactors(this int n)
        {
            var factors = new List<int>();
            double s = Math.Sqrt(n);
            while ((n & 1) == 0)
            {
                factors.Add(2);
                n >>= 1;
            }
            for (int i = 3; i <= s; i++)
            {
                if (n == 1)
                    return factors.ToArray();
                if (n % i == 0)
                {
                    factors.Add(i);
                    n /= i;
                    i = 2;
                }
            }
            factors.Add(n);
            return factors.ToArray();
        }

        public static int QuickExpMod(this int a, int exp, int mod)
        {
            long result = 1;
            uint rot = 0x80000000;
            for (int i = 0; i < 32; i++)
            {
                result = result * result % mod;
                if ((exp & rot) == rot)
                    result = result * a % mod;
                rot >>= 1;
            }
            return (int) result;
        }

        /// <summary>
        /// Performs a linear interpolation between 0 and an upper bound.
        /// </summary>
        /// <param name="minOutput">The minimum value to return.</param>
        /// <param name="maxOutput">The maximum value to return.</param>
        /// <param name="value">The value to use for interpolation.</param>
        /// <param name="valueRange">The upper bound, that is the maximum value expected for value.</param>
        /// <returns></returns>
        public static float LinearInterpolation(float minOutput, float maxOutput, float value, float valueRange)
        {
            return ((maxOutput - minOutput) / valueRange) * value + minOutput;
        }

        /// <summary>
        /// Performs the reverse of a linear interpolation.
        /// </summary>
        /// <param name="minOutputOfInterpol">The minimum value the interpolation process may return.</param>
        /// <param name="maxOutputOfInterpol">The maximum value the interpolation process may return.</param>
        /// <param name="output">The actual output value returned by the interpolation process.</param>
        /// <param name="valueRange">The maximum value used by the interpolation process as an interpolation-value.</param>
        /// <returns></returns>
        public static float LinearDeInterpolation(float minOutputOfInterpol, float maxOutputOfInterpol, float output,
                                                  float valueRange)
        {
            return (output - minOutputOfInterpol) * valueRange / (maxOutputOfInterpol - minOutputOfInterpol);
        }

        /// <summary>
        /// Limits the specified value to certain inclusive bounds.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="upperBound"></param>
        /// <param name="lowerBound"></param>
        /// <returns></returns>
        public static int Limit(int value, int lowerBound, int upperBound)
        {
            return Math.Max(lowerBound, Math.Min(value, upperBound));
        }

        public static double Limit(double value, double lowerBound, double upperBound)
        {
            return Math.Max(lowerBound, Math.Min(value, upperBound));
        }

        public static float Limit(float value, float lowerBound, float upperBound)
        {
            return Math.Max(lowerBound, Math.Min(value, upperBound));
        }

        /// <summary>
        /// Assuming a half ellipse with its center in the origin this function returns the height at a specified x position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Ellipse(double x, double a, double b)
        {
            if (a == 0 || b == 0 || x > a || x < -a)
                return 0;
            double h = b * b - x * x * b * b / (a * a);
            return Math.Sqrt(h);
        }
    }
}