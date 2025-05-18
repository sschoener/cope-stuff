using System;
using System.Drawing;

namespace cope.Graphics
{
    public class SimpleBlur : MultiThreadPixelProcessor
    {
        readonly int m_iBlurRadiusX;
        readonly int m_iblurRadiusY;

        public SimpleBlur(int radiusX, int radiusY)
            : base(false)
        {
            m_iBlurRadiusX = radiusX;
            m_iblurRadiusY = radiusY;
        }

        protected override Color PixelProcessor(Color c, int x, int y)
        {
            int xLimit = Math.Min(x + m_iBlurRadiusX, m_inputBitmap.Width);
            int xStart = Math.Max(0, x - m_iBlurRadiusX);
            int yLimit = Math.Min(y + m_iblurRadiusY, m_inputBitmap.Height);
            int yStart = Math.Max(0, y - m_iblurRadiusY);
            int pxCount = (xLimit - xStart) * (yLimit - yStart);
            int totalR = 0, totalG = 0, totalB = 0;
            for (int cX = xStart; cX < xLimit; cX++)
            {
                for (int cY = yStart; cY < yLimit; cY++)
                {
                    Color pixel = m_inputBitmap.GetPixel(cX, cY);
                    totalR += pixel.R;
                    totalG += pixel.G;
                    totalB += pixel.B;
                }
            }
            int r = (int)Math.Round((double)totalR / pxCount);
            int g = (int)Math.Round((double)totalG / pxCount);
            int b = (int)Math.Round((double)totalB / pxCount);
            return Color.FromArgb(0, r, g, b);
        }
    }
}