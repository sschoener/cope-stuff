#region

using System.Drawing;

#endregion

namespace cope.IO.Printing
{
    public interface IPrintableDocumentElement
    {
        /// <summary>
        /// Gets the width of this IPrintableDocumentElement (only relevant if AutoWidth = false)
        /// </summary>
        float Width { get; }

        /// <summary>
        /// Gets the height of this IPrintableDocumentElement (only relevant if AutoHeight = false)
        /// </summary>
        float Height { get; }

        float X { get; set; }

        float Y { get; set; }

        /// <summary>
        /// Gets whether this IPrintableDocumentElement has its own height (=false) or wants an automatically calculated height (=true).
        /// </summary>
        bool AutoHeight { get; }

        /// <summary>
        /// Gets whether this IPrintableDocumentElement has its own width (=false) or wants an automatically calculated width (=true).
        /// </summary>
        bool AutoWidth { get; }

        /// <summary>
        /// Tries to palce this element so it is not suffering form a page break right through it.
        /// </summary>
        bool AvoidPageBreak { get; }

        PrintableDocument Document { get; set; }

        /// <summary>
        /// Draws this object to the specified RectangleF using the given Graphics. Returns true whether or not it finished drawing.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        bool Draw(RectangleF rect, Graphics g);

        /// <summary>
        /// Returns the height of this IPrintableDocumentElement given a specified width.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        float MeasureHeight(Graphics g, float width);

        /// <summary>
        /// Called when the printing process begins.
        /// </summary>
        void Init();

        /// <summary>
        /// Called when the printing process has finished.
        /// </summary>
        void Finish();
    }
}