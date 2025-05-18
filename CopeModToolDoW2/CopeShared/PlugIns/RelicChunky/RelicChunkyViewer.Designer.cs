namespace ModTool.Core.PlugIns.RelicChunky
{
    partial class RelicChunkyViewer
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._trv_chunks = new System.Windows.Forms.TreeView();
            this._cms_chunkTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyChunkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutChunkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertChunkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertAtRootLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteChunkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._chi_info = new ChunkyHeaderInfo();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this._btn_save = new System.Windows.Forms.Button();
            this._btn_showInfo = new System.Windows.Forms.Button();
            this._btn_closeChunk = new System.Windows.Forms.Button();
            this._btn_closeAllChunks = new System.Windows.Forms.Button();
            this._pnl_chunkHandler = new System.Windows.Forms.Panel();
            this._tbc_chunkHandlers = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel1.SuspendLayout();
            this._cms_chunkTree.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this._pnl_chunkHandler.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this._trv_chunks, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._chi_info, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 293F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(355, 700);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // _trv_chunks
            //
            this._trv_chunks.ContextMenuStrip = this._cms_chunkTree;
            this._trv_chunks.Dock = System.Windows.Forms.DockStyle.Fill;
            this._trv_chunks.Location = new System.Drawing.Point(0, 0);
            this._trv_chunks.Margin = new System.Windows.Forms.Padding(0);
            this._trv_chunks.Name = "_trv_chunks";
            this._trv_chunks.Size = new System.Drawing.Size(355, 407);
            this._trv_chunks.TabIndex = 0;
            this._trv_chunks.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvChunksNodeMouseDoubleClick);
            this._trv_chunks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvChunksAfterSelect);
            this._trv_chunks.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvChunksNodeMouseClick);
            //
            // _cms_chunkTree
            //
            this._cms_chunkTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyChunkToolStripMenuItem,
            this.cutChunkToolStripMenuItem,
            this.insertChunkToolStripMenuItem,
            this.insertAtRootLevelToolStripMenuItem,
            this.deleteChunkToolStripMenuItem});
            this._cms_chunkTree.Name = "_cms_chunkTree";
            this._cms_chunkTree.Size = new System.Drawing.Size(185, 114);
            //
            // copyChunkToolStripMenuItem
            //
            this.copyChunkToolStripMenuItem.Name = "copyChunkToolStripMenuItem";
            this.copyChunkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyChunkToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.copyChunkToolStripMenuItem.Text = "Copy Chunk";
            this.copyChunkToolStripMenuItem.Click += new System.EventHandler(this.CopyChunkToolStripMenuItemClick);
            //
            // cutChunkToolStripMenuItem
            //
            this.cutChunkToolStripMenuItem.Name = "cutChunkToolStripMenuItem";
            this.cutChunkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutChunkToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.cutChunkToolStripMenuItem.Text = "Cut Chunk";
            this.cutChunkToolStripMenuItem.Click += new System.EventHandler(this.CutChunkToolStripMenuItemClick);
            //
            // insertChunkToolStripMenuItem
            //
            this.insertChunkToolStripMenuItem.Name = "insertChunkToolStripMenuItem";
            this.insertChunkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.insertChunkToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.insertChunkToolStripMenuItem.Text = "Insert Chunk";
            this.insertChunkToolStripMenuItem.Click += new System.EventHandler(this.InsertChunkToolStripMenuItemClick);
            //
            // insertAtRootLevelToolStripMenuItem
            //
            this.insertAtRootLevelToolStripMenuItem.Name = "insertAtRootLevelToolStripMenuItem";
            this.insertAtRootLevelToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.insertAtRootLevelToolStripMenuItem.Text = "Insert at Root Level";
            this.insertAtRootLevelToolStripMenuItem.Click += new System.EventHandler(this.InsertAtRootLevelToolStripMenuItemClick);
            //
            // deleteChunkToolStripMenuItem
            //
            this.deleteChunkToolStripMenuItem.Name = "deleteChunkToolStripMenuItem";
            this.deleteChunkToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteChunkToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteChunkToolStripMenuItem.Text = "Delete Chunk";
            this.deleteChunkToolStripMenuItem.Click += new System.EventHandler(this.DeleteChunkToolStripMenuItemClick);
            //
            // _chi_info
            //
            this._chi_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chi_info.Location = new System.Drawing.Point(3, 410);
            this._chi_info.Name = "_chi_info";
            this._chi_info.Size = new System.Drawing.Size(349, 287);
            this._chi_info.TabIndex = 1;
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(1040, 700);
            this.splitContainer1.SplitterDistance = 355;
            this.splitContainer1.TabIndex = 1;
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this._pnl_chunkHandler, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(681, 700);
            this.tableLayoutPanel2.TabIndex = 0;
            //
            // tableLayoutPanel3
            //
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel3.Controls.Add(this._btn_save, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this._btn_showInfo, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this._btn_closeChunk, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this._btn_closeAllChunks, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 668);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(675, 29);
            this.tableLayoutPanel3.TabIndex = 0;
            //
            // _btn_save
            //
            this._btn_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btn_save.Location = new System.Drawing.Point(548, 3);
            this._btn_save.Name = "_btn_save";
            this._btn_save.Size = new System.Drawing.Size(124, 23);
            this._btn_save.TabIndex = 0;
            this._btn_save.Text = "Save File and Chunks";
            this._btn_save.UseVisualStyleBackColor = true;
            this._btn_save.Click += new System.EventHandler(this.BtnSaveClick);
            //
            // _btn_showInfo
            //
            this._btn_showInfo.Location = new System.Drawing.Point(283, 3);
            this._btn_showInfo.Name = "_btn_showInfo";
            this._btn_showInfo.Size = new System.Drawing.Size(73, 23);
            this._btn_showInfo.TabIndex = 1;
            this._btn_showInfo.Text = "File Info";
            this._btn_showInfo.UseVisualStyleBackColor = true;
            this._btn_showInfo.Click += new System.EventHandler(this.BtnShowInfoClick);
            //
            // _btn_closeChunk
            //
            this._btn_closeChunk.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btn_closeChunk.Location = new System.Drawing.Point(362, 3);
            this._btn_closeChunk.Name = "_btn_closeChunk";
            this._btn_closeChunk.Size = new System.Drawing.Size(77, 23);
            this._btn_closeChunk.TabIndex = 2;
            this._btn_closeChunk.Text = "Close Chunk";
            this._btn_closeChunk.UseVisualStyleBackColor = true;
            this._btn_closeChunk.Click += new System.EventHandler(this.BtnCloseChunkClick);
            //
            // _btn_closeAllChunks
            //
            this._btn_closeAllChunks.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btn_closeAllChunks.Location = new System.Drawing.Point(445, 3);
            this._btn_closeAllChunks.Name = "_btn_closeAllChunks";
            this._btn_closeAllChunks.Size = new System.Drawing.Size(97, 23);
            this._btn_closeAllChunks.TabIndex = 3;
            this._btn_closeAllChunks.Text = "Close All Chunks";
            this._btn_closeAllChunks.UseVisualStyleBackColor = true;
            this._btn_closeAllChunks.Click += new System.EventHandler(this.BtnCloseAllChunksClick);
            //
            // _pnl_chunkHandler
            //
            this._pnl_chunkHandler.Controls.Add(this._tbc_chunkHandlers);
            this._pnl_chunkHandler.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnl_chunkHandler.Location = new System.Drawing.Point(3, 3);
            this._pnl_chunkHandler.Name = "_pnl_chunkHandler";
            this._pnl_chunkHandler.Size = new System.Drawing.Size(675, 659);
            this._pnl_chunkHandler.TabIndex = 1;
            //
            // _tbc_chunkHandlers
            //
            this._tbc_chunkHandlers.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbc_chunkHandlers.Location = new System.Drawing.Point(0, 0);
            this._tbc_chunkHandlers.Margin = new System.Windows.Forms.Padding(0);
            this._tbc_chunkHandlers.Name = "_tbc_chunkHandlers";
            this._tbc_chunkHandlers.SelectedIndex = 0;
            this._tbc_chunkHandlers.Size = new System.Drawing.Size(675, 659);
            this._tbc_chunkHandlers.TabIndex = 0;
            //
            // RelicChunkyViewer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.splitContainer1);
            this.Name = "RelicChunkyViewer";
            this.Size = new System.Drawing.Size(1040, 700);
            this.tableLayoutPanel1.ResumeLayout(false);
            this._cms_chunkTree.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this._pnl_chunkHandler.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Vom Komponenten-Designer generierter Code

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView _trv_chunks;
        private ChunkyHeaderInfo _chi_info;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button _btn_save;
        private System.Windows.Forms.Panel _pnl_chunkHandler;
        private System.Windows.Forms.Button _btn_showInfo;
        private System.Windows.Forms.ContextMenuStrip _cms_chunkTree;
        private System.Windows.Forms.ToolStripMenuItem copyChunkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertChunkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteChunkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutChunkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertAtRootLevelToolStripMenuItem;
        private System.Windows.Forms.TabControl _tbc_chunkHandlers;
        private System.Windows.Forms.Button _btn_closeChunk;
        private System.Windows.Forms.Button _btn_closeAllChunks;
        
    }
}