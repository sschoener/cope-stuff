namespace ModTool.Core
{
    partial class PluginManagerControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lbxFileTypes = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_chklbxPlugins = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_labVersionValue = new System.Windows.Forms.Label();
            this.m_labAuthorValue = new System.Windows.Forms.Label();
            this.m_labNameValue = new System.Windows.Forms.Label();
            this._labVersion = new System.Windows.Forms.Label();
            this._labAuthor = new System.Windows.Forms.Label();
            this._labName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_lbxFileTypes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_chklbxPlugins, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(585, 452);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // tableLayoutPanel3
            //
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(295, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(287, 34);
            this.tableLayoutPanel3.TabIndex = 5;
            //
            // panel4
            //
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(106, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(74, 28);
            this.panel4.TabIndex = 0;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "PlugIns";
            //
            // _lbx_file_types
            //
            this.m_lbxFileTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbxFileTypes.FormattingEnabled = true;
            this.m_lbxFileTypes.Location = new System.Drawing.Point(3, 43);
            this.m_lbxFileTypes.Name = "m_lbxFileTypes";
            this.m_lbxFileTypes.Size = new System.Drawing.Size(286, 330);
            this.m_lbxFileTypes.Sorted = true;
            this.m_lbxFileTypes.TabIndex = 0;
            this.m_lbxFileTypes.SelectedValueChanged += new System.EventHandler(this.LbxFileTypesSelectedValueChanged);
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(286, 34);
            this.tableLayoutPanel2.TabIndex = 4;
            //
            // panel3
            //
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(86, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(114, 28);
            this.panel3.TabIndex = 0;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Extensions";
            //
            // _clbx_plugins
            //
            this.m_chklbxPlugins.CheckOnClick = true;
            this.m_chklbxPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_chklbxPlugins.FormattingEnabled = true;
            this.m_chklbxPlugins.Location = new System.Drawing.Point(295, 43);
            this.m_chklbxPlugins.Name = "m_chklbxPlugins";
            this.m_chklbxPlugins.Size = new System.Drawing.Size(287, 330);
            this.m_chklbxPlugins.TabIndex = 6;
            this.m_chklbxPlugins.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ClbxPluginsItemCheck);
            this.m_chklbxPlugins.SelectedIndexChanged += new System.EventHandler(this.ClbxPluginsSelectedIndexChanged);
            //
            // panel1
            //
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.m_labVersionValue);
            this.panel1.Controls.Add(this.m_labAuthorValue);
            this.panel1.Controls.Add(this.m_labNameValue);
            this.panel1.Controls.Add(this._labVersion);
            this.panel1.Controls.Add(this._labAuthor);
            this.panel1.Controls.Add(this._labName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 379);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(579, 70);
            this.panel1.TabIndex = 7;
            //
            // _labVersionValue
            //
            this.m_labVersionValue.AutoSize = true;
            this.m_labVersionValue.Location = new System.Drawing.Point(80, 46);
            this.m_labVersionValue.Name = "m_labVersionValue";
            this.m_labVersionValue.Size = new System.Drawing.Size(48, 13);
            this.m_labVersionValue.TabIndex = 5;
            this.m_labVersionValue.Text = "no value";
            //
            // _labAuthorValue
            //
            this.m_labAuthorValue.AutoSize = true;
            this.m_labAuthorValue.Location = new System.Drawing.Point(80, 29);
            this.m_labAuthorValue.Name = "m_labAuthorValue";
            this.m_labAuthorValue.Size = new System.Drawing.Size(48, 13);
            this.m_labAuthorValue.TabIndex = 4;
            this.m_labAuthorValue.Text = "no value";
            //
            // _labNameValue
            //
            this.m_labNameValue.AutoSize = true;
            this.m_labNameValue.Location = new System.Drawing.Point(80, 12);
            this.m_labNameValue.Name = "m_labNameValue";
            this.m_labNameValue.Size = new System.Drawing.Size(48, 13);
            this.m_labNameValue.TabIndex = 3;
            this.m_labNameValue.Text = "no value";
            //
            // _labVersion
            //
            this._labVersion.AutoSize = true;
            this._labVersion.Location = new System.Drawing.Point(13, 46);
            this._labVersion.Name = "_labVersion";
            this._labVersion.Size = new System.Drawing.Size(42, 13);
            this._labVersion.TabIndex = 2;
            this._labVersion.Text = "Version";
            //
            // _labAuthor
            //
            this._labAuthor.AutoSize = true;
            this._labAuthor.Location = new System.Drawing.Point(13, 29);
            this._labAuthor.Name = "_labAuthor";
            this._labAuthor.Size = new System.Drawing.Size(38, 13);
            this._labAuthor.TabIndex = 1;
            this._labAuthor.Text = "Author";
            //
            // _labName
            //
            this._labName.AutoSize = true;
            this._labName.Location = new System.Drawing.Point(13, 12);
            this._labName.Name = "_labName";
            this._labName.Size = new System.Drawing.Size(35, 13);
            this._labName.TabIndex = 0;
            this._labName.Text = "Name";
            //
            // PluginManagerControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PluginManagerControl";
            this.Size = new System.Drawing.Size(585, 452);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Vom Komponenten-Designer generierter Code

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox m_lbxFileTypes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox m_chklbxPlugins;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label m_labVersionValue;
        private System.Windows.Forms.Label m_labAuthorValue;
        private System.Windows.Forms.Label m_labNameValue;
        private System.Windows.Forms.Label _labVersion;
        private System.Windows.Forms.Label _labAuthor;
        private System.Windows.Forms.Label _labName;
    }
}