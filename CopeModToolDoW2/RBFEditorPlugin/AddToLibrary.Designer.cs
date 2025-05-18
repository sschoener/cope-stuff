namespace RBFPlugin
{
    partial class AddToLibrary
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
            this.m_labName = new System.Windows.Forms.Label();
            this.m_tbxName = new System.Windows.Forms.TextBox();
            this.m_rtbTags = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tbxMenu = new System.Windows.Forms.TextBox();
            this.m_chkbxAddTags = new System.Windows.Forms.CheckBox();
            this.m_chklbxTagGroups = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_btnOK
            // 
            this.m_btnOK.Location = new System.Drawing.Point(364, 256);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 0;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(12, 256);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // m_labName
            // 
            this.m_labName.AutoSize = true;
            this.m_labName.Location = new System.Drawing.Point(9, 13);
            this.m_labName.Name = "m_labName";
            this.m_labName.Size = new System.Drawing.Size(38, 13);
            this.m_labName.TabIndex = 2;
            this.m_labName.Text = "Name:";
            // 
            // m_tbxName
            // 
            this.m_tbxName.Location = new System.Drawing.Point(53, 10);
            this.m_tbxName.Name = "m_tbxName";
            this.m_tbxName.Size = new System.Drawing.Size(219, 20);
            this.m_tbxName.TabIndex = 3;
            // 
            // m_rtbTags
            // 
            this.m_rtbTags.Location = new System.Drawing.Point(12, 51);
            this.m_rtbTags.Name = "m_rtbTags";
            this.m_rtbTags.Size = new System.Drawing.Size(260, 149);
            this.m_rtbTags.TabIndex = 4;
            this.m_rtbTags.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tags (one per line):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sub menu:";
            // 
            // m_tbxMenu
            // 
            this.m_tbxMenu.Location = new System.Drawing.Point(74, 206);
            this.m_tbxMenu.Name = "m_tbxMenu";
            this.m_tbxMenu.Size = new System.Drawing.Size(198, 20);
            this.m_tbxMenu.TabIndex = 7;
            // 
            // m_chkbxAddTags
            // 
            this.m_chkbxAddTags.AutoSize = true;
            this.m_chkbxAddTags.Location = new System.Drawing.Point(13, 232);
            this.m_chkbxAddTags.Name = "m_chkbxAddTags";
            this.m_chkbxAddTags.Size = new System.Drawing.Size(217, 17);
            this.m_chkbxAddTags.TabIndex = 8;
            this.m_chkbxAddTags.Text = "Only add tags to entry (if it already exists)";
            this.m_chkbxAddTags.UseVisualStyleBackColor = true;
            this.m_chkbxAddTags.CheckedChanged += new System.EventHandler(this.ChkbxAddTagsCheckedChanged);
            // 
            // m_chklbxTagGroups
            // 
            this.m_chklbxTagGroups.FormattingEnabled = true;
            this.m_chklbxTagGroups.Location = new System.Drawing.Point(278, 51);
            this.m_chklbxTagGroups.Name = "m_chklbxTagGroups";
            this.m_chklbxTagGroups.Size = new System.Drawing.Size(161, 199);
            this.m_chklbxTagGroups.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tag groups:";
            // 
            // AddToLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(451, 287);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_chklbxTagGroups);
            this.Controls.Add(this.m_chkbxAddTags);
            this.Controls.Add(this.m_tbxMenu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_rtbTags);
            this.Controls.Add(this.m_tbxName);
            this.Controls.Add(this.m_labName);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddToLibrary";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Add To Library";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label m_labName;
        private System.Windows.Forms.TextBox m_tbxName;
        private System.Windows.Forms.RichTextBox m_rtbTags;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_tbxMenu;
        private System.Windows.Forms.CheckBox m_chkbxAddTags;
        private System.Windows.Forms.CheckedListBox m_chklbxTagGroups;
        private System.Windows.Forms.Label label3;
    }
}