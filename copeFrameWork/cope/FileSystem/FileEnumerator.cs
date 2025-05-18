#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.FileSystem
{
    public class FileEnumerator : FileSystemEnumeratorBase, IEnumerator<IFileDescriptor>
    {
        public FileEnumerator(IDirectoryDescriptor rootDirectory, bool sort = false) : base(rootDirectory, EnumerationOptions.EnumerateFiles, sort)
        {
        }

        #region IEnumerator<IFileDescriptor> Members

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

        public IFileDescriptor Current
        {
            get { return CurrentIntern as IFileDescriptor; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }
}