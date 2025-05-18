#region

using System;
using System.IO;
using System.Linq;
using cope.Extensions;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.SGA
{
    public class SGAFileHeader : IStreamExtBinaryCompatible
    {
        #region fields

        private const uint LENGTH = 196;
        private static readonly byte[] s_stdSignature = "_ARCHIVE".ToByteArray(true);
        private byte[] m_contentChecksum;
        private byte[] m_dataHeaderChecksum;
        private uint m_dataHeaderOffset;
        private uint m_dataHeaderSize;
        private uint m_dataOffset;
        private uint m_flags;
        private string m_name; // UniCode string!
        private uint m_platform = 1;
        private byte[] m_signature;
        private uint m_unixTimeStamp;
        private UInt16 m_versionLower;
        private UInt16 m_versionUpper;

        #endregion

        #region ctors

        #endregion

        #region Properties

        /// <summary>
        /// Gets the signature of SGA-archives.
        /// </summary>
        public static byte[] Signature
        {
            get { return s_stdSignature; }
        }

        /// <summary>
        /// Gets the length of an SGAFileHeader in bytes.
        /// </summary>
        public static uint Length
        {
            get { return LENGTH; }
        }

        /// <summary>
        /// Gets or sets the upper part of the version of this SGAArchive, 5 is used by DoW2.
        /// </summary>
        public UInt16 VersionUpper
        {
            get { return m_versionUpper; }
            set { m_versionUpper = value; }
        }

        /// <summary>
        /// Gets or sets the upper part of the version of this SGAArchive, 0 is used by DoW2.
        /// </summary>
        public UInt16 VersionLower
        {
            get { return m_versionLower; }
            set { m_versionLower = value; }
        }

        /// <summary>
        /// Gets or sets the checksum for the content of the SGAArchive.
        /// </summary>
        public byte[] ContentChecksum
        {
            get { return m_contentChecksum; }
            set { m_contentChecksum = value; }
        }

        /// <summary>
        /// Gets or sets the checksum for the DataHeader of this SGAArchive.
        /// </summary>
        public byte[] DataHeaderChecksum
        {
            get { return m_dataHeaderChecksum; }
            set { m_dataHeaderChecksum = value; }
        }

        /// <summary>
        /// Gets or sets the name of the SGA belonging to this header. Unicode string.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the size of the DataHeader of the SGA belonging to this header.
        /// </summary>
        public uint DataHeaderSize
        {
            get { return m_dataHeaderSize; }
            set { m_dataHeaderSize = value; }
        }

        /// <summary>
        /// Gets or sets the offset of the DataSection relative to the beginning of the file of the SGA belonging to this header.
        /// </summary>
        public uint DataOffset
        {
            get { return m_dataOffset; }
            set { m_dataOffset = value; }
        }

        /// <summary>
        /// Gets or sets the offset of the DataHeader relative to the beginning of the file of the SGA belonging to this header.
        /// </summary>
        public uint DataHeaderOffset
        {
            get { return m_dataHeaderOffset; }
            set { m_dataHeaderOffset = value; }
        }

        /// <summary>
        /// Gets or sets the platform of this SGAFileHeader. Use 1 for PC.
        /// </summary>
        public uint Platform
        {
            get { return m_platform; }
            set { m_platform = value; }
        }

        /// <summary>
        /// Gets or sets the Flags for this header. They're normally set to 0 although I don't have any idea what they're there for.
        /// </summary>
        public uint Flags
        {
            get { return m_flags; }
            set { m_flags = value; }
        }

        /// <summary>
        /// Gets or sets the TimeStamp of this header.
        /// </summary>
        public uint UnixTimeStamp
        {
            get { return m_unixTimeStamp; }
            set { m_unixTimeStamp = value; }
        }

        #endregion

        #region IStreamExtBinaryCompatible<SGAFileHeader> Member

        public void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(BinaryWriter bw)
        {
            bw.Write(s_stdSignature);
            bw.Write(m_versionUpper);
            bw.Write(m_versionLower);
            bw.Write(m_contentChecksum);

            long curPos = bw.BaseStream.Position;
            bw.Write(m_name.ToByteArray());
            bw.BaseStream.Position = curPos + 128;

            bw.Write(m_dataHeaderChecksum);
            bw.Write(m_dataHeaderSize);
            bw.Write(m_dataOffset);
            if (m_versionUpper >= 5)
                bw.Write(m_dataHeaderOffset);
            if (m_versionUpper >= 4)
            {
                bw.Write(m_platform);
                if (m_versionUpper >= 5)
                {
                    bw.Write(m_flags);
                    bw.Write(m_unixTimeStamp);
                }
            }
        }

        public void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        /// <exception cref="CopeDoW2Exception">Unknown signature: ! Please ensure that the file you're trying to load is an SGA-File!</exception>
        public void GetFromStream(BinaryReader br)
        {
            m_signature = br.ReadBytes(8);
            if (!m_signature.SequenceEqual(s_stdSignature))
            {
                throw new CopeDoW2Exception("Unknown signature: " + m_signature.ToString(true) +
                                            "! Please ensure that the file you're trying to load is an SGA-File!");
            }
            m_versionUpper = br.ReadUInt16();
            m_versionLower = br.ReadUInt16();
            if (m_versionUpper < 4 || (m_versionLower != 0 && m_versionUpper != 5))
                throw new CopeDoW2Exception("Unsupported SGA-Version: " + m_versionUpper + "." + m_versionLower +
                                            "! Only version 5/5.1 (DoW2/CoH: Online America) and version 4/4.1 (CoH/CoH: Online China) are supported!");
            m_contentChecksum = br.ReadBytes(16);

            // the name is UniCode, padded to be 128 bytes in size
            m_name = br.ReadBytes(128).ToString(false).SubstringBeforeFirst('\0');

            // another checksum. fantastic!
            m_dataHeaderChecksum = br.ReadBytes(16);
            m_dataHeaderSize = br.ReadUInt32();
            m_dataOffset = br.ReadUInt32();
            if (m_versionUpper >= 5 && m_versionLower != 1)
                m_dataHeaderOffset = br.ReadUInt32();
            if (m_versionUpper >= 4)
            {
                m_platform = br.ReadUInt32();
                if (m_platform != 1)
                    throw new CopeDoW2Exception("Unknown SGA-platform: " + m_platform +
                                                "! Only platform 1 is supported!");
                if (m_versionUpper >= 5 && m_versionLower != 1)
                {
                    m_flags = br.ReadUInt32();
                    m_unixTimeStamp = br.ReadUInt32();
                }
            }
            if (m_versionUpper < 5 || m_versionLower == 1)
                m_dataHeaderOffset = (uint) br.BaseStream.Position;
        }

        #endregion
    }
}