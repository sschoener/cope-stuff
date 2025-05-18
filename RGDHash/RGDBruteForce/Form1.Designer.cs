namespace RGDBruteForce
{
    partial class RGDBFMain
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
            this.tbxValues = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxInput = new System.Windows.Forms.TextBox();
            this.btnInputFile = new System.Windows.Forms.Button();
            this.nudMaxLength = new System.Windows.Forms.NumericUpDown();
            this.labLength = new System.Windows.Forms.Label();
            this.dlgSelectInputFile = new System.Windows.Forms.OpenFileDialog();
            this.lbxResults = new System.Windows.Forms.ListBox();
            this.btnDump = new System.Windows.Forms.Button();
            this.dlgSaveDump = new System.Windows.Forms.SaveFileDialog();
            this.nudMinLength = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinLength)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(346, 149);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(128, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbxValues
            // 
            this.tbxValues.Location = new System.Drawing.Point(13, 41);
            this.tbxValues.Name = "tbxValues";
            this.tbxValues.Size = new System.Drawing.Size(327, 20);
            this.tbxValues.TabIndex = 3;
            this.tbxValues.Text = "_abcdefghijklmnopqrstuvwxyz";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(346, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Values";
            // 
            // tbxInput
            // 
            this.tbxInput.Location = new System.Drawing.Point(13, 15);
            this.tbxInput.Name = "tbxInput";
            this.tbxInput.Size = new System.Drawing.Size(327, 20);
            this.tbxInput.TabIndex = 5;
            // 
            // btnInputFile
            // 
            this.btnInputFile.Location = new System.Drawing.Point(346, 13);
            this.btnInputFile.Name = "btnInputFile";
            this.btnInputFile.Size = new System.Drawing.Size(128, 23);
            this.btnInputFile.TabIndex = 6;
            this.btnInputFile.Text = "Select Input File";
            this.btnInputFile.UseVisualStyleBackColor = true;
            this.btnInputFile.Click += new System.EventHandler(this.btnInputFile_Click);
            // 
            // nudMaxLength
            // 
            this.nudMaxLength.Location = new System.Drawing.Point(175, 68);
            this.nudMaxLength.Maximum = new decimal(new int[] {
            101,
            0,
            0,
            0});
            this.nudMaxLength.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudMaxLength.Name = "nudMaxLength";
            this.nudMaxLength.Size = new System.Drawing.Size(164, 20);
            this.nudMaxLength.TabIndex = 8;
            this.nudMaxLength.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMaxLength.ValueChanged += new System.EventHandler(this.nudMaxLength_ValueChanged);
            // 
            // labLength
            // 
            this.labLength.AutoSize = true;
            this.labLength.Location = new System.Drawing.Point(346, 70);
            this.labLength.Name = "labLength";
            this.labLength.Size = new System.Drawing.Size(40, 13);
            this.labLength.TabIndex = 9;
            this.labLength.Text = "Length";
            // 
            // dlgSelectInputFile
            // 
            this.dlgSelectInputFile.Filter = "*.txt|*.txt";
            // 
            // lbxResults
            // 
            this.lbxResults.FormattingEnabled = true;
            this.lbxResults.Location = new System.Drawing.Point(496, 13);
            this.lbxResults.Name = "lbxResults";
            this.lbxResults.Size = new System.Drawing.Size(263, 160);
            this.lbxResults.TabIndex = 11;
            // 
            // btnDump
            // 
            this.btnDump.Location = new System.Drawing.Point(211, 149);
            this.btnDump.Name = "btnDump";
            this.btnDump.Size = new System.Drawing.Size(128, 23);
            this.btnDump.TabIndex = 12;
            this.btnDump.Text = "Dump results to file...";
            this.btnDump.UseVisualStyleBackColor = true;
            this.btnDump.Click += new System.EventHandler(this.btnDump_Click);
            // 
            // nudMinLength
            // 
            this.nudMinLength.Location = new System.Drawing.Point(13, 68);
            this.nudMinLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinLength.Name = "nudMinLength";
            this.nudMinLength.Size = new System.Drawing.Size(156, 20);
            this.nudMinLength.TabIndex = 13;
            this.nudMinLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinLength.ValueChanged += new System.EventHandler(this.nudMinLength_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "[ left value, right value )";
            // 
            // RGDBFMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 184);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudMinLength);
            this.Controls.Add(this.btnDump);
            this.Controls.Add(this.lbxResults);
            this.Controls.Add(this.labLength);
            this.Controls.Add(this.nudMaxLength);
            this.Controls.Add(this.btnInputFile);
            this.Controls.Add(this.tbxInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxValues);
            this.Controls.Add(this.btnStart);
            this.Name = "RGDBFMain";
            this.Text = "RGD BruteForce";
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbxValues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxInput;
        private System.Windows.Forms.Button btnInputFile;
        private System.Windows.Forms.NumericUpDown nudMaxLength;
        private System.Windows.Forms.Label labLength;
        private System.Windows.Forms.OpenFileDialog dlgSelectInputFile;
        private System.Windows.Forms.ListBox lbxResults;
        private System.Windows.Forms.Button btnDump;
        private System.Windows.Forms.SaveFileDialog dlgSaveDump;
        private System.Windows.Forms.NumericUpDown nudMinLength;
        private System.Windows.Forms.Label label2;
    }
}

