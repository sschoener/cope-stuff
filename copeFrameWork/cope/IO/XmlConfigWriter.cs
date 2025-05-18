#region

using System.IO;

#endregion

namespace cope.IO
{
    /// <summary>
    /// Helper class to write XmlConfig
    /// </summary>
    public static class XmlConfigWriter
    {
        /// <summary>
        /// Writes the contents of the specified XmlConfig to the specified stream.
        /// </summary>
        /// <param name="xmlcon"></param>
        /// <param name="str"></param>
        public static void Write(XmlConfig xmlcon, Stream str)
        {
            xmlcon.Write(str);
        }
    }
}