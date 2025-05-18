namespace CopeDefenseLauncher
{
    partial class MainForm
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
            this.m_btnStart = new System.Windows.Forms.Button();
            this.m_rtbOutput = new System.Windows.Forms.RichTextBox();
            this.m_tbxUsername = new System.Windows.Forms.TextBox();
            this.m_tbxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tbxStartArguments = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnPlayerSetup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_btnStart
            // 
            this.m_btnStart.Location = new System.Drawing.Point(428, 62);
            this.m_btnStart.Name = "m_btnStart";
            this.m_btnStart.Size = new System.Drawing.Size(75, 23);
            this.m_btnStart.TabIndex = 0;
            this.m_btnStart.Text = "Start";
            this.m_btnStart.UseVisualStyleBackColor = true;
            this.m_btnStart.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // m_rtbOutput
            // 
            this.m_rtbOutput.BackColor = System.Drawing.Color.Black;
            this.m_rtbOutput.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_rtbOutput.ForeColor = System.Drawing.Color.White;
            this.m_rtbOutput.Location = new System.Drawing.Point(12, 96);
            this.m_rtbOutput.Name = "m_rtbOutput";
            this.m_rtbOutput.ReadOnly = true;
            this.m_rtbOutput.Size = new System.Drawing.Size(491, 203);
            this.m_rtbOutput.TabIndex = 2;
            this.m_rtbOutput.Text = "";
            // 
            // m_tbxUsername
            // 
            this.m_tbxUsername.Location = new System.Drawing.Point(94, 12);
            this.m_tbxUsername.MaxLength = 25;
            this.m_tbxUsername.Name = "m_tbxUsername";
            this.m_tbxUsername.Size = new System.Drawing.Size(195, 20);
            this.m_tbxUsername.TabIndex = 3;
            // 
            // m_tbxPassword
            // 
            this.m_tbxPassword.Location = new System.Drawing.Point(94, 38);
            this.m_tbxPassword.MaxLength = 40;
            this.m_tbxPassword.Name = "m_tbxPassword";
            this.m_tbxPassword.PasswordChar = '*';
            this.m_tbxPassword.Size = new System.Drawing.Size(195, 20);
            this.m_tbxPassword.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password";
            // 
            // m_tbxStartArguments
            // 
            this.m_tbxStartArguments.Location = new System.Drawing.Point(94, 64);
            this.m_tbxStartArguments.Name = "m_tbxStartArguments";
            this.m_tbxStartArguments.Size = new System.Drawing.Size(195, 20);
            this.m_tbxStartArguments.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "DoW2 Params";
            // 
            // m_btnPlayerSetup
            // 
            this.m_btnPlayerSetup.Location = new System.Drawing.Point(428, 10);
            this.m_btnPlayerSetup.Name = "m_btnPlayerSetup";
            this.m_btnPlayerSetup.Size = new System.Drawing.Size(75, 23);
            this.m_btnPlayerSetup.TabIndex = 9;
            this.m_btnPlayerSetup.Text = "Player setup";
            this.m_btnPlayerSetup.UseVisualStyleBackColor = true;
            this.m_btnPlayerSetup.Click += new System.EventHandler(this.PlayerSetupButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 311);
            this.Controls.Add(this.m_btnPlayerSetup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_tbxStartArguments);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tbxPassword);
            this.Controls.Add(this.m_tbxUsername);
            this.Controls.Add(this.m_rtbOutput);
            this.Controls.Add(this.m_btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Cope\'s Defense Launcher v0.1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnStart;
        private System.Windows.Forms.RichTextBox m_rtbOutput;
        private System.Windows.Forms.TextBox m_tbxUsername;
        private System.Windows.Forms.TextBox m_tbxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_tbxStartArguments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_btnPlayerSetup;
    }
}

