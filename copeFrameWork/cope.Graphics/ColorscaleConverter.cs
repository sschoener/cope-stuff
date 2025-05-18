using System;
using System.Drawing;

namespace cope.Graphics
{
    /// <summary>
    /// Transforms all the colors in the picture into a color from a gradient between to other colors.
    /// </summary>
    public class ColorscaleConverter : MultiThreadPixelProcessor
    {
        Color m_cScaleBegin;
        Color m_cScaleEnd;

        public ColorscaleConverter(Color scaleBegin, Color scaleEnd, bool inPlace)
            : base(inPlace)
        {
            m_cScaleBegin = scaleBegin;
            m_cScaleEnd = scaleEnd;
        }

        protected override Color PixelProcessor(Color c, int x, int y)
        {
            return Color.FromArgb(0, CalculateR(c.R),
                                  CalculateG(c.G),
                                  CalculateB(c.B));
        }

        protected ushort CalculateR(ushort r)
        {
            return (ushort)Math.Round((((double)m_cScaleEnd.R - m_cScaleBegin.R) / 255) * r + m_cScaleBegin.R);
        }

        protected ushort CalculateG(ushort g)
        {
            return (ushort)Math.Round((((double)m_cScaleEnd.G - m_cScaleBegin.G) / 255) * g + m_cScaleBegin.G);
        }

        protected ushort CalculateB(ushort b)
        {
            return (ushort)Math.Round((((double)m_cScaleEnd.B - m_cScaleBegin.B) / 255) * b + m_cScaleBegin.B);
        }

        public Color ScaleBegin
        {
            get { return m_cScaleBegin; }
        }
        public Color ScaleEnd
        {
            get { return m_cScaleEnd; }
        }
    }
}