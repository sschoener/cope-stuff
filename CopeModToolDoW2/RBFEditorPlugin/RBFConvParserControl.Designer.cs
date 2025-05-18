namespace RBFPlugin
{
    partial class RBFConvParserControl
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
            this.m_rtbInput = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnDone = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cbxSeperator = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 282F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.m_rtbInput, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(546, 548);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_rtbInput
            // 
            this.m_rtbInput.AcceptsTab = true;
            this.tableLayoutPanel1.SetColumnSpan(this.m_rtbInput, 3);
            this.m_rtbInput.DetectUrls = false;
            this.m_rtbInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbInput.Location = new System.Drawing.Point(3, 3);
            this.m_rtbInput.Name = "m_rtbInput";
            this.m_rtbInput.Size = new System.Drawing.Size(540, 510);
            this.m_rtbInput.TabIndex = 0;
            this.m_rtbInput.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnDone);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(465, 516);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(81, 32);
            this.panel1.TabIndex = 1;
            // 
            // m_btnDone
            // 
            this.m_btnDone.Location = new System.Drawing.Point(3, 5);
            this.m_btnDone.Name = "m_btnDone";
            this.m_btnDone.Size = new System.Drawing.Size(75, 23);
            this.m_btnDone.TabIndex = 0;
            this.m_btnDone.Text = "Done";
            this.m_btnDone.UseVisualStyleBackColor = true;
            this.m_btnDone.Click += new System.EventHandler(this.BtnDoneClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.m_cbxSeperator);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(183, 516);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(282, 32);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select seperator";
            // 
            // m_cbxSeperator
            // 
            this.m_cbxSeperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxSeperator.FormattingEnabled = true;
            this.m_cbxSeperator.Items.AddRange(new object[] {
            "Vertical Line (|)",
            "Tabstop (\\t)"});
            this.m_cbxSeperator.Location = new System.Drawing.Point(119, 7);
            this.m_cbxSeperator.Name = "m_cbxSeperator";
            this.m_cbxSeperator.Size = new System.Drawing.Size(160, 21);
            this.m_cbxSeperator.TabIndex = 0;
            // 
            // RBFConvParserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RBFConvParserControl";
            this.Size = new System.Drawing.Size(546, 548);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox m_rtbInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnDone;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_cbxSeperator;
    }
}