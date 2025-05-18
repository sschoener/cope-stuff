#region

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace cope.UI
{
    public delegate void TabHeaderClickedHandler(object sender, MouseEventArgs e, int tabIndex);

    /// <summary>
    /// Extends the TabControl with an event that is called when the tab's header gets clicked.
    /// </summary>
    public class TabControlExt : TabControl
    {
        public event TabHeaderClickedHandler TabHeaderClicked;

        /// <summary>
        /// Overrides the OnMouseClick to first check whether or not the tab's header has been clicked.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                var tabHeader = GetTabRect(i);
                if (tabHeader.Contains(p))
                {
                    if (TabHeaderClicked != null)
                        TabHeaderClicked(this, e, i);
                }
            }
            base.OnMouseClick(e);
        }
    }
}