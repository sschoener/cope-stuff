#region

using System.IO;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.SGA
{
    public sealed class SGAStoredDirectory : SGAContainer, IStreamExtBinaryCompatible
    {
        #region fields

        private const uint LENGTH = sizeof (uint) + 4 * sizeof (ushort);
        // Raw Data
        // virtual data
        private readonly ushort m_versionLower;
        private readonly ushort m_versionUpper;
        private uint m_nameOffset; // relative to the beginning of the string section

        #endregion

        #region Properties

        /// <summary>
        /// Gets the length of a directory-entry in a SGA in bytes.
        /// </summary>
        public static uint Length
        {
            get { return LENGTH; }
        }

        /// <summary>
        /// Gets or sets the Offset of the name of this SGAStoredDirectory.
        /// </summary>
        public uint NameOffset
        {
            get { return m_nameOffset; }
            set { m_nameOffset = value; }
        }

        /// <summary>
        /// Gets or sets the index of this SGAStoredDirectory.
        /// </summary>
        public uint Index { get; set; }

        #endregion

        #region ctors

        public SGAStoredDirectory(ushort versionUpper, ushort versionLower)
        {
            m_versionLower = versionLower;
            m_versionUpper = versionUpper;
        }

        public SGAStoredDirectory(Stream str, uint index, ushort versionUpper, ushort versionLower)
            : this(versionUpper, versionLower)
        {
            GetFromStream(str);
            Index = index;
        }

        #endregion

        #region Methods

        #endregion

        #region IStreamExtBinaryCompatible<SGAStoredDirectory> Member

        public void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(BinaryWriter bw)
        {
            bw.Write(m_nameOffset);
            if (m_versionUpper == 5 && m_versionLower == 1)
            {
                bw.Write(DirectoryFirst);
                bw.Write(DirectoryLast);
                bw.Write(FileFirst);
                bw.Write(FileLast);
            }
            else
            {
                bw.Write((ushort) DirectoryFirst);
                bw.Write((ushort) DirectoryLast);
                bw.Write((ushort) FileFirst);
                bw.Write((ushort) FileLast);
            }
        }

        public void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        public void GetFromStream(BinaryReader br)
        {
            m_nameOffset = br.ReadUInt32();
            if (m_versionUpper == 5 && m_versionLower == 1)
            {
                DirectoryFirst = br.ReadUInt32();
                DirectoryLast = br.ReadUInt32();
                FileFirst = br.ReadUInt32();
                FileLast = br.ReadUInt32();
            }
            else
            {
                DirectoryFirst = br.ReadUInt16();
                DirectoryLast = br.ReadUInt16();
                FileFirst = br.ReadUInt16();
                FileLast = br.ReadUInt16();
            }
        }

        #endregion
    }
}