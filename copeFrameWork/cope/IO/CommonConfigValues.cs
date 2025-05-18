#region

using System;
using System.Linq;
using System.Xml;

#endregion

namespace cope.IO
{
    /// <summary>
    /// Contains serializing fascilities for common data types: bool, DateTime, decimal, double, float, int, long, string.
    /// This is a helper classed used by XmlConfig.
    /// </summary>
    public static class CommonConfigValues
    {
        private static readonly string[] s_typeNames = {
                                                           "bool", "DateTime", "decimal", "double", "float", "int", "long",
                                                           "string"
                                                       };

        /// <summary>
        /// Tries to write the specified 'value' to the XmlWriter. This will only work for primitive types such as string, bool etc.
        /// Returns 'true' on success, otherwise 'false'.
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryWriteValue(XmlWriter xmlWriter, string name, object value)
        {
            string typeName;
            if (value is bool)
                typeName = "bool";
            else if (value is DateTime)
                typeName = "DateTime";
            else if (value is decimal)
                typeName = "decimal";
            else if (value is double)
                typeName = "double";
            else if (value is float)
                typeName = "float";
            else if (value is int)
                typeName = "int";
            else if (value is long)
                typeName = "long";
            else if (value is string)
                typeName = "string";
            else
                return false;
            xmlWriter.WriteStartElement(name);
            xmlWriter.WriteAttributeString("type", typeName);
            xmlWriter.WriteValue(value);
            xmlWriter.WriteFullEndElement();
            return true;
        }

        /// <summary>
        /// Tries to read a value from the specified XmlReader given a type name. It will only read primitive types such
        /// as int, string, etc. and return it. Returns false on failure.
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <param name="typeName">One of these: 'bool', 'decimal', 'double', 'float', 'int', 'long', 'string'</param>
        /// <param name="value">This out parameter will contain the value if the function returns true.</param>
        /// <returns></returns>
        public static bool TryReadValue(XmlReader xmlReader, string typeName, out object value)
        {
            value = null;
            if (!s_typeNames.Any(type => type == typeName))
                return false;
            xmlReader.ReadStartElement();
            switch (typeName)
            {
                case "bool":
                    value = xmlReader.ReadContentAsBoolean();
                    break;
                case "DateTime":
                    value = xmlReader.ReadContentAsDateTime();
                    break;
                case "decimal":
                    value = xmlReader.ReadContentAsDecimal();
                    break;
                case "double":
                    value = xmlReader.ReadContentAsDouble();
                    break;
                case "float":
                    value = xmlReader.ReadContentAsFloat();
                    break;
                case "int":
                    value = xmlReader.ReadContentAsInt();
                    break;
                case "long":
                    value = xmlReader.ReadContentAsLong();
                    break;
                case "string":
                    value = xmlReader.ReadContentAsString();
                    break;
            }
            xmlReader.ReadEndElement();
            return true;
        }
    }
}