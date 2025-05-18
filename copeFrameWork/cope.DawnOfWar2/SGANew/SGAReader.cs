#region

using System;
using System.IO;
using System.Linq;
using System.Text;
using cope.Extensions;
using cope.Helper;

#endregion

namespace cope.DawnOfWar2.SGANew
{
    internal class SGAEntryPoint
    {
        #region properties

        public string Alias { get; set; }

        public string Name { get; set; }

        public uint IndexOfFirstDirectory { get; set; }

        public uint IndexOfLastDirectory { get; set; }

        public uint IndexOfFirstFile { get; set; }

        public uint IndexOfLastFile { get; set; }

        public uint UnknownValue { get; set; }

        #endregion

        public static SGAEntryPoint Read(BinaryReader reader, SGAVersion version)
        {
            SGAEntryPoint entryPoint = new SGAEntryPoint();
            entryPoint.Name = reader.ReadAsciiString(64).SubstringBeforeFirst('\0');
            entryPoint.Alias = reader.ReadAsciiString(64).SubstringBeforeFirst('\0');
            if (version == SGAVersion.Version5_1)
            {
                entryPoint.IndexOfFirstDirectory = reader.ReadUInt32();
                entryPoint.IndexOfLastDirectory = reader.ReadUInt32();
                entryPoint.IndexOfFirstFile = reader.ReadUInt32();
                entryPoint.IndexOfLastFile = reader.ReadUInt32();
                entryPoint.UnknownValue = reader.ReadUInt32();
            }
            else
            {
                entryPoint.IndexOfFirstDirectory = reader.ReadUInt16();
                entryPoint.IndexOfLastDirectory = reader.ReadUInt16();
                entryPoint.IndexOfFirstFile = reader.ReadUInt16();
                entryPoint.IndexOfLastFile = reader.ReadUInt16();
                entryPoint.UnknownValue = reader.ReadUInt16();
            }

            return entryPoint;
        }
    }

    internal class SGAReader : UniFile
    {
        private SGADataHeader m_dataHeader;

        private SGAEntryPoint[] m_entryPoints;
        private SGAFileHeader m_fileHeader;

        private long m_lBaseOffset;
        private BinaryReader m_reader;

        /// <exception cref="CopeDoW2Exception">Error while reading header of SGA file.</exception>
        protected override void Read(Stream str)
        {
            m_lBaseOffset = str.Position;
            m_reader = new BinaryReader(str);

            // read file header
            try
            {
                m_fileHeader = SGAFileHeader.Read(m_reader);
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex, "Error while reading header of SGA file.");
            }

            #region read data header

            str.Position = m_lBaseOffset + m_fileHeader.DataHeaderOffset;
            byte[] dataHeaderBytes = null;
            try
            {
                m_reader.ReadBytes((int) m_fileHeader.DataHeaderSize);
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex, "Error while reading data header of SGA file for hashing.");
            }

            // compare hashes
            MD5Hash dataHeaderHasher = new MD5Hash();
            dataHeaderHasher.Update(SGAConstants.DATA_HEADER_SECURITY_KEY);
            dataHeaderHasher.Update(dataHeaderBytes);
            byte[] dataHeaderHash = dataHeaderHasher.FinalizeHash();
            if (dataHeaderHash.SequenceEqual(m_fileHeader.DataHeaderChecksum))
            {
                var excep = new CopeDoW2Exception("DataHeader Hash mismatch! Computed hash does not equal stored hash.");
                excep.Data["stored hash"] = m_fileHeader.DataHeaderChecksum.ToHexString(false);
                excep.Data["computed hash"] = dataHeaderHash.ToHexString(false);
                throw excep;
            }

            // initialize data header
            try
            {
                str.Position = m_lBaseOffset + m_fileHeader.DataHeaderOffset;
                m_dataHeader = SGADataHeader.Read(m_reader, m_fileHeader.Version);
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex, "Error while reading data header of SGA file.");
            }

            #endregion

            // read directories
            // read files
            // read strings

            // now for the names, reading the strings for the string section
            var br = new BinaryReader(str);
            var xkcd = new StringBuilder(128);
            char c;
        }

        private byte[] ReadStringSection()
        {
            long offset = m_lBaseOffset + m_fileHeader.DataHeaderOffset + m_dataHeader.StringSectionOffset;
            byte[] strings = null;

            // decrypt for archive versions v4.1 / v5.1
            if (m_fileHeader.Version == SGAVersion.Version4_1 || m_fileHeader.Version == SGAVersion.Version5_1)
            {
                m_reader.BaseStream.Position = offset;
                uint keyLength = m_reader.ReadUInt32();
                byte[] key = m_reader.ReadBytes((int) keyLength);
                uint stringsLength = m_reader.ReadUInt32();
                strings = m_reader.ReadBytes((int) stringsLength);

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
                m_reader.BaseStream.Position = offset;
            }
            return strings;
        }
    }
}