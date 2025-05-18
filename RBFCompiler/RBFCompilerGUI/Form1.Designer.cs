using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RBFCompilerGUI
{
    partial class Form1 : Form
    {
        // Fields
        private Button m_btnAbout;
        private Button m_btnLuaFile;
        private Button m_btnModuleFile;
        private Button m_btnSourceDir;
        private Button m_btnStart;
        private Button m_btnTargetDir;
        private FolderBrowserDialog m_dlgFolderBrowserOutput;
        private FolderBrowserDialog m_dlgFolderBrowserSource;
        private OpenFileDialog m_dlgOpenLuaFile;
        private OpenFileDialog m_dlgOpenModuleFile;
        private ListBox m_lbxReports;
        private TextWriter _log_file;
        private TextBox m_tbxLuaFile;
        private TextBox m_tbxModuleFile;
        private TextBox m_tbxSourceDir;
        private TextBox m_tbxTargetDir;
        private IContainer components;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.m_dlgOpenLuaFile = new System.Windows.Forms.OpenFileDialog();
            this.m_dlgFolderBrowserSource = new System.Windows.Forms.FolderBrowserDialog();
            this.m_dlgFolderBrowserOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.m_btnTargetDir = new System.Windows.Forms.Button();
            this.m_btnSourceDir = new System.Windows.Forms.Button();
            this.m_btnLuaFile = new System.Windows.Forms.Button();
            this.m_btnModuleFile = new System.Windows.Forms.Button();
            this.m_tbxTargetDir = new System.Windows.Forms.TextBox();
            this.m_tbxSourceDir = new System.Windows.Forms.TextBox();
            this.m_tbxLuaFile = new System.Windows.Forms.TextBox();
            this.m_dlgOpenModuleFile = new System.Windows.Forms.OpenFileDialog();
            this.m_tbxModuleFile = new System.Windows.Forms.TextBox();
            this.m_lbxReports = new System.Windows.Forms.ListBox();
            this.m_btnStart = new System.Windows.Forms.Button();
            this.m_btnAbout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_dlgOpenLuaFile
            // 
            this.m_dlgOpenLuaFile.Filter = "LUA|*.lua";
            // 
            // m_btnTargetDir
            // 
            this.m_btnTargetDir.Location = new System.Drawing.Point(13, 13);
            this.m_btnTargetDir.Name = "m_btnTargetDir";
            this.m_btnTargetDir.Size = new System.Drawing.Size(140, 23);
            this.m_btnTargetDir.TabIndex = 0;
            this.m_btnTargetDir.Text = "Output Directory";
            this.m_btnTargetDir.UseVisualStyleBackColor = true;
            this.m_btnTargetDir.Click += new System.EventHandler(this.BtnTargetDirClick);
            // 
            // m_btnSourceDir
            // 
            this.m_btnSourceDir.Location = new System.Drawing.Point(13, 43);
            this.m_btnSourceDir.Name = "m_btnSourceDir";
            this.m_btnSourceDir.Size = new System.Drawing.Size(140, 23);
            this.m_btnSourceDir.TabIndex = 1;
            this.m_btnSourceDir.Text = "Source Directory";
            this.m_btnSourceDir.UseVisualStyleBackColor = true;
            this.m_btnSourceDir.Click += new System.EventHandler(this.BtnSourceDirClick);
            // 
            // m_btnLuaFile
            // 
            this.m_btnLuaFile.Location = new System.Drawing.Point(13, 73);
            this.m_btnLuaFile.Name = "m_btnLuaFile";
            this.m_btnLuaFile.Size = new System.Drawing.Size(140, 23);
            this.m_btnLuaFile.TabIndex = 2;
            this.m_btnLuaFile.Text = "LUA file to start with";
            this.m_btnLuaFile.UseVisualStyleBackColor = true;
            this.m_btnLuaFile.Click += new System.EventHandler(this.BtnLuaFileClick);
            // 
            // m_btnModuleFile
            // 
            this.m_btnModuleFile.Location = new System.Drawing.Point(13, 103);
            this.m_btnModuleFile.Name = "m_btnModuleFile";
            this.m_btnModuleFile.Size = new System.Drawing.Size(140, 23);
            this.m_btnModuleFile.TabIndex = 3;
            this.m_btnModuleFile.Text = "Module File";
            this.m_btnModuleFile.UseVisualStyleBackColor = true;
            this.m_btnModuleFile.Click += new System.EventHandler(this.BtnModuleFileClick);
            // 
            // m_tbxTargetDir
            // 
            this.m_tbxTargetDir.Location = new System.Drawing.Point(159, 15);
            this.m_tbxTargetDir.Name = "m_tbxTargetDir";
            this.m_tbxTargetDir.Size = new System.Drawing.Size(545, 20);
            this.m_tbxTargetDir.TabIndex = 4;
            // 
            // m_tbxSourceDir
            // 
            this.m_tbxSourceDir.Location = new System.Drawing.Point(159, 45);
            this.m_tbxSourceDir.Name = "m_tbxSourceDir";
            this.m_tbxSourceDir.Size = new System.Drawing.Size(545, 20);
            this.m_tbxSourceDir.TabIndex = 5;
            // 
            // m_tbxLuaFile
            // 
            this.m_tbxLuaFile.Location = new System.Drawing.Point(159, 75);
            this.m_tbxLuaFile.Name = "m_tbxLuaFile";
            this.m_tbxLuaFile.Size = new System.Drawing.Size(545, 20);
            this.m_tbxLuaFile.TabIndex = 6;
            // 
            // m_tbxModuleFile
            // 
            this.m_tbxModuleFile.Location = new System.Drawing.Point(159, 105);
            this.m_tbxModuleFile.Name = "m_tbxModuleFile";
            this.m_tbxModuleFile.Size = new System.Drawing.Size(545, 20);
            this.m_tbxModuleFile.TabIndex = 7;
            // 
            // m_lbxReports
            // 
            this.m_lbxReports.FormattingEnabled = true;
            this.m_lbxReports.HorizontalScrollbar = true;
            this.m_lbxReports.Location = new System.Drawing.Point(13, 133);
            this.m_lbxReports.Name = "m_lbxReports";
            this.m_lbxReports.Size = new System.Drawing.Size(691, 277);
            this.m_lbxReports.TabIndex = 8;
            // 
            // m_btnStart
            // 
            this.m_btnStart.Location = new System.Drawing.Point(629, 416);
            this.m_btnStart.Name = "m_btnStart";
            this.m_btnStart.Size = new System.Drawing.Size(75, 23);
            this.m_btnStart.TabIndex = 9;
            this.m_btnStart.Text = "Compile!";
            this.m_btnStart.UseVisualStyleBackColor = true;
            this.m_btnStart.Click += new System.EventHandler(this.BtnStartClick);
            // 
            // m_btnAbout
            // 
            this.m_btnAbout.Location = new System.Drawing.Point(12, 416);
            this.m_btnAbout.Name = "m_btnAbout";
            this.m_btnAbout.Size = new System.Drawing.Size(75, 23);
            this.m_btnAbout.TabIndex = 10;
            this.m_btnAbout.Text = "About";
            this.m_btnAbout.UseVisualStyleBackColor = true;
            this.m_btnAbout.Click += new System.EventHandler(this.BtnAboutClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 444);
            this.Controls.Add(this.m_btnAbout);
            this.Controls.Add(this.m_btnStart);
            this.Controls.Add(this.m_lbxReports);
            this.Controls.Add(this.m_tbxModuleFile);
            this.Controls.Add(this.m_tbxLuaFile);
            this.Controls.Add(this.m_tbxSourceDir);
            this.Controls.Add(this.m_tbxTargetDir);
            this.Controls.Add(this.m_btnModuleFile);
            this.Controls.Add(this.m_btnLuaFile);
            this.Controls.Add(this.m_btnSourceDir);
            this.Controls.Add(this.m_btnTargetDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Cope\'s RBF Compiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }


}

