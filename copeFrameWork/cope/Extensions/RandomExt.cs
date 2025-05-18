#region

using System;

#endregion

namespace cope.Extensions
{
    public static class RandomExt
    {
        /// <summary>
        /// Returns a random boolean value.
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        public static bool NextBool(this Random rng)
        {
            return rng.NextDouble() >= 0.5;
        }

        /// <summary>
        /// Returns a random boolean value with a specified chance of it being 'true'.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="chance">The chance of the boolean value to be 'true'.</param>
        /// <returns></returns>
        public static bool NextBool(this Random rng, double chance)
        {
            return rng.NextDouble() >= (1.0 - chance);
        }

        /// <summary>
        /// Returns a double-value within the specified interval. The upper limit is exclusive.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="lowerLimit">The inclusive lower limit of the interval.</param>
        /// <param name="upperLimit">The exclusive upper limit of the interval.</param>
        /// <returns></returns>
        public static double NextDouble(this Random rng, double lowerLimit, double upperLimit)
        {
            return lowerLimit + rng.NextDouble() * (upperLimit - lowerLimit);
        }

        /// <summary>
        /// Returns a random value which follows a standard normal distribution.
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        public static double NextNormal(this Random rng)
        {
            double n1, n2;
            PolarMethod(rng, 0, 1, out n1, out n2);
            return n1;
        }

        /// <summary>
        /// Returns a normally distributed value in the specified interval.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="from">The lowest value to return.</param>
        /// <param name="to">The highest value to return.</param>
        /// <returns></returns>
        public static double NextNormal(this Random rng, double from, double to)
        {
            double median = (from + to) / 2.0;
            double stdDev = Math.Abs(from - median) / 3.0;
            double n1, n2;
            PolarMethod(rng, median, stdDev, out n1, out n2);
            if (n1 < from)
                return from;
            if (n1 > to)
                return to;
            return n1;
        }

        /// <summary>
        /// Returns a random value which is distributed in the form an inverse bell function.
        /// This means that extreme values are more likely to occur than values close to the median.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static double NextInverseBell(this Random rng, double from, double to)
        {
            // using an inverse sigmoid function:
            // 1 / (1 + exp(-x)) * scaling
            double scaling = to - from;
            double x = rng.NextDouble();

            double value = scaling / (1 + Math.Exp(5 + -x * 10));
            return from + value;
        }

        /// <summary>
        /// Returns a random value which is distributed in the form a quadratic function.
        /// This means that values closer to the 'to' limit are more likely to occur than values close to
        /// the 'from' limit.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static double NextQuadratic(this Random rng, double from, double to)
        {
            // distribution: x^2
            // using the inverse integral (= x - (1/3 * x^3)) to get a function
            // scaling the function to be [0,1] (multiply w/ 3/2)
            double x = rng.NextDouble();
            double scaling = to - from;
            double value = scaling * 3.0 / 2.0 * (x - 1.0 / 3.0 * x * x * x);
            return from + value;
        }

        private static void PolarMethod(Random rng, double median, double stdDev, out double norm1, out double norm2)
        {
            double a1, a2, q;
            do
            {
                a1 = 2 * rng.NextDouble() - 1;
                a2 = 2 * rng.NextDouble() - 1;
                q = a1 * a1 + a2 * a2;
            } while (q < 0 || q > 1);
            double p = Math.Sqrt(-2 * Math.Log(q) / q);
            norm1 = median + a1 * p * stdDev;
            norm2 = median + a2 * p * stdDev;
        }
    }
}