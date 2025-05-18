#region

using System;
using System.Drawing;

#endregion

namespace cope.UI
{
    public class GridOverlaySized : IOverlay
    {
        #region fields

        private int m_elementHeight;
        private Pen m_elementPen;
        private int m_elementWidth;

        #endregion fields

        #region ctors

        public GridOverlaySized(Pen pen, int elementHeight, int elementWidth)
        {
            m_elementHeight = elementHeight;
            m_elementWidth = elementWidth;
            m_elementPen = pen;
        }

        public GridOverlaySized(int elementHeight, int elementWidth)
        {
            m_elementHeight = elementHeight;
            m_elementWidth = elementWidth;
            m_elementPen = new Pen(Color.Black, 1);
        }

        public GridOverlaySized(Color color, int elementHeight, int elementWidth)
        {
            m_elementHeight = elementHeight;
            m_elementWidth = elementWidth;
            m_elementPen = new Pen(color, 1);
        }

        #endregion ctors

        #region methods

        public void Render(Graphics g, Rectangle r)
        {
            // double   ->  float  = fast
            // double   ->  int    = slow
            double f = (r.Width / m_elementWidth);
            var countX = (int) Math.Floor(f);
            f = r.Height / m_elementHeight;
            var countY = (int) Math.Floor(f);

            for (int k = 1; k <= countX; k++)
            {
                int pos = k * m_elementWidth;
                g.DrawLine(m_elementPen, pos, r.Y, pos, r.Bottom);
            }
            for (int k = 1; k <= countY; k++)
            {
                int pos = k * m_elementHeight;
                g.DrawLine(m_elementPen, r.X, pos, r.Right, pos);
            }
        }

        #endregion methods

        #region properties

        public int ElementHeight
        {
            get { return m_elementHeight; }
            set { m_elementHeight = value; }
        }

        public int ElementWidth
        {
            get { return m_elementWidth; }
            set { m_elementWidth = value; }
        }

        public Color Color
        {
            get { return m_elementPen.Color; }
            set { m_elementPen.Color = value; }
        }

        public Pen Pen
        {
            get { return m_elementPen; }
            set { m_elementPen = value; }
        }

        #endregion properties
    }
}