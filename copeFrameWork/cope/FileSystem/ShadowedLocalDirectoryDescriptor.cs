#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace cope.FileSystem
{
    /// <summary>
    /// DirectoryDescriptor used by the ShadowedLocalFileSystem class.
    /// Supports shadowing.
    /// </summary>
    public class ShadowedLocalDirectoryDescriptor : LocalDirectoryDescriptor
    {
        private readonly Dictionary<string, IFileSystemEntry> m_shadows;

        public ShadowedLocalDirectoryDescriptor(LocalFileSystem fileSystem, string path) : base(fileSystem, path)
        {
            m_shadows = new Dictionary<string, IFileSystemEntry>();
        }

        /// <summary>
        /// Returns whether the entry with the specified name has a shadow.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasShadow(string name)
        {
            return m_shadows.ContainsKey(name);
        }

        /// <summary>
        /// Returns the directory at the specified path or null if there is none.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override IDirectoryDescriptor GetDirectory(string name)
        {
            IFileSystemEntry entry;
            if (m_shadows.TryGetValue(name, out entry) && entry is ShadowedLocalDirectoryDescriptor)
                return entry as ShadowedLocalDirectoryDescriptor;
            return base.GetDirectory(name);
        }

        /// <summary>
        /// Returns the file at the specified path of null if there is none.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override IFileDescriptor GetFile(string name)
        {
            IFileSystemEntry entry;
            if (m_shadows.TryGetValue(name, out entry) && entry is ShadowedLocalFileDescriptor)
                return entry as ShadowedLocalFileDescriptor;
            return base.GetFile(name);
        }

        /// <summary>
        /// Returns whether there is a directory at the specified path.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override bool HasDirectory(string name)
        {
            IFileSystemEntry entry;
            if (m_shadows.TryGetValue(name, out entry) && entry is ShadowedLocalDirectoryDescriptor)
                return true;
            return base.HasDirectory(name);
        }

        /// <summary>
        /// Returns whether there is a file at the specified path.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override bool HasFile(string name)
        {
            IFileSystemEntry entry;
            if (m_shadows.TryGetValue(name, out entry) && entry is ShadowedLocalFileDescriptor)
                return true;
            return base.HasFile(name);
        }

        /// <summary>
        /// Returns all files in this directory.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IFileDescriptor> GetFiles()
        {
            var files =
                (IEnumerable<IFileDescriptor>)
                m_shadows.Select(kvp => kvp.Value).Where(v => v is ShadowedLocalFileDescriptor);
            return files.Concat(base.GetFiles().Where(file => !HasShadow(file.GetName())));
        }

        /// <summary>
        /// Returns all subdirectories of this directory.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IDirectoryDescriptor> GetDirectories()
        {
            var dirs =
                (IEnumerable<IDirectoryDescriptor>)
                m_shadows.Select(kvp => kvp.Value).Where(v => v is ShadowedLocalDirectoryDescriptor);
            return dirs.Concat(base.GetDirectories().Where(dir => !HasShadow(dir.GetName())));
        }

        /// <summary>
        /// Creates a new shadow directory with the specified name and adds it to this instance's shadows.
        /// </summary>
        /// <param name="name"></param>
        internal ShadowedLocalDirectoryDescriptor CreateShadowDir(string name)
        {
            var newShadow = new ShadowedLocalDirectoryDescriptor(m_fileSystem, Path.Combine(GetPath(), name));
            m_shadows.Add(name, newShadow);
            return newShadow;
        }

        /// <summary>
        /// Returns the shadow directory with the specified name or null if there is no such shadow directory.
        /// </summary>
        /// <param name="name"></param>
        internal ShadowedLocalDirectoryDescriptor GetShadowDir(string name)
        {
            IFileSystemEntry entry;
            if (m_shadows.TryGetValue(name, out entry) && entry is ShadowedLocalDirectoryDescriptor)
                return entry as ShadowedLocalDirectoryDescriptor;
            return null;
        }

        /// <summary>
        /// Adds a shadowed file with the specified name to this instance's shadows.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shadow"></param>
        internal void AddShadowFile(string name, IFileDescriptor shadow)
        {
            var file = new ShadowedLocalFileDescriptor(Path.Combine(m_sPath, name), m_fileSystem, shadow);
            m_shadows.Add(name, file);
        }
    }
}