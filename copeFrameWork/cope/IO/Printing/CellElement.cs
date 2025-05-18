#region

using System.Drawing;

#endregion

namespace cope.IO.Printing
{
    public class CellElement : IGenericClonable<CellElement>
    {
        public CellElement(CellSizeMode mode, float width, IPrintableDocumentElement item)
        {
            Item = item;
            CellSizeMode = mode;
            Width = width;
        }

        public CellElement(CellSizeMode mode, float width) : this(mode, width, null)
        {
        }

        public float Width { get; set; }

        public float Height
        {
            get
            {
                if (Item != null && Item.AutoHeight) return Item.Height;
                return 0f;
            }
        }

        public IPrintableDocumentElement Item { get; set; }

        public CellSizeMode CellSizeMode { get; set; }

        public bool AutoHeight
        {
            get
            {
                if (Item != null) return Item.AutoHeight;
                return false;
            }
        }

        #region IGenericClonable<CellElement> Members

        public CellElement GClone()
        {
            var ce = new CellElement(CellSizeMode, Width, null);
            if (Item != null && Item is IGenericClonable<IPrintableDocumentElement>)
            {
                ce.Item = (Item as IGenericClonable<IPrintableDocumentElement>).GClone();
            }
            return ce;
        }

        #endregion

        public bool Draw(RectangleF rect, Graphics g)
        {
            if (Item != null)
                return Item.Draw(rect, g);
            return true;
        }

        public float MeasureHeight(Graphics g, float width)
        {
            if (Item != null)
                return Item.MeasureHeight(g, width);
            return 0f;
        }

        public void Init()
        {
            if (Item != null)
                Item.Init();
        }

        public void Finish()
        {
            if (Item != null)
                Item.Finish();
        }
    }
}