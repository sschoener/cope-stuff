namespace ModTool.FE
{
    partial class RenameFile
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
            this._btn_ok = new System.Windows.Forms.Button();
            this._btn_cancel = new System.Windows.Forms.Button();
            this.m_tbxNewName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _btn_ok
            // 
            this._btn_ok.Location = new System.Drawing.Point(253, 49);
            this._btn_ok.Name = "_btn_ok";
            this._btn_ok.Size = new System.Drawing.Size(75, 23);
            this._btn_ok.TabIndex = 1;
            this._btn_ok.Text = "OK";
            this._btn_ok.UseVisualStyleBackColor = true;
            this._btn_ok.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // _btn_cancel
            // 
            this._btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btn_cancel.Location = new System.Drawing.Point(12, 49);
            this._btn_cancel.Name = "_btn_cancel";
            this._btn_cancel.Size = new System.Drawing.Size(75, 23);
            this._btn_cancel.TabIndex = 2;
            this._btn_cancel.Text = "Cancel";
            this._btn_cancel.UseVisualStyleBackColor = true;
            this._btn_cancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // m_tbxNewName
            // 
            this.m_tbxNewName.Location = new System.Drawing.Point(12, 23);
            this.m_tbxNewName.Name = "m_tbxNewName";
            this.m_tbxNewName.Size = new System.Drawing.Size(316, 20);
            this.m_tbxNewName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "New name:";
            // 
            // RenameFile
            // 
            this.AcceptButton = this._btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btn_cancel;
            this.ClientSize = new System.Drawing.Size(340, 80);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tbxNewName);
            this.Controls.Add(this._btn_cancel);
            this.Controls.Add(this._btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RenameFile";
            this.Text = "Rename File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btn_ok;
        private System.Windows.Forms.Button _btn_cancel;
        private System.Windows.Forms.TextBox m_tbxNewName;
        private System.Windows.Forms.Label label1;
    }
}