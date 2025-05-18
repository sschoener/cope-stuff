namespace CopeDefenseLauncher
{
    partial class PlayerSetupForm
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
            this.m_chklbxHeroes = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_labMoney = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.m_btnOpenUnlockMenu = new System.Windows.Forms.Button();
            this.m_btnOpenWargearMenu = new System.Windows.Forms.Button();
            this.m_labLosses = new System.Windows.Forms.Label();
            this.m_labKills = new System.Windows.Forms.Label();
            this.m_labMaxWave = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.m_labClass = new System.Windows.Forms.Label();
            this.m_btnOpenUpgradeMenu = new System.Windows.Forms.Button();
            this.m_btnDeleteHero = new System.Windows.Forms.Button();
            this.m_btnSaveHero = new System.Windows.Forms.Button();
            this.m_labNextExperience = new System.Windows.Forms.Label();
            this.m_labExperience = new System.Windows.Forms.Label();
            this.m_labLevel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_prgExperience = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_labAttribPointsLeft = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_nupAttributeEnergy = new System.Windows.Forms.NumericUpDown();
            this.m_nupAttributeRanged = new System.Windows.Forms.NumericUpDown();
            this.m_nupAttributeHealth = new System.Windows.Forms.NumericUpDown();
            this.m_nupAttributeMelee = new System.Windows.Forms.NumericUpDown();
            this.m_cbxCurrentHero = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnCreateNewHero = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupAttributeEnergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupAttributeRanged)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupAttributeHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupAttributeMelee)).BeginInit();
            this.SuspendLayout();
            // 
            // m_chklbxHeroes
            // 
            this.m_chklbxHeroes.FormattingEnabled = true;
            this.m_chklbxHeroes.Location = new System.Drawing.Point(69, 9);
            this.m_chklbxHeroes.Name = "m_chklbxHeroes";
            this.m_chklbxHeroes.Size = new System.Drawing.Size(165, 94);
            this.m_chklbxHeroes.TabIndex = 1;
            this.m_chklbxHeroes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChklbxHeroesItemCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Heroes:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.m_labMoney);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.m_btnOpenUnlockMenu);
            this.groupBox1.Controls.Add(this.m_btnOpenWargearMenu);
            this.groupBox1.Controls.Add(this.m_labLosses);
            this.groupBox1.Controls.Add(this.m_labKills);
            this.groupBox1.Controls.Add(this.m_labMaxWave);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.m_labClass);
            this.groupBox1.Controls.Add(this.m_btnOpenUpgradeMenu);
            this.groupBox1.Controls.Add(this.m_btnDeleteHero);
            this.groupBox1.Controls.Add(this.m_btnSaveHero);
            this.groupBox1.Controls.Add(this.m_labNextExperience);
            this.groupBox1.Controls.Add(this.m_labExperience);
            this.groupBox1.Controls.Add(this.m_labLevel);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.m_prgExperience);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.m_cbxCurrentHero);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(15, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 291);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Hero";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CopeDefenseLauncher.Properties.Resources.GenericIconMoney;
            this.pictureBox1.Location = new System.Drawing.Point(356, 114);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            // 
            // m_labMoney
            // 
            this.m_labMoney.Location = new System.Drawing.Point(313, 114);
            this.m_labMoney.Name = "m_labMoney";
            this.m_labMoney.Size = new System.Drawing.Size(38, 13);
            this.m_labMoney.TabIndex = 32;
            this.m_labMoney.Text = "money";
            this.m_labMoney.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(225, 114);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Money:";
            // 
            // m_btnOpenUnlockMenu
            // 
            this.m_btnOpenUnlockMenu.Location = new System.Drawing.Point(236, 203);
            this.m_btnOpenUnlockMenu.Name = "m_btnOpenUnlockMenu";
            this.m_btnOpenUnlockMenu.Size = new System.Drawing.Size(146, 23);
            this.m_btnOpenUnlockMenu.TabIndex = 30;
            this.m_btnOpenUnlockMenu.Text = "Open unlock menu";
            this.m_btnOpenUnlockMenu.UseVisualStyleBackColor = true;
            this.m_btnOpenUnlockMenu.Click += new System.EventHandler(this.BtnOpenUnlockMenuClick);
            // 
            // m_btnOpenWargearMenu
            // 
            this.m_btnOpenWargearMenu.Location = new System.Drawing.Point(236, 174);
            this.m_btnOpenWargearMenu.Name = "m_btnOpenWargearMenu";
            this.m_btnOpenWargearMenu.Size = new System.Drawing.Size(146, 23);
            this.m_btnOpenWargearMenu.TabIndex = 29;
            this.m_btnOpenWargearMenu.Text = "Open wargear menu";
            this.m_btnOpenWargearMenu.UseVisualStyleBackColor = true;
            this.m_btnOpenWargearMenu.Click += new System.EventHandler(this.BtnOpenWargearMenuClick);
            // 
            // m_labLosses
            // 
            this.m_labLosses.AutoSize = true;
            this.m_labLosses.Location = new System.Drawing.Point(313, 84);
            this.m_labLosses.Name = "m_labLosses";
            this.m_labLosses.Size = new System.Drawing.Size(36, 13);
            this.m_labLosses.TabIndex = 28;
            this.m_labLosses.Text = "losses";
            // 
            // m_labKills
            // 
            this.m_labKills.AutoSize = true;
            this.m_labKills.Location = new System.Drawing.Point(313, 62);
            this.m_labKills.Name = "m_labKills";
            this.m_labKills.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_labKills.Size = new System.Drawing.Size(24, 13);
            this.m_labKills.TabIndex = 27;
            this.m_labKills.Text = "kills";
            this.m_labKills.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_labMaxWave
            // 
            this.m_labMaxWave.AutoSize = true;
            this.m_labMaxWave.Location = new System.Drawing.Point(313, 41);
            this.m_labMaxWave.Name = "m_labMaxWave";
            this.m_labMaxWave.Size = new System.Drawing.Size(52, 13);
            this.m_labMaxWave.TabIndex = 26;
            this.m_labMaxWave.Text = "maxwave";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(225, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Squads lost:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(225, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Kills:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(225, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Max wave:";
            // 
            // m_labClass
            // 
            this.m_labClass.AutoSize = true;
            this.m_labClass.Location = new System.Drawing.Point(225, 16);
            this.m_labClass.Name = "m_labClass";
            this.m_labClass.Size = new System.Drawing.Size(31, 13);
            this.m_labClass.TabIndex = 22;
            this.m_labClass.Text = "class";
            // 
            // m_btnOpenUpgradeMenu
            // 
            this.m_btnOpenUpgradeMenu.Location = new System.Drawing.Point(236, 145);
            this.m_btnOpenUpgradeMenu.Name = "m_btnOpenUpgradeMenu";
            this.m_btnOpenUpgradeMenu.Size = new System.Drawing.Size(146, 23);
            this.m_btnOpenUpgradeMenu.TabIndex = 21;
            this.m_btnOpenUpgradeMenu.Text = "Open upgrade menu";
            this.m_btnOpenUpgradeMenu.UseVisualStyleBackColor = true;
            this.m_btnOpenUpgradeMenu.Click += new System.EventHandler(this.BtnOpenUpgradeMenuClick);
            // 
            // m_btnDeleteHero
            // 
            this.m_btnDeleteHero.Location = new System.Drawing.Point(236, 262);
            this.m_btnDeleteHero.Name = "m_btnDeleteHero";
            this.m_btnDeleteHero.Size = new System.Drawing.Size(146, 23);
            this.m_btnDeleteHero.TabIndex = 20;
            this.m_btnDeleteHero.Text = "Delete Hero";
            this.m_btnDeleteHero.UseVisualStyleBackColor = true;
            this.m_btnDeleteHero.Click += new System.EventHandler(this.BtnDeleteHeroClick);
            // 
            // m_btnSaveHero
            // 
            this.m_btnSaveHero.Location = new System.Drawing.Point(236, 233);
            this.m_btnSaveHero.Name = "m_btnSaveHero";
            this.m_btnSaveHero.Size = new System.Drawing.Size(146, 23);
            this.m_btnSaveHero.TabIndex = 6;
            this.m_btnSaveHero.Text = "Save attribute changes";
            this.m_btnSaveHero.UseVisualStyleBackColor = true;
            this.m_btnSaveHero.Click += new System.EventHandler(this.BtnSaveHeroClick);
            // 
            // m_labNextExperience
            // 
            this.m_labNextExperience.AutoSize = true;
            this.m_labNextExperience.Location = new System.Drawing.Point(129, 84);
            this.m_labNextExperience.Name = "m_labNextExperience";
            this.m_labNextExperience.Size = new System.Drawing.Size(45, 13);
            this.m_labNextExperience.TabIndex = 19;
            this.m_labNextExperience.Text = "nextExp";
            // 
            // m_labExperience
            // 
            this.m_labExperience.AutoSize = true;
            this.m_labExperience.Location = new System.Drawing.Point(129, 62);
            this.m_labExperience.Name = "m_labExperience";
            this.m_labExperience.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_labExperience.Size = new System.Drawing.Size(59, 13);
            this.m_labExperience.TabIndex = 18;
            this.m_labExperience.Text = "experience";
            this.m_labExperience.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_labLevel
            // 
            this.m_labLevel.AutoSize = true;
            this.m_labLevel.Location = new System.Drawing.Point(129, 41);
            this.m_labLevel.Name = "m_labLevel";
            this.m_labLevel.Size = new System.Drawing.Size(29, 13);
            this.m_labLevel.TabIndex = 17;
            this.m_labLevel.Text = "level";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Next Level at:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Experience:";
            // 
            // m_prgExperience
            // 
            this.m_prgExperience.Location = new System.Drawing.Point(9, 109);
            this.m_prgExperience.Name = "m_prgExperience";
            this.m_prgExperience.Size = new System.Drawing.Size(210, 23);
            this.m_prgExperience.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Level:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.m_labAttribPointsLeft);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.m_nupAttributeEnergy);
            this.groupBox2.Controls.Add(this.m_nupAttributeRanged);
            this.groupBox2.Controls.Add(this.m_nupAttributeHealth);
            this.groupBox2.Controls.Add(this.m_nupAttributeMelee);
            this.groupBox2.Location = new System.Drawing.Point(9, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 147);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Attributes";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Points left:";
            // 
            // m_labAttribPointsLeft
            // 
            this.m_labAttribPointsLeft.AutoSize = true;
            this.m_labAttribPointsLeft.Location = new System.Drawing.Point(147, 124);
            this.m_labAttribPointsLeft.Name = "m_labAttribPointsLeft";
            this.m_labAttribPointsLeft.Size = new System.Drawing.Size(13, 13);
            this.m_labAttribPointsLeft.TabIndex = 16;
            this.m_labAttribPointsLeft.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Ranged";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Melee";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Energy";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Health";
            // 
            // m_nupAttributeEnergy
            // 
            this.m_nupAttributeEnergy.Location = new System.Drawing.Point(148, 44);
            this.m_nupAttributeEnergy.Name = "m_nupAttributeEnergy";
            this.m_nupAttributeEnergy.Size = new System.Drawing.Size(56, 20);
            this.m_nupAttributeEnergy.TabIndex = 9;
            this.m_nupAttributeEnergy.ValueChanged += new System.EventHandler(this.AttributeValueChanged);
            // 
            // m_nupAttributeRanged
            // 
            this.m_nupAttributeRanged.Location = new System.Drawing.Point(148, 98);
            this.m_nupAttributeRanged.Name = "m_nupAttributeRanged";
            this.m_nupAttributeRanged.Size = new System.Drawing.Size(56, 20);
            this.m_nupAttributeRanged.TabIndex = 11;
            this.m_nupAttributeRanged.ValueChanged += new System.EventHandler(this.AttributeValueChanged);
            // 
            // m_nupAttributeHealth
            // 
            this.m_nupAttributeHealth.Location = new System.Drawing.Point(148, 17);
            this.m_nupAttributeHealth.Name = "m_nupAttributeHealth";
            this.m_nupAttributeHealth.Size = new System.Drawing.Size(56, 20);
            this.m_nupAttributeHealth.TabIndex = 8;
            this.m_nupAttributeHealth.ValueChanged += new System.EventHandler(this.AttributeValueChanged);
            // 
            // m_nupAttributeMelee
            // 
            this.m_nupAttributeMelee.Location = new System.Drawing.Point(148, 71);
            this.m_nupAttributeMelee.Name = "m_nupAttributeMelee";
            this.m_nupAttributeMelee.Size = new System.Drawing.Size(56, 20);
            this.m_nupAttributeMelee.TabIndex = 10;
            this.m_nupAttributeMelee.ValueChanged += new System.EventHandler(this.AttributeValueChanged);
            // 
            // m_cbxCurrentHero
            // 
            this.m_cbxCurrentHero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxCurrentHero.FormattingEnabled = true;
            this.m_cbxCurrentHero.Location = new System.Drawing.Point(78, 13);
            this.m_cbxCurrentHero.Name = "m_cbxCurrentHero";
            this.m_cbxCurrentHero.Size = new System.Drawing.Size(141, 21);
            this.m_cbxCurrentHero.TabIndex = 6;
            this.m_cbxCurrentHero.SelectedIndexChanged += new System.EventHandler(this.CbxCurrentHeroSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Current Hero:";
            // 
            // m_btnCreateNewHero
            // 
            this.m_btnCreateNewHero.Location = new System.Drawing.Point(254, 9);
            this.m_btnCreateNewHero.Name = "m_btnCreateNewHero";
            this.m_btnCreateNewHero.Size = new System.Drawing.Size(143, 23);
            this.m_btnCreateNewHero.TabIndex = 5;
            this.m_btnCreateNewHero.Text = "Create new Hero";
            this.m_btnCreateNewHero.UseVisualStyleBackColor = true;
            this.m_btnCreateNewHero.Click += new System.EventHandler(this.BtnCreateNewHeroClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(248, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(157, 39);
            this.label14.TabIndex = 7;
            this.label14.Text = "Check your active hero.\r\nIf you don\'t have an active hero\r\nthere WILL BE ERRORS.";
            // 
            // PlayerSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 409);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.m_btnCreateNewHero);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_chklbxHeroes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayerSetupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Player Setup";
            this.Shown += new System.EventHandler(this.PlayerSetupFormShown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupAttributeEnergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupAttributeRanged)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupAttributeHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupAttributeMelee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox m_chklbxHeroes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label m_labAttribPointsLeft;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown m_nupAttributeEnergy;
        private System.Windows.Forms.NumericUpDown m_nupAttributeRanged;
        private System.Windows.Forms.NumericUpDown m_nupAttributeHealth;
        private System.Windows.Forms.NumericUpDown m_nupAttributeMelee;
        private System.Windows.Forms.ComboBox m_cbxCurrentHero;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_btnCreateNewHero;
        private System.Windows.Forms.Button m_btnSaveHero;
        private System.Windows.Forms.Label m_labLevel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ProgressBar m_prgExperience;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label m_labExperience;
        private System.Windows.Forms.Label m_labNextExperience;
        private System.Windows.Forms.Button m_btnDeleteHero;
        private System.Windows.Forms.Button m_btnOpenUpgradeMenu;
        private System.Windows.Forms.Label m_labClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label m_labLosses;
        private System.Windows.Forms.Label m_labKills;
        private System.Windows.Forms.Label m_labMaxWave;
        private System.Windows.Forms.Button m_btnOpenUnlockMenu;
        private System.Windows.Forms.Button m_btnOpenWargearMenu;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label m_labMoney;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}