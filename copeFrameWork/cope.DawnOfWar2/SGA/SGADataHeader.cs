#region

using System;
using System.IO;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.SGA
{
    public class SGADataHeader : IStreamExtBinaryCompatible
    {
        #region fields

        // all these offsets are relative to the DataHeader!!!
        private readonly ushort m_versionLower;
        private readonly ushort m_versionUpper;

        #endregion

        #region ctors

        public SGADataHeader(ushort versionUpper, ushort versionLower)
        {
            m_versionUpper = versionUpper;
            m_versionLower = versionLower;
        }

        #endregion

        #region properties

        public uint DirectorySectionOffset { get; set; }

        public uint DirectoryCount { get; set; }

        public uint EntryPointSectionOffset { get; set; }

        public uint EntryPointCount { get; set; }

        public uint FileSectionOffset { get; set; }

        public uint FileCount { get; set; }

        public uint StringSectionOffset { get; set; }

        public uint StringCount { get; set; }

        public int Length
        {
            get
            {
                if (m_versionUpper == 5 && m_versionLower == 1)
                    return 8 * sizeof (Int32);
                return 4 * sizeof (Int32) + 4 * sizeof (Int16);
            }
        }

        #endregion

        #region IStreamExtBinaryCompatible<SGADataHeader> Member

        public void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(BinaryWriter bw)
        {
            bw.Write(EntryPointSectionOffset);
            if (m_versionLower == 1 && m_versionUpper == 5)
                bw.Write(EntryPointCount);
            else
                bw.Write((ushort) EntryPointCount);
            bw.Write(DirectorySectionOffset);
            if (m_versionLower == 1 && m_versionUpper == 5)
                bw.Write(DirectoryCount);
            else
                bw.Write((ushort) DirectoryCount);
            bw.Write(FileSectionOffset);
            if (m_versionLower == 1 && m_versionUpper == 5)
                bw.Write(FileCount);
            else
                bw.Write((ushort) FileCount);
            bw.Write(StringSectionOffset);
            if (m_versionLower == 1 && m_versionUpper == 5)
                bw.Write(StringCount);
            else
                bw.Write((ushort) StringCount);
        }

        public void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        public void GetFromStream(BinaryReader br)
        {
            EntryPointSectionOffset = br.ReadUInt32();
            if (m_versionUpper == 5 && m_versionLower == 1)
                EntryPointCount = br.ReadUInt32();
            else
                EntryPointCount = br.ReadUInt16();
            DirectorySectionOffset = br.ReadUInt32();
            if (m_versionUpper == 5 && m_versionLower == 1)
                DirectoryCount = br.ReadUInt32();
            else
                DirectoryCount = br.ReadUInt16();
            FileSectionOffset = br.ReadUInt32();
            if (m_versionUpper == 5 && m_versionLower == 1)
                FileCount = br.ReadUInt32();
            else
                FileCount = br.ReadUInt16();
            StringSectionOffset = br.ReadUInt32();
            if (m_versionUpper == 5 && m_versionLower == 1)
                StringCount = br.ReadUInt32();
            else
                StringCount = br.ReadUInt16();
        }

        #endregion
    }
}