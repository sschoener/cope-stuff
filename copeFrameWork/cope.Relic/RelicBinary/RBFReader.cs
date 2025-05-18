#region

using System;
using System.IO;
using cope.Extensions;
using cope.Relic.RelicAttribute;

#endregion

namespace cope.Relic.RelicBinary
{
    /// <summary>
    /// Helper class to read RBF-data.
    /// </summary>
    public class RBFReader
    {
        #region fields

        private const int KEY_BYTE_LENGTH_PADDED = 64;

        private readonly bool m_bReadRetributionFormat;
        private readonly IRBFKeyProvider m_keyProvider;
        private AttributeValue[] m_attributeValues;
        private RBFHeader m_header;

        private long m_lBaseOffset;
        private BinaryReader m_reader;
        private string[] m_sKeys;
        private byte[] m_sStringSection;
        private AttributeTable[] m_tables;
        private uint[] m_uDataIndices;

        #endregion

        private RBFReader(IRBFKeyProvider keyProvider)
        {
            m_keyProvider = keyProvider;
            m_bReadRetributionFormat = keyProvider != null;
        }

        /// <summary>
        /// Reads an AttributeStructure formatted as an RBF from a given Stream. You may also provide a KeyProvider.
        /// Post-Retribution RBF files require a KeyProvider.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keyProvider"></param>
        /// <returns></returns>
        public static AttributeStructure Read(Stream str, IRBFKeyProvider keyProvider = null)
        {
            RBFReader reader = new RBFReader(keyProvider);
            return reader.Read(str);
        }

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        private AttributeStructure Read(Stream str)
        {
            try
            {
                m_reader = new BinaryReader(str);
                m_lBaseOffset = str.Position;

                m_header = new RBFHeader(m_reader, m_bReadRetributionFormat);

                // the keys should not be read for RBFs in RB2 mode as they're provided by the
                // RBFKeyProvider (aka FLB file)
                m_sKeys = null;
                if (!m_bReadRetributionFormat)
                    m_sKeys = ReadKeys();

                // directly reading the strings is possible but the RBFData references them by offset
                // so it's more sensible to just load it into memory without interpreting the data yet
                m_reader.BaseStream.Position = m_lBaseOffset + m_header.StringSectionOffset;
                m_sStringSection = m_reader.ReadBytes((int) m_header.StringSectionLength);

                // the data items already reference the tables so it is a good idea
                // to setup the tables before reading the data
                m_tables = new AttributeTable[m_header.TableArrayCount];
                for (int i = 0; i < m_tables.Length; i++)
                    m_tables[i] = new AttributeTable();

                // read the data
                m_attributeValues = ReadDataValues();

                // declare the root element
                // per definitionem the root always holds the first table-entry
                AttributeValue root = new AttributeValue(RelicAttribute.AttributeValueType.Table, "GameData", m_tables[0]);

                // read the data index array and use it to fill the tables with the proper values
                m_reader.BaseStream.Position = m_lBaseOffset + m_header.DataIndexArrayOffset;
                m_uDataIndices = m_reader.ReadUInt32Array((int) m_header.DataIndexArrayCount);
                AssignDataToTables();
                return new AttributeStructure(root);
            }
            catch (Exception ex)
            {
                var excp = new RelicException(ex,
                                              "Error while trying to read RBF-file.");
                excp.Data["RetributionFormat"] = m_bReadRetributionFormat;
                excp.Data["Stream"] = str;
                throw excp;
            }
        }

        private string[] ReadKeys()
        {
            m_reader.BaseStream.Position = m_lBaseOffset + m_header.KeyArrayOffset;
            string[] keys = new string[m_header.KeyArrayCount];
            int keyArrayLength = (int) (m_header.KeyArrayCount * KEY_BYTE_LENGTH_PADDED);
            byte[] keyBytes = m_reader.ReadBytes(keyArrayLength);
            int currentOffset = 0;
            for (int i = 0; i < m_header.KeyArrayCount; i++)
            {
                keys[i] = keyBytes.ToString(true, currentOffset, KEY_BYTE_LENGTH_PADDED).SubstringBeforeFirst('\0');
                currentOffset += KEY_BYTE_LENGTH_PADDED;
            }
            return keys;
        }

        private AttributeValueType GetType(uint type)
        {
            switch(type)
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
            m_reader.BaseStream.Position = m_lBaseOffset + m_header.DataArrayOffset;
            AttributeValue[] values = new AttributeValue[m_header.DataArrayCount];
            int numDataEntries = (int) m_header.DataArrayCount;
            for (int i = 0; i < numDataEntries; i++)
            {
                // Read type and the index used for retrieving the key
                AttributeValueType type;
                int keyIndex;
                if (m_bReadRetributionFormat)
                {
                    type = GetType(m_reader.ReadUInt16());
                    keyIndex = m_reader.ReadUInt16();
                }
                else
                {
                    type = GetType(m_reader.ReadUInt32());
                    keyIndex = (int) m_reader.ReadUInt32();
                }

                // get the key: either by using the specified key provider or by using the keys-array
                string key;
                if (m_bReadRetributionFormat)
                    key = m_keyProvider.GetNameByIndex(keyIndex);
                else
                    key = m_sKeys[keyIndex];

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
                        int offset = (int) m_reader.ReadUInt32(); // offset of the string-length
                        int strLength = (int) m_sStringSection.ToUInt32(offset);
                        // the string is prefixed with its length
                        value = m_sStringSection.ToString(true, offset + 4, strLength);
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
            m_reader.BaseStream.Position = m_lBaseOffset + m_header.TableArrayOffset;
            for (int i = 0; i < m_tables.Length; i++)
            {
                AttributeTable table = m_tables[i];
                int childCount = (int) m_reader.ReadUInt32();
                int firstDataIndex = (int) m_reader.ReadUInt32();
                // that's a speciality: if there's only one child then the index doesn't refer to the data DataIndex-array...
                if (childCount == 1)
                {
                    // ... but instead to the data-array
                    AttributeValue data = m_attributeValues[firstDataIndex];
                    if (data.DataType == RelicAttribute.AttributeValueType.Table)
                        table.AddValue(data);
                    else
                        // some people think it's cool to cache values within RBFs, that's why we need to deal with such specialities...
                        table.AddValue(data.GClone());
                }
                else
                {
                    for (int childNum = 0; childNum < childCount; childNum++)
                    {
                        int idx = (int) m_uDataIndices[firstDataIndex + childNum];
                        AttributeValue data = m_attributeValues[idx];
                        if (data.DataType == RelicAttribute.AttributeValueType.Table)
                            table.AddValue(data);
                        else
                            table.AddValue(data.GClone());
                    }
                }
            }
        }
    }
}