#region

using System.Collections.Generic;
using System.Linq;
using cope.Extensions;

#endregion

namespace cope.FileSystem
{
    /// <summary>
    /// Base class for enumerators enumerating file system entries.
    /// Recursively walks the file system starting at the given starting directory (using DFS) and enumerates its contents.
    /// </summary>
    public abstract class FileSystemEnumeratorBase
    {
        #region EnumerationOptions enum

        #endregion

        private readonly Stack<IDirectoryDescriptor> m_directories;
        private readonly IDirectoryDescriptor m_rootDir;
        private IEnumerator<IDirectoryDescriptor> m_currentDirs;
        private IEnumerator<IFileDescriptor> m_currentFiles;

        protected FileSystemEnumeratorBase(IDirectoryDescriptor startDir,
                                           EnumerationOptions options =
                                               EnumerationOptions.EnumerateDirectories |
                                               EnumerationOptions.EnumerateFiles, bool sort = false)
        {
            m_directories = new Stack<IDirectoryDescriptor>();
            m_directories.Push(startDir);
            m_rootDir = startDir;
            Options = options;
            SortValues = sort;
        }

        /// <summary>
        /// Gets whether the values returned by this FileSystemEnumerator will be returned in sorted order.
        /// Sorting takes time!
        /// </summary>
        public bool SortValues { get; private set; }

        /// <summary>
        /// Gets the EnumerationOptions for this enumerator. These set what kind of things it should enumerate.
        /// </summary>
        public EnumerationOptions Options { get; private set; }

        protected IFileSystemEntry CurrentIntern { get; private set; }

        protected bool MoveNextIntern()
        {
            if (m_currentDirs == null)
            {
                GetNextDir();
            }

            if (Options.HasFlag(EnumerationOptions.EnumerateDirectories) && m_currentDirs.MoveNext())
                CurrentIntern = m_currentDirs.Current;
            else if (Options.HasFlag(EnumerationOptions.EnumerateFiles) && m_currentFiles.MoveNext())
                CurrentIntern = m_currentFiles.Current;
            else if (m_directories.Count == 0)
                return false;
            else
            {
                m_currentDirs = null;
                m_currentFiles = null;
                return MoveNextIntern();
            }
            return true;
        }

        protected void ResetIntern()
        {
            m_directories.Clear();
            m_directories.Push(m_rootDir);
            m_currentFiles = null;
        }

        private void GetNextDir()
        {
            var newDir = m_directories.Pop();
            var dirs = newDir.GetDirectories();
            if (SortValues)
            {
                var sortedDirs = new List<IDirectoryDescriptor>(dirs);
                sortedDirs.Sort();
                dirs = sortedDirs;
            }

            m_directories.Push(dirs.Reverse());
            m_currentDirs = dirs.GetEnumerator();

            var files = newDir.GetFiles();
            if (SortValues)
            {
                var sortedFiles = new List<IFileDescriptor>(files);
                sortedFiles.Sort();
                files = sortedFiles;
            }
            m_currentFiles = files.GetEnumerator();
        }
    }
}