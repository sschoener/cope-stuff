using System.IO;

namespace cope.Relic.RelicChunky
{
    /// <summary>
    /// Helper class for writing chunks to RelicChunky files.
    /// </summary>
    public static class ChunkWriter
    {
        /// <summary>
        /// Writes a Chunk to a Stream using a BinaryWriter. ChunkInfo may contain additional information which is used to create the chunk headers.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="chunk"></param>
        /// <param name="chunkInfo"></param>
        /// <exception cref="RelicException">Unknown chunk type!</exception>
        public static void WriteChunk(BinaryWriter bw, Chunk chunk, ChunkInfo chunkInfo)
        {
            long basePos = bw.BaseStream.Position;
            ChunkHeader header;
            if (chunk is DataChunk)
            {
                DataChunk dc = chunk as DataChunk;
                header = ChunkHeader.FromChunkInfo(chunkInfo, dc.Name, dc.Signature, ChunkType.DATA);
                bw.BaseStream.Position += header.Length;
                bw.Write(dc.GetData());
            }
            else if (chunk is FolderChunk)
            {
                FolderChunk fc = chunk as FolderChunk;
                header = ChunkHeader.FromChunkInfo(chunkInfo, fc.Name, fc.Signature, ChunkType.FOLD);
                bw.BaseStream.Position += header.Length;
                foreach (var chk in fc)
                    WriteChunk(bw, chk, chunkInfo);
            }
            else throw new RelicException("Unknown chunk type!");

            // calculate chunk length, update header and write header.
            header.ChunkSize = (uint)(bw.BaseStream.Position - basePos - header.Length);
            long end = bw.BaseStream.Position;
            bw.BaseStream.Position = basePos;
            header.WriteToStream(bw);
            bw.BaseStream.Position = end;
        }

        /// <summary>
        /// Writes a chunk to a stream and tries to find an appropriate ChunkInfo for that chunk.
        /// If it fails to do so, it will return false. In that case use one of the preset ChunkInfos provided in this class.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public static bool WriteChunk(BinaryWriter bw, Chunk chunk)
        {
            if (chunk.OriginalHeader == null)
                return false;
            WriteChunk(bw, chunk, GetInfoFromHeader(chunk.OriginalHeader));
            return true;
        }

        public struct ChunkInfo
        {
            public uint Version;
            public int MinVersion;
            public uint Flags;
            public uint FileVersion;
        }

        /// <summary>
        /// Returns a ChunkInfo structure suitable for DoW2 chunks
        /// </summary>
        public static ChunkInfo DawnOfWar2Chunk
        {
            get
            {
                return new ChunkInfo
                           {
                               Version = 2004,
                               MinVersion = 3,
                               Flags = 0,
                               FileVersion = 3
                           };
            }
        }

        /// <summary>
        /// Returns a ChunkInfo structure suitable for CoH chunks.
        /// </summary>
        public static ChunkInfo CompanyOfHeroesChunk
        {
            get { return DawnOfWar2Chunk; }
        }

        /// <summary>
        /// Returns a ChunkInfo structure suitable for CoH2 chunks.
        /// </summary>
        public static ChunkInfo CompanyOfHeroes2Chunk
        {
            get
            {
                return new ChunkInfo
                           {
                               Version = 1,
                               MinVersion = -1,
                               Flags = 0,
                               FileVersion = 3
                           };
            }
        }

        public static ChunkInfo DawnOfWar3Chunk
        {
            get
            {
                return new ChunkInfo
                {
                    Version = 2,
                    FileVersion = 4
                };
            }
        }

        private static ChunkInfo GetInfoFromHeader(ChunkHeader header)
        {
            ChunkInfo info = new ChunkInfo
                                 {
                                     Flags = header.Flags,
                                     MinVersion = header.MinVersion,
                                     Version = header.Version,
                                     FileVersion = header.FileVersion
                                 };
            return info;
        }
    }
}