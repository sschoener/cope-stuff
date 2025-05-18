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
using System;
using System.Windows.Forms;
using ModTool.Core.PlugIns;

namespace ModTool.Core
{
    public partial class PluginManagerControl : UserControl
    {
        public PluginManagerControl()
        {
            InitializeComponent();
            foreach (string str in PluginManager.FileTypePlugins.Keys)
            {
                m_lbxFileTypes.Items.Add(str);
            }
        }

        #region eventhandlers

        private void ClbxPluginsItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < m_chklbxPlugins.Items.Count; i++)
                    m_chklbxPlugins.SetItemChecked(i, false);
            }
            FileTypeManager.FileTypes[m_lbxFileTypes.SelectedItem.ToString()] = (FileTypePlugin)m_chklbxPlugins.Items[e.Index];
        }

        private void LbxFileTypesSelectedValueChanged(object sender, EventArgs e)
        {
            m_chklbxPlugins.Items.Clear();
            if (m_lbxFileTypes.SelectedItem == null)
            {
                return;
            }
            foreach (FileTypePlugin ftp in PluginManager.FileTypePlugins[m_lbxFileTypes.SelectedItem.ToString()])
            {
                int index = m_chklbxPlugins.Items.Add(ftp, false);

                if (FileTypeManager.FileTypes.ContainsKey(m_lbxFileTypes.SelectedItem.ToString()))
                {
                    if (ftp == FileTypeManager.FileTypes[m_lbxFileTypes.SelectedItem.ToString()])
                        m_chklbxPlugins.SetItemChecked(index, true);
                }
            }
        }

        private void ClbxPluginsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_chklbxPlugins.SelectedItem == null)
            {
                m_labAuthorValue.Text = "nothing selected";
                m_labNameValue.Text = "nothing selected";
                m_labVersionValue.Text = "nothing selected";
                return;
            }

            m_labAuthorValue.Text = (m_chklbxPlugins.SelectedItem as FileTypePlugin).Author;
            m_labNameValue.Text = (m_chklbxPlugins.SelectedItem as FileTypePlugin).PluginName;
            m_labVersionValue.Text = (m_chklbxPlugins.SelectedItem as FileTypePlugin).Version;
        }

        #endregion eventhandlers
    }
}