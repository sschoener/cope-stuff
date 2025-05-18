#region

using System.Xml;

#endregion

namespace cope.IO
{
    /// <summary>
    /// Abstract base class for all kinds of additional ConfigValues 
    /// </summary>
    public abstract class BaseConfigValue
    {
        public string Name { get; protected set; }
        public abstract string TypeName { get; }

        /// <summary>
        /// Reads a value from the specified XmlReader.
        /// </summary>
        /// <param name="xmlReader"></param>
        public void ReadXml(XmlReader xmlReader)
        {
            Name = xmlReader.Name;
            xmlReader.ReadStartElement();
            ReadInnerXml(xmlReader);
            xmlReader.ReadEndElement();
        }

        /// <summary>
        /// Writes a value to the specified XmlReader.
        /// </summary>
        /// <param name="xmlWriter"></param>
        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(Name);
            xmlWriter.WriteAttributeString("type", TypeName);
            WriteInnerXml(xmlWriter);
            xmlWriter.WriteFullEndElement();
        }

        /// <summary>
        /// Implements the reading of the actual values. The name is already read in ReadXml.
        /// All this happens inside the node containing the name of the value.
        /// </summary>
        /// <param name="xmlReader"></param>
        protected abstract void ReadInnerXml(XmlReader xmlReader);

        /// <summary>
        /// Implements the writing of the actual values. The name and type is already written out in in WriteXml.
        /// All this happens inside the node containing the name and the type of the value.
        /// </summary>
        /// <param name="xmlWriter"></param>
        protected abstract void WriteInnerXml(XmlWriter xmlWriter);
    }
}