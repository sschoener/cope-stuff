/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using cope;
using ModTool.Core;
using ModTool.FE.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ModTool.FE
{
    public partial class UCSAdder : Form
    {
        public UCSAdder()
        {
            InitializeComponent();
            m_chkbxCopyToClipboard.Checked = Settings.Default.bUCSCopyIndexToClipboard;
            m_chkbxAutoIndex.Checked = Settings.Default.bUCSAutoIndex;
            m_nupIndex.Value = UCSManager.NextIndex;
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            if (!CommitNewUCSEntry())
                return;
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool CommitNewUCSEntry()
        {
            // check if index is still available
            var index = (uint)m_nupIndex.Value;
            if (UCSManager.HasString(index))
            {
                 UIHelper.ShowError("The selected index already exists! Choose another one!");
                return false;
            }

            // merge text into one line
            string text = m_rtbUCSText.Text;
            if (m_rtbUCSText.Lines.Length > 1)
            {
                text = m_rtbUCSText.Lines.Aggregate(string.Empty, (current, s) => current + " " + s);
            }

            UCSManager.AddString(text, index);
            if (m_chkbxCopyToClipboard.Checked)
                Clipboard.SetText(index.ToString());
            return true;
        }

        private void ChkbxAutoIndexCheckedChanged(object sender, EventArgs e)
        {
            if (m_chkbxAutoIndex.Checked)
            {
                m_nupIndex.Enabled = false;
                m_nupIndex.Value = UCSManager.NextIndex;
            }
            else
                m_nupIndex.Enabled = true;
        }

        private void UCSAdderShown(object sender, EventArgs e)
        {
            m_rtbUCSText.Focus();
        }
    }
}