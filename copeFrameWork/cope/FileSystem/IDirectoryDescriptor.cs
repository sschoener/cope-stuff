#region

using System.Collections.Generic;

#endregion

namespace cope.FileSystem
{
    /// <summary>
    /// Interface for a directory of a virtual file system.
    /// A static 
    /// </summary>
    public interface IDirectoryDescriptor : IFileSystemEntry
    {
        /// <summary>
        /// Creates a new file.
        /// </summary>
        /// <param name="name"></param>
        void CreateFile(string name);

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="name"></param>
        void CreateDirectory(string name);

        /// <summary>
        /// Deletes the element with the given namen and returns whether or not the element existed prior to deletion.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Delete(string name);

        /// <summary>
        /// Returns whether or not there is a file with the specified name in this instance of DirectoryDescriptor.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool HasFile(string name);

        /// <summary>
        /// Returns whether or not therese is a directory with the specified name in this instance of DirectoryDescriptor.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool HasDirectory(string name);

        IFileDescriptor GetFile(string name);

        IDirectoryDescriptor GetDirectory(string name);

        /// <summary>
        /// Returns all subdirectories of this directory.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDirectoryDescriptor> GetDirectories();

        /// <summary>
        /// Returns all files of this directory.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IFileDescriptor> GetFiles();

        /// <summary>
        /// Returns everything in this directory.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IFileSystemEntry> GetAll();
    }
}