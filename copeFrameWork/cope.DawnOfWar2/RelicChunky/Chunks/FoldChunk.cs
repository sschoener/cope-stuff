#region

using System.Collections.Generic;
using System.IO;

#endregion

namespace cope.DawnOfWar2.RelicChunky.Chunks
{
    /// <summary>
    /// Class for all FOLD*-Chunks.
    /// </summary>
    public class FoldChunk : RelicChunk, IEnumerable<RelicChunk>
    {
        #region fields

        /// <summary>
        /// The subchunks of this folder chunk.
        /// </summary>
        protected List<RelicChunk> m_chunks;

        #endregion

        #region constructors

        internal FoldChunk()
        {
        }

        public FoldChunk(Stream str)
            : base(str)
        {
        }

        public FoldChunk(BinaryReader br)
            : base(br)
        {
        }

        #endregion

        #region methods

        public override void InterpretRawData()
        {
            if (m_chunks == null)
                m_chunks = new List<RelicChunk>();
            var ms = new MemoryStream(m_rawData);
            while (ms.Position < ChunkHeader.ChunkSize)
            {
                try
                {
                    var tmp = new RelicChunkHeader(ms, ChunkHeader.FileVersion);
                    RelicChunk chk = null;
                    switch (tmp.Type)
                    {
                        case ChunkType.DATA:
                            chk = new DataChunk();
                            break;
                        case ChunkType.FOLD:
                            chk = new FoldChunk();
                            break;
                    }
                    chk.ChunkHeader = tmp;
                    chk.GetFromStream(ms);
                    if (tmp.Type == ChunkType.FOLD)
                        chk.InterpretRawData();

                    chk.Parent = this;
                }
                catch (EndOfStreamException)
                {
                    break;
                }
            }
        }

        public override void WriteToStream(BinaryWriter bw)
        {
            long basePos = bw.BaseStream.Position;
            bw.BaseStream.Position += ChunkHeader.Length;
            foreach (RelicChunk rc in m_chunks)
            {
                rc.WriteToStream(bw);
            }
            long endPos = bw.BaseStream.Position;
            bw.BaseStream.Position = basePos;
            ChunkHeader.ChunkSize = (uint) (endPos - basePos - ChunkHeader.Length);
            ChunkHeader.WriteToStream(bw);
            bw.BaseStream.Position = endPos;
        }

        public override RelicChunk GClone()
        {
            var foldchk = new FoldChunk {ChunkHeader = ChunkHeader.GClone(), m_chunks = new List<RelicChunk>()};
            foreach (RelicChunk rc in m_chunks)
            {
                foldchk.m_chunks.Add(rc.GClone());
            }
            return foldchk;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets the list of subchunks.
        /// </summary>
        public List<RelicChunk> SubChunks
        {
            get { return m_chunks; }
            set { m_chunks = value; }
        }

        #endregion

        #region IEnumerable<RelicChunk> Member

        public IEnumerator<RelicChunk> GetEnumerator()
        {
            return m_chunks.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_chunks.GetEnumerator();
        }

        #endregion
    }
}