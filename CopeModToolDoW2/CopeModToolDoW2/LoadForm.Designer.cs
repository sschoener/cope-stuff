namespace ModTool.FE
{
    partial class LoadForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_btnDonate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::ModTool.FE.Properties.Resources.toolbox_load;
            this.pictureBox1.InitialImage = global::ModTool.FE.Properties.Resources.toolbox_load;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(578, 390);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // m_btnDonate
            // 
            this.m_btnDonate.BackColor = System.Drawing.Color.Black;
            this.m_btnDonate.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.m_btnDonate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.m_btnDonate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDonate.ForeColor = System.Drawing.Color.White;
            this.m_btnDonate.Location = new System.Drawing.Point(231, 355);
            this.m_btnDonate.Name = "m_btnDonate";
            this.m_btnDonate.Size = new System.Drawing.Size(132, 23);
            this.m_btnDonate.TabIndex = 1;
            this.m_btnDonate.Text = "Support the developer";
            this.m_btnDonate.UseVisualStyleBackColor = false;
            this.m_btnDonate.Click += new System.EventHandler(this.DonateButtonClick);
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 390);
            this.Controls.Add(this.m_btnDonate);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cope\'s DoW2 Toolbox Loading...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button m_btnDonate;
    }
}