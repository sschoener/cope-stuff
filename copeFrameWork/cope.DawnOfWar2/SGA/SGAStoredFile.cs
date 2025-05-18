#region

using System.IO;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.SGA
{
    public sealed class SGAStoredFile : Taggable, IStreamExtBinaryCompatible
    {
        #region fields

        private const uint LENGTH = sizeof (uint) * 5 + sizeof (ushort);
        // Raw Data
        private uint m_dataCompressedSize;
        private uint m_dataOffset; // offset relative to the beginning of the data-section
        private uint m_dataUnCompressedSize;
        private ushort m_flags = 256; // flags = 256 indicates that the file is compressed (last byte set).
        // added data
        private string m_name;
        private uint m_nameOffset; // offset relative to the beginning of the strings-section
        private SGAContainer m_parent;
        private uint m_unixTimeStamp;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the length of a file-entry in a SGA in bytes.
        /// </summary>
        public static uint Length
        {
            get { return LENGTH; }
        }

        /// <summary>
        /// Gets or sets the parent SGA of this SGAStoredFile.
        /// </summary>
        public SGAFile SGA { get; set; }

        /// <summary>
        /// Gets or sets the Offset of the name of this SGAStoredFile.
        /// </summary>
        public uint NameOffset
        {
            get { return m_nameOffset; }
            set { m_nameOffset = value; }
        }

        /// <summary>
        /// Gets or sets the Offset of the data of this SGAStoredFile.
        /// </summary>
        public uint DataOffset
        {
            get { return m_dataOffset; }
            set { m_dataOffset = value; }
        }

        /// <summary>
        /// Gets or sets the compressed size (in bytes) of the data held by this SGAStoredFile.
        /// </summary>
        public uint DataCompressedSize
        {
            get { return m_dataCompressedSize; }
            set { m_dataCompressedSize = value; }
        }

        /// <summary>
        /// Gets or sets the compressed size (in bytes) of the data held by this SGAStoredFile.
        /// </summary>
        public uint DataUnCompressedSize
        {
            get { return m_dataUnCompressedSize; }
            set { m_dataUnCompressedSize = value; }
        }

        /// <summary>
        /// Gets or sets the timestamp for this SGAStoredFile.
        /// </summary>
        public uint UnixTimeStamp
        {
            get { return m_unixTimeStamp; }
            set { m_unixTimeStamp = value; }
        }

        /// <summary>
        /// Gets or sets the flags of this SGAStoredFile. Normally 1 is used for compressed files.
        /// </summary>
        public ushort Flags
        {
            get { return m_flags; }
            set { m_flags = value; }
        }

        /// <summary>
        /// Gets or sets the index of this SGAStoredFile.
        /// </summary>
        public uint Index { get; set; }

        /// <summary>
        /// Gets or sets the name of this RBFValue. Takes ASCII strings.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the parent of this RBFValue.
        /// </summary>
        public SGAContainer Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

        #endregion

        #region ctors

        public SGAStoredFile()
        {
        }

        public SGAStoredFile(Stream str, uint index)
        {
            GetFromStream(str);
            Index = index;
        }

        #endregion

        /// <summary>
        /// Returns the path to this SGAStoredFile.
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            if (m_parent == null)
                return m_name;
            string parentPath = m_parent.GetPath();
            if (parentPath != string.Empty)
                parentPath += '\\';
            return parentPath + m_name;
        }

        #region IStreamExtBinaryCompatible<SGAStoredFile> Member

        public void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(BinaryWriter bw)
        {
            bw.Write(m_nameOffset);
            bw.Write(m_dataOffset);
            bw.Write(m_dataCompressedSize);
            bw.Write(m_dataUnCompressedSize);
            bw.Write(m_unixTimeStamp);
            bw.Write(m_flags);
        }

        public void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        public void GetFromStream(BinaryReader br)
        {
            m_nameOffset = br.ReadUInt32();
            m_dataOffset = br.ReadUInt32();
            m_dataCompressedSize = br.ReadUInt32();
            m_dataUnCompressedSize = br.ReadUInt32();
            m_unixTimeStamp = br.ReadUInt32();
            m_flags = br.ReadUInt16();
        }

        #endregion
    }
}