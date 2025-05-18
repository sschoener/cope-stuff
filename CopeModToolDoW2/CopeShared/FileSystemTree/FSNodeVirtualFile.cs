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
using cope.DawnOfWar2.SGA;
using cope.Extensions;
using cope.DawnOfWar2;

namespace ModTool.Core
{
    public sealed class FSNodeVirtualFile : FSNodeFile
    {
        #region fields

        private SGAStoredFile m_virtual;

        #endregion fields

        #region ctors

        public FSNodeVirtualFile(string name, SGAStoredFile sgasf, FileTree tree, FSNodeDir parent = null)
            : base(name, tree, parent)
        {
            m_virtual = sgasf;
        }

        private FSNodeVirtualFile(string name, FileTree tree)
            : base(name, tree)
        {
        }

        #endregion ctors

        #region methods

        public override FSNode GClone()
        {
            var fsnvf = new FSNodeVirtualFile(m_name, m_tree) {m_virtual = m_virtual};
            return fsnvf;
        }

        public override TreeNode ConvertToTreeNode(bool usePictures = true, bool colorLocalFiles = true,
                                                   bool noLocal = false)
        {
            var file = new TreeNode(m_name) {Name = 'F' + m_name};

            bool local = HasLocal;

            if (local && !noLocal)
                file.ToolTipText = GetPath();
            else
                file.ToolTipText = m_virtual.SGA.FilePath + "::" + PathInTree;

            if (usePictures)
            {
                file.ImageIndex = m_tree.FilePic;
                file.SelectedImageIndex = m_tree.FilePic;
            }
            if (colorLocalFiles)
            {
                if (local && !noLocal)
                    file.ForeColor = Color.Red;
                else
                    file.ForeColor = Color.Blue;
            }
            return file;
        }

        /// <summary>
        /// Extracts this file to its absolute path or an optional output path.
        /// </summary>
        /// <param name="path">Set to a value to specify the file path to extract this FSNodeVirtualFile to.</param>
        /// <returns></returns>
        public FileInfo Extract(string path = null)
        {
            if (path == null)
            {
                path = Path;
            }
            string dir = path.SubstringBeforeLast('\\');
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            FileStream extractedFile = File.Create(path);
            byte[] file = m_virtual.SGA.ExtractFile(m_virtual, (m_virtual.SGA).Stream);
            extractedFile.Write(file, 0, file.Length);
            extractedFile.Flush();
            extractedFile.Close();
            return new FileInfo(path);
        }

        /// <summary>
        /// Returns a UniFile based on the information from this FSNodeVirtualFile. It will first search for a local file and if there's no local file it'll get the virtual file.
        /// </summary>
        /// <param name="onlyVirtual">Set to true to ignore local files.</param>
        /// <param name="onlyLocal">Set to true to ignore virtual files.</param>
        /// <returns></returns>
        public override UniFile GetUniFile(bool onlyVirtual = false, bool onlyLocal = false)
        {
            UniFile uniFile = null;

            if (!onlyVirtual && HasLocal)
            {
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
            // is it stored in a SGA archive?
            if (!onlyLocal || !HasLocal)
            {
                SGAStoredFile sgasfExtract = VirtualFile;
                try
                {
                    // try to extract that file for our use into RAM
                    uniFile = new UniFile(sgasfExtract.SGA.ExtractFile(sgasfExtract, (sgasfExtract.SGA.Stream)));
                }
                catch (Exception e)
                {
                     UIHelper.ShowError(
                        "An error occured while trying to load the file! Please ensure that it's SGA archive still exists! Info: {0}",
                        e.Message);
                    if (uniFile != null)
                        uniFile.Close();
                    LoggingManager.HandleException(e);
                }
                // set a pseudo path that will be used when saving the file
                if (uniFile != null)
                    uniFile.FilePath = GetPath();
            }
            return uniFile;
        }

        public override void Deconstruct()
        {
            Parent = null;
            m_virtual = null;
            m_tree = null;
        }

        public override void ResetPath()
        {
        }

        #endregion methods

        #region properties

        public SGAStoredFile VirtualFile
        {
            get { return m_virtual; }
        }

        public override string Name
        {
            get { return base.Name; }
            internal set
            {
                if (value != m_name && m_parent != null)
                {
                    string name = value;
                    if (!m_parent.ContainsFile(value))
                        new FSNodeFile(name, m_tree, m_parent);
                    m_parent.LocalCountAdd(-1);
                    if (m_tree != null)
                        m_tree.InvokeNodeFileIsVirtualChanged(this);
                }
            }
        }

        #endregion properties
    }
}