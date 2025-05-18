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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;
using cope.Extensions;
using cope;

namespace ModTool.Core
{
    public partial class FileTreeControl : UserControl
    {
        #region fields

        bool m_colorLocalFiles = true;
        bool m_colorLocalDirectories = true;
        bool m_noVirtual;
        bool m_noLocal;
        bool m_useImages = true;
        bool m_noRefresh;
        int m_directoryImageIndex;
        int m_directoryExpandedImageIndex = 1;
        int m_fileImageIndex = 2;
        ImageList m_images;

        readonly Dictionary<FileTree, string> m_fileTrees = new Dictionary<FileTree, string>();
        readonly Dictionary<string, FileTree> m_treeNames = new Dictionary<string, FileTree>();

        #endregion fields

        #region ctors

        public FileTreeControl()
        {
            InitializeComponent();
        }

        #endregion ctors

        #region methods

        public void AddFileTree(FileTree tree, string rootName)
        {
            if (tree == null)
                return;
            tree.FilePic = m_fileImageIndex;
            tree.FolderPic = m_directoryImageIndex;
            m_fileTrees.Add(tree, rootName);
            m_treeNames.Add(rootName, tree);
            tree.TransferIntoTreeNodeCollection(m_trvFileTreeView, m_trvFileTreeView.Nodes, rootName, true, m_noVirtual, m_noLocal, m_colorLocalFiles, m_colorLocalDirectories);
            tree.NodeAdded += NodeAdded;
            tree.NodeFileIsVirtualChanged += NodeFileIsVirtualChanged;
            tree.NodeHasLocalChanged += NodeHasLocalChanged;
            tree.NodeRemoved += NodeRemoved;
        }

        public void ClearTree()
        {
            foreach (FileTree tree in m_fileTrees.Keys)
            {
                tree.NodeAdded -= NodeAdded;
                tree.NodeFileIsVirtualChanged -= NodeFileIsVirtualChanged;
                tree.NodeHasLocalChanged -= NodeHasLocalChanged;
                tree.NodeRemoved -= NodeRemoved;
            }
            m_trvFileTreeView.Nodes.Clear();
            m_fileTrees.Clear();
            m_treeNames.Clear();
        }

        public void DeleteSelected()
        {
            if (m_trvFileTreeView.SelectedNode == null)
                return;
            FSNode selected = GetFSNodeByTreeNode(m_trvFileTreeView.SelectedNode);
            if (selected == null || (selected is FSNodeVirtualFile && !(selected as FSNodeVirtualFile).HasLocal))
                return;
            string path = selected.Path;
            try{
                if (selected is FSNodeDir)
                    Directory.Delete(path, true);
                else
                    File.Delete(path);
            }
            catch (Exception ex)
            {
                 UIHelper.ShowError("Could not delete selected file/directory: " + ex.Message);
            }
        }

        public void ExtractSelected()
        {
            if (m_trvFileTreeView.SelectedNode == null)
                return;
            FSNode selected = GetFSNodeByTreeNode(m_trvFileTreeView.SelectedNode);
            if (selected != null)
            {
                if (selected is FSNodeVirtualFile)
                    ((FSNodeVirtualFile)selected).Extract();
                else if (selected is FSNodeDir)
                    ((FSNodeDir)selected).Extract();
            }
        }

        public FSNode GetFSNodeByTreeNode(TreeNode tn)
        {
            if (tn.TreeView != m_trvFileTreeView)
                return null;

            string path = tn.FullPath;
            string toplevel = path.SubstringBeforeFirst('\\');
            path = path.SubstringAfterFirst('\\');
            return m_treeNames[toplevel].RootNode.GetSubNodeByPath(path);
        }

        public FSNode GetSelectedNode()
        {
            if (m_trvFileTreeView.SelectedNode == null)
                return null;
            return GetFSNodeByTreeNode(m_trvFileTreeView.SelectedNode);
        }

        public void OpenSelectedInExplorer()
        {
            if (m_trvFileTreeView.SelectedNode == null)
                return;
            FSNode selected = GetFSNodeByTreeNode(m_trvFileTreeView.SelectedNode);
            if (selected == null)
                return;
            string path;

            if (selected is FSNodeDir)
                path = selected.Path;
            else
                path = selected.Path.SubstringBeforeLast('\\');

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            System.Diagnostics.Process.Start(path);
        }

        public void RefreshView()
        {
            if (m_noRefresh)
                return;
            m_trvFileTreeView.Nodes.Clear();
            foreach (KeyValuePair<FileTree, string> tree in m_fileTrees)
            {
                tree.Key.NodeAdded -= NodeAdded;
                tree.Key.NodeFileIsVirtualChanged -= NodeFileIsVirtualChanged;
                tree.Key.NodeHasLocalChanged -= NodeHasLocalChanged;
                tree.Key.NodeRemoved -= NodeRemoved;

                tree.Key.TransferIntoTreeNodeCollection(m_trvFileTreeView, m_trvFileTreeView.Nodes, tree.Value, true, m_noVirtual, m_noLocal, m_colorLocalFiles, m_colorLocalDirectories);

                tree.Key.NodeAdded += NodeAdded;
                tree.Key.NodeFileIsVirtualChanged += NodeFileIsVirtualChanged;
                tree.Key.NodeHasLocalChanged += NodeHasLocalChanged;
                tree.Key.NodeRemoved += NodeRemoved;
            }
        }

        public void RenameSelected(string newName)
        {
            if (m_trvFileTreeView.SelectedNode == null)
                return;
            FSNode selected = GetFSNodeByTreeNode(m_trvFileTreeView.SelectedNode);
            if (selected == null || (selected is FSNodeVirtualFile && !(selected as FSNodeVirtualFile).HasLocal))
                return;
            string path = selected.Path;
            string newPath = path.SubstringBeforeLast('\\', true) + newName;
            if (File.Exists(newPath) || Directory.Exists(newPath))
            {
                UIHelper.ShowError("There already is an element with the specified name.");
                return;
            }
            if (selected is FSNodeDir)
            {
                if (Directory.Exists(path))
                    Directory.Move(path, newPath);
                else
                    Directory.CreateDirectory(newPath);
                return;
            }
            
            try
            {
                File.Move(path, newPath);
            }
            catch (Exception ex)
            {
                 UIHelper.ShowError("Could not rename selected file: " + ex.Message);
            }
        }

        #endregion methods

        #region eventhandlers

        void NodeRemoved(FSNodeDir sender, FileTreeDirChangedEventArgs e)
        {
            TreeNode tn;
            if (e.Child is FSNodeDir)
            {
                tn = m_trvFileTreeView.GetFileTreeNodeByPath(m_fileTrees[sender.Tree] + '\\' + e.Child.GetPathInTree(), false);
                if (tn == null)
                    return;
                if (m_trvFileTreeView.InvokeRequired)
                    m_trvFileTreeView.Invoke(new MethodInvoker(tn.Remove));
                else
                    tn.Remove();
            }
            else
            {
                tn = m_trvFileTreeView.GetFileTreeNodeByPath(m_fileTrees[sender.Tree] + '\\' + e.Child.GetPathInTree());
                if (tn == null)
                    return;
                if (m_trvFileTreeView.InvokeRequired)
                    m_trvFileTreeView.Invoke(new MethodInvoker(tn.Remove));
                else
                    tn.Remove();
            }
        }

        void NodeHasLocalChanged(FSNode sender)
        {
            var dir = (FSNodeDir)sender;
            TreeNode tn = m_trvFileTreeView.GetFileTreeNodeByPath(m_fileTrees[dir.Tree] + '\\' + dir.GetPathInTree(), false);
            if (tn == null)
            {
                // a tree without virtuals might just not include that node; therefore it must be created
                if (m_noVirtual && dir.HasLocal)
                {
                    // the recursive nature of the FileTree class causes the upper-most node to trigger this event first
                    TreeNode parent = m_trvFileTreeView.GetFileTreeNodeByPath(m_fileTrees[dir.Tree] + '\\' + dir.GetPathInTree().SubstringBeforeLast('\\'), false);
                    TreeNode tmp = dir.ConvertToTreeNode(m_noVirtual, m_noLocal, true, m_colorLocalFiles, m_colorLocalDirectories);
                    if (m_trvFileTreeView.InvokeRequired)
                        m_trvFileTreeView.Invoke(new TreeNodeAddInvoke(parent.Nodes.InsertNodeSorted), tmp);
                    else
                        parent.Nodes.InsertNodeSorted(tmp);
                }
                return;
            }
            if (dir.HasLocal && m_colorLocalDirectories && !m_noLocal)
                tn.ForeColor = Color.Red;
            else
            {
                if (m_noVirtual)
                {
                    tn.Remove();
                    return;
                }
                tn.ForeColor = Color.Blue;
            }
        }

        void NodeFileIsVirtualChanged(FSNodeVirtualFile sender)
        {
            TreeNode tn = m_trvFileTreeView.GetFileTreeNodeByPath(m_fileTrees[sender.Tree] + '\\' + sender.GetPathInTree());
            if (tn == null)
            {
                if (m_noVirtual && sender.HasLocal)
                {
                    TreeNode parent = m_trvFileTreeView.GetFileTreeNodeByPath(m_fileTrees[sender.Tree] + '\\' + sender.GetPathInTree().SubstringBeforeLast('\\'), false);
                    TreeNode tmp = sender.ConvertToTreeNode(true, m_colorLocalFiles, m_noLocal);
                    if (m_trvFileTreeView.InvokeRequired)
                        m_trvFileTreeView.Invoke(new TreeNodeAddInvoke(parent.Nodes.InsertNodeSorted), tmp);
                    else
                        parent.Nodes.InsertNodeSorted(tmp);
                }
                return;
            }
            if (sender.HasLocal && !m_noLocal)
            {
                tn.ToolTipText = sender.Path;
                tn.ForeColor = m_colorLocalFiles ? Color.Red : Color.Blue;
            }
            else
            {
                if (m_noVirtual)
                {
                    if (m_trvFileTreeView.InvokeRequired)
                        m_trvFileTreeView.Invoke(new MethodInvoker(tn.Remove));
                    else
                        tn.Remove();
                }
                tn.ToolTipText = sender.VirtualFile.SGA.FilePath + "::" + sender.PathInTree;
                tn.ForeColor = Color.Blue;
            }
        }

        void NodeAdded(FSNodeDir sender, FileTreeDirChangedEventArgs e)
        {
            TreeNode parent = m_trvFileTreeView.GetFileTreeNodeByPath(m_fileTrees[sender.Tree] + '\\' + e.Parent.GetPathInTree(), false);
            TreeNode tn;
            if (e.Child is FSNodeDir)
            {
                tn = ((FSNodeDir)e.Child).ConvertToTreeNode(m_noVirtual, m_noLocal, true, m_colorLocalFiles, m_colorLocalDirectories);
            }
            else
            {
                tn = ((FSNodeFile)e.Child).ConvertToTreeNode(true, m_colorLocalFiles);
            }
            if (tn == null || parent == null) // possible for virtual trees
                return;
            if (m_trvFileTreeView.InvokeRequired)
                m_trvFileTreeView.Invoke(new TreeNodeAddInvoke(parent.Nodes.InsertNodeSorted), tn);
            else
                parent.Nodes.InsertNodeSorted(tn);
        }

        private void TrvFileTreeViewAfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = m_directoryImageIndex;
            e.Node.SelectedImageIndex = m_directoryImageIndex;
        }

        private void TrvFileTreeViewAfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = m_directoryExpandedImageIndex;
            e.Node.SelectedImageIndex = m_directoryExpandedImageIndex;
        }

        #endregion eventhandlers

        #region properties

        public int DirectoryImageIndex
        {
            get
            {
                return m_directoryImageIndex;
            }
            set
            {
                m_directoryImageIndex = value;
                foreach (FileTree ft in m_fileTrees.Keys)
                    ft.FolderPic = value;
                RefreshView();
            }
        }

        public int DirectoryExpandedImageIndex
        {
            get
            {
                return m_directoryExpandedImageIndex;
            }
            set
            {
                m_directoryExpandedImageIndex = value;
            }
        }

        public int FileImageIndex
        {
            get
            {
                return m_fileImageIndex;
            }
            set
            {
                m_fileImageIndex = value;
                foreach (FileTree ft in m_fileTrees.Keys)
                    ft.FilePic = value;
                RefreshView();
            }
        }

        public bool ColorLocalFiles
        {
            get
            {
                return m_colorLocalFiles;
            }
            set
            {
                m_colorLocalFiles = value;
                RefreshView();
            }
        }

        public bool ColorLocalDirectories
        {
            get
            {
                return m_colorLocalDirectories;
            }
            set
            {
                m_colorLocalDirectories = value;
                RefreshView();
            }
        }

        public bool NoVirtualFiles
        {
            get
            {
                return m_noVirtual;
            }
            set
            {
                m_noVirtual = value;
                RefreshView();
            }
        }

        public bool NoLocalFiles
        {
            get
            {
                return m_noLocal;
            }
            set
            {
                m_noLocal = value;
                RefreshView();
            }
        }

        public bool UseImages
        {
            get
            {
                return m_useImages;
            }
            set
            {
                m_useImages = value;
                if (!m_useImages)
                {
                    m_images = m_trvFileTreeView.ImageList;
                    m_trvFileTreeView.ImageList = null;
                }
                else if (m_trvFileTreeView.ImageList == null)
                    m_trvFileTreeView.ImageList = m_images;
            }
        }

        public bool NoRefresh
        {
            get
            {
                return m_noRefresh;
            }
            set
            {
                m_noRefresh = value;
            }
        }

        public ImageList ImageList
        {
            get
            {
                return m_images;
            }
            set
            {
                if (m_useImages)
                    m_trvFileTreeView.ImageList = value;
                m_images = value;
            }
        }

        public TreeView InnerTreeView
        {
            get
            {
                return m_trvFileTreeView;
            }
        }

        #endregion properties
    }
}