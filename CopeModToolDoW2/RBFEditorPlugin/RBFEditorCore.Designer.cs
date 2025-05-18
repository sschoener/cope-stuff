using System.Windows.Forms;
using ModTool.Core;

namespace RBFPlugin
{
    partial class RBFEditorCore
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._cms_dgvValues = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryToOpenFileByValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsCorsixstringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyIntoLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cms_TableTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addValuesToRBFDictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyValuePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertFromLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertValueFromCorsixStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertIntoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertValueIntoAllSubtablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMainRBFEditor = new System.Windows.Forms.Panel();
            this.spcRBFEditor = new System.Windows.Forms.SplitContainer();
            this.m_trvTables = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbaKey = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labValue = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labType = new System.Windows.Forms.Label();
            this.m_tbxKey = new System.Windows.Forms.TextBox();
            this.m_cbxValue = new System.Windows.Forms.ComboBox();
            this.m_cbxDataType = new System.Windows.Forms.ComboBox();
            this.m_dgvValues = new System.Windows.Forms.DataGridView();
            this.cohKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cohValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cohType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.m_rtbCurrentUCS = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._cms_dgvValues.SuspendLayout();
            this._cms_TableTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcRBFEditor)).BeginInit();
            this.spcRBFEditor.Panel1.SuspendLayout();
            this.spcRBFEditor.Panel2.SuspendLayout();
            this.spcRBFEditor.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvValues)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cms_dgvValues
            // 
            this._cms_dgvValues.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addValueToolStripMenuItem,
            this.tryToOpenFileByValueToolStripMenuItem});
            this._cms_dgvValues.Name = "_cms_dgvValues";
            this._cms_dgvValues.Size = new System.Drawing.Size(210, 48);
            // 
            // addValueToolStripMenuItem
            // 
            this.addValueToolStripMenuItem.Image = global::RBFPlugin.Properties.Resources.bookmarks;
            this.addValueToolStripMenuItem.Name = "addValueToolStripMenuItem";
            this.addValueToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.addValueToolStripMenuItem.Text = "Add Value To Dictionary";
            this.addValueToolStripMenuItem.ToolTipText = "Adds the selected value to the corresponding key in the RBF dictionary.";
            this.addValueToolStripMenuItem.Click += new System.EventHandler(this.AddValueToolStripMenuItemClick);
            // 
            // tryToOpenFileByValueToolStripMenuItem
            // 
            this.tryToOpenFileByValueToolStripMenuItem.Image = global::RBFPlugin.Properties.Resources.document_open;
            this.tryToOpenFileByValueToolStripMenuItem.Name = "tryToOpenFileByValueToolStripMenuItem";
            this.tryToOpenFileByValueToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.tryToOpenFileByValueToolStripMenuItem.Text = "Try To Open File By Value";
            this.tryToOpenFileByValueToolStripMenuItem.ToolTipText = "Try to open the file specified by the value of the current cell.";
            this.tryToOpenFileByValueToolStripMenuItem.Click += new System.EventHandler(this.TryToOpenFileByValueToolStripMenuItemClick);
            // 
            // cutTableToolStripMenuItem
            // 
            this.cutTableToolStripMenuItem.Image = global::RBFPlugin.Properties.Resources.edit_cut;
            this.cutTableToolStripMenuItem.Name = "cutTableToolStripMenuItem";
            this.cutTableToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutTableToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.cutTableToolStripMenuItem.Text = "Cut Value";
            this.cutTableToolStripMenuItem.ToolTipText = "Copies a node to the clipboard and deletes it from the RBF.";
            this.cutTableToolStripMenuItem.Click += new System.EventHandler(this.CutTableToolStripMenuItemClick);
            // 
            // copyAsCorsixstringToolStripMenuItem
            // 
            this.copyAsCorsixstringToolStripMenuItem.Name = "copyAsCorsixstringToolStripMenuItem";
            this.copyAsCorsixstringToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.copyAsCorsixstringToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.copyAsCorsixstringToolStripMenuItem.Text = "Copy As Corsix-String";
            this.copyAsCorsixstringToolStripMenuItem.ToolTipText = "Copies the selected value (/table) as a string readable by Corsix\' RBFConv.";
            this.copyAsCorsixstringToolStripMenuItem.Click += new System.EventHandler(this.CopyAsCorsixstringToolStripMenuItemClick);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::RBFPlugin.Properties.Resources.edit_delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.ToolTipText = "Deletes a node from the RBF.";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
            // 
            // copyIntoLibraryToolStripMenuItem
            // 
            this.copyIntoLibraryToolStripMenuItem.Name = "copyIntoLibraryToolStripMenuItem";
            this.copyIntoLibraryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.copyIntoLibraryToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.copyIntoLibraryToolStripMenuItem.Text = "Copy Into Library";
            this.copyIntoLibraryToolStripMenuItem.Click += new System.EventHandler(this.CopyIntoLibraryToolStripMenuItemClick);
            // 
            // _cms_TableTree
            // 
            this._cms_TableTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addValuesToRBFDictionaryToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.cutTableToolStripMenuItem,
            this.copyAsCorsixstringToolStripMenuItem,
            this.copyIntoLibraryToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.copyValuePathToolStripMenuItem,
            this.insertToolStripMenuItem,
            this.insertFromLibraryToolStripMenuItem,
            this.insertValueFromCorsixStringToolStripMenuItem,
            this.insertIntoToolStripMenuItem,
            this.insertValueIntoAllSubtablesToolStripMenuItem});
            this._cms_TableTree.Name = "cmsTableTree";
            this._cms_TableTree.Size = new System.Drawing.Size(294, 290);
            // 
            // addValuesToRBFDictionaryToolStripMenuItem
            // 
            this.addValuesToRBFDictionaryToolStripMenuItem.Image = global::RBFPlugin.Properties.Resources.bookmarks;
            this.addValuesToRBFDictionaryToolStripMenuItem.Name = "addValuesToRBFDictionaryToolStripMenuItem";
            this.addValuesToRBFDictionaryToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.addValuesToRBFDictionaryToolStripMenuItem.Text = "Add Values To RBF-Dictionary";
            this.addValuesToRBFDictionaryToolStripMenuItem.ToolTipText = "Adds all children of the selected table to the RBF-Dictionary using the name of t" +
                "he table as a key.\\nThis is useful for adding the tables from attributes.rbf";
            this.addValuesToRBFDictionaryToolStripMenuItem.Click += new System.EventHandler(this.AddValuesToRBFDictionaryToolStripMenuItemClick);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::RBFPlugin.Properties.Resources.edit_copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.copyToolStripMenuItem.Text = "Copy Value";
            this.copyToolStripMenuItem.ToolTipText = "Copies a node to the clipboard.";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
            // 
            // copyValuePathToolStripMenuItem
            // 
            this.copyValuePathToolStripMenuItem.Name = "copyValuePathToolStripMenuItem";
            this.copyValuePathToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.copyValuePathToolStripMenuItem.Text = "Copy Value Path";
            this.copyValuePathToolStripMenuItem.ToolTipText = "Copies the full path of a node as a string to the clipboard.";
            this.copyValuePathToolStripMenuItem.Click += new System.EventHandler(this.CopyValuePathToolStripMenuItemClick);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.V)));
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.insertToolStripMenuItem.Text = "Paste Value";
            this.insertToolStripMenuItem.ToolTipText = "Overrides the selected node\'s value and type with the data from the clipboard.";
            this.insertToolStripMenuItem.Click += new System.EventHandler(this.InsertToolStripMenuItemClick);
            // 
            // insertFromLibraryToolStripMenuItem
            // 
            this.insertFromLibraryToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.insertFromLibraryToolStripMenuItem.Name = "insertFromLibraryToolStripMenuItem";
            this.insertFromLibraryToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.insertFromLibraryToolStripMenuItem.Text = "Insert From Library Into Table";
            this.insertFromLibraryToolStripMenuItem.DropDownOpening += new System.EventHandler(this.InsertFromLibraryToolStripMenuItemDropDownOpening);
            // 
            // insertValueFromCorsixStringToolStripMenuItem
            // 
            this.insertValueFromCorsixStringToolStripMenuItem.Name = "insertValueFromCorsixStringToolStripMenuItem";
            this.insertValueFromCorsixStringToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.insertValueFromCorsixStringToolStripMenuItem.Text = "Insert Value From Corsix-String Into Table";
            this.insertValueFromCorsixStringToolStripMenuItem.ToolTipText = "Inserts tables and values generated by Corsix\' RBFConv into the selected table.";
            this.insertValueFromCorsixStringToolStripMenuItem.Click += new System.EventHandler(this.InsertValueFromCorsixStringToolStripMenuItemClick);
            // 
            // insertIntoToolStripMenuItem
            // 
            this.insertIntoToolStripMenuItem.Image = global::RBFPlugin.Properties.Resources.edit_paste;
            this.insertIntoToolStripMenuItem.Name = "insertIntoToolStripMenuItem";
            this.insertIntoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.insertIntoToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.insertIntoToolStripMenuItem.Text = "Insert Value Into Table";
            this.insertIntoToolStripMenuItem.ToolTipText = "Inserts the node from the clipboard to the table of your choice as a childnode.";
            this.insertIntoToolStripMenuItem.Click += new System.EventHandler(this.InsertIntoToolStripMenuItemClick);
            // 
            // insertValueIntoAllSubtablesToolStripMenuItem
            // 
            this.insertValueIntoAllSubtablesToolStripMenuItem.Name = "insertValueIntoAllSubtablesToolStripMenuItem";
            this.insertValueIntoAllSubtablesToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.insertValueIntoAllSubtablesToolStripMenuItem.Text = "Insert Value Into All Subtables";
            this.insertValueIntoAllSubtablesToolStripMenuItem.Click += new System.EventHandler(this.InsertValueIntoAllSubtablesToolStripMenuItemClick);
            // 
            // pnlMainRBFEditor
            // 
            this.pnlMainRBFEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainRBFEditor.Location = new System.Drawing.Point(0, 0);
            this.pnlMainRBFEditor.Name = "pnlMainRBFEditor";
            this.pnlMainRBFEditor.Size = new System.Drawing.Size(861, 687);
            this.pnlMainRBFEditor.TabIndex = 2;
            // 
            // spcRBFEditor
            // 
            this.spcRBFEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcRBFEditor.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcRBFEditor.Location = new System.Drawing.Point(0, 0);
            this.spcRBFEditor.Name = "spcRBFEditor";
            // 
            // spcRBFEditor.Panel1
            // 
            this.spcRBFEditor.Panel1.Controls.Add(this.m_trvTables);
            // 
            // spcRBFEditor.Panel2
            // 
            this.spcRBFEditor.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.spcRBFEditor.Size = new System.Drawing.Size(861, 687);
            this.spcRBFEditor.SplitterDistance = 306;
            this.spcRBFEditor.TabIndex = 1;
            // 
            // m_trvTables
            // 
            this.m_trvTables.ContextMenuStrip = this._cms_TableTree;
            this.m_trvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_trvTables.Location = new System.Drawing.Point(0, 0);
            this.m_trvTables.Name = "m_trvTables";
            this.m_trvTables.Size = new System.Drawing.Size(306, 687);
            this.m_trvTables.TabIndex = 0;
            this.m_trvTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvTablesAfterSelect);
            this.m_trvTables.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvTablesNodeMouseClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.m_dgvValues, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(551, 687);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel4, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.m_tbxKey, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.m_cbxValue, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.m_cbxDataType, 1, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(551, 100);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbaKey);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(44, 27);
            this.panel2.TabIndex = 0;
            // 
            // lbaKey
            // 
            this.lbaKey.AutoSize = true;
            this.lbaKey.Location = new System.Drawing.Point(7, 3);
            this.lbaKey.Name = "lbaKey";
            this.lbaKey.Size = new System.Drawing.Size(25, 13);
            this.lbaKey.TabIndex = 0;
            this.lbaKey.Text = "Key";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labValue);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(44, 27);
            this.panel3.TabIndex = 1;
            // 
            // labValue
            // 
            this.labValue.AutoSize = true;
            this.labValue.Location = new System.Drawing.Point(7, 3);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(34, 13);
            this.labValue.TabIndex = 0;
            this.labValue.Text = "Value";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.labType);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 69);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(44, 28);
            this.panel4.TabIndex = 2;
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Location = new System.Drawing.Point(7, 3);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(31, 13);
            this.labType.TabIndex = 0;
            this.labType.Text = "Type";
            // 
            // m_tbxKey
            // 
            this.m_tbxKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxKey.Location = new System.Drawing.Point(53, 3);
            this.m_tbxKey.Name = "m_tbxKey";
            this.m_tbxKey.Size = new System.Drawing.Size(495, 20);
            this.m_tbxKey.TabIndex = 3;
            this.m_tbxKey.TextChanged += new System.EventHandler(this.TbxKeyTextChanged);
            // 
            // m_cbxValue
            // 
            this.m_cbxValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cbxValue.FormattingEnabled = true;
            this.m_cbxValue.Location = new System.Drawing.Point(53, 36);
            this.m_cbxValue.Name = "m_cbxValue";
            this.m_cbxValue.Size = new System.Drawing.Size(495, 21);
            this.m_cbxValue.Sorted = true;
            this.m_cbxValue.TabIndex = 4;
            this.m_cbxValue.TextUpdate += new System.EventHandler(this.CbxValueTextUpdate);
            // 
            // m_cbxDataType
            // 
            this.m_cbxDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cbxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxDataType.Enabled = false;
            this.m_cbxDataType.FormattingEnabled = true;
            this.m_cbxDataType.Items.AddRange(new object[] {
            "bool",
            "float",
            "int",
            "string",
            "table"});
            this.m_cbxDataType.Location = new System.Drawing.Point(53, 69);
            this.m_cbxDataType.Name = "m_cbxDataType";
            this.m_cbxDataType.Size = new System.Drawing.Size(495, 21);
            this.m_cbxDataType.Sorted = true;
            this.m_cbxDataType.TabIndex = 5;
            this.m_cbxDataType.TextUpdate += new System.EventHandler(this.CbxDataTypeTextUpdate);
            // 
            // m_dgvValues
            // 
            this.m_dgvValues.AllowUserToAddRows = false;
            this.m_dgvValues.AllowUserToDeleteRows = false;
            this.m_dgvValues.AllowUserToOrderColumns = true;
            this.m_dgvValues.AllowUserToResizeRows = false;
            this.m_dgvValues.BackgroundColor = System.Drawing.SystemColors.Control;
            this.m_dgvValues.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.m_dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgvValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cohKey,
            this.cohValue,
            this.cohType});
            this.m_dgvValues.ContextMenuStrip = this._cms_dgvValues;
            this.m_dgvValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dgvValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.m_dgvValues.Location = new System.Drawing.Point(0, 100);
            this.m_dgvValues.Margin = new System.Windows.Forms.Padding(0);
            this.m_dgvValues.Name = "m_dgvValues";
            this.m_dgvValues.RowHeadersVisible = false;
            this.m_dgvValues.RowHeadersWidth = 24;
            this.m_dgvValues.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.m_dgvValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dgvValues.Size = new System.Drawing.Size(551, 551);
            this.m_dgvValues.StandardTab = true;
            this.m_dgvValues.TabIndex = 1;
            this.m_dgvValues.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvValuesCellClick);
            this.m_dgvValues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvValuesCellContentDoubleClick);
            this.m_dgvValues.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvValuesCellMouseClick);
            this.m_dgvValues.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DgvValuesCellValidating);
            this.m_dgvValues.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvValuesCellValueChanged);
            this.m_dgvValues.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgvValuesEditingControlShowing);
            this.m_dgvValues.SelectionChanged += new System.EventHandler(this.DgvValuesSelectionChanged);
            // 
            // cohKey
            // 
            this.cohKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cohKey.HeaderText = "Key";
            this.cohKey.MaxInputLength = 64;
            this.cohKey.Name = "cohKey";
            this.cohKey.ToolTipText = "Keys cannot be longer than 64 ASCII letters.";
            this.cohKey.Width = 50;
            // 
            // cohValue
            // 
            this.cohValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cohValue.HeaderText = "Value";
            this.cohValue.Name = "cohValue";
            this.cohValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cohValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cohValue.ToolTipText = "Value of the subnote. The options for the value depend on the subnode\'s types.";
            // 
            // cohType
            // 
            this.cohType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cohType.HeaderText = "Type";
            this.cohType.Items.AddRange(new object[] {
            "int",
            "float",
            "bool",
            "table",
            "string"});
            this.cohType.Name = "cohType";
            this.cohType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cohType.ToolTipText = "Possible values: Boolean, Float, Integer, Table and String";
            this.cohType.Width = 37;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 651);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 36);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.m_rtbCurrentUCS, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(551, 36);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // m_rtbCurrentUCS
            // 
            this.m_rtbCurrentUCS.DetectUrls = false;
            this.m_rtbCurrentUCS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbCurrentUCS.Location = new System.Drawing.Point(75, 0);
            this.m_rtbCurrentUCS.Margin = new System.Windows.Forms.Padding(0);
            this.m_rtbCurrentUCS.Name = "m_rtbCurrentUCS";
            this.m_rtbCurrentUCS.Size = new System.Drawing.Size(476, 36);
            this.m_rtbCurrentUCS.TabIndex = 0;
            this.m_rtbCurrentUCS.Text = "";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(75, 36);
            this.panel5.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current UCS:";
            // 
            // RBFEditorCore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcRBFEditor);
            this.Controls.Add(this.pnlMainRBFEditor);
            this.Name = "RBFEditorCore";
            this.Size = new System.Drawing.Size(861, 687);
            this._cms_dgvValues.ResumeLayout(false);
            this._cms_TableTree.ResumeLayout(false);
            this.spcRBFEditor.Panel1.ResumeLayout(false);
            this.spcRBFEditor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcRBFEditor)).EndInit();
            this.spcRBFEditor.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvValues)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private System.Windows.Forms.ContextMenuStrip _cms_dgvValues;
        private System.Windows.Forms.ToolStripMenuItem addValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tryToOpenFileByValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsCorsixstringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyIntoLibraryToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip _cms_TableTree;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyValuePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertFromLibraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertValueFromCorsixStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertIntoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertValueIntoAllSubtablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addValuesToRBFDictionaryToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMainRBFEditor;
        private System.Windows.Forms.SplitContainer spcRBFEditor;
        private System.Windows.Forms.TreeView m_trvTables;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbaKey;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labValue;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.TextBox m_tbxKey;
        private System.Windows.Forms.ComboBox m_cbxValue;
        private System.Windows.Forms.ComboBox m_cbxDataType;
        private System.Windows.Forms.DataGridView m_dgvValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn cohKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn cohValue;
        private System.Windows.Forms.DataGridViewComboBoxColumn cohType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RichTextBox m_rtbCurrentUCS;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
    }
}