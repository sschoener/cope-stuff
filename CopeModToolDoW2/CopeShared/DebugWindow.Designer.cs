namespace ModTool.Core
{
    partial class DebugWindow
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
            this._dlg_selectPG = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._tbx_command = new System.Windows.Forms.TextBox();
            this._lbx_log = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _dlg_selectPG
            // 
            this._dlg_selectPG.Filter = "*.rbf|*.rbf";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._tbx_command, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lbx_log, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 419);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // _tbx_command
            // 
            this._tbx_command.BackColor = System.Drawing.Color.Black;
            this._tbx_command.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._tbx_command.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbx_command.ForeColor = System.Drawing.Color.White;
            this._tbx_command.Location = new System.Drawing.Point(3, 402);
            this._tbx_command.Name = "_tbx_command";
            this._tbx_command.Size = new System.Drawing.Size(727, 13);
            this._tbx_command.TabIndex = 6;
            this._tbx_command.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TbxCommandPreviewKeyDown);
            // 
            // _lbx_log
            // 
            this._lbx_log.BackColor = System.Drawing.SystemColors.MenuText;
            this._lbx_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lbx_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lbx_log.ForeColor = System.Drawing.Color.White;
            this._lbx_log.FormattingEnabled = true;
            this._lbx_log.Location = new System.Drawing.Point(3, 3);
            this._lbx_log.Name = "_lbx_log";
            this._lbx_log.Size = new System.Drawing.Size(727, 393);
            this._lbx_log.TabIndex = 2;
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(733, 419);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DebugWindow";
            this.ShowIcon = false;
            this.Text = "Advanced Debug Mode";
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.DebugWindowPreviewKeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.OpenFileDialog _dlg_selectPG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox _tbx_command;
        private System.Windows.Forms.ListBox _lbx_log;
    }
}