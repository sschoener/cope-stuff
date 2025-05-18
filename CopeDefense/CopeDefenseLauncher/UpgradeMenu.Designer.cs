namespace CopeDefenseLauncher
{
    partial class UpgradeMenu
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_labSkillEnergyCost = new System.Windows.Forms.Label();
            this.m_picbxSkillEnergyCost = new System.Windows.Forms.PictureBox();
            this.m_cbxSkillLevel = new System.Windows.Forms.ComboBox();
            this.m_rtbSkillDescription = new System.Windows.Forms.RichTextBox();
            this.m_labSkillName = new System.Windows.Forms.Label();
            this.m_chklbxSkills = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.m_labUnitPopCost = new System.Windows.Forms.Label();
            this.m_labUnitReqCost = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_labUnitName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_picbxRequisiton = new System.Windows.Forms.PictureBox();
            this.m_rtbUnitDescription = new System.Windows.Forms.RichTextBox();
            this.m_labUnitSlots = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnDeactivateUnit = new System.Windows.Forms.Button();
            this.m_btnDeactivateAllUnits = new System.Windows.Forms.Button();
            this.m_btnActivateUnit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_lbxAvailableUnits = new System.Windows.Forms.ListBox();
            this.m_lbxActiveUnits = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_rtbMiscDescription = new System.Windows.Forms.RichTextBox();
            this.m_labMiscSlots = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_btnDeactivateMisc = new System.Windows.Forms.Button();
            this.m_btnDeactivateAllMisc = new System.Windows.Forms.Button();
            this.m_btnActivateMisc = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_lbxAvailableMisc = new System.Windows.Forms.ListBox();
            this.m_lbxActiveMisc = new System.Windows.Forms.ListBox();
            this.m_picbxUnit = new System.Windows.Forms.PictureBox();
            this.m_picbxMisc = new System.Windows.Forms.PictureBox();
            this.m_picbxSkill = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxSkillEnergyCost)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxRequisiton)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxMisc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxSkill)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_picbxSkill);
            this.groupBox1.Controls.Add(this.m_labSkillEnergyCost);
            this.groupBox1.Controls.Add(this.m_picbxSkillEnergyCost);
            this.groupBox1.Controls.Add(this.m_cbxSkillLevel);
            this.groupBox1.Controls.Add(this.m_rtbSkillDescription);
            this.groupBox1.Controls.Add(this.m_labSkillName);
            this.groupBox1.Controls.Add(this.m_chklbxSkills);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 212);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skills";
            // 
            // m_labSkillEnergyCost
            // 
            this.m_labSkillEnergyCost.AutoSize = true;
            this.m_labSkillEnergyCost.Location = new System.Drawing.Point(223, 71);
            this.m_labSkillEnergyCost.Name = "m_labSkillEnergyCost";
            this.m_labSkillEnergyCost.Size = new System.Drawing.Size(62, 13);
            this.m_labSkillEnergyCost.TabIndex = 5;
            this.m_labSkillEnergyCost.Text = "energy cost";
            // 
            // m_picBoxSkillCost
            // 
            this.m_picbxSkillEnergyCost.Image = global::CopeDefenseLauncher.Properties.Resources.GenericIconEnergy;
            this.m_picbxSkillEnergyCost.Location = new System.Drawing.Point(201, 70);
            this.m_picbxSkillEnergyCost.Name = "m_picbxSkillEnergyCost";
            this.m_picbxSkillEnergyCost.Size = new System.Drawing.Size(16, 16);
            this.m_picbxSkillEnergyCost.TabIndex = 4;
            this.m_picbxSkillEnergyCost.TabStop = false;
            // 
            // m_cbxSkillLevel
            // 
            this.m_cbxSkillLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxSkillLevel.FormattingEnabled = true;
            this.m_cbxSkillLevel.Items.AddRange(new object[] {
            "Skill Level 1",
            "Skill Level 5",
            "Skill Level 10",
            "Skill Level 15",
            "Skill Level 20"});
            this.m_cbxSkillLevel.Location = new System.Drawing.Point(199, 20);
            this.m_cbxSkillLevel.Name = "m_cbxSkillLevel";
            this.m_cbxSkillLevel.Size = new System.Drawing.Size(235, 21);
            this.m_cbxSkillLevel.TabIndex = 3;
            this.m_cbxSkillLevel.SelectedIndexChanged += new System.EventHandler(this.CbxSkillLevelSelectedIndexChanged);
            // 
            // m_rtbSkillDescription
            // 
            this.m_rtbSkillDescription.Location = new System.Drawing.Point(10, 122);
            this.m_rtbSkillDescription.Name = "m_rtbSkillDescription";
            this.m_rtbSkillDescription.ReadOnly = true;
            this.m_rtbSkillDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.m_rtbSkillDescription.Size = new System.Drawing.Size(424, 84);
            this.m_rtbSkillDescription.TabIndex = 2;
            this.m_rtbSkillDescription.Text = "";
            // 
            // m_labSkillName
            // 
            this.m_labSkillName.AutoSize = true;
            this.m_labSkillName.Location = new System.Drawing.Point(200, 52);
            this.m_labSkillName.Name = "m_labSkillName";
            this.m_labSkillName.Size = new System.Drawing.Size(53, 13);
            this.m_labSkillName.TabIndex = 1;
            this.m_labSkillName.Text = "skill name";
            // 
            // m_chklbxSkills
            // 
            this.m_chklbxSkills.FormattingEnabled = true;
            this.m_chklbxSkills.Location = new System.Drawing.Point(10, 19);
            this.m_chklbxSkills.Name = "m_chklbxSkills";
            this.m_chklbxSkills.Size = new System.Drawing.Size(182, 94);
            this.m_chklbxSkills.Sorted = true;
            this.m_chklbxSkills.TabIndex = 0;
            this.m_chklbxSkills.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChklbxSkillsItemCheck);
            this.m_chklbxSkills.SelectedIndexChanged += new System.EventHandler(this.ChklbxSkillsSelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.m_labUnitSlots);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.m_btnDeactivateUnit);
            this.groupBox2.Controls.Add(this.m_btnDeactivateAllUnits);
            this.groupBox2.Controls.Add(this.m_btnActivateUnit);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.m_lbxAvailableUnits);
            this.groupBox2.Controls.Add(this.m_lbxActiveUnits);
            this.groupBox2.Location = new System.Drawing.Point(468, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 517);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Units";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.m_picbxUnit);
            this.groupBox4.Controls.Add(this.m_labUnitPopCost);
            this.groupBox4.Controls.Add(this.m_labUnitReqCost);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.m_labUnitName);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.m_picbxRequisiton);
            this.groupBox4.Controls.Add(this.m_rtbUnitDescription);
            this.groupBox4.Location = new System.Drawing.Point(6, 194);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(425, 317);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selected unit";
            // 
            // m_labUnitPopCost
            // 
            this.m_labUnitPopCost.AutoSize = true;
            this.m_labUnitPopCost.Location = new System.Drawing.Point(71, 67);
            this.m_labUnitPopCost.Name = "m_labUnitPopCost";
            this.m_labUnitPopCost.Size = new System.Drawing.Size(48, 13);
            this.m_labUnitPopCost.TabIndex = 20;
            this.m_labUnitPopCost.Text = "pop cost";
            // 
            // m_labUnitReqCost
            // 
            this.m_labUnitReqCost.AutoSize = true;
            this.m_labUnitReqCost.Location = new System.Drawing.Point(71, 41);
            this.m_labUnitReqCost.Name = "m_labUnitReqCost";
            this.m_labUnitReqCost.Size = new System.Drawing.Size(45, 13);
            this.m_labUnitReqCost.TabIndex = 19;
            this.m_labUnitReqCost.Text = "req cost";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CopeDefenseLauncher.Properties.Resources.GenericIconPopcap;
            this.pictureBox1.Location = new System.Drawing.Point(10, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // m_labUnitName
            // 
            this.m_labUnitName.AutoSize = true;
            this.m_labUnitName.Location = new System.Drawing.Point(71, 20);
            this.m_labUnitName.Name = "m_labUnitName";
            this.m_labUnitName.Size = new System.Drawing.Size(50, 13);
            this.m_labUnitName.TabIndex = 17;
            this.m_labUnitName.Text = "unitname";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Name:";
            // 
            // m_picbxRequisiton
            // 
            this.m_picbxRequisiton.Image = global::CopeDefenseLauncher.Properties.Resources.GenericIconRequisition;
            this.m_picbxRequisiton.Location = new System.Drawing.Point(10, 41);
            this.m_picbxRequisiton.Name = "m_picbxRequisiton";
            this.m_picbxRequisiton.Size = new System.Drawing.Size(16, 16);
            this.m_picbxRequisiton.TabIndex = 15;
            this.m_picbxRequisiton.TabStop = false;
            // 
            // m_rtbUnitDescription
            // 
            this.m_rtbUnitDescription.Location = new System.Drawing.Point(10, 89);
            this.m_rtbUnitDescription.Name = "m_rtbUnitDescription";
            this.m_rtbUnitDescription.ReadOnly = true;
            this.m_rtbUnitDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.m_rtbUnitDescription.Size = new System.Drawing.Size(409, 222);
            this.m_rtbUnitDescription.TabIndex = 14;
            this.m_rtbUnitDescription.Text = "";
            // 
            // m_labUnitSlots
            // 
            this.m_labUnitSlots.AutoSize = true;
            this.m_labUnitSlots.Location = new System.Drawing.Point(71, 175);
            this.m_labUnitSlots.Name = "m_labUnitSlots";
            this.m_labUnitSlots.Size = new System.Drawing.Size(48, 13);
            this.m_labUnitSlots.TabIndex = 9;
            this.m_labUnitSlots.Text = "numslots";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Slots left:";
            // 
            // m_btnDeactivateUnit
            // 
            this.m_btnDeactivateUnit.Location = new System.Drawing.Point(199, 147);
            this.m_btnDeactivateUnit.Name = "m_btnDeactivateUnit";
            this.m_btnDeactivateUnit.Size = new System.Drawing.Size(27, 23);
            this.m_btnDeactivateUnit.TabIndex = 7;
            this.m_btnDeactivateUnit.Text = ">";
            this.m_btnDeactivateUnit.UseVisualStyleBackColor = true;
            this.m_btnDeactivateUnit.Click += new System.EventHandler(this.BtnDeactivateUnitClick);
            // 
            // m_btnDeactivateAllUnits
            // 
            this.m_btnDeactivateAllUnits.Location = new System.Drawing.Point(200, 118);
            this.m_btnDeactivateAllUnits.Name = "m_btnDeactivateAllUnits";
            this.m_btnDeactivateAllUnits.Size = new System.Drawing.Size(27, 23);
            this.m_btnDeactivateAllUnits.TabIndex = 6;
            this.m_btnDeactivateAllUnits.Text = ">>";
            this.m_btnDeactivateAllUnits.UseVisualStyleBackColor = true;
            this.m_btnDeactivateAllUnits.Click += new System.EventHandler(this.BtnDeactivateAllUnitsClick);
            // 
            // m_btnActivateUnit
            // 
            this.m_btnActivateUnit.Location = new System.Drawing.Point(200, 36);
            this.m_btnActivateUnit.Name = "m_btnActivateUnit";
            this.m_btnActivateUnit.Size = new System.Drawing.Size(27, 23);
            this.m_btnActivateUnit.TabIndex = 4;
            this.m_btnActivateUnit.Text = "<";
            this.m_btnActivateUnit.UseVisualStyleBackColor = true;
            this.m_btnActivateUnit.Click += new System.EventHandler(this.BtnActivateUnitClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available units";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Active units";
            // 
            // m_lbxAvailableUnits
            // 
            this.m_lbxAvailableUnits.FormattingEnabled = true;
            this.m_lbxAvailableUnits.Location = new System.Drawing.Point(233, 36);
            this.m_lbxAvailableUnits.Name = "m_lbxAvailableUnits";
            this.m_lbxAvailableUnits.Size = new System.Drawing.Size(198, 134);
            this.m_lbxAvailableUnits.Sorted = true;
            this.m_lbxAvailableUnits.TabIndex = 1;
            this.m_lbxAvailableUnits.SelectedIndexChanged += new System.EventHandler(this.LbxAvailableUnitsSelectedIndexChanged);
            // 
            // m_lbxActiveUnits
            // 
            this.m_lbxActiveUnits.FormattingEnabled = true;
            this.m_lbxActiveUnits.Location = new System.Drawing.Point(10, 36);
            this.m_lbxActiveUnits.Name = "m_lbxActiveUnits";
            this.m_lbxActiveUnits.Size = new System.Drawing.Size(183, 134);
            this.m_lbxActiveUnits.Sorted = true;
            this.m_lbxActiveUnits.TabIndex = 0;
            this.m_lbxActiveUnits.SelectedIndexChanged += new System.EventHandler(this.LbxActiveUnitsSelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_picbxMisc);
            this.groupBox3.Controls.Add(this.m_rtbMiscDescription);
            this.groupBox3.Controls.Add(this.m_labMiscSlots);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.m_btnDeactivateMisc);
            this.groupBox3.Controls.Add(this.m_btnDeactivateAllMisc);
            this.groupBox3.Controls.Add(this.m_btnActivateMisc);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.m_lbxAvailableMisc);
            this.groupBox3.Controls.Add(this.m_lbxActiveMisc);
            this.groupBox3.Location = new System.Drawing.Point(15, 230);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(437, 299);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Misc. upgrades";
            // 
            // m_rtbMiscDescription
            // 
            this.m_rtbMiscDescription.Location = new System.Drawing.Point(10, 191);
            this.m_rtbMiscDescription.Name = "m_rtbMiscDescription";
            this.m_rtbMiscDescription.ReadOnly = true;
            this.m_rtbMiscDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.m_rtbMiscDescription.Size = new System.Drawing.Size(351, 102);
            this.m_rtbMiscDescription.TabIndex = 10;
            this.m_rtbMiscDescription.Text = "";
            // 
            // m_labMiscSlots
            // 
            this.m_labMiscSlots.AutoSize = true;
            this.m_labMiscSlots.Location = new System.Drawing.Point(63, 175);
            this.m_labMiscSlots.Name = "m_labMiscSlots";
            this.m_labMiscSlots.Size = new System.Drawing.Size(48, 13);
            this.m_labMiscSlots.TabIndex = 9;
            this.m_labMiscSlots.Text = "numslots";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Slots left:";
            // 
            // m_btnDeactivateMisc
            // 
            this.m_btnDeactivateMisc.Location = new System.Drawing.Point(199, 147);
            this.m_btnDeactivateMisc.Name = "m_btnDeactivateMisc";
            this.m_btnDeactivateMisc.Size = new System.Drawing.Size(27, 23);
            this.m_btnDeactivateMisc.TabIndex = 7;
            this.m_btnDeactivateMisc.Text = ">";
            this.m_btnDeactivateMisc.UseVisualStyleBackColor = true;
            this.m_btnDeactivateMisc.Click += new System.EventHandler(this.BtnDeactivateMiscClick);
            // 
            // m_btnDeactivateAllMisc
            // 
            this.m_btnDeactivateAllMisc.Location = new System.Drawing.Point(200, 118);
            this.m_btnDeactivateAllMisc.Name = "m_btnDeactivateAllMisc";
            this.m_btnDeactivateAllMisc.Size = new System.Drawing.Size(27, 23);
            this.m_btnDeactivateAllMisc.TabIndex = 6;
            this.m_btnDeactivateAllMisc.Text = ">>";
            this.m_btnDeactivateAllMisc.UseVisualStyleBackColor = true;
            this.m_btnDeactivateAllMisc.Click += new System.EventHandler(this.BtnDeactivateAllMiscClick);
            // 
            // m_btnActivateMisc
            // 
            this.m_btnActivateMisc.Location = new System.Drawing.Point(200, 36);
            this.m_btnActivateMisc.Name = "m_btnActivateMisc";
            this.m_btnActivateMisc.Size = new System.Drawing.Size(27, 23);
            this.m_btnActivateMisc.TabIndex = 4;
            this.m_btnActivateMisc.Text = "<";
            this.m_btnActivateMisc.UseVisualStyleBackColor = true;
            this.m_btnActivateMisc.Click += new System.EventHandler(this.BtnActivateMiscClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Available";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Active";
            // 
            // m_lbxAvailableMisc
            // 
            this.m_lbxAvailableMisc.FormattingEnabled = true;
            this.m_lbxAvailableMisc.Location = new System.Drawing.Point(233, 36);
            this.m_lbxAvailableMisc.Name = "m_lbxAvailableMisc";
            this.m_lbxAvailableMisc.Size = new System.Drawing.Size(198, 134);
            this.m_lbxAvailableMisc.Sorted = true;
            this.m_lbxAvailableMisc.TabIndex = 1;
            this.m_lbxAvailableMisc.SelectedIndexChanged += new System.EventHandler(this.LbxAvailableMiscSelectedIndexChanged);
            // 
            // m_lbxActiveMisc
            // 
            this.m_lbxActiveMisc.FormattingEnabled = true;
            this.m_lbxActiveMisc.Location = new System.Drawing.Point(10, 36);
            this.m_lbxActiveMisc.Name = "m_lbxActiveMisc";
            this.m_lbxActiveMisc.Size = new System.Drawing.Size(183, 134);
            this.m_lbxActiveMisc.Sorted = true;
            this.m_lbxActiveMisc.TabIndex = 0;
            this.m_lbxActiveMisc.SelectedIndexChanged += new System.EventHandler(this.LbxActiveMiscSelectedIndexChanged);
            // 
            // m_picbxUnit
            // 
            this.m_picbxUnit.Location = new System.Drawing.Point(355, 19);
            this.m_picbxUnit.Name = "m_picbxUnit";
            this.m_picbxUnit.Size = new System.Drawing.Size(64, 64);
            this.m_picbxUnit.TabIndex = 21;
            this.m_picbxUnit.TabStop = false;
            // 
            // m_picbxMisc
            // 
            this.m_picbxMisc.Location = new System.Drawing.Point(367, 191);
            this.m_picbxMisc.Name = "m_picbxMisc";
            this.m_picbxMisc.Size = new System.Drawing.Size(64, 64);
            this.m_picbxMisc.TabIndex = 22;
            this.m_picbxMisc.TabStop = false;
            // 
            // m_picbxSkill
            // 
            this.m_picbxSkill.Location = new System.Drawing.Point(370, 52);
            this.m_picbxSkill.Name = "m_picbxSkill";
            this.m_picbxSkill.Size = new System.Drawing.Size(64, 64);
            this.m_picbxSkill.TabIndex = 22;
            this.m_picbxSkill.TabStop = false;
            // 
            // UpgradeMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 541);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "UpgradeMenu";
            this.ShowInTaskbar = false;
            this.Text = "Upgrade Menu";
            this.Shown += new System.EventHandler(this.UpgradeMenuShown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxSkillEnergyCost)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxRequisiton)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxMisc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxSkill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox m_lbxAvailableUnits;
        private System.Windows.Forms.ListBox m_lbxActiveUnits;
        private System.Windows.Forms.ComboBox m_cbxSkillLevel;
        private System.Windows.Forms.RichTextBox m_rtbSkillDescription;
        private System.Windows.Forms.Label m_labSkillName;
        private System.Windows.Forms.CheckedListBox m_chklbxSkills;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnActivateUnit;
        private System.Windows.Forms.Button m_btnDeactivateUnit;
        private System.Windows.Forms.Button m_btnDeactivateAllUnits;
        private System.Windows.Forms.Label m_labUnitSlots;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox m_rtbMiscDescription;
        private System.Windows.Forms.Label m_labMiscSlots;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button m_btnDeactivateMisc;
        private System.Windows.Forms.Button m_btnDeactivateAllMisc;
        private System.Windows.Forms.Button m_btnActivateMisc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox m_lbxAvailableMisc;
        private System.Windows.Forms.ListBox m_lbxActiveMisc;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label m_labUnitPopCost;
        private System.Windows.Forms.Label m_labUnitReqCost;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label m_labUnitName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox m_picbxRequisiton;
        private System.Windows.Forms.RichTextBox m_rtbUnitDescription;
        private System.Windows.Forms.PictureBox m_picbxSkillEnergyCost;
        private System.Windows.Forms.Label m_labSkillEnergyCost;
        private System.Windows.Forms.PictureBox m_picbxUnit;
        private System.Windows.Forms.PictureBox m_picbxSkill;
        private System.Windows.Forms.PictureBox m_picbxMisc;
    }
}