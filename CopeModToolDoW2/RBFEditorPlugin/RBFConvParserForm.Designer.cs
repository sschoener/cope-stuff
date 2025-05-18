namespace RBFPlugin
{
    partial class RBFConvParserForm
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
            this.m_rbfConvParser = new RBFPlugin.RBFConvParserControl();
            this.SuspendLayout();
            // 
            // m_rbfConvParser
            // 
            this.m_rbfConvParser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rbfConvParser.Location = new System.Drawing.Point(0, 0);
            this.m_rbfConvParser.Name = "m_rbfConvParser";
            this.m_rbfConvParser.Size = new System.Drawing.Size(698, 606);
            this.m_rbfConvParser.TabIndex = 0;
            this.m_rbfConvParser.OnSuccessfulParse += new cope.NotifyEventHandler(this._rbf_ConvParser_OnSuccessfulParse);
            // 
            // RBFConvParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 606);
            this.Controls.Add(this.m_rbfConvParser);
            this.Name = "RBFConvParserForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Insert Corsix\' style string";
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        internal RBFConvParserControl m_rbfConvParser;
    }
}