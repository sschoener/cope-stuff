#region

using System.IO;

#endregion

namespace cope.Relic.SGA
{
    public sealed class SGADataHeader
    {
        #region properties

        /// <summary>
        /// Relative offset (relative to the DataHeader) to the entry point section.
        /// </summary>
        public uint EntryPointSectionOffset { get; set; }

        /// <summary>
        /// Gets/sets the number of entry points in the entry point section.
        /// Before SGAV5.1 this was a ushort.
        /// </summary>
        public uint EntryPointCount { get; set; }

        /// <summary>
        /// Relative offset (relative to the DataHeader) to the directory section.
        /// </summary>
        public uint DirectorySectionOffset { get; set; }

        /// <summary>
        /// Gets/sets the number of directories in the directory section.
        /// Before SGAV5.1 this was a ushort.
        /// </summary>
        public uint DirectoryCount { get; set; }

        /// <summary>
        /// Relative offset (relative to the DataHeader) to the file section.
        /// </summary>
        public uint FileSectionOffset { get; set; }

        /// <summary>
        /// Gets/sets the number of files in the file section.
        /// Before SGAV5.1 this was a ushort.
        /// </summary>
        public uint FileCount { get; set; }

        /// <summary>
        /// Relative offset (relative to the DataHeader) to the string section.
        /// </summary>
        public uint StringSectionOffset { get; set; }

        /// <summary>
        /// Gets/Sets the number of strings -- _not_ the length in bytes!
        /// Before SGAV5.1 this was a ushort.
        /// </summary>
        public uint StringCount { get; set; }

        public uint StringBlobSize { get; set; }

        #endregion

        public static uint GetLengthInBytes(SGAVersion version)
        {
            if (version == SGAVersion.Version5_1)
            {
                return 8 * sizeof (uint);
            }
            return 4 * sizeof (uint) + 4 * sizeof (ushort);
        }

        public static SGADataHeader Read(BinaryReader reader, SGAVersion version)
        {
            var dataHeader = new SGADataHeader();
            if (SGAReader.Uses32BitEntries(version))
            {
                dataHeader.EntryPointSectionOffset = reader.ReadUInt32();
                dataHeader.EntryPointCount = reader.ReadUInt32();
                dataHeader.DirectorySectionOffset = reader.ReadUInt32();
                dataHeader.DirectoryCount = reader.ReadUInt32();
                dataHeader.FileSectionOffset = reader.ReadUInt32();
                dataHeader.FileCount = reader.ReadUInt32();
                dataHeader.StringSectionOffset = reader.ReadUInt32();
                if (version == SGAVersion.Version9_0)
                {
                    dataHeader.StringBlobSize = reader.ReadUInt32();
                } else
                {
                    dataHeader.StringCount = reader.ReadUInt32();
                    dataHeader.StringBlobSize = 0;
                }
            }
            else
            {
                dataHeader.EntryPointSectionOffset = reader.ReadUInt32();
                dataHeader.EntryPointCount = reader.ReadUInt16();
                dataHeader.DirectorySectionOffset = reader.ReadUInt32();
                dataHeader.DirectoryCount = reader.ReadUInt16();
                dataHeader.FileSectionOffset = reader.ReadUInt32();
                dataHeader.FileCount = reader.ReadUInt16();
                dataHeader.StringSectionOffset = reader.ReadUInt32();
                dataHeader.StringCount = reader.ReadUInt16();
            }
            return dataHeader;
        }

        public static void Write(BinaryWriter writer, SGAVersion version, SGADataHeader header)
        {
            if (!SGAReader.Uses32BitEntries(version))
            {
                writer.Write(header.EntryPointSectionOffset);
                writer.Write((ushort) header.EntryPointCount);
                writer.Write(header.DirectorySectionOffset);
                writer.Write((ushort) header.DirectoryCount);
                writer.Write(header.FileSectionOffset);
                writer.Write((ushort) header.FileCount);
                writer.Write(header.StringSectionOffset);
                writer.Write((ushort) header.StringCount);
            }
            else
            {
                writer.Write(header.EntryPointSectionOffset);
                writer.Write(header.EntryPointCount);
                writer.Write(header.DirectorySectionOffset);
                writer.Write(header.DirectoryCount);
                writer.Write(header.FileSectionOffset);
                writer.Write(header.FileCount);
                writer.Write(header.StringSectionOffset);
                writer.Write(header.StringCount);
            }
        }
    }
}