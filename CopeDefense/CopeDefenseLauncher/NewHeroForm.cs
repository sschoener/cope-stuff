using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cope;
using cope.Helper;
using DefenseShared;

namespace CopeDefenseLauncher
{
    partial class NewHeroForm : Form
    {
        public NewHeroForm()
        {
            InitializeComponent();
            m_cbxHeroType.Items.AddRange(ServerInterface.GetHeroTypes());
            m_cbxHeroType.SelectedIndex = 0;
        }

        public string HeroName
        {
            get { return m_tbxHeroName.Text; }
        }

        public HeroType HeroType
        {
            get { return (HeroType) m_cbxHeroType.SelectedItem; }
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            if (HeroName.Length == 0)
            {
                UIHelper.ShowError("You need to enter a valid hero name!");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
