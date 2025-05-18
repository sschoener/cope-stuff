using System;
using System.Windows.Forms;

namespace RBFCompilerGUI
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            m_rtbAbout.Text = Properties.Resources.License;
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
