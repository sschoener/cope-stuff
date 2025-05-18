#region

using System.Drawing;

#endregion

namespace cope.IO.Printing
{
    public sealed class PageBreakElement : IPrintableDocumentElement
    {
        #region IPrintableDocumentElement Members

        public float Width
        {
            get { return 0; }
        }

        public float Height
        {
            get { return float.NegativeInfinity; }
        }

        public float X { get; set; }

        public float Y { get; set; }

        public bool AutoHeight
        {
            get { return false; }
        }

        public bool AutoWidth
        {
            get { return false; }
        }

        public bool AvoidPageBreak
        {
            get { return false; }
        }

        public bool Draw(RectangleF rect, Graphics g)
        {
            return true;
        }

        public float MeasureHeight(Graphics g, float width)
        {
            return float.NegativeInfinity;
        }

        public void Init()
        {
        }

        public void Finish()
        {
        }

        public PrintableDocument Document { get; set; }

        #endregion
    }
}