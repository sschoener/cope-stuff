#region

using System;
using System.Globalization;
using System.Xml;

#endregion

namespace cope.Extensions
{
    /// <summary>
    /// Static helper class to read primitive values from XmlNodes.
    /// </summary>
    public static class XmlNodeExt
    {
        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public static double ReadDouble(this XmlNode node, string subNodeName)
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                throw new Exception("Found " + node.Name + " node without a " + subNodeName + " node!");
            double value;
            if (!double.TryParse(subNode.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                throw new Exception("Failed to parse " + subNodeName + " of " + node.Name + ':' + subNode.InnerText);
            return value;
        }

        public static double ReadDouble(this XmlNode node, string subNodeName, double defaultValue)
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                return defaultValue;
            double value;
            if (!double.TryParse(subNode.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                return defaultValue;
            return value;
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public static int ReadInt(this XmlNode node, string subNodeName)
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                throw new Exception("Found " + node.Name + " node without a " + subNodeName + " node!");
            int value;
            if (!int.TryParse(subNode.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                throw new Exception("Failed to parse " + subNodeName + " of " + node.Name + ':' + subNode.InnerText);
            return value;
        }

        public static int ReadInt(this XmlNode node, string subNodeName, int defaultValue)
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                return defaultValue;
            int value;
            if (!int.TryParse(subNode.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                return defaultValue;
            return value;
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public static string ReadString(this XmlNode node, string subNodeName)
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                throw new Exception("Found " + node.Name + " node without a " + subNodeName + " node!");
            return subNode.InnerText;
        }

        public static string ReadString(this XmlNode node, string subNodeName, string defaultValue)
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                return defaultValue;
            return subNode.InnerText;
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public static bool ReadBool(this XmlNode node, string subNodeName)
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                throw new Exception("Found " + node.Name + " node without a " + subNodeName + " node!");
            bool value;
            if (!bool.TryParse(subNode.InnerText, out value))
                throw new Exception("Failed to parse " + subNodeName + " of " + node.Name + ':' + subNode.InnerText);
            return value;
        }

        public static bool ReadBool(this XmlNode node, string subNodeName, bool defaultValue)
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                return defaultValue;
            bool value;
            if (!bool.TryParse(subNode.InnerText, out value))
                return defaultValue;
            return value;
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public static TEnum ReadEnum<TEnum>(this XmlNode node, string subNodeName) where TEnum : struct
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                throw new Exception("Found " + node.Name + " node without a " + subNodeName + " node!");
            TEnum value;
            if (!Enum.TryParse(subNode.InnerText, out value))
                throw new Exception("Failed to parse " + subNodeName + " of " + node.Name + ':' + subNode.InnerText);
            return value;
        }

        public static TEnum ReadEnum<TEnum>(this XmlNode node, string subNodeName, TEnum defaultValue)
            where TEnum : struct
        {
            var subNode = node.SelectSingleNode(subNodeName);
            if (subNode == null)
                return defaultValue;
            TEnum value;
            if (!Enum.TryParse(subNode.InnerText, out value))
                return defaultValue;
            return value;
        }
    }
}