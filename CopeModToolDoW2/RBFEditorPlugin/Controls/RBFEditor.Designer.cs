namespace cope.DawnOfWar2.Controls
{
    partial class RBFEditor
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlMainRBFEditor = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.spcRBFEditor = new System.Windows.Forms.SplitContainer();
            this.trvTables = new System.Windows.Forms.TreeView();
            this.cmsTableTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyValuePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertIntoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbaKey = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labValue = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labType = new System.Windows.Forms.Label();
            this.tbxKey = new System.Windows.Forms.TextBox();
            this.cbxValue = new System.Windows.Forms.ComboBox();
            this.dgvValues = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSaveRBF = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbxDataType = new System.Windows.Forms.ComboBox();
            this.cohKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cohValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cohType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMainRBFEditor.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.spcRBFEditor.Panel1.SuspendLayout();
            this.spcRBFEditor.Panel2.SuspendLayout();
            this.spcRBFEditor.SuspendLayout();
            this.cmsTableTree.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainRBFEditor
            // 
            this.pnlMainRBFEditor.Controls.Add(this.tableLayoutPanel1);
            this.pnlMainRBFEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainRBFEditor.Location = new System.Drawing.Point(0, 0);
            this.pnlMainRBFEditor.Name = "pnlMainRBFEditor";
            this.pnlMainRBFEditor.Size = new System.Drawing.Size(926, 697);
            this.pnlMainRBFEditor.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.spcRBFEditor, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(926, 697);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // spcRBFEditor
            // 
            this.spcRBFEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcRBFEditor.Location = new System.Drawing.Point(3, 3);
            this.spcRBFEditor.Name = "spcRBFEditor";
            // 
            // spcRBFEditor.Panel1
            // 
            this.spcRBFEditor.Panel1.Controls.Add(this.trvTables);
            // 
            // spcRBFEditor.Panel2
            // 
            this.spcRBFEditor.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.spcRBFEditor.Size = new System.Drawing.Size(920, 641);
            this.spcRBFEditor.SplitterDistance = 306;
            this.spcRBFEditor.TabIndex = 1;
            // 
            // trvTables
            // 
            this.trvTables.ContextMenuStrip = this.cmsTableTree;
            this.trvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTables.Location = new System.Drawing.Point(0, 0);
            this.trvTables.Name = "trvTables";
            this.trvTables.Size = new System.Drawing.Size(306, 641);
            this.trvTables.TabIndex = 0;
            this.trvTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTables_AfterSelect);
            this.trvTables.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvTables_NodeMouseClick);
            // 
            // cmsTableTree
            // 
            this.cmsTableTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.cutTableToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.copyValuePathToolStripMenuItem,
            this.insertToolStripMenuItem,
            this.insertIntoToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveFileToolStripMenuItem,
            this.closeFileToolStripMenuItem});
            this.cmsTableTree.Name = "cmsTableTree";
            this.cmsTableTree.ShowImageMargin = false;
            this.cmsTableTree.Size = new System.Drawing.Size(293, 208);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.copyToolStripMenuItem.Text = "Copy Value";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyValuePathToolStripMenuItem
            // 
            this.copyValuePathToolStripMenuItem.Name = "copyValuePathToolStripMenuItem";
            this.copyValuePathToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.copyValuePathToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.copyValuePathToolStripMenuItem.Text = "Copy Value Path";
            this.copyValuePathToolStripMenuItem.Click += new System.EventHandler(this.copyValuePathToolStripMenuItem_Click);
            // 
            // cutTableToolStripMenuItem
            // 
            this.cutTableToolStripMenuItem.Name = "cutTableToolStripMenuItem";
            this.cutTableToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutTableToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.cutTableToolStripMenuItem.Text = "Cut Value";
            this.cutTableToolStripMenuItem.Click += new System.EventHandler(this.cutTableToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.insertToolStripMenuItem.Text = "Paste Value";
            this.insertToolStripMenuItem.Click += new System.EventHandler(this.insertToolStripMenuItem_Click);
            // 
            // insertIntoToolStripMenuItem
            // 
            this.insertIntoToolStripMenuItem.Name = "insertIntoToolStripMenuItem";
            this.insertIntoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.V)));
            this.insertIntoToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.insertIntoToolStripMenuItem.Text = "Insert Value Into Table";
            this.insertIntoToolStripMenuItem.Click += new System.EventHandler(this.insertIntoToolStripMenuItem_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dgvValues, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(610, 641);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel4, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.tbxKey, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbxValue, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.cbxDataType, 1, 2);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(604, 94);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbaKey);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(44, 25);
            this.panel2.TabIndex = 0;
            // 
            // lbaKey
            // 
            this.lbaKey.AutoSize = true;
            this.lbaKey.Location = new System.Drawing.Point(3, 8);
            this.lbaKey.Name = "lbaKey";
            this.lbaKey.Size = new System.Drawing.Size(25, 13);
            this.lbaKey.TabIndex = 0;
            this.lbaKey.Text = "Key";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labValue);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(44, 25);
            this.panel3.TabIndex = 1;
            // 
            // labValue
            // 
            this.labValue.AutoSize = true;
            this.labValue.Location = new System.Drawing.Point(3, 7);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(34, 13);
            this.labValue.TabIndex = 0;
            this.labValue.Text = "Value";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.labType);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 65);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(44, 26);
            this.panel4.TabIndex = 2;
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Location = new System.Drawing.Point(3, 8);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(31, 13);
            this.labType.TabIndex = 0;
            this.labType.Text = "Type";
            // 
            // tbxKey
            // 
            this.tbxKey.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbxKey.Location = new System.Drawing.Point(53, 8);
            this.tbxKey.Name = "tbxKey";
            this.tbxKey.Size = new System.Drawing.Size(548, 20);
            this.tbxKey.TabIndex = 3;
            this.tbxKey.TextChanged += new System.EventHandler(this.tbxKey_TextChanged);
            // 
            // cbxValue
            // 
            this.cbxValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbxValue.FormattingEnabled = true;
            this.cbxValue.Location = new System.Drawing.Point(53, 38);
            this.cbxValue.Name = "cbxValue";
            this.cbxValue.Size = new System.Drawing.Size(548, 21);
            this.cbxValue.Sorted = true;
            this.cbxValue.TabIndex = 4;
            this.cbxValue.TextUpdate += new System.EventHandler(this.cbxValue_TextUpdate);
            // 
            // dgvValues
            // 
            this.dgvValues.AllowUserToOrderColumns = true;
            this.dgvValues.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cohKey,
            this.cohValue,
            this.cohType});
            this.dgvValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvValues.Location = new System.Drawing.Point(3, 103);
            this.dgvValues.Name = "dgvValues";
            this.dgvValues.Size = new System.Drawing.Size(604, 535);
            this.dgvValues.StandardTab = true;
            this.dgvValues.TabIndex = 1;
            this.dgvValues.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvValues_CellValueChanged);
            this.dgvValues.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvValues_UserDeletingRow);
            this.dgvValues.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvValues_UserAddedRow);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 650);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(920, 44);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveRBF);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(823, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(94, 38);
            this.panel1.TabIndex = 0;
            // 
            // btnSaveRBF
            // 
            this.btnSaveRBF.Location = new System.Drawing.Point(9, 7);
            this.btnSaveRBF.Name = "btnSaveRBF";
            this.btnSaveRBF.Size = new System.Drawing.Size(75, 23);
            this.btnSaveRBF.TabIndex = 0;
            this.btnSaveRBF.Text = "Save";
            this.btnSaveRBF.UseVisualStyleBackColor = true;
            this.btnSaveRBF.Click += new System.EventHandler(this.btnSaveRBF_Click);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(814, 38);
            this.panel5.TabIndex = 1;
            // 
            // cbxDataType
            // 
            this.cbxDataType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataType.FormattingEnabled = true;
            this.cbxDataType.Items.AddRange(new object[] {
            "bool",
            "float",
            "int",
            "string",
            "table"});
            this.cbxDataType.Location = new System.Drawing.Point(53, 70);
            this.cbxDataType.Name = "cbxDataType";
            this.cbxDataType.Size = new System.Drawing.Size(548, 21);
            this.cbxDataType.Sorted = true;
            this.cbxDataType.TabIndex = 5;
            // 
            // cohKey
            // 
            this.cohKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cohKey.HeaderText = "Key";
            this.cohKey.MaxInputLength = 64;
            this.cohKey.Name = "cohKey";
            this.cohKey.ToolTipText = "Keys cannot be longer than 64 ASCII letters.";
            // 
            // cohValue
            // 
            this.cohValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cohValue.HeaderText = "Value";
            this.cohValue.Name = "cohValue";
            this.cohValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cohValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.cohType.ReadOnly = true;
            this.cohType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cohType.ToolTipText = "Possible values: Boolean, Float, Integer, Table and String";
            this.cohType.Width = 39;
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(289, 6);
            // 
            // closeFileToolStripMenuItem
            // 
            this.closeFileToolStripMenuItem.Name = "closeFileToolStripMenuItem";
            this.closeFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeFileToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.closeFileToolStripMenuItem.Text = "Close File";
            this.closeFileToolStripMenuItem.Click += new System.EventHandler(this.closeFileToolStripMenuItem_Click);
            // 
            // RBFEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMainRBFEditor);
            this.Name = "RBFEditor";
            this.Size = new System.Drawing.Size(926, 697);
            this.EnabledChanged += new System.EventHandler(this.RBFEditor_EnabledChanged);
            this.pnlMainRBFEditor.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.spcRBFEditor.Panel1.ResumeLayout(false);
            this.spcRBFEditor.Panel2.ResumeLayout(false);
            this.spcRBFEditor.ResumeLayout(false);
            this.cmsTableTree.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMainRBFEditor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer spcRBFEditor;
        private System.Windows.Forms.TreeView trvTables;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSaveRBF;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbxKey;
        private System.Windows.Forms.ComboBox cbxValue;
        private System.Windows.Forms.Label lbaKey;
        private System.Windows.Forms.Label labValue;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.DataGridView dgvValues;
        private System.Windows.Forms.ContextMenuStrip cmsTableTree;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertIntoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyValuePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutTableToolStripMenuItem;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cbxDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cohKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn cohValue;
        private System.Windows.Forms.DataGridViewComboBoxColumn cohType;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeFileToolStripMenuItem;

    }
}
