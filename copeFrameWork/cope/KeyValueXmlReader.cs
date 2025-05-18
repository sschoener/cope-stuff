using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace cope
{
    /// <summary>
    /// Provides functions to convert XML to <c>KeyedValues</c>.
    /// </summary>
    public static class KeyValueXmlReader
    {
        /// <summary>
        /// Tries to open the file at the given path and convert its XML-contents to <c>KeyValue</c> objects.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="CopeException">Failed to open file to read XML as KeyedValues.</exception>
        public static List<KeyedValue> Read(string path)
        {
            Stream file = null;
            List<KeyedValue> data;
            try
            {
                file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                data = Read(file);
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Failed to open file to read XML as KeyedValues.");
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
            return data;
        }

        /// <summary>
        /// Tries to read XML from the given stream and convert it to <c>KeyValue</c> objects.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<KeyedValue> Read(Stream str)
        {
            var settings = new XmlReaderSettings { IgnoreWhitespace = true };
            XmlReader reader = XmlReader.Create(str, settings);
            var data = Read(reader);
            return data;
        }

        /// <summary>
        /// Tries to read a <c>KeyedValue</c> objects from a given <c>XmlReader</c>.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        /// <exception cref="CopeException">Failed to read KeyedValue from XML. Expected node with name KeyedValues.</exception>
        public static List<KeyedValue> ReadData(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.Name == KeyValueXmlWriter.IDENTIFIER)
            {
                reader.ReadStartElement();
                List<KeyedValue> attribValues = new List<KeyedValue>();
                while (reader.IsStartElement())
                {
                    attribValues.Add(ReadValue(reader));
                }
                reader.ReadEndElement();
                return attribValues;
            }
            throw new CopeException("Failed to read KeyedValue from XML. Expected node with name " +
                                    KeyValueXmlWriter.IDENTIFIER);
        }

        private static List<KeyedValue> Read(XmlReader reader)
        {
            reader.Read(); // document begin
            var keyedValues = ReadData(reader);
            return keyedValues;
        }
        
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        private static KeyedValue ReadValue(XmlReader reader)
        {
            if (!reader.IsStartElement())
                throw new CopeException("Expected an KeyedValue to start but got node with name " + reader.Name +
                                        " instead.");
            reader.Read();
            string key = null;
            KeyValueType dataType = KeyValueType.Invalid;
            object data = null;
            string metaData = string.Empty;
            while (reader.MoveToContent() == XmlNodeType.Element && reader.IsStartElement())
            {
                if (reader.Name == "Key")
                    key = reader.ReadElementContentAsString();
                else if (reader.Name == "Type")
                {
                    string type = reader.ReadElementContentAsString();
                    dataType = KeyedValue.ConvertStringToType(type);
                    if (type == string.Empty || dataType == KeyValueType.Invalid)
                        throw new CopeException("Empty or invalid type attribute for a node called " + key + ": " +
                                                type);
                }
                else if (reader.Name == "Data")
                {
                    if (dataType == KeyValueType.Invalid)
                        throw new CopeException(
                            "Failed to read Data node, please ensure that this node's 'Type' node is before the 'Data' node.");
                    switch (dataType)
                    {
                        case KeyValueType.Boolean:
                            data = bool.Parse(reader.ReadElementContentAsString());
                            break;
                        case KeyValueType.Float:
                            data = float.Parse(reader.ReadElementContentAsString(), CultureInfo.InvariantCulture);
                            break;
                        case KeyValueType.Integer:
                            data = reader.ReadElementContentAsInt();
                            break;
                        case KeyValueType.String:
                            data = reader.ReadElementContentAsString();
                            break;
                        case KeyValueType.Table:
                            var table = new KeyValueTable();
                            reader.Read();
                            while (reader.IsStartElement())
                            {
                                var attribValue = ReadValue(reader);
                                table.AddValue(attribValue);
                            }
                            data = table;
                            reader.ReadEndElement();
                            break;
                    }
                }
                else if (reader.Name == "MetaData")
                    metaData = reader.ReadElementContentAsString();
            }

            reader.ReadEndElement();
            return new KeyedValue(dataType, key, data) { MetaData = metaData };
        }
    }
}