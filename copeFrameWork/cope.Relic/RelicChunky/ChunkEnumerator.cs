#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Relic.RelicChunky
{
    /// <summary>
    /// Enumerator which enumerates a chunk and all is subchunks (recursively).
    /// </summary>
    public sealed class ChunkEnumerator : IEnumerator<Chunk>
    {
        private readonly Stack<FolderChunk> m_folders;
        private IEnumerator<Chunk> m_currentChunks;

        public ChunkEnumerator(Chunk start)
        {
            m_folders = new Stack<FolderChunk>();
            if (start is FolderChunk)
                m_folders.Push(start as FolderChunk);
            else
                m_currentChunks = new List<Chunk> {start}.GetEnumerator();
        }

        #region IEnumerator<Chunk> Members

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (m_currentChunks == null)
            {
                if (m_folders.Count == 0)
                    return false;
                var folder = m_folders.Pop();
                m_currentChunks = folder.GetEnumerator();
            }

            if (!m_currentChunks.MoveNext())
            {
                m_currentChunks = null;
                return MoveNext();
            }
            if (m_currentChunks.Current is FolderChunk)
                m_folders.Push(m_currentChunks.Current as FolderChunk);
            Current = m_currentChunks.Current;
            return true;
        }

        /// <exception cref="InvalidOperationException">Not supported for objects of type ChunkEnumerator.</exception>
        public void Reset()
        {
            throw new InvalidOperationException("Not supported for objects of type ChunkEnumerator.");
        }

        public Chunk Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }
}