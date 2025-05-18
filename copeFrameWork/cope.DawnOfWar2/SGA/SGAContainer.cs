#region

using System.Collections.Generic;
using cope.Extensions;

#endregion

namespace cope.DawnOfWar2.SGA
{
    public abstract class SGAContainer : Taggable
    {
        // virtual data for editing etc.
        protected string m_name = string.Empty;
        protected SGAContainer m_parent;

        protected Dictionary<string, SGAStoredDirectory> m_storedDirectories =
            new Dictionary<string, SGAStoredDirectory>();

        protected Dictionary<string, SGAStoredFile> m_storedFiles = new Dictionary<string, SGAStoredFile>();

        #region Properties

        /// <summary>
        /// Gets or sets the index of the first directory contained by this EntryPoint.
        /// </summary>
        public uint DirectoryFirst { get; set; }

        /// <summary>
        /// Gets or sets the index of the first directory not contained by this EntryPoint anymore.
        /// </summary>
        public uint DirectoryLast { get; set; }

        /// <summary>
        /// Gets or sets the index of the first file contained by this EntryPoint.
        /// </summary>
        public uint FileFirst { get; set; }

        /// <summary>
        /// Gets or sets the index of the first file not contained by this EntryPoint anymore.
        /// </summary>
        public uint FileLast { get; set; }

        /// <summary>
        /// Gets the number of directories stored in this EntryPoint (non-recursive!).
        /// </summary>
        public int DirectoryCount
        {
            get { return m_storedDirectories.Count; }
        }

        /// <summary>
        /// Gets the number of files stored in this EntryPoint (non-recursive!).
        /// </summary>
        public int FileCount
        {
            get { return m_storedFiles.Count; }
        }

        /// <summary>
        /// Gets or sets the name of this SGAContainer.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the parent of this Container.
        /// </summary>
        public SGAContainer Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

        public Dictionary<string, SGAStoredDirectory> StoredDirectories
        {
            get { return m_storedDirectories; }
        }

        public Dictionary<string, SGAStoredFile> StoredFiles
        {
            get { return m_storedFiles; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified SGADirectory to this Container.
        /// </summary>
        /// <param name="sd">SGADirectory to add.</param>
        public bool AddDirectory(SGAStoredDirectory sd)
        {
            if (m_storedDirectories.ContainsKey(sd.m_name))
                return false;
            m_storedDirectories.Add(sd.Name, sd);
            return true;
        }

        /// <summary>
        /// Removes the specified SGADirectory from this Container.
        /// </summary>
        /// <param name="sd">SGADirectory to remove.</param>
        /// <returns></returns>
        public bool RemoveDirectory(SGAStoredDirectory sd)
        {
            return m_storedDirectories.Remove(sd.Name);
        }

        /// <summary>
        /// Adds the specified SGAFile to this Container.
        /// </summary>
        /// <param name="sf">SGAFile to add.</param>
        public bool AddFile(SGAStoredFile sf)
        {
            string path = sf.GetPath();
            if (m_storedFiles.ContainsKey(path))
                return false;
            m_storedFiles.Add(path, sf);
            return true;
        }

        /// <summary>
        /// Removes the specified SGAFile from this Container.
        /// </summary>
        /// <param name="sf">SGAFile to remove.</param>
        /// <returns></returns>
        public bool RemoveFile(SGAStoredFile sf)
        {
            return m_storedFiles.Remove(sf.Name);
        }

        /// <summary>
        /// Returns the SGADirectory with the given name / path.
        /// </summary>
        /// <param name="name">Path of the SGADirectory, e.g. "simulation\attrib\tuning\".</param>
        /// <returns></returns>
        public SGAStoredDirectory GetDirectoryAt(string name)
        {
            return m_storedDirectories[name];
        }

        /// <summary>
        /// Returns the SGAfile at the given path.
        /// </summary>
        /// <param name="name">Path of the SGAFile, e.g. "simulation\attrib\tuning\tuning_info.rbf".</param>
        /// <returns></returns>
        public SGAStoredFile GetFileAt(string name)
        {
            return m_storedFiles[name];
        }

        /// <summary>
        /// Returns the SGAStoredDirectory at the specified path or NULL if there's no such directory.
        /// </summary>
        /// <param name="path">e.g. simulation\attrib\tuning\</param>
        /// <returns></returns>
        public SGAStoredDirectory GetDirectoryFromPath(string path)
        {
            if (m_storedDirectories.ContainsKey(path))
                return m_storedDirectories[path];
            return null;
        }

        /// <summary>
        /// Returns the SGAStoredFile at the specified path or NULL if there's no such file.
        /// </summary>
        /// <param name="path">e.g. simulation\attrib\tuning\tuning.rbf</param>
        /// <returns></returns>
        public SGAStoredFile GetFileFromPath(string path)
        {
            if (m_storedFiles.ContainsKey(path))
                return m_storedFiles[path];
            return null;
        }

        /// <summary>
        /// Returns the path to this SGAContainer.
        /// </summary>
        /// <returns></returns>
        public virtual string GetPath()
        {
            if (m_parent == null)
                return m_name;
            string parentPath = m_parent.GetPath();
            if (parentPath != string.Empty)
                parentPath += '\\';
            return parentPath + m_name.SubstringAfterLast('\\');
        }

        #endregion
    }
}