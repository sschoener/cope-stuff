using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace cope
{
    /// <summary>
    /// Provides functions to convert <c>KeyedValues</c> to XML.
    /// </summary>
    public static class KeyValueXmlWriter
    {
        public const string IDENTIFIER = "KeyedValues";

        /// <summary>
        /// Writes the specified <c>KeyedValue</c> objects using the given <c>XmlWriter</c>.
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <param name="keyedData"></param>
        /// <exception cref="ArgumentNullException"><paramref name="keyedData" /> is <c>null</c>.</exception>
        public static void Write(XmlWriter xmlWriter, IEnumerable<KeyedValue> keyedData)
        {
            if (xmlWriter == null) throw new ArgumentNullException("xmlWriter");
            if (keyedData == null) throw new ArgumentNullException("keyedData");
            WriteData(xmlWriter, keyedData);
        }

        /// <summary>
        /// Tries to create a file at the given path and write the specified <c>KeyedValue</c> objects to the newly created
        /// file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="keyedValues"></param>
        /// <exception cref="CopeException">Failed to create file to write KeyedValues to.</exception>
        public static void Write(string path, IEnumerable<KeyedValue> keyedValues)
        {
            Stream file = null;
            try
            {
                file = File.Create(path);
            }
            catch (Exception ex)
            {
                if (file != null)
                    file.Close();
                var excep = new CopeException(ex, "Failed to create file to write KeyedValues to.");
                excep.Data["Path"] = path;
                throw excep;
            }
            try
            {
                WriteIntern(file, keyedValues);
            }
            catch (Exception ex)
            {
                var excep = new CopeException(ex, "Failed to write KeyedValues.");
                excep.Data["Path"] = path;
                throw excep;
            }
            file.Flush();
            file.Close();
        }

        /// <summary>
        /// Tries to write the specified <c>KeyValue</c> objects to the given stream.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keyedValues"></param>
        /// <exception cref="CopeException">Failed to write KeyedValues.</exception>
        public static void Write(Stream str, IEnumerable<KeyedValue> keyedValues)
        {
            try
            {
                WriteIntern(str, keyedValues);
            }
            catch (Exception ex)
            {
                var excep =  new CopeException(ex, "Failed to write KeyedValues.");
                excep.Data["Stream"] = str;
                throw excep;
            }
            str.Flush();
        }

        private static void WriteIntern(Stream stream, IEnumerable<KeyedValue> keyedValues)
        {
            var settings = new XmlWriterSettings { Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create(stream, settings);
            xmlWriter.WriteStartDocument();
            WriteData(xmlWriter, keyedValues);
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
        }

        /// <exception cref="CopeException">Failed to write KeyedValues as XML.</exception>
        private static void WriteData(XmlWriter xmlWriter, IEnumerable<KeyedValue> keyedValues)
        {
            try
            {
                xmlWriter.WriteStartElement(IDENTIFIER);
                foreach (var attribValue in keyedValues)
                    WriteData(xmlWriter, attribValue);
                xmlWriter.WriteFullEndElement();
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Failed to write KeyedValues as XML.");
            }
        }

        private static void WriteData(XmlWriter xmlWriter, KeyedValue keyedValue)
        {
            xmlWriter.WriteStartElement("Value");

            if (!string.IsNullOrWhiteSpace(keyedValue.AutoComment))
                xmlWriter.WriteComment(keyedValue.AutoComment);

            xmlWriter.WriteStartElement("Key");
            xmlWriter.WriteValue(keyedValue.Key);
            xmlWriter.WriteFullEndElement();
            xmlWriter.WriteStartElement("Type");
            xmlWriter.WriteValue(keyedValue.Type.ToString());
            xmlWriter.WriteFullEndElement();
            xmlWriter.WriteStartElement("Data");

            if (keyedValue.Type == KeyValueType.Table)
            {
                foreach (var av in keyedValue.Value as KeyValueTable)
                    WriteData(xmlWriter, av);
            }
            else if (keyedValue.Type == KeyValueType.Float)
                xmlWriter.WriteValue(((float)keyedValue.Value).ToString(CultureInfo.InvariantCulture));
            else
                xmlWriter.WriteValue(keyedValue.Value.ToString());

            xmlWriter.WriteFullEndElement();

            if (!string.IsNullOrWhiteSpace(keyedValue.MetaData))
            {
                xmlWriter.WriteStartElement("MetaData");
                xmlWriter.WriteValue(keyedValue.MetaData);
                xmlWriter.WriteFullEndElement();
            }

            xmlWriter.WriteFullEndElement();
        }
    }
}