namespace DefenseAdmin
{
    partial class HeroTypeManager
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
            this.m_lbxHeroTypes = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnRemoveHeroType = new System.Windows.Forms.Button();
            this.m_btnAddHeroType = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_nupStandardWargear1 = new System.Windows.Forms.NumericUpDown();
            this.m_nupStandardWargear2 = new System.Windows.Forms.NumericUpDown();
            this.m_nupStandardWargear3 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.m_cbxStandardUnlock = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_nupUnlockGroupId = new System.Windows.Forms.NumericUpDown();
            this.m_chkbxAddToCurrentGroup = new System.Windows.Forms.CheckBox();
            this.m_btnAddToUnlockGroup = new System.Windows.Forms.Button();
            this.m_tbxId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_tbxHeroBlueprint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tbxHeroTypeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_lbxUnlockGroupEntries = new System.Windows.Forms.ListBox();
            this.m_btnRemoveFromUnlockGroup = new System.Windows.Forms.Button();
            this.m_lbxUnlockGroups = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupStandardWargear1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupStandardWargear2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupStandardWargear3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUnlockGroupId)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 447F));
            this.tableLayoutPanel1.Controls.Add(this.m_lbxHeroTypes, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(727, 512);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_lbxHeroTypes
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.m_lbxHeroTypes, 2);
            this.m_lbxHeroTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbxHeroTypes.FormattingEnabled = true;
            this.m_lbxHeroTypes.Location = new System.Drawing.Point(3, 3);
            this.m_lbxHeroTypes.Name = "m_lbxHeroTypes";
            this.tableLayoutPanel1.SetRowSpan(this.m_lbxHeroTypes, 2);
            this.m_lbxHeroTypes.Size = new System.Drawing.Size(274, 477);
            this.m_lbxHeroTypes.Sorted = true;
            this.m_lbxHeroTypes.TabIndex = 0;
            this.m_lbxHeroTypes.SelectedIndexChanged += new System.EventHandler(this.LbxHeroTypesSelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnRemoveHeroType);
            this.panel1.Controls.Add(this.m_btnAddHeroType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 483);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 29);
            this.panel1.TabIndex = 1;
            // 
            // m_btnRemoveHeroType
            // 
            this.m_btnRemoveHeroType.Location = new System.Drawing.Point(96, 3);
            this.m_btnRemoveHeroType.Name = "m_btnRemoveHeroType";
            this.m_btnRemoveHeroType.Size = new System.Drawing.Size(86, 23);
            this.m_btnRemoveHeroType.TabIndex = 8;
            this.m_btnRemoveHeroType.Text = "Remove";
            this.m_btnRemoveHeroType.UseVisualStyleBackColor = true;
            this.m_btnRemoveHeroType.Click += new System.EventHandler(this.BtnRemoveHeroTypeClick);
            // 
            // m_btnAddHeroType
            // 
            this.m_btnAddHeroType.Location = new System.Drawing.Point(3, 3);
            this.m_btnAddHeroType.Name = "m_btnAddHeroType";
            this.m_btnAddHeroType.Size = new System.Drawing.Size(87, 23);
            this.m_btnAddHeroType.TabIndex = 7;
            this.m_btnAddHeroType.Text = "Add";
            this.m_btnAddHeroType.UseVisualStyleBackColor = true;
            this.m_btnAddHeroType.Click += new System.EventHandler(this.BtnAddHeroTypeClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_nupStandardWargear1);
            this.groupBox1.Controls.Add(this.m_nupStandardWargear2);
            this.groupBox1.Controls.Add(this.m_nupStandardWargear3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.m_cbxStandardUnlock);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_nupUnlockGroupId);
            this.groupBox1.Controls.Add(this.m_chkbxAddToCurrentGroup);
            this.groupBox1.Controls.Add(this.m_btnAddToUnlockGroup);
            this.groupBox1.Controls.Add(this.m_tbxId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_tbxHeroBlueprint);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.m_tbxHeroTypeName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(283, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 153);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current hero type";
            // 
            // m_nupStandardWargear1
            // 
            this.m_nupStandardWargear1.Location = new System.Drawing.Point(116, 96);
            this.m_nupStandardWargear1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_nupStandardWargear1.Name = "m_nupStandardWargear1";
            this.m_nupStandardWargear1.Size = new System.Drawing.Size(92, 20);
            this.m_nupStandardWargear1.TabIndex = 13;
            // 
            // m_nupStandardWargear2
            // 
            this.m_nupStandardWargear2.Location = new System.Drawing.Point(225, 96);
            this.m_nupStandardWargear2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_nupStandardWargear2.Name = "m_nupStandardWargear2";
            this.m_nupStandardWargear2.Size = new System.Drawing.Size(92, 20);
            this.m_nupStandardWargear2.TabIndex = 12;
            // 
            // m_nupStandardWargear3
            // 
            this.m_nupStandardWargear3.Location = new System.Drawing.Point(336, 96);
            this.m_nupStandardWargear3.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_nupStandardWargear3.Name = "m_nupStandardWargear3";
            this.m_nupStandardWargear3.Size = new System.Drawing.Size(92, 20);
            this.m_nupStandardWargear3.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Standard wargear:";
            // 
            // m_cbxStandardUnlock
            // 
            this.m_cbxStandardUnlock.FormattingEnabled = true;
            this.m_cbxStandardUnlock.Location = new System.Drawing.Point(116, 69);
            this.m_cbxStandardUnlock.Name = "m_cbxStandardUnlock";
            this.m_cbxStandardUnlock.Size = new System.Drawing.Size(211, 21);
            this.m_cbxStandardUnlock.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Standard unlock:";
            // 
            // m_nupUnlockGroupId
            // 
            this.m_nupUnlockGroupId.Enabled = false;
            this.m_nupUnlockGroupId.Location = new System.Drawing.Point(152, 123);
            this.m_nupUnlockGroupId.Name = "m_nupUnlockGroupId";
            this.m_nupUnlockGroupId.Size = new System.Drawing.Size(120, 20);
            this.m_nupUnlockGroupId.TabIndex = 4;
            // 
            // m_chkbxAddToCurrentGroup
            // 
            this.m_chkbxAddToCurrentGroup.AutoSize = true;
            this.m_chkbxAddToCurrentGroup.Checked = true;
            this.m_chkbxAddToCurrentGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkbxAddToCurrentGroup.Location = new System.Drawing.Point(6, 124);
            this.m_chkbxAddToCurrentGroup.Name = "m_chkbxAddToCurrentGroup";
            this.m_chkbxAddToCurrentGroup.Size = new System.Drawing.Size(144, 17);
            this.m_chkbxAddToCurrentGroup.TabIndex = 3;
            this.m_chkbxAddToCurrentGroup.Text = "use current unlock group";
            this.m_chkbxAddToCurrentGroup.UseVisualStyleBackColor = true;
            this.m_chkbxAddToCurrentGroup.CheckedChanged += new System.EventHandler(this.ChkbxAddToCurrentGroupCheckedChanged);
            // 
            // m_btnAddToUnlockGroup
            // 
            this.m_btnAddToUnlockGroup.Location = new System.Drawing.Point(281, 122);
            this.m_btnAddToUnlockGroup.Name = "m_btnAddToUnlockGroup";
            this.m_btnAddToUnlockGroup.Size = new System.Drawing.Size(150, 22);
            this.m_btnAddToUnlockGroup.TabIndex = 5;
            this.m_btnAddToUnlockGroup.Text = "Add to unlock group";
            this.m_btnAddToUnlockGroup.UseVisualStyleBackColor = true;
            this.m_btnAddToUnlockGroup.Click += new System.EventHandler(this.BtnAddToUnlockGroupClick);
            // 
            // m_tbxId
            // 
            this.m_tbxId.Location = new System.Drawing.Point(358, 69);
            this.m_tbxId.Name = "m_tbxId";
            this.m_tbxId.ReadOnly = true;
            this.m_tbxId.Size = new System.Drawing.Size(73, 20);
            this.m_tbxId.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Id:";
            // 
            // m_tbxHeroBlueprint
            // 
            this.m_tbxHeroBlueprint.Location = new System.Drawing.Point(72, 43);
            this.m_tbxHeroBlueprint.MaxLength = 127;
            this.m_tbxHeroBlueprint.Name = "m_tbxHeroBlueprint";
            this.m_tbxHeroBlueprint.Size = new System.Drawing.Size(359, 20);
            this.m_tbxHeroBlueprint.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Blueprint:";
            // 
            // m_tbxHeroTypeName
            // 
            this.m_tbxHeroTypeName.Location = new System.Drawing.Point(72, 17);
            this.m_tbxHeroTypeName.MaxLength = 64;
            this.m_tbxHeroTypeName.Name = "m_tbxHeroTypeName";
            this.m_tbxHeroTypeName.Size = new System.Drawing.Size(359, 20);
            this.m_tbxHeroTypeName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_lbxUnlockGroupEntries);
            this.groupBox2.Controls.Add(this.m_btnRemoveFromUnlockGroup);
            this.groupBox2.Controls.Add(this.m_lbxUnlockGroups);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(283, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 318);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unlock groups";
            // 
            // m_lbxUnlockGroupEntries
            // 
            this.m_lbxUnlockGroupEntries.FormattingEnabled = true;
            this.m_lbxUnlockGroupEntries.Location = new System.Drawing.Point(183, 20);
            this.m_lbxUnlockGroupEntries.Name = "m_lbxUnlockGroupEntries";
            this.m_lbxUnlockGroupEntries.Size = new System.Drawing.Size(245, 264);
            this.m_lbxUnlockGroupEntries.Sorted = true;
            this.m_lbxUnlockGroupEntries.TabIndex = 2;
            // 
            // m_btnRemoveFromUnlockGroup
            // 
            this.m_btnRemoveFromUnlockGroup.Location = new System.Drawing.Point(278, 290);
            this.m_btnRemoveFromUnlockGroup.Name = "m_btnRemoveFromUnlockGroup";
            this.m_btnRemoveFromUnlockGroup.Size = new System.Drawing.Size(150, 23);
            this.m_btnRemoveFromUnlockGroup.TabIndex = 6;
            this.m_btnRemoveFromUnlockGroup.Text = "Remove from unlock group";
            this.m_btnRemoveFromUnlockGroup.UseVisualStyleBackColor = true;
            this.m_btnRemoveFromUnlockGroup.Click += new System.EventHandler(this.BtnRemoveFromUnlockGroupClick);
            // 
            // m_lbxUnlockGroups
            // 
            this.m_lbxUnlockGroups.FormattingEnabled = true;
            this.m_lbxUnlockGroups.Location = new System.Drawing.Point(7, 20);
            this.m_lbxUnlockGroups.Name = "m_lbxUnlockGroups";
            this.m_lbxUnlockGroups.Size = new System.Drawing.Size(169, 264);
            this.m_lbxUnlockGroups.Sorted = true;
            this.m_lbxUnlockGroups.TabIndex = 0;
            this.m_lbxUnlockGroups.SelectedIndexChanged += new System.EventHandler(this.LbxUnlockGroupsSelectedIndexChanged);
            // 
            // HeroTypeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 512);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "HeroTypeManager";
            this.ShowIcon = false;
            this.Text = "HeroType Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HeroTypeManagerFormClosing);
            this.Shown += new System.EventHandler(this.HeroTypeManagerShown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupStandardWargear1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupStandardWargear2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupStandardWargear3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUnlockGroupId)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnRemoveHeroType;
        private System.Windows.Forms.Button m_btnAddHeroType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button m_btnAddToUnlockGroup;
        private System.Windows.Forms.TextBox m_tbxId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_tbxHeroBlueprint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_tbxHeroTypeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox m_lbxHeroTypes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox m_lbxUnlockGroups;
        private System.Windows.Forms.Button m_btnRemoveFromUnlockGroup;
        private System.Windows.Forms.ListBox m_lbxUnlockGroupEntries;
        private System.Windows.Forms.NumericUpDown m_nupUnlockGroupId;
        private System.Windows.Forms.CheckBox m_chkbxAddToCurrentGroup;
        private System.Windows.Forms.ComboBox m_cbxStandardUnlock;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown m_nupStandardWargear1;
        private System.Windows.Forms.NumericUpDown m_nupStandardWargear2;
        private System.Windows.Forms.NumericUpDown m_nupStandardWargear3;
    }
}