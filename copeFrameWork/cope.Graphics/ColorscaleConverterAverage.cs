using System;
using System.Drawing;

namespace cope.Graphics
{
    public class ColorscaleConverterAverage : ColorscaleConverter
    {
        public ColorscaleConverterAverage(Color scaleBegin, Color scaleEnd, bool inPlace)
            : base(scaleBegin, scaleEnd, inPlace)
        { }

        protected override Color PixelProcessor(Color c, int x, int y)
        {
            var average = (ushort)Math.Round((double)(c.R + c.B + c.G) / 3);
            return Color.FromArgb(0, CalculateR(average),
                                  CalculateG(average),
                                  CalculateB(average));
        }
    }
}