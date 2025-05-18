using cope.Extensions;
using System.Collections.Generic;
using System.IO;

namespace cope.Relic.RelicChunky.ChunkTypes.KeysChunk
{
    public static class KeysReader
    {
        public static Dictionary<ulong, string> Read(Stream str)
        {
            Dictionary<ulong, string> hashes = new Dictionary<ulong, string>();
            BinaryReader br = new BinaryReader(str);
            uint numberOfEntries = br.ReadUInt32();
            for (int i = 0; i < numberOfEntries; i++)
            {
                ulong key = br.ReadUInt64();
                uint length = br.ReadUInt32();
                string value = br.ReadAsciiString((int)length);
                hashes[key] = value;
            }
            br.Close();
            return hashes;
        }
    }

    public static class KeysWriter
    {
        public static void Write(Stream str, Dictionary<ulong, string> hashes)
        {
            BinaryWriter bw = new BinaryWriter(str);
            bw.Write(hashes.Count);
            foreach (var kvp in hashes)
            {
                bw.Write(kvp.Key);
                bw.Write(kvp.Value.Length);
                bw.Write(kvp.Value.ToByteArray(true));
            }
        }
    }
}
