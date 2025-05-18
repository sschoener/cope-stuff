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
using cope.Extensions;
using ModTool.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace ModTool.FE
{
    public partial class NewModDialog : Form
    {
        public NewModDialog(string workingDir)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            m_dlgSelectBaseModule.InitialDirectory = workingDir;
            if (ToolSettings.IsInRetributionMode)
                m_chkbxRepackAttrib.Checked = true;
            else
                m_chkbxRepackAttrib.Enabled = false;
        }

        #region properties

        public string ModName
        {
            get
            {
                return m_tbxModName.Text;
            }
        }

        public string DisplayedModName
        {
            get
            {
                if (m_tbxDisplayedModName.Text != string.Empty)
                    return m_tbxDisplayedModName.Text;
                return m_tbxModName.Text;
            }
        }

        public string BaseModule
        {
            get
            {
                return m_tbxBaseModule.Text;
            }
        }

        public string ModVersion
        {
            get
            {
                return m_nupModVersion.Value.ToString("0.0").Replace(',', '.');
            }
        }

        public string ModDescription
        {
            get
            {
                if (m_tbxModName.Text != string.Empty)
                    return m_tbxModDescription.Text;
                return "No description available. It's not my fault. -cope.";
            }
        }

        public uint UCSBaseIndex
        {
            get
            {
                return (uint)m_nupUCSBaseIndex.Value;
            }
        }

        public bool RepackAttribArchive
        {
            get { return m_chkbxRepackAttrib.Checked; }
        }

        #endregion

        private void BtnBaseModuleSearchClick(object sender, EventArgs e)
        {
            if (m_dlgSelectBaseModule.ShowDialog() == DialogResult.OK)
            {
                m_tbxBaseModule.Text = m_dlgSelectBaseModule.FileName;
            }
        }

        private void BtnCreateModClick(object sender, EventArgs e)
        {
            if (m_tbxModName.Text == string.Empty || m_tbxModName.Text.ContainsAny(CharType.Whitespace, CharType.IllegalInFilename))
            {
                 UIHelper.ShowError("Please enter a valid name for your mod.");
                return;
            }

            if (!File.Exists(m_tbxBaseModule.Text))
            {
                 UIHelper.ShowError("The selected base module does not exist!");
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

        private void TbxBaseModuleTextChanged(object sender, EventArgs e)
        {
            if (m_tbxBaseModule.Text.ToLowerInvariant().Contains("retribution"))
            {
                ToolSettings.IsInRetributionMode = true;
                m_chkbxRepackAttrib.Enabled = true;
            }
            else
            {
                ToolSettings.IsInRetributionMode = false;
                m_chkbxRepackAttrib.Enabled = false;
            }
        }
    }
}