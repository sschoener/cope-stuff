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
using System.Text;
using cope;

namespace ModTool.Core
{
    // Todo: get rid of the Deconstruct method
    /// <summary>
    /// Base class for all FileSystemNodes.
    /// </summary>
    public abstract class FSNode : IGenericClonable<FSNode>
    {
        #region fields

        protected string m_path;
        protected string m_name;
        protected FSNodeDir m_parent;
        protected FileTree m_tree;

        #endregion fields

        #region ctors

        protected FSNode(string name, FileTree tree, FSNodeDir parent = null)
        {
            m_name = name;
            m_tree = tree;
            if (parent != null)
                Parent = parent;
            else
                m_parent = null;
        }

        protected FSNode(string name)
        {
            m_name = name;
        }

        #endregion ctors

        #region methods

        public override string ToString()
        {
            return m_name;
        }

        public override int GetHashCode()
        {
            int hash = m_name.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Clones this instance of FSNode.
        /// </summary>
        /// <returns></returns>
        public abstract FSNode GClone();

        /// <summary>
        /// Returns the absolute path of this FSNode: Tree.BasePath + PathInTree.
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            if (m_path != null)
                return m_tree.BasePath + m_path;
            return m_tree.BasePath + GetPathInTree();
        }

        /// <summary>
        /// Returns the path of this FSNode inside the tree.
        /// </summary>
        /// <returns></returns>
        public string GetPathInTree()
        {
            if (m_path != null)
                return m_path;
            var path = new StringBuilder(260);
            if (m_parent != null)
                m_parent.GetPathInTreeIntern(path);
            path.Append(m_name);
            m_path = path.ToString();
            return m_path;
        }

        /// <summary>
        /// Resets the path of this FSNode. The path of this FSNode within the tree is cached, as it's a rather
        /// expensive (recursive!) process to get the path.
        /// </summary>
        public virtual void ResetPath()
        {
            m_path = null;
        }

        protected void GetPathIntern(StringBuilder path)
        {
            if (m_parent != null)
                m_parent.GetPathIntern(path);
            else if (m_tree != null)
                path.Append(m_tree.BasePath);
            if (m_name == string.Empty) // root node
                return;
            path.Append(m_name);
            path.Append('\\');
        }

        protected void GetPathInTreeIntern(StringBuilder path)
        {
            if (m_parent != null)
                m_parent.GetPathInTreeIntern(path);
            else if (m_name == string.Empty)
                return;
            path.Append(m_name);
            path.Append('\\');
        }

        public void RemoveSelf(bool forceLocal = false)
        {
            if (forceLocal)
            {
                if (m_parent != null)
                {
                    m_parent.RemoveChildIntern(this, true);
                    m_parent = null;
                    m_tree = null;
                }
            }
            else
            {
                Parent = null;
            }
            ResetPath();
        }

        public abstract void Deconstruct();

        #endregion methods

        #region properties

        /// <summary>
        /// Gets the name of the file/directory of this FSNode.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return m_name;
            }
            internal set
            {
                FSNodeDir tmp = m_parent;
                if (m_parent != null)
                    m_parent.RemoveChildIntern(this, true);
                ResetPath();
                m_name = value;
                if (tmp != null)
                    tmp.AddChildIntern(this);
            }
        }

        /// <summary>
        /// Gets or sets the parent FSNodeDir containing this FSNode.
        /// </summary>
        public virtual FSNodeDir Parent
        {
            get
            {
                return m_parent;
            }
            set
            {
                if (m_parent != null)
                    m_parent.RemoveChildIntern(this);
                m_parent = value;
                if (value != null)
                {
                    value.AddChildIntern(this);
                    m_tree = value.m_tree;
                }
                if (m_parent == null)
                    m_tree = null;
            }
        }

        /// <summary>
        /// Gets whether this FSNode has a local component; it'll return true if it is a local file or contains any local files.
        /// </summary>
        public abstract bool HasLocal
        {
            get;
        }

        /// <summary>
        /// Returns the absolute path of this FSNode.
        /// </summary>
        public string Path
        {
            get
            {
                return GetPath();
            }
        }

        /// <summary>
        /// Returns the path of this FSNode within the tree.
        /// </summary>
        public string PathInTree
        {
            get
            {
                return GetPathInTree();
            }
        }

        /// <summary>
        /// Gets the FileTree associated with this FSNode.
        /// </summary>
        public FileTree Tree
        {
            get
            {
                return m_tree;
            }
            internal set
            {
                m_tree = value;
            }
        }

        #endregion properties
    }
}