using System;
using System.IO;

namespace cope.FileSystem
{
    public class RealFileDescriptor : IFileDescriptor
    {
        protected readonly string m_sPath;

        public RealFileDescriptor(string path)
        {
            m_sPath = path;
        }

        public string FullPath
        {
            get { return m_sPath; }
        }

        #region IFileDescriptor Members

        /// <summary>
        /// Compars this instance's path to another instance's path.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IFileSystemEntry other)
        {
            return GetPath().CompareTo(other.GetPath());
        }

        /// <summary>
        /// Returns the relative path of this file.
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return m_sPath;
        }

        public virtual FileSystemPossibility GetPossibleActions()
        {
            return FileSystemPossibility.Read | FileSystemPossibility.Delete | FileSystemPossibility.Write;
        }

        public virtual Stream Open()
        {
            return File.Open(FullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }

        /// <summary>
        /// Opens this file using the specified FileOpenOptions.
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        public virtual Stream Open(FileOpenOptions opt)
        {
            return File.Open(FullPath, opt.Mode, opt.Access, opt.Share);
        }

        /// <summary>
        /// Returns whether this file supports file opening options.
        /// </summary>
        /// <returns></returns>
        public virtual bool SupportsFileOpenOptions()
        {
            return true;
        }

        /// <summary>
        /// Retuns the size of this file in bytes.
        /// </summary>
        /// <returns></returns>
        public virtual long GetSize()
        {
            return new FileInfo(FullPath).Length;
        }

        /// <summary>
        /// Returns the time this file has last been modified.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetLastModified()
        {
            return File.GetLastWriteTimeUtc(FullPath);
        }

        #endregion
    }

}