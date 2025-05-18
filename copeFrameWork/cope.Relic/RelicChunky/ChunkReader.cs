using System;
using System.Collections.Generic;
using System.IO;
using cope.Extensions;

namespace cope.Relic.RelicChunky
{
    public static class ChunkReader
    {
        /// <summary>
        /// Will read a chunk from a give binary reader.
        /// </summary>
        /// <param name="br"></param>
        /// <param name="fileVersion">Version of the chunky file containing this chunk.</param>
        /// <returns></returns>
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="br" /> is <c>null</c>.</exception>
        public static Chunk ReadChunk(BinaryReader br, uint fileVersion)
        {
            if (br == null) throw new ArgumentNullException("br");
            var header = new ChunkHeader(fileVersion);
            header.GetFromStream(br);

            byte[] data = br.ReadBytes((int)header.ChunkSize);
            switch (header.Type)
            {
                case ChunkType.DATA:
                    return new DataChunk(header, data);
                case ChunkType.FOLD:
                    MemoryStream ms = new MemoryStream(data);
                    var chunks = ReadChunks(ms, fileVersion);
                    ms.Close();
                    return new FolderChunk(header, chunks);
                default:
                    throw new RelicException("Unknown chunk type: " + header.Type);
            }
        }

        /// <summary>
        /// Reads all chunks from a stream.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fileVersion">Version of the chunky file containing the chunks.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="str" /> is <c>null</c>.</exception>
        public static List<Chunk> ReadChunks(Stream str, uint fileVersion)
        {
            if (str == null) throw new ArgumentNullException("str");
            BinaryReader br = new BinaryReader(str);
            List<Chunk> chunks = new List<Chunk>();
            while (!br.IsAtEnd())
                chunks.Add(ReadChunk(br, fileVersion));
            return chunks;
        }
    }
}