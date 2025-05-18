using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using cope.IO;
using cope.DawnOfWar2.Formats.RelicBinary;
using cope.Script.LUA;
using cope.StringExt;

namespace cope.DawnOfWar2.Controls
{
    public partial class RBFEditor : UserControl, IEditor
    {
        RelicBinaryFile RBF;
        string[] type_names = new string[] {"bool", "float", "int", "string", "table"};

        public delegate void RBFVExportHandler(RBFValue rbfv_);

        public delegate RBFValue RBFVImportHandler();

        public RBFEditor()
        {
            InitializeComponent();
        }

        private void AnalyzeFile()
        {
            trvTables.Nodes.Clear();
            TreeNode t = trvTables.Nodes.Add("GameData");
            t.Tag = RBF.Root;
            foreach (KeyValuePair<string,lua_value> kvp in ((RBFTable)RBF.Root.Value).Values)
            {
                AddToTree((RBFValue)kvp.Value, t);
            }
        }

        private void AddToTree(RBFValue rbf, TreeNode t)
        {
            TreeNode tmp = new TreeNode(rbf.Key);
            tmp.Tag = rbf;
            t.Nodes.Add(tmp);
            if (rbf.Type == lua_value_type.lvtTable)
            {
                foreach (KeyValuePair<string, lua_value> kvp in ((RBFTable)rbf.Value).Values)
                {
                    AddToTree((RBFValue)kvp.Value, tmp);
                }
            }
        }

        private lua_value_type GetTypeFromString(string s)
        {
            s = s.ToLower();
            if (s == "bool")
                return lua_value_type.lvtBoolean;
            else if (s == "float")
                return lua_value_type.lvtFloat;
            else if (s == "integer")
                return lua_value_type.lvtInteger;
            else if (s == "string")
                return lua_value_type.lvtString;
            else if (s == "table")
                return lua_value_type.lvtTable;
            throw new Exception("Unknown RBF-Type-String: " + s + " !");
        }

        private void ClearDataGrid()
        {
            dgvValues.Rows.Clear();
            cbxValue.Items.Clear();
            tbxKey.Text = "";
            cbxValue.Text = "";
            cbxDataType.Text = "";
        }

        private void UpdateDataGrid(RBFValue current)
        {
            dgvValues.Rows.Clear();
            dgvValues.AllowUserToAddRows = false;
            cbxValue.Items.Clear();
            tbxKey.Text = current._Name;
            cbxValue.Text = current.Value.ToString();
            cbxDataType.Text = type_names[(int)current.Type];
            if (current.Type == lua_value_type.lvtTable)
            {
                dgvValues.AllowUserToAddRows = true;
                foreach (lua_value lv in ((RBFTable)current.Value).Values.Values)
                {
                    DataGridViewRow tmp = (DataGridViewRow)dgvValues.RowTemplate.Clone();

                    // setting up the cells
                    DataGridViewTextBoxCell key = new DataGridViewTextBoxCell();
                    key.Value = lv._Name;
                    tmp.Cells.Add(key);

                    DataGridViewCell value;
                    switch (lv.Type)
                    {
                        case lua_value_type.lvtBoolean:
                            value = new DataGridViewComboBoxCell();
                            value.Value = ((RBFValue)lv).Value.ToString().ToLower(); ;
                            ((DataGridViewComboBoxCell)value).Items.AddRange(new string[]{"true", "false"});
                            ((DataGridViewComboBoxCell)value).DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                            tmp.Cells.Add(value);
                            break;
                        default:
                            value = new DataGridViewTextBoxCell();
                            value.Value = ((RBFValue)lv).Value.ToString();
                            tmp.Cells.Add(value);
                            break;
                    }

                    DataGridViewComboBoxCell type = new DataGridViewComboBoxCell();
                    type.Items.AddRange(new string[] {"float", "int", "bool", "table", "string"});
                    type.Value = type_names[(int)lv.Type];
                    tmp.Cells.Add(type);

                    int index = dgvValues.Rows.Add(tmp);
                    dgvValues.Rows[index].Tag = lv;
                }
            }
            else if (current.Type == lua_value_type.lvtBoolean)
            {
                cbxValue.Items.Add("true");
                cbxValue.Items.Add("false");
            }
            dgvValues.Sort(dgvValues.Columns[0], ListSortDirection.Ascending);
        }

        public void Close()
        {
            if (RBF != null)
                RBF.Close();
        }

        #region IEditor<RelicBinaryFile> Member

        public event FileActionEventHandler FileSaved;

        public event FileActionEventHandler FileLoaded;

        public event EventHandler EditorClosed;

        public void LoadFile(UniFile file)
        {
            RBF = (RelicBinaryFile)file;
            AnalyzeFile();
            if (FileLoaded != null)
            {
                FileActionEventArgs e = new FileActionEventArgs(FileActionType.FAT_Load, RBF);
                FileLoaded(this, e);
            }
            RBF.Close();
            trvTables.Sort();
        }

        public void SaveFile(string path)
        {
            RBF.WriteDataTo(path);
            if (FileSaved != null)
            {
                FileActionEventArgs e = new FileActionEventArgs(FileActionType.FAT_Save, RBF, RBF.Tag);
                FileSaved(this, e);
            }
            RBF.Close();
        }

        public UniFile GetFile()
        {
            return RBF;
        }

        public string FileName
        {
            get
            {
                return RBF.FileName;
            }
        }

        #endregion

        #region Eventhandlers

        private void btnSaveRBF_Click(object sender, EventArgs e)
        {
            SaveFile(RBF.FilePath);
        }

        private void trvTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateDataGrid((RBFValue)e.Node.Tag);
        }

        private void cbxDataType_TextUpdate(object sender, EventArgs e)
        {
            RBFValue rbfv = (RBFValue)trvTables.SelectedNode.Tag;
            ComboBox x = (ComboBox)sender;
            rbfv.Type = GetTypeFromString(x.Text);
            UpdateDataGrid(rbfv);
        }

        private void cbxValue_TextUpdate(object sender, EventArgs e)
        {
            RBFValue rbfv = (RBFValue)trvTables.SelectedNode.Tag;
            ComboBox x = (ComboBox)sender;
            rbfv.SetValueByString(x.Text);
        }

        private void tbxKey_TextChanged(object sender, EventArgs e)
        {
            RBFValue rbfv = (RBFValue)trvTables.SelectedNode.Tag;
            TextBox x = (TextBox)sender;
            rbfv.Key = x.Text;
        }

        private void dgvValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            // key
            if (e.ColumnIndex == 0)
            {
                ((RBFValue)dgvValues.Rows[e.RowIndex].Tag).Key = dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            // value
            else if (e.ColumnIndex == 1)
            {
                if (dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    ((RBFValue)dgvValues.Rows[e.RowIndex].Tag).SetValueByString("");
                else
                    ((RBFValue)dgvValues.Rows[e.RowIndex].Tag).SetValueByString(dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
            // type
            else if (e.ColumnIndex == 2)
            {
                ((RBFValue)dgvValues.Rows[e.RowIndex].Tag).Type = GetTypeFromString(dgvValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void dgvValues_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            ((RBFTable)((RBFValue)e.Row.Tag).Parent).remove_value((RBFValue)e.Row.Tag);
            ((RBFValue)e.Row.Tag).Parent = null;
            AnalyzeFile();
        }

        private void dgvValues_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RBFValue tmp = new RBFValue(GetTypeFromString(e.Row.Cells[2].Value.ToString()), e.Row.Cells[0].Value.ToString(), e.Row.Cells[1].Value.ToString());
            tmp.Parent = (RBFTable)((RBFValue)trvTables.SelectedNode.Tag).Value;
            ((RBFTable)((RBFValue)trvTables.SelectedNode.Tag).Value).add_value(tmp);
        }

        private void RBFEditor_EnabledChanged(object sender, EventArgs e)
        {
            if (RBF != null)
                RBF.Close();
        }

        private void trvTables_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            trvTables.SelectedNode = e.Node;
        }

        #region ContextMenuStrip

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RBFVExport != null)
                RBFVExport(((RBFValue)trvTables.SelectedNode.Tag).GClone());
        }

        private void copyValuePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(((RBFValue)trvTables.SelectedNode.Tag).ValuePath, TextDataFormat.Text);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((RBFValue)trvTables.SelectedNode.Tag).Parent == null)
            {
                MessageBox.Show("You cannot delete the ROOT-table!");
                return;
            }
            ((RBFTable)((RBFValue)trvTables.SelectedNode.Tag).Parent).remove_value(((RBFValue)trvTables.SelectedNode.Tag));
            ((RBFValue)trvTables.SelectedNode.Tag).Parent = null;
            trvTables.SelectedNode.Remove();
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RBFVImport == null)
                return;
            RBFValue clipboard = RBFVImport();
            if (clipboard != null)
            {
                if (clipboard.Type != lua_value_type.lvtTable)
                {
                    ((RBFValue)trvTables.SelectedNode.Tag).Value = clipboard.Value;
                    trvTables.SelectedNode.Nodes.Clear();
                }
                else
                {
                    RBFValue tmp_val = (RBFValue)trvTables.SelectedNode.Tag;
                    tmp_val.Value = ((RBFTable)clipboard.Value).GClone();
                    TreeNode tn_tmp = trvTables.SelectedNode.Parent;
                    trvTables.SelectedNode.Remove();
                    AddToTree(tmp_val, tn_tmp);
                }

                if (trvTables.SelectedNode != null && trvTables.SelectedNode.Tag != null)
                {
                    UpdateDataGrid((RBFValue)trvTables.SelectedNode.Tag);
                }
            }
        }

        private void insertIntoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RBFVImport == null)
                return;
            RBFValue clipboard = RBFVImport();
            if (clipboard != null && ((RBFValue)trvTables.SelectedNode.Tag).Type == lua_value_type.lvtTable)
            {
                RBFValue tmp = clipboard.GClone();
                tmp.Parent = (RBFTable)(((RBFValue)trvTables.SelectedNode.Tag).Value);
                AddToTree(tmp, trvTables.SelectedNode);
                if (trvTables.SelectedNode != null && trvTables.SelectedNode.Tag != null)
                    UpdateDataGrid((RBFValue)trvTables.SelectedNode.Tag);
            }

        }

        private void cutTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RBFVExport == null)
                return;
            if (((RBFValue)trvTables.SelectedNode.Tag).Type == lua_value_type.lvtTable)
            {
                RBFValue clipboard = (RBFValue)trvTables.SelectedNode.Tag;
                clipboard.Parent = null;
                RBFVExport(clipboard);
                trvTables.SelectedNode.Remove();
                if (trvTables.SelectedNode != null && trvTables.SelectedNode.Tag != null)
                    UpdateDataGrid((RBFValue)trvTables.SelectedNode.Tag);
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(RBF.FilePath);
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditorClosed != null)
                EditorClosed(this, null);
        }

        #endregion

        #endregion

        #region events

        public event RBFVExportHandler RBFVExport;

        public event RBFVImportHandler RBFVImport;

        #endregion
    }
}
