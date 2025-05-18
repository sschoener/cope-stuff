#region

using System;
using System.Collections.Generic;
using System.Linq;
using cope.FileSystem;

#endregion

namespace cope.Relic.SGA
{
    internal sealed class SGAStoredDirectory : IDirectoryDescriptor
    {
        internal readonly SGAEntryPoint.DirectoryEntry DirEntry;
        private readonly Dictionary<string, SGAEntryPoint.Entry> m_entries;
        private readonly SGAEntryPoint m_entryPoint;

        internal SGAStoredDirectory(SGAEntryPoint entryPoint, Dictionary<string, SGAEntryPoint.Entry> entries,
                                    SGAEntryPoint.DirectoryEntry directoryEntry)
        {
            m_entryPoint = entryPoint;
            m_entries = entries;
            DirEntry = directoryEntry;
        }

        #region IStaticDirectory Members

        public string GetPath()
        {
            return DirEntry.Path;
        }

        public FileSystemPossibility GetPossibleActions()
        {
            return FileSystemPossibility.Read;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="InvalidOperationException">File creation not supported by SGAStoredDirectory.</exception>
        public void CreateFile(string name)
        {
            throw new InvalidOperationException("File creation not supported by SGAStoredDirectory.");
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="InvalidOperationException">Directory creation not supported by SGAStoredDirectory.</exception>
        public void CreateDirectory(string name)
        {
            throw new InvalidOperationException("Directory creation not supported by SGAStoredDirectory.");
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Deletion not supported by SGAStoredDirectory.</exception>
        public bool Delete(string name)
        {
            throw new InvalidOperationException("Deletion not supported by SGAStoredDirectory.");
        }

        /// <summary>
        /// Returns whether there is a file with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasFile(string name)
        {
            SGAEntryPoint.Entry entry;
            if (!m_entries.TryGetValue(name, out entry))
                return false;
            return entry is SGAEntryPoint.FileEntry;
        }

        /// <summary>
        /// Returns whether there is a directory with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasDirectory(string name)
        {
            SGAEntryPoint.Entry entry;
            if (!m_entries.TryGetValue(name, out entry))
                return false;
            return entry is SGAEntryPoint.DirectoryEntry;
        }

        /// <summary>
        /// Returns the file with the specified name. May throw exceptions if there is no such file.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public IFileDescriptor GetFile(string name)
        {
            SGAEntryPoint.Entry entry;
            if (m_entries.TryGetValue(name, out entry) && entry is SGAEntryPoint.FileEntry)
                    return m_entryPoint.GetFile(DirEntry.Path + '\\' + name, entry as SGAEntryPoint.FileEntry);
            throw new RelicException("There is no file with name: " + name);
        }

        /// <summary>
        /// Returns the directory with the specified name. May throw exceptions if there is no such directory.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public IDirectoryDescriptor GetDirectory(string name)
        {
            SGAEntryPoint.Entry entry;
            if (m_entries.TryGetValue(name, out entry) && entry is SGAEntryPoint.DirectoryEntry)
                return m_entryPoint.GetDirectory(entry as SGAEntryPoint.DirectoryEntry);
            throw new RelicException("There is no directory with name: " + name);
        }

        /// <summary>
        /// Returns all subdirectories of this directory.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDirectoryDescriptor> GetDirectories()
        {
            return from kvp in m_entries
                   where kvp.Value is SGAEntryPoint.DirectoryEntry
                   select kvp.Value as SGAEntryPoint.DirectoryEntry
                   into dir
                   select m_entryPoint.GetDirectory(dir.Path);
        }

        /// <summary>
        /// Returns all files of this directory.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IFileDescriptor> GetFiles()
        {
            var files = new List<IFileDescriptor>(m_entries.Count);
            files.AddRange(from kvp in m_entries
                           where kvp.Value is SGAEntryPoint.FileEntry
                           let dir = kvp.Value as SGAEntryPoint.FileEntry
                           select DirEntry.Path == string.Empty ? kvp.Key : DirEntry.Path + SGAEntryPoint.PATH_SEPARATOR + kvp.Key
                           into path select m_entryPoint.GetFile(path));
            return files;
        }

        public IEnumerable<IFileSystemEntry> GetAll()
        {
            IEnumerable<IFileSystemEntry> entries = from kvp in m_entries
                          where kvp.Value is SGAEntryPoint.DirectoryEntry
                          select kvp.Value as SGAEntryPoint.DirectoryEntry
                          into dir
                          select m_entryPoint.GetDirectory(dir.Path);
            entries.Concat(from kvp in m_entries
                           where kvp.Value is SGAEntryPoint.FileEntry
                           let dir = kvp.Value as SGAEntryPoint.FileEntry
                           select DirEntry.Path == string.Empty ? kvp.Key : SGAEntryPoint.PATH_SEPARATOR + kvp.Key
                           into path
                           select m_entryPoint.GetFile(path));
            return entries;
        }

        #endregion

        public int CompareTo(IFileSystemEntry other)
        {
            return GetPath().CompareTo(other.GetPath());
        }
    }
}