#region

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

#endregion

namespace cope.IO
{
    /// <summary>
    /// Manages the different types of available BaseConfigValues. These are used by XmlConfig to serialize values.
    /// </summary>
    public static class ConfigValueFactory
    {
        #region Delegates

        /// <summary>
        /// A Producer function constructs a BaseConfigValue from a given XmlReader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public delegate BaseConfigValue Producer(XmlReader reader);

        #endregion

        private static readonly Dictionary<string, Producer> s_registeredTypes = new Dictionary<string, Producer>();

        /// <summary>
        /// This static ctor takes care of registering all ConfigValues in all loaded assemblies.
        /// Additionally, it registers a callback to the AssemblyLoad-event of the current domain.
        /// </summary>
        static ConfigValueFactory()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly a in assemblies)
            {
                RegisterConfigValues(a);
            }
        }

        /// <summary>
        /// This method registers all ConfigValues from a specified assembly by calling their static constructor.
        /// A type's static constructor normally is only invoked when the type is actually being used, but that's too late for our purposes.
        /// </summary>
        /// <param name="assembly"></param>
        public static void RegisterConfigValues(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                if (t.IsSubclassOf(typeof (BaseConfigValue)))
                {
                    var ctor = t.TypeInitializer;
                    if (ctor != null)
                        ctor.Invoke(null, null);
                }
            }
        }

        /// <summary>
        /// Callback for the AssemblyLoad event of the current Domain. This will take care of registering
        /// all ConfigValues from newly loaded assemblies.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            if (args.LoadedAssembly != null)
                RegisterConfigValues(args.LoadedAssembly);
        }

        /// <summary>
        /// Associates a 'type name' with a production delegate.
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="producer"></param>
        public static void RegisterType(string typeName, Producer producer)
        {
            s_registeredTypes[typeName] = producer;
        }

        /// <summary>
        /// Unregisters the BaseConfigValue with the specified 'typeName'. Returns whether or not a type with the given name was registered.
        /// </summary>
        /// <param name="typeName"></param>
        public static bool UnregisterType(string typeName)
        {
            return s_registeredTypes.Remove(typeName);
        }

        /// <summary>
        /// Returns whether or not a type with the specified typeName is registered.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsTypeRegistered(string typeName)
        {
            return s_registeredTypes.ContainsKey(typeName);
        }

        /// <summary>
        /// Constructs a BaseConfigValue given a type name and a XmlReader. This will check the 'type'-attribue of the current
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="xmlReader"></param>
        /// <returns></returns>
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        public static object GetByType(string typeName, XmlReader xmlReader)
        {
            Producer prod;
            if (s_registeredTypes.TryGetValue(typeName, out prod))
                return prod(xmlReader);

            object value;
            if (CommonConfigValues.TryReadValue(xmlReader, typeName, out value))
                return value;

            if (xmlReader is XmlTextReader)
                throw new CopeException("Unknown or invalid type attribute: '" + typeName + "' in line " +
                                        ((XmlTextReader) xmlReader).LineNumber);
            throw new CopeException("Unknown or invalid type attribute: '" + typeName + "'.");
        }

        /// <summary>
        /// Reads a BaseConfigValue or a common config value such as an integer from the specified XmlReader.
        /// Will throw an exception if the 'type'-attribute of the XmlElement it is trying to read is unknown to it.
        /// Returns the BaseConfigValue / the common config value.
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <returns></returns>
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        public static object GetFromXmlReader(XmlReader xmlReader)
        {
            string typeName = xmlReader.GetAttribute("type");
            if (string.IsNullOrWhiteSpace(typeName))
            {
                if (xmlReader is XmlTextReader)
                    throw new CopeException("In line " + ((XmlTextReader) xmlReader).LineNumber +
                                            " is a node with name '" + xmlReader.Name +
                                            "' without any attributes which is thus missing type information.");
                throw new CopeException("At " + xmlReader.Name +
                                        " is a node without any attributes which is thus missing type information.");
            }
            return GetByType(typeName, xmlReader);
        }
    }
}