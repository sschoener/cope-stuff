#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.FileSystem
{
    /// <summary>
    /// Recursively walks the file system but only enumerats its files.
    /// </summary>
    public class FileWalker : IEnumerable<IFileDescriptor>
    {
        public FileWalker(IDirectoryDescriptor rootDirectory, bool sortValues = false)
        {
            RootDirectory = rootDirectory;
            SortValues = sortValues;
        }

        /// <summary>
        /// Gets the root directory this FileWalker is operating on.
        /// </summary>
        public IDirectoryDescriptor RootDirectory { get; protected set; }

        /// <summary>
        /// Gets or sets whether to sort the values prior to returning them.
        /// </summary>
        public bool SortValues { get; set; }

        #region IEnumerable<IFileDescriptor> Members

        public IEnumerator<IFileDescriptor> GetEnumerator()
        {
            return new FileEnumerator(RootDirectory, SortValues);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}