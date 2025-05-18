namespace ModTool.FE
{
    partial class ModSettingsForm
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
            this.m_tbxModFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_tbxVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_tbxDescription = new System.Windows.Forms.TextBox();
            this.m_tbxUiName = new System.Windows.Forms.TextBox();
            this.m_tbxModName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._btn_OK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.m_nupUcsBaseIndex = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._btn_addSGA = new System.Windows.Forms.Button();
            this.m_lbxSgas = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_lbxSections = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_dlgAddSGA = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUcsBaseIndex)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.m_tbxModFolder);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.m_tbxVersion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_tbxDescription);
            this.groupBox1.Controls.Add(this.m_tbxUiName);
            this.groupBox1.Controls.Add(this.m_tbxModName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global";
            //
            // _tbx_folder
            //
            this.m_tbxModFolder.Enabled = false;
            this.m_tbxModFolder.Location = new System.Drawing.Point(67, 118);
            this.m_tbxModFolder.Name = "m_tbxModFolder";
            this.m_tbxModFolder.Size = new System.Drawing.Size(173, 20);
            this.m_tbxModFolder.TabIndex = 9;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Folder";
            //
            // _tbx_version
            //
            this.m_tbxVersion.Location = new System.Drawing.Point(67, 92);
            this.m_tbxVersion.Name = "m_tbxVersion";
            this.m_tbxVersion.Size = new System.Drawing.Size(173, 20);
            this.m_tbxVersion.TabIndex = 7;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Version";
            //
            // _tbx_description
            //
            this.m_tbxDescription.Location = new System.Drawing.Point(67, 65);
            this.m_tbxDescription.Name = "m_tbxDescription";
            this.m_tbxDescription.Size = new System.Drawing.Size(173, 20);
            this.m_tbxDescription.TabIndex = 5;
            //
            // _tbx_uiName
            //
            this.m_tbxUiName.Location = new System.Drawing.Point(67, 39);
            this.m_tbxUiName.Name = "m_tbxUiName";
            this.m_tbxUiName.Size = new System.Drawing.Size(173, 20);
            this.m_tbxUiName.TabIndex = 4;
            //
            // _tbx_modName
            //
            this.m_tbxModName.Location = new System.Drawing.Point(67, 13);
            this.m_tbxModName.Name = "m_tbxModName";
            this.m_tbxModName.Size = new System.Drawing.Size(173, 20);
            this.m_tbxModName.TabIndex = 3;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Description";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "UI-Name";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            //
            // _btn_OK
            //
            this._btn_OK.Location = new System.Drawing.Point(726, 351);
            this._btn_OK.Name = "_btn_OK";
            this._btn_OK.Size = new System.Drawing.Size(75, 23);
            this._btn_OK.TabIndex = 1;
            this._btn_OK.Text = "OK";
            this._btn_OK.UseVisualStyleBackColor = true;
            this._btn_OK.Click += new System.EventHandler(this.BtnOkClick);
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "UCS Base Index";
            //
            // _nup_ucsBaseIndex
            //
            this.m_nupUcsBaseIndex.Location = new System.Drawing.Point(134, 210);
            this.m_nupUcsBaseIndex.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
            this.m_nupUcsBaseIndex.Name = "m_nupUcsBaseIndex";
            this.m_nupUcsBaseIndex.Size = new System.Drawing.Size(117, 20);
            this.m_nupUcsBaseIndex.TabIndex = 3;
            this.m_nupUcsBaseIndex.Value = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this._btn_addSGA);
            this.groupBox2.Controls.Add(this.m_lbxSgas);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.m_lbxSections);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(264, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 333);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Archives";
            //
            // _btn_addSGA
            //
            this._btn_addSGA.Location = new System.Drawing.Point(462, 302);
            this._btn_addSGA.Name = "_btn_addSGA";
            this._btn_addSGA.Size = new System.Drawing.Size(75, 23);
            this._btn_addSGA.TabIndex = 4;
            this._btn_addSGA.Text = "Add SGAs";
            this._btn_addSGA.UseVisualStyleBackColor = true;
            this._btn_addSGA.Click += new System.EventHandler(this.BtnAddSGAClick);
            //
            // _lbx_sgas
            //
            this.m_lbxSgas.FormattingEnabled = true;
            this.m_lbxSgas.Location = new System.Drawing.Point(206, 32);
            this.m_lbxSgas.Name = "m_lbxSgas";
            this.m_lbxSgas.Size = new System.Drawing.Size(331, 264);
            this.m_lbxSgas.TabIndex = 3;
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(203, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "SGAs";
            //
            // _lbx_sections
            //
            this.m_lbxSections.FormattingEnabled = true;
            this.m_lbxSections.Location = new System.Drawing.Point(9, 32);
            this.m_lbxSections.Name = "m_lbxSections";
            this.m_lbxSections.Size = new System.Drawing.Size(191, 264);
            this.m_lbxSections.TabIndex = 1;
            this.m_lbxSections.SelectedIndexChanged += new System.EventHandler(this.LbxSectionsSelectedIndexChanged);
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Sections";
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 233);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(225, 52);
            this.label9.TabIndex = 5;
            this.label9.Text = "The UCS base index is used by the auto-index\r\nfunctionality. You should choose a " +
                "value\r\nsomewhat higher than the one\'s used in\r\nDOW2.ucs.";
            //
            // _dlg_addSGA
            //
            this.m_dlgAddSGA.Filter = "*.sga|*.sga";
            this.m_dlgAddSGA.Multiselect = true;
            this.m_dlgAddSGA.Title = "Select a SGA file";
            //
            // ModSettingsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 378);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.m_nupUcsBaseIndex);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._btn_OK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ModSettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Mod Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupUcsBaseIndex)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _btn_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_tbxModName;
        private System.Windows.Forms.TextBox m_tbxUiName;
        private System.Windows.Forms.TextBox m_tbxDescription;
        private System.Windows.Forms.TextBox m_tbxVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_tbxModFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown m_nupUcsBaseIndex;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox m_lbxSections;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox m_lbxSgas;
        private System.Windows.Forms.Button _btn_addSGA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog m_dlgAddSGA;
    }
}