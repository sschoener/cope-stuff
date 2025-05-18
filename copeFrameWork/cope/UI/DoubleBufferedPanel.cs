#region

using System.Windows.Forms;

#endregion

namespace cope.UI
{
    public sealed partial class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
    }
}