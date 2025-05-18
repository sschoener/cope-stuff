using System;

namespace cope.Maths
{
    public static class NormalDistribution
    {
        public static double GetValue(double x, double sigma = 1.0)
        {
            double exponent = -(x * x) / (2 * sigma * sigma);
            return (1 / (Math.Sqrt(2 * Math.PI) * sigma)) * Math.Exp(exponent);
        }

        public static double GetValueBySquared(double xSquared, double sigma = 1.0)
        {
            double exponent = -xSquared / (2 * sigma * sigma);
            return (1 / (Math.Sqrt(2 * Math.PI) * sigma)) * Math.Exp(exponent);
        }

        public static double GetValue(double x, double y, double sigma = 1.0)
        {
            double exponent = -(x * x + y * y) / (4 * sigma * sigma);
            return 1 / (2 * Math.PI * sigma * sigma) * Math.Exp(exponent);
        }
    }
}
