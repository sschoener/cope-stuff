namespace RBFPlugin
{
    partial class DictionaryCrawler
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnCopyIntoDictionary = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.m_progBarSearch = new System.Windows.Forms.ProgressBar();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnSearch = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_chklbxEntries = new System.Windows.Forms.CheckedListBox();
            this.m_rtbResultDisplay = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_rtbResultDisplay);
            this.splitContainer1.Size = new System.Drawing.Size(635, 316);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 286F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_chklbxEntries, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(286, 316);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnCopyIntoDictionary);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.m_progBarSearch);
            this.panel1.Controls.Add(this.m_btnCancel);
            this.panel1.Controls.Add(this.m_btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 115);
            this.panel1.TabIndex = 0;
            // 
            // m_btnCopyIntoDictionary
            // 
            this.m_btnCopyIntoDictionary.Location = new System.Drawing.Point(3, 89);
            this.m_btnCopyIntoDictionary.Name = "m_btnCopyIntoDictionary";
            this.m_btnCopyIntoDictionary.Size = new System.Drawing.Size(280, 23);
            this.m_btnCopyIntoDictionary.TabIndex = 10;
            this.m_btnCopyIntoDictionary.Text = "Copy checked entries into dictionary";
            this.m_btnCopyIntoDictionary.UseVisualStyleBackColor = true;
            this.m_btnCopyIntoDictionary.Click += new System.EventHandler(this.BtnCopyIntoDictionaryClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Progress:";
            // 
            // m_progBarSearch
            // 
            this.m_progBarSearch.Location = new System.Drawing.Point(3, 60);
            this.m_progBarSearch.Name = "m_progBarSearch";
            this.m_progBarSearch.Size = new System.Drawing.Size(280, 23);
            this.m_progBarSearch.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_progBarSearch.TabIndex = 8;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(208, 12);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 5;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // m_btnSearch
            // 
            this.m_btnSearch.Location = new System.Drawing.Point(3, 12);
            this.m_btnSearch.Name = "m_btnSearch";
            this.m_btnSearch.Size = new System.Drawing.Size(75, 23);
            this.m_btnSearch.TabIndex = 3;
            this.m_btnSearch.Text = "Search";
            this.m_btnSearch.UseVisualStyleBackColor = true;
            this.m_btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 115);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 26);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Keys:";
            // 
            // m_chklbxEntries
            // 
            this.m_chklbxEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_chklbxEntries.FormattingEnabled = true;
            this.m_chklbxEntries.Location = new System.Drawing.Point(3, 144);
            this.m_chklbxEntries.Name = "m_chklbxEntries";
            this.m_chklbxEntries.Size = new System.Drawing.Size(280, 169);
            this.m_chklbxEntries.TabIndex = 4;
            this.m_chklbxEntries.SelectedIndexChanged += new System.EventHandler(this.EntriesSelectedIndexChanged);
            // 
            // m_rtbResultDisplay
            // 
            this.m_rtbResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbResultDisplay.Location = new System.Drawing.Point(0, 0);
            this.m_rtbResultDisplay.Name = "m_rtbResultDisplay";
            this.m_rtbResultDisplay.Size = new System.Drawing.Size(345, 316);
            this.m_rtbResultDisplay.TabIndex = 0;
            this.m_rtbResultDisplay.Text = "";
            // 
            // DictionaryCrawler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 316);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(651, 354);
            this.Name = "DictionaryCrawler";
            this.ShowIcon = false;
            this.Text = "Dictionary Builder";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar m_progBarSearch;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Button m_btnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox m_rtbResultDisplay;
        private System.Windows.Forms.Button m_btnCopyIntoDictionary;
        private System.Windows.Forms.CheckedListBox m_chklbxEntries;
    }
}