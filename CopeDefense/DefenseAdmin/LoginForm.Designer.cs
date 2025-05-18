namespace DefenseAdmin
{
    partial class LoginForm
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
            this.m_btnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_tbxAdminPassword = new System.Windows.Forms.TextBox();
            this.m_tbxAdminName = new System.Windows.Forms.TextBox();
            this.m_btnManageWargear = new System.Windows.Forms.Button();
            this.m_btnManageUpgrades = new System.Windows.Forms.Button();
            this.m_btnManageHeroes = new System.Windows.Forms.Button();
            this.m_btnManageUnlocks = new System.Windows.Forms.Button();
            this.m_btnOpenStats = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_btnConnect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.m_tbxAdminPassword);
            this.groupBox1.Controls.Add(this.m_tbxAdminName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 104);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // m_btnConnect
            // 
            this.m_btnConnect.Location = new System.Drawing.Point(186, 72);
            this.m_btnConnect.Name = "m_btnConnect";
            this.m_btnConnect.Size = new System.Drawing.Size(75, 23);
            this.m_btnConnect.TabIndex = 4;
            this.m_btnConnect.Text = "Connect";
            this.m_btnConnect.UseVisualStyleBackColor = true;
            this.m_btnConnect.Click += new System.EventHandler(this.BtnConnectClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // m_tbxAdminPassword
            // 
            this.m_tbxAdminPassword.Location = new System.Drawing.Point(65, 46);
            this.m_tbxAdminPassword.Name = "m_tbxAdminPassword";
            this.m_tbxAdminPassword.PasswordChar = '*';
            this.m_tbxAdminPassword.Size = new System.Drawing.Size(196, 20);
            this.m_tbxAdminPassword.TabIndex = 1;
            this.m_tbxAdminPassword.TextChanged += new System.EventHandler(this.TbxAdminPasswordTextChanged);
            // 
            // m_tbxAdminName
            // 
            this.m_tbxAdminName.Location = new System.Drawing.Point(65, 20);
            this.m_tbxAdminName.Name = "m_tbxAdminName";
            this.m_tbxAdminName.Size = new System.Drawing.Size(196, 20);
            this.m_tbxAdminName.TabIndex = 0;
            this.m_tbxAdminName.TextChanged += new System.EventHandler(this.TbxAdminNameTextChanged);
            // 
            // m_btnManageWargear
            // 
            this.m_btnManageWargear.Location = new System.Drawing.Point(287, 12);
            this.m_btnManageWargear.Name = "m_btnManageWargear";
            this.m_btnManageWargear.Size = new System.Drawing.Size(75, 23);
            this.m_btnManageWargear.TabIndex = 3;
            this.m_btnManageWargear.Text = "Wargear";
            this.m_btnManageWargear.UseVisualStyleBackColor = true;
            this.m_btnManageWargear.Click += new System.EventHandler(this.BtnManageWargearClick);
            // 
            // m_btnManageUpgrades
            // 
            this.m_btnManageUpgrades.Location = new System.Drawing.Point(368, 12);
            this.m_btnManageUpgrades.Name = "m_btnManageUpgrades";
            this.m_btnManageUpgrades.Size = new System.Drawing.Size(75, 23);
            this.m_btnManageUpgrades.TabIndex = 4;
            this.m_btnManageUpgrades.Text = "Upgrades";
            this.m_btnManageUpgrades.UseVisualStyleBackColor = true;
            this.m_btnManageUpgrades.Click += new System.EventHandler(this.BtnManageUpgradesClick);
            // 
            // m_btnManageHeroes
            // 
            this.m_btnManageHeroes.Location = new System.Drawing.Point(287, 41);
            this.m_btnManageHeroes.Name = "m_btnManageHeroes";
            this.m_btnManageHeroes.Size = new System.Drawing.Size(75, 23);
            this.m_btnManageHeroes.TabIndex = 5;
            this.m_btnManageHeroes.Text = "Heroes";
            this.m_btnManageHeroes.UseVisualStyleBackColor = true;
            this.m_btnManageHeroes.Click += new System.EventHandler(this.BtnManageHeroesClick);
            // 
            // m_btnManageUnlocks
            // 
            this.m_btnManageUnlocks.Location = new System.Drawing.Point(368, 41);
            this.m_btnManageUnlocks.Name = "m_btnManageUnlocks";
            this.m_btnManageUnlocks.Size = new System.Drawing.Size(75, 23);
            this.m_btnManageUnlocks.TabIndex = 6;
            this.m_btnManageUnlocks.Text = "Unlocks";
            this.m_btnManageUnlocks.UseVisualStyleBackColor = true;
            this.m_btnManageUnlocks.Click += new System.EventHandler(this.BtnManageUnlocksClick);
            // 
            // m_btnOpenStats
            // 
            this.m_btnOpenStats.Location = new System.Drawing.Point(287, 70);
            this.m_btnOpenStats.Name = "m_btnOpenStats";
            this.m_btnOpenStats.Size = new System.Drawing.Size(75, 23);
            this.m_btnOpenStats.TabIndex = 7;
            this.m_btnOpenStats.Text = "Stats";
            this.m_btnOpenStats.UseVisualStyleBackColor = true;
            this.m_btnOpenStats.Click += new System.EventHandler(this.BtnOpenStatsClick);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 124);
            this.Controls.Add(this.m_btnOpenStats);
            this.Controls.Add(this.m_btnManageUnlocks);
            this.Controls.Add(this.m_btnManageHeroes);
            this.Controls.Add(this.m_btnManageUpgrades);
            this.Controls.Add(this.m_btnManageWargear);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Text = "Cope\'s Defense Mod - Admin";
            this.Load += new System.EventHandler(this.FormLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button m_btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbxAdminPassword;
        private System.Windows.Forms.TextBox m_tbxAdminName;
        private System.Windows.Forms.Button m_btnManageWargear;
        private System.Windows.Forms.Button m_btnManageUpgrades;
        private System.Windows.Forms.Button m_btnManageHeroes;
        private System.Windows.Forms.Button m_btnManageUnlocks;
        private System.Windows.Forms.Button m_btnOpenStats;


    }
}

