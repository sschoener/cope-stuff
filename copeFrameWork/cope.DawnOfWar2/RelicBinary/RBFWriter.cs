#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cope.DawnOfWar2.RelicAttribute;

#endregion

namespace cope.DawnOfWar2.RelicBinary
{
    public class RBFWriter
    {
        #region fields

        private const int KEY_BYTE_LENGTH_PADDED = 64;
        private const int DATA_INDEX_LENGTH = 4;
        private const int DATA_LENGTH_OLD = 12;
        private const int DATA_LENGTH_NEW = 8;

        private readonly bool m_bWriteRetributionFormat;
        private readonly IRBFKeyProvider m_keyProvider;
        private readonly Dictionary<string, uint> m_keyToIndex;

        private BinaryWriter m_dataIndexWriter;
        private BinaryWriter m_dataWriter;
        private RBFHeader m_header;
        private BinaryWriter m_keyWriter;

        private AttributeStructure m_rbf;
        private BinaryWriter m_stringWriter;
        private BinaryWriter m_tableArrayWriter;
        private uint m_uDataIndex;
        private uint m_uKeyIndex;
        private uint m_uTableIndex;

        #endregion

        private RBFWriter(IRBFKeyProvider keyProvider)
        {
            m_keyProvider = keyProvider;
            m_bWriteRetributionFormat = keyProvider != null;
            m_keyToIndex = new Dictionary<string, uint>();
        }

        public static void Write(Stream str, AttributeStructure rbf, IRBFKeyProvider keyProvider = null)
        {
            var writer = new RBFWriter(keyProvider) {m_rbf = rbf};
            writer.Write(str);
        }

        /// <exception cref="CopeDoW2Exception"><c>CopeDoW2Exception</c>.</exception>
        private void Write(Stream str)
        {
            try
            {
                MemoryStream dataArray = new MemoryStream();
                m_dataWriter = new BinaryWriter(dataArray);
                MemoryStream dataIndexArray = new MemoryStream();
                m_dataIndexWriter = new BinaryWriter(dataIndexArray);
                MemoryStream tableArray = new MemoryStream();
                m_tableArrayWriter = new BinaryWriter(tableArray);
                MemoryStream stringArray = new MemoryStream();
                m_stringWriter = new BinaryWriter(stringArray);
                MemoryStream keyArray = null;
                if (!m_bWriteRetributionFormat)
                {
                    keyArray = new MemoryStream();
                    m_keyWriter = new BinaryWriter(keyArray);
                }

                BinaryWriter bw = new BinaryWriter(str);
                long baseOffset = str.Position;

                m_header = new RBFHeader();
                if (m_bWriteRetributionFormat)
                    str.Position += m_header.Length - 8;
                else
                    str.Position += m_header.Length;

                WriteRoot(m_rbf.Root);

                long pos;
                // write key array if needed
                if (!m_bWriteRetributionFormat)
                {
                    pos = str.Position;
                    m_header.KeyArrayOffset = (uint) (str.Position - baseOffset);
                    m_header.KeyArrayCount = m_uKeyIndex;
                    keyArray.Flush();
                    keyArray.WriteTo(str);
                    str.Position = pos + m_uKeyIndex * KEY_BYTE_LENGTH_PADDED;
                }

                // write table array
                m_header.TableArrayOffset = (uint) (str.Position - baseOffset);
                m_header.TableArrayCount = m_uTableIndex;
                tableArray.Flush();
                tableArray.WriteTo(str);

                pos = str.Position;
                // write data index array
                m_header.DataIndexArrayOffset = (uint) (str.Position - baseOffset);
                m_header.DataIndexArrayCount = (uint) dataIndexArray.Length / 4;
                dataIndexArray.Flush();
                dataIndexArray.WriteTo(str);
                str.Position = pos + m_header.DataIndexArrayCount * DATA_INDEX_LENGTH;

                pos = str.Position;
                // write data array
                m_header.DataArrayOffset = (uint) (str.Position - baseOffset);
                m_header.DataArrayCount = m_uDataIndex;
                dataArray.Flush();
                dataArray.WriteTo(str);
                str.Position = pos + m_uDataIndex * (m_bWriteRetributionFormat ? DATA_LENGTH_NEW : DATA_LENGTH_OLD);

                // write strings
                m_header.StringSectionOffset = (uint) (str.Position - baseOffset);
                m_header.StringSectionLength = (uint) stringArray.Length;
                stringArray.Flush();
                stringArray.WriteTo(str);

                str.Position = baseOffset;
                m_header.WriteToStream(bw, m_bWriteRetributionFormat);
                return;
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex,
                                            "Error while trying to write RBF-file. RB2 mode = " +
                                            m_bWriteRetributionFormat);
            }
        }

        private void WriteRoot(AttributeValue root)
        {
            m_uTableIndex = 1;
            AttributeTable table = root.Data as AttributeTable;

            m_tableArrayWriter.Write(table.ChildCount);
            long tablePosition = m_tableArrayWriter.BaseStream.Position;
            m_tableArrayWriter.BaseStream.Position += 4;
            if (table.ChildCount == 1)
            {
                uint dataIndex = WriteData(table.First());
                m_tableArrayWriter.BaseStream.Position = tablePosition;
                m_tableArrayWriter.Write(dataIndex);
            }
            else
            {
                uint[] indices = new uint[table.ChildCount];
                int i = 0;
                foreach (var rbfChild in table)
                {
                    indices[i] = WriteData(rbfChild);
                    i++;
                }
                m_tableArrayWriter.BaseStream.Position = tablePosition;
                m_tableArrayWriter.Write((uint) m_dataIndexWriter.BaseStream.Position / 4u);
                foreach (uint idx in indices)
                    m_dataIndexWriter.Write(idx);
            }
        }

        private uint WriteKey(AttributeValue attribute)
        {
            if (!m_bWriteRetributionFormat)
            {
                if (m_keyToIndex.ContainsKey(attribute.Key))
                    return m_keyToIndex[attribute.Key];

                long pos = m_keyWriter.BaseStream.Position;
                m_keyWriter.Write(attribute.Key.ToByteArray(true));
                m_keyWriter.BaseStream.Position = pos + KEY_BYTE_LENGTH_PADDED;
                m_keyToIndex.Add(attribute.Key, m_uKeyIndex);
                uint idx = m_uKeyIndex;
                m_uKeyIndex++;
                return idx;
            }
            return 0;
        }

        private uint WriteData(AttributeValue attribute)
        {
            if (m_bWriteRetributionFormat)
            {
                m_dataWriter.Write((ushort) attribute.DataType);
                m_dataWriter.Write((ushort) GetIndexForKey(attribute.Key));
            }
            else
            {
                m_dataWriter.Write((uint) attribute.DataType);
                m_dataWriter.Write(WriteKey(attribute));
            }

            uint thisIndex = m_uDataIndex;
            m_uDataIndex++;
            switch (attribute.DataType)
            {
                case AttributeDataType.Boolean:
                    uint toWrite = ((bool) attribute.Data) ? 1u : 0u;
                    m_dataWriter.Write(toWrite);
                    break;
                case AttributeDataType.Float:
                    m_dataWriter.Write((float) attribute.Data);
                    break;
                case AttributeDataType.Integer:
                    m_dataWriter.Write((int) attribute.Data);
                    break;
                case AttributeDataType.String:
                    m_dataWriter.Write((uint) m_stringWriter.BaseStream.Position);
                    string value = attribute.Data as string;
                    m_stringWriter.Write(value.Length);
                    m_stringWriter.Write((value.ToByteArray(true)));
                    break;
                case AttributeDataType.Table:
                    AttributeTable table = attribute.Data as AttributeTable;
                    m_dataWriter.Write(m_uTableIndex);
                    m_uTableIndex++;

                    m_tableArrayWriter.Write(table.ChildCount);
                    long tablePosition = m_tableArrayWriter.BaseStream.Position;
                    m_tableArrayWriter.BaseStream.Position += 4;
                    if (table.ChildCount == 1)
                    {
                        uint dataIndex = WriteData(table.First());

                        long currentPos = m_tableArrayWriter.BaseStream.Position;
                        m_tableArrayWriter.BaseStream.Position = tablePosition;
                        m_tableArrayWriter.Write(dataIndex);
                        m_tableArrayWriter.BaseStream.Position = currentPos;
                    }
                    else
                    {
                        uint[] indices = new uint[table.ChildCount];
                        int i = 0;
                        foreach (var rbfChild in table)
                        {
                            indices[i] = WriteData(rbfChild);
                            i++;
                        }

                        long currentPos = m_tableArrayWriter.BaseStream.Position;
                        m_tableArrayWriter.BaseStream.Position = tablePosition;
                        m_tableArrayWriter.Write((uint) m_dataIndexWriter.BaseStream.Position / 4u);
                        m_tableArrayWriter.BaseStream.Position = currentPos;

                        foreach (uint idx in indices)
                            m_dataIndexWriter.Write(idx);
                    }
                    break;
            }
            return thisIndex;
        }

        /// <exception cref="CopeDoW2Exception"><c>CopeDoW2Exception</c>.</exception>
        private uint GetIndexForKey(string key)
        {
            try
            {
                int idx = m_keyProvider.GetIndexForKey(key);
                if (idx == -1)
                    idx = m_keyProvider.AddKey(key);
                return (uint) idx;
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex,
                                            "Can't get index for key '" + key +
                                            "' from KeyProvider, it's probably not a standard key.");
            }
        }
    }
}