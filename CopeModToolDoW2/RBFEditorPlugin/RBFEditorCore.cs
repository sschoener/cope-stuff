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
using cope.DawnOfWar2;
using cope.DawnOfWar2.RelicAttribute;
using cope.Extensions;
using cope.UI;
using ModTool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RBFPlugin
{
    // Todo: complete rewrite for 2.0, this code is VERY ugly.
    public partial class RBFEditorCore : UserControl
    {
        private static AttributeValue s_clipboard;
        private static readonly string[] s_typeNames = new[] {"bool", "float", "int", "string", "table"};
        private static readonly char[] s_lineSeperator = new[] {'\n'};
        private ICollection<AttributeValue> m_collection;
        /// <summary>
        /// Determines whether this is used on a collection of RBF-values instead of a RBF-file
        /// </summary>
        private bool m_collectionMode;

        public RBFEditorCore()
        {
            InitializeComponent();
        }

        public event GenericHandler<bool> HasChangesChanged;

        #region methods

        public void Analyze(AttributeValue root)
        {
            Clear();
            TreeNode t = m_trvTables.Nodes.Add("GameData");
            t.Tag = root;
            foreach (var rbfData in (root.Data as AttributeTable))
            {
                AddToTree(rbfData, t);
            }
            m_trvTables.Sort();
            t.Expand();
        }

        public void Analyze(ICollection<AttributeValue> values)
        {
            Clear();
            TreeNode t = m_trvTables.Nodes.Add("GameData");
            m_collectionMode = true;
            m_collection = values;
            foreach (AttributeValue r in values)
            {
                AddToTree(r, t);
            }
            m_trvTables.Sort();
            t.Expand();
        }

        /// <summary>
        /// Clears everything (also removes collection).
        /// </summary>
        public void Clear()
        {
            m_collectionMode = false;
            m_collection = null;
            m_trvTables.Nodes.Clear();
            ClearDataGrid();
        }

        /// <summary>
        /// Adds the specified RBFValue as a child to the specified TreeNode.
        /// </summary>
        /// <param name="rbf"></param>
        /// <param name="t"></param>
        private static void AddToTree(AttributeValue rbf, TreeNode t)
        {
            var tmp = new TreeNode(rbf.Key) {Tag = rbf, Name = rbf.Key};
            t.Nodes.Add(tmp);
            if (rbf.DataType == AttributeDataType.Table)
            {
                foreach (var rbfData in rbf.Data as AttributeTable)
                {
                    AddToTree(rbfData, tmp);
                }
            }
        }

        private void ClearDataGrid()
        {
            m_dgvValues.Rows.Clear();
            m_cbxValue.Items.Clear();
            m_tbxKey.Text = string.Empty;
            m_cbxValue.Text = string.Empty;
            m_cbxDataType.Text = string.Empty;
        }

        private void UpdateDataGrid(AttributeValue current)
        {
            m_dgvValues.Rows.Clear();
            m_cbxValue.Items.Clear();
            m_tbxKey.Text = current.Key;
            m_cbxValue.Text = AttributeValue.ConvertDataToString(current);
            m_cbxDataType.Text = s_typeNames[(int) current.DataType];
            if (current.DataType == AttributeDataType.Table)
            {
                m_dgvValues.AllowUserToAddRows = true;
                int index = -1;
                foreach (AttributeValue attribValue in (current.Data as AttributeTable))
                {
                    var currentRow = (DataGridViewRow) m_dgvValues.RowTemplate.Clone();
                    // setting up the cells
                    var keyCell = new DataGridViewTextBoxCell {Value = attribValue.Key};
                    currentRow.Cells.Add(keyCell);

                    DataGridViewCell value;
                    switch (attribValue.DataType)
                    {
                        case AttributeDataType.Boolean:
                            value = new DataGridViewCheckBoxCell {Value = (bool)attribValue.Data};
                            break;
                        case AttributeDataType.String:
                            // filling the dropdown boxes
                            value = new DataGridViewComboxCell();
                            var cell = (DataGridViewComboxCell) value;
                            cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                            cell.Sorted = true;
                            if (index != -1 && m_dgvValues.Rows[index].Cells[0].Value.Equals(attribValue.Key))
                            {
                                var upperCell = m_dgvValues.Rows[index].Cells[1] as DataGridViewComboBoxCell;
                                if (upperCell != null)
                                    cell.Items.AddRange(upperCell.Items);
                            }

                            if (cell.Items.Count == 0)
                            {
                                // get preset-entries from the dictionary
                                cell.Items.AddRange(RBFDictionary.GetDictEntries(attribValue.Key));
                                // try to get strings from userpath -- else go search for paths on our own
                                GetPathsForComboBox(cell, attribValue.Key, attribValue.Data as string);
                            }
                            if (!cell.Items.Contains(attribValue.Data as string))
                                cell.Items.Add(attribValue.Data);
                            value.Value = AttributeValue.ConvertDataToString(attribValue);
                            break;
                        default:
                            value = new DataGridViewTextBoxCell {Value = AttributeValue.ConvertDataToString(attribValue)};
                            break;
                    }
                    currentRow.Cells.Add(value);

                    var type = new DataGridViewComboBoxCell();
                    type.Items.AddRange(new[] {"float", "int", "bool", "table", "string"});
                    type.Value = s_typeNames[(int) attribValue.DataType];
                    currentRow.Cells.Add(type);
                    type.ReadOnly = true;

                    index = m_dgvValues.Rows.Add(currentRow);
                    m_dgvValues.Rows[index].Tag = attribValue;
                }
            }
            else if (current.DataType == AttributeDataType.Boolean)
            {
                m_cbxValue.Items.Add("true");
                m_cbxValue.Items.Add("false");
            }
            m_dgvValues.Sort(m_dgvValues.Columns[0], ListSortDirection.Ascending);
        }

        private static string[] GetAttribFilesAbs(string absolutePath, string removeFromFront = "simulation\\attrib\\",
                                           string[] extensions = null, string[] excludeEndings = null,
                                           bool recursive = true)
        {
            if (FileManager.AttribTree != null)
            {
                FSNodeDir dir = FileManager.AttribTree.RootNode.GetSubDirByPath(absolutePath);
                if (dir != null)
                {
                    string[] files = recursive
                                         ? dir.GetRelativeFilePathsWithSubs(removeFromFront, ".", extensions, excludeEndings).
                                               ToArray()
                                         : dir.GetRelativeFilePaths(removeFromFront, ".", extensions, excludeEndings);
                    return files;
                }
            }
            return null;
        }

        private static string[] GetDataFilesAbs(string absolutePath, string removeFromFront = "", string[] extensions = null,
                                 string[] excludeEndings = null, bool recursive = true)
        {
            if (FileManager.DataTree != null)
            {
                FSNodeDir dir = FileManager.DataTree.RootNode.GetSubDirByPath(absolutePath);
                if (dir != null)
                {
                    string[] files;
                    if (recursive)
                        files =
                            dir.GetRelativeFilePathsWithSubs(removeFromFront, ".", extensions, excludeEndings).ToArray();
                    else
                        files = dir.GetRelativeFilePaths(removeFromFront, ".", extensions, excludeEndings);
                    return files;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns all file paths from a relative path in ATTRIB..
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        private static string[] GetAttribFiles(string relativePath)
        {
            if (FileManager.AttribTree != null)
            {
                FSNodeDir dir = FileManager.AttribTree.RootNode.GetSubDirByPath("simulation\\attrib\\" + relativePath);
                if (dir != null)
                {
                    string[] files = dir.GetRelativeFilePaths("simulation\\attrib\\", ".");
                    return files;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns all paths from a relative path in ATTRIB.
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        private static string[] GetAttribEntries(string relativePath)
        {
            if (FileManager.AttribTree != null)
            {
                FSNodeDir dir = FileManager.AttribTree.RootNode.GetSubDirByPath("simulation\\attrib\\" + relativePath);
                if (dir != null)
                {
                    string[] files = dir.GetRelativePaths("simulation\\attrib\\", ".");
                    return files;
                }
            }
            return null;
        }

        private static string[] GetDataFiles(string relativePath)
        {
            if (FileManager.DataTree != null)
            {
                FSNodeDir dir = FileManager.DataTree.RootNode.GetSubDirByPath(relativePath);
                if (dir != null)
                {
                    string[] files = dir.GetRelativeFilePaths(removeFromEnding: ".");
                    return files;
                }
            }
            return null;
        }

        private static string[] GetDataEntries(string relativePath)
        {
            if (FileManager.DataTree != null)
            {
                FSNodeDir dir = FileManager.DataTree.RootNode.GetSubDirByPath(relativePath);
                if (dir != null)
                {
                    string[] files = dir.GetRelativePaths(removeFromFileEnding: ".");
                    return files;
                }
            }
            return null;
        }

        private static bool FillComboBoxByUserPath(object o, string key)
        {
            var c = o as ComboBox.ObjectCollection;
            var c2 = o as DataGridViewComboBoxCell.ObjectCollection;
            HashSet<SearchPathInfo> paths = RBFDictionary.GetPaths(key);
            if (paths != null && paths.Count > 0)
            {
                foreach (var spi in paths)
                {
                    string[] values = null;
                    string removeFromFront = spi.RemoveFromFront ?? string.Empty;
                    switch (spi.Root)
                    {
                        case SearchPathRoot.Attrib:
                            values = GetAttribFilesAbs(spi.Path, removeFromFront, spi.ExtensionsToInclude, spi.EndingsToExclude);
                            break;
                        case SearchPathRoot.Data:
                            values = GetDataFilesAbs(spi.Path, removeFromFront, spi.ExtensionsToInclude, spi.EndingsToExclude);
                            break;
                    }
                    if (values == null)
                        continue;
                    if (c == null)
                        c2.AddRange(values);
                    else
                        c.AddRange(values);
                }
                return true;
            }
            return false;
        }

        private void GetPathsForComboBox(DataGridViewComboBoxCell cell, string key, string path)
        {
            if (!FillComboBoxByUserPath(cell.Items, key) && path.Contains('\\'))
            {
                string relativePath = path.SubstringBeforeLast('\\');
                string[] files = GetAttribFiles(relativePath);
                if (files != null)
                {
                    cell.Items.AddRange(files);
                    m_cbxValue.Items.AddRange(files);
                }
                files = GetDataFiles(relativePath);
                if (files != null)
                {
                    cell.Items.AddRange(files);
                    m_cbxValue.Items.AddRange(files);
                }
            }
        }

        protected void InsertValueIntoSelected(AttributeValue value)
        {
            if (m_trvTables.SelectedNode == null)
                return;
            if (m_collectionMode && m_trvTables.SelectedNode.Tag == null)
            {
                AttributeValue tmp = value.GClone();
                m_collection.Add(tmp);
                AddToTree(tmp, m_trvTables.Nodes[0]);
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);
                if (m_trvTables.SelectedNode != null && m_trvTables.SelectedNode.Tag != null)
                    UpdateDataGrid((AttributeValue) m_trvTables.SelectedNode.Tag);
            }
            else if (((AttributeValue) m_trvTables.SelectedNode.Tag).DataType == AttributeDataType.Table)
            {
                AttributeValue tmp = value.GClone();
                AttributeTable table = (AttributeTable) (((AttributeValue) m_trvTables.SelectedNode.Tag).Data);
                table.AddValue(tmp);
                AddToTree(tmp, m_trvTables.SelectedNode);
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);
                if (m_trvTables.SelectedNode != null && m_trvTables.SelectedNode.Tag != null)
                    UpdateDataGrid((AttributeValue) m_trvTables.SelectedNode.Tag);
            }
        }

        #endregion methods

        #region Eventhandlers

        private void DgvValuesSelectionChanged(object sender, EventArgs e)
        {
            if (m_dgvValues.SelectedRows.Count <= 0)
                return;
            DataGridViewRow row = m_dgvValues.SelectedRows[0];
            if ((row.Cells[2].Value as string) == "int")
            {
                var value = (uint) int.Parse(row.Cells[1].Value.ToString());
                m_rtbCurrentUCS.Text = UCSManager.HasString(value) ? UCSManager.GetString(value) : string.Empty;
            }
            else
                m_rtbCurrentUCS.Text = string.Empty;
        }

        private void DgvValuesCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            foreach (DataGridViewRow r in m_dgvValues.Rows)
                r.Selected = false;
            DataGridViewRow row = m_dgvValues.Rows[e.RowIndex];
            row.Selected = true;
        }

        private void DgvValuesCellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = m_dgvValues.Rows[e.RowIndex];
            var rbf = row.Tag as AttributeValue;
            if (rbf == null)
                return;
            if (rbf.DataType == AttributeDataType.Table && m_trvTables.SelectedNode != null)
                m_trvTables.SelectedNode = m_trvTables.SelectedNode.Nodes[rbf.Key];
        }

        private void DgvValuesCellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow r in m_dgvValues.Rows)
                r.Selected = false;
            if (e.RowIndex < 0 || e.RowIndex >= m_dgvValues.Rows.Count)
                return;
            DataGridViewRow row = m_dgvValues.Rows[e.RowIndex];
            row.Selected = true;
        }

        #region treeview

        private void TrvTablesAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
                return;
            var current = e.Node.Tag as AttributeValue;
            UpdateDataGrid(current);
            if (current.DataType == AttributeDataType.Integer)
            {
                var value = (uint) (int) current.Data;
                m_rtbCurrentUCS.Text = UCSManager.HasString(value) ? UCSManager.GetString(value) : string.Empty;
            }
        }

        private void TrvTablesNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            m_trvTables.SelectedNode = e.Node;
        }

        #endregion treeview

        #region RBF data

        private void CbxDataTypeTextUpdate(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null || m_trvTables.SelectedNode.Tag == null)
                return;
            var attribValue = (AttributeValue) m_trvTables.SelectedNode.Tag;

            string dataTypeName = m_cbxDataType.Text;
            AttributeDataType oldType = attribValue.DataType;
            AttributeDataType newType = AttributeValue.ConvertStringToType(dataTypeName);
            if (newType == AttributeDataType.Invalid)
            {
                 UIHelper.ShowError("The DataType " + dataTypeName + " is invalid!");
                return;
            }
            attribValue.DataType = newType;
            if (newType != oldType)
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);
            UpdateDataGrid(attribValue);
        }

        private void CbxValueTextUpdate(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null || m_trvTables.SelectedNode.Tag == null)
                return;
            var attribValue = (AttributeValue) m_trvTables.SelectedNode.Tag;

            object oldData = attribValue.Data;
            object newData;
            try
            {
                newData = AttributeValue.ConvertStringToData(m_cbxValue.Text, attribValue.DataType);
            }
            catch(Exception ex)
            {
                 UIHelper.ShowError("Failed to assign value " + m_cbxValue.Text + " as type " + attribValue.DataType);
                return;
            }
            if (newData == null)
            {
                 UIHelper.ShowError("Failed to assign value " + m_cbxValue.Text + " as type " + attribValue.DataType);
                return;
            }

            attribValue.Data = newData;
            if (newData != oldData)
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);

            string value = m_cbxValue.Text;
            if (value.Contains('\\'))
            {
                string relativePath = value.SubstringBeforeLast('\\');
                string[] files = GetAttribFiles(relativePath);
                if (files != null)
                    m_cbxValue.Items.AddRange(files);
                files = GetDataFiles(relativePath);
                if (files != null)
                    m_cbxValue.Items.AddRange(files);
            }
        }

        private void TbxKeyTextChanged(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null || m_trvTables.SelectedNode.Tag == null)
                return;
            var rbfv = (AttributeValue) m_trvTables.SelectedNode.Tag;
            var x = (TextBox) sender;
            string old = rbfv.Key;
            rbfv.Key = x.Text;
            if (old != rbfv.Key)
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);

            m_cbxValue.Items.Clear();
            if (!FillComboBoxByUserPath(m_cbxValue.Items, m_tbxKey.Text))
                m_cbxValue.Items.AddRange(RBFDictionary.GetDictEntries(m_tbxKey.Text));
        }

        private void DgvValuesCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            DataGridViewRow row = m_dgvValues.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            var attribValue = (AttributeValue) row.Tag;
            if (attribValue == null)
                return;
            // key
            if (e.ColumnIndex == 0)
            {
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);
                attribValue.Key = cell.Value.ToString();
                if (cell is DataGridViewComboBoxCell)
                {
                    var combo = row.Cells[1] as DataGridViewComboBoxCell;
                    string value = combo.Value.ToString();
                    for (int i = 0; i < combo.Items.Count; i++)
                        if (combo.Items[i].ToString() != value)
                            combo.Items.RemoveAt(i--);
                    if (!FillComboBoxByUserPath(combo.Items, cell.Value.ToString()))
                        combo.Items.AddRange(RBFDictionary.GetDictEntries(cell.Value.ToString()));
                }
            }
                // value
            else if (e.ColumnIndex == 1)
            {
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);
                if (cell == null || cell.Value == null)
                {
                    attribValue.Data = AttributeValue.ConvertStringToData(string.Empty, attribValue.DataType);
                    return;
                }
                object oldData = attribValue.Data;
                try
                {
                    attribValue.Data = AttributeValue.ConvertStringToData(cell.Value.ToString(), attribValue.DataType);
                }
                catch (Exception ex)
                {
                    attribValue.Data = oldData;
                     UIHelper.ShowError("Invalid value!");
                }

                if (cell is DataGridViewComboBoxCell)
                {
                    var combo = cell as DataGridViewComboBoxCell;
                    string value = combo.Value.ToString();
                    for (int i = 0; i < combo.Items.Count; i++)
                        if (combo.Items[i].ToString() != value)
                            combo.Items.RemoveAt(i--);
                    if (value.Contains('\\'))
                    {
                        string relativePath = value.SubstringBeforeLast('\\');
                        string[] files = GetAttribFiles(relativePath);
                        if (files != null)
                            combo.Items.AddRange(files);
                        files = GetDataFiles(relativePath);
                        if (files != null)
                            combo.Items.AddRange(files);
                    }
                }
            }
                // type
            else if (e.ColumnIndex == 2)
            {
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);
                attribValue.DataType = AttributeValue.ConvertStringToType(cell.Value.ToString());
            }
        }

        private void DgvValuesCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var cell = m_dgvValues.CurrentCell as DataGridViewComboBoxCell;
            if (cell == null)
                return;
            if (!cell.Items.Contains(e.FormattedValue))
            {
                cell.Items.Insert(0, e.FormattedValue);
                if (m_dgvValues.IsCurrentCellDirty)
                    m_dgvValues.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            cell.Value = e.FormattedValue;
        }

        private void DgvValuesEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var c = e.Control as DataGridViewComboxEditingControl;
            if (c == null)
                return;
            c.DropDownStyle = ComboBoxStyle.DropDown;
            if (RBFSettings.UseAutoCompletion)
            {
                c.TextChanged += ComboBoxTextChanged;
                c.AutoCompleteMode = AutoCompleteMode.Append;
                c.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        void ComboBoxTextChanged(object sender, EventArgs e)
        {
            var cell = m_dgvValues.CurrentCell as DataGridViewComboxCell;
            if (cell == null)
                return;
            var row = m_dgvValues.Rows[cell.RowIndex];
            var attribValue = row.Tag as AttributeValue;
            if (attribValue == null)
                return;
            var cbx = sender as DataGridViewComboxEditingControl;
            if (cbx == null)
                return;
            var path = cbx.Text;
            if (!path.EndsWith('\\'))
                return;


            int ss = cbx.SelectionStart;
            int sl = cbx.SelectionLength;
            cbx.AutoCompleteCustomSource.Clear();
            string relativePath = path.SubstringBeforeLast('\\');
            string[] files = GetAttribEntries(relativePath);
            if (files != null)
                cbx.AutoCompleteCustomSource.AddRange(files);
            files = GetDataEntries(relativePath);
            if (files != null)
                cbx.AutoCompleteCustomSource.AddRange(files);
            cbx.SelectionStart = ss;
            cbx.SelectionLength = sl;
        }

        #endregion RBF data

        #region ContextMenuStrip tree

        private void CopyToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode != null && m_trvTables.SelectedNode.Tag != null)
                s_clipboard = ((AttributeValue) m_trvTables.SelectedNode.Tag).GClone();
        }

        private void CopyValuePathToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode != null && m_trvTables.SelectedNode.Tag != null)
                Clipboard.SetText(((AttributeValue) m_trvTables.SelectedNode.Tag).GetPath(), TextDataFormat.Text);
        }

        private void DeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null)
                return;
            AttributeValue rbfv = ((AttributeValue) m_trvTables.SelectedNode.Tag);
            if (rbfv.Parent == null)
            {
                 UIHelper.ShowError(@"You cannot delete the Root-table!");
                return;
            }
            rbfv.Parent.RemoveValue(rbfv);
            m_trvTables.SelectedNode.Remove();
            if (HasChangesChanged != null)
                HasChangesChanged(this, true);
        }

        private void InsertToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null || m_trvTables.SelectedNode.Tag == null)
                return;
            if (s_clipboard != null)
            {
                if (s_clipboard.DataType != AttributeDataType.Table)
                {
                    var attribValue = m_trvTables.SelectedNode.Tag as AttributeValue;
                    attribValue.DataType = s_clipboard.DataType;
                    attribValue.Data = s_clipboard.Data;
                    m_trvTables.SelectedNode.Nodes.Clear();
                }
                else
                {
                    var tmpVal = (AttributeValue) m_trvTables.SelectedNode.Tag;
                    tmpVal.Data = ((AttributeTable) s_clipboard.Data).GClone();
                    TreeNode tnTmp = m_trvTables.SelectedNode.Parent;
                    m_trvTables.SelectedNode.Remove();
                    AddToTree(tmpVal, tnTmp);
                }

                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);

                if (m_trvTables.SelectedNode != null && m_trvTables.SelectedNode.Tag != null)
                {
                    UpdateDataGrid((AttributeValue) m_trvTables.SelectedNode.Tag);
                }
            }
        }

        private void InsertIntoToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null)
                return;
            if (s_clipboard != null)
            {
                InsertValueIntoSelected(s_clipboard);
            }
        }

        private void InsertValueIntoAllSubtablesToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null)
                return;
            if (s_clipboard != null && ((AttributeValue) m_trvTables.SelectedNode.Tag).DataType == AttributeDataType.Table)
            {
                foreach (TreeNode tn in m_trvTables.SelectedNode.Nodes)
                {
                    if (tn.Tag == null)
                        continue;
                    var rbfv = tn.Tag as AttributeValue;
                    if (rbfv.DataType != AttributeDataType.Table)
                        continue;
                    AttributeValue tmp = s_clipboard.GClone();
                    AttributeTable table = (AttributeTable) rbfv.Data;
                    if (table != null)
                        table.AddValue(tmp);
                    AddToTree(tmp, tn);
                }
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);
                if (m_trvTables.SelectedNode != null && m_trvTables.SelectedNode.Tag != null)
                    UpdateDataGrid((AttributeValue) m_trvTables.SelectedNode.Tag);
            }
        }

        private void CutTableToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null || m_trvTables.SelectedNode.Tag == null)
            {
                 UIHelper.ShowError("You haven't selected a valid value.");
                return;
            }
            s_clipboard = (AttributeValue) m_trvTables.SelectedNode.Tag;
            if (s_clipboard.Parent != null)
                s_clipboard.Parent.RemoveValue(s_clipboard);
            m_trvTables.SelectedNode.Remove();
            if (HasChangesChanged != null)
                HasChangesChanged(this, true);
            if (m_trvTables.SelectedNode != null && m_trvTables.SelectedNode.Tag != null)
                UpdateDataGrid((AttributeValue) m_trvTables.SelectedNode.Tag);
        }

        private void AddValuesToRBFDictionaryToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null)
                return;
            if (m_collectionMode)
            {
                 UIHelper.ShowError("Does not work in RBF-library.");
                return;
            }
            var rbfv = m_trvTables.SelectedNode.Tag as AttributeValue;
            if (rbfv.DataType == AttributeDataType.String)
            {
                RBFDictionary.AddDictEntry(rbfv.Key, rbfv.Data as string);
            }
            else if (rbfv.DataType == AttributeDataType.Table)
            {
                string key = rbfv.Key;
                foreach (var rbfData in (AttributeTable)rbfv.Data)
                {
                    if (rbfData.DataType != AttributeDataType.String)
                        continue;
                    RBFDictionary.AddDictEntry(key, (string) rbfData.Data);
                }
            }
        }

        private void CopyAsCorsixstringToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null || (m_trvTables.SelectedNode.Tag == null && !m_collectionMode))
                return;
            var output = new StringBuilder(255);
            if (m_collectionMode && m_trvTables.SelectedNode.Tag == null)
            {
                foreach (AttributeValue value in m_collection)
                {
                    output.Append(CorsixStyleConverter.ToCorsixStyle(value));
                    output.Append('\n');
                }
            }
            else
            {
                var topNode = (AttributeValue) m_trvTables.SelectedNode.Tag;
                output.Append(CorsixStyleConverter.ToCorsixStyle(topNode));
            }
            Clipboard.SetText(output.ToString());
        }

        private void InsertValueFromCorsixStringToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_trvTables.SelectedNode == null)
                return;
            if (((AttributeValue) m_trvTables.SelectedNode.Tag).DataType != AttributeDataType.Table && !m_collectionMode)
                return;
            var parser = new RBFConvParserForm();
            if (parser.ShowDialog() == DialogResult.OK)
            {
                if (parser.m_rbfConvParser.Output.Count == 0)
                    return;
                foreach (AttributeValue rbfv in parser.m_rbfConvParser.Output)
                {
                    if (m_collectionMode)
                    {
                        m_collection.Add(rbfv);
                        AddToTree(rbfv, m_trvTables.Nodes[0]);
                    }
                    else
                    {
                        AttributeTable table = (AttributeTable) (((AttributeValue) m_trvTables.SelectedNode.Tag).Data);
                        table.AddValue(rbfv);
                        AddToTree(rbfv, m_trvTables.SelectedNode);
                    }
                }
                if (HasChangesChanged != null)
                    HasChangesChanged(this, true);
                if (m_trvTables.SelectedNode != null && m_trvTables.SelectedNode.Tag != null)
                    UpdateDataGrid((AttributeValue) m_trvTables.SelectedNode.Tag);
            }
        }

        // RBF Library link

        private void InsertFromLibraryToolStripMenuItemDropDownOpening(object sender, EventArgs e)
        {
            insertFromLibraryToolStripMenuItem.DropDownItems.Clear();
            TreeNode selected = m_trvTables.SelectedNode;
            if (selected == null)
                return;
            SortedDictionary<string, RBFLibEntry> entries = RBFLibrary.GetEntriesForTag(selected.Text);
            if (entries == null)
                return;

            foreach (var entry in entries)
            {
                ToolStripMenuItem menu = insertFromLibraryToolStripMenuItem;
                if (entry.Value.Submenu != null)
                {
                    string subname = entry.Value.Submenu;
                    if (!insertFromLibraryToolStripMenuItem.DropDownItems.ContainsKey(subname))
                    {
                        var s = new ToolStripMenuItem(subname) {Name = subname};
                        insertFromLibraryToolStripMenuItem.DropDownItems.Add(s);
                        menu = s;
                    }
                    else
                        menu = insertFromLibraryToolStripMenuItem.DropDownItems[subname] as ToolStripMenuItem;
                }
                ToolStripItem rbfLibItem = menu.DropDownItems.Add(entry.Key);
                rbfLibItem.Click += RBFLibItemClick;
            }
        }

        private void RBFLibItemClick(object sender, EventArgs e)
        {
            var item = sender as ToolStripItem;
            TreeNode selected = m_trvTables.SelectedNode;
            if (selected == null)
                return;
            SortedDictionary<string, RBFLibEntry> entries = RBFLibrary.GetEntriesForTag(selected.Text);
            if (entries == null || !entries.ContainsKey(item.Text))
                return;
            List<AttributeValue> values = entries[item.Text].Values;
            foreach (AttributeValue value in values)
                InsertValueIntoSelected(value);
        }

        private void CopyIntoLibraryToolStripMenuItemClick(object sender, EventArgs e)
        {
            TreeNode selected = m_trvTables.SelectedNode;
            if (selected == null || selected.Tag == null)
                return;

            var dlg = new AddToLibrary();
            if (selected.Parent != null)
                dlg.Tags = new[] {selected.Parent.Text + '\n'};
            dlg.ValueName = selected.Text;
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                dlg.Dispose();
                return;
            }
            var tmp = new RBFLibEntry
                          {
                              Name = dlg.ValueName,
                              Tags = dlg.Tags,
                              TagGroups = dlg.TagGroups
                          };
            if (dlg.SubMenu != string.Empty)
                tmp.Submenu = dlg.SubMenu;
            tmp.Values = new List<AttributeValue> {selected.Tag as AttributeValue};

            if (RBFLibrary.GetEntry(tmp.Name) == null)
                RBFLibrary.AddEntry(tmp);
            else if (dlg.AddTags)
            {
                RBFLibEntry entry = RBFLibrary.GetEntry(tmp.Name);
                foreach (string t in tmp.Tags)
                    RBFLibrary.AddEntryToTag(entry, t);
            }
            dlg.Dispose();
        }

        #endregion ContextMenuStrip tree

        #region ContextMenuStrip RBF data

        private void AddValueToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_dgvValues.SelectedRows.Count <= 0 || (string) m_dgvValues.SelectedRows[0].Cells[2].Value != "string")
                return;
            RBFDictionary.AddDictEntry((string) m_dgvValues.SelectedRows[0].Cells[0].Value,
                                       (string) m_dgvValues.SelectedRows[0].Cells[1].Value);
        }

        private void TryToOpenFileByValueToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_dgvValues.SelectedRows.Count <= 0 || (string) m_dgvValues.SelectedRows[0].Cells[2].Value != "string")
                return;
            if (m_dgvValues.SelectedRows[0].Cells[1].Value == null)
            {
                 UIHelper.ShowError("The selected value is not a valid path.");
                return;
            }

            string path = (string) m_dgvValues.SelectedRows[0].Cells[1].Value + ".rbf";
            if (FileManager.AttribTree != null)
            {
                FSNodeFile node = FileManager.AttribTree.RootNode.GetFileByPath("simulation\\attrib\\" + path);
                if (node == null)
                {
                     UIHelper.ShowError(
                        "Could not open the selected file. Maybe it does not exist or the tool could not find it.");
                    return;
                }
                UniFile file = node.GetUniFile();
                if (file == null)
                {
                     UIHelper.ShowError(
                        "Could not open the selected file. Maybe it does not exist or the tool could not find it.");
                    return;
                }
                if (!FileManager.AllowOpeningFilesTwice && FileManager.OpenFiles.Contains(file.FilePath))
                {
                    UIHelper.ShowWarning(
                        "You're already editing the file you're trying to load! You cannot reopen it." +
                        " View the options-menu to remove this limitation.");
                    return;
                }
                FileManager.LoadFile(file);
                return;
            }
             UIHelper.ShowError(
                "Could not open the selected file. There's no file-tree available to look the path up." +
                " This might be because there's no mod loaded.");
        }

        #endregion ContextMenuStrip RBF data

        #endregion Eventhandlers

        #region events

        public new event KeyEventHandler KeyDown
        {
            add { m_trvTables.KeyDown += value; }
            remove { }
        }

        #endregion events
    }
}