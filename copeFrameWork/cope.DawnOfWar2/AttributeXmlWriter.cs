﻿#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using cope.DawnOfWar2.RelicAttribute;

#endregion

namespace cope.DawnOfWar2
{
    public static class AttributeXmlWriter
    {
        #region Delegates

        public delegate void AdditionalInfoWriter(XmlWriter xmlWriter, AttributeValue attribValue);

        #endregion

        public const string IDENTIFIER = "RelicAttribute";

        public static void Write(XmlWriter xmlWriter, AttributeStructure attribStructure,
                                 AdditionalInfoWriter infoWriter = null)
        {
            Write(xmlWriter, new[] {attribStructure.Root}, infoWriter);
        }

        public static void Write(XmlWriter xmlWriter, IEnumerable<AttributeValue> attribData,
                                 AdditionalInfoWriter infoWriter = null)
        {
            WriteData(xmlWriter, attribData, infoWriter);
        }

        /// <exception cref="CopeDoW2Exception">Failed to create file to write RelicAttribute to.</exception>
        public static void Write(string path, AttributeStructure attribStructure)
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
                throw new CopeDoW2Exception(ex, "Failed to create file to write RelicAttribute to.");
            }
            try
            {
                Write(file, new[] {attribStructure.Root});
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex, "Failed to write RelicAttribute to " + path);
            }
            file.Flush();
            file.Close();
        }

        /// <exception cref="CopeDoW2Exception"><c>CopeDoW2Exception</c>.</exception>
        public static void Write(Stream str, AttributeStructure attribStructure)
        {
            try
            {
                Write(str, new[] {attribStructure.Root});
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex, "Failed to write RelicAttribute to " + str);
            }
            str.Flush();
        }

        private static void Write(Stream stream, IEnumerable<AttributeValue> attribValues)
        {
            var settings = new XmlWriterSettings {Indent = true};
            XmlWriter xmlWriter = XmlWriter.Create(stream, settings);
            xmlWriter.WriteStartDocument();
            Write(xmlWriter, attribValues);
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
        }

        /// <exception cref="CopeDoW2Exception">Failed to write RelicAttributes to stream.</exception>
        private static void WriteData(XmlWriter xmlWriter, IEnumerable<AttributeValue> attribValues,
                                      AdditionalInfoWriter infoWriter)
        {
            try
            {
                xmlWriter.WriteStartElement(IDENTIFIER);
                foreach (var attribValue in attribValues)
                    WriteData(xmlWriter, attribValue, infoWriter);
                xmlWriter.WriteFullEndElement();
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex, "Failed to write RelicAttributes as XML.");
            }
        }

        /// <exception cref="CopeDoW2Exception">Error while writing additional info via the specified info-writer!</exception>
        private static void WriteData(XmlWriter xmlWriter, AttributeValue attribValue, AdditionalInfoWriter infoWriter)
        {
            xmlWriter.WriteStartElement("Value");

            xmlWriter.WriteStartElement("Key");
            xmlWriter.WriteValue(attribValue.Key);
            xmlWriter.WriteFullEndElement();
            xmlWriter.WriteStartElement("Type");
            xmlWriter.WriteValue(attribValue.DataType.ToString());
            xmlWriter.WriteFullEndElement();
            xmlWriter.WriteStartElement("Data");

            if (infoWriter != null)
            {
                try
                {
                    xmlWriter.WriteStartElement("AdditionalInfo");
                    infoWriter(xmlWriter, attribValue);
                    xmlWriter.WriteFullEndElement();
                }
                catch (Exception ex)
                {
                    throw new CopeDoW2Exception(ex, "Error while writing additional info via the specified info-writer!");
                }
            }

            if (attribValue.DataType == AttributeDataType.Table)
            {
                foreach (var av in attribValue.Data as AttributeTable)
                    WriteData(xmlWriter, av, infoWriter);
            }
            else if (attribValue.DataType == AttributeDataType.Float)
                xmlWriter.WriteValue(((float) attribValue.Data).ToString(CultureInfo.InvariantCulture));
            else
                xmlWriter.WriteValue(attribValue.Data.ToString());

            xmlWriter.WriteFullEndElement();
            xmlWriter.WriteFullEndElement();
        }
    }
}