namespace ModTool.Core.PlugIns.Text
{
    partial class TextEditor
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
            this.m_rtbText = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_rtbText, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(721, 652);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // panel1
            //
            this.panel1.Controls.Add(this._btn_save);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(643, 625);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(75, 24);
            this.panel1.TabIndex = 0;
            //
            // _btn_save
            //
            this._btn_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btn_save.Location = new System.Drawing.Point(0, 0);
            this._btn_save.Name = "_btn_save";
            this._btn_save.Size = new System.Drawing.Size(75, 24);
            this._btn_save.TabIndex = 0;
            this._btn_save.Text = "Save";
            this._btn_save.UseVisualStyleBackColor = true;
            this._btn_save.Click += new System.EventHandler(this.BtnSaveClick);
            //
            // _rtb_text
            //
            this.m_rtbText.AcceptsTab = true;
            this.m_rtbText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.m_rtbText, 2);
            this.m_rtbText.DetectUrls = false;
            this.m_rtbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbText.Location = new System.Drawing.Point(3, 3);
            this.m_rtbText.Name = "m_rtbText";
            this.m_rtbText.Size = new System.Drawing.Size(715, 616);
            this.m_rtbText.TabIndex = 1;
            this.m_rtbText.Text = "";
            //
            // TextEditor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TextEditor";
            this.Size = new System.Drawing.Size(721, 652);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _btn_save;
        private System.Windows.Forms.RichTextBox m_rtbText;
    }
}