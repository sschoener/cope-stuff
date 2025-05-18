#region

using System.IO;
using cope.Extensions;
using cope.Relic.RelicAttribute;

#endregion

namespace cope.Relic.RelicChunky.ChunkTypes.GameDataChunk
{
    /// <summary>
    /// Helper class to read AttributeStructures encoded as RGDs.
    /// </summary>
    public static class RGDReader
    {
        /// <exception cref="Exception">Invalid crc32</exception>
        public static AttributeStructure Read(Stream str, IRGDKeyConverter keyConverter, uint version)
        {
            BinaryReader br = new BinaryReader(str);
            uint crc32 = br.ReadUInt32(); // read CRC32
            uint dataSize = br.ReadUInt32();
            byte[] data = br.ReadBytes((int) dataSize);
            /*uint crc32Comp = Crc32.Compute(data);
            if (crc32 != crc32Comp)
                throw new RelicException("Invalid crc32");*/
            MemoryStream ms = new MemoryStream(data);
            BinaryReader memoryBr = new BinaryReader(ms);
            var root = ReadTable(memoryBr, keyConverter, version);
            return new AttributeStructure(new AttributeValue("GameData", root));
        }

        /// <exception cref="RelicException">Invalid data type.</exception>
        private static AttributeTable ReadTable(BinaryReader br, IRGDKeyConverter keyConverter, uint version, bool createList = false)
        {
            int numEntries = (int) br.ReadUInt32();
            string[] keys = new string[numEntries];
            var types = new AttributeValueType[numEntries];
            int[] offsets = new int[numEntries];
            for (int idx = 0; idx < numEntries; idx++)
            {
                if (version == 1)
                    keys[idx] = keyConverter.HashToKey(br.ReadUInt32());
                else if (version == 2)
                    keys[idx] = keyConverter.HashToKey(br.ReadUInt64());
                types[idx] = GetType(br.ReadUInt32());
                offsets[idx] = (int) br.ReadUInt32();
            }

            AttributeTable table;
            if (createList)
                table = new AttributeList();
            else
                table = new AttributeTable();
            long dataOffset = br.BaseStream.Position;
            object[] data = new object[numEntries];
            for (int idx = 0; idx < numEntries; idx++)
            {
                br.BaseStream.Position = dataOffset + offsets[idx];
                data[idx] = ReadData(types[idx], br, keyConverter, version);
                AttributeValue value = new AttributeValue(types[idx], keys[idx], data[idx]);
                table.AddValue(value);
            }
            return table;
        }

        private static object ReadData(AttributeValueType type, BinaryReader br, IRGDKeyConverter keyConverter, uint version)
        {
            switch (type)
            {
                case AttributeValueType.Boolean:
                    return br.ReadBoolean();
                case AttributeValueType.Float:
                    return br.ReadSingle();
                case AttributeValueType.Integer:
                    return br.ReadInt32();
                case AttributeValueType.String:
                    return br.ReadCString();
                case AttributeValueType.Table:
                    return ReadTable(br, keyConverter, version);
                case AttributeValueType.List:
                    return ReadTable(br, keyConverter, version, true);
                case AttributeValueType.Invalid:
                    throw new RelicException("Invalid data type.");
                default:
                    throw new RelicException("Invalid data type.");
            }
        }

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        private static AttributeValueType GetType(uint code)
        {
            if (code == 0x0)
                return AttributeValueType.Float;
            if (code == 0x1)
                return AttributeValueType.Integer;
            if (code == 0x2)
                return AttributeValueType.Boolean;
            if (code == 0x3)
                return AttributeValueType.String;
            if (code == 0x64)
                return AttributeValueType.Table;
            if (code == 0x65)
                return AttributeValueType.List;
            throw new RelicException("Unknown data type code: " + code);
        }
    }
}