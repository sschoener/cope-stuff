namespace ModTool.Core.PlugIns.RelicChunky
{
    partial class ChunkyHeaderInfo
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
            this.m_tbxChunkID = new System.Windows.Forms.TextBox();
            this._lab_chunkType = new System.Windows.Forms.Label();
            this.m_tbxChunkType = new System.Windows.Forms.TextBox();
            this._lab_chunkSize = new System.Windows.Forms.Label();
            this.m_tbxChunkSize = new System.Windows.Forms.TextBox();
            this._lab_chunkName = new System.Windows.Forms.Label();
            this.m_tbxChunkName = new System.Windows.Forms.TextBox();
            this._lab_chunkVersion = new System.Windows.Forms.Label();
            this._lab_chunkMinVersion = new System.Windows.Forms.Label();
            this._lab_chunkFlags = new System.Windows.Forms.Label();
            this.m_tbxChunkVersion = new System.Windows.Forms.TextBox();
            this.m_tbxChunkMinVersion = new System.Windows.Forms.TextBox();
            this.m_tbxChunkFlags = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this._lab_chunkInfo = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this._rtb_chunkInfo = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this._lab_chunkID = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            //
            // _tbx_chunkID
            //
            this.m_tbxChunkID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxChunkID.Location = new System.Drawing.Point(109, 28);
            this.m_tbxChunkID.Name = "m_tbxChunkID";
            this.m_tbxChunkID.ReadOnly = true;
            this.m_tbxChunkID.Size = new System.Drawing.Size(400, 20);
            this.m_tbxChunkID.TabIndex = 1;
            //
            // _lab_chunkType
            //
            this._lab_chunkType.AutoSize = true;
            this._lab_chunkType.Location = new System.Drawing.Point(3, 3);
            this._lab_chunkType.Name = "_lab_chunkType";
            this._lab_chunkType.Size = new System.Drawing.Size(65, 13);
            this._lab_chunkType.TabIndex = 2;
            this._lab_chunkType.Text = "Chunk Type";
            //
            // _tbx_chunkType
            //
            this.m_tbxChunkType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxChunkType.Location = new System.Drawing.Point(109, 3);
            this.m_tbxChunkType.Name = "m_tbxChunkType";
            this.m_tbxChunkType.ReadOnly = true;
            this.m_tbxChunkType.Size = new System.Drawing.Size(400, 20);
            this.m_tbxChunkType.TabIndex = 3;
            //
            // _lab_chunkSize
            //
            this._lab_chunkSize.AutoSize = true;
            this._lab_chunkSize.Location = new System.Drawing.Point(3, 3);
            this._lab_chunkSize.Name = "_lab_chunkSize";
            this._lab_chunkSize.Size = new System.Drawing.Size(61, 13);
            this._lab_chunkSize.TabIndex = 4;
            this._lab_chunkSize.Text = "Chunk Size";
            //
            // _tbx_chunkSize
            //
            this.m_tbxChunkSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxChunkSize.Location = new System.Drawing.Point(109, 53);
            this.m_tbxChunkSize.Name = "m_tbxChunkSize";
            this.m_tbxChunkSize.ReadOnly = true;
            this.m_tbxChunkSize.Size = new System.Drawing.Size(400, 20);
            this.m_tbxChunkSize.TabIndex = 5;
            //
            // _lab_chunkName
            //
            this._lab_chunkName.AutoSize = true;
            this._lab_chunkName.Location = new System.Drawing.Point(3, 3);
            this._lab_chunkName.Name = "_lab_chunkName";
            this._lab_chunkName.Size = new System.Drawing.Size(69, 13);
            this._lab_chunkName.TabIndex = 6;
            this._lab_chunkName.Text = "Chunk Name";
            //
            // _tbx_chunkName
            //
            this.m_tbxChunkName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxChunkName.Location = new System.Drawing.Point(109, 78);
            this.m_tbxChunkName.Name = "m_tbxChunkName";
            this.m_tbxChunkName.Size = new System.Drawing.Size(400, 20);
            this.m_tbxChunkName.TabIndex = 7;
            this.m_tbxChunkName.TextChanged += new System.EventHandler(this.TbxChunkNameTextChanged);
            //
            // _lab_chunkVersion
            //
            this._lab_chunkVersion.AutoSize = true;
            this._lab_chunkVersion.Location = new System.Drawing.Point(3, 3);
            this._lab_chunkVersion.Name = "_lab_chunkVersion";
            this._lab_chunkVersion.Size = new System.Drawing.Size(76, 13);
            this._lab_chunkVersion.TabIndex = 8;
            this._lab_chunkVersion.Text = "Chunk Version";
            //
            // _lab_chunkMinVersion
            //
            this._lab_chunkMinVersion.AutoSize = true;
            this._lab_chunkMinVersion.Location = new System.Drawing.Point(3, 3);
            this._lab_chunkMinVersion.Name = "_lab_chunkMinVersion";
            this._lab_chunkMinVersion.Size = new System.Drawing.Size(96, 13);
            this._lab_chunkMinVersion.TabIndex = 9;
            this._lab_chunkMinVersion.Text = "Chunk Min Version";
            //
            // _lab_chunkFlags
            //
            this._lab_chunkFlags.AutoSize = true;
            this._lab_chunkFlags.Location = new System.Drawing.Point(3, 3);
            this._lab_chunkFlags.Name = "_lab_chunkFlags";
            this._lab_chunkFlags.Size = new System.Drawing.Size(66, 13);
            this._lab_chunkFlags.TabIndex = 10;
            this._lab_chunkFlags.Text = "Chunk Flags";
            //
            // _tbx_chunkVersion
            //
            this.m_tbxChunkVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxChunkVersion.Location = new System.Drawing.Point(109, 103);
            this.m_tbxChunkVersion.Name = "m_tbxChunkVersion";
            this.m_tbxChunkVersion.ReadOnly = true;
            this.m_tbxChunkVersion.Size = new System.Drawing.Size(400, 20);
            this.m_tbxChunkVersion.TabIndex = 11;
            //
            // _tbx_chunkMinVersion
            //
            this.m_tbxChunkMinVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxChunkMinVersion.Location = new System.Drawing.Point(109, 128);
            this.m_tbxChunkMinVersion.Name = "m_tbxChunkMinVersion";
            this.m_tbxChunkMinVersion.ReadOnly = true;
            this.m_tbxChunkMinVersion.Size = new System.Drawing.Size(400, 20);
            this.m_tbxChunkMinVersion.TabIndex = 12;
            //
            // _tbx_ChunkFlags
            //
            this.m_tbxChunkFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbxChunkFlags.Location = new System.Drawing.Point(109, 153);
            this.m_tbxChunkFlags.Name = "m_tbxChunkFlags";
            this.m_tbxChunkFlags.ReadOnly = true;
            this.m_tbxChunkFlags.Size = new System.Drawing.Size(400, 20);
            this.m_tbxChunkFlags.TabIndex = 13;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_tbxChunkType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_tbxChunkFlags, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.m_tbxChunkID, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_tbxChunkMinVersion, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_tbxChunkVersion, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.m_tbxChunkName, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel9, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.m_tbxChunkSize, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel16, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(512, 270);
            this.tableLayoutPanel1.TabIndex = 14;
            //
            // panel1
            //
            this.panel1.Controls.Add(this._lab_chunkType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 19);
            this.panel1.TabIndex = 0;
            //
            // panel4
            //
            this.panel4.Controls.Add(this._lab_chunkSize);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 53);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 19);
            this.panel4.TabIndex = 3;
            //
            // panel5
            //
            this.panel5.Controls.Add(this._lab_chunkName);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 78);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(100, 19);
            this.panel5.TabIndex = 4;
            //
            // panel6
            //
            this.panel6.Controls.Add(this._lab_chunkVersion);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 103);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(100, 19);
            this.panel6.TabIndex = 5;
            //
            // panel7
            //
            this.panel7.Controls.Add(this._lab_chunkMinVersion);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 128);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(100, 19);
            this.panel7.TabIndex = 6;
            //
            // panel8
            //
            this.panel8.Controls.Add(this._lab_chunkFlags);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 153);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(100, 19);
            this.panel8.TabIndex = 7;
            //
            // panel9
            //
            this.panel9.Controls.Add(this._lab_chunkInfo);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 178);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(100, 89);
            this.panel9.TabIndex = 8;
            //
            // _lab_chunkInfo
            //
            this._lab_chunkInfo.AutoSize = true;
            this._lab_chunkInfo.Location = new System.Drawing.Point(3, 3);
            this._lab_chunkInfo.Name = "_lab_chunkInfo";
            this._lab_chunkInfo.Size = new System.Drawing.Size(59, 13);
            this._lab_chunkInfo.TabIndex = 0;
            this._lab_chunkInfo.Text = "Chunk Info";
            //
            // panel16
            //
            this.panel16.Controls.Add(this._rtb_chunkInfo);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(109, 178);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(400, 89);
            this.panel16.TabIndex = 15;
            //
            // _rtb_chunkInfo
            //
            this._rtb_chunkInfo.BackColor = System.Drawing.SystemColors.Window;
            this._rtb_chunkInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rtb_chunkInfo.Location = new System.Drawing.Point(0, 0);
            this._rtb_chunkInfo.Name = "_rtb_chunkInfo";
            this._rtb_chunkInfo.Size = new System.Drawing.Size(400, 89);
            this._rtb_chunkInfo.TabIndex = 0;
            this._rtb_chunkInfo.Text = "";
            //
            // panel2
            //
            this.panel2.Controls.Add(this._lab_chunkID);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 19);
            this.panel2.TabIndex = 16;
            //
            // _lab_chunkID
            //
            this._lab_chunkID.AutoSize = true;
            this._lab_chunkID.Location = new System.Drawing.Point(3, 3);
            this._lab_chunkID.Name = "_lab_chunkID";
            this._lab_chunkID.Size = new System.Drawing.Size(52, 13);
            this._lab_chunkID.TabIndex = 0;
            this._lab_chunkID.Text = "Chunk ID";
            //
            // ChunkyHeaderInfo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChunkyHeaderInfo";
            this.Size = new System.Drawing.Size(512, 270);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Vom Komponenten-Designer generierter Code

        private System.Windows.Forms.TextBox m_tbxChunkID;
        private System.Windows.Forms.Label _lab_chunkType;
        private System.Windows.Forms.TextBox m_tbxChunkType;
        private System.Windows.Forms.Label _lab_chunkSize;
        private System.Windows.Forms.TextBox m_tbxChunkSize;
        private System.Windows.Forms.Label _lab_chunkName;
        private System.Windows.Forms.TextBox m_tbxChunkName;
        private System.Windows.Forms.Label _lab_chunkVersion;
        private System.Windows.Forms.Label _lab_chunkMinVersion;
        private System.Windows.Forms.Label _lab_chunkFlags;
        private System.Windows.Forms.TextBox m_tbxChunkVersion;
        private System.Windows.Forms.TextBox m_tbxChunkMinVersion;
        private System.Windows.Forms.TextBox m_tbxChunkFlags;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label _lab_chunkID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox _rtb_chunkInfo;
        private System.Windows.Forms.Label _lab_chunkInfo;
    }
}