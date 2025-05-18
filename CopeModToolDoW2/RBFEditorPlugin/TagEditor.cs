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
using System.Linq;
using System.Windows.Forms;

namespace RBFPlugin
{
    public partial class TagEditor : Form
    {
        public TagEditor()
        {
            InitializeComponent();
            m_chklbxTagGroups.Items.AddRange(RBFLibrary.GetTagGroupNames().ToArray());
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string[] TagGroups
        {
            get
            {
                string[] items = new string[m_chklbxTagGroups.CheckedItems.Count];
                m_chklbxTagGroups.CheckedItems.CopyTo(items, 0);
                return items;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    for (int i = 0; i < m_chklbxTagGroups.Items.Count; i++)
                        m_chklbxTagGroups.SetItemChecked(i, false);
                }
                else
                {
                    for (int i = 0; i < m_chklbxTagGroups.Items.Count; i++)
                    {
                        string item = m_chklbxTagGroups.Items[i] as string;
                        m_chklbxTagGroups.SetItemChecked(i, value.Contains(item));
                    }
                }
            }
        }
    }
}