#region

using System;

#endregion

namespace cope.FileSystem
{
    /// <summary>
    /// Interface for all kinds of entries of a virtual file system.
    /// There are two properties common to all of them: They all got a path and a set of permissions.
    /// </summary>
    public interface IFileSystemEntry : IComparable<IFileSystemEntry>
    {
        /// <summary>
        /// Returns the path to this entry in the file system.
        /// </summary>
        /// <returns></returns>
        string GetPath();

        /// <summary>
        /// Gets the possible actions for this entry of the file system.
        /// </summary>
        /// <returns></returns>
        FileSystemPossibility GetPossibleActions();
    }
}