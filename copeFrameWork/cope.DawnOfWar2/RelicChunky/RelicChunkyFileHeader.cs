#region

using cope.Extensions;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.RelicChunky
{
    public class RelicChunkyFileHeader : IStreamExtBinaryCompatible
    {
        #region fields

        public static byte[] std_signature = "Relic Chunky\x0D\x0A\x1A\x00".ToByteArray(true);
        private uint m_fileHeaderSize = (uint) (sizeof (uint) * 5 + std_signature.Length);
        private byte[] m_signature;

        #endregion

        #region ctors

        /// <summary>
        /// Constructs a new RelicChunkyFileHeader getting the data from the stream.
        /// </summary>
        /// <param name="str">Stream to fetch the data from.</param>
        public RelicChunkyFileHeader(System.IO.Stream str)
        {
            GetFromStream(str);
        }

        public RelicChunkyFileHeader(System.IO.BinaryReader br)
        {
            GetFromStream(br);
        }

        /// <summary>
        /// Constructs a new RelicChunkyFileHeader without any initialized data.
        /// </summary>
        public RelicChunkyFileHeader()
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
        /// Gets the size of the RelicChunkyFileHeader in bytes.
        /// </summary>
        public uint FileHeaderSize
        {
            get { return m_fileHeaderSize; }
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

        #endregion

        #region IStreamExtBinaryCompatible<RelicChunkyFileHeader> Member

        public void WriteToStream(System.IO.Stream str)
        {
            var bw = new System.IO.BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(std_signature);
            bw.Write(Version);
            bw.Write(Platform);
            if (Version >= 3)
            {
                bw.Write(m_fileHeaderSize);
                bw.Write(ChunkHeaderSize);
                bw.Write(MinVersion);
            }
        }

        public void GetFromStream(System.IO.Stream str)
        {
            var br = new System.IO.BinaryReader(str);
            GetFromStream(br);
        }

        /// <exception cref="CopeDoW2Exception">This is not a RelicChunky file!</exception>
        public void GetFromStream(System.IO.BinaryReader br)
        {
            m_signature = br.ReadChars(16).ToByteArray();
            if (!m_signature.IsComparable(std_signature))
                throw new CopeDoW2Exception("This is not a RelicChunky file!");
            Version = br.ReadUInt32();
            Platform = br.ReadUInt32();
            if (Version >= 3)
            {
                m_fileHeaderSize = br.ReadUInt32();
                ChunkHeaderSize = br.ReadUInt32();
                MinVersion = br.ReadUInt32();
            }
        }

        #endregion
    }
}