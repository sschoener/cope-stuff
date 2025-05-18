#region

using System;
using System.IO;
using System.IO.Compression;
using cope.Extensions;
using cope.FileSystem;

#endregion

namespace cope.Relic.SGA
{
    internal sealed class SGAStoredFile : IFileDescriptor
    {
        private readonly SGAEntryPoint m_entryPoint;
        private readonly SGAEntryPoint.FileEntry m_fileEntry;
        private readonly string m_sPath;

        internal SGAStoredFile(string path, SGAEntryPoint entryPoint, SGAEntryPoint.FileEntry fileEntry)
        {
            m_sPath = path;
            m_entryPoint = entryPoint;
            m_fileEntry = fileEntry;
        }

        internal SGAEntryPoint.FileEntry FileEntry { get { return m_fileEntry; } }

        #region IFileDescriptor Members

        public string GetPath()
        {
            return m_sPath;
        }

        public FileSystemPossibility GetPossibleActions()
        {
            return FileSystemPossibility.Read;
        }

        public Stream Open()
        {
            byte[] bytes = m_entryPoint.GetBytes(m_fileEntry.DataOffset, (int)m_fileEntry.CompressedSize);
            if (m_fileEntry.CompressedSize < m_fileEntry.DecompressedSize)
            {
                byte[] decompressed = new byte[m_fileEntry.DecompressedSize];
                MemoryStream ms = new MemoryStream(bytes);
                ms.ReadByte();
                ms.ReadByte(); // skip the first two bytes to accomodate .NET's implementation of Deflate
                var deflate = new DeflateStream(ms, CompressionMode.Decompress, false);
                deflate.Read(decompressed, 0, (int)m_fileEntry.DecompressedSize);
                return new MemoryStream(decompressed);
            }
            return new MemoryStream(bytes);
        }
    
        /// <summary>
        /// Not supported for SGAStoredFile.
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        /// <exception cref="RelicException">SGA stored files do not support FileOpenOptions!</exception>
        public Stream Open(FileOpenOptions opt)
        {
            throw new RelicException("SGA stored files do not support FileOpenOptions!");
        }

        public bool SupportsFileOpenOptions()
        {
            return false;
        }

        public long GetSize()
        {
            return m_fileEntry.DecompressedSize;
        }

        public DateTime GetLastModified()
        {
            return DateTimeExt.GetFromUnixTimeStamp(m_fileEntry.UnixTimeStamp);
        }

        public int CompareTo(IFileSystemEntry other)
        {
            return GetPath().CompareTo(other.GetPath());
        }

        #endregion
    }
}