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
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using cope.ADT;
using cope.DawnOfWar2.SGA;
using cope.Extensions;

namespace ModTool.Core
{
    // Todo: clean up the whole File-system code, it's old, ugly & difficult to maintain
    // Todo: make it independent from SGA files and introduce an interface for SGA like IArchiveFile
    // Todo: also rework the SGA file class itself to be a reader and return a SubFileSystem or similar
    public enum FileTreeActions
    {
        FT_NODE_ADDED,
        FT_NODE_REMOVED
    }

    public delegate void FileTreeDirChangedEventHandler(FSNodeDir sender, FileTreeDirChangedEventArgs e);

    public delegate void FileTreeNodeChangedEventHandler(FSNode sender);

    public delegate void FileTreeFileChangedEventHandler(FSNodeFile sender);

    public delegate void FileTreeVirtualChangedEventHandler(FSNodeVirtualFile sender);

    public class FileTreeDirChangedEventArgs
    {
        #region fields

        public FSNodeDir Parent;
        public FSNode Child;
        public FileTreeActions Action;

        #endregion fields

        public FileTreeDirChangedEventArgs(FSNodeDir parent, FSNode child, FileTreeActions action)
        {
            Parent = parent;
            Child = child;
            Action = action;
        }
    }

    /// <summary>
    /// Class which stores information about local and virtual files and grants access to them.
    /// </summary>
    public class FileTree
    {
        #region fields

        protected string m_basePath;
        protected FSNodeDir m_rootNode;
        protected FileSystemWatcher m_watcher;

        protected ThreadSafeQueue<FileSystemEventArgs> m_events = new ThreadSafeQueue<FileSystemEventArgs>();
        protected EventWaitHandle m_shutDown = new AutoResetEvent(false);

        protected int m_folderPic;
        protected int m_filePic;

        #endregion fields

        #region ctors

        /// <summary>
        /// Constructs a new FileTree from the given path and SGAs.
        /// Set an EntryPoint name to only add files from SGAs which belong to an EntryPoint with the specified name.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sgas"></param>
        /// <param name="epName"></param>
        public FileTree(string path, IEnumerable<SGAFile> sgas, string epName = null)
        {
            if (!path.EndsWith('\\'))
                path += '\\';
            m_basePath = path;
            m_rootNode = new FSNodeDir(path, this);

            foreach (SGAFile sga in sgas)
            {
                foreach (SGAEntryPoint ep in sga)
                {
                    if (epName != null && string.Compare(ep.Name, epName, true) != 0)
                        continue;

                    foreach (SGAStoredDirectory sgasd in ep.StoredDirectories.Values)
                    {
                        string dirpath = sgasd.GetPath();
                        FSNodeDir dirnode = m_rootNode.TryAddDirFromRelativePath(dirpath);
                        if (sgasd.StoredFiles.Values.Count > 0)
                            dirnode.HasVirtual = true;
                        foreach (SGAStoredFile sgasf in sgasd.StoredFiles.Values)
                        {
                            FSNodeFile file = dirnode.GetFile(sgasf.Name);
                            if (file == null)
                            {
                                new FSNodeVirtualFile(sgasf.Name, sgasf, this) {Parent = dirnode};
                            }
                            else if (!(file is FSNodeVirtualFile))
                            {
                                dirnode.RemoveChild(file);
                                new FSNodeVirtualFile(sgasf.Name, sgasf, this) {Parent = dirnode};
                            }
                        }
                    }
                }
            }

            SetupWatcher(path);
            SetupConsumerThread();
        }

        public FileTree(string path)
        {
            if (!path.EndsWith('\\'))
                path += '\\';
            m_basePath = path;
            m_rootNode = new FSNodeDir(path, this);

            SetupWatcher(path);
            SetupConsumerThread();
        }

        private void SetupWatcher(string path)
        {
            m_watcher = new FileSystemWatcher(path)
                            {
                                Filter = string.Empty,
                                IncludeSubdirectories = true,
                                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName,
                                EnableRaisingEvents = true,
                                InternalBufferSize = 4096
                            };

            m_watcher.Error += WatcherError;
            m_watcher.Deleted += WatcherDeleted;
            m_watcher.Renamed += WatcherRenamed;
            m_watcher.Created += WatcherCreated;
        }

        private void SetupConsumerThread()
        {
            var consumer = new Thread(ConsumerThreadStart);
            consumer.Start();
        }

        #endregion ctors

        #region events

        public event FileTreeDirChangedEventHandler NodeAdded;

        public event FileTreeDirChangedEventHandler NodeRemoved;

        public event FileTreeNodeChangedEventHandler NodeHasLocalChanged;

        public event FileTreeVirtualChangedEventHandler NodeFileIsVirtualChanged;

        #endregion events

        #region eventhandlers

        static void WatcherError(object sender, ErrorEventArgs e)
        {
            LoggingManager.SendMessage("FileSystemWatcher buffer overflow!");
            LoggingManager.HandleException(e.GetException());
        }

        void WatcherCreated(object sender, FileSystemEventArgs e)
        {
            m_events.Enqueue(e);
        }

        void WatcherRenamed(object sender, RenamedEventArgs e)
        {
            m_events.Enqueue(e);
        }

        void WatcherDeleted(object sender, FileSystemEventArgs e)
        {
            m_events.Enqueue(e);
        }

        #endregion eventhandlers

        #region methods

        internal void InvokeNodeAdded(FSNodeDir sender, FileTreeDirChangedEventArgs e)
        {
            if (NodeAdded != null)
                NodeAdded(sender, e);
        }

        internal void InvokeNodeRemoved(FSNodeDir sender, FileTreeDirChangedEventArgs e)
        {
            if (NodeRemoved != null)
                NodeRemoved(sender, e);
        }

        internal void InvokeNodeHasLocalChanged(FSNode sender)
        {
            if (NodeHasLocalChanged != null)
                NodeHasLocalChanged(sender);
        }

        internal void InvokeNodeFileIsVirtualChanged(FSNodeVirtualFile sender)
        {
            if (NodeFileIsVirtualChanged != null)
                NodeFileIsVirtualChanged(sender);
        }

        /// <summary>
        /// Adds all nodes from this FileTree to a TreeNodeCollection.
        /// </summary>
        /// <param name="owner">The owner of the TreeNodeCollection, e.g. the TreeView.</param>
        /// <param name="collection">The collection to add the File nodes to.</param>
        /// <param name="rootName">The name of the rootnode.</param>
        /// <param name="usePictures">Set to true to set the picture index for the TreeNodes.</param>
        /// <param name="noVirtual">Set to true to exclude virtual files.</param>
        /// <param name="noLocal">Set to true to exclude local files.</param>
        /// <param name="colorLocalFiles">Set to true to color local files.</param>
        /// <param name="colorLocalDirs">Set to true to color local directories.</param>
        public void TransferIntoTreeNodeCollection(Control owner, TreeNodeCollection collection, string rootName, bool usePictures = true,
                                                  bool noVirtual = false, bool noLocal = false, bool colorLocalFiles = true, bool colorLocalDirs = true)
        {
            if (m_rootNode == null)
            {
                LoggingManager.SendWarning("Tried to convert a FileTree without a RootNode to a node collection");
                return;
            }
            var root = new TreeNode(rootName) {Name = 'D' + rootName};
            TransferIntoTnc(owner, m_rootNode, root.Nodes, noVirtual, noLocal, usePictures, colorLocalFiles, colorLocalDirs);
            if (owner.InvokeRequired)
            {
                MethodInvoker k = () => collection.Add(root);
                owner.Invoke(k);
            }
            else
                collection.Add(root);
        }

        protected void TransferIntoTnc(Control owner, FSNodeDir parent, TreeNodeCollection collection, bool noVirtual, bool noLocal, bool usePictures,
                                       bool colorLocalFiles, bool colorLocalDirs)
        {
            foreach (FSNodeDir fsnd in parent.SubDirsList.Values)
            {
                if (noVirtual && !fsnd.HasLocal)
                    continue;
                if (noLocal && !fsnd.HasVirtual)
                    continue;
                TreeNode dir = fsnd.ConvertToTreeNode(noVirtual, noLocal, usePictures, colorLocalFiles, colorLocalDirs);
                if (owner.InvokeRequired)
                {
                    MethodInvoker k = () => collection.Add(dir);
                    owner.Invoke(k);
                }
                else
                    collection.Add(dir);
            }

            foreach (FSNodeFile fsnf in parent.FilesList.Values)
            {
                if (noVirtual && !fsnf.HasLocal)
                    continue;
                if (noLocal && !(fsnf is FSNodeVirtualFile))
                    continue;
                TreeNode file = fsnf.ConvertToTreeNode(usePictures, colorLocalFiles);
                if (owner.InvokeRequired)
                {
                    MethodInvoker k = () => collection.Add(file);
                    owner.Invoke(k);
                }
                else
                    collection.Add(file);
            }
        }

        /// <summary>
        /// Shuts down the FileTree, freeing all resources.
        /// </summary>
        public void ShutDown()
        {
            m_watcher.EnableRaisingEvents = false;
            m_watcher.Dispose();
            m_shutDown.Set();
            m_rootNode = null;
        }

        private void ConsumerThreadStart()
        {
            while (true)
            {
                if (m_shutDown.WaitOne(0))
                    break;
                if (!m_events.HeadValidEvent.WaitOne(25))
                    continue;
                while (!m_events.IsEmpty)
                {
                    if (m_shutDown.WaitOne(0))
                        break;
                    FileSystemEventArgs e = m_events.Dequeue();
                    ProcessEvent(e);
                }
            }
            m_shutDown.Close();
            m_shutDown.Dispose();
        }

        private void ProcessEvent(FileSystemEventArgs e)
        {
            string relativePath;
            if (e.FullPath.StartsWith(m_basePath))
                relativePath = e.FullPath.SubstringAfterFirst(m_basePath);
            else
                return;
            try
            {
                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Created:
                        if (Directory.Exists(e.FullPath))
                        {
                            FSNodeDir tmp = m_rootNode.GetSubDirByPath(relativePath);
                            if (tmp == null)
                            {
                                m_rootNode.TryAddDirFromRelativePath(relativePath);
                            }
                        }
                        else
                        {
                            FSNodeFile file = m_rootNode.TryAddFileFromRelativePath(relativePath);
                            if (file != null && file is FSNodeVirtualFile)
                            {
                                if (file.Parent != null)
                                    file.Parent.LocalCountAdd(1);
                                InvokeNodeFileIsVirtualChanged((FSNodeVirtualFile)file);
                            }
                        }
                        break;
                    case WatcherChangeTypes.Deleted:
                        FSNode tmp2 = m_rootNode.GetSubNodeByPath(relativePath);
                        if (tmp2 == null)
                            return;
                        if (tmp2 is FSNodeDir)
                        {
                            if (!((FSNodeDir)tmp2).HasVirtual)
                                tmp2.RemoveSelf(true);
                            else
                                ((FSNodeDir)tmp2).RemoveAllLocalChilds();
                            return;
                        }
                        if (tmp2 is FSNodeVirtualFile)
                        {
                            if (tmp2.Parent != null)
                                tmp2.Parent.LocalCountAdd(-1);
                            InvokeNodeFileIsVirtualChanged((FSNodeVirtualFile)tmp2);
                            return;
                        }
                        tmp2.RemoveSelf(true);
                        break;
                    case WatcherChangeTypes.Renamed:
                        relativePath = ((RenamedEventArgs)e).OldFullPath.SubstringAfterFirst(m_basePath);
                        FSNode file2 = m_rootNode.GetSubNodeByPath(relativePath);
                        file2.Name = e.Name.SubstringAfterLast('\\');
                        break;
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }

        #endregion methods

        #region properties

        /// <summary>
        /// Gets the BasePath of this FileTree. All nodes of this FileTree are within this path;
        /// this property is used by FSNodes to get their absolute path.
        /// </summary>
        public string BasePath
        {
            get
            {
                return m_basePath;
            }
        }

        /// <summary>
        /// Gets the root node of this FileTree.
        /// </summary>
        public FSNodeDir RootNode
        {
            get
            {
                return m_rootNode;
            }
        }

        /// <summary>
        /// Gets or sets the picture index to be used for Folders.
        /// </summary>
        public int FolderPic
        {
            get
            {
                return m_folderPic;
            }
            set
            {
                m_folderPic = value;
            }
        }

        /// <summary>
        /// Gets or sets the picture index to be used for regular files.
        /// </summary>
        public int FilePic
        {
            get
            {
                return m_filePic;
            }
            set
            {
                m_filePic = value;
            }
        }

        #endregion properties
    }
}