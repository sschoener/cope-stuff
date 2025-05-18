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
using ModTool.Core;
using ModTool.FE.Properties;

namespace ModTool.FE
{
    public partial class UCSEditor : UserControl
    {
        public UCSEditor()
        {
            InitializeComponent();

            m_chkbxCopyToClipboard.Checked = Settings.Default.bUCSCopyIndexToClipboard;
            m_chkbxAutoIndex.Checked = Settings.Default.bUCSAutoIndex;
            m_nupIndex.Value = UCSManager.NextIndex;

            UCSManager.StringAdded += OnStringAdded;
            UCSManager.StringModified += OnStringModified;
            UCSManager.StringRemoved += OnStringRemoved;
        }

        /// <summary>
        /// Commits the changes made to the current UCS string.
        /// </summary>
        /// <returns></returns>
        private void CommitUCSEntry()
        {
            // check if index is still available
            var index = (uint)m_nupIndex.Value;

            string text = m_rtbUCSText.Text;
            if (m_rtbUCSText.Lines.Length > 1)
                text = m_rtbUCSText.Lines.Aggregate(string.Empty, (current, s) => current + " " + s);

            UCSManager.ModifyOrAddString(text, index);
            if (m_chkbxCopyToClipboard.Checked)
                Clipboard.SetText(index.ToString());
        }
        
        private void LoadEntries()
        {
            var entries = UCSManager.GetStrings();
            if (entries == null)
                return;
            m_dgvStrings.AllowUserToAddRows = true;
            foreach (var kvp in entries)
                AddRow(kvp.Key, kvp.Value);
            m_dgvStrings.AllowUserToAddRows = false;
        }

        private void AddRow(uint index, string text)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell { Value = index });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = text });
            m_dgvStrings.Rows.Add(row);
        }

        public void UnregisterEvents()
        {
            UCSManager.StringAdded -= OnStringAdded;
            UCSManager.StringModified -= OnStringModified;
            UCSManager.StringRemoved -= OnStringRemoved;
        }

        #region eventhandlers

        private void OnLoad(object sender, EventArgs e)
        {
            LoadEntries();
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            CommitUCSEntry();
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

        private void DgvStringsCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= 0 || e.RowIndex < 0)
                return;
            DataGridViewRow row = m_dgvStrings.Rows[e.RowIndex];
            uint index = (uint) row.Cells[0].Value;
            string text = (string)row.Cells[1].Value;
            UCSManager.ModifyString(text, index);
        }

        private void DgvStringsKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Delete) == Keys.Delete)
            {
                if (m_dgvStrings.SelectedCells.Count > 0)
                {
                    var cell = m_dgvStrings.SelectedCells[0];
                    if (cell.ColumnIndex != 0)
                        return;
                    uint index = (uint) cell.Value;
                    m_dgvStrings.Rows.RemoveAt(cell.RowIndex);
                    UCSManager.RemoveString(index);
                }
            }
        }

        #region UCSManager

        void OnStringRemoved(uint index)
        {
            int rowIdx = -1;
            for (int i = 0; i < m_dgvStrings.Rows.Count; i++)
            {
                DataGridViewRow row = m_dgvStrings.Rows[i];
                if ((uint)row.Cells[0].Value == index)
                {
                    rowIdx = i;
                    break;
                }
            }

            if (rowIdx >= 0)
                m_dgvStrings.Rows.RemoveAt(rowIdx);
        }

        void OnStringModified(uint index, string text)
        {
            for (int i = 0; i < m_dgvStrings.Rows.Count; i++)
            {
                DataGridViewRow row = m_dgvStrings.Rows[i];
                if ((uint)row.Cells[0].Value == index)
                {
                    if ((string)row.Cells[1].Value != text)
                        row.Cells[1].Value = text;
                    break;
                }
            }
        }

        void OnStringAdded(uint index, string text)
        {
            AddRow(index, text);
        }

        #endregion

        #endregion
    }
}
