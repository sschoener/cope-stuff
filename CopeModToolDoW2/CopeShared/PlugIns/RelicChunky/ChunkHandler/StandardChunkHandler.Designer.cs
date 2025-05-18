namespace ModTool.Core.PlugIns.RelicChunky
{
    partial class StandardChunkHandler
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
            this.m_hbxRawData = new Be.Windows.Forms.HexBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._btn_Save = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_hbxRawData, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(445, 329);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // _hbx_rawData
            //
            this.m_hbxRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_hbxRawData.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_hbxRawData.InfoForeColor = System.Drawing.Color.Blue;
            this.m_hbxRawData.LineInfoVisible = true;
            this.m_hbxRawData.Location = new System.Drawing.Point(3, 3);
            this.m_hbxRawData.Name = "m_hbxRawData";
            this.m_hbxRawData.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.m_hbxRawData.Size = new System.Drawing.Size(439, 288);
            this.m_hbxRawData.StringViewVisible = true;
            this.m_hbxRawData.TabIndex = 1;
            this.m_hbxRawData.UseFixedBytesPerLine = true;
            this.m_hbxRawData.VScrollBarVisible = true;
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.Controls.Add(this._btn_Save, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 297);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(439, 29);
            this.tableLayoutPanel2.TabIndex = 3;
            //
            // _btn_Save
            //
            this._btn_Save.Location = new System.Drawing.Point(362, 3);
            this._btn_Save.Name = "_btn_Save";
            this._btn_Save.Size = new System.Drawing.Size(74, 23);
            this._btn_Save.TabIndex = 0;
            this._btn_Save.Text = "Save";
            this._btn_Save.UseVisualStyleBackColor = true;
            this._btn_Save.Click += new System.EventHandler(this.BtnSaveClick);
            //
            // StdChkHandler
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StandardChunkHandler";
            this.Size = new System.Drawing.Size(445, 329);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Vom Komponenten-Designer generierter Code

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button _btn_Save;
        private Be.Windows.Forms.HexBox m_hbxRawData;
    }
}