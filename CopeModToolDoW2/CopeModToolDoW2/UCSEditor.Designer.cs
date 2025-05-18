namespace ModTool.FE
{
    partial class UCSEditor
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
            this.m_tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.m_dgvStrings = new System.Windows.Forms.DataGridView();
            this.clmIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_rtbUCSText = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_chkbxAutoIndex = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_chkbxCopyToClipboard = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.m_nupIndex = new System.Windows.Forms.NumericUpDown();
            this.m_tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvStrings)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // m_tlpMain
            // 
            this.m_tlpMain.ColumnCount = 1;
            this.m_tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_tlpMain.Controls.Add(this.m_dgvStrings, 0, 0);
            this.m_tlpMain.Controls.Add(this.panel1, 0, 1);
            this.m_tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tlpMain.Location = new System.Drawing.Point(0, 0);
            this.m_tlpMain.MinimumSize = new System.Drawing.Size(150, 150);
            this.m_tlpMain.Name = "m_tlpMain";
            this.m_tlpMain.RowCount = 2;
            this.m_tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.m_tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_tlpMain.Size = new System.Drawing.Size(697, 489);
            this.m_tlpMain.TabIndex = 1;
            // 
            // m_dgvStrings
            // 
            this.m_dgvStrings.AllowUserToAddRows = false;
            this.m_dgvStrings.AllowUserToResizeRows = false;
            this.m_dgvStrings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.m_dgvStrings.BackgroundColor = System.Drawing.SystemColors.Control;
            this.m_dgvStrings.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.m_dgvStrings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgvStrings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmIndex,
            this.clmString});
            this.m_dgvStrings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dgvStrings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.m_dgvStrings.Location = new System.Drawing.Point(3, 3);
            this.m_dgvStrings.MultiSelect = false;
            this.m_dgvStrings.Name = "m_dgvStrings";
            this.m_dgvStrings.RowHeadersVisible = false;
            this.m_dgvStrings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.m_dgvStrings.Size = new System.Drawing.Size(691, 366);
            this.m_dgvStrings.TabIndex = 1;
            this.m_dgvStrings.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvStringsCellValueChanged);
            this.m_dgvStrings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvStringsKeyDown);
            // 
            // clmIndex
            // 
            this.clmIndex.FillWeight = 20F;
            this.clmIndex.HeaderText = "Index";
            this.clmIndex.Name = "clmIndex";
            this.clmIndex.ReadOnly = true;
            // 
            // clmString
            // 
            this.clmString.HeaderText = "String";
            this.clmString.Name = "clmString";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(691, 111);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_rtbUCSText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(691, 111);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // m_rtbUCSText
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.m_rtbUCSText, 4);
            this.m_rtbUCSText.DetectUrls = false;
            this.m_rtbUCSText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbUCSText.Location = new System.Drawing.Point(3, 3);
            this.m_rtbUCSText.Name = "m_rtbUCSText";
            this.m_rtbUCSText.Size = new System.Drawing.Size(685, 80);
            this.m_rtbUCSText.TabIndex = 7;
            this.m_rtbUCSText.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(350, 86);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.MinimumSize = new System.Drawing.Size(100, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(341, 25);
            this.panel2.TabIndex = 8;
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnAdd.Location = new System.Drawing.Point(246, 0);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(95, 25);
            this.m_btnAdd.TabIndex = 10;
            this.m_btnAdd.Text = "Add string";
            this.m_btnAdd.UseVisualStyleBackColor = true;
            this.m_btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_chkbxAutoIndex);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 86);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(88, 25);
            this.panel3.TabIndex = 9;
            // 
            // m_chkbxAutoIndex
            // 
            this.m_chkbxAutoIndex.AutoSize = true;
            this.m_chkbxAutoIndex.Checked = true;
            this.m_chkbxAutoIndex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkbxAutoIndex.Location = new System.Drawing.Point(3, 6);
            this.m_chkbxAutoIndex.Name = "m_chkbxAutoIndex";
            this.m_chkbxAutoIndex.Size = new System.Drawing.Size(77, 17);
            this.m_chkbxAutoIndex.TabIndex = 6;
            this.m_chkbxAutoIndex.Text = "Auto-Index";
            this.m_chkbxAutoIndex.UseVisualStyleBackColor = true;
            this.m_chkbxAutoIndex.CheckedChanged += new System.EventHandler(this.ChkbxAutoIndexCheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_chkbxCopyToClipboard);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(88, 86);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(149, 25);
            this.panel4.TabIndex = 10;
            // 
            // m_chkbxCopyToClipboard
            // 
            this.m_chkbxCopyToClipboard.AutoSize = true;
            this.m_chkbxCopyToClipboard.Checked = true;
            this.m_chkbxCopyToClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkbxCopyToClipboard.Location = new System.Drawing.Point(3, 6);
            this.m_chkbxCopyToClipboard.Name = "m_chkbxCopyToClipboard";
            this.m_chkbxCopyToClipboard.Size = new System.Drawing.Size(138, 17);
            this.m_chkbxCopyToClipboard.TabIndex = 9;
            this.m_chkbxCopyToClipboard.Text = "Copy Index to Clipboard";
            this.m_chkbxCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.m_nupIndex);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(237, 86);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(113, 25);
            this.panel5.TabIndex = 11;
            // 
            // m_nupIndex
            // 
            this.m_nupIndex.Enabled = false;
            this.m_nupIndex.Location = new System.Drawing.Point(6, 3);
            this.m_nupIndex.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
            this.m_nupIndex.Name = "m_nupIndex";
            this.m_nupIndex.Size = new System.Drawing.Size(98, 20);
            this.m_nupIndex.TabIndex = 8;
            // 
            // UCSEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_tlpMain);
            this.MinimumSize = new System.Drawing.Size(465, 200);
            this.Name = "UCSEditor";
            this.Size = new System.Drawing.Size(697, 489);
            this.Load += new System.EventHandler(this.OnLoad);
            this.m_tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvStrings)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_nupIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel m_tlpMain;
        private System.Windows.Forms.DataGridView m_dgvStrings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox m_chkbxCopyToClipboard;
        private System.Windows.Forms.NumericUpDown m_nupIndex;
        private System.Windows.Forms.RichTextBox m_rtbUCSText;
        private System.Windows.Forms.CheckBox m_chkbxAutoIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmString;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button m_btnAdd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;

    }
}
