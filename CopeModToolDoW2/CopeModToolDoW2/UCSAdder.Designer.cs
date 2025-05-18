namespace ModTool.FE
{
    partial class UCSAdder
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
            this._btn_OK = new System.Windows.Forms.Button();
            this._btn_cancel = new System.Windows.Forms.Button();
            this.m_chkbxAutoIndex = new System.Windows.Forms.CheckBox();
            this.m_rtbUCSText = new System.Windows.Forms.RichTextBox();
            this.m_nupIndex = new System.Windows.Forms.NumericUpDown();
            this.m_chkbxCopyToClipboard = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // _btn_OK
            // 
            this._btn_OK.Location = new System.Drawing.Point(306, 189);
            this._btn_OK.Name = "_btn_OK";
            this._btn_OK.Size = new System.Drawing.Size(75, 23);
            this._btn_OK.TabIndex = 0;
            this._btn_OK.Text = "OK";
            this._btn_OK.UseVisualStyleBackColor = true;
            this._btn_OK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // _btn_cancel
            // 
            this._btn_cancel.Location = new System.Drawing.Point(12, 189);
            this._btn_cancel.Name = "_btn_cancel";
            this._btn_cancel.Size = new System.Drawing.Size(75, 23);
            this._btn_cancel.TabIndex = 1;
            this._btn_cancel.Text = "Cancel";
            this._btn_cancel.UseVisualStyleBackColor = true;
            this._btn_cancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // m_chkbxAutoIndex
            // 
            this.m_chkbxAutoIndex.AutoSize = true;
            this.m_chkbxAutoIndex.Checked = true;
            this.m_chkbxAutoIndex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkbxAutoIndex.Location = new System.Drawing.Point(12, 152);
            this.m_chkbxAutoIndex.Name = "m_chkbxAutoIndex";
            this.m_chkbxAutoIndex.Size = new System.Drawing.Size(77, 17);
            this.m_chkbxAutoIndex.TabIndex = 2;
            this.m_chkbxAutoIndex.Text = "Auto-Index";
            this.m_chkbxAutoIndex.UseVisualStyleBackColor = true;
            this.m_chkbxAutoIndex.CheckedChanged += new System.EventHandler(this.ChkbxAutoIndexCheckedChanged);
            // 
            // m_rtbUCSText
            // 
            this.m_rtbUCSText.DetectUrls = false;
            this.m_rtbUCSText.Location = new System.Drawing.Point(12, 12);
            this.m_rtbUCSText.Name = "m_rtbUCSText";
            this.m_rtbUCSText.Size = new System.Drawing.Size(369, 134);
            this.m_rtbUCSText.TabIndex = 3;
            this.m_rtbUCSText.Text = "";
            // 
            // m_nupIndex
            // 
            this.m_nupIndex.Enabled = false;
            this.m_nupIndex.Location = new System.Drawing.Point(239, 151);
            this.m_nupIndex.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
            this.m_nupIndex.Name = "m_nupIndex";
            this.m_nupIndex.Size = new System.Drawing.Size(142, 20);
            this.m_nupIndex.TabIndex = 4;
            // 
            // m_chkbxCopyToClipboard
            // 
            this.m_chkbxCopyToClipboard.AutoSize = true;
            this.m_chkbxCopyToClipboard.Checked = true;
            this.m_chkbxCopyToClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkbxCopyToClipboard.Location = new System.Drawing.Point(95, 152);
            this.m_chkbxCopyToClipboard.Name = "m_chkbxCopyToClipboard";
            this.m_chkbxCopyToClipboard.Size = new System.Drawing.Size(138, 17);
            this.m_chkbxCopyToClipboard.TabIndex = 5;
            this.m_chkbxCopyToClipboard.Text = "Copy Index to Clipboard";
            this.m_chkbxCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // UCSAdder
            // 
            this.AcceptButton = this._btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 220);
            this.Controls.Add(this.m_chkbxCopyToClipboard);
            this.Controls.Add(this.m_nupIndex);
            this.Controls.Add(this.m_rtbUCSText);
            this.Controls.Add(this.m_chkbxAutoIndex);
            this.Controls.Add(this._btn_cancel);
            this.Controls.Add(this._btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UCSAdder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Add UCS String";
            this.Shown += new System.EventHandler(this.UCSAdderShown);
            ((System.ComponentModel.ISupportInitialize)(this.m_nupIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Button _btn_OK;
        private System.Windows.Forms.Button _btn_cancel;
        private System.Windows.Forms.CheckBox m_chkbxAutoIndex;
        private System.Windows.Forms.RichTextBox m_rtbUCSText;
        private System.Windows.Forms.NumericUpDown m_nupIndex;
        private System.Windows.Forms.CheckBox m_chkbxCopyToClipboard;
    }
}