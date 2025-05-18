using cope.Relic.RelicChunky;
using cope.Relic.RelicChunky.ChunkTypes.GameDataChunk;
using System;
using System.IO;

namespace cope.Relic.RelicAttribute
{
    public static class RGDFileHelper
    {
        public static AttributeStructure ReadRGD(Stream str, IRGDKeyConverter hashDict, uint version = 1)
        {
            try
            {
                var chunks = ChunkyFileReader.Read(str);
                DataChunk chunk = (DataChunk)chunks[0];
                MemoryStream ms = new MemoryStream(chunk.GetData());
                var attrib = RGDReader.Read(ms, hashDict, version);
                ms.Close();
                return attrib;
            }
            catch (Exception ex)
            {
                var excep = new RelicException(ex, "Failed to read file as RGD");
                throw excep;
            }
        }
    }
}
