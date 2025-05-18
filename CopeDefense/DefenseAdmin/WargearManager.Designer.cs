namespace DefenseAdmin
{
    partial class WargearManager
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
            this.m_lbxWargear = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_btnRemoveWargear = new System.Windows.Forms.Button();
            this.m_btnAddWargear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.m_rtbWargearDescription = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_cbxRequiredUnlock = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_cbxWargearType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_tbxWargearId = new System.Windows.Forms.TextBox();
            this.m_tbxWargearBlueprint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tbxWargearName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.19149F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.80851F));
            this.tableLayoutPanel1.Controls.Add(this.m_lbxWargear, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(731, 613);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_lbxWargear
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.m_lbxWargear, 2);
            this.m_lbxWargear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbxWargear.FormattingEnabled = true;
            this.m_lbxWargear.Location = new System.Drawing.Point(3, 3);
            this.m_lbxWargear.Name = "m_lbxWargear";
            this.m_lbxWargear.Size = new System.Drawing.Size(316, 577);
            this.m_lbxWargear.Sorted = true;
            this.m_lbxWargear.TabIndex = 0;
            this.m_lbxWargear.SelectedIndexChanged += new System.EventHandler(this.LbxWargearSelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnCopy);
            this.panel1.Controls.Add(this.m_btnRemoveWargear);
            this.panel1.Controls.Add(this.m_btnAddWargear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 583);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 30);
            this.panel1.TabIndex = 1;
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Location = new System.Drawing.Point(134, 3);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(55, 23);
            this.m_btnCopy.TabIndex = 2;
            this.m_btnCopy.Text = "Copy";
            this.m_btnCopy.UseVisualStyleBackColor = true;
            this.m_btnCopy.Click += new System.EventHandler(this.BtnCopyClick);
            // 
            // m_btnRemoveWargear
            // 
            this.m_btnRemoveWargear.Location = new System.Drawing.Point(72, 3);
            this.m_btnRemoveWargear.Name = "m_btnRemoveWargear";
            this.m_btnRemoveWargear.Size = new System.Drawing.Size(56, 23);
            this.m_btnRemoveWargear.TabIndex = 1;
            this.m_btnRemoveWargear.Text = "Remove";
            this.m_btnRemoveWargear.UseVisualStyleBackColor = true;
            this.m_btnRemoveWargear.Click += new System.EventHandler(this.BtnRemoveWargearClick);
            // 
            // m_btnAddWargear
            // 
            this.m_btnAddWargear.Location = new System.Drawing.Point(12, 3);
            this.m_btnAddWargear.Name = "m_btnAddWargear";
            this.m_btnAddWargear.Size = new System.Drawing.Size(54, 23);
            this.m_btnAddWargear.TabIndex = 0;
            this.m_btnAddWargear.Text = "Add";
            this.m_btnAddWargear.UseVisualStyleBackColor = true;
            this.m_btnAddWargear.Click += new System.EventHandler(this.BtnAddWargearClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(325, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 577);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current upgrade";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 396F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.m_rtbWargearDescription, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(397, 558);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // m_rtbWargearDescription
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.m_rtbWargearDescription, 2);
            this.m_rtbWargearDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbWargearDescription.Location = new System.Drawing.Point(3, 111);
            this.m_rtbWargearDescription.Name = "m_rtbWargearDescription";
            this.m_rtbWargearDescription.Size = new System.Drawing.Size(391, 444);
            this.m_rtbWargearDescription.TabIndex = 0;
            this.m_rtbWargearDescription.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_cbxRequiredUnlock);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.m_cbxWargearType);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.m_tbxWargearId);
            this.panel2.Controls.Add(this.m_tbxWargearBlueprint);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.m_tbxWargearName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(396, 108);
            this.panel2.TabIndex = 1;
            // 
            // m_cbxRequiredUnlock
            // 
            this.m_cbxRequiredUnlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxRequiredUnlock.FormattingEnabled = true;
            this.m_cbxRequiredUnlock.Location = new System.Drawing.Point(97, 82);
            this.m_cbxRequiredUnlock.Name = "m_cbxRequiredUnlock";
            this.m_cbxRequiredUnlock.Size = new System.Drawing.Size(296, 21);
            this.m_cbxRequiredUnlock.Sorted = true;
            this.m_cbxRequiredUnlock.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Required unlock:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Type:";
            // 
            // m_cbxWargearType
            // 
            this.m_cbxWargearType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxWargearType.FormattingEnabled = true;
            this.m_cbxWargearType.Location = new System.Drawing.Point(97, 56);
            this.m_cbxWargearType.Name = "m_cbxWargearType";
            this.m_cbxWargearType.Size = new System.Drawing.Size(142, 21);
            this.m_cbxWargearType.Sorted = true;
            this.m_cbxWargearType.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(242, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Id:";
            // 
            // m_tbxWargearId
            // 
            this.m_tbxWargearId.Location = new System.Drawing.Point(283, 56);
            this.m_tbxWargearId.Name = "m_tbxWargearId";
            this.m_tbxWargearId.ReadOnly = true;
            this.m_tbxWargearId.Size = new System.Drawing.Size(110, 20);
            this.m_tbxWargearId.TabIndex = 4;
            // 
            // m_tbxWargearBlueprint
            // 
            this.m_tbxWargearBlueprint.Location = new System.Drawing.Point(97, 29);
            this.m_tbxWargearBlueprint.MaxLength = 127;
            this.m_tbxWargearBlueprint.Name = "m_tbxWargearBlueprint";
            this.m_tbxWargearBlueprint.Size = new System.Drawing.Size(296, 20);
            this.m_tbxWargearBlueprint.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Blueprint:";
            // 
            // m_tbxWargearName
            // 
            this.m_tbxWargearName.Location = new System.Drawing.Point(97, 3);
            this.m_tbxWargearName.MaxLength = 64;
            this.m_tbxWargearName.Name = "m_tbxWargearName";
            this.m_tbxWargearName.Size = new System.Drawing.Size(296, 20);
            this.m_tbxWargearName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // WargearManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 613);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "WargearManager";
            this.ShowIcon = false;
            this.Text = "Upgrade Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WargearManagerFormClosing);
            this.Shown += new System.EventHandler(this.WargearManagerShown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox m_lbxWargear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RichTextBox m_rtbWargearDescription;
        private System.Windows.Forms.Button m_btnAddWargear;
        private System.Windows.Forms.Button m_btnRemoveWargear;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbxWargearName;
        private System.Windows.Forms.TextBox m_tbxWargearId;
        private System.Windows.Forms.TextBox m_tbxWargearBlueprint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox m_cbxWargearType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox m_cbxRequiredUnlock;
        private System.Windows.Forms.Button m_btnCopy;
    }
}