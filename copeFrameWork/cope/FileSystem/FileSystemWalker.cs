#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.FileSystem
{
    /// <summary>
    /// Recursively walks the file system.
    /// </summary>
    public class FileSystemWalker : IEnumerable<IFileSystemEntry>
    {
        public FileSystemWalker(IDirectoryDescriptor rootDir, bool sortValues = false)
        {
            RootDirectory = rootDir;
            SortValues = sortValues;
            Options = EnumerationOptions.EnumerateDirectories | EnumerationOptions.EnumerateFiles;
        }

        /// <summary>
        /// Gets the directory this FileSystemWalker is operating on.
        /// </summary>
        public IDirectoryDescriptor RootDirectory { get; protected set; }

        /// <summary>
        /// Gets or sets whether to sort the values prior to returning them.
        /// </summary>
        public bool SortValues { get; set; }

        /// <summary>
        /// Gets or sets what to enumerate.
        /// </summary>
        public EnumerationOptions Options { get; set; }

        #region IEnumerable<IFileSystemEntry> Members

        public IEnumerator<IFileSystemEntry> GetEnumerator()
        {
            return new FileSystemEnumerator(RootDirectory, Options , SortValues);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}