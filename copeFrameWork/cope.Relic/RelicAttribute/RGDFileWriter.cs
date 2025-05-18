using System;
using System.IO;
using cope.Relic.RelicChunky;
using cope.Relic.RelicChunky.ChunkTypes.GameDataChunk;

namespace cope.Relic.RelicAttribute
{
    public static class RGDFileWriter
    {
        public static void WriteRGD(AttributeTable table, Stream str, IRGDKeyConverter hashDict, ChunkWriter.ChunkInfo chunkInfo)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                RGDWriter.Write(ms, table, hashDict, (uint) chunkInfo.Version);
                ms.Flush();
                byte[] data = ms.GetBuffer();
                ms.Close();
                DataChunk rgdChunk = new DataChunk(string.Empty, "AEGD", data);
                ChunkyFileWriter.Write(str, rgdChunk, chunkInfo);
            }
            catch (Exception ex)
            {
                var excep = new RelicException(ex, "Failed to write table as RGD");
                throw excep;
            }
        }
    }
}
