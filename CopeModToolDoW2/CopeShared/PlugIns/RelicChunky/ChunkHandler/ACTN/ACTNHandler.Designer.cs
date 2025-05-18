namespace ModTool.Core.PlugIns.RelicChunky
{
    partial class ACTNHandler
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.m_tbxDelay = new System.Windows.Forms.TextBox();
            this.m_tbxActionName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this._lab_actionDelay = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this._lab_actionName = new System.Windows.Forms.Label();
            this.m_lbxActions = new System.Windows.Forms.ListBox();
            this._cms_actionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.m_dgvValues = new System.Windows.Forms.DataGridView();
            this._col_key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._col_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this._btn_save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this._cms_actionsMenu.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvValues)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(532, 421);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_lbxActions, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(196, 421);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // panel1
            //
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 363);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 55);
            this.panel1.TabIndex = 0;
            //
            // tableLayoutPanel4
            //
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.m_tbxDelay, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.m_tbxActionName, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(190, 55);
            this.tableLayoutPanel4.TabIndex = 4;
            //
            // _tbx_delay
            //
            this.m_tbxDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxDelay.Location = new System.Drawing.Point(78, 3);
            this.m_tbxDelay.Name = "m_tbxDelay";
            this.m_tbxDelay.Size = new System.Drawing.Size(109, 20);
            this.m_tbxDelay.TabIndex = 1;
            this.m_tbxDelay.TextChanged += new System.EventHandler(this.TbxDelayTextChanged);
            //
            // _tbx_actionName
            //
            this.m_tbxActionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxActionName.Location = new System.Drawing.Point(78, 30);
            this.m_tbxActionName.Name = "m_tbxActionName";
            this.m_tbxActionName.Size = new System.Drawing.Size(109, 20);
            this.m_tbxActionName.TabIndex = 3;
            this.m_tbxActionName.TextChanged += new System.EventHandler(this.TbxActionNameTextChanged);
            //
            // panel2
            //
            this.panel2.Controls.Add(this._lab_actionDelay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(69, 21);
            this.panel2.TabIndex = 4;
            //
            // _lab_actionDelay
            //
            this._lab_actionDelay.AutoSize = true;
            this._lab_actionDelay.Location = new System.Drawing.Point(3, 3);
            this._lab_actionDelay.Name = "_lab_actionDelay";
            this._lab_actionDelay.Size = new System.Drawing.Size(34, 13);
            this._lab_actionDelay.TabIndex = 1;
            this._lab_actionDelay.Text = "Delay";
            //
            // panel3
            //
            this.panel3.Controls.Add(this._lab_actionName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(69, 22);
            this.panel3.TabIndex = 5;
            //
            // _lab_actionName
            //
            this._lab_actionName.AutoSize = true;
            this._lab_actionName.Location = new System.Drawing.Point(0, 3);
            this._lab_actionName.Name = "_lab_actionName";
            this._lab_actionName.Size = new System.Drawing.Size(68, 13);
            this._lab_actionName.TabIndex = 3;
            this._lab_actionName.Text = "Action Name";
            //
            // _lbx_actions
            //
            this.m_lbxActions.ContextMenuStrip = this._cms_actionsMenu;
            this.m_lbxActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbxActions.FormattingEnabled = true;
            this.m_lbxActions.Location = new System.Drawing.Point(3, 3);
            this.m_lbxActions.Name = "m_lbxActions";
            this.m_lbxActions.Size = new System.Drawing.Size(190, 354);
            this.m_lbxActions.TabIndex = 1;
            this.m_lbxActions.SelectedIndexChanged += new System.EventHandler(this.LbxActionsSelectedIndexChanged);
            this.m_lbxActions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LbxActionsMouseDown);
            //
            // _cms_actionsMenu
            //
            this._cms_actionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyActionToolStripMenuItem,
            this.cutActionToolStripMenuItem,
            this.insertActionToolStripMenuItem,
            this.deleteActionToolStripMenuItem});
            this._cms_actionsMenu.Name = "_cms_actionsMenu";
            this._cms_actionsMenu.Size = new System.Drawing.Size(146, 92);
            //
            // copyActionToolStripMenuItem
            //
            this.copyActionToolStripMenuItem.Name = "copyActionToolStripMenuItem";
            this.copyActionToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.copyActionToolStripMenuItem.Text = "Copy Action";
            this.copyActionToolStripMenuItem.Click += new System.EventHandler(this.CopyActionToolStripMenuItemClick);
            //
            // cutActionToolStripMenuItem
            //
            this.cutActionToolStripMenuItem.Name = "cutActionToolStripMenuItem";
            this.cutActionToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.cutActionToolStripMenuItem.Text = "Cut Action";
            this.cutActionToolStripMenuItem.Click += new System.EventHandler(this.CutActionToolStripMenuItemClick);
            //
            // insertActionToolStripMenuItem
            //
            this.insertActionToolStripMenuItem.Name = "insertActionToolStripMenuItem";
            this.insertActionToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.insertActionToolStripMenuItem.Text = "Insert Action";
            this.insertActionToolStripMenuItem.Click += new System.EventHandler(this.InsertActionToolStripMenuItemClick);
            //
            // deleteActionToolStripMenuItem
            //
            this.deleteActionToolStripMenuItem.Name = "deleteActionToolStripMenuItem";
            this.deleteActionToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteActionToolStripMenuItem.Text = "Delete Action";
            this.deleteActionToolStripMenuItem.Click += new System.EventHandler(this.DeleteActionToolStripMenuItemClick);
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.m_dgvValues, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(332, 421);
            this.tableLayoutPanel2.TabIndex = 0;
            //
            // _dgv_values
            //
            this.m_dgvValues.AllowUserToOrderColumns = true;
            this.m_dgvValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.m_dgvValues.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.m_dgvValues.BackgroundColor = System.Drawing.SystemColors.Control;
            this.m_dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgvValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._col_key,
            this._col_value});
            this.m_dgvValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dgvValues.Location = new System.Drawing.Point(3, 3);
            this.m_dgvValues.Name = "m_dgvValues";
            this.m_dgvValues.RowHeadersWidth = 24;
            this.m_dgvValues.Size = new System.Drawing.Size(326, 381);
            this.m_dgvValues.TabIndex = 1;
            this.m_dgvValues.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvValuesCellBeginEdit);
            this.m_dgvValues.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvValuesCellValueChanged);
            this.m_dgvValues.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DgvValuesUserAddedRow);
            //
            // _col_key
            //
            this._col_key.HeaderText = "Key";
            this._col_key.Name = "_col_key";
            //
            // _col_value
            //
            this._col_value.HeaderText = "Value";
            this._col_value.Name = "_col_value";
            //
            // tableLayoutPanel3
            //
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel3.Controls.Add(this._btn_save, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 390);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(326, 28);
            this.tableLayoutPanel3.TabIndex = 2;
            //
            // _btn_save
            //
            this._btn_save.Location = new System.Drawing.Point(239, 3);
            this._btn_save.Name = "_btn_save";
            this._btn_save.Size = new System.Drawing.Size(84, 22);
            this._btn_save.TabIndex = 0;
            this._btn_save.Text = "Save";
            this._btn_save.UseVisualStyleBackColor = true;
            this._btn_save.Click += new System.EventHandler(this.BtnSaveClick);
            //
            // ACTNHandler
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "ACTNHandler";
            this.Size = new System.Drawing.Size(532, 421);
            this.Resize += new System.EventHandler(this.ACTNHandler_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this._cms_actionsMenu.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvValues)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Vom Komponenten-Designer generierter Code

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_tbxDelay;
        private System.Windows.Forms.ListBox m_lbxActions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView m_dgvValues;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ContextMenuStrip _cms_actionsMenu;
        private System.Windows.Forms.ToolStripMenuItem copyActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteActionToolStripMenuItem;
        private System.Windows.Forms.Button _btn_save;
        private System.Windows.Forms.DataGridViewTextBoxColumn _col_key;
        private System.Windows.Forms.DataGridViewTextBoxColumn _col_value;
        private System.Windows.Forms.TextBox m_tbxActionName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label _lab_actionDelay;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label _lab_actionName;
    }
}