namespace CopeDefenseLauncher
{
    partial class UnlockMenu
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
            this.m_lbxUnlocks = new System.Windows.Forms.ListBox();
            this.m_btnPurchase = new System.Windows.Forms.Button();
            this.m_labUnlockName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_labMoney = new System.Windows.Forms.Label();
            this.m_labUnlockPrice = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_rtbUnlockDescription = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_labUnlockReqName = new System.Windows.Forms.Label();
            this.m_chkbxOnlyShowAvailable = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_picbxUnlock = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxUnlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lbxUnlocks
            // 
            this.m_lbxUnlocks.FormattingEnabled = true;
            this.m_lbxUnlocks.Location = new System.Drawing.Point(12, 12);
            this.m_lbxUnlocks.Name = "m_lbxUnlocks";
            this.m_lbxUnlocks.Size = new System.Drawing.Size(274, 355);
            this.m_lbxUnlocks.Sorted = true;
            this.m_lbxUnlocks.TabIndex = 0;
            this.m_lbxUnlocks.SelectedIndexChanged += new System.EventHandler(this.LbxUnlocksSelectedIndexChanged);
            // 
            // m_btnPurchase
            // 
            this.m_btnPurchase.Location = new System.Drawing.Point(474, 373);
            this.m_btnPurchase.Name = "m_btnPurchase";
            this.m_btnPurchase.Size = new System.Drawing.Size(75, 23);
            this.m_btnPurchase.TabIndex = 1;
            this.m_btnPurchase.Text = "Purchase";
            this.m_btnPurchase.UseVisualStyleBackColor = true;
            this.m_btnPurchase.Click += new System.EventHandler(this.BtnPurchaseClick);
            // 
            // m_labUnlockName
            // 
            this.m_labUnlockName.AutoSize = true;
            this.m_labUnlockName.Location = new System.Drawing.Point(292, 12);
            this.m_labUnlockName.Name = "m_labUnlockName";
            this.m_labUnlockName.Size = new System.Drawing.Size(68, 13);
            this.m_labUnlockName.TabIndex = 2;
            this.m_labUnlockName.Text = "unlock name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 378);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Money:";
            // 
            // m_labMoney
            // 
            this.m_labMoney.Location = new System.Drawing.Point(356, 378);
            this.m_labMoney.Name = "m_labMoney";
            this.m_labMoney.Size = new System.Drawing.Size(42, 13);
            this.m_labMoney.TabIndex = 4;
            this.m_labMoney.Text = "amount";
            this.m_labMoney.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_labUnlockPrice
            // 
            this.m_labUnlockPrice.Location = new System.Drawing.Point(356, 51);
            this.m_labUnlockPrice.Name = "m_labUnlockPrice";
            this.m_labUnlockPrice.Size = new System.Drawing.Size(42, 13);
            this.m_labUnlockPrice.TabIndex = 5;
            this.m_labUnlockPrice.Text = "amount";
            this.m_labUnlockPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Price:";
            // 
            // m_rtbUnlockDescription
            // 
            this.m_rtbUnlockDescription.Location = new System.Drawing.Point(295, 121);
            this.m_rtbUnlockDescription.Name = "m_rtbUnlockDescription";
            this.m_rtbUnlockDescription.ReadOnly = true;
            this.m_rtbUnlockDescription.Size = new System.Drawing.Size(254, 246);
            this.m_rtbUnlockDescription.TabIndex = 7;
            this.m_rtbUnlockDescription.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Requires:";
            // 
            // m_labUnlockReqName
            // 
            this.m_labUnlockReqName.AutoSize = true;
            this.m_labUnlockReqName.Location = new System.Drawing.Point(347, 68);
            this.m_labUnlockReqName.Name = "m_labUnlockReqName";
            this.m_labUnlockReqName.Size = new System.Drawing.Size(51, 13);
            this.m_labUnlockReqName.TabIndex = 9;
            this.m_labUnlockReqName.Text = "req name";
            // 
            // m_chkbxOnlyShowAvailable
            // 
            this.m_chkbxOnlyShowAvailable.AutoSize = true;
            this.m_chkbxOnlyShowAvailable.Location = new System.Drawing.Point(12, 377);
            this.m_chkbxOnlyShowAvailable.Name = "m_chkbxOnlyShowAvailable";
            this.m_chkbxOnlyShowAvailable.Size = new System.Drawing.Size(160, 17);
            this.m_chkbxOnlyShowAvailable.TabIndex = 10;
            this.m_chkbxOnlyShowAvailable.Text = "Only show available unlocks";
            this.m_chkbxOnlyShowAvailable.UseVisualStyleBackColor = true;
            this.m_chkbxOnlyShowAvailable.CheckedChanged += new System.EventHandler(this.ChkbxOnlyShowAvailableCheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CopeDefenseLauncher.Properties.Resources.GenericIconMoney;
            this.pictureBox1.Location = new System.Drawing.Point(398, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // m_picbxUnlock
            // 
            this.m_picbxUnlock.Location = new System.Drawing.Point(485, 51);
            this.m_picbxUnlock.Name = "m_picbxUnlock";
            this.m_picbxUnlock.Size = new System.Drawing.Size(64, 64);
            this.m_picbxUnlock.TabIndex = 12;
            this.m_picbxUnlock.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CopeDefenseLauncher.Properties.Resources.GenericIconMoney;
            this.pictureBox2.Location = new System.Drawing.Point(398, 376);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // UnlockMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 414);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.m_picbxUnlock);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.m_chkbxOnlyShowAvailable);
            this.Controls.Add(this.m_labUnlockReqName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_rtbUnlockDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_labUnlockPrice);
            this.Controls.Add(this.m_labMoney);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_labUnlockName);
            this.Controls.Add(this.m_btnPurchase);
            this.Controls.Add(this.m_lbxUnlocks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "UnlockMenu";
            this.ShowInTaskbar = false;
            this.Text = "Unlock Menu";
            this.Shown += new System.EventHandler(this.UnlockMenuShown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picbxUnlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox m_lbxUnlocks;
        private System.Windows.Forms.Button m_btnPurchase;
        private System.Windows.Forms.Label m_labUnlockName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_labMoney;
        private System.Windows.Forms.Label m_labUnlockPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox m_rtbUnlockDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label m_labUnlockReqName;
        private System.Windows.Forms.CheckBox m_chkbxOnlyShowAvailable;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox m_picbxUnlock;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}