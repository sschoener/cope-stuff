namespace QuickHash
{
    partial class Form1
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
            this.tbxIn = new System.Windows.Forms.TextBox();
            this.tbxOut = new System.Windows.Forms.TextBox();
            this.btnHash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxIn
            // 
            this.tbxIn.Location = new System.Drawing.Point(13, 13);
            this.tbxIn.Name = "tbxIn";
            this.tbxIn.Size = new System.Drawing.Size(259, 20);
            this.tbxIn.TabIndex = 0;
            // 
            // tbxOut
            // 
            this.tbxOut.Location = new System.Drawing.Point(13, 39);
            this.tbxOut.Name = "tbxOut";
            this.tbxOut.Size = new System.Drawing.Size(259, 20);
            this.tbxOut.TabIndex = 1;
            // 
            // btnHash
            // 
            this.btnHash.Location = new System.Drawing.Point(197, 66);
            this.btnHash.Name = "btnHash";
            this.btnHash.Size = new System.Drawing.Size(75, 23);
            this.btnHash.TabIndex = 2;
            this.btnHash.Text = "Hash!";
            this.btnHash.UseVisualStyleBackColor = true;
            this.btnHash.Click += new System.EventHandler(this.btnHash_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 98);
            this.Controls.Add(this.btnHash);
            this.Controls.Add(this.tbxOut);
            this.Controls.Add(this.tbxIn);
            this.Name = "Form1";
            this.Text = "RGD QuickHasher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxIn;
        private System.Windows.Forms.TextBox tbxOut;
        private System.Windows.Forms.Button btnHash;
    }
}

