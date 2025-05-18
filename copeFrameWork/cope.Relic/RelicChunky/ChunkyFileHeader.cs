#region

using cope.Extensions;

#endregion

namespace cope.Relic.RelicChunky
{
    /// <summary>
    /// Represents the header of a RelicChunky file.
    /// </summary>
    public sealed class ChunkyFileHeader
    {
        #region fields

        public const uint PLATFORM_PC = 1;
        public const uint VERSION_COH = 3;
        public const uint VERSION_DOW2 = 3;
        public const uint VERSION_DOW3 = 4;
        public const uint STD_FILE_HEADER_SIZE = (sizeof(uint) * 5 + 16);
        public const uint OLD_FILE_HEADER_SIZE = (sizeof(uint) * 2 + 16);
        private static readonly byte[] s_signature = "Relic Chunky\x0D\x0A\x1A\x00".ToByteArray(true);
        private byte[] m_signature;
        private uint m_fileHeaderSize = STD_FILE_HEADER_SIZE;

        #endregion

        #region ctors

        public ChunkyFileHeader(System.IO.BinaryReader br)
        {
            Read(br);
        }

        /// <summary>
        /// Constructs a new RelicChunkyFileHeader without any initialized data.
        /// </summary>
        public ChunkyFileHeader()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the signature of the RelicChunkyFileHeader.
        /// </summary>
        public byte[] Signature
        {
            get { return m_signature; }
        }

        /// <summary>
        /// Gets or sets the size of the header of the first Chunk of the RelicChunkyFile.
        /// </summary>
        public uint ChunkHeaderSize { get; set; }

        /// <summary>
        /// Gets or sets the MinVersion of this RelicChunky, DawnOfWar2 uses everything including version 3.
        /// </summary>
        public uint MinVersion { get; set; }

        /// <summary>
        /// Gets or sets the Platform of this RelicChunkyFileHeader, set it to 1 for PC.
        /// </summary>
        public uint Platform { get; set; }

        /// <summary>
        /// Gets or sets the version of this RelicChunkyFileHeader, DawnOfWar2 uses everything including version 3.
        /// </summary>
        public uint Version { get; set; }

        public uint ActualHeaderSize
        {
            get
            {
                if (Version == 3)
                    return m_fileHeaderSize;
                return (uint) s_signature.Length + 2 * sizeof(uint);
            }
        }

        #endregion

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(s_signature);
            bw.Write(Version);
            bw.Write(Platform);
            if (Version == 3)
            {
                bw.Write(m_fileHeaderSize);
                bw.Write(ChunkHeaderSize);
                bw.Write(MinVersion);
            }
        }

        /// <exception cref="RelicException">This is not a RelicChunky file!</exception>
        public void Read(System.IO.BinaryReader br)
        {
            m_signature = br.ReadBytes(16);
            if (!m_signature.IsComparable(s_signature))
                throw new RelicException("This is not a RelicChunky file!");
            Version = br.ReadUInt32();
            Platform = br.ReadUInt32();
            if (Version == 3)
            {
                m_fileHeaderSize = br.ReadUInt32();
                ChunkHeaderSize = br.ReadUInt32();
                MinVersion = br.ReadUInt32();
            }
        }
    }
}