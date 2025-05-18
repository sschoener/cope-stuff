namespace ModTool.Core.PlugIns.RelicChunky
{
    partial class ChunkyFileInfo
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
            this._lab_headerSize = new System.Windows.Forms.Label();
            this._lab_chunkHeaderSize = new System.Windows.Forms.Label();
            this._lab_version = new System.Windows.Forms.Label();
            this._lab_minVersion = new System.Windows.Forms.Label();
            this._lab_platform = new System.Windows.Forms.Label();
            this.m_tbxHeaderSize = new System.Windows.Forms.TextBox();
            this.m_tbxChunkHeaderSize = new System.Windows.Forms.TextBox();
            this.m_tbxVersion = new System.Windows.Forms.TextBox();
            this.m_tbxMinVersion = new System.Windows.Forms.TextBox();
            this.m_tbxPlatform = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //
            // _lab_headerSize
            //
            this._lab_headerSize.AutoSize = true;
            this._lab_headerSize.Location = new System.Drawing.Point(6, 6);
            this._lab_headerSize.Name = "_lab_headerSize";
            this._lab_headerSize.Size = new System.Drawing.Size(65, 13);
            this._lab_headerSize.TabIndex = 0;
            this._lab_headerSize.Text = "Header Size";
            //
            // _lab_chunkHeaderSize
            //
            this._lab_chunkHeaderSize.AutoSize = true;
            this._lab_chunkHeaderSize.Location = new System.Drawing.Point(6, 29);
            this._lab_chunkHeaderSize.Name = "_lab_chunkHeaderSize";
            this._lab_chunkHeaderSize.Size = new System.Drawing.Size(99, 13);
            this._lab_chunkHeaderSize.TabIndex = 1;
            this._lab_chunkHeaderSize.Text = "Chunk Header Size";
            //
            // _lab_version
            //
            this._lab_version.AutoSize = true;
            this._lab_version.Location = new System.Drawing.Point(6, 52);
            this._lab_version.Name = "_lab_version";
            this._lab_version.Size = new System.Drawing.Size(42, 13);
            this._lab_version.TabIndex = 2;
            this._lab_version.Text = "Version";
            //
            // _lab_minVersion
            //
            this._lab_minVersion.AutoSize = true;
            this._lab_minVersion.Location = new System.Drawing.Point(6, 75);
            this._lab_minVersion.Name = "_lab_minVersion";
            this._lab_minVersion.Size = new System.Drawing.Size(65, 13);
            this._lab_minVersion.TabIndex = 3;
            this._lab_minVersion.Text = "Min. Version";
            //
            // _lab_platform
            //
            this._lab_platform.AutoSize = true;
            this._lab_platform.Location = new System.Drawing.Point(6, 98);
            this._lab_platform.Name = "_lab_platform";
            this._lab_platform.Size = new System.Drawing.Size(45, 13);
            this._lab_platform.TabIndex = 4;
            this._lab_platform.Text = "Platform";
            //
            // _tbx_headerSize
            //
            this.m_tbxHeaderSize.Location = new System.Drawing.Point(111, 3);
            this.m_tbxHeaderSize.Name = "m_tbxHeaderSize";
            this.m_tbxHeaderSize.ReadOnly = true;
            this.m_tbxHeaderSize.Size = new System.Drawing.Size(100, 20);
            this.m_tbxHeaderSize.TabIndex = 5;
            //
            // _tbx_chunkHeaderSize
            //
            this.m_tbxChunkHeaderSize.Location = new System.Drawing.Point(111, 26);
            this.m_tbxChunkHeaderSize.Name = "m_tbxChunkHeaderSize";
            this.m_tbxChunkHeaderSize.ReadOnly = true;
            this.m_tbxChunkHeaderSize.Size = new System.Drawing.Size(100, 20);
            this.m_tbxChunkHeaderSize.TabIndex = 6;
            //
            // _tbx_version
            //
            this.m_tbxVersion.Location = new System.Drawing.Point(111, 49);
            this.m_tbxVersion.Name = "m_tbxVersion";
            this.m_tbxVersion.ReadOnly = true;
            this.m_tbxVersion.Size = new System.Drawing.Size(100, 20);
            this.m_tbxVersion.TabIndex = 7;
            //
            // _tbx_minVersion
            //
            this.m_tbxMinVersion.Location = new System.Drawing.Point(111, 72);
            this.m_tbxMinVersion.Name = "m_tbxMinVersion";
            this.m_tbxMinVersion.ReadOnly = true;
            this.m_tbxMinVersion.Size = new System.Drawing.Size(100, 20);
            this.m_tbxMinVersion.TabIndex = 8;
            //
            // _tbx_platform
            //
            this.m_tbxPlatform.Location = new System.Drawing.Point(111, 95);
            this.m_tbxPlatform.Name = "m_tbxPlatform";
            this.m_tbxPlatform.ReadOnly = true;
            this.m_tbxPlatform.Size = new System.Drawing.Size(100, 20);
            this.m_tbxPlatform.TabIndex = 9;
            //
            // ChunkyFileInfo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_tbxPlatform);
            this.Controls.Add(this.m_tbxMinVersion);
            this.Controls.Add(this.m_tbxVersion);
            this.Controls.Add(this.m_tbxChunkHeaderSize);
            this.Controls.Add(this.m_tbxHeaderSize);
            this.Controls.Add(this._lab_platform);
            this.Controls.Add(this._lab_minVersion);
            this.Controls.Add(this._lab_version);
            this.Controls.Add(this._lab_chunkHeaderSize);
            this.Controls.Add(this._lab_headerSize);
            this.Name = "ChunkyFileInfo";
            this.Size = new System.Drawing.Size(220, 119);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Vom Komponenten-Designer generierter Code

        private System.Windows.Forms.Label _lab_headerSize;
        private System.Windows.Forms.Label _lab_chunkHeaderSize;
        private System.Windows.Forms.Label _lab_version;
        private System.Windows.Forms.Label _lab_minVersion;
        private System.Windows.Forms.Label _lab_platform;
        private System.Windows.Forms.TextBox m_tbxHeaderSize;
        private System.Windows.Forms.TextBox m_tbxChunkHeaderSize;
        private System.Windows.Forms.TextBox m_tbxVersion;
        private System.Windows.Forms.TextBox m_tbxMinVersion;
        private System.Windows.Forms.TextBox m_tbxPlatform;
    }
}