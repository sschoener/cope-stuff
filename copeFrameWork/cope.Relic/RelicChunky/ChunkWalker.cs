#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Relic.RelicChunky
{
    /// <summary>
    /// Enumerable which walks over a chunk and all its subchunks if it is a folder chunk.
    /// </summary>
    public class ChunkWalker : IEnumerable<Chunk>
    {
        private readonly Chunk m_start;

        public ChunkWalker(Chunk start)
        {
            m_start = start;
        }

        #region IEnumerable<Chunk> Members

        public IEnumerator<Chunk> GetEnumerator()
        {
            return new ChunkEnumerator(m_start);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}