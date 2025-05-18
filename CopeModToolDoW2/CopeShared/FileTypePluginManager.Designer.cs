namespace ModTool.Core
{
    partial class FileTypePluginManager
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._btn_OK = new System.Windows.Forms.Button();
            this.pluginManagerControl1 = new ModTool.Core.PluginManagerControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.Controls.Add(this.pluginManagerControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(516, 381);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // panel1
            //
            this.panel1.Controls.Add(this._btn_OK);
            this.panel1.Location = new System.Drawing.Point(433, 353);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 25);
            this.panel1.TabIndex = 1;
            //
            // _btn_OK
            //
            this._btn_OK.Location = new System.Drawing.Point(3, 1);
            this._btn_OK.Name = "_btn_OK";
            this._btn_OK.Size = new System.Drawing.Size(75, 23);
            this._btn_OK.TabIndex = 0;
            this._btn_OK.Text = "OK";
            this._btn_OK.UseVisualStyleBackColor = true;
            this._btn_OK.Click += new System.EventHandler(this.BtnOkClick);
            //
            // pluginManagerControl1
            //
            this.tableLayoutPanel1.SetColumnSpan(this.pluginManagerControl1, 2);
            this.pluginManagerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginManagerControl1.Location = new System.Drawing.Point(3, 3);
            this.pluginManagerControl1.Name = "pluginManagerControl1";
            this.pluginManagerControl1.Size = new System.Drawing.Size(510, 344);
            this.pluginManagerControl1.TabIndex = 0;
            //
            // FileTypePluginManager
            //
            this.AcceptButton = this._btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 381);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FileTypePluginManager";
            this.ShowInTaskbar = false;
            this.Text = "File Type Plugin Manager";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Vom Windows Form-Designer generierter Code

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private PluginManagerControl pluginManagerControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _btn_OK;
    }
}