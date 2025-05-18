namespace cope.UI
{
    partial class DragBox<T>
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._pnl_header = new System.Windows.Forms.Panel();
            this._lab_title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this._pnl_header.SuspendLayout();
            this.SuspendLayout();
            //
            // _pnl_header
            //
            this._pnl_header.Controls.Add(this._lab_title);
            this._pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this._pnl_header.Location = new System.Drawing.Point(0, 0);
            this._pnl_header.Margin = new System.Windows.Forms.Padding(0);
            this._pnl_header.Name = "_pnl_header";
            this._pnl_header.Size = new System.Drawing.Size(329, 27);
            this._pnl_header.TabIndex = 1;
            //
            // _lab_title
            //
            this._lab_title.AutoSize = true;
            this._lab_title.BackColor = System.Drawing.Color.Transparent;
            this._lab_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lab_title.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lab_title.Location = new System.Drawing.Point(7, 7);
            this._lab_title.Name = "_lab_title";
            this._lab_title.Size = new System.Drawing.Size(27, 13);
            this._lab_title.TabIndex = 0;
            this._lab_title.Text = "Title";
            //
            // panel1
            //
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 137);
            this.panel1.TabIndex = 2;
            //
            // DragBox
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._pnl_header);
            this.Name = "DragBox";
            this.Size = new System.Drawing.Size(329, 164);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DragBoxMouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragBoxMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragBoxMouseMove);
            this._pnl_header.ResumeLayout(false);
            this._pnl_header.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        private System.Windows.Forms.Panel _pnl_header;
        private System.Windows.Forms.Label _lab_title;
        private System.Windows.Forms.Panel panel1;
    }
}