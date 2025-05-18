#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Relic.RelicChunky
{
    /// <summary>
    /// Represents a FolderChunk, which basically holds other chunks.
    /// </summary>
    public class FolderChunk : Chunk, IEnumerable<Chunk>
    {
        protected List<Chunk> m_chunks;

        public FolderChunk(ChunkHeader header, IEnumerable<Chunk> chunks) : base(header)
        {
            SetChunks(chunks);
        }

        public FolderChunk(IEnumerable<Chunk> chunks)
        {
            SetChunks(chunks);
        }

        #region IEnumerable<Chunk> Members

        public IEnumerator<Chunk> GetEnumerator()
        {
            return m_chunks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private void SetChunks(IEnumerable<Chunk> chunks)
        {
            m_chunks = new List<Chunk>(chunks);
            foreach (var c in chunks)
                c.Parent = this;
        }

        public override string ToString()
        {
            string retVal;
            if (string.IsNullOrWhiteSpace(Name))
                retVal = "FOLD" + Signature;
            else
                retVal = "FOLD" + Signature + " - " + Name;
            retVal += " - children: " + m_chunks.Count;
            return retVal;
        }
    }
}