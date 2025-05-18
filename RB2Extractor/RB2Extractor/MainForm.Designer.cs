namespace RB2Extractor
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_tbxRB2Path = new System.Windows.Forms.TextBox();
            this.m_tbxFLBPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnExtract = new System.Windows.Forms.Button();
            this.m_prgbarProgress = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.m_tbxOutputDir = new System.Windows.Forms.TextBox();
            this.m_btnSelectRB2File = new System.Windows.Forms.Button();
            this.m_btnSelectFLBFile = new System.Windows.Forms.Button();
            this.m_btnSelectOutputDir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.m_dlgSelectRB2 = new System.Windows.Forms.OpenFileDialog();
            this.m_dlgSelectFLB = new System.Windows.Forms.OpenFileDialog();
            this.m_dlgSelectOutputDir = new System.Windows.Forms.FolderBrowserDialog();
            this.m_labProgress = new System.Windows.Forms.Label();
            this.m_tbxRBFDirectory = new System.Windows.Forms.TextBox();
            this.m_btnSelectRBFDirectory = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.m_dlgSelectRBFDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.m_chkbxPerformConversion = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_tbxRB2Path
            // 
            this.m_tbxRB2Path.Location = new System.Drawing.Point(86, 6);
            this.m_tbxRB2Path.Name = "m_tbxRB2Path";
            this.m_tbxRB2Path.Size = new System.Drawing.Size(427, 20);
            this.m_tbxRB2Path.TabIndex = 0;
            // 
            // m_tbxFLBPath
            // 
            this.m_tbxFLBPath.Location = new System.Drawing.Point(86, 58);
            this.m_tbxFLBPath.Name = "m_tbxFLBPath";
            this.m_tbxFLBPath.Size = new System.Drawing.Size(427, 20);
            this.m_tbxFLBPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "RB2 file path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "FLB file path";
            // 
            // m_btnExtract
            // 
            this.m_btnExtract.Location = new System.Drawing.Point(519, 110);
            this.m_btnExtract.Name = "m_btnExtract";
            this.m_btnExtract.Size = new System.Drawing.Size(75, 23);
            this.m_btnExtract.TabIndex = 4;
            this.m_btnExtract.Text = "Extract";
            this.m_btnExtract.UseVisualStyleBackColor = true;
            this.m_btnExtract.Click += new System.EventHandler(this.BtnExtractClick);
            // 
            // m_prgbarProgress
            // 
            this.m_prgbarProgress.Location = new System.Drawing.Point(86, 110);
            this.m_prgbarProgress.Name = "m_prgbarProgress";
            this.m_prgbarProgress.Size = new System.Drawing.Size(427, 23);
            this.m_prgbarProgress.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Extract to...";
            // 
            // m_tbxOutputDir
            // 
            this.m_tbxOutputDir.Location = new System.Drawing.Point(86, 84);
            this.m_tbxOutputDir.Name = "m_tbxOutputDir";
            this.m_tbxOutputDir.Size = new System.Drawing.Size(427, 20);
            this.m_tbxOutputDir.TabIndex = 7;
            // 
            // m_btnSelectRB2File
            // 
            this.m_btnSelectRB2File.Location = new System.Drawing.Point(519, 4);
            this.m_btnSelectRB2File.Name = "m_btnSelectRB2File";
            this.m_btnSelectRB2File.Size = new System.Drawing.Size(75, 23);
            this.m_btnSelectRB2File.TabIndex = 8;
            this.m_btnSelectRB2File.Text = "Select";
            this.m_btnSelectRB2File.UseVisualStyleBackColor = true;
            this.m_btnSelectRB2File.Click += new System.EventHandler(this.BtnSelectRb2FileClick);
            // 
            // m_btnSelectFLBFile
            // 
            this.m_btnSelectFLBFile.Location = new System.Drawing.Point(519, 56);
            this.m_btnSelectFLBFile.Name = "m_btnSelectFLBFile";
            this.m_btnSelectFLBFile.Size = new System.Drawing.Size(75, 23);
            this.m_btnSelectFLBFile.TabIndex = 9;
            this.m_btnSelectFLBFile.Text = "Select";
            this.m_btnSelectFLBFile.UseVisualStyleBackColor = true;
            this.m_btnSelectFLBFile.Click += new System.EventHandler(this.BtnSelectFlbFileClick);
            // 
            // m_btnSelectOutputDir
            // 
            this.m_btnSelectOutputDir.Location = new System.Drawing.Point(519, 82);
            this.m_btnSelectOutputDir.Name = "m_btnSelectOutputDir";
            this.m_btnSelectOutputDir.Size = new System.Drawing.Size(75, 23);
            this.m_btnSelectOutputDir.TabIndex = 10;
            this.m_btnSelectOutputDir.Text = "Select";
            this.m_btnSelectOutputDir.UseVisualStyleBackColor = true;
            this.m_btnSelectOutputDir.Click += new System.EventHandler(this.BtnSelectOutputDirClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(388, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "v1.21, 03/06/2011, -cope";
            // 
            // m_dlgSelectRB2
            // 
            this.m_dlgSelectRB2.Filter = "RB2 files|*.rb2";
            this.m_dlgSelectRB2.Title = "Select RB2 file";
            // 
            // m_dlgSelectFLB
            // 
            this.m_dlgSelectFLB.Filter = "FLB files|*.flb";
            this.m_dlgSelectFLB.Title = "Select FLB file";
            // 
            // m_labProgress
            // 
            this.m_labProgress.AutoSize = true;
            this.m_labProgress.Location = new System.Drawing.Point(513, 140);
            this.m_labProgress.Name = "m_labProgress";
            this.m_labProgress.Size = new System.Drawing.Size(0, 13);
            this.m_labProgress.TabIndex = 12;
            this.m_labProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_tbxRBFDirectory
            // 
            this.m_tbxRBFDirectory.Location = new System.Drawing.Point(86, 32);
            this.m_tbxRBFDirectory.Name = "m_tbxRBFDirectory";
            this.m_tbxRBFDirectory.Size = new System.Drawing.Size(427, 20);
            this.m_tbxRBFDirectory.TabIndex = 14;
            // 
            // m_btnSelectRBFDirectory
            // 
            this.m_btnSelectRBFDirectory.Location = new System.Drawing.Point(519, 30);
            this.m_btnSelectRBFDirectory.Name = "m_btnSelectRBFDirectory";
            this.m_btnSelectRBFDirectory.Size = new System.Drawing.Size(75, 23);
            this.m_btnSelectRBFDirectory.TabIndex = 15;
            this.m_btnSelectRBFDirectory.Text = "Select";
            this.m_btnSelectRBFDirectory.UseVisualStyleBackColor = true;
            this.m_btnSelectRBFDirectory.Click += new System.EventHandler(this.BtnSelectRBFDirectoryClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "RBF directory";
            // 
            // m_chkbxPerformConversion
            // 
            this.m_chkbxPerformConversion.AutoSize = true;
            this.m_chkbxPerformConversion.Location = new System.Drawing.Point(86, 143);
            this.m_chkbxPerformConversion.Name = "m_chkbxPerformConversion";
            this.m_chkbxPerformConversion.Size = new System.Drawing.Size(171, 17);
            this.m_chkbxPerformConversion.TabIndex = 17;
            this.m_chkbxPerformConversion.Text = "Convert to pre-Retribution RBF";
            this.m_chkbxPerformConversion.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 172);
            this.Controls.Add(this.m_chkbxPerformConversion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_btnSelectRBFDirectory);
            this.Controls.Add(this.m_tbxRBFDirectory);
            this.Controls.Add(this.m_labProgress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_btnSelectOutputDir);
            this.Controls.Add(this.m_btnSelectFLBFile);
            this.Controls.Add(this.m_btnSelectRB2File);
            this.Controls.Add(this.m_tbxOutputDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_prgbarProgress);
            this.Controls.Add(this.m_btnExtract);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tbxFLBPath);
            this.Controls.Add(this.m_tbxRB2Path);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "RB2 extractor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbxRB2Path;
        private System.Windows.Forms.TextBox m_tbxFLBPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_btnExtract;
        private System.Windows.Forms.ProgressBar m_prgbarProgress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_tbxOutputDir;
        private System.Windows.Forms.Button m_btnSelectRB2File;
        private System.Windows.Forms.Button m_btnSelectFLBFile;
        private System.Windows.Forms.Button m_btnSelectOutputDir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog m_dlgSelectRB2;
        private System.Windows.Forms.OpenFileDialog m_dlgSelectFLB;
        private System.Windows.Forms.FolderBrowserDialog m_dlgSelectOutputDir;
        private System.Windows.Forms.Label m_labProgress;
        private System.Windows.Forms.TextBox m_tbxRBFDirectory;
        private System.Windows.Forms.Button m_btnSelectRBFDirectory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FolderBrowserDialog m_dlgSelectRBFDirectory;
        private System.Windows.Forms.CheckBox m_chkbxPerformConversion;
    }
}

