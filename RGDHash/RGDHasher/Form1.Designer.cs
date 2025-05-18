namespace RGDHasher
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
            this.btnStart = new System.Windows.Forms.Button();
            this.tbxStringFile = new System.Windows.Forms.TextBox();
            this.btnString = new System.Windows.Forms.Button();
            this.tbxDictionary = new System.Windows.Forms.TextBox();
            this.btnDict = new System.Windows.Forms.Button();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(459, 66);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start!";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbxStringFile
            // 
            this.tbxStringFile.Location = new System.Drawing.Point(12, 12);
            this.tbxStringFile.Name = "tbxStringFile";
            this.tbxStringFile.Size = new System.Drawing.Size(408, 20);
            this.tbxStringFile.TabIndex = 1;
            // 
            // btnString
            // 
            this.btnString.Location = new System.Drawing.Point(426, 10);
            this.btnString.Name = "btnString";
            this.btnString.Size = new System.Drawing.Size(113, 23);
            this.btnString.TabIndex = 2;
            this.btnString.Text = "Select StringFile";
            this.btnString.UseVisualStyleBackColor = true;
            this.btnString.Click += new System.EventHandler(this.btnString_Click);
            // 
            // tbxDictionary
            // 
            this.tbxDictionary.Location = new System.Drawing.Point(13, 39);
            this.tbxDictionary.Name = "tbxDictionary";
            this.tbxDictionary.Size = new System.Drawing.Size(407, 20);
            this.tbxDictionary.TabIndex = 3;
            // 
            // btnDict
            // 
            this.btnDict.Location = new System.Drawing.Point(426, 37);
            this.btnDict.Name = "btnDict";
            this.btnDict.Size = new System.Drawing.Size(113, 23);
            this.btnDict.TabIndex = 4;
            this.btnDict.Text = "Select RGD-Dict.";
            this.btnDict.UseVisualStyleBackColor = true;
            this.btnDict.Click += new System.EventHandler(this.btnDict_Click);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "*.txt|*.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 100);
            this.Controls.Add(this.btnDict);
            this.Controls.Add(this.tbxDictionary);
            this.Controls.Add(this.btnString);
            this.Controls.Add(this.tbxStringFile);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "RGD-Hasher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbxStringFile;
        private System.Windows.Forms.Button btnString;
        private System.Windows.Forms.TextBox tbxDictionary;
        private System.Windows.Forms.Button btnDict;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    }
}

