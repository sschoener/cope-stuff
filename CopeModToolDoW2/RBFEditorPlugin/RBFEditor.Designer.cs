namespace RBFPlugin
{
    partial class RBFEditor
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMainRBFEditor = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._btn_saveRBF = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this._btn_openLibrary = new System.Windows.Forms.Button();
            this.m_rbfEditorCore = new RBFPlugin.RBFEditorCore();
            this.pnlMainRBFEditor.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainRBFEditor
            // 
            this.pnlMainRBFEditor.Controls.Add(this.tableLayoutPanel1);
            this.pnlMainRBFEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainRBFEditor.Location = new System.Drawing.Point(0, 0);
            this.pnlMainRBFEditor.Name = "pnlMainRBFEditor";
            this.pnlMainRBFEditor.Size = new System.Drawing.Size(926, 697);
            this.pnlMainRBFEditor.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_rbfEditorCore, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(926, 697);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel6, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel7, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 650);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(920, 44);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._btn_saveRBF);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(814, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 38);
            this.panel1.TabIndex = 0;
            // 
            // _btn_saveRBF
            // 
            this._btn_saveRBF.Image = global::RBFPlugin.Properties.Resources.document_save;
            this._btn_saveRBF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._btn_saveRBF.Location = new System.Drawing.Point(3, 7);
            this._btn_saveRBF.Name = "_btn_saveRBF";
            this._btn_saveRBF.Size = new System.Drawing.Size(97, 23);
            this._btn_saveRBF.TabIndex = 0;
            this._btn_saveRBF.Text = "Save";
            this._btn_saveRBF.UseVisualStyleBackColor = true;
            this._btn_saveRBF.Click += new System.EventHandler(this.BtnSaveRBFClick);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(589, 38);
            this.panel5.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(714, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(94, 38);
            this.panel6.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this._btn_openLibrary);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(598, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(110, 38);
            this.panel7.TabIndex = 3;
            // 
            // _btn_openLibrary
            // 
            this._btn_openLibrary.Location = new System.Drawing.Point(3, 7);
            this._btn_openLibrary.Name = "_btn_openLibrary";
            this._btn_openLibrary.Size = new System.Drawing.Size(104, 23);
            this._btn_openLibrary.TabIndex = 0;
            this._btn_openLibrary.Text = "Open RBF-Library";
            this._btn_openLibrary.UseVisualStyleBackColor = true;
            this._btn_openLibrary.Click += new System.EventHandler(this.BtnOpenLibraryClick);
            // 
            // m_rbfEditorCore
            // 
            this.m_rbfEditorCore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rbfEditorCore.Location = new System.Drawing.Point(3, 3);
            this.m_rbfEditorCore.Name = "m_rbfEditorCore";
            this.m_rbfEditorCore.Size = new System.Drawing.Size(920, 641);
            this.m_rbfEditorCore.TabIndex = 3;
            this.m_rbfEditorCore.HasChangesChanged += new cope.GenericHandler<bool>(this.RBFEditorCoreHasChangesChanged);
            // 
            // RBFEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMainRBFEditor);
            this.Name = "RBFEditor";
            this.Size = new System.Drawing.Size(926, 697);
            this.pnlMainRBFEditor.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion Vom Komponenten-Designer generierter Code

        private System.Windows.Forms.Panel pnlMainRBFEditor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _btn_saveRBF;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button _btn_openLibrary;
        private RBFEditorCore m_rbfEditorCore;
    }
}