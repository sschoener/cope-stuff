namespace RBFPlugin
{
    partial class RBFSearchForm
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
            this.m_grpbxSearchOptions = new System.Windows.Forms.GroupBox();
            this.m_chkbxSearchForValue = new System.Windows.Forms.CheckBox();
            this.m_chkbxFullText = new System.Windows.Forms.CheckBox();
            this.m_tbxSearchKey = new System.Windows.Forms.TextBox();
            this.m_tbxSearchValue = new System.Windows.Forms.TextBox();
            this.m_chkbxSearchForKey = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_progBarSearch = new System.Windows.Forms.ProgressBar();
            this.m_tbxInitialNode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnSearch = new System.Windows.Forms.Button();
            this.m_lbxSearchResults = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_rtbResultDisplay = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_grpbxSearchOptions.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.splitContainer1.Size = new System.Drawing.Size(889, 563);
            this.splitContainer1.SplitterDistance = 596;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 596F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_lbxSearchResults, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(596, 563);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_grpbxSearchOptions);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.m_progBarSearch);
            this.panel1.Controls.Add(this.m_tbxInitialNode);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.m_btnCancel);
            this.panel1.Controls.Add(this.m_btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 101);
            this.panel1.TabIndex = 0;
            // 
            // m_grpbxSearchOptions
            // 
            this.m_grpbxSearchOptions.Controls.Add(this.m_chkbxSearchForValue);
            this.m_grpbxSearchOptions.Controls.Add(this.m_chkbxFullText);
            this.m_grpbxSearchOptions.Controls.Add(this.m_tbxSearchKey);
            this.m_grpbxSearchOptions.Controls.Add(this.m_tbxSearchValue);
            this.m_grpbxSearchOptions.Controls.Add(this.m_chkbxSearchForKey);
            this.m_grpbxSearchOptions.Location = new System.Drawing.Point(346, 7);
            this.m_grpbxSearchOptions.Name = "m_grpbxSearchOptions";
            this.m_grpbxSearchOptions.Size = new System.Drawing.Size(245, 88);
            this.m_grpbxSearchOptions.TabIndex = 14;
            this.m_grpbxSearchOptions.TabStop = false;
            this.m_grpbxSearchOptions.Text = "Search for";
            // 
            // m_chkbxSearchForValue
            // 
            this.m_chkbxSearchForValue.AutoSize = true;
            this.m_chkbxSearchForValue.Location = new System.Drawing.Point(6, 19);
            this.m_chkbxSearchForValue.Name = "m_chkbxSearchForValue";
            this.m_chkbxSearchForValue.Size = new System.Drawing.Size(53, 17);
            this.m_chkbxSearchForValue.TabIndex = 12;
            this.m_chkbxSearchForValue.Text = "Value";
            this.m_chkbxSearchForValue.UseVisualStyleBackColor = true;
            // 
            // m_chkbxFullText
            // 
            this.m_chkbxFullText.AutoSize = true;
            this.m_chkbxFullText.Location = new System.Drawing.Point(6, 65);
            this.m_chkbxFullText.Name = "m_chkbxFullText";
            this.m_chkbxFullText.Size = new System.Drawing.Size(97, 17);
            this.m_chkbxFullText.TabIndex = 10;
            this.m_chkbxFullText.Text = "Full-text search";
            this.m_chkbxFullText.UseVisualStyleBackColor = true;
            // 
            // m_tbxSearchKey
            // 
            this.m_tbxSearchKey.Location = new System.Drawing.Point(65, 40);
            this.m_tbxSearchKey.Name = "m_tbxSearchKey";
            this.m_tbxSearchKey.Size = new System.Drawing.Size(174, 20);
            this.m_tbxSearchKey.TabIndex = 13;
            // 
            // m_tbxSearchValue
            // 
            this.m_tbxSearchValue.Location = new System.Drawing.Point(65, 17);
            this.m_tbxSearchValue.Name = "m_tbxSearchValue";
            this.m_tbxSearchValue.Size = new System.Drawing.Size(174, 20);
            this.m_tbxSearchValue.TabIndex = 13;
            // 
            // m_chkbxSearchForKey
            // 
            this.m_chkbxSearchForKey.AutoSize = true;
            this.m_chkbxSearchForKey.Location = new System.Drawing.Point(6, 42);
            this.m_chkbxSearchForKey.Name = "m_chkbxSearchForKey";
            this.m_chkbxSearchForKey.Size = new System.Drawing.Size(44, 17);
            this.m_chkbxSearchForKey.TabIndex = 11;
            this.m_chkbxSearchForKey.Text = "Key";
            this.m_chkbxSearchForKey.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Progress:";
            // 
            // m_progBarSearch
            // 
            this.m_progBarSearch.Location = new System.Drawing.Point(94, 42);
            this.m_progBarSearch.Name = "m_progBarSearch";
            this.m_progBarSearch.Size = new System.Drawing.Size(248, 23);
            this.m_progBarSearch.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_progBarSearch.TabIndex = 8;
            // 
            // m_tbxInitialNode
            // 
            this.m_tbxInitialNode.Location = new System.Drawing.Point(92, 16);
            this.m_tbxInitialNode.Name = "m_tbxInitialNode";
            this.m_tbxInitialNode.Size = new System.Drawing.Size(250, 20);
            this.m_tbxInitialNode.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "Relative Path\r\nof Start-Node:";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(186, 73);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 5;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // m_btnSearch
            // 
            this.m_btnSearch.Location = new System.Drawing.Point(267, 73);
            this.m_btnSearch.Name = "m_btnSearch";
            this.m_btnSearch.Size = new System.Drawing.Size(75, 23);
            this.m_btnSearch.TabIndex = 3;
            this.m_btnSearch.Text = "Search";
            this.m_btnSearch.UseVisualStyleBackColor = true;
            this.m_btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // m_lbxSearchResults
            // 
            this.m_lbxSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbxSearchResults.FormattingEnabled = true;
            this.m_lbxSearchResults.Location = new System.Drawing.Point(3, 126);
            this.m_lbxSearchResults.Name = "m_lbxSearchResults";
            this.m_lbxSearchResults.Size = new System.Drawing.Size(590, 434);
            this.m_lbxSearchResults.Sorted = true;
            this.m_lbxSearchResults.TabIndex = 1;
            this.m_lbxSearchResults.SelectedIndexChanged += new System.EventHandler(this.LbxSearchResultsSelectedIndexChanged);
            this.m_lbxSearchResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LbxSearchResultsMouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 101);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(596, 22);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Results:";
            // 
            // m_rtbResultDisplay
            // 
            this.m_rtbResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbResultDisplay.Location = new System.Drawing.Point(0, 0);
            this.m_rtbResultDisplay.Name = "m_rtbResultDisplay";
            this.m_rtbResultDisplay.Size = new System.Drawing.Size(289, 563);
            this.m_rtbResultDisplay.TabIndex = 0;
            this.m_rtbResultDisplay.Text = "";
            // 
            // RBFSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 563);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(651, 354);
            this.Name = "RBFSearchForm";
            this.ShowIcon = false;
            this.Text = "RBF Search";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.m_grpbxSearchOptions.ResumeLayout(false);
            this.m_grpbxSearchOptions.PerformLayout();
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
        private System.Windows.Forms.TextBox m_tbxInitialNode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Button m_btnSearch;
        private System.Windows.Forms.ListBox m_lbxSearchResults;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox m_rtbResultDisplay;
        private System.Windows.Forms.CheckBox m_chkbxFullText;
        private System.Windows.Forms.GroupBox m_grpbxSearchOptions;
        private System.Windows.Forms.CheckBox m_chkbxSearchForValue;
        private System.Windows.Forms.TextBox m_tbxSearchKey;
        private System.Windows.Forms.TextBox m_tbxSearchValue;
        private System.Windows.Forms.CheckBox m_chkbxSearchForKey;
    }
}