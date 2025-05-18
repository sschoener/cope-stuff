namespace CopeDefenseLauncher
{
    partial class NewHeroForm
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
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_tbxHeroName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cbxHeroType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // m_btnOK
            // 
            this.m_btnOK.Location = new System.Drawing.Point(321, 65);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 0;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(12, 65);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hero name:";
            // 
            // m_tbxHeroName
            // 
            this.m_tbxHeroName.Location = new System.Drawing.Point(94, 10);
            this.m_tbxHeroName.MaxLength = 25;
            this.m_tbxHeroName.Name = "m_tbxHeroName";
            this.m_tbxHeroName.Size = new System.Drawing.Size(302, 20);
            this.m_tbxHeroName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hero type:";
            // 
            // m_cbxHeroType
            // 
            this.m_cbxHeroType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxHeroType.FormattingEnabled = true;
            this.m_cbxHeroType.Location = new System.Drawing.Point(94, 38);
            this.m_cbxHeroType.Name = "m_cbxHeroType";
            this.m_cbxHeroType.Size = new System.Drawing.Size(302, 21);
            this.m_cbxHeroType.TabIndex = 7;
            // 
            // NewHeroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 93);
            this.Controls.Add(this.m_cbxHeroType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_tbxHeroName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewHeroForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "New Hero";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbxHeroName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox m_cbxHeroType;
    }
}