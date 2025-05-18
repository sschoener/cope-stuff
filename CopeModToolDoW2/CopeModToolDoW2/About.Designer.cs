namespace ModTool.FE
{
    partial class About
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
            this._btn_close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_rtbAboutBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // _btn_close
            // 
            this._btn_close.Location = new System.Drawing.Point(201, 406);
            this._btn_close.Name = "_btn_close";
            this._btn_close.Size = new System.Drawing.Size(75, 23);
            this._btn_close.TabIndex = 0;
            this._btn_close.Text = "Close";
            this._btn_close.UseVisualStyleBackColor = true;
            this._btn_close.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cope\'s DoW2 Toolbox, V1.991h";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "-cope. 09/01/2012";
            // 
            // m_rtbAboutBox
            // 
            this.m_rtbAboutBox.Location = new System.Drawing.Point(15, 25);
            this.m_rtbAboutBox.Name = "m_rtbAboutBox";
            this.m_rtbAboutBox.ReadOnly = true;
            this.m_rtbAboutBox.Size = new System.Drawing.Size(261, 375);
            this.m_rtbAboutBox.TabIndex = 4;
            this.m_rtbAboutBox.Text = "";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 436);
            this.Controls.Add(this.m_rtbAboutBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._btn_close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Vom Windows Form-Designer generierter Code

        private System.Windows.Forms.Button _btn_close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox m_rtbAboutBox;
    }
}