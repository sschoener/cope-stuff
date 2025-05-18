namespace ModTool.FE
{
    partial class ReleaseModDialog
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
            this._btn_cancel = new System.Windows.Forms.Button();
            this._btn_release = new System.Windows.Forms.Button();
            this.m_tbxOutputDir = new System.Windows.Forms.TextBox();
            this._btn_selectOutputDir = new System.Windows.Forms.Button();
            this.m_dlgSelectOutputDir = new System.Windows.Forms.FolderBrowserDialog();
            this.m_chkbxInstallInstructions = new System.Windows.Forms.CheckBox();
            this.m_chkbxCreateLauncher = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_btnSelectIcon = new System.Windows.Forms.Button();
            this.m_tbxIconPath = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_labStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_dlgSelectIcon = new System.Windows.Forms.OpenFileDialog();
            this.m_radDoW2 = new System.Windows.Forms.RadioButton();
            this.m_radRetribution = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btn_cancel
            // 
            this._btn_cancel.Location = new System.Drawing.Point(18, 140);
            this._btn_cancel.Name = "_btn_cancel";
            this._btn_cancel.Size = new System.Drawing.Size(75, 23);
            this._btn_cancel.TabIndex = 0;
            this._btn_cancel.Text = "Cancel";
            this._btn_cancel.UseVisualStyleBackColor = true;
            this._btn_cancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // _btn_release
            // 
            this._btn_release.Location = new System.Drawing.Point(444, 140);
            this._btn_release.Name = "_btn_release";
            this._btn_release.Size = new System.Drawing.Size(126, 23);
            this._btn_release.TabIndex = 1;
            this._btn_release.Text = "Release!";
            this._btn_release.UseVisualStyleBackColor = true;
            this._btn_release.Click += new System.EventHandler(this.BtnReleaseClick);
            // 
            // m_tbxOutputDir
            // 
            this.m_tbxOutputDir.Location = new System.Drawing.Point(162, 13);
            this.m_tbxOutputDir.Name = "m_tbxOutputDir";
            this.m_tbxOutputDir.Size = new System.Drawing.Size(408, 20);
            this.m_tbxOutputDir.TabIndex = 2;
            // 
            // _btn_selectOutputDir
            // 
            this._btn_selectOutputDir.Location = new System.Drawing.Point(18, 11);
            this._btn_selectOutputDir.Name = "_btn_selectOutputDir";
            this._btn_selectOutputDir.Size = new System.Drawing.Size(135, 23);
            this._btn_selectOutputDir.TabIndex = 4;
            this._btn_selectOutputDir.Text = "Select Output Directory";
            this._btn_selectOutputDir.UseVisualStyleBackColor = true;
            this._btn_selectOutputDir.Click += new System.EventHandler(this.BtnSelectOutputDirClick);
            // 
            // m_chkbxInstallInstructions
            // 
            this.m_chkbxInstallInstructions.AutoSize = true;
            this.m_chkbxInstallInstructions.Checked = true;
            this.m_chkbxInstallInstructions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkbxInstallInstructions.Location = new System.Drawing.Point(6, 19);
            this.m_chkbxInstallInstructions.Name = "m_chkbxInstallInstructions";
            this.m_chkbxInstallInstructions.Size = new System.Drawing.Size(167, 17);
            this.m_chkbxInstallInstructions.TabIndex = 6;
            this.m_chkbxInstallInstructions.Text = "Create Installation Instructions";
            this.m_chkbxInstallInstructions.UseVisualStyleBackColor = true;
            // 
            // m_chkbxCreateLauncher
            // 
            this.m_chkbxCreateLauncher.AutoSize = true;
            this.m_chkbxCreateLauncher.Checked = true;
            this.m_chkbxCreateLauncher.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkbxCreateLauncher.Location = new System.Drawing.Point(6, 42);
            this.m_chkbxCreateLauncher.Name = "m_chkbxCreateLauncher";
            this.m_chkbxCreateLauncher.Size = new System.Drawing.Size(139, 17);
            this.m_chkbxCreateLauncher.TabIndex = 7;
            this.m_chkbxCreateLauncher.Text = "Create Simple Launcher";
            this.m_chkbxCreateLauncher.UseVisualStyleBackColor = true;
            this.m_chkbxCreateLauncher.CheckedChanged += new System.EventHandler(this.ChkbxCreateLauncherCheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_radRetribution);
            this.groupBox1.Controls.Add(this.m_radDoW2);
            this.groupBox1.Controls.Add(this.m_btnSelectIcon);
            this.groupBox1.Controls.Add(this.m_tbxIconPath);
            this.groupBox1.Controls.Add(this.m_chkbxInstallInstructions);
            this.groupBox1.Controls.Add(this.m_chkbxCreateLauncher);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 95);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // m_btnSelectIcon
            // 
            this.m_btnSelectIcon.Location = new System.Drawing.Point(6, 65);
            this.m_btnSelectIcon.Name = "m_btnSelectIcon";
            this.m_btnSelectIcon.Size = new System.Drawing.Size(135, 23);
            this.m_btnSelectIcon.TabIndex = 9;
            this.m_btnSelectIcon.Text = "Select Launcher Icon";
            this.m_btnSelectIcon.UseVisualStyleBackColor = true;
            this.m_btnSelectIcon.Click += new System.EventHandler(this.BtnSelectIconClick);
            // 
            // m_tbxIconPath
            // 
            this.m_tbxIconPath.Location = new System.Drawing.Point(147, 67);
            this.m_tbxIconPath.Name = "m_tbxIconPath";
            this.m_tbxIconPath.Size = new System.Drawing.Size(411, 20);
            this.m_tbxIconPath.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_labStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 171);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(588, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // m_labStatus
            // 
            this.m_labStatus.Name = "m_labStatus";
            this.m_labStatus.Size = new System.Drawing.Size(39, 17);
            this.m_labStatus.Text = "Ready";
            // 
            // m_dlgSelectIcon
            // 
            this.m_dlgSelectIcon.Filter = "Icon Files|*.ico";
            this.m_dlgSelectIcon.Title = "Select an Icon";
            // 
            // m_radDoW2
            // 
            this.m_radDoW2.AutoSize = true;
            this.m_radDoW2.Location = new System.Drawing.Point(147, 41);
            this.m_radDoW2.Name = "m_radDoW2";
            this.m_radDoW2.Size = new System.Drawing.Size(91, 17);
            this.m_radDoW2.TabIndex = 10;
            this.m_radDoW2.Text = "for DoW2/CR";
            this.m_radDoW2.UseVisualStyleBackColor = true;
            // 
            // m_radRetribution
            // 
            this.m_radRetribution.AutoSize = true;
            this.m_radRetribution.Checked = true;
            this.m_radRetribution.Location = new System.Drawing.Point(244, 41);
            this.m_radRetribution.Name = "m_radRetribution";
            this.m_radRetribution.Size = new System.Drawing.Size(91, 17);
            this.m_radRetribution.TabIndex = 11;
            this.m_radRetribution.TabStop = true;
            this.m_radRetribution.Text = "for Retribution";
            this.m_radRetribution.UseVisualStyleBackColor = true;
            // 
            // ReleaseModDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 193);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._btn_selectOutputDir);
            this.Controls.Add(this.m_tbxOutputDir);
            this.Controls.Add(this._btn_release);
            this.Controls.Add(this._btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReleaseModDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Release Mod";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Button _btn_cancel;
        private System.Windows.Forms.Button _btn_release;
        private System.Windows.Forms.TextBox m_tbxOutputDir;
        private System.Windows.Forms.Button _btn_selectOutputDir;
        private System.Windows.Forms.FolderBrowserDialog m_dlgSelectOutputDir;
        private System.Windows.Forms.CheckBox m_chkbxInstallInstructions;
        private System.Windows.Forms.CheckBox m_chkbxCreateLauncher;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel m_labStatus;
        private System.Windows.Forms.Button m_btnSelectIcon;
        private System.Windows.Forms.TextBox m_tbxIconPath;
        private System.Windows.Forms.OpenFileDialog m_dlgSelectIcon;
        private System.Windows.Forms.RadioButton m_radRetribution;
        private System.Windows.Forms.RadioButton m_radDoW2;
    }
}