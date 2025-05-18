#region

using System;
using System.IO;
using cope.Extensions;
using cope.Relic.RelicAttribute;

#endregion

namespace cope.Relic.BAF
{
    public class BAFReader
    {
        #region fields

        private AttributeValue[] m_attributeValues;
        private BAFHeader m_header;
        private long m_lBaseOffset;
        private BinaryReader m_reader;
        private string[] m_sStrings;
        private AttributeTable[] m_tables;

        #endregion

        private BAFReader()
        {
        }

        public static AttributeStructure Read(Stream str)
        {
            BAFReader reader = new BAFReader();
            return reader.ReadIntern(str);
        }

        private void ReadStrings()
        {
            m_reader.BaseStream.Position = m_lBaseOffset + m_header.StringIndexSectionOffset;
            int[] stringIndices = m_reader.ReadInt32Array((int) m_header.StringIndexCount);
            long baseStringPos = m_lBaseOffset + m_header.StringSectionOffset;
            m_reader.BaseStream.Position = baseStringPos;
            m_sStrings = new string[(int) m_header.StringIndexCount];
            for (int i = 0; i < stringIndices.Length; i++)
            {
                m_reader.BaseStream.Position = baseStringPos + stringIndices[i];
                m_sStrings[i] = m_reader.ReadCString();
            }
        }

        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        private AttributeStructure ReadIntern(Stream str)
        {
            try
            {
                m_reader = new BinaryReader(str);
                m_lBaseOffset = str.Position;

                m_header = new BAFHeader(m_reader);
                ReadStrings();

                // the data items already reference the tables so it is a good idea
                // to setup the tables before reading the data
                m_tables = new AttributeTable[m_header.TableCount];
                for (int i = 0; i < m_tables.Length; i++)
                    m_tables[i] = new AttributeTable();

                // read the data
                m_attributeValues = ReadDataValues();

                // declare the root element
                // per definitionem the root always holds the first table-entry
                AttributeValue root = new AttributeValue(RelicAttribute.AttributeValueType.Table, m_sStrings[0], m_tables[0]);
                AssignDataToTables();
                return new AttributeStructure(root);
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Failed to read BAF file.");
            }
        }

        private AttributeValueType GetType(uint type)
        {
            switch (type)
            {
                case 0:
                    return AttributeValueType.Boolean;
                case 1:
                    return AttributeValueType.Float;
                case 2:
                    return AttributeValueType.Integer;
                case 3:
                    return AttributeValueType.String;
                case 4:
                    return AttributeValueType.Table;
                default:
                    return AttributeValueType.Invalid;
            }
        }

        private AttributeValue[] ReadDataValues()
        {
            m_reader.BaseStream.Position = m_lBaseOffset + m_header.DataSectionOffset;
            AttributeValue[] values = new AttributeValue[m_header.DataCount];
            int numDataEntries = (int) m_header.DataCount;
            for (int i = 0; i < numDataEntries; i++)
            {
                // Read type and the index used for retrieving the key
                RelicAttribute.AttributeValueType type = GetType(m_reader.ReadUInt32());
                int keyIndex = (int) m_reader.ReadUInt32();
                string key = m_sStrings[keyIndex];

                // depending on the type you need to read another type of data
                object value = null;
                switch (type)
                {
                    case RelicAttribute.AttributeValueType.Boolean:
                        value = (m_reader.ReadUInt32() == 1) ? true : false;
                        break;
                    case RelicAttribute.AttributeValueType.Float:
                        value = m_reader.ReadSingle();
                        break;
                    case RelicAttribute.AttributeValueType.Integer:
                        value = m_reader.ReadInt32();
                        break;
                    case RelicAttribute.AttributeValueType.String:
                        int stringIndex = (int) m_reader.ReadUInt32();
                        value = m_sStrings[stringIndex];
                        break;
                    case RelicAttribute.AttributeValueType.Table:
                        value = m_tables[(int) m_reader.ReadUInt32()];
                        break;
                }
                values[i] = new AttributeValue(type, key, value);
            }
            return values;
        }

        private void AssignDataToTables()
        {
            // this loop assigns the RBFDataValues to the proper tables
            m_reader.BaseStream.Position = m_lBaseOffset + m_header.TableSectionOffset;
            for (int i = 0; i < m_tables.Length; i++)
            {
                AttributeTable table = m_tables[i];
                int childCount = (int) m_reader.ReadUInt32();
                int firstDataIndex = (int) m_reader.ReadUInt32();
                for (int childIdx = 0; childIdx < childCount; childIdx++)
                {
                    int dataIdx = firstDataIndex + childIdx;
                    AttributeValue data = m_attributeValues[dataIdx];
                    table.AddValue(data);
                }
            }
        }
    }
}