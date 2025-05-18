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
using System.IO;
using System.Windows.Forms;
using cope;
using cope.DawnOfWar2.RelicChunky;
using cope.DawnOfWar2.RelicChunky.Chunks;
using cope.Extensions;

namespace ModTool.Core.PlugIns.RelicChunky
{
    // Todo: recode, this code is ancient!
    public partial class ACTNHandler : BaseHandler
    {
        #region fields

        protected static ACTNAction s_clipboard;
        protected ACTNChunk m_actionChunk;
        protected bool m_hasChanges;
        private string m_oldKey;

        #endregion fields

        public ACTNHandler()
        {
            InitializeComponent();
        }

        #region methods

        public override void LoadChunk(RelicChunk rc)
        {
            m_relicChunk = rc;
            m_actionChunk = new ACTNChunk(rc.RawData) {ChunkHeader = rc.ChunkHeader};
            m_actionChunk.InterpretRawData();

            foreach (ACTNAction act in m_actionChunk.Actions.Values)
            {
                m_lbxActions.Items.Add(act);
            }
        }

        public override void SaveChunk()
        {
            var newRaw = new byte[m_actionChunk.GetLength()];
            var ms = new MemoryStream(newRaw);
            m_actionChunk.WriteToStream(ms);
            ms.Close();
            m_relicChunk.RawData = newRaw;
            m_hasChanges = false;
        }

        private void SaveDelay()
        {
            if (m_lbxActions.SelectedItem == null)
                return;
            try
            {
                float old = ((ACTNAction) m_lbxActions.SelectedItem).Delay;
                if (m_tbxDelay.Text == string.Empty)
                {
                    if (old != 0)
                        m_hasChanges = true;
                    ((ACTNAction) m_lbxActions.SelectedItem).Delay = 0;
                }
                else
                {
                    float newval = Parser.ParseFloatSave(m_tbxDelay.Text);
                    ((ACTNAction) m_lbxActions.SelectedItem).Delay = newval;
                    if (old != newval)
                        m_hasChanges = true;
                }
            }
            catch
            {
                MessageBox.Show("Invalid delay value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #endregion methods

        #region properties

        public override bool HasChanges
        {
            get { return m_hasChanges; }
        }

        #endregion properties

        #region eventhandlers

        private void LbxActionsSelectedIndexChanged(object sender, EventArgs e)
        {
            m_dgvValues.Rows.Clear();
            if (m_lbxActions.SelectedItem == null)
            {
                m_tbxActionName.Text = string.Empty;
                m_tbxDelay.Text = string.Empty;
                return;
            }
            var act = (ACTNAction) m_lbxActions.SelectedItem;
            m_tbxDelay.Text = act.Delay.ToString().Replace(',', '.');
            m_tbxActionName.Text = act.Name;
            foreach (var kvp in act.Params)
            {
                if (m_dgvValues.Rows.Count > act.Params.Count)
                    break;
                m_dgvValues.Rows.Add(new object[] {kvp.Key, kvp.Value});
            }
        }

        private void DgvValuesCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var act = (ACTNAction) m_lbxActions.SelectedItem;
            var key = (string) m_dgvValues.Rows[e.RowIndex].Cells[0].Value;
            var value = (string) m_dgvValues.Rows[e.RowIndex].Cells[1].Value;
            if (e.ColumnIndex == 0)
            {
                if (m_oldKey != key)
                {
                    m_hasChanges = true;
                    act.Params.Remove(m_oldKey);
                }
            }
            if (act.Params.ContainsKey(key) && act.Params[key] != value)
                m_hasChanges = true;
            act.Params[key] = value;
        }

        private void DgvValuesUserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            var act = (ACTNAction) m_lbxActions.SelectedItem;
            var key = (string) e.Row.Cells[0].Value;
            var value = (string) e.Row.Cells[1].Value;
            try
            {
                act.Params[key] = value;
            }
            catch
            {
                MessageBox.Show("Invalid key/value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m_hasChanges = true;
        }

        private void DgvValuesCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            m_oldKey = (string) m_dgvValues.Rows[e.RowIndex].Cells[0].Value;
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            SaveChunk();
        }

        private void LbxActionsMouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < m_lbxActions.Items.Count; i++)
            {
                if (m_lbxActions.GetItemRectangle(i).Contains(e.Location))
                {
                    m_lbxActions.SelectedItem = m_lbxActions.Items[i];
                    return;
                }
            }
        }

        private void TbxDelayTextChanged(object sender, EventArgs e)
        {
            SaveDelay();
        }

        private void TbxActionNameTextChanged(object sender, EventArgs e)
        {
            if (m_lbxActions.SelectedItem == null)
                return;
            var act = (ACTNAction) m_lbxActions.SelectedItem;
            if (act.Name != m_tbxActionName.Text)
            {
                m_hasChanges = true;
                act.Name = m_tbxActionName.Text;
                m_lbxActions.Items.Clear();

                foreach (ACTNAction action in m_actionChunk.Actions.Values)
                {
                    m_lbxActions.Items.Add(action);
                }
                m_dgvValues.Rows.Clear();
                m_lbxActions.SelectedItem = act;
            }
        }

        private void ACTNHandler_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Dock = DockStyle.None;
            tableLayoutPanel2.Dock = DockStyle.None;
            tableLayoutPanel3.Dock = DockStyle.None;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel3.Dock = DockStyle.Fill;
        }

        #region action menu

        private void CopyActionToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_lbxActions.SelectedItem == null)
                return;
            s_clipboard = ((ACTNAction) m_lbxActions.SelectedItem).GClone();
        }

        private void CutActionToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_lbxActions.SelectedItem == null)
                return;
            s_clipboard = (ACTNAction) m_lbxActions.SelectedItem;
            m_lbxActions.Items.Remove(s_clipboard);
        }

        private void InsertActionToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (s_clipboard == null)
                return;
            ACTNAction tmp = s_clipboard.GClone();
            m_actionChunk.Actions.Add(tmp, tmp);
            m_lbxActions.Items.Add(tmp);
        }

        private void DeleteActionToolStripMenuItemClick(object sender, EventArgs e)
        {
            var tmp = (ACTNAction) m_lbxActions.SelectedItem;
            m_lbxActions.Items.Remove(m_lbxActions.SelectedItem);
            m_actionChunk.Actions.Remove(tmp);
        }

        #endregion action menu

        #endregion eventhandlers
    }
}