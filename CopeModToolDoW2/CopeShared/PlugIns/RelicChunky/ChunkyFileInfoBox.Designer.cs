namespace ModTool.Core.PlugIns.RelicChunky
{
    partial class ChunkyFileInfoBox
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_cfiChunkInfo = new ChunkyFileInfo();
            this._btn_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // _cfi_chunkInfo
            //
            this.m_cfiChunkInfo.Location = new System.Drawing.Point(0, 1);
            this.m_cfiChunkInfo.Name = "m_cfiChunkInfo";
            this.m_cfiChunkInfo.Size = new System.Drawing.Size(220, 119);
            this.m_cfiChunkInfo.TabIndex = 0;
            //
            // _btn_OK
            //
            this._btn_OK.Location = new System.Drawing.Point(134, 123);
            this._btn_OK.Name = "_btn_OK";
            this._btn_OK.Size = new System.Drawing.Size(75, 23);
            this._btn_OK.TabIndex = 1;
            this._btn_OK.Text = "OK";
            this._btn_OK.UseVisualStyleBackColor = true;
            this._btn_OK.Click += new System.EventHandler(this.BtnOkClick);
            //
            // ChunkyFileInfoBox
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 153);
            this.Controls.Add(this._btn_OK);
            this.Controls.Add(this.m_cfiChunkInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChunkyFileInfoBox";
            this.ShowInTaskbar = false;
            this.Text = "RelicChunky File Info";
            this.ResumeLayout(false);
        }

        #endregion Vom Windows Form-Designer generierter Code

        private ChunkyFileInfo m_cfiChunkInfo;
        private System.Windows.Forms.Button _btn_OK;
    }
}