using System;
using System.Drawing;

namespace cope.Graphics
{
    public class ColorRangeConverter
    {
        Color m_cUpperBound;
        Color m_cLowerBound;
        readonly int m_iUpperBound;

        public ColorRangeConverter(Color lowerBound, Color upperBound, int ilowerBound, int iupperBound)
        {
            m_cLowerBound = lowerBound;
            m_cUpperBound = upperBound;
            m_iUpperBound = iupperBound - ilowerBound;
        }

        public virtual Color GetColor(int i)
        {
            if (i > m_iUpperBound)
                i = m_iUpperBound;
            else if (i < 0)
                i = 0;
            return Color.FromArgb(CalculateA(i), CalculateR(i), CalculateG(i), CalculateB(i));
        }

        protected int CalculateA(int a)
        {
            return (int)Math.Round((((double)m_cUpperBound.A - m_cLowerBound.A) / (m_iUpperBound)) * a + m_cLowerBound.A);
        }

        protected int CalculateR(int r)
        {
            return (int)Math.Round((((double)m_cUpperBound.R - m_cLowerBound.R) / (m_iUpperBound)) * r + m_cLowerBound.R);
        }

        protected int CalculateG(int g)
        {
            return (int)Math.Round((((double)m_cUpperBound.G - m_cLowerBound.G) / (m_iUpperBound)) * g + m_cLowerBound.G);
        }

        protected int CalculateB(int b)
        {
            return (int)Math.Round((((double)m_cUpperBound.B - m_cLowerBound.B) / (m_iUpperBound)) * b + m_cLowerBound.B);
        }
    }
}