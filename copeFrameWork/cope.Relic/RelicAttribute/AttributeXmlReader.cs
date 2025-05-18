#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

#endregion

namespace cope.Relic.RelicAttribute
{
    public static class AttributeXmlReader
    {
        #region Delegates

        public delegate void AdditionalInfoReader(XmlReader reader);

        #endregion

        /// <exception cref="RelicException">Failed to open file to read RelicAttribute as XML.</exception>
        public static AttributeStructure Read(string path, AdditionalInfoReader infoReader = null)
        {
            Stream file = null;
            AttributeStructure data;
            try
            {
                file = File.Open(path, FileMode.Open, FileAccess.Read);
                data = Read(file, infoReader);
            }
            catch (Exception ex)
            {
                if (file != null)
                    file.Close();
                throw new RelicException(ex, "Failed to open file to read RelicAttribute as XML.");
            }
            return data;
        }

        public static AttributeStructure Read(Stream str)
        {
            var settings = new XmlReaderSettings { IgnoreWhitespace = true };
            XmlReader reader = XmlReader.Create(str, settings);
            var data = Read(reader, null);
            return data;
        }

        public static AttributeStructure Read(Stream str, AdditionalInfoReader infoReader = null)
        {
            var settings = new XmlReaderSettings {IgnoreWhitespace = true};
            XmlReader reader = XmlReader.Create(str, settings);
            var data = Read(reader, infoReader);
            return data;
        }

        private static AttributeStructure Read(XmlReader reader, AdditionalInfoReader infoReader)
        {
            reader.Read(); // document begin
            var attribValues = ReadData(reader, infoReader);
            return new AttributeStructure(attribValues.First());
        }

        /// <exception cref="RelicException">Failed to read RelicAttribute from XML. Expected node with name RelicAttribute</exception>
        public static IEnumerable<AttributeValue> ReadData(XmlReader reader, AdditionalInfoReader infoReader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.Name == AttributeXmlWriter.IDENTIFIER)
            {
                reader.ReadStartElement();
                List<AttributeValue> attribValues = new List<AttributeValue>();
                while (reader.IsStartElement())
                {
                    attribValues.Add(ReadValue(reader, infoReader));
                }
                reader.ReadEndElement();
                return attribValues;
            }
            throw new RelicException("Failed to read RelicAttribute from XML. Expected node with name " +
                                     AttributeXmlWriter.IDENTIFIER);
        }

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        private static AttributeValue ReadValue(XmlReader reader, AdditionalInfoReader infoReader)
        {
            if (!reader.IsStartElement())
                throw new RelicException("Expected an AttributeValue to start but got node with name " + reader.Name +
                                         " instead.");
            reader.Read();
            string key = null;
            AttributeValueType dataType = AttributeValueType.Invalid;
            object data = null;
            while (reader.MoveToContent() == XmlNodeType.Element && reader.IsStartElement())
            {
                if (reader.Name == "Key")
                    key = reader.ReadElementContentAsString();
                else if (reader.Name == "Type")
                {
                    string type = reader.ReadElementContentAsString();
                    dataType = AttributeValue.ConvertStringToType(type);
                    if (type == string.Empty || dataType == AttributeValueType.Invalid)
                        throw new RelicException("Empty or invalid type attribute for a node called " + key + ": " +
                                                 type);
                }
                else if (reader.Name == "Data")
                {
                    if (dataType == AttributeValueType.Invalid)
                        throw new RelicException(
                            "Failed to read Data node, please ensure that this node's 'Type' node is before the 'Data' node.");
                    switch (dataType)
                    {
                        case AttributeValueType.Boolean:
                            data = bool.Parse(reader.ReadElementContentAsString());
                            break;
                        case AttributeValueType.Float:
                            data = float.Parse(reader.ReadElementContentAsString(), CultureInfo.InvariantCulture);
                            break;
                        case AttributeValueType.Integer:
                            data = reader.ReadElementContentAsInt();
                            break;
                        case AttributeValueType.String:
                            data = reader.ReadElementContentAsString();
                            break;
                        case AttributeValueType.Table:
                            var table = new AttributeTable();
                            reader.Read();
                            while (reader.IsStartElement())
                            {
                                var attribValue = ReadValue(reader, infoReader);
                                table.AddValue(attribValue);
                            }
                            data = table;
                            reader.ReadEndElement();
                            break;
                        case AttributeValueType.List:
                            var list = new AttributeList();
                            reader.Read();
                            while (reader.IsStartElement())
                            {
                                var attribValue = ReadValue(reader, infoReader);
                                list.AddValue(attribValue);
                            }
                            data = list;
                            reader.ReadEndElement();
                            break;
                    }
                }
                else if (reader.Name == "AdditionalInfo")
                {
                    try
                    {
                        if (infoReader != null)
                            infoReader(reader);
                    }
                    catch (Exception ex)
                    {
                        throw new RelicException(ex,
                                                 "Error while reading additional info via the specified info-reader.");
                    }
                }
            }

            reader.ReadEndElement();
            return new AttributeValue(dataType, key, data);
        }
    }
}