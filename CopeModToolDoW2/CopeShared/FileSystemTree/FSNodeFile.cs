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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using cope;
using cope.Extensions;
using cope.DawnOfWar2;

namespace ModTool.Core
{
    public class FSNodeFile : FSNode
    {
        #region fields

        #endregion fields

        #region ctors

        public FSNodeFile(string name, FileTree tree, FSNodeDir parent = null)
            : base(name, tree, parent)
        {
        }

        #endregion ctors

        #region methods

        public override FSNode GClone()
        {
            var copy = new FSNodeFile(m_name, m_tree);
            return copy;
        }

        public virtual TreeNode ConvertToTreeNode(bool usePictures = true, bool colorLocalFiles = true,
                                                  bool noLocal = false)
        {
            var file = new TreeNode(m_name) {Name = 'F' + m_name, ToolTipText = GetPath()};

            if (usePictures)
            {
                file.ImageIndex = m_tree.FilePic;
                file.SelectedImageIndex = m_tree.FilePic;
            }
            if (colorLocalFiles)
            {
                file.ForeColor = Color.Red;
            }
            return file;
        }

        /// <summary>
        /// Creates a UniFile based on the information from this FSNodeFile.
        /// </summary>
        /// <param name="onlyVirtual">Set to true to only return a UniFile if this file is virtual (otherwise null).</param>
        /// <param name="onlyLocal">Set to true to only return a UniFile if this file is local (otherwise null).</param>
        /// <returns></returns>
        public virtual UniFile GetUniFile(bool onlyVirtual = false, bool onlyLocal = false)
        {
            if (onlyVirtual)
                return null;
            UniFile uniFile = null;
            try
            {
                // try to open it
                uniFile = new UniFile(GetPath());
            }
            catch (Exception e)
            {
                UIHelper.ShowError(
                    "An error occured while trying to load the file! Please ensure that it still exists! Info: {0}",
                    e.Message);
                if (uniFile != null)
                    uniFile.Close();
                LoggingManager.HandleException(e);
            }
            return uniFile;
        }

        public override void Deconstruct()
        {
            m_tree = null;
            Parent = null;
        }

        #endregion methods

        #region properties

        public override string Name
        {
            get { return base.Name; }
            internal set
            {
                if (m_parent != null)
                {
                    if (m_parent.HasVirtual && m_parent.ContainsFile(value))
                    {
                        if (m_parent.FilesList[value] is FSNodeVirtualFile)
                        {
                            if (m_tree != null)
                            {
                                m_tree.InvokeNodeFileIsVirtualChanged((FSNodeVirtualFile) m_parent.FilesList[value]);
                            }
                            Parent = null;
                            return;
                        }
                    }
                }
                base.Name = value;
            }
        }

        public override bool HasLocal
        {
            get { return File.Exists(GetPath()); }
        }

        /// <summary>
        /// Returns a FileInfo object containing further information about the file referred to by this FSNodeFile.
        /// </summary>
        public FileInfo FileInfo
        {
            get { return new FileInfo(GetPath()); }
        }

        #endregion properties
    }
}