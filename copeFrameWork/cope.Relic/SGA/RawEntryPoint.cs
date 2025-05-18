using System.IO;
using cope.Extensions;

namespace cope.Relic.SGA
{
    internal class RawEntryPoint
    {
        #region properties

        /// <summary>
        /// Gets/sets the alias of this entry point (ASCII). The alias is a short description of the contents.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets/sets the name of this entry point (ASCII). E.g. 'DATA' or 'ATTRIB'.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the index of the first directory in this entry point.
        /// The first directory of an entry point is the root directory.
        /// </summary>
        public uint IndexOfFirstDirectory { get; set; }

        /// <summary>
        /// Gets/sets the index of the first directory which is _not_ in this entry point anymore.
        /// </summary>
        public uint IndexOfLastDirectory { get; set; }

        /// <summary>
        /// Gets/sets the index of the first file in this entry point.
        /// </summary>
        public uint IndexOfFirstFile { get; set; }

        /// <summary>
        /// Gets/sets the index of the first file which is _not_ in this entry point anymore.
        /// </summary>
        public uint IndexOfLastFile { get; set; }

        public uint UnknownValue { get; set; }

        /// <summary>
        /// Gets or sets the offset of this entry point descriptor in the archive.
        /// </summary>
        public long ThisDescriptorOffset { get; set; }

        #endregion

        public static RawEntryPoint Read(BinaryReader reader, SGAVersion version)
        {
            var entryPoint = new RawEntryPoint();
            entryPoint.Name = reader.ReadAsciiString(64).SubstringBeforeFirst('\0');
            entryPoint.Alias = reader.ReadAsciiString(64).SubstringBeforeFirst('\0');
            if (SGAReader.Uses32BitEntries(version))
            {
                entryPoint.IndexOfFirstDirectory = reader.ReadUInt32();
                entryPoint.IndexOfLastDirectory = reader.ReadUInt32();
                entryPoint.IndexOfFirstFile = reader.ReadUInt32();
                entryPoint.IndexOfLastFile = reader.ReadUInt32();
                entryPoint.UnknownValue = reader.ReadUInt32();
            }
            else
            {
                entryPoint.IndexOfFirstDirectory = reader.ReadUInt16();
                entryPoint.IndexOfLastDirectory = reader.ReadUInt16();
                entryPoint.IndexOfFirstFile = reader.ReadUInt16();
                entryPoint.IndexOfLastFile = reader.ReadUInt16();
                entryPoint.UnknownValue = reader.ReadUInt16();
            }

            return entryPoint;
        }
    }
}