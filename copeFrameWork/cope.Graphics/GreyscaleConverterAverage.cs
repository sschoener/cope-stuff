using System;
using System.Drawing;

namespace cope.Graphics
{
    /// <summary>
    /// Converts a BMP to greyscale by taking the average of RGB.
    /// </summary>
    public sealed class GreyscaleConverterAverage : MultiThreadPixelProcessor
    {
        public GreyscaleConverterAverage(bool inPlace)
            : base(inPlace)
        { }

        protected override Color PixelProcessor(Color c, int x, int y)
        {
            int totalColor = c.R + c.G + c.B;
            var newColor = (int)Math.Round((double)totalColor / 3);
            return Color.FromArgb(0, newColor, newColor, newColor);
        }
    }
}