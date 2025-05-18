#region

using System.Drawing;

#endregion

namespace cope.IO.Printing
{
    public class VerticalSpaceElement : IPrintableDocumentElement, IGenericClonable<VerticalSpaceElement>
    {
        private float m_fRemaining;

        public VerticalSpaceElement(float height)
        {
            Height = height;
        }

        #region IGenericClonable<VerticalSpaceElement> Members

        public VerticalSpaceElement GClone()
        {
            return new VerticalSpaceElement(Height);
        }

        #endregion

        #region IPrintableDocumentElement Members

        public PrintableDocument Document { get; set; }

        public float Width
        {
            get { return -1f; }
        }

        public float Height { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public bool AutoHeight
        {
            get { return true; }
        }

        public bool AutoWidth
        {
            get { return true; }
        }

        public bool AvoidPageBreak
        {
            get { return false; }
        }

        public bool Draw(RectangleF rect, Graphics g)
        {
            if (rect.Height < m_fRemaining)
            {
                m_fRemaining -= rect.Height;
                return false;
            }
            return true;
        }

        public float MeasureHeight(Graphics g, float width)
        {
            return m_fRemaining;
        }

        public void Init()
        {
            m_fRemaining = Height;
        }

        public void Finish()
        {
            m_fRemaining = -1;
        }

        #endregion
    }
}