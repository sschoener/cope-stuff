using System.Collections.Generic;
using System.IO;
using System.Linq;
using cope.Relic.RelicAttribute;

namespace cope.Relic.RelicChunky.ChunkTypes.GameDataChunk
{
    /// <summary>
    /// Helper class to write AttributeStructures encoded as RGDs.
    /// </summary>
    public static class RGDWriter
    {
        public static void Write(Stream str, AttributeTable attribTable, IRGDKeyConverter keyConverter, uint version)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            // write the actual data to the memory stream
            WriteTable(bw, attribTable, keyConverter, version);
            
            // write to the output stream and construct the header
            ms.Flush();
            byte[] data = ms.ToArray();

            BinaryWriter streamBw = new BinaryWriter(str);
            WriteHeader(streamBw, data);
            streamBw.Write(data);
            streamBw.Flush();
        }

        private static int CompareEntries(AttributeValue av1, AttributeValue av2, IRGDKeyConverter keyConverter)
        {
            ulong hash1 = keyConverter.KeyToHash(av1.Key);
            ulong hash2 = keyConverter.KeyToHash(av2.Key);
            int cmp = hash1.CompareTo(hash2);
            if (cmp == 0 && av1.DataType == av2.DataType)
            {
                switch(av1.DataType)
                {
                    case AttributeValueType.String:
                        return (av1.Data as string).CompareTo(av2.Data as string);
                    case AttributeValueType.List:
                        int child1 = (av1.Data as AttributeList).ChildCount;
                        int child2 = (av2.Data as AttributeList).ChildCount;
                        return child1.CompareTo(child2);
                    case AttributeValueType.Table:
                        child1 = (av1.Data as AttributeTable).ChildCount;
                        child2 = (av2.Data as AttributeTable).ChildCount;
                        return child1.CompareTo(child2);
                }
                return 0;
            }
            else return cmp;
        }

        private static void WriteTable(BinaryWriter bw, AttributeTable table, IRGDKeyConverter keyConverter, uint version)
        {
            bw.Write(table.ChildCount);
            int skip;
            if (version == 1)
                skip = table.ChildCount * (sizeof(int) * 2 + sizeof(uint));
            else
                skip = table.ChildCount * (sizeof(int) * 2 + sizeof(ulong));
            long basePos = bw.BaseStream.Position;
            long dataBase = basePos + skip;
            bw.BaseStream.Position = dataBase;

            // entries need to be sorted by hash
            var entryList = table.ToList();
            entryList.Sort((av1, av2) => CompareEntries(av1, av2, keyConverter));

            int[] offsets = new int[table.ChildCount];
            int i = 0;
            foreach (var entry in entryList)
            {
                int padding = GetDataSize(entry.DataType);
                long pos = bw.BaseStream.Position - dataBase;
                if (pos % padding != 0)
                    bw.BaseStream.Position += padding - pos % padding;

                offsets[i] = (int)(bw.BaseStream.Position - dataBase);
                WriteData(bw, entry, keyConverter, version);
                i++;
            }
            long terminalPos = bw.BaseStream.Position;

            bw.BaseStream.Position = basePos;
            int j = 0;
            foreach (var entry in entryList)
            {
                if (version == 1)
                    bw.Write((uint) keyConverter.KeyToHash(entry.Key));
                else if (version == 2)
                    bw.Write(keyConverter.KeyToHash(entry.Key));
                bw.Write(GetTypeCode(entry.DataType));
                bw.Write(offsets[j]);
                j++;
            }
            bw.BaseStream.Position = terminalPos;
        }

        private static int GetDataSize(AttributeValueType type)
        {
            switch (type)
            {
                case AttributeValueType.Boolean:
                    return 1;
                case AttributeValueType.Float:
                    return 4;
                case AttributeValueType.Integer:
                    return 4;
                case AttributeValueType.List:
                    return 4;
                case AttributeValueType.String:
                    return 1;
                case AttributeValueType.Table:
                    return 4;
                default:
                    return 1;
            }
        }

        private static void WriteData(BinaryWriter bw, AttributeValue value, IRGDKeyConverter keyConverter, uint version)
        {
            switch (value.DataType)
            {
                case AttributeValueType.Boolean:
                    bw.Write((bool) value.Data);
                    break;
                case AttributeValueType.Float:
                    bw.Write((float)value.Data);
                    break;
                case AttributeValueType.Integer:
                    bw.Write((int)value.Data);
                    break;
                case AttributeValueType.String:
                    string val = (string) value.Data;
                    bw.Write(val.ToByteArray(true));
                    bw.Write(false); // 0 terminated strings
                    break;
                case AttributeValueType.Table:
                    AttributeTable table = (AttributeTable) value.Data;
                    //CheckTableKeys(table);
                    WriteTable(bw, table, keyConverter, version);
                    break;
                case AttributeValueType.List:
                    AttributeList list = (AttributeList) value.Data;
                    WriteTable(bw, list, keyConverter, version);
                    break;
                case AttributeValueType.Invalid:
                    throw new RelicException("Trying to write data with invalid Attribute Value Type!");
                default:
                    throw new RelicException("Trying to write data with invalid Attribute Value Type!");
            }
        }

        private static uint GetTypeCode(AttributeValueType type)
        {
            switch (type)
            {
                case AttributeValueType.Boolean:
                    return 0x2;
                case AttributeValueType.Float:
                    return 0x0;
                case AttributeValueType.Integer:
                    return 0x1;
                case AttributeValueType.String:
                    return 0x3;
                case AttributeValueType.Table:
                    return 0x64;
                case AttributeValueType.List:
                    return 0x65;
                case AttributeValueType.Invalid:
                    throw new RelicException("Trying to write invalid Attribute Value Type!");
                default:
                    throw new RelicException("Trying to write invalid Attribute Value Type!");
            }
        }

        private static void WriteHeader(BinaryWriter bw, byte[] data)
        {
            uint crc32 = Crc32.Compute(data);
            bw.Write(crc32);
            bw.Write(data.Length);
        }

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        private static void CheckTableKeys(AttributeTable table)
        {
            HashSet<string> keys = new HashSet<string>();
            foreach (var value in table)
            {
                if (!keys.Contains(value.Key))
                    keys.Add(value.Key);
                else
                {
                    var excep = new RelicException("Can't write table to RGD, it contains entries sharing a common key.");
                    excep.Data["Key"] = value.Key;
                    excep.Data["Table"] = table.Owner.GetPath();
                    throw excep;
                }
            }

        }
    }
}