using System.IO;

namespace ImageViewerPlugin
{
    partial class ImageViewer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._btn_save = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this._btn_saveAs = new System.Windows.Forms.Button();
            this.m_pnlImage = new System.Windows.Forms.Panel();
            this.m_picbxImage = new System.Windows.Forms.PictureBox();
            this._dlg_saveFile = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxImage)).BeginInit();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlImage, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(744, 603);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // panel1
            //
            this.panel1.Controls.Add(this._btn_save);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(663, 579);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(81, 24);
            this.panel1.TabIndex = 0;
            //
            // _btn_save
            //
            this._btn_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btn_save.Location = new System.Drawing.Point(0, 0);
            this._btn_save.Name = "_btn_save";
            this._btn_save.Size = new System.Drawing.Size(81, 24);
            this._btn_save.TabIndex = 0;
            this._btn_save.Text = "Save";
            this._btn_save.UseVisualStyleBackColor = true;
            this._btn_save.Click += new System.EventHandler(this.BtnSaveClick);
            //
            // panel2
            //
            this.panel2.Controls.Add(this._btn_saveAs);
            this.panel2.Location = new System.Drawing.Point(582, 579);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(81, 24);
            this.panel2.TabIndex = 2;
            //
            // _btn_saveAs
            //
            this._btn_saveAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btn_saveAs.Location = new System.Drawing.Point(0, 0);
            this._btn_saveAs.Name = "_btn_saveAs";
            this._btn_saveAs.Size = new System.Drawing.Size(81, 24);
            this._btn_saveAs.TabIndex = 0;
            this._btn_saveAs.Text = "Save as";
            this._btn_saveAs.UseVisualStyleBackColor = true;
            this._btn_saveAs.Click += new System.EventHandler(this.BtnSaveAsClick);
            //
            // _pnl_image
            //
            this.m_pnlImage.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.m_pnlImage, 3);
            this.m_pnlImage.Controls.Add(this.m_picbxImage);
            this.m_pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlImage.Location = new System.Drawing.Point(3, 3);
            this.m_pnlImage.Name = "m_pnlImage";
            this.m_pnlImage.Size = new System.Drawing.Size(738, 573);
            this.m_pnlImage.TabIndex = 3;
            //
            // _picbx_image
            //
            this.m_picbxImage.Location = new System.Drawing.Point(3, 0);
            this.m_picbxImage.Margin = new System.Windows.Forms.Padding(0);
            this.m_picbxImage.Name = "m_picbxImage";
            this.m_picbxImage.Size = new System.Drawing.Size(576, 572);
            this.m_picbxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_picbxImage.TabIndex = 4;
            this.m_picbxImage.TabStop = false;
            //
            // ImageViewer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ImageViewer";
            this.Size = new System.Drawing.Size(744, 603);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.m_pnlImage.ResumeLayout(false);
            this.m_pnlImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxImage)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _btn_save;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button _btn_saveAs;
        private System.Windows.Forms.SaveFileDialog _dlg_saveFile;
        private System.Windows.Forms.Panel m_pnlImage;
        private System.Windows.Forms.PictureBox m_picbxImage;
    }
}