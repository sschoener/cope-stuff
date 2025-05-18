#region

using System.IO;
using cope.DawnOfWar2.RelicChunky.Chunks;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.RelicChunky
{
    /// <summary>
    /// Class representing a Chunk held by a RelicChunkyFile
    /// </summary>
    public class RelicChunk : Taggable, IStreamExtBinaryCompatible, IGenericClonable<RelicChunk>
    {
        #region fields

        /// <summary>
        /// The parent of this instance of RelicChunk.
        /// </summary>
        protected FoldChunk m_parent;

        /// <summary>
        /// The initial rawData of this RelicChunk.
        /// </summary>
        protected byte[] m_rawData;

        #endregion

        #region constructors

        public RelicChunk(BinaryReader br)
        {
            GetFromStream(br);
        }

        public RelicChunk(Stream str)
        {
            GetFromStream(str);
        }

        protected RelicChunk()
        {
        }

        /// <summary>
        /// Constructs a new RelicChunk from the specified ChunkHeader.
        /// </summary>
        /// <param name="header">Header of the RelicChunk.</param>
        internal RelicChunk(RelicChunkHeader header)
        {
            ChunkHeader = header;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Header of this RelicChunk
        /// </summary>
        public RelicChunkHeader ChunkHeader { get; set; }

        /// <summary>
        /// Gets the initial rawData of this RelicChunk.
        /// </summary>
        public byte[] RawData
        {
            get { return m_rawData; }
            set
            {
                ChunkHeader.ChunkSize = (uint) value.Length;
                m_rawData = value;
            }
        }

        /// <summary>
        /// Gets or sets the _parent.
        /// </summary>
        public FoldChunk Parent
        {
            get { return m_parent; }
            set
            {
                if (m_parent != null)
                    m_parent.SubChunks.Remove(this);
                m_parent = value;
                if (m_parent != null)
                {
                    m_parent.SubChunks.Add(this);
                    ChunkHeader.FileVersion = m_parent.ChunkHeader.FileVersion;
                }
            }
        }

        #endregion

        #region methods

        internal byte[] ReadRawData(BinaryReader br)
        {
            m_rawData = br.ReadBytes((int) ChunkHeader.ChunkSize);
            return m_rawData;
        }

        public virtual void InterpretRawData()
        {
            return;
        }

        #endregion

        #region IStreamExtBinaryCompatible<RelicChunk> Member

        public virtual void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public virtual void WriteToStream(BinaryWriter bw)
        {
            long basePos = bw.BaseStream.Position;
            bw.BaseStream.Position += ChunkHeader.Length;
            bw.Write(m_rawData);
            uint chunkLength = (uint) (bw.BaseStream.Position - basePos - ChunkHeader.Length);
            ChunkHeader.ChunkSize = chunkLength;
            long endPos = bw.BaseStream.Position;
            bw.BaseStream.Position = basePos;
            ChunkHeader.WriteToStream(bw);
            bw.BaseStream.Position = endPos;
        }

        /// <summary>
        /// Reads the RelicChunk from a stream, skips the header if it's already present.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        /// <summary>
        /// Reads the RelicChunk from a stream, skips the header if it's already present.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public virtual void GetFromStream(BinaryReader br)
        {
            if (ChunkHeader == null)
            {
                ChunkHeader = new RelicChunkHeader();
                ChunkHeader.GetFromStream(br);
            }
            m_rawData = br.ReadBytes((int) ChunkHeader.ChunkSize);
        }

        #endregion

        #region IGenericClonable<RelicChunk> Member

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public virtual RelicChunk GClone()
        {
            var rc = new RelicChunk {ChunkHeader = ChunkHeader.GClone(), m_rawData = (byte[]) m_rawData.Clone()};
            return rc;
        }

        #endregion
    }
}