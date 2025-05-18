#region

using System.Drawing;

#endregion

namespace cope.UI
{
    public interface IOverlay
    {
        void Render(Graphics g, Rectangle r);
    }
}