namespace RBFUpdater
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
            this.m_tbxFLBFile = new System.Windows.Forms.TextBox();
            this.m_btnSelectFLBFile = new System.Windows.Forms.Button();
            this.m_btnInputPath = new System.Windows.Forms.Button();
            this.m_btnOutputPath = new System.Windows.Forms.Button();
            this.m_btnConvert = new System.Windows.Forms.Button();
            this.m_tbxInputDirectory = new System.Windows.Forms.TextBox();
            this.m_tbxOutputDirectory = new System.Windows.Forms.TextBox();
            this.m_radToOld = new System.Windows.Forms.RadioButton();
            this.m_radToNew = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_dlgSelectFLBFile = new System.Windows.Forms.OpenFileDialog();
            this.m_dlgSelectInputDir = new System.Windows.Forms.FolderBrowserDialog();
            this.m_dlgSelectOutputDir = new System.Windows.Forms.FolderBrowserDialog();
            this.m_prgbarConversionProgress = new System.Windows.Forms.ProgressBar();
            this.m_labProgress = new System.Windows.Forms.Label();
            this.m_btnSwitchDirectories = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_tbxFLBFile
            // 
            this.m_tbxFLBFile.Location = new System.Drawing.Point(93, 14);
            this.m_tbxFLBFile.Name = "m_tbxFLBFile";
            this.m_tbxFLBFile.Size = new System.Drawing.Size(406, 20);
            this.m_tbxFLBFile.TabIndex = 0;
            // 
            // m_btnSelectFLBFile
            // 
            this.m_btnSelectFLBFile.Location = new System.Drawing.Point(12, 12);
            this.m_btnSelectFLBFile.Name = "m_btnSelectFLBFile";
            this.m_btnSelectFLBFile.Size = new System.Drawing.Size(75, 23);
            this.m_btnSelectFLBFile.TabIndex = 1;
            this.m_btnSelectFLBFile.Text = "FLB-File";
            this.m_btnSelectFLBFile.UseVisualStyleBackColor = true;
            this.m_btnSelectFLBFile.Click += new System.EventHandler(this.BtnSelectFlbFileClick);
            // 
            // m_btnInputPath
            // 
            this.m_btnInputPath.Location = new System.Drawing.Point(12, 41);
            this.m_btnInputPath.Name = "m_btnInputPath";
            this.m_btnInputPath.Size = new System.Drawing.Size(75, 23);
            this.m_btnInputPath.TabIndex = 2;
            this.m_btnInputPath.Text = "Input Dir";
            this.m_btnInputPath.UseVisualStyleBackColor = true;
            this.m_btnInputPath.Click += new System.EventHandler(this.BtnInputPathClick);
            // 
            // m_btnOutputPath
            // 
            this.m_btnOutputPath.Location = new System.Drawing.Point(12, 70);
            this.m_btnOutputPath.Name = "m_btnOutputPath";
            this.m_btnOutputPath.Size = new System.Drawing.Size(75, 23);
            this.m_btnOutputPath.TabIndex = 3;
            this.m_btnOutputPath.Text = "Output Dir";
            this.m_btnOutputPath.UseVisualStyleBackColor = true;
            this.m_btnOutputPath.Click += new System.EventHandler(this.BtnOutputPathClick);
            // 
            // m_btnConvert
            // 
            this.m_btnConvert.Location = new System.Drawing.Point(424, 98);
            this.m_btnConvert.Name = "m_btnConvert";
            this.m_btnConvert.Size = new System.Drawing.Size(75, 23);
            this.m_btnConvert.TabIndex = 4;
            this.m_btnConvert.Text = "Convert";
            this.m_btnConvert.UseVisualStyleBackColor = true;
            this.m_btnConvert.Click += new System.EventHandler(this.BtnConvertClick);
            // 
            // m_tbxInputDirectory
            // 
            this.m_tbxInputDirectory.Location = new System.Drawing.Point(93, 43);
            this.m_tbxInputDirectory.Name = "m_tbxInputDirectory";
            this.m_tbxInputDirectory.Size = new System.Drawing.Size(406, 20);
            this.m_tbxInputDirectory.TabIndex = 5;
            // 
            // m_tbxOutputDirectory
            // 
            this.m_tbxOutputDirectory.Location = new System.Drawing.Point(93, 72);
            this.m_tbxOutputDirectory.Name = "m_tbxOutputDirectory";
            this.m_tbxOutputDirectory.Size = new System.Drawing.Size(406, 20);
            this.m_tbxOutputDirectory.TabIndex = 6;
            // 
            // m_radToOld
            // 
            this.m_radToOld.AutoSize = true;
            this.m_radToOld.Location = new System.Drawing.Point(93, 101);
            this.m_radToOld.Name = "m_radToOld";
            this.m_radToOld.Size = new System.Drawing.Size(108, 17);
            this.m_radToOld.TabIndex = 7;
            this.m_radToOld.Text = "DoW2/CR format";
            this.m_radToOld.UseVisualStyleBackColor = true;
            // 
            // m_radToNew
            // 
            this.m_radToNew.AutoSize = true;
            this.m_radToNew.Checked = true;
            this.m_radToNew.Location = new System.Drawing.Point(220, 101);
            this.m_radToNew.Name = "m_radToNew";
            this.m_radToNew.Size = new System.Drawing.Size(108, 17);
            this.m_radToNew.TabIndex = 8;
            this.m_radToNew.TabStop = true;
            this.m_radToNew.Text = "Retribution format";
            this.m_radToNew.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Convert to:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "v1.2, 3/9/2011 by -cope.";
            // 
            // m_dlgSelectFLBFile
            // 
            this.m_dlgSelectFLBFile.Filter = "FLB|*.flb";
            // 
            // m_prgbarConversionProgress
            // 
            this.m_prgbarConversionProgress.Location = new System.Drawing.Point(12, 127);
            this.m_prgbarConversionProgress.Name = "m_prgbarConversionProgress";
            this.m_prgbarConversionProgress.Size = new System.Drawing.Size(487, 23);
            this.m_prgbarConversionProgress.TabIndex = 11;
            // 
            // m_labProgress
            // 
            this.m_labProgress.AutoSize = true;
            this.m_labProgress.Location = new System.Drawing.Point(386, 153);
            this.m_labProgress.Name = "m_labProgress";
            this.m_labProgress.Size = new System.Drawing.Size(0, 13);
            this.m_labProgress.TabIndex = 12;
            this.m_labProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_btnSwitchDirectories
            // 
            this.m_btnSwitchDirectories.Location = new System.Drawing.Point(343, 98);
            this.m_btnSwitchDirectories.Name = "m_btnSwitchDirectories";
            this.m_btnSwitchDirectories.Size = new System.Drawing.Size(75, 23);
            this.m_btnSwitchDirectories.TabIndex = 13;
            this.m_btnSwitchDirectories.Text = "Switch!";
            this.m_btnSwitchDirectories.UseVisualStyleBackColor = true;
            this.m_btnSwitchDirectories.Click += new System.EventHandler(this.BtnSwitchDirectoriesClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 171);
            this.Controls.Add(this.m_btnSwitchDirectories);
            this.Controls.Add(this.m_labProgress);
            this.Controls.Add(this.m_prgbarConversionProgress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_radToNew);
            this.Controls.Add(this.m_radToOld);
            this.Controls.Add(this.m_tbxOutputDirectory);
            this.Controls.Add(this.m_tbxInputDirectory);
            this.Controls.Add(this.m_btnConvert);
            this.Controls.Add(this.m_btnOutputPath);
            this.Controls.Add(this.m_btnInputPath);
            this.Controls.Add(this.m_btnSelectFLBFile);
            this.Controls.Add(this.m_tbxFLBFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Retribution RBF converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbxFLBFile;
        private System.Windows.Forms.Button m_btnSelectFLBFile;
        private System.Windows.Forms.Button m_btnInputPath;
        private System.Windows.Forms.Button m_btnOutputPath;
        private System.Windows.Forms.Button m_btnConvert;
        private System.Windows.Forms.TextBox m_tbxInputDirectory;
        private System.Windows.Forms.TextBox m_tbxOutputDirectory;
        private System.Windows.Forms.RadioButton m_radToOld;
        private System.Windows.Forms.RadioButton m_radToNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog m_dlgSelectFLBFile;
        private System.Windows.Forms.FolderBrowserDialog m_dlgSelectInputDir;
        private System.Windows.Forms.FolderBrowserDialog m_dlgSelectOutputDir;
        private System.Windows.Forms.ProgressBar m_prgbarConversionProgress;
        private System.Windows.Forms.Label m_labProgress;
        private System.Windows.Forms.Button m_btnSwitchDirectories;
    }
}

