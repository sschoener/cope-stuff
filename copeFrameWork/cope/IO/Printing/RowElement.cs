#region

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#endregion

namespace cope.IO.Printing
{
    public class RowElement : IPrintableDocumentElement, IGenericClonable<RowElement>
    {
        private readonly List<CellElement> m_cells;

        public RowElement(int numCells) : this(numCells, 0f)
        {
            AutoWidth = true;
        }

        public RowElement(int numCells, float width)
        {
            m_cells = new List<CellElement>(numCells);
            Width = width;
            AvoidPageBreak = true;
            for (int i = 0; i < numCells; i++)
                m_cells.Add(new CellElement(CellSizeMode.Weight, 1f / numCells));
        }

        private RowElement()
        {
            m_cells = new List<CellElement>();
            AvoidPageBreak = true;
        }

        public int NumCells
        {
            get { return m_cells.Count; }
        }

        public float SpacingBetweenCells { get; set; }

        public CellElement this[int i]
        {
            get { return m_cells[i]; }
            set { m_cells[i] = value; }
        }

        #region IGenericClonable<RowElement> Members

        public RowElement GClone()
        {
            var re = new RowElement
                         {
                             Width = Width,
                             SpacingBetweenCells = SpacingBetweenCells,
                             AutoWidth = AutoWidth
                         };
            foreach (CellElement c in m_cells)
                re.m_cells.Add(c.GClone());
            return re;
        }

        #endregion

        #region IPrintableDocumentElement Members

        public float Width { get; private set; }

        public float Height
        {
            get { return 0f; }
        }

        public float X { get; set; }

        public float Y { get; set; }

        public bool AutoHeight
        {
            get { return true; }
        }

        public bool AutoWidth { get; set; }

        public bool AvoidPageBreak { get; set; }

        public bool Draw(RectangleF rect, Graphics g)
        {
            float cx = rect.X;
            float[] ws = GetWidthForCells(rect.Width);
            int i = 0;
            foreach (CellElement c in m_cells)
            {
                var re = new RectangleF(cx, rect.Y, ws[i], rect.Height);
                cx += ws[i] + SpacingBetweenCells;
                i++;
                c.Draw(re, g);
            }
            return true;
        }

        public float MeasureHeight(Graphics g, float width)
        {
            float max = 0;
            float[] ws = GetWidthForCells(width);
            for (int i = 0; i < m_cells.Count; i++)
            {
                float h = m_cells[i].MeasureHeight(g, ws[i]);
                if (h > max)
                    max = h;
            }
            return max;
        }

        public void Init()
        {
            foreach (CellElement c in m_cells)
                c.Init();
        }

        public void Finish()
        {
            foreach (CellElement c in m_cells)
                c.Finish();
        }

        public PrintableDocument Document { get; set; }

        #endregion

        private float[] GetWidthForCells(float baseWidth)
        {
            var widths = new float[m_cells.Count];

            float totalWeight = m_cells.Sum(c => c.CellSizeMode == CellSizeMode.Weight ? c.Width : 0);
            float totalAbs = m_cells.Sum(c => c.CellSizeMode == CellSizeMode.Absolute ? c.Width : 0);

            baseWidth -= (totalAbs + (m_cells.Count - 1) * SpacingBetweenCells);

            for (int i = 0; i < m_cells.Count; i++)
            {
                CellElement c = m_cells[i];
                widths[i] = c.CellSizeMode == CellSizeMode.Absolute ? c.Width : (c.Width / totalWeight) * baseWidth;
            }
            return widths;
        }

        public void Append(CellElement e)
        {
            m_cells.Add(e);
        }

        public void AppendRange(IEnumerable<CellElement> es)
        {
            m_cells.AddRange(es);
        }

        public void RemoveCell(CellElement e)
        {
            m_cells.Remove(e);
        }

        public void InsertCellAt(CellElement e, int index)
        {
            if (index >= m_cells.Count)
                m_cells.Add(e);
            else
                m_cells.Insert(index, e);
        }

        public void RemoveCellAt(int index)
        {
            if (index < m_cells.Count)
                m_cells.RemoveAt(index);
        }

        public void Clear()
        {
            m_cells.Clear();
        }

        public static RowElement CreateHeaderRow(IEnumerable<string> columnNames, string fontName, int fontSize,
                                                 GraphicsUnit fontSizeUnit)
        {
            var re = new RowElement(0);
            float totalLength = columnNames.Sum(s => s.Length);
            foreach (string s in columnNames)
            {
                var te = new TextElement(s, fontName, fontSize, fontSizeUnit) {FontStyle = FontStyle.Bold};
                var ce = new CellElement(CellSizeMode.Weight, s.Length / totalLength, te);
                re.Append(ce);
            }
            return re;
        }

        public static RowElement CreateTextRow(IEnumerable<string> columnNames, string fontName, int fontSize,
                                               GraphicsUnit fontSizeUnit)
        {
            var re = new RowElement(0);
            float totalLength = columnNames.Sum(s => s.Length);
            foreach (string s in columnNames)
            {
                var te = new TextElement(s, fontName, fontSize, fontSizeUnit);
                var ce = new CellElement(CellSizeMode.Weight, s.Length / totalLength, te);
                re.Append(ce);
            }
            return re;
        }
    }
}