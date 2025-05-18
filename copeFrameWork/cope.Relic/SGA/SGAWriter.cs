#region

using System;
using System.Collections.Generic;
using System.IO;
using cope.Extensions;

#endregion

namespace cope.Relic.SGA
{
    /// <summary>
    /// Writes SGA files in V6 format (CoH2).
    /// </summary>
    public class SGAWriter
    {
        private readonly BinaryWriter m_binaryWriter;
        private readonly SGAWriterSettings m_sgaWriterSettings;
        private readonly string m_strBasePath;
        private readonly Stream m_stream;

        private SGADataHeader m_dataHeader;

        private List<DirectoryDescriptor> m_dirs;
        private SGAFileHeader m_fileHeader;
        private List<FileDescriptor> m_files;
        private uint m_stringCount;
        private BinaryWriter m_strings;

        private SGAWriter(Stream stream, string strBasePath, SGAWriterSettings sgaWriterSettings)
        {
            m_sgaWriterSettings = sgaWriterSettings;
            m_strBasePath = strBasePath;
            m_stream = stream;
            m_binaryWriter = new BinaryWriter(stream);
        }

        private void Write()
        {
            MemoryStream strings = new MemoryStream();
            m_strings = new BinaryWriter(strings);


            // Build the directory tree, creating the directory descriptors on the way.
            List<string> files = BuildDirectoryTree();
            ReadFiles(files);

            m_strings.Flush();

            m_fileHeader = new SGAFileHeader(SGAVersion.Version6_0);
            m_fileHeader.ArchiveName = m_sgaWriterSettings.ArchiveName;
            const int entryPointSize = 2 * 64 + 5 * sizeof (uint);
            const int headerSize = 8 * sizeof (int);
            int dataHeaderSize = (int) (entryPointSize + headerSize + m_files.Count * FileDescriptor.SIZE +
                                        m_dirs.Count * DirectoryDescriptor.SIZE + m_strings.BaseStream.Length);
            m_fileHeader.DataHeaderSize = (uint) dataHeaderSize;

            const int fileHaderSize = 152;
            m_fileHeader.DataOffset = (uint) (dataHeaderSize + fileHaderSize);
            SGAFileHeader.Write(m_binaryWriter, m_fileHeader);

            long dataHeaderPosition = m_binaryWriter.BaseStream.Position;

            m_dataHeader = new SGADataHeader();
            m_dataHeader.EntryPointCount = 1;
            m_dataHeader.EntryPointSectionOffset = 8 * sizeof (uint);
            m_dataHeader.DirectoryCount = (uint) m_dirs.Count;
            m_dataHeader.DirectorySectionOffset = m_dataHeader.EntryPointSectionOffset + entryPointSize;
            m_dataHeader.FileCount = (uint) m_files.Count;
            m_dataHeader.FileSectionOffset = (uint) (m_dataHeader.DirectorySectionOffset +
                                                     m_dirs.Count * DirectoryDescriptor.SIZE);
            m_dataHeader.StringCount = m_stringCount;
            m_dataHeader.StringSectionOffset =
                (uint) (m_dataHeader.FileSectionOffset + m_files.Count * FileDescriptor.SIZE);
            SGADataHeader.Write(m_binaryWriter, SGAVersion.Version6_0, m_dataHeader);

            // write the actual file data
            long entryPointPosition = m_binaryWriter.BaseStream.Position;
            m_binaryWriter.BaseStream.Position = dataHeaderPosition + dataHeaderSize;

            WriteFileData(m_binaryWriter.BaseStream.Position);

            m_binaryWriter.BaseStream.Position = entryPointPosition;

            WriteEntryPoint();
            WriteDirectories();
            WriteFiles();
            m_binaryWriter.Write(strings.ToArray());

            m_binaryWriter.Flush();
            m_binaryWriter.Close();

            m_strings.Close();
        }

        private void WriteFileData(long baseOffset)
        {
            foreach (var fd in m_files)
            {
                fd.DataOffset = (uint)(m_binaryWriter.BaseStream.Position - baseOffset);
                var stream = File.Open(fd.Path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                fd.CRC32 = Crc32.Compute(stream);
                stream.Position = 0;
                if (m_sgaWriterSettings.UseCompression)
                {
                    var compressed = Compress(stream);
                    if (compressed.Length < fd.DataSize)
                    {
                        fd.DataSizeCompressed = (uint)compressed.Length;
                        m_binaryWriter.Write(compressed, 0, compressed.Length);
                    }
                    else
                    {
                        fd.DataSizeCompressed = fd.DataSize;
                        stream.CopyTo(m_binaryWriter.BaseStream);
                    }
                }
                else
                {
                    fd.DataSizeCompressed = fd.DataSize;
                    stream.CopyTo(m_binaryWriter.BaseStream);
                }
            }
        }

        private void WriteFiles()
        {
            foreach (var fd in m_files)
            {
                m_binaryWriter.Write(fd.NameOffset);
                m_binaryWriter.Write(fd.DataOffset);
                m_binaryWriter.Write(fd.DataSizeCompressed);
                m_binaryWriter.Write(fd.DataSize);
                m_binaryWriter.Write(fd.TimeStamp);
                const ushort flag = 0;
                const ushort compressedFlag = 0x0200;
                if (fd.DataSizeCompressed < fd.DataSize)
                    m_binaryWriter.Write(compressedFlag);
                else
                    m_binaryWriter.Write(flag);
                m_binaryWriter.Write(fd.CRC32);
            }
        }

        private void WriteDirectories()
        {
            foreach (var dd in m_dirs)
            {
                m_binaryWriter.Write(dd.NameOffset);
                m_binaryWriter.Write(dd.FirstDir);
                m_binaryWriter.Write(dd.LastDir);
                m_binaryWriter.Write(dd.FirstFile);
                m_binaryWriter.Write(dd.LastFile);
            }
        }

        private void WriteEntryPoint()
        {
            long basePos = m_binaryWriter.BaseStream.Position;
            m_binaryWriter.Write(m_sgaWriterSettings.EntryPointType.ToByteArray(true));
            m_binaryWriter.BaseStream.Position = basePos + 64;
            m_binaryWriter.Write(m_sgaWriterSettings.EntryPointName.ToByteArray(true));
            m_binaryWriter.BaseStream.Position = basePos + 128;
            m_binaryWriter.Write(0); // first directory
            m_binaryWriter.Write(m_dirs.Count); // last directory
            m_binaryWriter.Write(0); // first file
            m_binaryWriter.Write(m_files.Count); // last file
            m_binaryWriter.Write(0); // ???
        }

        private static byte[] Compress(Stream input)
        {
            using (var compressStream = new MemoryStream())
            {
                using (var compressor = new Ionic.Zlib.ZlibStream(compressStream, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
                {
                    input.CopyTo(compressor);
                    compressor.Close();
                    return compressStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Reads the files given by the input IEnumerable of files; it will create all file descriptors,
        /// but they will lack their CRC32s and compressed size. Furthermore, their data will not have been
        /// written to the output stream yet.
        /// </summary>
        /// <param name="paths"></param>
        private void ReadFiles(IEnumerable<string> paths)
        {
            m_files = new List<FileDescriptor>();
            foreach (string f in paths)
            {
                FileDescriptor fd = new FileDescriptor
                                        {
                                            NameOffset = (uint) m_strings.BaseStream.Position,
                                            Path = f
                                        };
                string nameToStore = f.SubstringAfterLast('\\');
                m_strings.WriteCString(nameToStore);
                m_stringCount++;

                FileInfo fi = new FileInfo(f);
                fd.DataSize = (uint) fi.Length;
                fd.TimeStamp = fi.LastWriteTime.GetUnixTimeStamp();

                m_files.Add(fd);
            }
        }

        /// <summary>
        /// Constructs the list of directory descriptors and returns a list of file paths.
        /// </summary>
        /// <returns></returns>
        private List<String> BuildDirectoryTree()
        {
            var paths = new Queue<string>();
            paths.Enqueue(m_strBasePath);

            m_dirs = new List<DirectoryDescriptor>();
            var files = new List<string>();
            uint directoryCounter = 1;
            uint fileCounter = 0;

            while (paths.Count > 0)
            {
                string current = paths.Dequeue();
                var dd = new DirectoryDescriptor {FirstDir = directoryCounter, FirstFile = fileCounter};

                // collect directories
                foreach (var d in Directory.EnumerateDirectories(current))
                {
                    paths.Enqueue(d);
                    directoryCounter++;
                }
                dd.LastDir = directoryCounter;

                // collect files
                foreach (var f in Directory.EnumerateFiles(current))
                {
                    files.Add(f);
                    fileCounter++;
                }
                dd.LastFile = fileCounter;

                // write strings
                dd.NameOffset = (uint) m_strings.BaseStream.Position;
                string nameToStore = current.SubstringAfterFirst(m_strBasePath);
                m_strings.WriteCString(nameToStore);
                m_stringCount++;

                m_dirs.Add(dd);
            }
            return files;
        }

        /// <summary>
        /// Writes a version 6 SGA into the target stream using the files from
        /// the given basepath.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="basepath"></param>
        /// <param name="sgaWriterSettings"></param>
        public static void Write(Stream stream, string basepath, SGAWriterSettings sgaWriterSettings)
        {
            basepath = Path.GetFullPath(basepath);
            if (!basepath.EndsWith('\\'))
                basepath += '\\';
            new SGAWriter(stream, basepath, sgaWriterSettings).Write();
        }

        #region Nested type: DirectoryDescriptor

        private class DirectoryDescriptor
        {
            public const int SIZE = 5 * sizeof (int);
            public uint FirstDir;
            public uint FirstFile;
            public uint LastDir;
            public uint LastFile;
            public uint NameOffset;
        }

        #endregion

        #region Nested type: FileDescriptor

        private class FileDescriptor
        {
            public const int SIZE = 6 * sizeof (uint) + sizeof (ushort);
            public uint CRC32;
            public uint DataOffset;
            public uint DataSize;
            public uint DataSizeCompressed;
            public uint NameOffset;
            public uint TimeStamp;

            public string Path;
        }

        #endregion
    }
}