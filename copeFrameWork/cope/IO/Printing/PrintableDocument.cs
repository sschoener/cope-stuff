#region

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

#endregion

namespace cope.IO.Printing
{
    public class PrintableDocument : PrintDocument
    {
        private readonly List<DocElement> m_elements;
        public bool UseMargins;
        private int m_currentPageNumber;
        private int m_stoppedAt;

        public PrintableDocument(string docName)
        {
            m_elements = new List<DocElement>();
            DocumentName = docName;
            DrawPageNumbers = true;
            StartPageNumber = 1;
            DefaultFontName = "Arial";
            DefaultFontSize = 12;
            DefaultFontSizeUnit = GraphicsUnit.Point;
            DefaultFontStyle = FontStyle.Regular;
            DefaultFontColor = Color.Black;
        }

        public bool DrawPageNumbers { get; set; }
        public int StartPageNumber { get; set; }
        public Font DefaultFont { get; set; }
        public string DefaultFontName { get; set; }
        public int DefaultFontSize { get; set; }
        public GraphicsUnit DefaultFontSizeUnit { get; set; }
        public FontStyle DefaultFontStyle { get; set; }
        public Color DefaultFontColor { get; set; }
        public IPrintableDocumentElement Header { get; set; }

        /// <summary>
        /// Adds the specified IPrintableDocumentElement to the document.
        /// </summary>
        /// <param name="e"></param>
        public void AddIndependentElement(IPrintableDocumentElement e)
        {
            m_elements.Add(new DocElement(e, false));
            e.Document = this;
        }

        /// <summary>
        /// Appends the specified IPrintableDocumentElement to the document, so it tries to adjust the position to the other elements.
        /// </summary>
        /// <param name="e"></param>
        public void Append(IPrintableDocumentElement e)
        {
            m_elements.Add(new DocElement(e, true));
            e.Document = this;
        }

        /// <summary>
        /// Removes all IPrintableDocumentElements from this document.
        /// </summary>
        public void Clear()
        {
            foreach (DocElement e in m_elements)
                e.Element.Document = null;
            m_elements.Clear();
        }

        /// <summary>
        /// Removes the specified IPrintableDocumentElement from this document.
        /// </summary>
        /// <param name="e"></param>
        public void Remove(IPrintableDocumentElement e)
        {
            m_elements.RemoveAll(d =>
                                     {
                                         if (d.Element == e) e.Document = null;
                                         return d.Element == e;
                                     });
        }

        private static RectangleF GetRect(IPrintableDocumentElement pde, Graphics g, RectangleF clip)
        {
            float drawWidth = !pde.AutoWidth && pde.Width < clip.Width ? pde.Width : clip.Width;
            // if it is set to AutoHeight -> measure it!
            float drawHeight = pde.Height;
            if (pde.AutoHeight)
                drawHeight = pde.MeasureHeight(g, drawWidth);
            return new RectangleF(clip.X + pde.X, clip.Y + pde.Y, drawWidth, drawHeight);
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            foreach (DocElement se in m_elements)
                se.Element.Init();
            m_stoppedAt = 0;
            m_currentPageNumber = StartPageNumber;
            DefaultFont = new Font(DefaultFontName, DefaultFontSize, DefaultFontStyle, DefaultFontSizeUnit);
            base.OnBeginPrint(e);
        }

        /// <exception cref="CopeException">Can't have a page break in a page header!</exception>
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            // setup draw clip
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            RectangleF clip = e.Graphics.VisibleClipBounds;
            if (UseMargins)
            {
                clip.X += UnitConverter.InchToMillimeter(e.MarginBounds.X / 100f);
                clip.Y += UnitConverter.InchToMillimeter(e.MarginBounds.Y / 100f);
                clip.Width = UnitConverter.InchToMillimeter(e.MarginBounds.Width / 100f);
                clip.Height = UnitConverter.InchToMillimeter(e.MarginBounds.Height / 100f);
            }
            else if (DrawPageNumbers)
                clip.Height -= 8;

            // draw header
            if (Header != null)
            {
                var drawRect = GetRect(Header, e.Graphics, clip);
                if (drawRect.Height == float.NegativeInfinity)
                    throw new CopeException("Can't have a page break in a page header!");
                if (drawRect.Y + drawRect.Height > clip.Height || !Header.Draw(drawRect, e.Graphics))
                    throw new CopeException("Page header is too big to fit on a single page!");
            }
            float currentY = clip.Y;

            // print elements
            for (int i = m_stoppedAt; i < m_elements.Count; i++)
            {
                DocElement docElement = m_elements[i];
                IPrintableDocumentElement pde = docElement.Element;
                var drawRect = GetRect(pde, e.Graphics, clip);
                // check for forced pagebreak
                if (drawRect.Height == float.NegativeInfinity)
                {
                    e.HasMorePages = true;
                    m_stoppedAt = i + 1;
                    break;
                }
                if (docElement.Appended)
                    drawRect.Y = currentY;
                if (drawRect.Y + drawRect.Height > clip.Height) // check for page break
                {
                    // check whether the object is small enough for a single page
                    // and go to the next page if it does not like page breaks
                    if (drawRect.Height < clip.Height && pde.AvoidPageBreak)
                    {
                        m_stoppedAt = i;
                        e.HasMorePages = true;
                        break;
                    }
                    // else: cut it to pieces by giving it a smaller rectangle
                    drawRect.Height = clip.Height - drawRect.Y;
                }
                if (docElement.Appended)
                    currentY += drawRect.Height;
                if (!pde.Draw(drawRect, e.Graphics))
                {
                    m_stoppedAt = i;
                    e.HasMorePages = true;
                    break;
                }
            }
            if (DrawPageNumbers)
            {
                var font = new Font("Arial", 5, GraphicsUnit.Millimeter);
                string num = m_currentPageNumber.ToString();
                SizeF sizef = e.Graphics.MeasureString(num, font);
                float posX = e.Graphics.VisibleClipBounds.Width / 2f - sizef.Width / 2f;
                float posY = e.Graphics.VisibleClipBounds.Bottom - sizef.Height;
                var brush = new SolidBrush(Color.Black);
                e.Graphics.DrawString(num, font, brush, posX, posY);
            }
            base.OnPrintPage(e);
            m_currentPageNumber++;
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            foreach (DocElement se in m_elements)
                se.Element.Finish();
            m_stoppedAt = -1;
            DefaultFont.Dispose();
            DefaultFont = null;
            base.OnEndPrint(e);
        }

        #region Nested type: DocElement

        private struct DocElement
        {
            public readonly bool Appended;
            public readonly IPrintableDocumentElement Element;

            public DocElement(IPrintableDocumentElement element, bool appended)
            {
                Appended = appended;
                Element = element;
            }
        }

        #endregion
    }
}