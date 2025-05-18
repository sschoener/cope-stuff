#region

using System;
using System.Windows.Forms;
using cope;

#endregion

namespace CopeDefenseLauncher
{
    public partial class DeleteHeroForm : Form
    {
        public DeleteHeroForm()
        {
            InitializeComponent();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            if (m_tbxConfirmDeletion.Text != @"DELETE")
            {
                UIHelper.ShowError("Confirmation failed, type 'DELETE' into the textbox.");
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