namespace ModTool.FE
{
    partial class OptionsDialog
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._chkbx_dvHideIcons = new System.Windows.Forms.CheckBox();
            this._chkbx_dvMarkChangedDirs = new System.Windows.Forms.CheckBox();
            this._btn_OK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._chkbx_useAdvancedMode = new System.Windows.Forms.CheckBox();
            this._tbx_params = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._chkbx_debugWindow = new System.Windows.Forms.CheckBox();
            this._chkbx_window = new System.Windows.Forms.CheckBox();
            this._chkbx_noMovies = new System.Windows.Forms.CheckBox();
            this._btn_selectSteamExe = new System.Windows.Forms.Button();
            this._tbx_steamExe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._dlg_selectSteamExe = new System.Windows.Forms.OpenFileDialog();
            this._btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._cbx_language = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this._chkbx_allowOpeningTwice = new System.Windows.Forms.CheckBox();
            this._chkbx_markChangedTabs = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_radUseDoW2 = new System.Windows.Forms.RadioButton();
            this.m_radUseChaosRising = new System.Windows.Forms.RadioButton();
            this.m_radUseRetribution = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._chkbx_dvHideIcons);
            this.groupBox1.Controls.Add(this._chkbx_dvMarkChangedDirs);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 116);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Directory View";
            // 
            // _chkbx_dvHideIcons
            // 
            this._chkbx_dvHideIcons.AutoSize = true;
            this._chkbx_dvHideIcons.ForeColor = System.Drawing.SystemColors.ControlText;
            this._chkbx_dvHideIcons.Location = new System.Drawing.Point(6, 42);
            this._chkbx_dvHideIcons.Name = "_chkbx_dvHideIcons";
            this._chkbx_dvHideIcons.Size = new System.Drawing.Size(123, 17);
            this._chkbx_dvHideIcons.TabIndex = 1;
            this._chkbx_dvHideIcons.Text = "Hide file/folder icons";
            this._chkbx_dvHideIcons.UseVisualStyleBackColor = true;
            // 
            // _chkbx_dvMarkChangedDirs
            // 
            this._chkbx_dvMarkChangedDirs.AutoSize = true;
            this._chkbx_dvMarkChangedDirs.ForeColor = System.Drawing.SystemColors.ControlText;
            this._chkbx_dvMarkChangedDirs.Location = new System.Drawing.Point(6, 19);
            this._chkbx_dvMarkChangedDirs.Name = "_chkbx_dvMarkChangedDirs";
            this._chkbx_dvMarkChangedDirs.Size = new System.Drawing.Size(146, 17);
            this._chkbx_dvMarkChangedDirs.TabIndex = 0;
            this._chkbx_dvMarkChangedDirs.Text = "Mark changed directories";
            this._chkbx_dvMarkChangedDirs.UseVisualStyleBackColor = true;
            // 
            // _btn_OK
            // 
            this._btn_OK.Location = new System.Drawing.Point(543, 233);
            this._btn_OK.Name = "_btn_OK";
            this._btn_OK.Size = new System.Drawing.Size(75, 23);
            this._btn_OK.TabIndex = 1;
            this._btn_OK.Text = "OK";
            this._btn_OK.UseVisualStyleBackColor = true;
            this._btn_OK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_radUseRetribution);
            this.groupBox2.Controls.Add(this.m_radUseChaosRising);
            this.groupBox2.Controls.Add(this.m_radUseDoW2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this._chkbx_useAdvancedMode);
            this.groupBox2.Controls.Add(this._tbx_params);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this._chkbx_debugWindow);
            this.groupBox2.Controls.Add(this._chkbx_window);
            this.groupBox2.Controls.Add(this._chkbx_noMovies);
            this.groupBox2.Controls.Add(this._btn_selectSteamExe);
            this.groupBox2.Controls.Add(this._tbx_steamExe);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(182, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(436, 138);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Testing";
            // 
            // _chkbx_useAdvancedMode
            // 
            this._chkbx_useAdvancedMode.AutoSize = true;
            this._chkbx_useAdvancedMode.Location = new System.Drawing.Point(9, 113);
            this._chkbx_useAdvancedMode.Name = "_chkbx_useAdvancedMode";
            this._chkbx_useAdvancedMode.Size = new System.Drawing.Size(162, 17);
            this._chkbx_useAdvancedMode.TabIndex = 9;
            this._chkbx_useAdvancedMode.Text = "Use Advanced Debug Mode";
            this._chkbx_useAdvancedMode.UseVisualStyleBackColor = true;
            // 
            // _tbx_params
            // 
            this._tbx_params.Location = new System.Drawing.Point(121, 86);
            this._tbx_params.Name = "_tbx_params";
            this._tbx_params.Size = new System.Drawing.Size(308, 20);
            this._tbx_params.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Additional Parameters";
            // 
            // _chkbx_debugWindow
            // 
            this._chkbx_debugWindow.AutoSize = true;
            this._chkbx_debugWindow.Location = new System.Drawing.Point(285, 66);
            this._chkbx_debugWindow.Name = "_chkbx_debugWindow";
            this._chkbx_debugWindow.Size = new System.Drawing.Size(97, 17);
            this._chkbx_debugWindow.TabIndex = 6;
            this._chkbx_debugWindow.Text = "Debug window";
            this._chkbx_debugWindow.UseVisualStyleBackColor = true;
            // 
            // _chkbx_window
            // 
            this._chkbx_window.AutoSize = true;
            this._chkbx_window.Location = new System.Drawing.Point(202, 66);
            this._chkbx_window.Name = "_chkbx_window";
            this._chkbx_window.Size = new System.Drawing.Size(77, 17);
            this._chkbx_window.TabIndex = 5;
            this._chkbx_window.Text = "Windowed";
            this._chkbx_window.UseVisualStyleBackColor = true;
            // 
            // _chkbx_noMovies
            // 
            this._chkbx_noMovies.AutoSize = true;
            this._chkbx_noMovies.Location = new System.Drawing.Point(120, 66);
            this._chkbx_noMovies.Name = "_chkbx_noMovies";
            this._chkbx_noMovies.Size = new System.Drawing.Size(76, 17);
            this._chkbx_noMovies.TabIndex = 4;
            this._chkbx_noMovies.Text = "No movies";
            this._chkbx_noMovies.UseVisualStyleBackColor = true;
            // 
            // _btn_selectSteamExe
            // 
            this._btn_selectSteamExe.Location = new System.Drawing.Point(354, 15);
            this._btn_selectSteamExe.Name = "_btn_selectSteamExe";
            this._btn_selectSteamExe.Size = new System.Drawing.Size(75, 23);
            this._btn_selectSteamExe.TabIndex = 2;
            this._btn_selectSteamExe.Text = "Select";
            this._btn_selectSteamExe.UseVisualStyleBackColor = true;
            this._btn_selectSteamExe.Click += new System.EventHandler(this.BtnSelectDoW2ExeClick);
            // 
            // _tbx_steamExe
            // 
            this._tbx_steamExe.Location = new System.Drawing.Point(121, 17);
            this._tbx_steamExe.Name = "_tbx_steamExe";
            this._tbx_steamExe.Size = new System.Drawing.Size(227, 20);
            this._tbx_steamExe.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Steam Executable";
            // 
            // _dlg_selectSteamExe
            // 
            this._dlg_selectSteamExe.FileName = "Steam.exe";
            this._dlg_selectSteamExe.Filter = "Executables|*.exe";
            this._dlg_selectSteamExe.Title = "Select Steam Executable";
            // 
            // _btn_Cancel
            // 
            this._btn_Cancel.Location = new System.Drawing.Point(12, 233);
            this._btn_Cancel.Name = "_btn_Cancel";
            this._btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this._btn_Cancel.TabIndex = 101;
            this._btn_Cancel.Text = "Cancel";
            this._btn_Cancel.UseVisualStyleBackColor = true;
            this._btn_Cancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._cbx_language);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this._chkbx_allowOpeningTwice);
            this.groupBox3.Controls.Add(this._chkbx_markChangedTabs);
            this.groupBox3.Location = new System.Drawing.Point(12, 156);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(606, 71);
            this.groupBox3.TabIndex = 102;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "General";
            // 
            // _cbx_language
            // 
            this._cbx_language.FormattingEnabled = true;
            this._cbx_language.Items.AddRange(new object[] {
            "Chinese",
            "Czech",
            "English",
            "French",
            "German",
            "Hungarian",
            "Italian",
            "Polish",
            "Russian",
            "Spanish"});
            this._cbx_language.Location = new System.Drawing.Point(179, 42);
            this._cbx_language.Name = "_cbx_language";
            this._cbx_language.Size = new System.Drawing.Size(262, 21);
            this._cbx_language.Sorted = true;
            this._cbx_language.TabIndex = 3;
            this._cbx_language.Text = "English";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "DoW2 Language";
            // 
            // _chkbx_allowOpeningTwice
            // 
            this._chkbx_allowOpeningTwice.AutoSize = true;
            this._chkbx_allowOpeningTwice.Location = new System.Drawing.Point(179, 19);
            this._chkbx_allowOpeningTwice.Name = "_chkbx_allowOpeningTwice";
            this._chkbx_allowOpeningTwice.Size = new System.Drawing.Size(262, 17);
            this._chkbx_allowOpeningTwice.TabIndex = 1;
            this._chkbx_allowOpeningTwice.Text = "Allow opening the same file multiple times at a time";
            this._chkbx_allowOpeningTwice.UseVisualStyleBackColor = true;
            // 
            // _chkbx_markChangedTabs
            // 
            this._chkbx_markChangedTabs.AutoSize = true;
            this._chkbx_markChangedTabs.Location = new System.Drawing.Point(6, 19);
            this._chkbx_markChangedTabs.Name = "_chkbx_markChangedTabs";
            this._chkbx_markChangedTabs.Size = new System.Drawing.Size(118, 17);
            this._chkbx_markChangedTabs.TabIndex = 0;
            this._chkbx_markChangedTabs.Text = "Mark changed tabs";
            this._chkbx_markChangedTabs.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Parameters:";
            // 
            // m_radUseDoW2
            // 
            this.m_radUseDoW2.AutoSize = true;
            this.m_radUseDoW2.Checked = true;
            this.m_radUseDoW2.Location = new System.Drawing.Point(121, 41);
            this.m_radUseDoW2.Name = "m_radUseDoW2";
            this.m_radUseDoW2.Size = new System.Drawing.Size(56, 17);
            this.m_radUseDoW2.TabIndex = 11;
            this.m_radUseDoW2.TabStop = true;
            this.m_radUseDoW2.Text = "DoW2";
            this.m_radUseDoW2.UseVisualStyleBackColor = true;
            // 
            // m_radUseChaosRising
            // 
            this.m_radUseChaosRising.AutoSize = true;
            this.m_radUseChaosRising.Location = new System.Drawing.Point(202, 42);
            this.m_radUseChaosRising.Name = "m_radUseChaosRising";
            this.m_radUseChaosRising.Size = new System.Drawing.Size(40, 17);
            this.m_radUseChaosRising.TabIndex = 12;
            this.m_radUseChaosRising.Text = "CR";
            this.m_radUseChaosRising.UseVisualStyleBackColor = true;
            // 
            // m_radUseRetribution
            // 
            this.m_radUseRetribution.AutoSize = true;
            this.m_radUseRetribution.Location = new System.Drawing.Point(285, 41);
            this.m_radUseRetribution.Name = "m_radUseRetribution";
            this.m_radUseRetribution.Size = new System.Drawing.Size(76, 17);
            this.m_radUseRetribution.TabIndex = 13;
            this.m_radUseRetribution.Text = "Retribution";
            this.m_radUseRetribution.UseVisualStyleBackColor = true;
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 262);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this._btn_Cancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._btn_OK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Vom Windows Form-Designer generierter Code

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _chkbx_dvHideIcons;
        private System.Windows.Forms.CheckBox _chkbx_dvMarkChangedDirs;
        private System.Windows.Forms.Button _btn_OK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _tbx_steamExe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _btn_selectSteamExe;
        private System.Windows.Forms.OpenFileDialog _dlg_selectSteamExe;
        private System.Windows.Forms.Button _btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox _chkbx_markChangedTabs;
        private System.Windows.Forms.CheckBox _chkbx_allowOpeningTwice;
        private System.Windows.Forms.CheckBox _chkbx_noMovies;
        private System.Windows.Forms.CheckBox _chkbx_debugWindow;
        private System.Windows.Forms.CheckBox _chkbx_window;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _tbx_params;
        private System.Windows.Forms.ComboBox _cbx_language;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox _chkbx_useAdvancedMode;
        private System.Windows.Forms.RadioButton m_radUseRetribution;
        private System.Windows.Forms.RadioButton m_radUseChaosRising;
        private System.Windows.Forms.RadioButton m_radUseDoW2;
        private System.Windows.Forms.Label label4;
    }
}