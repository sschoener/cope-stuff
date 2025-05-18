#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#endregion

namespace cope.IO.Printing
{
    public class TableElement : IPrintableDocumentElement
    {
        private readonly List<RowElement> m_rows;
        private int m_stoppedAtIndex;

        public TableElement(float width)
        {
            m_rows = new List<RowElement>();
            Width = width;
        }

        public TableElement()
        {
            m_rows = new List<RowElement>();
            AutoWidth = true;
        }

        public float SpacingBetweenRows { get; set; }
        public bool PrintFirstRowOnEveryPage { get; set; }

        #region IPrintableDocumentElement Members

        public PrintableDocument Document { get; set; }

        public float Width { get; set; }

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
            if (m_stoppedAtIndex == -1)
                throw new Exception("Call Init() before calling Draw()!");
            float ly = rect.Y;
            if (PrintFirstRowOnEveryPage)
            {
                RowElement r = m_rows[0];
                float w = rect.Width;
                if (!r.AutoWidth && r.Width < rect.Width)
                    w = r.Width;
                float d = r.MeasureHeight(g, w);
                var re = new RectangleF(rect.X, ly, w, d);
                ly += d;
                if (m_stoppedAtIndex == 0)
                    m_stoppedAtIndex++;
                if (ly > rect.Y + rect.Height || !r.Draw(re, g))
                    return false;
                ly += SpacingBetweenRows;
            }
            for (int i = m_stoppedAtIndex; i < m_rows.Count; i++)
            {
                RowElement r = m_rows[i];
                float w = rect.Width;
                if (!r.AutoWidth && r.Width < rect.Width)
                    w = r.Width;
                float d = r.MeasureHeight(g, w);
                var re = new RectangleF(rect.X, ly, w, d);
                ly += d;
                if (ly > rect.Y + rect.Height || !r.Draw(re, g))
                {
                    m_stoppedAtIndex = i;
                    return false;
                }
                ly += SpacingBetweenRows;
            }
            return true;
        }

        public float MeasureHeight(Graphics g, float width)
        {
            // height of all remaining rows + spacing + tolerance
            var remainingRows = m_rows.SkipWhile((r, i) => i < m_stoppedAtIndex);
            float height = remainingRows.Sum(r => r.MeasureHeight(g, width)) + 0.1f;
            height += (m_rows.Count - 1 - m_stoppedAtIndex) * SpacingBetweenRows;
            return height;
        }

        public void Init()
        {
            foreach (RowElement c in m_rows)
                c.Init();
            m_stoppedAtIndex = 0;
        }

        public void Finish()
        {
            foreach (RowElement c in m_rows)
                c.Finish();
            m_stoppedAtIndex = -1;
        }

        #endregion

        public void Append(RowElement r)
        {
            m_rows.Add(r);
        }

        public void AppendRange(IEnumerable<RowElement> rs)
        {
            m_rows.AddRange(rs);
        }

        public void Remove(RowElement r)
        {
            m_rows.Remove(r);
        }

        public void Clear()
        {
            m_rows.Clear();
        }

        public void InsertAt(RowElement r, int index)
        {
            if (index >= m_rows.Count)
                m_rows.Add(r);
            else
                m_rows.Insert(index, r);
        }

        public void RemoveAt(int index)
        {
            if (index < m_rows.Count)
                m_rows.RemoveAt(index);
        }
    }
}