namespace SGAExtractor
{
    partial class Form1
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
            this._btn_selectArchive = new System.Windows.Forms.Button();
            this._tbx_sgaArchive = new System.Windows.Forms.TextBox();
            this._btn_selectOutput = new System.Windows.Forms.Button();
            this._tbx_outputPath = new System.Windows.Forms.TextBox();
            this._btn_extract = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._dlg_selectArchive = new System.Windows.Forms.OpenFileDialog();
            this._dlg_selectOutputDir = new System.Windows.Forms.FolderBrowserDialog();
            this._pgb_extract = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // _btn_selectArchive
            // 
            this._btn_selectArchive.Location = new System.Drawing.Point(16, 11);
            this._btn_selectArchive.Name = "_btn_selectArchive";
            this._btn_selectArchive.Size = new System.Drawing.Size(102, 23);
            this._btn_selectArchive.TabIndex = 0;
            this._btn_selectArchive.Text = "Select archive";
            this._btn_selectArchive.UseVisualStyleBackColor = true;
            this._btn_selectArchive.Click += new System.EventHandler(this.BtnSelectArchiveClick);
            // 
            // _tbx_sgaArchive
            // 
            this._tbx_sgaArchive.Location = new System.Drawing.Point(124, 13);
            this._tbx_sgaArchive.Name = "_tbx_sgaArchive";
            this._tbx_sgaArchive.Size = new System.Drawing.Size(302, 20);
            this._tbx_sgaArchive.TabIndex = 1;
            // 
            // _btn_selectOutput
            // 
            this._btn_selectOutput.Location = new System.Drawing.Point(16, 40);
            this._btn_selectOutput.Name = "_btn_selectOutput";
            this._btn_selectOutput.Size = new System.Drawing.Size(102, 23);
            this._btn_selectOutput.TabIndex = 2;
            this._btn_selectOutput.Text = "Select output dir";
            this._btn_selectOutput.UseVisualStyleBackColor = true;
            this._btn_selectOutput.Click += new System.EventHandler(this.BtnSelectOutputClick);
            // 
            // _tbx_outputPath
            // 
            this._tbx_outputPath.Location = new System.Drawing.Point(124, 42);
            this._tbx_outputPath.Name = "_tbx_outputPath";
            this._tbx_outputPath.Size = new System.Drawing.Size(302, 20);
            this._tbx_outputPath.TabIndex = 3;
            // 
            // _btn_extract
            // 
            this._btn_extract.Location = new System.Drawing.Point(257, 68);
            this._btn_extract.Name = "_btn_extract";
            this._btn_extract.Size = new System.Drawing.Size(169, 23);
            this._btn_extract.TabIndex = 4;
            this._btn_extract.Text = "Extract!";
            this._btn_extract.UseVisualStyleBackColor = true;
            this._btn_extract.Click += new System.EventHandler(this.BtnExtractClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Made by cope - 07/31/2010 - v1.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 65);
            this.label2.TabIndex = 6;
            this.label2.Text = "Supported versions:\r\nv4 (CoH, CoH:OF, CoH:ToV)\r\nv4.1 (CoH: Online China)\r\nv5 (DoW" +
                "2, DoW2:CR)\r\nv5.1 (CoH: Online America)";
            // 
            // _dlg_selectArchive
            // 
            this._dlg_selectArchive.Filter = "*.sga|*.sga";
            this._dlg_selectArchive.Title = "Select SGA archive...";
            // 
            // _pgb_extract
            // 
            this._pgb_extract.Location = new System.Drawing.Point(257, 99);
            this._pgb_extract.Name = "_pgb_extract";
            this._pgb_extract.Size = new System.Drawing.Size(169, 23);
            this._pgb_extract.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 150);
            this.Controls.Add(this._pgb_extract);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._btn_extract);
            this.Controls.Add(this._tbx_outputPath);
            this.Controls.Add(this._btn_selectOutput);
            this.Controls.Add(this._tbx_sgaArchive);
            this.Controls.Add(this._btn_selectArchive);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Cope\'s SGA Extractor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btn_selectArchive;
        private System.Windows.Forms.TextBox _tbx_sgaArchive;
        private System.Windows.Forms.Button _btn_selectOutput;
        private System.Windows.Forms.TextBox _tbx_outputPath;
        private System.Windows.Forms.Button _btn_extract;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog _dlg_selectArchive;
        private System.Windows.Forms.FolderBrowserDialog _dlg_selectOutputDir;
        private System.Windows.Forms.ProgressBar _pgb_extract;
    }
}

