#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#endregion

namespace cope.IO
{
    /// <summary>
    /// This class represents an XmlConfig which is able to store arbitrary kinds of values given that they
    /// come with a class inheriting from BaseConfigValue which does all the reading/writing.
    /// As a special feature, all it's values are returned as dynamics to reduce boiler-plate code.
    /// </summary>
    public class XmlConfig
    {
        private const string IDENTIFIER = "CopeXmlConfigFile";
        private readonly Dictionary<string, object> m_configValues;

        /// <summary>
        /// Constructs a new empty XmlConfig.
        /// </summary>
        public XmlConfig()
        {
            m_configValues = new Dictionary<string, object>();
        }

        #region methods

        /// <summary>
        /// Checks whether or not this instance of XmlConfig contains a value with a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsValue(string name)
        {
            return m_configValues.ContainsKey(name);
        }

        /// <summary>
        /// Removes a value from this instance of XmlConfig and returns whether or not a value with that name existed in the first place.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveValue(string name)
        {
            return m_configValues.Remove(name);
        }

        /// <summary>
        /// Adds a value to this instance of XmlConfig using the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configValue"></param>
        public void AddValue(string name, object configValue)
        {
            m_configValues.Add(name, configValue);
        }

        /// <summary>
        /// Adds a value to this instance of XmlConfig.
        /// </summary>
        /// <param name="configValue"></param>
        public void AddValue(BaseConfigValue configValue)
        {
            m_configValues.Add(configValue.Name, configValue);
        }

        /// <summary>
        /// Removes all values from this instance of XmlConfig.
        /// </summary>
        public void Clear()
        {
            m_configValues.Clear();
        }

        /// <summary>
        /// Retrieves a value with a specific name from this instance of XmlConfig.
        /// May throw exceptions if there is no such value.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public dynamic GetValue(string name)
        {
            return m_configValues[name];
        }

        /// <summary>
        /// Tries to get a value with the specified name from this instance of XmlConfig. Returns whether or not the operation succeeded.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configValue"></param>
        /// <returns></returns>
        public bool TryGetValue(string name, out dynamic configValue)
        {
            return m_configValues.TryGetValue(name, out configValue);
        }

        /// <summary>
        /// Reads the contents of this XmlConfig from a stream, which should of course contain Xml-data.
        /// </summary>
        /// <param name="str"></param>
        /// <exception cref="CopeException">Error while reading config file as Xml. See inner exception for details.</exception>
        internal void Read(Stream str)
        {
            if (str.Length == 0)
                return;

            var settings = new XmlReaderSettings {IgnoreWhitespace = true};
            XmlReader xmlReader = XmlReader.Create(str, settings);
            try
            {
                if (!xmlReader.Read() || !xmlReader.Read()) // Read Xml-Declaration node and the main node
                    throw new CopeException("Tried to read Xml config but there are no nodes at all!");

                // check that this is an appropriate file
                if (xmlReader.Name != IDENTIFIER)
                    throw new CopeException(
                        "Tried to read Xml config but the name of the uppermost node is different to " + IDENTIFIER +
                        ".");

                // read all the nodes
                while (xmlReader.Read())
                {
                    if (!xmlReader.IsStartElement())
                        continue;

                    // use the ConfigValueFactory class to read the config values.
                    string name = xmlReader.Name;
                    object configValue = ConfigValueFactory.GetFromXmlReader(xmlReader);
                    m_configValues.Add(name, configValue);
                }
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Error while reading config file as Xml. See inner exception for details.");
            }
        }

        /// <summary>
        /// Writes the contents of this instance of XmlConfig to the specified stream as XML.
        /// </summary>
        /// <param name="stream"></param>
        /// <exception cref="CopeException">Error while writing config file as Xml. See inner exception for details.</exception>
        internal void Write(Stream stream)
        {
            var settings = new XmlWriterSettings {Indent = true};
            XmlWriter xmlWriter = XmlWriter.Create(stream, settings);
            try
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(IDENTIFIER);
                foreach (var keyValuePair in m_configValues)
                {
                    if (keyValuePair.Value is BaseConfigValue)
                        (keyValuePair.Value as BaseConfigValue).WriteXml(xmlWriter);
                    else
                    {
                        bool success = CommonConfigValues.TryWriteValue(xmlWriter, keyValuePair.Key, keyValuePair.Value);
                        if (!success)
                            throw new CopeException("Unable to write config element: '" + keyValuePair.Key +
                                                    "' with value '" + keyValuePair.Value +
                                                    "' is not associated with any BaseConfigValue and is not a primitive value.");
                    }
                }
                xmlWriter.WriteFullEndElement();
                xmlWriter.WriteEndDocument();
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Error while writing config file as Xml. See inner exception for details.");
            }
            xmlWriter.Flush();
        }

        #endregion

        #region properties

        public int Count
        {
            get { return m_configValues.Count; }
        }

        public dynamic this[string id]
        {
            get { return m_configValues[id]; }
            set { m_configValues[id] = value; }
        }

        #endregion
    }
}