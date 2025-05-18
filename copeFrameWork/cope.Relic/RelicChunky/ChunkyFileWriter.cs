#region

using System.Collections.Generic;
using System.IO;

#endregion

namespace cope.Relic.RelicChunky
{
    /// <summary>
    /// Helper class used to write chunky files.
    /// </summary>
    public static class ChunkyFileWriter
    {
        /// <summary>
        /// Writes the given chunk to the specified stream using a given chunky version. By default, DoW2's chunky version is used.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunk"></param>
        /// <param name="version"></param>
        public static void Write(Stream str, Chunk chunk, uint version = ChunkyFileHeader.VERSION_DOW2)
        {
            Write(str, new[] {chunk}, version);
        }

        /// <summary>
        /// Writes the given chunks to the specified stream using a given chunky version. By default, DoW2's chunky version is used.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunks"></param>
        /// <param name="version"></param>
        public static void Write(Stream str, IEnumerable<Chunk> chunks, uint version = ChunkyFileHeader.VERSION_DOW2)
        {
            long baseOffset = str.Position;

            ChunkyFileHeader header = new ChunkyFileHeader
                                               {
                                                   Platform = ChunkyFileHeader.PLATFORM_PC,
                                                   MinVersion = 1,
                                                   Version = version
                                               };
            str.Position += header.ActualHeaderSize;

            BinaryWriter bw = new BinaryWriter(str);
            foreach (Chunk rc in chunks)
                ChunkWriter.WriteChunk(bw, rc);
            bw.BaseStream.Position = baseOffset;
            header.ChunkHeaderSize = (uint) ChunkHeader.ComputeHeaderLength((int) version, string.Empty);
            header.Write(bw);
        }

        /// <summary>
        /// Writes the given chunk to the specified stream using a given chunk info structure.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunk"></param>
        /// <param name="chunkInfo"></param>
        public static void Write(Stream str, Chunk chunk, ChunkWriter.ChunkInfo chunkInfo)
        {
            Write(str, new[] { chunk }, chunkInfo);
        }

        /// <summary>
        /// Writes the given chunks to the specified stream using a given chunk info structure.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunks"></param>
        /// <param name="chunkInfo"></param>
        public static void Write(Stream str, IEnumerable<Chunk> chunks, ChunkWriter.ChunkInfo chunkInfo)
        {
            long baseOffset = str.Position;

            ChunkyFileHeader header = new ChunkyFileHeader
            {
                Platform = ChunkyFileHeader.PLATFORM_PC,
                MinVersion = 1,
                Version = chunkInfo.FileVersion
            };
            str.Position += header.ActualHeaderSize;

            BinaryWriter bw = new BinaryWriter(str);
            foreach (Chunk rc in chunks)
                ChunkWriter.WriteChunk(bw, rc, chunkInfo);
            bw.BaseStream.Position = baseOffset;
            header.ChunkHeaderSize = (uint)ChunkHeader.ComputeHeaderLength((int)chunkInfo.FileVersion, string.Empty);
            header.Write(bw);
        }

        public static void Write(Stream str, Chunk[] chunks, ChunkWriter.ChunkInfo[] chunkInfo)
        {
            long baseOffset = str.Position;

            ChunkyFileHeader header = new ChunkyFileHeader
            {
                Platform = ChunkyFileHeader.PLATFORM_PC,
                MinVersion = 1,
                Version = chunkInfo[0].FileVersion
            };
            str.Position += header.ActualHeaderSize;

            BinaryWriter bw = new BinaryWriter(str);
            for (int i = 0; i < chunks.Length; i++)
                ChunkWriter.WriteChunk(bw, chunks[i], chunkInfo[i]);
            bw.BaseStream.Position = baseOffset;
            header.ChunkHeaderSize = (uint)ChunkHeader.ComputeHeaderLength((int)chunkInfo[0].FileVersion, string.Empty);
            header.Write(bw);
        }
    }
}