using System;
using System.Windows.Forms;
using cope.IO;
using ModTool.Core.PlugIns;

namespace ModTool.FE
{
    partial class MainDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.m_mesMain = new System.Windows.Forms.MenuStrip();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDoW2ModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRBFStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openSGAStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUCSStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileTypeManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDoW2LogDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ucsEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleDirectoryViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plugInsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_imlDirectoryView = new System.Windows.Forms.ImageList(this.components);
            this.m_dlgOpenMod = new System.Windows.Forms.OpenFileDialog();
            this._dlg_folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.m_mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_tbcTools = new System.Windows.Forms.TabControl();
            this.m_tpgCombinedView = new System.Windows.Forms.TabPage();
            this.m_fitCombined = new ModTool.Core.FileTreeControl();
            this.m_cmsFileTreeCombined = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCopyToolStripMenuItemCombined = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFileToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRelativePathToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFilelocalOnlyToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.openInExplorerToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.openAllFilesInFolderToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.openAsTextToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteFileToolStripMenuItem_combined = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tpgVirtualView = new System.Windows.Forms.TabPage();
            this.m_fitVirtual = new ModTool.Core.FileTreeControl();
            this.m_cmsFileTreeVirtual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractFileToolStripMenuItem_virtual = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFileToolStripMenuItem_virtual = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRelativePathToolStripMenuItem_virtual = new System.Windows.Forms.ToolStripMenuItem();
            this.openInExplorerToolStripMenuItem_virtual = new System.Windows.Forms.ToolStripMenuItem();
            this.openAllFilesInFolderToolStripMenuItem_virtual = new System.Windows.Forms.ToolStripMenuItem();
            this.openAsTextToolStripMenuItem_virtual = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpApps = new System.Windows.Forms.TableLayoutPanel();
            this.tlpButtonRow = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnCloseTab = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnCloseAllTabs = new System.Windows.Forms.Button();
            this.m_tbcApps = new cope.UI.TabControlExt();
            this.m_dlgOpenSGA = new System.Windows.Forms.OpenFileDialog();
            this.m_dlgOpenRBF = new System.Windows.Forms.OpenFileDialog();
            this.m_mesMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mainSplitContainer)).BeginInit();
            this.m_mainSplitContainer.Panel1.SuspendLayout();
            this.m_mainSplitContainer.Panel2.SuspendLayout();
            this.m_mainSplitContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_tbcTools.SuspendLayout();
            this.m_tpgCombinedView.SuspendLayout();
            this.m_cmsFileTreeCombined.SuspendLayout();
            this.m_tpgVirtualView.SuspendLayout();
            this.m_cmsFileTreeVirtual.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpApps.SuspendLayout();
            this.tlpButtonRow.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_mesMain
            // 
            this.m_mesMain.BackColor = System.Drawing.SystemColors.Control;
            this.m_mesMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.plugInsToolStripMenuItem});
            this.m_mesMain.Location = new System.Drawing.Point(0, 0);
            this.m_mesMain.Name = "m_mesMain";
            this.m_mesMain.Size = new System.Drawing.Size(1008, 24);
            this.m_mesMain.TabIndex = 0;
            this.m_mesMain.Text = "menuStrip1";
            this.m_mesMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbcAppsKeyDown);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDoW2ModToolStripMenuItem,
            this.openRBFStripMenuItem1,
            this.openSGAStripMenuItem1,
            this.openModToolStripMenuItem,
            this.closeModToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // newDoW2ModToolStripMenuItem
            // 
            this.newDoW2ModToolStripMenuItem.Image = global::ModTool.FE.Properties.Resources.document_new;
            this.newDoW2ModToolStripMenuItem.Name = "newDoW2ModToolStripMenuItem";
            this.newDoW2ModToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newDoW2ModToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.newDoW2ModToolStripMenuItem.Text = "New DoW2 Mod";
            this.newDoW2ModToolStripMenuItem.Click += new System.EventHandler(this.NewDoW2ModToolStripMenuItemClick);
            // 
            // openRBFStripMenuItem1
            // 
            this.openRBFStripMenuItem1.Name = "openRBFStripMenuItem1";
            this.openRBFStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.openRBFStripMenuItem1.Size = new System.Drawing.Size(204, 22);
            this.openRBFStripMenuItem1.Text = "Open RBF";
            this.openRBFStripMenuItem1.Click += new System.EventHandler(this.OpenRBFStripMenuItem1Click);
            // 
            // openSGAStripMenuItem1
            // 
            this.openSGAStripMenuItem1.Name = "openSGAStripMenuItem1";
            this.openSGAStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.openSGAStripMenuItem1.Size = new System.Drawing.Size(204, 22);
            this.openSGAStripMenuItem1.Text = "Open SGA";
            this.openSGAStripMenuItem1.Click += new System.EventHandler(this.OpenSGAStripMenuItem1Click);
            // 
            // openModToolStripMenuItem
            // 
            this.openModToolStripMenuItem.Image = global::ModTool.FE.Properties.Resources.document_open;
            this.openModToolStripMenuItem.Name = "openModToolStripMenuItem";
            this.openModToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openModToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.openModToolStripMenuItem.Text = "Open Mod";
            this.openModToolStripMenuItem.Click += new System.EventHandler(this.OpenModToolStripMenuItemClick);
            // 
            // closeModToolStripMenuItem
            // 
            this.closeModToolStripMenuItem.Name = "closeModToolStripMenuItem";
            this.closeModToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.closeModToolStripMenuItem.Text = "Close SGA / Mod";
            this.closeModToolStripMenuItem.Click += new System.EventHandler(this.CloseModToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::ModTool.FE.Properties.Resources.application_exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.addUCSStringToolStripMenuItem,
            this.fileTypeManagerToolStripMenuItem,
            this.modSettingsToolStripMenuItem,
            this.openDoW2LogDirectoryToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.releaseModToolStripMenuItem,
            this.testModToolStripMenuItem,
            this.ucsEditorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // addUCSStringToolStripMenuItem
            // 
            this.addUCSStringToolStripMenuItem.Enabled = false;
            this.addUCSStringToolStripMenuItem.Name = "addUCSStringToolStripMenuItem";
            this.addUCSStringToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.addUCSStringToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.addUCSStringToolStripMenuItem.Text = "Add UCS-String";
            this.addUCSStringToolStripMenuItem.Click += new System.EventHandler(this.AddUCSStringToolStripMenuItemClick);
            // 
            // fileTypeManagerToolStripMenuItem
            // 
            this.fileTypeManagerToolStripMenuItem.Name = "fileTypeManagerToolStripMenuItem";
            this.fileTypeManagerToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.fileTypeManagerToolStripMenuItem.Text = "File Type Manager";
            this.fileTypeManagerToolStripMenuItem.Click += new System.EventHandler(this.FileTypeManagerToolStripMenuItemClick);
            // 
            // modSettingsToolStripMenuItem
            // 
            this.modSettingsToolStripMenuItem.Enabled = false;
            this.modSettingsToolStripMenuItem.Name = "modSettingsToolStripMenuItem";
            this.modSettingsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.modSettingsToolStripMenuItem.Text = "Mod Settings";
            this.modSettingsToolStripMenuItem.Click += new System.EventHandler(this.ModSettingsToolStripMenuItemClick);
            // 
            // openDoW2LogDirectoryToolStripMenuItem
            // 
            this.openDoW2LogDirectoryToolStripMenuItem.Name = "openDoW2LogDirectoryToolStripMenuItem";
            this.openDoW2LogDirectoryToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openDoW2LogDirectoryToolStripMenuItem.Text = "Open DoW2 Log Directory";
            this.openDoW2LogDirectoryToolStripMenuItem.Click += new System.EventHandler(this.OpenDoW2LogDirectoryToolStripMenuItemClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::ModTool.FE.Properties.Resources.applications_settings;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
            // 
            // releaseModToolStripMenuItem
            // 
            this.releaseModToolStripMenuItem.Enabled = false;
            this.releaseModToolStripMenuItem.Name = "releaseModToolStripMenuItem";
            this.releaseModToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.releaseModToolStripMenuItem.Text = "Release Mod";
            this.releaseModToolStripMenuItem.Click += new System.EventHandler(this.ReleaseModToolStripMenuItemClick);
            // 
            // testModToolStripMenuItem
            // 
            this.testModToolStripMenuItem.Enabled = false;
            this.testModToolStripMenuItem.Name = "testModToolStripMenuItem";
            this.testModToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.testModToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.testModToolStripMenuItem.Text = "Test Mod";
            this.testModToolStripMenuItem.Click += new System.EventHandler(this.TestModToolStripMenuItemClick);
            // 
            // ucsEditorToolStripMenuItem
            // 
            this.ucsEditorToolStripMenuItem.Enabled = false;
            this.ucsEditorToolStripMenuItem.Name = "ucsEditorToolStripMenuItem";
            this.ucsEditorToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ucsEditorToolStripMenuItem.Text = "UCS Editor";
            this.ucsEditorToolStripMenuItem.Click += new System.EventHandler(this.UCSEditorToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleDirectoryViewToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toggleDirectoryViewToolStripMenuItem
            // 
            this.toggleDirectoryViewToolStripMenuItem.Name = "toggleDirectoryViewToolStripMenuItem";
            this.toggleDirectoryViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)));
            this.toggleDirectoryViewToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.toggleDirectoryViewToolStripMenuItem.Text = "Toggle Directory View";
            this.toggleDirectoryViewToolStripMenuItem.Click += new System.EventHandler(this.ToggleDirectoryViewToolStripMenuItemClick);
            // 
            // plugInsToolStripMenuItem
            // 
            this.plugInsToolStripMenuItem.Name = "plugInsToolStripMenuItem";
            this.plugInsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.plugInsToolStripMenuItem.Text = "PlugIns";
            // 
            // m_imlDirectoryView
            // 
            this.m_imlDirectoryView.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.m_imlDirectoryView.ImageSize = new System.Drawing.Size(16, 16);
            this.m_imlDirectoryView.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // m_dlgOpenMod
            // 
            this.m_dlgOpenMod.Filter = "Module file|*.module";
            this.m_dlgOpenMod.Title = "Open DoW2 Mod";
            // 
            // m_mainSplitContainer
            // 
            this.m_mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_mainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.m_mainSplitContainer.Name = "m_mainSplitContainer";
            // 
            // m_mainSplitContainer.Panel1
            // 
            this.m_mainSplitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // m_mainSplitContainer.Panel2
            // 
            this.m_mainSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.m_mainSplitContainer.Panel2.Controls.Add(this.tlpMain);
            this.m_mainSplitContainer.Size = new System.Drawing.Size(1008, 618);
            this.m_mainSplitContainer.SplitterDistance = 259;
            this.m_mainSplitContainer.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.m_tbcTools, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(259, 618);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_tbcTools
            // 
            this.m_tbcTools.Controls.Add(this.m_tpgCombinedView);
            this.m_tbcTools.Controls.Add(this.m_tpgVirtualView);
            this.m_tbcTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbcTools.Location = new System.Drawing.Point(3, 3);
            this.m_tbcTools.Name = "m_tbcTools";
            this.m_tbcTools.SelectedIndex = 0;
            this.m_tbcTools.Size = new System.Drawing.Size(253, 612);
            this.m_tbcTools.TabIndex = 5;
            // 
            // m_tpgCombinedView
            // 
            this.m_tpgCombinedView.Controls.Add(this.m_fitCombined);
            this.m_tpgCombinedView.Location = new System.Drawing.Point(4, 22);
            this.m_tpgCombinedView.Name = "m_tpgCombinedView";
            this.m_tpgCombinedView.Size = new System.Drawing.Size(245, 586);
            this.m_tpgCombinedView.TabIndex = 0;
            this.m_tpgCombinedView.Text = "Combined";
            this.m_tpgCombinedView.UseVisualStyleBackColor = true;
            // 
            // m_fitCombined
            // 
            this.m_fitCombined.ColorLocalDirectories = true;
            this.m_fitCombined.ColorLocalFiles = true;
            this.m_fitCombined.ContextMenuStrip = this.m_cmsFileTreeCombined;
            this.m_fitCombined.DirectoryExpandedImageIndex = 1;
            this.m_fitCombined.DirectoryImageIndex = 0;
            this.m_fitCombined.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_fitCombined.FileImageIndex = 1;
            this.m_fitCombined.ImageList = null;
            this.m_fitCombined.Location = new System.Drawing.Point(0, 0);
            this.m_fitCombined.Margin = new System.Windows.Forms.Padding(0);
            this.m_fitCombined.Name = "m_fitCombined";
            this.m_fitCombined.NoLocalFiles = false;
            this.m_fitCombined.NoRefresh = false;
            this.m_fitCombined.NoVirtualFiles = false;
            this.m_fitCombined.Size = new System.Drawing.Size(245, 586);
            this.m_fitCombined.TabIndex = 0;
            this.m_fitCombined.UseImages = true;
            // 
            // m_cmsFileTreeCombined
            // 
            this.m_cmsFileTreeCombined.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_cmsFileTreeCombined.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFolderToolStripMenuItem,
            this.createCopyToolStripMenuItemCombined,
            this.extractToolStripMenuItem_combined,
            this.copyFileToolStripMenuItem_combined,
            this.copyRelativePathToolStripMenuItem_combined,
            this.deleteFilelocalOnlyToolStripMenuItem_combined,
            this.openInExplorerToolStripMenuItem_combined,
            this.openAllFilesInFolderToolStripMenuItem_combined,
            this.openAsTextToolStripMenuItem_combined,
            this.renameToolStripMenuItem_combined,
            this.pasteFileToolStripMenuItem_combined});
            this.m_cmsFileTreeCombined.Name = "cms_directory_view";
            this.m_cmsFileTreeCombined.ShowImageMargin = false;
            this.m_cmsFileTreeCombined.Size = new System.Drawing.Size(223, 268);
            // 
            // createFolderToolStripMenuItem
            // 
            this.createFolderToolStripMenuItem.Name = "createFolderToolStripMenuItem";
            this.createFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.createFolderToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.createFolderToolStripMenuItem.Text = "Create Folder";
            this.createFolderToolStripMenuItem.Click += new System.EventHandler(this.CreateFolderToolStripMenuItemCombinedClick);
            // 
            // createCopyToolStripMenuItemCombined
            // 
            this.createCopyToolStripMenuItemCombined.Name = "createCopyToolStripMenuItemCombined";
            this.createCopyToolStripMenuItemCombined.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.createCopyToolStripMenuItemCombined.Size = new System.Drawing.Size(222, 22);
            this.createCopyToolStripMenuItemCombined.Text = "Create Copy";
            this.createCopyToolStripMenuItemCombined.Click += new System.EventHandler(this.CreateCopyToolStripMenuItemCombinedClick);
            // 
            // extractToolStripMenuItem_combined
            // 
            this.extractToolStripMenuItem_combined.Name = "extractToolStripMenuItem_combined";
            this.extractToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.extractToolStripMenuItem_combined.Text = "Extract";
            this.extractToolStripMenuItem_combined.Click += new System.EventHandler(this.ExtractToolStripMenuItemCombinedClick);
            // 
            // copyFileToolStripMenuItem_combined
            // 
            this.copyFileToolStripMenuItem_combined.Name = "copyFileToolStripMenuItem_combined";
            this.copyFileToolStripMenuItem_combined.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyFileToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.copyFileToolStripMenuItem_combined.Text = "Copy File";
            this.copyFileToolStripMenuItem_combined.Click += new System.EventHandler(this.CopyToolStripMenuItemCombinedClick);
            // 
            // copyRelativePathToolStripMenuItem_combined
            // 
            this.copyRelativePathToolStripMenuItem_combined.Name = "copyRelativePathToolStripMenuItem_combined";
            this.copyRelativePathToolStripMenuItem_combined.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.copyRelativePathToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.copyRelativePathToolStripMenuItem_combined.Text = "Copy Relative Path";
            this.copyRelativePathToolStripMenuItem_combined.Click += new System.EventHandler(this.CopyRelativePathToolStripMenuItemCombinedClick);
            // 
            // deleteFilelocalOnlyToolStripMenuItem_combined
            // 
            this.deleteFilelocalOnlyToolStripMenuItem_combined.Name = "deleteFilelocalOnlyToolStripMenuItem_combined";
            this.deleteFilelocalOnlyToolStripMenuItem_combined.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteFilelocalOnlyToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.deleteFilelocalOnlyToolStripMenuItem_combined.Text = "Delete File (local only)";
            this.deleteFilelocalOnlyToolStripMenuItem_combined.Click += new System.EventHandler(this.DeleteFilelocalOnlyToolStripMenuItemCombinedClick);
            // 
            // openInExplorerToolStripMenuItem_combined
            // 
            this.openInExplorerToolStripMenuItem_combined.Name = "openInExplorerToolStripMenuItem_combined";
            this.openInExplorerToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.openInExplorerToolStripMenuItem_combined.Text = "Open in Explorer";
            this.openInExplorerToolStripMenuItem_combined.Click += new System.EventHandler(this.OpenInExplorerToolStripMenuItemCombinedClick);
            // 
            // openAllFilesInFolderToolStripMenuItem_combined
            // 
            this.openAllFilesInFolderToolStripMenuItem_combined.Name = "openAllFilesInFolderToolStripMenuItem_combined";
            this.openAllFilesInFolderToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.openAllFilesInFolderToolStripMenuItem_combined.Text = "Open all Files in Folder";
            this.openAllFilesInFolderToolStripMenuItem_combined.Click += new System.EventHandler(this.OpenAllFilesInFolderToolStripMenuItemCombinedClick);
            // 
            // openAsTextToolStripMenuItem_combined
            // 
            this.openAsTextToolStripMenuItem_combined.Name = "openAsTextToolStripMenuItem_combined";
            this.openAsTextToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.openAsTextToolStripMenuItem_combined.Text = "Open as Text";
            this.openAsTextToolStripMenuItem_combined.Click += new System.EventHandler(this.OpenAsTextToolStripMenuItemCombinedClick);
            // 
            // renameToolStripMenuItem_combined
            // 
            this.renameToolStripMenuItem_combined.Name = "renameToolStripMenuItem_combined";
            this.renameToolStripMenuItem_combined.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.renameToolStripMenuItem_combined.Text = "Rename (local only)";
            this.renameToolStripMenuItem_combined.Click += new System.EventHandler(this.RenameToolStripMenuItemCombinedClick);
            // 
            // pasteFileToolStripMenuItem_combined
            // 
            this.pasteFileToolStripMenuItem_combined.Name = "pasteFileToolStripMenuItem_combined";
            this.pasteFileToolStripMenuItem_combined.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteFileToolStripMenuItem_combined.Size = new System.Drawing.Size(222, 22);
            this.pasteFileToolStripMenuItem_combined.Text = "Paste File";
            this.pasteFileToolStripMenuItem_combined.Click += new System.EventHandler(this.PasteToolStripMenuItemCombinedClick);
            // 
            // m_tpgVirtualView
            // 
            this.m_tpgVirtualView.Controls.Add(this.m_fitVirtual);
            this.m_tpgVirtualView.Location = new System.Drawing.Point(4, 22);
            this.m_tpgVirtualView.Name = "m_tpgVirtualView";
            this.m_tpgVirtualView.Size = new System.Drawing.Size(245, 586);
            this.m_tpgVirtualView.TabIndex = 1;
            this.m_tpgVirtualView.Text = "Virtual";
            this.m_tpgVirtualView.UseVisualStyleBackColor = true;
            // 
            // m_fitVirtual
            // 
            this.m_fitVirtual.ColorLocalDirectories = true;
            this.m_fitVirtual.ColorLocalFiles = true;
            this.m_fitVirtual.ContextMenuStrip = this.m_cmsFileTreeVirtual;
            this.m_fitVirtual.DirectoryExpandedImageIndex = 1;
            this.m_fitVirtual.DirectoryImageIndex = 0;
            this.m_fitVirtual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_fitVirtual.FileImageIndex = 1;
            this.m_fitVirtual.ImageList = null;
            this.m_fitVirtual.Location = new System.Drawing.Point(0, 0);
            this.m_fitVirtual.Margin = new System.Windows.Forms.Padding(0);
            this.m_fitVirtual.Name = "m_fitVirtual";
            this.m_fitVirtual.NoLocalFiles = true;
            this.m_fitVirtual.NoRefresh = false;
            this.m_fitVirtual.NoVirtualFiles = false;
            this.m_fitVirtual.Size = new System.Drawing.Size(245, 586);
            this.m_fitVirtual.TabIndex = 0;
            this.m_fitVirtual.UseImages = true;
            // 
            // m_cmsFileTreeVirtual
            // 
            this.m_cmsFileTreeVirtual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_cmsFileTreeVirtual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractFileToolStripMenuItem_virtual,
            this.copyFileToolStripMenuItem_virtual,
            this.copyRelativePathToolStripMenuItem_virtual,
            this.openInExplorerToolStripMenuItem_virtual,
            this.openAllFilesInFolderToolStripMenuItem_virtual,
            this.openAsTextToolStripMenuItem_virtual});
            this.m_cmsFileTreeVirtual.Name = "cms_directory_view";
            this.m_cmsFileTreeVirtual.ShowImageMargin = false;
            this.m_cmsFileTreeVirtual.Size = new System.Drawing.Size(223, 136);
            // 
            // extractFileToolStripMenuItem_virtual
            // 
            this.extractFileToolStripMenuItem_virtual.Name = "extractFileToolStripMenuItem_virtual";
            this.extractFileToolStripMenuItem_virtual.Size = new System.Drawing.Size(222, 22);
            this.extractFileToolStripMenuItem_virtual.Text = "Extract";
            this.extractFileToolStripMenuItem_virtual.ToolTipText = "Use this on any node to extract that file / directory.";
            this.extractFileToolStripMenuItem_virtual.Click += new System.EventHandler(this.ExtractFileToolStripMenuItemVirtualClick);
            // 
            // copyFileToolStripMenuItem_virtual
            // 
            this.copyFileToolStripMenuItem_virtual.Name = "copyFileToolStripMenuItem_virtual";
            this.copyFileToolStripMenuItem_virtual.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyFileToolStripMenuItem_virtual.Size = new System.Drawing.Size(222, 22);
            this.copyFileToolStripMenuItem_virtual.Text = "Copy File";
            this.copyFileToolStripMenuItem_virtual.Click += new System.EventHandler(this.CopyFileToolStripMenuItemVirtualClick);
            // 
            // copyRelativePathToolStripMenuItem_virtual
            // 
            this.copyRelativePathToolStripMenuItem_virtual.Name = "copyRelativePathToolStripMenuItem_virtual";
            this.copyRelativePathToolStripMenuItem_virtual.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.copyRelativePathToolStripMenuItem_virtual.Size = new System.Drawing.Size(222, 22);
            this.copyRelativePathToolStripMenuItem_virtual.Text = "Copy Relative Path";
            this.copyRelativePathToolStripMenuItem_virtual.Click += new System.EventHandler(this.CopyRelativePathToolStripMenuItemVirtualClick);
            // 
            // openInExplorerToolStripMenuItem_virtual
            // 
            this.openInExplorerToolStripMenuItem_virtual.Name = "openInExplorerToolStripMenuItem_virtual";
            this.openInExplorerToolStripMenuItem_virtual.Size = new System.Drawing.Size(222, 22);
            this.openInExplorerToolStripMenuItem_virtual.Text = "Open in Explorer";
            this.openInExplorerToolStripMenuItem_virtual.ToolTipText = "Use this on any file- or directory node to view it in the explorer.";
            this.openInExplorerToolStripMenuItem_virtual.Click += new System.EventHandler(this.OpenInExplorerToolStripMenuItemVirtualClick);
            // 
            // openAllFilesInFolderToolStripMenuItem_virtual
            // 
            this.openAllFilesInFolderToolStripMenuItem_virtual.Name = "openAllFilesInFolderToolStripMenuItem_virtual";
            this.openAllFilesInFolderToolStripMenuItem_virtual.Size = new System.Drawing.Size(222, 22);
            this.openAllFilesInFolderToolStripMenuItem_virtual.Text = "Open all Files in Selected Folder";
            this.openAllFilesInFolderToolStripMenuItem_virtual.Click += new System.EventHandler(this.OpenAllFilesInFolderToolStripMenuItemVirtualClick);
            // 
            // openAsTextToolStripMenuItem_virtual
            // 
            this.openAsTextToolStripMenuItem_virtual.Name = "openAsTextToolStripMenuItem_virtual";
            this.openAsTextToolStripMenuItem_virtual.Size = new System.Drawing.Size(222, 22);
            this.openAsTextToolStripMenuItem_virtual.Text = "Open as Text";
            this.openAsTextToolStripMenuItem_virtual.Click += new System.EventHandler(this.OpenAsTextToolStripMenuItemVirtualClick);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Controls.Add(this.tlpApps, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 593F));
            this.tlpMain.Size = new System.Drawing.Size(745, 618);
            this.tlpMain.TabIndex = 0;
            // 
            // tlpApps
            // 
            this.tlpApps.ColumnCount = 1;
            this.tlpApps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpApps.Controls.Add(this.tlpButtonRow, 0, 1);
            this.tlpApps.Controls.Add(this.m_tbcApps, 0, 0);
            this.tlpApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpApps.Location = new System.Drawing.Point(3, 3);
            this.tlpApps.Name = "tlpApps";
            this.tlpApps.RowCount = 2;
            this.tlpApps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpApps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpApps.Size = new System.Drawing.Size(739, 612);
            this.tlpApps.TabIndex = 0;
            // 
            // tlpButtonRow
            // 
            this.tlpButtonRow.ColumnCount = 3;
            this.tlpButtonRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtonRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tlpButtonRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tlpButtonRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpButtonRow.Controls.Add(this.panel1, 2, 0);
            this.tlpButtonRow.Controls.Add(this.panel2, 1, 0);
            this.tlpButtonRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtonRow.Location = new System.Drawing.Point(3, 579);
            this.tlpButtonRow.Name = "tlpButtonRow";
            this.tlpButtonRow.RowCount = 1;
            this.tlpButtonRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtonRow.Size = new System.Drawing.Size(733, 30);
            this.tlpButtonRow.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnCloseTab);
            this.panel1.Location = new System.Drawing.Point(658, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(72, 24);
            this.panel1.TabIndex = 0;
            // 
            // m_btnCloseTab
            // 
            this.m_btnCloseTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btnCloseTab.Location = new System.Drawing.Point(0, 0);
            this.m_btnCloseTab.Name = "m_btnCloseTab";
            this.m_btnCloseTab.Size = new System.Drawing.Size(72, 24);
            this.m_btnCloseTab.TabIndex = 0;
            this.m_btnCloseTab.Text = "Close Tab";
            this.m_btnCloseTab.UseVisualStyleBackColor = true;
            this.m_btnCloseTab.Click += new System.EventHandler(this.BtnCloseTabClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_btnCloseAllTabs);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(562, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(90, 24);
            this.panel2.TabIndex = 1;
            // 
            // m_btnCloseAllTabs
            // 
            this.m_btnCloseAllTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btnCloseAllTabs.Location = new System.Drawing.Point(0, 0);
            this.m_btnCloseAllTabs.Name = "m_btnCloseAllTabs";
            this.m_btnCloseAllTabs.Size = new System.Drawing.Size(90, 24);
            this.m_btnCloseAllTabs.TabIndex = 0;
            this.m_btnCloseAllTabs.Text = "Close All Tabs";
            this.m_btnCloseAllTabs.UseVisualStyleBackColor = true;
            this.m_btnCloseAllTabs.Click += new System.EventHandler(this.BtnCloseAllTabsClick);
            // 
            // m_tbcApps
            // 
            this.m_tbcApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbcApps.Location = new System.Drawing.Point(0, 0);
            this.m_tbcApps.Margin = new System.Windows.Forms.Padding(0);
            this.m_tbcApps.Name = "m_tbcApps";
            this.m_tbcApps.SelectedIndex = 0;
            this.m_tbcApps.Size = new System.Drawing.Size(739, 576);
            this.m_tbcApps.TabIndex = 0;
            this.m_tbcApps.TabHeaderClicked += new cope.UI.TabHeaderClickedHandler(this.TbcAppsHeaderClicked);
            this.m_tbcApps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbcAppsKeyDown);
            // 
            // m_dlgOpenSGA
            // 
            this.m_dlgOpenSGA.Filter = "SGA Archive|*.SGA";
            // 
            // m_dlgOpenRBF
            // 
            this.m_dlgOpenRBF.Filter = "Relic Binary File|*.RBF";
            // 
            // MainDialog
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 642);
            this.Controls.Add(this.m_mainSplitContainer);
            this.Controls.Add(this.m_mesMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_mesMain;
            this.MinimumSize = new System.Drawing.Size(462, 100);
            this.Name = "MainDialog";
            this.Text = "Cope\'s DoW2 Toolbox";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainDialogDragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainDialogDragOver);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbcAppsKeyDown);
            this.m_mesMain.ResumeLayout(false);
            this.m_mesMain.PerformLayout();
            this.m_mainSplitContainer.Panel1.ResumeLayout(false);
            this.m_mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_mainSplitContainer)).EndInit();
            this.m_mainSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_tbcTools.ResumeLayout(false);
            this.m_tpgCombinedView.ResumeLayout(false);
            this.m_cmsFileTreeCombined.ResumeLayout(false);
            this.m_tpgVirtualView.ResumeLayout(false);
            this.m_cmsFileTreeVirtual.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpApps.ResumeLayout(false);
            this.tlpButtonRow.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Vom Windows Form-Designer generierter Code

        private System.Windows.Forms.MenuStrip m_mesMain;
        private System.Windows.Forms.ImageList m_imlDirectoryView;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog m_dlgOpenMod;
        private System.Windows.Forms.FolderBrowserDialog _dlg_folderBrowser;
        private System.Windows.Forms.SplitContainer m_mainSplitContainer;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpApps;
        private System.Windows.Forms.TableLayoutPanel tlpButtonRow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnCloseTab;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button m_btnCloseAllTabs;
        private System.Windows.Forms.ContextMenuStrip m_cmsFileTreeVirtual;
        private System.Windows.Forms.ToolStripMenuItem extractFileToolStripMenuItem_virtual;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem_virtual;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRBFStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openSGAStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog m_dlgOpenSGA;
        private System.Windows.Forms.OpenFileDialog m_dlgOpenRBF;
        private System.Windows.Forms.ToolStripMenuItem fileTypeManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDoW2ModToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private cope.UI.TabControlExt m_tbcApps;
        private System.Windows.Forms.TabControl m_tbcTools;
        private System.Windows.Forms.TabPage m_tpgCombinedView;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private Core.FileTreeControl m_fitCombined;
        private System.Windows.Forms.TabPage m_tpgVirtualView;
        private Core.FileTreeControl m_fitVirtual;
        private System.Windows.Forms.ContextMenuStrip m_cmsFileTreeCombined;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem testModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAllFilesInFolderToolStripMenuItem_virtual;
        private System.Windows.Forms.ToolStripMenuItem openAllFilesInFolderToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleDirectoryViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAsTextToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem openAsTextToolStripMenuItem_virtual;
        private System.Windows.Forms.ToolStripMenuItem copyRelativePathToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem copyRelativePathToolStripMenuItem_virtual;
        private System.Windows.Forms.ToolStripMenuItem addUCSStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDoW2LogDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plugInsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFilelocalOnlyToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem copyFileToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem pasteFileToolStripMenuItem_combined;
        private System.Windows.Forms.ToolStripMenuItem copyFileToolStripMenuItem_virtual;
        private ToolStripMenuItem createFolderToolStripMenuItem;
        private ToolStripMenuItem createCopyToolStripMenuItemCombined;
        private ToolStripMenuItem ucsEditorToolStripMenuItem;
    }
}