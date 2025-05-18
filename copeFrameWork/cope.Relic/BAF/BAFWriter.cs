#region

using System;
using System.Collections.Generic;
using System.IO;
using cope.Extensions;
using cope.Relic.RelicAttribute;

#endregion

namespace cope.Relic.BAF
{
    public class BAFWriter
    {
        #region fields

        private readonly Dictionary<string, uint> m_stringToIndex;
        private AttributeStructure m_attributes;

        private BinaryWriter m_dataWriter;
        private BAFHeader m_header;
        private BinaryWriter m_stringIndexWriter;
        private BinaryWriter m_stringWriter;
        private BinaryWriter m_tableArrayWriter;

        private uint m_uDataIndex;
        private uint m_uStringOffset;
        private uint m_uTableIndex;

        private Queue<AttributeTable> m_valuesToWrite;

        #endregion

        private BAFWriter()
        {
            m_stringToIndex = new Dictionary<string, uint>();
        }

        public static void Write(Stream str, AttributeStructure attrib)
        {
            var writer = new BAFWriter {m_attributes = attrib};
            writer.Write(str);
        }

        /// <exception cref="CopeException">Error while trying to write BAF-file</exception>
        private void Write(Stream str)
        {
            try
            {
                m_valuesToWrite = new Queue<AttributeTable>();
                MemoryStream dataArray = new MemoryStream();
                m_dataWriter = new BinaryWriter(dataArray);
                MemoryStream tableArray = new MemoryStream();
                m_tableArrayWriter = new BinaryWriter(tableArray);
                MemoryStream stringArray = new MemoryStream();
                m_stringWriter = new BinaryWriter(stringArray);
                MemoryStream stringIndexArray = new MemoryStream();
                m_stringIndexWriter = new BinaryWriter(stringIndexArray);

                BinaryWriter bw = new BinaryWriter(str);
                long baseOffset = str.Position;

                m_header = new BAFHeader();
                str.Position += m_header.Length;

                WriteRoot(m_attributes.Root);

                // write table array
                m_header.TableSectionOffset = (uint) (str.Position - baseOffset);
                m_header.TableCount = m_uTableIndex;
                tableArray.Flush();
                tableArray.WriteTo(str);

                // write data array
                m_header.DataSectionOffset = (uint) (str.Position - baseOffset);
                m_header.DataCount = m_uDataIndex;
                dataArray.Flush();
                dataArray.WriteTo(str);

                m_header.StringIndexCount = (uint) m_stringToIndex.Count;
                m_header.StringIndexSectionOffset = (uint) (str.Position - baseOffset);
                stringIndexArray.Flush();
                stringIndexArray.WriteTo(str);

                // write strings
                m_header.StringSectionOffset = (uint) (str.Position - baseOffset);
                stringArray.Flush();
                stringArray.WriteTo(str);

                str.Position = baseOffset + m_header.Length;
                Crc32 hash = new Crc32();
                str.SetLength(baseOffset + m_header.Length + tableArray.Length + dataArray.Length +
                              stringIndexArray.Length + stringArray.Length);
                m_header.CRC32Hash = hash.ComputeHash(str).ReverseBytes();
                str.Position = baseOffset;
                m_header.WriteToStream(bw);
                str.Flush();
                return;
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Error while trying to write BAF-file");
            }
        }

        private void WriteRoot(AttributeValue root)
        {
            m_uTableIndex = 1;
            WriteString(root.Key);
            m_valuesToWrite.Enqueue((AttributeTable) root.Data);
            while (m_valuesToWrite.Count > 0)
            {
                var tab = m_valuesToWrite.Dequeue();
                m_tableArrayWriter.Write(tab.ChildCount);
                if (tab.ChildCount == 0)
                    m_tableArrayWriter.Write(0xFFFFFFFF);
                else
                    m_tableArrayWriter.Write(m_uDataIndex);
                foreach (var data in tab)
                    WriteData(data);
            }
        }

        private uint WriteString(string str)
        {
            uint idx;
            if (m_stringToIndex.TryGetValue(str, out idx))
                return idx;

            m_stringIndexWriter.Write(m_uStringOffset);
            m_uStringOffset += (uint) (str.Length + 1);
            m_stringWriter.WriteCString(str);

            idx = (uint) (m_stringToIndex.Count);
            m_stringToIndex.Add(str, idx);
            return idx;
        }

        /// <exception cref="RelicException">Trying to get an uint for an invalid type.</exception>
        private static uint GetUInt32(AttributeValueType type)
        {
            switch (type)
            {
                case AttributeValueType.Boolean:
                    return 0;
                case AttributeValueType.Float:
                    return 1;
                case AttributeValueType.Integer:
                    return 2;
                case AttributeValueType.String:
                    return 3;
                case AttributeValueType.Table:
                    return 4;
                default:
                    throw new RelicException("Trying to get an uint for an invalid type.");
            }
        }

        private void WriteData(AttributeValue attribute)
        {
            m_dataWriter.Write((uint) attribute.DataType);
            m_dataWriter.Write(WriteString(attribute.Key));

            m_uDataIndex++;
            switch (attribute.DataType)
            {
                case RelicAttribute.AttributeValueType.Boolean:
                    uint toWrite = ((bool) attribute.Data) ? 1u : 0u;
                    m_dataWriter.Write(toWrite);
                    break;
                case RelicAttribute.AttributeValueType.Float:
                    m_dataWriter.Write((float) attribute.Data);
                    break;
                case RelicAttribute.AttributeValueType.Integer:
                    m_dataWriter.Write((int) attribute.Data);
                    break;
                case RelicAttribute.AttributeValueType.String:
                    m_dataWriter.Write(WriteString((string) attribute.Data));
                    break;
                case RelicAttribute.AttributeValueType.Table:
                    AttributeTable table = attribute.Data as AttributeTable;
                    m_dataWriter.Write(m_uTableIndex);
                    m_uTableIndex++;
                    m_valuesToWrite.Enqueue(table);
                    break;
            }
        }
    }
}