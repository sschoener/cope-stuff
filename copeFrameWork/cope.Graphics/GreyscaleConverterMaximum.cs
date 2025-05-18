using System;
using System.Drawing;

namespace cope.Graphics
{
    /// <summary>
    /// Converts a BMP to greyscale by taking the max of RGB.
    /// </summary>
    public sealed class GreyscaleConverterMaximum : MultiThreadPixelProcessor
    {
        public GreyscaleConverterMaximum(bool inPlace)
            : base(inPlace)
        { }

        protected override Color PixelProcessor(Color c, int x, int y)
        {
            int max = Math.Max(Math.Max(c.R, c.G), c.B);
            return Color.FromArgb(0, max, max, max);
        }
    }
}