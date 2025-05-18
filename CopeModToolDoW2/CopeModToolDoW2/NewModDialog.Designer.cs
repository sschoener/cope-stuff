namespace ModTool.FE
{
    partial class NewModDialog
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
            this._btn_createMod = new System.Windows.Forms.Button();
            this._lab_baseModule = new System.Windows.Forms.Label();
            this.m_tbxBaseModule = new System.Windows.Forms.TextBox();
            this._btn_baseModuleSearch = new System.Windows.Forms.Button();
            this._lab_modName = new System.Windows.Forms.Label();
            this.m_tbxModName = new System.Windows.Forms.TextBox();
            this._lab_modVersion = new System.Windows.Forms.Label();
            this.m_nupModVersion = new System.Windows.Forms.NumericUpDown();
            this._lab_modDescription = new System.Windows.Forms.Label();
            this.m_tbxModDescription = new System.Windows.Forms.TextBox();
            this.m_dlgSelectBaseModule = new System.Windows.Forms.OpenFileDialog();
            this.m_tbxDisplayedModName = new System.Windows.Forms.TextBox();
            this._lab_displayedName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_nupUCSBaseIndex = new System.Windows.Forms.NumericUpDown();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_chkbxRepackAttrib = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupModVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUCSBaseIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // _btn_createMod
            // 
            this._btn_createMod.Location = new System.Drawing.Point(494, 209);
            this._btn_createMod.Name = "_btn_createMod";
            this._btn_createMod.Size = new System.Drawing.Size(75, 23);
            this._btn_createMod.TabIndex = 6;
            this._btn_createMod.Text = "Create Mod";
            this._btn_createMod.UseVisualStyleBackColor = true;
            this._btn_createMod.Click += new System.EventHandler(this.BtnCreateModClick);
            // 
            // _lab_baseModule
            // 
            this._lab_baseModule.AutoSize = true;
            this._lab_baseModule.Location = new System.Drawing.Point(12, 9);
            this._lab_baseModule.Name = "_lab_baseModule";
            this._lab_baseModule.Size = new System.Drawing.Size(72, 13);
            this._lab_baseModule.TabIndex = 100;
            this._lab_baseModule.Text = "Base Module:";
            // 
            // m_tbxBaseModule
            // 
            this.m_tbxBaseModule.Location = new System.Drawing.Point(12, 25);
            this.m_tbxBaseModule.Name = "m_tbxBaseModule";
            this.m_tbxBaseModule.Size = new System.Drawing.Size(473, 20);
            this.m_tbxBaseModule.TabIndex = 0;
            this.m_tbxBaseModule.TextChanged += new System.EventHandler(this.TbxBaseModuleTextChanged);
            // 
            // _btn_baseModuleSearch
            // 
            this._btn_baseModuleSearch.Location = new System.Drawing.Point(494, 23);
            this._btn_baseModuleSearch.Name = "_btn_baseModuleSearch";
            this._btn_baseModuleSearch.Size = new System.Drawing.Size(75, 23);
            this._btn_baseModuleSearch.TabIndex = 1;
            this._btn_baseModuleSearch.Text = "Browse";
            this._btn_baseModuleSearch.UseVisualStyleBackColor = true;
            this._btn_baseModuleSearch.Click += new System.EventHandler(this.BtnBaseModuleSearchClick);
            // 
            // _lab_modName
            // 
            this._lab_modName.AutoSize = true;
            this._lab_modName.Location = new System.Drawing.Point(12, 48);
            this._lab_modName.Name = "_lab_modName";
            this._lab_modName.Size = new System.Drawing.Size(162, 13);
            this._lab_modName.TabIndex = 100;
            this._lab_modName.Text = "Mod Name (no spaces allowed!):";
            // 
            // m_tbxModName
            // 
            this.m_tbxModName.Location = new System.Drawing.Point(12, 64);
            this.m_tbxModName.Name = "m_tbxModName";
            this.m_tbxModName.Size = new System.Drawing.Size(473, 20);
            this.m_tbxModName.TabIndex = 2;
            // 
            // _lab_modVersion
            // 
            this._lab_modVersion.AutoSize = true;
            this._lab_modVersion.Location = new System.Drawing.Point(12, 167);
            this._lab_modVersion.Name = "_lab_modVersion";
            this._lab_modVersion.Size = new System.Drawing.Size(69, 13);
            this._lab_modVersion.TabIndex = 100;
            this._lab_modVersion.Text = "Mod Version:";
            // 
            // m_nupModVersion
            // 
            this.m_nupModVersion.DecimalPlaces = 1;
            this.m_nupModVersion.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_nupModVersion.Location = new System.Drawing.Point(12, 183);
            this.m_nupModVersion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_nupModVersion.Name = "m_nupModVersion";
            this.m_nupModVersion.Size = new System.Drawing.Size(89, 20);
            this.m_nupModVersion.TabIndex = 5;
            this.m_nupModVersion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _lab_modDescription
            // 
            this._lab_modDescription.AutoSize = true;
            this._lab_modDescription.Location = new System.Drawing.Point(12, 128);
            this._lab_modDescription.Name = "_lab_modDescription";
            this._lab_modDescription.Size = new System.Drawing.Size(63, 13);
            this._lab_modDescription.TabIndex = 100;
            this._lab_modDescription.Text = "Description:";
            // 
            // m_tbxModDescription
            // 
            this.m_tbxModDescription.Location = new System.Drawing.Point(12, 144);
            this.m_tbxModDescription.Name = "m_tbxModDescription";
            this.m_tbxModDescription.Size = new System.Drawing.Size(473, 20);
            this.m_tbxModDescription.TabIndex = 4;
            // 
            // m_dlgSelectBaseModule
            // 
            this.m_dlgSelectBaseModule.Filter = "Module Information File|*.module";
            this.m_dlgSelectBaseModule.Title = "Select the base module for your mod";
            // 
            // m_tbxDisplayedModName
            // 
            this.m_tbxDisplayedModName.Location = new System.Drawing.Point(12, 103);
            this.m_tbxDisplayedModName.Name = "m_tbxDisplayedModName";
            this.m_tbxDisplayedModName.Size = new System.Drawing.Size(473, 20);
            this.m_tbxDisplayedModName.TabIndex = 3;
            // 
            // _lab_displayedName
            // 
            this._lab_displayedName.AutoSize = true;
            this._lab_displayedName.Location = new System.Drawing.Point(12, 87);
            this._lab_displayedName.Name = "_lab_displayedName";
            this._lab_displayedName.Size = new System.Drawing.Size(111, 13);
            this._lab_displayedName.TabIndex = 100;
            this._lab_displayedName.Text = "Displayed Mod Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "UCS Base Index";
            // 
            // m_nupUCSBaseIndex
            // 
            this.m_nupUCSBaseIndex.Location = new System.Drawing.Point(128, 183);
            this.m_nupUCSBaseIndex.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
            this.m_nupUCSBaseIndex.Name = "m_nupUCSBaseIndex";
            this.m_nupUCSBaseIndex.Size = new System.Drawing.Size(109, 20);
            this.m_nupUCSBaseIndex.TabIndex = 102;
            this.m_nupUCSBaseIndex.Value = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(12, 209);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 103;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // m_chkbxRepackAttrib
            // 
            this.m_chkbxRepackAttrib.AutoSize = true;
            this.m_chkbxRepackAttrib.Location = new System.Drawing.Point(317, 171);
            this.m_chkbxRepackAttrib.Name = "m_chkbxRepackAttrib";
            this.m_chkbxRepackAttrib.Size = new System.Drawing.Size(168, 43);
            this.m_chkbxRepackAttrib.TabIndex = 104;
            this.m_chkbxRepackAttrib.Text = "Repack GameAttrib.sga\r\n(recommended for Retribution,\r\nmay take some time)";
            this.m_chkbxRepackAttrib.UseVisualStyleBackColor = true;
            // 
            // NewModDialog
            // 
            this.AcceptButton = this._btn_createMod;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 244);
            this.Controls.Add(this.m_chkbxRepackAttrib);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_nupUCSBaseIndex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tbxDisplayedModName);
            this.Controls.Add(this._lab_displayedName);
            this.Controls.Add(this.m_tbxModDescription);
            this.Controls.Add(this._lab_modDescription);
            this.Controls.Add(this.m_nupModVersion);
            this.Controls.Add(this._lab_modVersion);
            this.Controls.Add(this.m_tbxModName);
            this.Controls.Add(this._lab_modName);
            this.Controls.Add(this._btn_baseModuleSearch);
            this.Controls.Add(this._btn_createMod);
            this.Controls.Add(this._lab_baseModule);
            this.Controls.Add(this.m_tbxBaseModule);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewModDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "New DoW2 Mod";
            ((System.ComponentModel.ISupportInitialize)(this.m_nupModVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUCSBaseIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Vom Windows Form-Designer generierter Code

        private System.Windows.Forms.Button _btn_createMod;
        private System.Windows.Forms.Label _lab_baseModule;
        private System.Windows.Forms.TextBox m_tbxBaseModule;
        private System.Windows.Forms.Button _btn_baseModuleSearch;
        private System.Windows.Forms.Label _lab_modName;
        private System.Windows.Forms.TextBox m_tbxModName;
        private System.Windows.Forms.Label _lab_modVersion;
        private System.Windows.Forms.NumericUpDown m_nupModVersion;
        private System.Windows.Forms.Label _lab_modDescription;
        private System.Windows.Forms.TextBox m_tbxModDescription;
        private System.Windows.Forms.OpenFileDialog m_dlgSelectBaseModule;
        private System.Windows.Forms.TextBox m_tbxDisplayedModName;
        private System.Windows.Forms.Label _lab_displayedName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown m_nupUCSBaseIndex;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.CheckBox m_chkbxRepackAttrib;
    }
}