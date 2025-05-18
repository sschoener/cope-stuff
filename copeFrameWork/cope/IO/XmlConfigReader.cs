using System.IO;

namespace cope.IO
{
    /// <summary>
    /// Helper class to read XmlConfig
    /// </summary>
    public static class XmlConfigReader
    {
        /// <summary>
        /// Reads an XmlConfig from the specified stream.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static XmlConfig Read(Stream str)
        {
            XmlConfig config = new XmlConfig();
            config.Read(str);
            return config;
        }
    }
}
