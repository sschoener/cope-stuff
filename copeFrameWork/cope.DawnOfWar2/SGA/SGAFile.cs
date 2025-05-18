#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using cope.Extensions;
using cope.Helper;

#endregion

namespace cope.DawnOfWar2.SGA
{
    /// <summary>
    /// Class for READING SGAFiles. Writing is not yet implemented.
    /// </summary>
    public sealed class SGAFile : UniFile, IEnumerable<SGAEntryPoint>
    {
        // Relic choose to set a security-key for their files
        // we need to hash it together with the DataHeader from the archive to get a valid MD5 hash
        // here it is:
        private const string SECURITY_KEY_DATA_HEADER = "DFC9AF62-FC1B-4180-BC27-11CCE87D3EFF";
        private const string SECURITY_KEY_DATA = "E01519D6-2DB7-4640-AF54-0A23319C56C3";

        private SGADataHeader m_dataHeader;
        private List<SGAStoredDirectory> m_directories;
        private List<SGAEntryPoint> m_entryPoints;
        private SGAFileHeader m_fileHeader;
        private List<SGAStoredFile> m_files;

        #region ctors

        public SGAFile()
        {
        }

        public SGAFile(UniFile file)
        {
            Stream = file.Stream;
            Tag = file.Tag;
            m_filePath = file.FilePath;
        }

        public SGAFile(string path)
            : base(path)
        {
        }

        public SGAFile(string path, FileAccess access, FileShare share)
            : base(path, access, share)
        {
            Read(Stream);
        }

        #endregion ctors

        #region Helpers

        /// <summary>
        /// Helper-function to create the relationships between the EntryPoints, Directories and files.
        /// </summary>
        private void CreateRelationships()
        {
            // create the relationships between the objects
            foreach (SGAStoredDirectory sd in m_directories)
            {
                for (var i = (int) sd.DirectoryFirst; i < sd.DirectoryLast; i++)
                {
                    m_directories[i].Parent = sd;
                    sd.AddDirectory(m_directories[i]);
                }
                for (var i = (int) sd.FileFirst; i < sd.FileLast; i++)
                {
                    m_files[i].Parent = sd;
                    sd.AddFile(m_files[i]);
                }
            }

            foreach (SGAEntryPoint ep in m_entryPoints)
            {
                for (var i = (int) ep.DirectoryFirst; i < ep.DirectoryLast; i++)
                {
                    if (m_directories[i].Parent == null)
                        m_directories[i].Parent = ep;
                    if (i == ep.DirectoryFirst)
                        ep.Root = m_directories[i];
                    else
                        ep.AddDirectory(m_directories[i]);
                }
                for (var i = (int) ep.FileFirst; i < ep.FileLast; i++)
                {
                    if (m_files[i].Parent == null)
                        m_files[i].Parent = ep;
                    ep.AddFile(m_files[i]);
                }
            }
        }

        #endregion Helpers

        #region Methods

        /// <summary>
        /// Extracts the specified SGAStoredFile from this archive.
        /// </summary>
        /// <param name="sf">The SGAStoredFile which is to be extracted.</param>
        /// <param name="str">Archive stream where Position = 0 is the beginning of the Archive.</param>
        /// <returns></returns>
        public byte[] ExtractFile(SGAStoredFile sf, Stream str)
        {
            if (sf.DataCompressedSize < sf.DataUnCompressedSize /*(sf.Flags & 256) == 256 || (sf.Flags & 512) == 512*/)
            {
                var decompressed = new byte[sf.DataUnCompressedSize];
                str.Position = sf.DataOffset + m_fileHeader.DataOffset + 2;
                var deflate = new DeflateStream(str, CompressionMode.Decompress, true);
                deflate.Read(decompressed, 0, (int) sf.DataUnCompressedSize);
                return decompressed;
            }
            str.Position = sf.DataOffset + m_fileHeader.DataOffset;
            var br = new BinaryReader(str);
            return br.ReadBytes((int) sf.DataCompressedSize);
        }

        /// <summary>
        /// Returns the EntryPoint at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SGAEntryPoint GetEntryPointAt(int index)
        {
            return m_entryPoints[index];
        }

        /// <summary>
        /// Returns the index of the specified EntryPoint.
        /// </summary>
        /// <param name="ep">EntryPoint to search for.</param>
        /// <returns></returns>
        public int GetIndexOfEntryPoint(SGAEntryPoint ep)
        {
            return m_entryPoints.IndexOf(ep);
        }

        /// <summary>
        /// Removes the specified EntryPoint from the SGA.
        /// </summary>
        /// <param name="ep">The EntryPoitn to remove.</param>
        /// <returns></returns>
        public bool RemoveEntryPoint(SGAEntryPoint ep)
        {
            return m_entryPoints.Remove(ep);
        }

        /// <summary>
        /// Adds the specified EntryPoint to the SGA.
        /// </summary>
        /// <param name="ep">The EntryPoint that is to be added.</param>
        public void AddEntryPoint(SGAEntryPoint ep)
        {
            m_entryPoints.Add(ep);
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets the number of EntryPoints in this SGAArchive.
        /// </summary>
        public int EntryPointCount
        {
            get { return m_entryPoints.Count; }
        }

        /// <summary>
        /// Returns the EntryPoint at the given index.
        /// </summary>
        /// <param name="index">Index of the EntryPoint.</param>
        /// <returns></returns>
        public SGAEntryPoint this[int index]
        {
            get { return m_entryPoints[index]; }
            set { m_entryPoints[index] = value; }
        }

        #endregion Properties

        // TODO: implementieren!
        protected override void Write(Stream str)
        {
            // ACHTUNG: ALLES BLÖDSINN!

            //fileHeader.DataHeaderChecksum = CalculateDataHeaderMD5();
            //fileHeader.ContentChecksum = CalculateDataMD5();
            // ACHTUNG - STRING SECTION:
            // 1. Jeder String ist 0-terminiert!
            // 2. Es gibt auch leere Strings (vorzugsweise am Beginn der Section), die sind ebenfalls 0-terminiert.
            long baseOffset = str.Position;
            BinaryWriter writer = new BinaryWriter(str);

            str.Position += SGAFileHeader.Length;

            // 1. write entry points
            m_dataHeader.EntryPointCount = (uint) m_entryPoints.Count;
            m_dataHeader.EntryPointSectionOffset = (uint) (str.Position - baseOffset);

            foreach (var ep in m_entryPoints)
                ep.WriteToStream(writer);

            // 2. write directory entries
            m_dataHeader.DirectoryCount = (uint) m_directories.Count;
            m_dataHeader.DirectorySectionOffset = (uint) (str.Position - baseOffset);

            foreach (var de in m_directories)
                de.WriteToStream(writer);

            // 3. write file entries
            m_dataHeader.FileCount = (uint) m_files.Count;
            m_dataHeader.FileSectionOffset = (uint) (str.Position - baseOffset);

            foreach (var fe in m_files)
                fe.WriteToStream(writer);

            // 4. write files
            // 5. write data header
            // 6. write file header

            throw new NotImplementedException();
        }

        /// <exception cref="CopeDoW2Exception"><c>CopeDoW2Exception</c>.</exception>
        protected override void Read(Stream str)
        {
            long baseOffset = str.Position;
            // read file header
            m_fileHeader = new SGAFileHeader();
            try
            {
                m_fileHeader.GetFromStream(str);
            }
            catch (Exception e)
            {
                throw new CopeDoW2Exception(e, "Error reading SGA-Header of file" + FileName + ": " + e.Message);
            }

            // read data header
            str.Position = baseOffset + m_fileHeader.DataHeaderOffset;
            m_dataHeader = new SGADataHeader(m_fileHeader.VersionUpper, m_fileHeader.VersionLower);

            byte[] dataHeader = new byte[m_fileHeader.DataHeaderSize];
            str.Read(dataHeader, 0, (int) m_fileHeader.DataHeaderSize);
            var headerHash = new MD5Hash();
            headerHash.Update(SECURITY_KEY_DATA_HEADER);
            headerHash.Update(dataHeader);
            byte[] hash = headerHash.FinalizeHash();
            if (hash.IsEqual(m_fileHeader.DataHeaderChecksum))
            {
                var excep = new CopeDoW2Exception("DataHeader Hash mismatch! Computed hash does not equal stored hash.");
                excep.Data["stored hash"] = m_fileHeader.DataHeaderChecksum.ToHexString(false);
                excep.Data["computed hash"] = hash.ToHexString(false);
                throw excep;
            }
            str.Position = baseOffset + m_fileHeader.DataHeaderOffset;
            m_dataHeader.GetFromStream(str);


            // headers done. now the entry-points of the directory:
            m_entryPoints = new List<SGAEntryPoint>();
            str.Position = baseOffset + m_fileHeader.DataHeaderOffset + m_dataHeader.EntryPointSectionOffset;
            for (uint i = 0; i < m_dataHeader.EntryPointCount; i++)
            {
                m_entryPoints.Add(new SGAEntryPoint(str, m_fileHeader.VersionUpper, m_fileHeader.VersionLower));
            }
            // directories of the SGAFile
            m_directories = new List<SGAStoredDirectory>();
            str.Position = baseOffset + m_fileHeader.DataHeaderOffset + m_dataHeader.DirectorySectionOffset;
            for (uint i = 0; i < m_dataHeader.DirectoryCount; i++)
            {
                m_directories.Add(new SGAStoredDirectory(str, i, m_fileHeader.VersionUpper, m_fileHeader.VersionLower));
            }
            // files of the SGAFile
            m_files = new List<SGAStoredFile>();
            str.Position = baseOffset + m_fileHeader.DataHeaderOffset + m_dataHeader.FileSectionOffset;
            for (uint i = 0; i < m_dataHeader.FileCount; i++)
            {
                var f = new SGAStoredFile(str, i) {SGA = this};
                m_files.Add(f);
            }

            // now for the names, reading the strings for the string section
            var br = new BinaryReader(str);
            var xkcd = new StringBuilder(128);
            char c;

            long offset = baseOffset + m_fileHeader.DataHeaderOffset + m_dataHeader.StringSectionOffset;
            byte[] strings = null;

            // decrypt for archive versions v4.1 / v.51
            if (m_fileHeader.VersionLower == 1 && (m_fileHeader.VersionUpper == 5 || m_fileHeader.VersionLower == 4))
            {
                br.BaseStream.Position = offset;
                uint keyLength = br.ReadUInt32();
                byte[] key = br.ReadBytes((int) keyLength);
                uint stringsLength = br.ReadUInt32();
                strings = br.ReadBytes((int) stringsLength);

                IntPtr cryptProvider = IntPtr.Zero;
                IntPtr cryptKey = IntPtr.Zero;
                if (
                    !SGACrypt.CryptAcquireContext(ref cryptProvider, null,
                                                  "Microsoft Enhanced Cryptographic Provider v1.0",
                                                  SGACrypt.PROV_RSA_FULL,
                                                  SGACrypt.CRYPT_VERIFYCONTEXT))
                {
                    SGACrypt.CryptReleaseContext(cryptProvider, 0);
                    throw new CopeDoW2Exception("Failed to acquire crypt context!");
                }
                if (!SGACrypt.CryptImportKey(cryptProvider, key, keyLength, IntPtr.Zero, 0, ref cryptKey))
                {
                    SGACrypt.CryptDestroyKey(cryptKey);
                    SGACrypt.CryptReleaseContext(cryptProvider, 0);
                    throw new CopeDoW2Exception("Can't import RSA key from archive!");
                }
                if (!SGACrypt.CryptDecrypt(cryptKey, IntPtr.Zero, 1, 0, strings, ref stringsLength))
                {
                    SGACrypt.CryptDestroyKey(cryptKey);
                    SGACrypt.CryptReleaseContext(cryptProvider, 0);
                    throw new CopeDoW2Exception("Failed to decrypt strings from archive!");
                }
                offset += sizeof (uint) + keyLength;
                SGACrypt.CryptDestroyKey(cryptKey);
                SGACrypt.CryptReleaseContext(cryptProvider, 0);
                br.BaseStream.Position = offset;
            }

            if (strings != null)
            {
                var stringStream = new MemoryStream(strings);
                var stringReader = new BinaryReader(stringStream);
                foreach (SGAStoredDirectory sd in m_directories)
                {
                    xkcd.Clear();
                    stringReader.BaseStream.Position = sd.NameOffset;
                    while ((c = stringReader.ReadChar()) != '\0')
                    {
                        xkcd.Append(c);
                    }
                    sd.Name = xkcd.ToString();
                }
                foreach (SGAStoredFile sf in m_files)
                {
                    xkcd.Clear();
                    stringReader.BaseStream.Position = sf.NameOffset;
                    while ((c = stringReader.ReadChar()) != '\0')
                    {
                        xkcd.Append(c);
                    }
                    sf.Name = xkcd.ToString();
                }
            }
            else
            {
                foreach (SGAStoredDirectory sd in m_directories)
                {
                    xkcd.Clear();

                    br.BaseStream.Position = offset + sd.NameOffset;
                    while ((c = br.ReadChar()) != '\0')
                    {
                        xkcd.Append(c);
                    }
                    sd.Name = xkcd.ToString();
                }
                foreach (SGAStoredFile sf in m_files)
                {
                    xkcd.Clear();
                    br.BaseStream.Position = offset + sf.NameOffset;
                    while ((c = br.ReadChar()) != '\0')
                    {
                        xkcd.Append(c);
                    }
                    sf.Name = xkcd.ToString();
                }
            }

            CreateRelationships();
        }

        #region IEnumerable<SGAEntryPoint> Member

        public IEnumerator<SGAEntryPoint> GetEnumerator()
        {
            return m_entryPoints.GetEnumerator();
        }

        #endregion IEnumerable<SGAEntryPoint> Member

        #region IEnumerable Member

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_entryPoints.GetEnumerator();
        }

        #endregion IEnumerable Member
    }
}