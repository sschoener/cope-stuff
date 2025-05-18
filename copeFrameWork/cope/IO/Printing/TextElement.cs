#region

using System;
using System.Drawing;

#endregion

namespace cope.IO.Printing
{
    public class TextElement : IPrintableDocumentElement, IGenericClonable<TextElement>
    {
        private Font m_font;
        private int m_stoppedAt;

        public TextElement(string text, string fontName, int fontSize)
            : this(text, fontName, fontSize, GraphicsUnit.Point, 0)
        {
            AutoWidth = true;
        }

        public TextElement(string text, string fontName, int fontSize, GraphicsUnit sizeUnit)
            : this(text, fontName, fontSize, sizeUnit, 0)
        {
            AutoWidth = true;
        }

        public TextElement(string text, string fontName, int fontSize, float width)
            : this(text, fontName, fontSize, GraphicsUnit.Point, width)
        {
        }

        public TextElement(string text, string fontName, int fontSize, GraphicsUnit sizeUnit, float width)
        {
            Text = text;
            FontName = fontName;
            FontSize = fontSize;
            TextColor = Color.Black;
            Width = width;
            FontSizeUnit = sizeUnit;
            FontStyle = new FontStyle();
            Format = new StringFormat(StringFormatFlags.LineLimit);
        }

        public Color TextColor { get; set; }
        public string Text { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public GraphicsUnit FontSizeUnit { get; set; }
        public StringFormat Format { get; set; }
        public FontStyle FontStyle { get; set; }

        #region IGenericClonable<TextElement> Members

        public TextElement GClone()
        {
            return new TextElement(Text, FontName, FontSize, Width);
        }

        #endregion

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
            if (m_font == null)
                throw new Exception("Call Init() before calling Draw!");
            StringFormat format = Format ?? new StringFormat(StringFormatFlags.LineLimit);
            int charsFitted, linesFilled;
            var size = new SizeF(rect.Width, rect.Height);
            string text = Text.Substring(m_stoppedAt, Text.Length - m_stoppedAt);
            g.MeasureString(text, m_font, size, format, out charsFitted, out linesFilled);
            var b = new SolidBrush(TextColor);
            g.DrawString(text, m_font, b, rect, format);
            if (charsFitted < Text.Length)
            {
                m_stoppedAt += charsFitted;
                return false;
            }
            return true;
        }

        public float MeasureHeight(Graphics g, float width)
        {
            if (m_font == null)
                throw new Exception("Call Init() before calling MeasureHeight!");
            string remaining = Text.Substring(m_stoppedAt, Text.Length - m_stoppedAt);
            return g.MeasureString(remaining, m_font, new SizeF(width, float.MaxValue),
                                   Format ?? new StringFormat(StringFormatFlags.LineLimit)).Height;
        }

        public void Init()
        {
            m_font = new Font(FontName, FontSize, FontStyle, FontSizeUnit);
            m_stoppedAt = 0;
        }

        public void Finish()
        {
            m_stoppedAt = -1;
            m_font.Dispose();
            m_font = null;
        }

        #endregion
    }
}