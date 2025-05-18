#region

using System.Drawing;

#endregion

namespace cope.UI
{
    public interface IDraggable
    {
        bool IsSelected { get; set; }

        Point Position { get; set; }

        Size Size { get; set; }

        int PosX { get; set; }

        int PosY { get; set; }

        bool IsInDragSection(Point pos);
    }
}