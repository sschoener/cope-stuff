#region

using System.Collections.Generic;
using System.IO;
using cope.Extensions;
using cope.FileSystem;

#endregion

namespace cope.Relic.SGA
{
    /// <summary>
    /// Class representing an entry point of an SGA file (such ATTRIB from GameAttrib.sga)
    /// </summary>
    public sealed class SGAEntryPoint : IFileSystem
    {
        internal const char PATH_SEPARATOR = '\\';
        private readonly Dictionary<string, Dictionary<string, Entry>> m_entries;
        private readonly BinaryReader m_fileData;

        internal SGAEntryPoint(string name, string alias, BinaryReader fileData, Dictionary<string, Dictionary<string, Entry>> fileSystem)
        {
            Name = name;
            Alias = alias;
            m_fileData = fileData;
            m_entries = fileSystem;
        }

        public string Name { get; private set; }
        public string Alias { get; private set; }

        #region IFileSystem Members

        /// <summary>
        /// Returns whether a file exists at the specified path.
        /// A path looks like this: 'dir1\\dir2\\dir3\\filename'.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool DoesFileExist(string path)
        {
            string dir = path.SubstringBeforeLast(PATH_SEPARATOR);
            if (dir == path) // no '\\', so it is a file in the root-dir
                dir = string.Empty;

            string name = path.SubstringAfterLast(PATH_SEPARATOR);
            Dictionary<string, Entry> dict;
            if (!m_entries.TryGetValue(dir, out dict))
                return false;
            return dict.ContainsKey(name);
        }

        /// <summary>
        /// Returns whether a directory exists at the specified path.
        /// A path looks like this 'dir1\\dir2'. The root directory is at the empty-string.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool DoesDirectoryExist(string path)
        {
            path = path.Trim(PATH_SEPARATOR);
            return m_entries.ContainsKey(path);
        }

        /// <summary>
        /// Returns the FileSystemEntry at the specified path or null if there is no entry at that path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IFileSystemEntry GetElement(string path)
        {
            path = path.Trim(PATH_SEPARATOR);
            if (path == string.Empty)
                return GetRoot();

            string dir = path.SubstringBeforeLast(PATH_SEPARATOR);
            if (dir == path) // no '\\', thus an element in the root-dir
                dir = string.Empty;
            string name = path.SubstringAfterLast(PATH_SEPARATOR);
            
            Dictionary<string, Entry> dict;
            if (!m_entries.TryGetValue(dir, out dict))
                return null;
            Entry e;
            if (!dict.TryGetValue(name, out e))
                return null;
            if (e is FileEntry)
                return new SGAStoredFile(path, this, e as FileEntry);
            // e is DirectoryEntry
            DirectoryEntry dirEntry = e as DirectoryEntry;
            return new SGAStoredDirectory(this, m_entries[dirEntry.Path], dirEntry);
        }

        /// <summary>
        /// Returns the file at the specified path or null if there is no such file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IFileDescriptor GetFile(string path)
        {
            var e = GetElement(path);
            if (e is IFileDescriptor)
                return e as IFileDescriptor;
            return null;
        }

        /// <summary>
        /// Returns the directory at the specified path or null if there is no such directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IDirectoryDescriptor GetDirectory(string path)
        {
            var e = GetElement(path);
            if (e is IDirectoryDescriptor)
                return e as IDirectoryDescriptor;
            return null;
        }

        /// <summary>
        /// Returns the root directory of thie SGAEntryPoint.
        /// </summary>
        /// <returns></returns>
        public IDirectoryDescriptor GetRoot()
        {
            return new SGAStoredDirectory(this, m_entries[string.Empty], new DirectoryEntry(string.Empty));
        }

        internal IFileDescriptor GetFile(string path, FileEntry entry)
        {
            return new SGAStoredFile(path, this, entry);
        }

        internal IDirectoryDescriptor GetDirectory(DirectoryEntry entry)
        {
            return new SGAStoredDirectory(this, m_entries[entry.Path], entry);
        }

        #endregion

        /// <summary>
        /// Returns the actual size of a file within the archive or 0 if said file
        /// does not exist.
        /// </summary>
        /// <returns></returns>
        public uint GetSizeInArchive(string path)
        {
            if (!DoesFileExist(path))
                return 0;
            var file = GetFile(path) as SGAStoredFile;
            return file.FileEntry.CompressedSize;
        }

        /// <summary>
        /// Returns the absolute address of a file within the archive or 0 if there is no such file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public uint GetOffsetInArchive(string path)
        {
            if (!DoesFileExist(path))
                return 0;
            var file = GetFile(path) as SGAStoredFile;
            return file.FileEntry.DataOffset;
        }

        /// <summary>
        /// Returns whether a given path points to a compressed file in the SGA.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsCompressedInArchive(string path)
        {
            if (!DoesFileExist(path))
                return false;
            var file = GetFile(path) as SGAStoredFile;
            return file.FileEntry.IsCompressed;
        }

        internal byte[] GetBytes(uint dataOffset, int length)
        {
            m_fileData.BaseStream.Position = dataOffset;
            return m_fileData.ReadBytes(length);
        }

        #region Nested type: DirectoryEntry

        internal class DirectoryEntry : Entry
        {
            public DirectoryEntry(string path)
            {
                Path = path;
            }

            public string Path { get; protected set; }
        }

        #endregion

        #region Nested type: Entry

        internal abstract class Entry
        {
        }

        #endregion

        #region Nested type: FileEntry

        internal class FileEntry : Entry
        {
            /// <summary>
            /// Gets or sets the compressed size (in bytes) of the data of the file.
            /// </summary>
            public uint CompressedSize;

            /// <summary>
            /// Gets or sets the offset of the data of the file.
            /// </summary>
            public uint DataOffset;

            /// <summary>
            /// Gets or sets the uncompressed size (in bytes) of the data of the file.
            /// </summary>
            public uint DecompressedSize;

            /// <summary>
            /// Gets or sets the flags for this file descriptor. 0x200 seems to be a flag indicating compression.
            /// </summary>
            public ushort Flags;

            public string Name;

            /// <summary>
            /// Gets or sets the timestamp for the file.
            /// </summary>
            public uint UnixTimeStamp;

            /// <summary>
            /// The index of the file within the SGA.
            /// </summary>
            public int FileIndex;

            public FileEntry(string name, uint dataOffset, uint compressed, uint decompressed, uint timeStamp, ushort flags, int fileIndex)
            {
                Name = name;
                DataOffset = dataOffset;
                CompressedSize = compressed;
                DecompressedSize = decompressed;
                UnixTimeStamp = timeStamp;
                Flags = flags;
                FileIndex = fileIndex;
            }

            public bool IsCompressed { get { return (Flags > 0) || DecompressedSize < CompressedSize; } }
        }

        #endregion
    }
}