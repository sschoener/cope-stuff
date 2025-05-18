#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.FileSystem
{
    public class FileSystemEnumerator : FileSystemEnumeratorBase, IEnumerator<IFileSystemEntry>
    {
        public FileSystemEnumerator(IDirectoryDescriptor startDir,
                                    EnumerationOptions options =
                                        EnumerationOptions.EnumerateDirectories | EnumerationOptions.EnumerateFiles,
                                    bool sort = false)
            : base(startDir, options, sort)
        {
        }

        #region IEnumerator<IFileSystemEntry> Members

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return MoveNextIntern();
        }

        public void Reset()
        {
            ResetIntern();
        }

        public IFileSystemEntry Current
        {
            get { return CurrentIntern; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }
}