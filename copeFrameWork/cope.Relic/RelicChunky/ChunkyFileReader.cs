using System.Collections.Generic;
using System.IO;

namespace cope.Relic.RelicChunky
{
    public static class ChunkyFileReader
    {
        /// <summary>
        /// Reads a number of chunks encoded as an RelicChunky file from the given stream.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<Chunk> Read(Stream str)
        {
            BinaryReader br = new BinaryReader(str);
            ChunkyFileHeader fileHeader = new ChunkyFileHeader(br);
            return ChunkReader.ReadChunks(str, fileHeader.Version);
        }
    }
}