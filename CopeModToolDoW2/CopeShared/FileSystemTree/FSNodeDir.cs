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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cope;
using cope.Extensions;

namespace ModTool.Core
{
    public sealed class FSNodeDir : FSNode
    {
        #region fields

        private static readonly char[] s_splitter = {'\\'};
        private SortedList<string, FSNodeFile> m_files = new SortedList<string, FSNodeFile>();
        private bool m_hasVirtual;
        private int m_localCount;
        private int m_localCountChilds;
        private SortedList<string, FSNodeDir> m_subdirs = new SortedList<string, FSNodeDir>();

        #endregion fields

        #region ctors

        public FSNodeDir(string path, FileTree tree)
            : base(path.SubstringAfterLast('\\'), tree)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            IEnumerable<string> files = Directory.EnumerateFiles(path);
            IEnumerable<string> dirs = Directory.EnumerateDirectories(path);

            foreach (string file in files)
            {
                new FSNodeFile(file.SubstringAfterLast('\\'), m_tree, this);
            }

            foreach (string dir in dirs)
            {
                new FSNodeDir(dir, this);
            }
        }

        public FSNodeDir(string path, FSNodeDir parent)
            : base(path.SubstringAfterLast('\\'), parent.m_tree, parent)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(path);
            IEnumerable<string> dirs = Directory.EnumerateDirectories(path);

            foreach (string file in files)
            {
                new FSNodeFile(file.SubstringAfterLast('\\'), m_tree, this);
            }

            foreach (string dir in dirs)
            {
                new FSNodeDir(dir, this);
            }
        }

        public FSNodeDir(string name, FileTree tree, FSNodeDir parent = null)
            : base(name)
        {
            m_tree = tree;
            Parent = parent;
        }

        #endregion ctors

        #region events

        public event FileTreeDirChangedEventHandler NodeAdded;

        public event FileTreeDirChangedEventHandler NodeRemoved;

        #endregion events

        #region methods

        /// <summary>
        /// Checks for new files/subdirectories.
        /// </summary>
        public void CheckForNew()
        {
            string path = GetPath();

            IEnumerable<string> files = Directory.EnumerateFiles(path);
            IEnumerable<string> dirs = Directory.EnumerateDirectories(path);

            foreach (string file in files)
            {
                string name = file.SubstringAfterLast('\\');
                if (!ContainsFile(name))
                    new FSNodeFile(name, m_tree, this);
                else if (m_files[name] is FSNodeVirtualFile)
                {
                    var tmp = (FSNodeVirtualFile) m_files[name];
                    if (tmp.HasLocal)
                    {
                        LocalCountAdd(1);
                        if (m_tree != null)
                            m_tree.InvokeNodeFileIsVirtualChanged(tmp);
                    }
                }
            }

            foreach (string dir in dirs)
            {
                if (!ContainsDirectory(dir.SubstringAfterLast('\\')))
                {
                    new FSNodeDir(dir, this);
                }
            }
        }

        public void AddChild(FSNode child)
        {
            child.Parent = this;
        }

        internal void AddChildIntern(FSNode child)
        {
            if (child is FSNodeDir)
            {
                m_subdirs.Add(child.Name, (FSNodeDir) child);
                LocalCountAdd(((FSNodeDir) child).LocalCount, true);
            }
            else
            {
                m_files.Add(child.Name, (FSNodeFile) child);
                if ((child).HasLocal)
                    LocalCountAdd(1);
            }
            if (NodeAdded != null)
                NodeAdded(this, new FileTreeDirChangedEventArgs(this, child, FileTreeActions.FT_NODE_ADDED));
            if (m_tree != null)
                m_tree.InvokeNodeAdded(this, new FileTreeDirChangedEventArgs(this, child, FileTreeActions.FT_NODE_ADDED));
        }

        public FSNodeFile TryAddFileFromRelativePath(string path)
        {
            if (!File.Exists(m_tree.BasePath + path))
            {
                //throw new CopeException("The file you're trying to add does not exist! Base path: {0} ; relative path: {1}", _tree.BasePath, path);
                return null;
            }
            string dirPath = path.SubstringBeforeLast('\\', true);

            FSNodeFile newFile;
            if (dirPath.Contains('\\'))
            {
                FSNodeDir direc = TryAddDirFromRelativePath(dirPath);
                string filename = path.SubstringAfterLast('\\');
                newFile = !direc.ContainsFile(filename) ? new FSNodeFile(path.SubstringAfterLast('\\'), m_tree, direc) : direc.m_files[filename];
                return newFile;
            }
            newFile = new FSNodeFile(path.SubstringAfterLast('\\'), m_tree, this);
            return newFile;
        }

        public FSNodeDir TryAddDirFromRelativePath(string path)
        {
            string[] parts = path.Split(s_splitter, StringSplitOptions.RemoveEmptyEntries);
            return TryAddDirFromRelativePath(parts);
        }

        public FSNodeDir TryAddDirFromRelativePath(string[] parts)
        {
            FSNodeDir current = this;
            int pos = 0;
            while (true)
            {
                if (current.m_subdirs.ContainsKey(parts[pos]))
                {
                    current = current.m_subdirs[parts[pos]];
                    if (parts.Length - pos == 1)
                        return current;
                    pos++;
                    continue;
                }
                return current.AddDirFromRelativePath(parts, pos);
            }
        }

        public FSNodeDir AddDirFromRelativePath(string path)
        {
            string[] parts = path.Split(s_splitter, StringSplitOptions.RemoveEmptyEntries);
            return AddDirFromRelativePath(parts);
        }

        public FSNodeDir AddDirFromRelativePath(string[] parts, int startIndex = 0)
        {
            FSNodeDir current = this;

            for (int i = startIndex; i < parts.Length; i++)
            {
                current = new FSNodeDir(parts[i], m_tree, current);
            }
            return current;
        }

        public void RemoveChild(FSNode child)
        {
            child.Parent = null;
        }

        internal void RemoveChildIntern(FSNode child, bool forceLocal = false)
        {
            if (child is FSNodeDir)
            {
                m_subdirs.Remove(child.Name);
                LocalCountAdd(-((FSNodeDir) child).LocalCount, true);
            }
            else
            {
                m_files.Remove(child.Name);
                if ((child).HasLocal || forceLocal)
                    LocalCountAdd(-1);
            }
            if (NodeRemoved != null)
                NodeRemoved(this, new FileTreeDirChangedEventArgs(this, child, FileTreeActions.FT_NODE_REMOVED));
            if (m_tree != null)
                m_tree.InvokeNodeRemoved(this,
                                         new FileTreeDirChangedEventArgs(this, child, FileTreeActions.FT_NODE_REMOVED));
        }

        internal void RemoveAllLocalChilds()
        {
            for (int i = 0; i < m_files.Count;)
            {
                FSNodeFile fsnf = m_files.Values[i];
                if (!(fsnf is FSNodeVirtualFile))
                    fsnf.RemoveSelf();
                else
                {
                    i++;
                    if (m_tree != null)
                        m_tree.InvokeNodeFileIsVirtualChanged((FSNodeVirtualFile) fsnf);
                }
            }

            for (int i = 0; i < m_subdirs.Count;)
            {
                FSNodeDir fsnd = m_subdirs.Values[i];
                if (!fsnd.m_hasVirtual)
                    fsnd.RemoveSelf();
                else
                {
                    fsnd.RemoveAllLocalChilds();
                    i++;
                }
            }
            LocalCountZero();
        }

        public bool ContainsFile(string name)
        {
            return m_files.ContainsKey(name);
        }

        public bool ContainsFile(FSNodeFile file)
        {
            return m_files.ContainsValue(file);
        }

        /// <exception cref="CopeException">This node is not part of a file tree and can thus not search for files!</exception>
        public bool HasLocalFile(string filename)
        {
            if (m_tree == null)
                throw new CopeException("This node is not part of a file tree and can thus not search for files!");
            return File.Exists(GetPath() + '\\' + filename);
        }

        public bool ContainsDirectory(string name)
        {
            return m_subdirs.ContainsKey(name);
        }

        public bool ContainsDirectory(FSNodeDir dir)
        {
            return m_subdirs.ContainsValue(dir);
        }

        public FSNodeFile GetFile(string name)
        {
            if (m_files.ContainsKey(name))
                return m_files[name];
            return null;
        }

        public FSNodeDir GetDirectory(string name)
        {
            if (m_subdirs.ContainsKey(name))
                return m_subdirs[name];
            return null;
        }

        public override FSNode GClone()
        {
            var copy = new FSNodeDir(m_name, m_tree);

            foreach (FSNodeDir fsnd in m_subdirs.Values)
            {
                fsnd.GClone().Parent = copy;
            }

            foreach (FSNodeFile fsnf in m_files.Values)
            {
                fsnf.GClone().Parent = copy;
            }

            return copy;
        }

        public FSNode GetSubNodeByPath(string path)
        {
            FSNodeDir current = this;
            string[] nextDir = path.Split(s_splitter, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < nextDir.Length; i++)
            {
                if (current.m_subdirs.ContainsKey(nextDir[i]))
                {
                    if (i == nextDir.Length - 1)
                        return current.m_subdirs[nextDir[i]];
                    current = current.m_subdirs[nextDir[i]];
                }
                else if (i == nextDir.Length - 1)
                {
                    if (current.m_files.ContainsKey(nextDir[i]))
                        return current.m_files[nextDir[i]];
                }
                else
                    break;
            }
            return null;
        }

        public FSNodeDir GetSubDirByPath(string path)
        {
            string[] nextDir = path.Split(s_splitter, StringSplitOptions.RemoveEmptyEntries);
            FSNodeDir current = this;
            for (int i = 0; i < nextDir.Length; i++)
            {
                if (current.m_subdirs.ContainsKey(nextDir[i]))
                {
                    if (i == nextDir.Length - 1)
                        return current.m_subdirs[nextDir[i]];
                    current = current.m_subdirs[nextDir[i]];
                    continue;
                }
                return null;
            }
            return null;
        }

        public FSNodeFile GetFileByPath(string path)
        {
            string[] nextDir = path.Split(s_splitter, StringSplitOptions.RemoveEmptyEntries);
            FSNodeDir current = this;
            for (int i = 0; i < nextDir.Length; i++)
            {
                if (i == nextDir.Length - 1 && current.m_files.ContainsKey(nextDir[i]))
                    return current.m_files[nextDir[i]];
                if (current.m_subdirs.ContainsKey(nextDir[i]))
                    current = current.m_subdirs[nextDir[i]];
                else
                    break;
            }
            return null;
        }

        public void MergeWith(FSNodeDir node)
        {
            for (int i = 0; i < node.FilesList.Count; i++)
            {
                FSNodeFile fsnf = node.FilesList.Values[i];
                if (!ContainsFile(fsnf.Name))
                {
                    if (!HasLocalFile(fsnf.Name))
                        continue;
                    if (fsnf is FSNodeVirtualFile)
                    {
                        new FSNodeFile(fsnf.Name, m_tree, this);
                        if (m_tree != null)
                            m_tree.InvokeNodeFileIsVirtualChanged((FSNodeVirtualFile) node.m_files[fsnf.Name]);
                        node.LocalCountAdd(-1);
                    }
                    else
                    {
                        fsnf.Parent = this;
                        node.LocalCountAdd(-1);
                        i--;
                    }
                }
                else if (m_files[fsnf.Name] is FSNodeVirtualFile && m_files[fsnf.Name].HasLocal)
                {
                    if (m_tree != null)
                        m_tree.InvokeNodeFileIsVirtualChanged((FSNodeVirtualFile) m_files[fsnf.Name]);
                    LocalCountAdd(1);
                }
            }

            for (int i = 0; i < node.SubDirsList.Count; i++)
            {
                FSNodeDir fsnd = node.SubDirsList.Values[i];
                if (!ContainsDirectory(fsnd.Name))
                {
                    fsnd.Parent = this;
                    i--;
                }
                else
                    m_subdirs[fsnd.m_name].MergeWith(fsnd);
            }
        }

        internal void LocalCountAdd(int count, bool fromDir = false)
        {
            bool zero;
            if (fromDir)
            {
                zero = m_localCountChilds == 0;
                m_localCountChilds += count;
            }
            else
            {
                zero = m_localCount == 0;
                m_localCount += count;
            }

            if (m_parent != null)
            {
                m_parent.LocalCountAdd(count, true);
            }
            if ((m_localCount + m_localCountChilds == 0) || (zero && count != 0))
            {
                if (m_tree != null)
                    m_tree.InvokeNodeHasLocalChanged(this);
            }
        }

        internal void LocalCountZero()
        {
            if (m_localCount != 0)
            {
                if (m_parent != null)
                    m_parent.LocalCountAdd(-m_localCount, true);
                m_localCount = 0;

                if ((m_localCountChilds == 0 && m_tree != null))
                    m_tree.InvokeNodeHasLocalChanged(this);
            }
        }

        public TreeNode ConvertToTreeNode(bool noVirtual = false, bool noLocal = false, bool usePictures = true,
                                          bool colorLocalFiles = true, bool colorLocalDirs = true)
        {
            if (noLocal && !m_hasVirtual)
                return null;

            var result = new TreeNode {Name = 'D' + m_name, Text = m_name};

            if (usePictures)
            {
                result.ImageIndex = m_tree.FolderPic;
                result.SelectedImageIndex = m_tree.FolderPic;
            }

            if (LocalCount > 0 && colorLocalDirs && !noLocal)
                result.ForeColor = Color.Red;
            else
                result.ForeColor = Color.Blue;

            foreach (FSNodeDir fsnd in m_subdirs.Values)
            {
                if (noVirtual && !fsnd.HasLocal)
                    continue;
                if (noLocal && !fsnd.HasVirtual)
                    continue;
                result.Nodes.Add(fsnd.ConvertToTreeNode(noVirtual, noLocal, usePictures, colorLocalFiles, colorLocalDirs));
            }

            foreach (FSNodeFile fsnf in m_files.Values)
            {
                if (noVirtual && fsnf is FSNodeVirtualFile && !fsnf.HasLocal)
                    continue;
                if (noLocal && !(fsnf is FSNodeVirtualFile))
                    continue;
                TreeNode file = fsnf.ConvertToTreeNode(usePictures, colorLocalFiles, noLocal);
                result.Nodes.Add(file);
            }

            return result;
        }

        public void Extract()
        {
            if (!m_hasVirtual)
                return;

            string path = Path;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (FSNodeDir dir in m_subdirs.Values)
                dir.Extract();

            foreach (FSNodeFile file in m_files.Values)
            {
                if (file is FSNodeVirtualFile)
                    ((FSNodeVirtualFile) file).Extract();
            }
        }

        // Todo: recode these, bad code ahead
        public string[] GetRelativeFilePaths(string removeFromBeginning = "", string removeFromEnding = "",
                                             string[] extensions = null, string[] excludeEndings = null)
        {
            var subPaths = new string[m_files.Count];
            if (m_path == null)
                m_path = GetPathInTree();
            string path = m_path.SubstringAfterFirst(removeFromBeginning);
            if (!path.EndsWith('\\'))
                path += '\\';
            int i = 0;
            foreach (string f in m_files.Keys)
            {
                // check endings
                if (excludeEndings != null)
                {
                    bool ends = false;
                    foreach (string s in excludeEndings)
                        if (f.EndsWith(s))
                        {
                            ends = true;
                            break;
                        }
                    if (ends)
                        continue;
                }
                // check extensions
                if (extensions != null)
                {
                    bool ext = false;
                    foreach (string s in extensions)
                        if (f.EndsWith(s))
                        {
                            ext = true;
                            break;
                        }
                    if (!ext)
                        continue;
                }
                subPaths[i++] = path + f.SubstringBeforeLast(removeFromEnding);
            }
            return subPaths;
        }

        public string[] GetRelativePaths(string removeFromBeginning = "", string removeFromFileEnding = "")
        {
            var subPaths = new string[m_files.Count + m_subdirs.Count];
            if (m_path == null)
                m_path = GetPathInTree();
            string path = m_path;
            if (!path.EndsWith('\\'))
                path += '\\';
            path = path.SubstringAfterFirst(removeFromBeginning);
            int i = 0;
            foreach (string f in m_files.Keys)
                subPaths[i++] = path + f.SubstringBeforeLast(removeFromFileEnding);
            foreach (string d in m_subdirs.Keys)
                subPaths[i++] = path + d + '\\';
            return subPaths;
        }

        public List<string> GetRelativeFilePathsWithSubs(string removeFromBeginning = "", string removeFromEnding = "",
                                                         string[] extensions = null, string[] excludeEndings = null)
        {
            var subPaths = new List<string>();
            if (m_path == null)
                m_path = GetPathInTree();
            string path = m_path.SubstringAfterFirst(removeFromBeginning);
            if (!path.EndsWith('\\'))
                path += '\\';
            foreach (string f in m_files.Keys)
            {
                // check endings
                if (excludeEndings != null)
                {
                    bool ends = false;
                    foreach (string s in excludeEndings)
                        if (f.EndsWith(s))
                        {
                            ends = true;
                            break;
                        }
                    if (ends)
                        continue;
                }
                // check extensions
                if (extensions != null)
                {
                    bool ext = false;
                    foreach (string s in extensions)
                        if (f.EndsWith(s))
                        {
                            ext = true;
                            break;
                        }
                    if (!ext)
                        continue;
                }
                subPaths.Add(path + f.SubstringBeforeLast(removeFromEnding));
            }
            foreach (FSNodeDir d in m_subdirs.Values)
                d.GetRelativeFilePathsWithSubsInt(subPaths, path, removeFromEnding, extensions, excludeEndings);
            return subPaths;
        }

        private void GetRelativeFilePathsWithSubsInt(List<string> subPaths, string beginning, string removeFromEnding,
                                                     IEnumerable<string> extensions = null, IEnumerable<string> excludeEndings = null)
        {
            string path = beginning + Name + '\\';
            foreach (string f in m_files.Keys)
            {
                // check endings
                if (excludeEndings != null)
                {
                    bool ends = false;
                    foreach (string s in excludeEndings)
                        if (f.EndsWith(s))
                        {
                            ends = true;
                            break;
                        }
                    if (ends)
                        continue;
                }
                // check extensions
                if (extensions != null)
                {
                    bool ext = false;
                    foreach (string s in extensions)
                        if (f.EndsWith(s))
                        {
                            ext = true;
                            break;
                        }
                    if (!ext)
                        continue;
                }
                subPaths.Add(path + f.SubstringBeforeLast(removeFromEnding));
            }
            foreach (FSNodeDir d in m_subdirs.Values)
                d.GetRelativeFilePathsWithSubsInt(subPaths, path, removeFromEnding, extensions, excludeEndings);
        }

        public override void Deconstruct()
        {
            for (int i = 0; i < m_subdirs.Count; i++)
            {
                m_subdirs.Values[i--].Deconstruct();
            }
            m_subdirs = null;
            for (int i = 0; i < m_files.Count; i++)
            {
                m_files.Values[i--].Deconstruct();
            }
            m_files = null;
            m_tree = null;
            Parent = null;
        }

        public override void ResetPath()
        {
            m_path = null;
            foreach (FSNodeFile fsnf in m_files.Values)
                fsnf.ResetPath();
            foreach (FSNodeDir fsnd in m_subdirs.Values)
                fsnd.ResetPath();
        }

        public int GetTotalFileCount()
        {
            return m_files.Count + m_subdirs.Values.Sum(dir => dir.GetTotalFileCount());
        }

        #endregion methods

        #region properties

        public int FileCount
        {
            get { return m_files.Count; }
        }

        public int DirectoryCount
        {
            get { return m_subdirs.Count; }
        }

        public int LocalCount
        {
            get { return m_localCount + m_localCountChilds; }
        }

        public bool HasVirtual
        {
            get { return m_hasVirtual; }
            internal set
            {
                m_hasVirtual = true;
                if (m_parent != null && value)
                    m_parent.HasVirtual = true;
            }
        }

        public override bool HasLocal
        {
            get { return (m_localCount + m_localCountChilds) != 0; }
        }

        public override string Name
        {
            get { return base.Name; }
            internal set
            {
                if (m_hasVirtual)
                {
                    if (m_parent == null)
                    {
                        LoggingManager.SendMessage(LogSystemMessageType.Warning,
                                                   "Tried to rename a node without a parent -- probably the root-node.");
                        return;
                    }
                    FSNodeDir target;
                    if (m_parent.ContainsDirectory(value))
                        target = m_subdirs[value];
                    else
                        target = new FSNodeDir(value, m_tree, m_parent);
                    ResetPath();
                    target.MergeWith(this);
                    return;
                }
                if (m_parent != null && m_parent.ContainsDirectory(value))
                {
                    FSNodeDir target = m_parent.m_subdirs[value];
                    ResetPath();
                    target.MergeWith(this);
                    if (!HasVirtual)
                        Parent = null;
                    return;
                }
                base.Name = value;
            }
        }

        internal SortedList<string, FSNodeFile> FilesList
        {
            get { return m_files; }
        }

        internal SortedList<string, FSNodeDir> SubDirsList
        {
            get { return m_subdirs; }
        }

        /// <summary>
        /// Returns all files contained by this FSNodeDir (excluding those from subdirectories).
        /// </summary>
        public IEnumerable<FSNodeFile> Files
        {
            get { return m_files.Values; }
        }

        /// <summary>
        /// Returns all subdirectories.
        /// </summary>
        public IEnumerable<FSNodeDir> Directories
        {
            get { return m_subdirs.Values; }
        }

        #endregion properties
    }
}