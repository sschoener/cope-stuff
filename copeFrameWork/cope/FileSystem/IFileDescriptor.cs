#region

using System;
using System.IO;

#endregion

namespace cope.FileSystem
{
    /// <summary>
    /// Interface for files of a virtual file system.
    /// </summary>
    public interface IFileDescriptor : IFileSystemEntry
    {
        /// <summary>
        /// Opens the file for any operations supported by the current file.
        /// </summary>
        /// <returns></returns>
        Stream Open();

        /// <summary>
        /// Tries to open the file with a given set of options. May not work, depending on the file system.
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        Stream Open(FileOpenOptions opt);

        /// <summary>
        /// Returns whether this file descriptor supports FileOpenOptions.
        /// </summary>
        /// <returns></returns>
        bool SupportsFileOpenOptions();

        /// <summary>
        /// Returns the size of this file in bytes.
        /// </summary>
        /// <returns></returns>
        long GetSize();

        /// <summary>
        /// Returns the date of the last modification of this file.
        /// </summary>
        /// <returns></returns>
        DateTime GetLastModified();
    }
}