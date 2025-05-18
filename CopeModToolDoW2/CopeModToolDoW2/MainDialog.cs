/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using cope;
using cope.DawnOfWar2;
using cope.Extensions;
using ModTool.Core;
using ModTool.Core.PlugIns;
using ModTool.FE.Properties;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ModTool.FE
{
    // Todo: rewrite for 2.0
    public partial class MainDialog : Form
    {
        #region fields

        private const string DEFAULT_TITLE = "Cope's DoW2 Toolbox";

        private FSNode m_fileTreeClipboard;
        private LoadForm m_loadForm = new LoadForm();
        private UCSEditorForm m_ucsEditor;

        #endregion fields

        public MainDialog()
        {
            LoggingManager.SendMessage("FE - Starting front end...");
            InitializeComponent();

            ModToolEnvironment.PluginMenu = plugInsToolStripMenuItem.DropDownItems;
            ModToolEnvironment.CombinedFileTreeContext = m_cmsFileTreeCombined.Items;
            ModToolEnvironment.VirtualFileTreeContext = m_cmsFileTreeVirtual.Items;

            m_imlDirectoryView.Images.Add(Resources.folder);
            m_imlDirectoryView.Images.Add(Resources.folder_open);
            m_imlDirectoryView.Images.Add(Resources.document);

            LoggingManager.SendMessage("FE - FileTree controls setup started");

            #region combined file tree

            //
            // COMBINED FILE TREE
            //

            m_fitCombined.ColorLocalDirectories = Settings.Default.bDirviewMarkChanged;
            m_fitCombined.UseImages = !Settings.Default.bDirViewHideIcons;
            m_fitCombined.ImageList = m_imlDirectoryView;
            m_fitCombined.DirectoryImageIndex = 0;
            m_fitCombined.DirectoryExpandedImageIndex = 1;
            m_fitCombined.FileImageIndex = 2;

            // events

            m_fitCombined.InnerTreeView.NodeMouseDoubleClick += CombinedInnerTreeViewNodeMouseDoubleClick;
            m_fitCombined.InnerTreeView.NodeMouseClick += CombinedInnerTreeViewNodeMouseClick;

            #endregion combined file tree

            #region virtual only file tree

            //
            // VIRTUAL ONLY FILE TREE
            //

            m_fitVirtual.UseImages = !Settings.Default.bDirViewHideIcons;
            m_fitVirtual.ImageList = m_imlDirectoryView;
            m_fitVirtual.DirectoryImageIndex = 0;
            m_fitVirtual.DirectoryExpandedImageIndex = 1;
            m_fitVirtual.FileImageIndex = 2;

            // events

            m_fitVirtual.InnerTreeView.NodeMouseDoubleClick += VirtualOnlyInnerTreeViewNodeMouseDoubleClick;
            m_fitVirtual.InnerTreeView.NodeMouseClick += VirtualOnlyInnerTreeViewNodeMouseClick;

            #endregion virtual only file tree

            LoggingManager.SendMessage("FE - FileTree controls setup finished");

            ModManager.ModLoaded += ModManagerModLoaded;
            ModManager.ModUnloaded += ModManagerModUnloaded;
            ModManager.SGALoaded += ModManagerSGALoaded;
            ModManager.LoadingFailed += ModManagerModLoadingFailed;
            FileManager.FileTreesChanged += FileManagerFileTreesChanged;
            FileManager.FileLoaded += FileManagerFileLoaded;

            LoggingManager.SendMessage("FE - Front end successfully started!");
        }

        internal void HandleArgs(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].EndsWith(".module"))
                {
                    ParameterizedThreadStart load = x => ModManager.LoadModule(x as string);
                    var loadThread = new Thread(load);
                    loadThread.Start(args[0]);
                    ShowLoadForm();
                }
            }
        }

        #region EventHandler

        #region general

        private void TbcAppsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode & Keys.W) == Keys.W)
            {
                if (m_tbcApps.SelectedTab != null)
                    CloseTab(m_tbcApps.SelectedTab);
            }
            else if (e.Control && (e.KeyCode & Keys.S) == Keys.S)
            {
                if (m_tbcApps.SelectedTab != null)
                {
                    foreach (Control c in m_tbcApps.SelectedTab.Controls)
                        if (c is FileTool)
                            (c as FileTool).SaveFile();
                }
            }
        }

        private void TbcAppsHeaderClicked(object sender, MouseEventArgs e, int tabIndex)
        {
            if (e.Button == MouseButtons.Middle)
            {
                TabPage page = m_tbcApps.TabPages[tabIndex];
                CloseTab(page);
            }
        }

        private void ModManagerModUnloaded()
        {
            Text = DEFAULT_TITLE;
            foreach (TabPage tp in m_tbcApps.TabPages)
            {
                foreach (Control c in tp.Controls)
                {
                    if (c is FileTool)
                    {
                        FileManager.CloseTool((FileTool) c);
                    }
                }
                tp.Dispose();
            }
            if (!m_fitVirtual.IsDisposed)
                m_fitVirtual.ClearTree();
            if (!m_fitCombined.IsDisposed)
                m_fitCombined.ClearTree();
            testModToolStripMenuItem.Enabled = false;
            releaseModToolStripMenuItem.Enabled = false;
            addUCSStringToolStripMenuItem.Enabled = false;
            modSettingsToolStripMenuItem.Enabled = false;
            ucsEditorToolStripMenuItem.Enabled = false;
        }

        private void ModManagerModLoaded()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(HandleModLoaded));
            else
                HandleModLoaded();
        }

        private void ModManagerModLoadingFailed()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(CloseLoadForm));
            else
                CloseLoadForm();
        }

        private void ModManagerSGALoaded()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(HandleSGALoaded));
            else
                HandleSGALoaded();
        }

        private void FileManagerFileTreesChanged()
        {
            m_fitCombined.ClearTree();
            m_fitVirtual.ClearTree();

            m_fitCombined.AddFileTree(FileManager.AttribTree, "ATTRIB");
            m_fitVirtual.AddFileTree(FileManager.AttribTree, "ATTRIB");

            m_fitCombined.AddFileTree(FileManager.DataTree, "DATA");
            m_fitVirtual.AddFileTree(FileManager.DataTree, "DATA");
        }

        private void FileManagerFileLoaded(UniFile file, FileTool plugin)
        {
            SetupNewTab(plugin, file.FileName);
        }

        #endregion general

        #region drag and drop

        private void MainDialogDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void MainDialogDragDrop(object sender, DragEventArgs e)
        {
            DragDropGeneral(e);
        }

        private static void DragDropGeneral(DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var fileNames = (string[]) e.Data.GetData(DataFormats.FileDrop);

                    foreach (string s in fileNames)
                    {
                        // only one SGA / module at a time (currently)!
                        if (s.EndsWith(".sga"))
                        {
                            if (ModManager.IsAnythingLoaded && UIHelper.ShowYNQuestion("Warning!",
                                                                    "Opening a SGA archive will close all other archives and tabs!")
                                == DialogResult.No)
                                return;
                            ModManager.LoadSGAFile(s);
                            return;
                        }
                        if (s.EndsWith(".module"))
                        {
                            if (ModManager.IsAnythingLoaded && UIHelper.ShowYNQuestion("Warning!",
                                                                    "Opening another module will close the current mod with all it's tabs and SGAs! Continue anyway?")
                                == DialogResult.No)
                                return;
                            ModManager.LoadModule(s);
                            return;
                        }
                        var tmp = new UniFile(s);
                        try
                        {
                            FileManager.LoadFile(tmp);
                        }
                        catch (Exception ex)
                        {
                            tmp.Close();
                             UIHelper.ShowError(ex.Message);
                            return;
                        }
                    }
                }
            }
        }

        #endregion drag and drop

        #region menues

        #region view menu

        private void ToggleDirectoryViewToolStripMenuItemClick(object sender, EventArgs e)
        {
            m_mainSplitContainer.Panel1Collapsed = !m_mainSplitContainer.Panel1Collapsed;
        }

        #endregion view menu

        #region tools menu

        private void AddUCSStringToolStripMenuItemClick(object sender, EventArgs e)
        {
            var ucsAdder = new UCSAdder();
            ucsAdder.ShowDialog();
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void FileTypeManagerToolStripMenuItemClick(object sender, EventArgs e)
        {
            var fileTypePluginManager = new FileTypePluginManager();
            fileTypePluginManager.ShowDialog();
            Settings.Default.sFileTypes = fileTypePluginManager.GetFiletypes();
        }

        private void ModSettingsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var modSettings = new ModSettingsForm();
            modSettings.ShowDialog();
        }

        private void OpenDoW2LogDirectoryToolStripMenuItemClick(object sender, EventArgs e)
        {
            MainManager.OpenLogfileDirectory();
        }

        private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var options = new OptionsDialog();
            if (options.ShowDialog() == DialogResult.OK)
            {
                m_fitCombined.NoRefresh = true;
                m_fitVirtual.NoRefresh = true;

                m_fitCombined.UseImages = !options.HideIcons;
                m_fitVirtual.UseImages = !options.HideIcons;
                m_fitCombined.ColorLocalDirectories = options.MarkChangedDirs;
                m_fitVirtual.ColorLocalDirectories = options.MarkChangedDirs;

                m_fitCombined.NoRefresh = false;
                m_fitVirtual.NoRefresh = false;
            }
        }

        private void ReleaseModToolStripMenuItemClick(object sender, EventArgs e)
        {
            var releaseMod = new ReleaseModDialog();
            releaseMod.ShowDialog();
        }

        private void TestModToolStripMenuItemClick(object sender, EventArgs e)
        {
            MainManager.TestMod();
        }

        private void UCSEditorToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (m_ucsEditor == null || m_ucsEditor.IsDisposed)
                m_ucsEditor = new UCSEditorForm();
            m_ucsEditor.Show();
        }

        #endregion tools menu

        #region main menu

        private void NewDoW2ModToolStripMenuItemClick(object sender, EventArgs e)
        {
            var dlgNewMod = new NewModDialog(ModManager.GameDirectory);
            if (dlgNewMod.ShowDialog() != DialogResult.OK)
                return;

            ModCreator creator = new ModCreator(dlgNewMod.BaseModule, dlgNewMod.ModName, dlgNewMod.DisplayedModName,
                                                dlgNewMod.ModVersion, dlgNewMod.ModDescription, dlgNewMod.UCSBaseIndex,
                                                dlgNewMod.RepackAttribArchive);

            if (dlgNewMod.RepackAttribArchive)
            {
                ParameterizedThreadStart writeMod = x =>
                                                        {
                                                            creator.WriteMod();
                                                            ModManager.LoadModule(creator.ModulePath);
                                                        };
                var createThread = new Thread(writeMod);
                createThread.Start(null);
                ShowLoadForm();
                return;
            }

            try
            {
                creator.WriteMod();
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("ModCreator - Failed to create Mod");
                LoggingManager.HandleException(ex);
                 UIHelper.ShowError("Failed to create Mod. See logfile for more information.");
                return;
            }

            ParameterizedThreadStart load = x => ModManager.LoadModule(x as string);
            var loadThread = new Thread(load);
            loadThread.Start(creator.ModulePath);
            ShowLoadForm();
        }

        private void OpenModToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ModManager.IsAnythingLoaded && UIHelper.ShowYNQuestion("Warning!",
               "Opening another module will close the current mod with all it's tabs and SGAs! Continue anyway?")
                == DialogResult.No)
                return;

            if (Settings.Default.sLastPath != string.Empty && Directory.Exists(Settings.Default.sLastPath))
                m_dlgOpenMod.InitialDirectory = Settings.Default.sLastPath;
            else
                m_dlgOpenMod.InitialDirectory = ModManager.GameDirectory.Replace('/', '\\');
            if (m_dlgOpenMod.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.sLastPath = m_dlgOpenMod.FileName.SubstringBeforeLast('\\', true);
                ParameterizedThreadStart load = x => ModManager.LoadModule(x as string);
                var loadThread = new Thread(load);
                loadThread.Start(m_dlgOpenMod.FileName);
                ShowLoadForm();
            }
        }

        private void CloseModToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ModManager.IsAnythingLoaded && UIHelper.ShowYNQuestion("Warning!",
                  "Closing this module will close it's tabs and SGAs! Continue anyway?")
                == DialogResult.No)
            {
                return;
            }
            ModManager.CloseAll();
            m_fitCombined.ClearTree();
            m_fitVirtual.ClearTree();
            testModToolStripMenuItem.Enabled = false;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            foreach (TabPage tp in m_tbcApps.TabPages)
                CloseTab(tp);
            m_tbcApps.TabPages.Clear();
            ModManager.CloseAll();
            Close();
        }

        private void OpenRBFStripMenuItem1Click(object sender, EventArgs e)
        {
            if (m_dlgOpenRBF.ShowDialog() != DialogResult.OK)
                return;

            Settings.Default.sLastPath = m_dlgOpenRBF.FileName.SubstringBeforeLast('\\', true);
            FileManager.LoadFile(m_dlgOpenRBF.FileName);
        }

        private void OpenSGAStripMenuItem1Click(object sender, EventArgs e)
        {
            if (ModManager.IsAnythingLoaded && UIHelper.ShowYNQuestion("Warning!",
                "Opening a SGA archive will close all other archives! Your tabs will remain open though. Continue anyway?")
                == DialogResult.No)
            {
                return;
            }

            if (m_dlgOpenSGA.ShowDialog() != DialogResult.OK)
                return;

            Settings.Default.sLastPath = m_dlgOpenSGA.FileName.SubstringBeforeLast('\\', true);

            ParameterizedThreadStart load = x => ModManager.LoadSGAFile(x as string);
            var loadThread = new Thread(load);
            loadThread.Start(m_dlgOpenSGA.FileName);
            ShowLoadForm();
        }

        #endregion main menu

        #endregion menues

        #region tab system

        private static void PluginOnHasChangesChanged(object sender, bool hasChanges)
        {
            if (sender == null || !Settings.Default.bAppMarkChanged)
                return;
            // the control's parent is the tab it's assigned to
            Control tab = ((Control) sender).Parent;
            if (hasChanges && !tab.Text.EndsWith(" *"))
                tab.Text += @" *";
            else if (!hasChanges && tab.Text.EndsWith(" *"))
                tab.Text = tab.Text.Remove(tab.Text.Length - 2, 2);
        }

        private void BtnCloseAllTabsClick(object sender, EventArgs e)
        {
            foreach (TabPage tabPage in m_tbcApps.TabPages)
                CloseTab(tabPage);
        }

        private void BtnCloseTabClick(object sender, EventArgs e)
        {
            if (m_tbcApps.SelectedTab != null)
                CloseTab(m_tbcApps.SelectedTab);
            if (m_tbcApps.TabCount != 0)
                m_tbcApps.SelectedTab = m_tbcApps.TabPages[m_tbcApps.TabCount - 1];
        }

        #endregion tab system

        #region file trees

        #region combined file tree

        private void CombinedInnerTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                m_fitCombined.InnerTreeView.SelectedNode = e.Node;
        }

        private void CombinedInnerTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = m_fitCombined.InnerTreeView.SelectedNode;
            if (currentNode != null && e.Node == currentNode)
            {
                LoadFileFromTreeNode(currentNode);
            }
        }

        private void OpenAsTextToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            TreeNode tn = m_fitCombined.InnerTreeView.SelectedNode;
            if (tn != null)
            {
                LoadFileFromTreeNode(tn, forceText: true);
            }
        }

        private void CopyRelativePathToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            TreeNode currentNode = m_fitCombined.InnerTreeView.SelectedNode;
            if (currentNode != null)
            {
                FSNode fsNode = m_fitCombined.GetFSNodeByTreeNode(currentNode);
                if (fsNode == null)
                    return;
                Clipboard.SetText(fsNode.GetPathInTree());
            }
        }

        #endregion combined file tree

        #region virtual only file tree

        private void VirtualOnlyInnerTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                m_fitVirtual.InnerTreeView.SelectedNode = e.Node;
        }

        private void VirtualOnlyInnerTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = m_fitVirtual.InnerTreeView.SelectedNode;
            if (currentNode != null && e.Node == currentNode)
            {
                LoadFileFromTreeNode(currentNode, true);
            }
        }

        private void OpenAsTextToolStripMenuItemVirtualClick(object sender, EventArgs e)
        {
            TreeNode currentNode = m_fitVirtual.InnerTreeView.SelectedNode;
            if (currentNode != null)
            {
                LoadFileFromTreeNode(currentNode, true, true);
            }
        }

        private void CopyRelativePathToolStripMenuItemVirtualClick(object sender, EventArgs e)
        {
            TreeNode currentNode = m_fitVirtual.InnerTreeView.SelectedNode;
            if (currentNode != null)
            {
                FSNode fsNode = m_fitVirtual.GetFSNodeByTreeNode(currentNode);
                if (fsNode == null)
                    return;
                Clipboard.SetText(fsNode.GetPathInTree());
            }
        }

        #endregion virtual only file tree

        #region context menu strips

        #region combined file tree

        private void CopyToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            var node = m_fitCombined.GetSelectedNode() as FSNodeFile;
            if (node == null)
                return;
            m_fileTreeClipboard = node;
        }

        private void DeleteFilelocalOnlyToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            m_fitCombined.DeleteSelected();
        }

        private void ExtractToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            m_fitCombined.ExtractSelected();
        }

        private void OpenInExplorerToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            m_fitCombined.OpenSelectedInExplorer();
        }

        private void OpenAllFilesInFolderToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            TreeNode currentNode = m_fitCombined.InnerTreeView.SelectedNode;
            if (currentNode != null)
            {
                foreach (TreeNode tn in currentNode.Nodes)
                {
                    LoadFileFromTreeNode(tn);
                }
            }
        }

        private void PasteToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            if (m_fileTreeClipboard == null)
                return;
            var selected = m_fitCombined.GetSelectedNode() as FSNodeDir;
            if (selected == null)
                return;
            PasteNode(m_fileTreeClipboard, selected, m_fileTreeClipboard.Name);
        }

        private void RenameToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            TreeNode tn = m_fitCombined.InnerTreeView.SelectedNode;
            if (tn == null)
                return;
            var rf = new RenameFile {NewName = tn.Text};
            if (rf.ShowDialog() == DialogResult.Cancel)
                return;
            m_fitCombined.RenameSelected(rf.NewName);
        }

        private void CreateFolderToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            TreeNode tn = m_fitCombined.InnerTreeView.SelectedNode;
            if (tn == null)
                return;
            FSNode node = m_fitCombined.GetFSNodeByTreeNode(tn);
            if (node == null)
                return;
            var rf = new RenameFile("Create Folder");
            if (rf.ShowDialog() == DialogResult.Cancel)
                return;
            string directoryPath;
            if (node is FSNodeDir)
                directoryPath = node.GetPath() + '\\' + rf.NewName;
            else
                directoryPath = node.GetPath().SubstringBeforeLast('\\', true) + rf.NewName;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        private void CreateCopyToolStripMenuItemCombinedClick(object sender, EventArgs e)
        {
            var selected = m_fitCombined.GetSelectedNode() as FSNodeFile;
            if (selected == null)
                return;
            string name = selected.Name.SubstringBeforeLast('.') + "_copy." + selected.Name.SubstringAfterLast('.');
            var rf = new RenameFile("Name of copy") {NewName = name};
            if (rf.ShowDialog() == DialogResult.Cancel)
                return;
            name = rf.NewName;
            PasteNode(selected, selected.Parent, name);
        }

        #endregion combined file tree

        #region virtual only file tree

        private void CopyFileToolStripMenuItemVirtualClick(object sender, EventArgs e)
        {
            var node = m_fitVirtual.GetSelectedNode();// as FSNodeFile;
            if (node == null)
                return;
            m_fileTreeClipboard = node;
        }

        private void ExtractFileToolStripMenuItemVirtualClick(object sender, EventArgs e)
        {
            m_fitVirtual.ExtractSelected();
        }

        private void OpenInExplorerToolStripMenuItemVirtualClick(object sender, EventArgs e)
        {
            m_fitVirtual.OpenSelectedInExplorer();
        }

        private void OpenAllFilesInFolderToolStripMenuItemVirtualClick(object sender, EventArgs e)
        {
            TreeNode currentNode = m_fitCombined.InnerTreeView.SelectedNode;
            if (currentNode != null)
            {
                foreach (TreeNode tn in currentNode.Nodes)
                {
                    LoadFileFromTreeNode(tn);
                }
            }
        }

        #endregion virtual only file tree

        #endregion context menu strips

        #endregion file trees

        #endregion EventHandler

        #region Helpers

        #region UI

        private static void PasteNode(FSNode node, FSNodeDir into, string newName)
        {
            if (node == null || into == null)
                return;
            try
            {
                string dirPath = into.Path + '\\';

                if (File.Exists(dirPath + newName))
                {
                    string name = newName.SubstringBeforeLast('.');
                    string ext = newName.SubstringAfterLast('.', true);
                    int numOfThatKind = into.Directories.Count(d => d.Name.StartsWith(name + "_copy"));
                    numOfThatKind += into.Files.Count(d => d.Name.StartsWith(name + "_copy"));
                    if (numOfThatKind > 0)
                        PasteNode(node, into, name + "_copy_" + (numOfThatKind + 1) + ext);
                    else
                        PasteNode(node, into, name + "_copy" + ext);
                    return;
                }

                if (node is FSNodeVirtualFile)
                {
                    var virtualFile = node as FSNodeVirtualFile;
                    if (virtualFile.HasLocal)
                    {
                        if (!Directory.Exists(dirPath))
                            Directory.CreateDirectory(dirPath);
                        File.Copy(virtualFile.Path, dirPath + newName);
                    }
                    else
                        virtualFile.Extract(dirPath + newName);
                }
                else if (node is FSNodeFile)
                {
                    var file = node as FSNodeFile;
                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);
                    File.Copy(file.Path, dirPath + newName);
                }
                else if (node is FSNodeDir)
                {
                    var dir = node as FSNodeDir;
                    if (!Directory.Exists(dirPath + newName))
                        Directory.CreateDirectory(dirPath + newName);

                    into = into.GetDirectory(newName);
                    foreach (var d in dir.Directories)
                        PasteNode(d, into, d.Name);
                    foreach (var f in dir.Files)
                        PasteNode(f, into, f.Name);
                }
            }
            catch (Exception ex)
            {
                 UIHelper.ShowError("Failed to paste: " + ex.Message);
            }
        }

        private static void CloseTab(TabPage tp)
        {
            foreach (Control c in tp.Controls)
            {
                if (c is FileTool)
                {
                    if (!FileManager.CloseTool((FileTool) c))
                        break;
                }
            }
            tp.Dispose();
        }

        private void SetupNewTab(FileTool plugin, string tabName)
        {
            if (plugin == null)
                return;
            var tabpTmp = new TabPage(tabName);
            tabpTmp.Controls.Add(plugin);
            plugin.Dock = DockStyle.Fill;
            m_tbcApps.TabPages.Add(tabpTmp);

            // register events
            plugin.KeyDown += TbcAppsKeyDown;
            plugin.OnHasChangesChanged += PluginOnHasChangesChanged;

            // set the active tab to the newly loaded file
            m_tbcApps.SelectedTab = tabpTmp;
        }

        private UniFile GetUniFileFromNode(TreeNode tn, bool onlyVirtual = false, bool onlyLocal = false)
        {
            FSNode node;
            if (tn.TreeView == m_fitCombined.InnerTreeView)
                node = m_fitCombined.GetFSNodeByTreeNode(tn);
            else if (tn.TreeView == m_fitVirtual.InnerTreeView)
                node = m_fitVirtual.GetFSNodeByTreeNode(tn);
            else
                return null;

            if (node == null || !(node is FSNodeFile))
                return null;
            UniFile uniFile = ((FSNodeFile) node).GetUniFile(onlyVirtual, onlyLocal);
            return uniFile;
        }

        private void CloseLoadForm()
        {
            if (m_loadForm != null)
                m_loadForm.Close();
        }

        private void ShowLoadForm()
        {
            if (m_loadForm == null || m_loadForm.IsDisposed)
                m_loadForm = new LoadForm();
            m_loadForm.ShowDialog();
        }

        private void HandleModLoaded()
        {
            Text = DEFAULT_TITLE + @" - " + ModManager.ModName;
            testModToolStripMenuItem.Enabled = true;
            releaseModToolStripMenuItem.Enabled = true;
            addUCSStringToolStripMenuItem.Enabled = true;
            modSettingsToolStripMenuItem.Enabled = true;
            ucsEditorToolStripMenuItem.Enabled = true;
            CloseLoadForm();
        }

        private void HandleSGALoaded()
        {
            Text = DEFAULT_TITLE + @" - " + ModManager.ModName;
            CloseLoadForm();
        }

        #endregion UI

        #region loading stuff

        private void LoadFileFromTreeNode(TreeNode node, bool onlyVirtual = false, bool forceText = false)
        {
            UniFile uniFile = GetUniFileFromNode(node, onlyVirtual);
            if (uniFile == null)
                return;
            try
            {
                var filetool = FileManager.LoadFile(uniFile, forceText);
                if (filetool != null && filetool.Parent != null && filetool.Parent is TabPage)
                {
                    m_tbcApps.SelectedTab = filetool.Parent as TabPage;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.SendMessage("FE - failed to open file");
                LoggingManager.HandleException(ex);
                UIHelper.ShowError("Error while opening the selected file: " + ex.Message);
            }
        }

        #endregion loading stuff

        #endregion Helpers
    }
}
