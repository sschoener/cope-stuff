using System.Windows.Forms;

namespace ModTool.Core
{
    partial class FileTreeControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_trvFileTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // m_trvFileTreeView
            // 
            this.m_trvFileTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_trvFileTreeView.Location = new System.Drawing.Point(0, 0);
            this.m_trvFileTreeView.Margin = new System.Windows.Forms.Padding(0);
            this.m_trvFileTreeView.Name = "m_trvFileTreeView";
            this.m_trvFileTreeView.ShowNodeToolTips = true;
            this.m_trvFileTreeView.Size = new System.Drawing.Size(392, 527);
            this.m_trvFileTreeView.TabIndex = 0;
            this.m_trvFileTreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TrvFileTreeViewAfterCollapse);
            this.m_trvFileTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TrvFileTreeViewAfterExpand);
            // 
            // FileTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_trvFileTreeView);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FileTreeControl";
            this.Size = new System.Drawing.Size(392, 527);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private System.Windows.Forms.TreeView m_trvFileTreeView;
    }
}