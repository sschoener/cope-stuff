namespace cope.FileSystem
{
    public interface IFileSystem
    {
        /// <summary>
        /// Checks whether a file exists at the specified location.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool DoesFileExist(string path);

        /// <summary>
        /// Checks whether a directory exists at the specified location.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool DoesDirectoryExist(string path);

        /// <summary>
        /// Returns the element at the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IFileSystemEntry GetElement(string path);

        /// <summary>
        /// Returns the file at the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IFileDescriptor GetFile(string path);

        /// <summary>
        /// Returns the directory at the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IDirectoryDescriptor GetDirectory(string path);

        /// <summary>
        /// Returns the root directory of this instance of IFileSystem.
        /// </summary>
        /// <returns></returns>
        IDirectoryDescriptor GetRoot();
    }
}