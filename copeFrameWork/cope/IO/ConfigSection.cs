#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace cope.IO
{
    /// <summary>
    /// This class represents a section-type ConfigValue for the XmlConfig.
    /// </summary>
    public sealed class ConfigSection : BaseConfigValue
    {
        private const string TYPE_NAME = "section";
        private readonly Dictionary<string, object> m_configValues;

        #region statics

        /// <summary>
        /// Static constructor to be called by the static initializer in ConfigValueFactory.
        /// </summary>
        static ConfigSection()
        {
            ConfigValueFactory.RegisterType(TYPE_NAME, ProductionMethod);
        }

        /// <summary>
        /// Static production function to be added as a callback to the static table of production functions in ConfigValueFactory.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static ConfigSection ProductionMethod(XmlReader reader)
        {
            return new ConfigSection(reader);
        }

        #endregion

        /// <summary>
        /// Creates a new ConfigsSection with a given name.
        /// </summary>
        /// <param name="name"></param>
        public ConfigSection(string name)
        {
            Name = name;
            m_configValues = new Dictionary<string, object>();
        }

        /// <summary>
        /// Reads a ConfigSection from the given XmlReader.
        /// </summary>
        /// <param name="reader"></param>
        public ConfigSection(XmlReader reader)
        {
            m_configValues = new Dictionary<string, object>();
            ReadXml(reader);
        }

        #region methods

        /// <summary>
        /// Checks whether or not this section contains a value with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsValue(string name)
        {
            return m_configValues.ContainsKey(name);
        }

        /// <summary>
        /// Removse the value with the specified name from this instance of ConfigSection and returns whether or not such a value existed.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveValue(string name)
        {
            return m_configValues.Remove(name);
        }

        /// <summary>
        /// Adds the specified value to the ConfigSection.
        /// </summary>
        /// <param name="configValue"></param>
        public void AddValue(BaseConfigValue configValue)
        {
            m_configValues.Add(configValue.Name, configValue);
        }

        /// <summary>
        /// Adds a value to this instance of ConfigSection using the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configValue"></param>
        public void AddValue(string name, object configValue)
        {
            m_configValues.Add(name, configValue);
        }

        /// <summary>
        /// Removes all entries from this instance of ConfigSection.
        /// </summary>
        public void Clear()
        {
            m_configValues.Clear();
        }

        /// <summary>
        /// Retrieves a value with a specified name from this instance of ConfigSection.
        /// May throw exceptions if there is no entry with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public dynamic GetValue(string name)
        {
            return m_configValues[name];
        }

        /// <summary>
        /// Tries to get the config value with the specified name. Returns true if the operation succeeded.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configValue"></param>
        /// <returns></returns>
        public bool TryGetValue(string name, out dynamic configValue)
        {
            return m_configValues.TryGetValue(name, out configValue);
        }

        protected override void ReadInnerXml(XmlReader xmlReader)
        {
            while (true)
            {
                if (xmlReader.Name == Name && !xmlReader.IsStartElement())
                    break;
                string name = xmlReader.Name;
                object configValue = ConfigValueFactory.GetFromXmlReader(xmlReader);
                m_configValues.Add(name, configValue);
            }
        }

        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        protected override void WriteInnerXml(XmlWriter xmlWriter)
        {
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
        }

        #endregion

        #region properties

        public dynamic this[string id]
        {
            get { return m_configValues[id]; }
            set { m_configValues[id] = value; }
        }

        public int Count
        {
            get { return m_configValues.Count; }
        }

        public override string TypeName
        {
            get { return TYPE_NAME; }
        }

        #endregion
    }
}