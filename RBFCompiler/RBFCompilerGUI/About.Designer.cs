using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RBFCompilerGUI
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button m_btnClose;
        private Label m_labTitle;
        private Label m_labDate;
        private RichTextBox m_rtbAbout;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.m_btnClose = new System.Windows.Forms.Button();
            this.m_labTitle = new System.Windows.Forms.Label();
            this.m_labDate = new System.Windows.Forms.Label();
            this.m_rtbAbout = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // m_btnClose
            // 
            this.m_btnClose.Location = new System.Drawing.Point(197, 227);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 0;
            this.m_btnClose.Text = "Close";
            this.m_btnClose.UseVisualStyleBackColor = true;
            this.m_btnClose.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // m_labTitle
            // 
            this.m_labTitle.AutoSize = true;
            this.m_labTitle.Location = new System.Drawing.Point(12, 9);
            this.m_labTitle.Name = "m_labTitle";
            this.m_labTitle.Size = new System.Drawing.Size(108, 13);
            this.m_labTitle.TabIndex = 1;
            this.m_labTitle.Text = "RBF Compiler, V.1.22";
            // 
            // m_labDate
            // 
            this.m_labDate.AutoSize = true;
            this.m_labDate.Location = new System.Drawing.Point(174, 9);
            this.m_labDate.Name = "m_labDate";
            this.m_labDate.Size = new System.Drawing.Size(98, 13);
            this.m_labDate.TabIndex = 2;
            this.m_labDate.Text = "-cope. 11/13/2010";
            // 
            // m_rtbAbout
            // 
            this.m_rtbAbout.Location = new System.Drawing.Point(12, 25);
            this.m_rtbAbout.Name = "m_rtbAbout";
            this.m_rtbAbout.ReadOnly = true;
            this.m_rtbAbout.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.m_rtbAbout.Size = new System.Drawing.Size(260, 196);
            this.m_rtbAbout.TabIndex = 3;
            this.m_rtbAbout.Text = "";
            // 
            // About
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.m_rtbAbout);
            this.Controls.Add(this.m_labDate);
            this.Controls.Add(this.m_labTitle);
            this.Controls.Add(this.m_btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}