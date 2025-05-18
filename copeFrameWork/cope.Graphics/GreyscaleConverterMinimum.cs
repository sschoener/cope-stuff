using System;
using System.Drawing;

namespace cope.Graphics
{
    /// <summary>
    /// Converts a BMP to greyscale by taking the min of RGB.
    /// </summary>
    public sealed class GreyscaleConverterMinimum : MultiThreadPixelProcessor
    {
        public GreyscaleConverterMinimum(bool inPlace)
            : base(inPlace)
        { }

        protected override Color PixelProcessor(Color c, int x, int y)
        {
            int min = Math.Min(Math.Min(c.R, c.G), c.B);
            return Color.FromArgb(0, min, min, min);
        }
    }
}