#region

using System;
using System.Drawing;

#endregion

namespace cope.UI
{
    public class GridOverlayCount : IOverlay
    {
        #region fields

        private Pen m_pen;

        #endregion fields

        #region ctors

        public GridOverlayCount(int verticalCount, int horizontalCount)
        {
            VerticalCount = verticalCount;
            HorizontalCount = horizontalCount;
            m_pen = new Pen(Color.Black);
        }

        public GridOverlayCount(Color color, int verticalCount, int horizontalCount)
        {
            VerticalCount = verticalCount;
            HorizontalCount = horizontalCount;
            m_pen = new Pen(color);
        }

        public GridOverlayCount(Pen pen, int verticalCount, int horizontalCount)
        {
            VerticalCount = verticalCount;
            HorizontalCount = horizontalCount;
            m_pen = pen;
        }

        #endregion ctors

        #region methods

        public void Render(Graphics g, Rectangle r)
        {
            throw new NotImplementedException();
        }

        #endregion methods

        #region properties

        public int VerticalCount { get; set; }

        public int HorizontalCount { get; set; }

        public Color Color
        {
            get { return m_pen.Color; }
            set { m_pen.Color = value; }
        }

        public Pen Pen
        {
            get { return m_pen; }
            set { m_pen = value; }
        }

        #endregion properties
    }
}