#region

using System.IO;

#endregion

namespace cope.DawnOfWar2.SGANew
{
    internal class SGADataHeader
    {
        #region properties

        public uint EntryPointSectionOffset { get; set; }

        public uint EntryPointCount { get; set; }

        public uint DirectorySectionOffset { get; set; }

        public uint DirectoryCount { get; set; }

        public uint FileSectionOffset { get; set; }

        public uint FileCount { get; set; }

        public uint StringSectionOffset { get; set; }

        public uint StringCount { get; set; }

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
            if (version != SGAVersion.Version5_1)
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
            else
            {
                dataHeader.EntryPointSectionOffset = reader.ReadUInt32();
                dataHeader.EntryPointCount = reader.ReadUInt32();
                dataHeader.DirectorySectionOffset = reader.ReadUInt32();
                dataHeader.DirectoryCount = reader.ReadUInt32();
                dataHeader.FileSectionOffset = reader.ReadUInt32();
                dataHeader.FileCount = reader.ReadUInt32();
                dataHeader.StringSectionOffset = reader.ReadUInt32();
                dataHeader.StringCount = reader.ReadUInt32();
            }
            return dataHeader;
        }

        public static void Write(BinaryWriter writer, SGAVersion version, SGADataHeader header)
        {
            if (version != SGAVersion.Version5_1)
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