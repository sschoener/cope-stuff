namespace DefenseAdmin
{
    partial class UnlockManager
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_lbxUnlocks = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_btnRemove = new System.Windows.Forms.Button();
            this.m_btnAddUnlock = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_nupUnlockGroup = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.m_cbxUnlockRequirement = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_nupUnlockPrice = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.m_tbxUnlockId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tbxUnlockName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_rtbUnlockDescription = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUnlockGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUnlockPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 208F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.m_lbxUnlocks, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(731, 503);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_lbxUnlocks
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.m_lbxUnlocks, 2);
            this.m_lbxUnlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbxUnlocks.FormattingEnabled = true;
            this.m_lbxUnlocks.Location = new System.Drawing.Point(3, 3);
            this.m_lbxUnlocks.Name = "m_lbxUnlocks";
            this.m_lbxUnlocks.Size = new System.Drawing.Size(358, 467);
            this.m_lbxUnlocks.Sorted = true;
            this.m_lbxUnlocks.TabIndex = 0;
            this.m_lbxUnlocks.SelectedIndexChanged += new System.EventHandler(this.LbxUnlocksSelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnCopy);
            this.panel1.Controls.Add(this.m_btnRemove);
            this.panel1.Controls.Add(this.m_btnAddUnlock);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 473);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 30);
            this.panel1.TabIndex = 1;
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Location = new System.Drawing.Point(142, 4);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(61, 23);
            this.m_btnCopy.TabIndex = 2;
            this.m_btnCopy.Text = "Copy";
            this.m_btnCopy.UseVisualStyleBackColor = true;
            this.m_btnCopy.Click += new System.EventHandler(this.BtnCopyClick);
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.Location = new System.Drawing.Point(75, 4);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.Size = new System.Drawing.Size(61, 23);
            this.m_btnRemove.TabIndex = 1;
            this.m_btnRemove.Text = "Remove";
            this.m_btnRemove.UseVisualStyleBackColor = true;
            this.m_btnRemove.Click += new System.EventHandler(this.BtnRemoveClick);
            // 
            // m_btnAddUnlock
            // 
            this.m_btnAddUnlock.Location = new System.Drawing.Point(12, 4);
            this.m_btnAddUnlock.Name = "m_btnAddUnlock";
            this.m_btnAddUnlock.Size = new System.Drawing.Size(57, 23);
            this.m_btnAddUnlock.TabIndex = 0;
            this.m_btnAddUnlock.Text = "Add";
            this.m_btnAddUnlock.UseVisualStyleBackColor = true;
            this.m_btnAddUnlock.Click += new System.EventHandler(this.BtnAddUnlockClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(367, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 467);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current unlock";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 356F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.m_rtbUnlockDescription, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(355, 448);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_nupUnlockGroup);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.m_cbxUnlockRequirement);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.m_nupUnlockPrice);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.m_tbxUnlockId);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.m_tbxUnlockName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(356, 116);
            this.panel2.TabIndex = 0;
            // 
            // m_nupUnlockGroup
            // 
            this.m_nupUnlockGroup.Location = new System.Drawing.Point(97, 87);
            this.m_nupUnlockGroup.Name = "m_nupUnlockGroup";
            this.m_nupUnlockGroup.Size = new System.Drawing.Size(120, 20);
            this.m_nupUnlockGroup.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Unlock group:";
            // 
            // m_cbxUnlockRequirement
            // 
            this.m_cbxUnlockRequirement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxUnlockRequirement.FormattingEnabled = true;
            this.m_cbxUnlockRequirement.Location = new System.Drawing.Point(97, 59);
            this.m_cbxUnlockRequirement.Name = "m_cbxUnlockRequirement";
            this.m_cbxUnlockRequirement.Size = new System.Drawing.Size(251, 21);
            this.m_cbxUnlockRequirement.Sorted = true;
            this.m_cbxUnlockRequirement.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Required unlock:";
            // 
            // m_nupUnlockPrice
            // 
            this.m_nupUnlockPrice.Location = new System.Drawing.Point(97, 30);
            this.m_nupUnlockPrice.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.m_nupUnlockPrice.Name = "m_nupUnlockPrice";
            this.m_nupUnlockPrice.Size = new System.Drawing.Size(120, 20);
            this.m_nupUnlockPrice.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Price:";
            // 
            // m_tbxUnlockId
            // 
            this.m_tbxUnlockId.Location = new System.Drawing.Point(248, 29);
            this.m_tbxUnlockId.Name = "m_tbxUnlockId";
            this.m_tbxUnlockId.ReadOnly = true;
            this.m_tbxUnlockId.Size = new System.Drawing.Size(100, 20);
            this.m_tbxUnlockId.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Id:";
            // 
            // m_tbxUnlockName
            // 
            this.m_tbxUnlockName.Location = new System.Drawing.Point(97, 2);
            this.m_tbxUnlockName.Name = "m_tbxUnlockName";
            this.m_tbxUnlockName.Size = new System.Drawing.Size(251, 20);
            this.m_tbxUnlockName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // m_rtbUnlockDescription
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.m_rtbUnlockDescription, 2);
            this.m_rtbUnlockDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbUnlockDescription.Location = new System.Drawing.Point(3, 119);
            this.m_rtbUnlockDescription.Name = "m_rtbUnlockDescription";
            this.m_rtbUnlockDescription.Size = new System.Drawing.Size(349, 326);
            this.m_rtbUnlockDescription.TabIndex = 1;
            this.m_rtbUnlockDescription.Text = "";
            // 
            // UnlockManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 503);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UnlockManager";
            this.ShowIcon = false;
            this.Text = "Unlock Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnlockManagerFormClosing);
            this.Shown += new System.EventHandler(this.UnlockManagerShown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUnlockGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUnlockPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox m_lbxUnlocks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAddUnlock;
        private System.Windows.Forms.Button m_btnRemove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox m_tbxUnlockId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_tbxUnlockName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown m_nupUnlockPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox m_cbxUnlockRequirement;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown m_nupUnlockGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox m_rtbUnlockDescription;
        private System.Windows.Forms.Button m_btnCopy;
    }
}