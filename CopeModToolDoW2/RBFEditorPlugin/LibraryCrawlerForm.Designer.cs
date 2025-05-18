namespace RBFPlugin
{
    partial class LibraryCrawlerForm
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
            this.components = new System.ComponentModel.Container();
            this.m_btnStartStop = new System.Windows.Forms.Button();
            this.m_prgProgress = new System.Windows.Forms.ProgressBar();
            this.m_tbxActionTagGroup = new System.Windows.Forms.TextBox();
            this.m_tbxModifierTagGroup = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_chklbxFilter = new System.Windows.Forms.CheckedListBox();
            this.m_tipFilters = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.m_tbxRequirementTagGroup = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_tbxBuffTagGroup = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_tbxTargetTagGroup = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_tbxExpActionTagGroup = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_btnStartStop
            // 
            this.m_btnStartStop.Location = new System.Drawing.Point(315, 178);
            this.m_btnStartStop.Name = "m_btnStartStop";
            this.m_btnStartStop.Size = new System.Drawing.Size(75, 23);
            this.m_btnStartStop.TabIndex = 0;
            this.m_btnStartStop.Text = "Start";
            this.m_btnStartStop.UseVisualStyleBackColor = true;
            this.m_btnStartStop.Click += new System.EventHandler(this.BtnStartStopClick);
            // 
            // m_prgProgress
            // 
            this.m_prgProgress.Location = new System.Drawing.Point(12, 207);
            this.m_prgProgress.Name = "m_prgProgress";
            this.m_prgProgress.Size = new System.Drawing.Size(378, 23);
            this.m_prgProgress.TabIndex = 1;
            // 
            // m_tbxActionTagGroup
            // 
            this.m_tbxActionTagGroup.Location = new System.Drawing.Point(213, 23);
            this.m_tbxActionTagGroup.Name = "m_tbxActionTagGroup";
            this.m_tbxActionTagGroup.Size = new System.Drawing.Size(178, 20);
            this.m_tbxActionTagGroup.TabIndex = 2;
            // 
            // m_tbxModifierTagGroup
            // 
            this.m_tbxModifierTagGroup.Location = new System.Drawing.Point(213, 49);
            this.m_tbxModifierTagGroup.Name = "m_tbxModifierTagGroup";
            this.m_tbxModifierTagGroup.Size = new System.Drawing.Size(178, 20);
            this.m_tbxModifierTagGroup.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "actions";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "modifiers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tag group for...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Filter groups:";
            // 
            // m_chklbxFilter
            // 
            this.m_chklbxFilter.FormattingEnabled = true;
            this.m_chklbxFilter.Location = new System.Drawing.Point(12, 26);
            this.m_chklbxFilter.Name = "m_chklbxFilter";
            this.m_chklbxFilter.Size = new System.Drawing.Size(120, 169);
            this.m_chklbxFilter.TabIndex = 8;
            // 
            // m_tipFilters
            // 
            this.m_tipFilters.ToolTipTitle = "The search will remove all children from tables with keys belonging to one of the" +
                " tag-groups below.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "requirements";
            // 
            // m_tbxRequirementTagGroup
            // 
            this.m_tbxRequirementTagGroup.Location = new System.Drawing.Point(212, 75);
            this.m_tbxRequirementTagGroup.Name = "m_tbxRequirementTagGroup";
            this.m_tbxRequirementTagGroup.Size = new System.Drawing.Size(178, 20);
            this.m_tbxRequirementTagGroup.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(144, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "buffs";
            // 
            // m_tbxBuffTagGroup
            // 
            this.m_tbxBuffTagGroup.Location = new System.Drawing.Point(213, 101);
            this.m_tbxBuffTagGroup.Name = "m_tbxBuffTagGroup";
            this.m_tbxBuffTagGroup.Size = new System.Drawing.Size(178, 20);
            this.m_tbxBuffTagGroup.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(143, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "targets";
            // 
            // m_tbxTargetTagGroup
            // 
            this.m_tbxTargetTagGroup.Location = new System.Drawing.Point(212, 127);
            this.m_tbxTargetTagGroup.Name = "m_tbxTargetTagGroup";
            this.m_tbxTargetTagGroup.Size = new System.Drawing.Size(178, 20);
            this.m_tbxTargetTagGroup.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(144, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 26);
            this.label8.TabIndex = 16;
            this.label8.Text = "expendable\r\nactions";
            // 
            // m_tbxExpActionTagGroup
            // 
            this.m_tbxExpActionTagGroup.Location = new System.Drawing.Point(212, 153);
            this.m_tbxExpActionTagGroup.Name = "m_tbxExpActionTagGroup";
            this.m_tbxExpActionTagGroup.Size = new System.Drawing.Size(178, 20);
            this.m_tbxExpActionTagGroup.TabIndex = 15;
            // 
            // LibraryCrawlerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 242);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.m_tbxExpActionTagGroup);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_tbxTargetTagGroup);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_tbxBuffTagGroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_tbxRequirementTagGroup);
            this.Controls.Add(this.m_chklbxFilter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tbxModifierTagGroup);
            this.Controls.Add(this.m_tbxActionTagGroup);
            this.Controls.Add(this.m_prgProgress);
            this.Controls.Add(this.m_btnStartStop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LibraryCrawlerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Library Crawler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnStartStop;
        private System.Windows.Forms.ProgressBar m_prgProgress;
        private System.Windows.Forms.TextBox m_tbxActionTagGroup;
        private System.Windows.Forms.TextBox m_tbxModifierTagGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox m_chklbxFilter;
        private System.Windows.Forms.ToolTip m_tipFilters;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_tbxRequirementTagGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_tbxBuffTagGroup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_tbxTargetTagGroup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox m_tbxExpActionTagGroup;
    }
}