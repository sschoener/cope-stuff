#region

using System.IO;
using cope.Extensions;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.SGA
{
    /// <summary>
    /// Class representing an EntryPoint from a SGA file
    /// </summary>
    public sealed class SGAEntryPoint : SGAContainer, IStreamExtBinaryCompatible
    {
        // raw data

        // virtual data for editing etc.
        private readonly ushort m_versionLower;
        private readonly ushort m_versionUpper;
        private string m_alias; // ASCII string

        private uint m_directoryOffset;
                     // offset to whatever, doesn't seem to be used at all. Should be relative to the EntryPoint

        #region Properties

        /// <summary>
        /// Gets or sets the Root-SGAStoredDirectory of this EntryPoint.
        /// </summary>
        public SGAStoredDirectory Root { get; set; }

        /// <summary>
        /// Gets or sets the alias of this EntryPoint. Takes ASCII strings.
        /// </summary>
        public string Alias
        {
            get { return m_alias; }
            set { m_alias = value; }
        }

        /// <summary>
        /// Gets or sets the so called DirectoryOffset of this EntryPoint which I really don't know what it is. It's normally set to 0.
        /// </summary>
        public uint DirectoryOffset
        {
            get { return m_directoryOffset; }
            set { m_directoryOffset = value; }
        }

        #endregion

        public SGAEntryPoint(ushort versionUpper, ushort versionLower)
        {
            m_versionLower = versionLower;
            m_versionUpper = versionUpper;
        }

        public SGAEntryPoint(Stream str, ushort versionUpper, ushort versionLower)
            : this(versionUpper, versionLower)
        {
            GetFromStream(str);
        }

        #region Methods

        public override string GetPath()
        {
            return string.Empty;
        }

        #endregion

        #region IStreamExtBinaryCompatible<SGAEntryPoint> Member

        public void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(BinaryWriter bw)
        {
            long baseOffset = bw.BaseStream.Position;
            bw.Write(m_name.ToByteArray(true));
            bw.BaseStream.Position = baseOffset + 64;
            bw.Write(m_alias.ToByteArray(true));
            bw.BaseStream.Position = baseOffset + 128;
            if (m_versionUpper == 5 && m_versionLower == 1)
            {
                bw.Write(DirectoryFirst);
                bw.Write(DirectoryLast);
                bw.Write(FileFirst);
                bw.Write(FileLast);
                bw.Write(m_directoryOffset);
            }
            else
            {
                bw.Write((ushort) DirectoryFirst);
                bw.Write((ushort) DirectoryLast);
                bw.Write((ushort) FileFirst);
                bw.Write((ushort) FileLast);
                bw.Write((ushort) m_directoryOffset);
            }
        }

        public void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        public void GetFromStream(BinaryReader br)
        {
            m_name = br.ReadBytes(64).ToString(true).SubstringBeforeFirst('\0');
            m_alias = br.ReadBytes(64).ToString(true).SubstringBeforeFirst('\0');
            if (m_versionUpper == 5 && m_versionLower == 1)
            {
                DirectoryFirst = br.ReadUInt32();
                DirectoryLast = br.ReadUInt32();
                FileFirst = br.ReadUInt32();
                FileLast = br.ReadUInt32();
                m_directoryOffset = br.ReadUInt32();
            }
            else
            {
                DirectoryFirst = br.ReadUInt16();
                DirectoryLast = br.ReadUInt16();
                FileFirst = br.ReadUInt16();
                FileLast = br.ReadUInt16();
                m_directoryOffset = br.ReadUInt16();
            }
        }

        #endregion
    }
}