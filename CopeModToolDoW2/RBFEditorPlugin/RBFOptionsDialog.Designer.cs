namespace RBFPlugin
{
    partial class RBFOptionsDialog
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
            this.m_chkbxReloadOnSave = new System.Windows.Forms.CheckBox();
            this.m_chkbxUseRetributionLoading = new System.Windows.Forms.CheckBox();
            this.m_chkbxUseRetributionSaving = new System.Windows.Forms.CheckBox();
            this.m_chkbxUseAutoCompletion = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_btnOK
            // 
            this.m_btnOK.Location = new System.Drawing.Point(197, 118);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 0;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(12, 118);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // m_chkbxReloadOnSave
            // 
            this.m_chkbxReloadOnSave.AutoSize = true;
            this.m_chkbxReloadOnSave.Location = new System.Drawing.Point(12, 12);
            this.m_chkbxReloadOnSave.Name = "m_chkbxReloadOnSave";
            this.m_chkbxReloadOnSave.Size = new System.Drawing.Size(238, 30);
            this.m_chkbxReloadOnSave.TabIndex = 2;
            this.m_chkbxReloadOnSave.Text = "Automatically reload RBFs on saving when in\r\nAdvanced Test Mode";
            this.m_chkbxReloadOnSave.UseVisualStyleBackColor = true;
            // 
            // m_chkbxUseRetributionLoading
            // 
            this.m_chkbxUseRetributionLoading.AutoSize = true;
            this.m_chkbxUseRetributionLoading.Location = new System.Drawing.Point(12, 48);
            this.m_chkbxUseRetributionLoading.Name = "m_chkbxUseRetributionLoading";
            this.m_chkbxUseRetributionLoading.Size = new System.Drawing.Size(176, 17);
            this.m_chkbxUseRetributionLoading.TabIndex = 3;
            this.m_chkbxUseRetributionLoading.Text = "Load RBFs as Retribution-RBFs";
            this.m_chkbxUseRetributionLoading.UseVisualStyleBackColor = true;
            // 
            // m_chkbxUseRetributionSaving
            // 
            this.m_chkbxUseRetributionSaving.AutoSize = true;
            this.m_chkbxUseRetributionSaving.Location = new System.Drawing.Point(12, 72);
            this.m_chkbxUseRetributionSaving.Name = "m_chkbxUseRetributionSaving";
            this.m_chkbxUseRetributionSaving.Size = new System.Drawing.Size(177, 17);
            this.m_chkbxUseRetributionSaving.TabIndex = 4;
            this.m_chkbxUseRetributionSaving.Text = "Save RBFs as Retribution-RBFs";
            this.m_chkbxUseRetributionSaving.UseVisualStyleBackColor = true;
            // 
            // m_chkbxUseAutoCompletion
            // 
            this.m_chkbxUseAutoCompletion.AutoSize = true;
            this.m_chkbxUseAutoCompletion.Location = new System.Drawing.Point(12, 95);
            this.m_chkbxUseAutoCompletion.Name = "m_chkbxUseAutoCompletion";
            this.m_chkbxUseAutoCompletion.Size = new System.Drawing.Size(185, 17);
            this.m_chkbxUseAutoCompletion.TabIndex = 5;
            this.m_chkbxUseAutoCompletion.Text = "Use experimental auto-completion";
            this.m_chkbxUseAutoCompletion.UseVisualStyleBackColor = true;
            // 
            // RBFOptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 153);
            this.Controls.Add(this.m_chkbxUseAutoCompletion);
            this.Controls.Add(this.m_chkbxUseRetributionSaving);
            this.Controls.Add(this.m_chkbxUseRetributionLoading);
            this.Controls.Add(this.m_chkbxReloadOnSave);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RBFOptionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "RBF Editor Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.CheckBox m_chkbxReloadOnSave;
        private System.Windows.Forms.CheckBox m_chkbxUseRetributionLoading;
        private System.Windows.Forms.CheckBox m_chkbxUseRetributionSaving;
        private System.Windows.Forms.CheckBox m_chkbxUseAutoCompletion;
    }
}