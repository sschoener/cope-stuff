#region

using System;
using System.IO;
using cope.Extensions;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.RelicChunky
{
    public enum ChunkType : byte
    {
        DATA,
        FOLD
    }

    public class RelicChunkHeader : IStreamExtBinaryCompatible, IGenericClonable<RelicChunkHeader>
    {
        #region fields

        /// <summary>
        /// The signature of this RelicChunkHeader, e.g. ACTN
        /// </summary>
        protected byte[] m_signature;

        #endregion

        #region constructors

        /// <summary>
        /// Constructs a new RelicChunkHeader and initializes it with data read from the stream.
        /// </summary>
        /// <param name="str">Stream to read data from.</param>
        /// <param name="fileVersion"></param>
        public RelicChunkHeader(Stream str, uint fileVersion)
        {
            FileVersion = fileVersion;
            GetFromStream(str);
        }

        public RelicChunkHeader(BinaryReader br, uint fileVersion)
        {
            FileVersion = fileVersion;
            GetFromStream(br);
        }

        /// <summary>
        /// Constructs a new RelicChunkHeader which is totally uninitialized.
        /// </summary>
        public RelicChunkHeader()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the length of the Header.
        /// </summary>
        public int Length
        {
            get
            {
                int length = 5 * sizeof (UInt32);
                if (Name.Length != 0)
                    length += Name.Length + 1;
                if (FileVersion >= 2)
                {
                    length += sizeof (UInt32);
                    if (FileVersion >= 3)
                        length += sizeof (UInt32);
                }
                return length;
            }
        }

        /// <summary>
        /// Gets the length of the Header without the name of the chunk.
        /// </summary>
        public int LengthWithoutName
        {
            get
            {
                int length = 5 * sizeof (UInt32);
                if (FileVersion >= 2)
                {
                    length += sizeof (UInt32);
                    if (FileVersion >= 3)
                        length += sizeof (UInt32);
                }
                return length;
            }
        }

        /// <summary>
        /// Gets or sets the name of this RelicChunkHeader.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the signature of this RelicChunkData.
        /// </summary>
        public string Signature
        {
            get { return m_signature.RemoveComparable((byte) 0x00).ToString(true); }
            set
            {
                if (value.Length < 4)
                {
                    int missing = 4 - value.Length;
                    m_signature = value.Append(" ", missing).ToByteArray(true);
                }
                m_signature = value.ToByteArray(true);
            }
        }

        /// <exception cref="CopeDoW2Exception">Invalid Chunk Identifier: Wrong size! Must be 4 Bytes!</exception>
        public byte[] SignatureAsByte
        {
            get { return m_signature; }
            set
            {
                if (value.Length != 4)
                    throw new CopeDoW2Exception("Invalid Chunk Identifier: Wrong size! Must be 4 Bytes!");
                m_signature = value;
            }
        }

        /// <summary>
        /// Gets or sets the MinVersion of this RelicChunky.
        /// </summary>
        public int MinVersion { get; set; }

        /// <summary>
        /// Gets or sets the chunkSize of this RelicChunkHeader. Be careful with setting it!
        /// </summary>
        public uint ChunkSize { get; set; }

        /// <summary>
        /// Gets or sets the Flags of this RelicChunkHeader.
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// Gets or sets the version of this Chunk.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the type of this Chunk (either DATA or FOLD).
        /// </summary>
        public ChunkType Type { get; set; }

        /// <summary>
        /// Gets the string connected to this Chunk's type.
        /// </summary>
        public string TypeString
        {
            get
            {
                switch (Type)
                {
                    case ChunkType.DATA:
                        return "DATA";
                    case ChunkType.FOLD:
                        return "FOLD";
                }
                return "UNKN";
            }
        }

        /// <summary>
        /// Gets or sets the version of the file this chunk is in.
        /// </summary>
        public uint FileVersion { get; set; }

        #endregion

        #region IStreamExtBinaryCompatible<RelicChunkHeader> Member

        public void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(BinaryWriter bw)
        {
            bw.Write(Type == ChunkType.DATA ? "DATA".ToByteArray(true) : "FOLD".ToByteArray(true));
            bw.Write(m_signature);
            bw.Write(Version);
            bw.Write(ChunkSize);
            if (Name.Length != 0)
                bw.Write(Name.Length + 1);
            else
                bw.Write(Name.Length);
            if (FileVersion >= 2)
            {
                bw.Write(MinVersion);
                if (FileVersion >= 3)
                    bw.Write(Flags);
            }
            if (Name.Length != 0)
                bw.Write(Name.ToByteArray(true).Append((byte) 0x00));
        }

        public void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        public void GetFromStream(BinaryReader br)
        {
            string type = br.ReadBytes(4).ToString(true);
            Type = type.Equals("FOLD") ? ChunkType.FOLD : ChunkType.DATA;
            m_signature = br.ReadBytes(4);
            Version = br.ReadInt32();
            ChunkSize = br.ReadUInt32();
            uint nameLength = br.ReadUInt32();
            if (FileVersion >= 2)
            {
                MinVersion = br.ReadInt32();
                if (FileVersion >= 3)
                    Flags = br.ReadUInt32();
            }
            if (nameLength == 0)
                Name = string.Empty;
            else
            {
                Name = br.ReadBytes((int) nameLength - 1).ToString(true);
                br.ReadByte();
            }
        }

        #endregion

        #region IGenericClonable<RelicChunkHeader> Member

        public RelicChunkHeader GClone()
        {
            var rch = new RelicChunkHeader
                          {
                              ChunkSize = ChunkSize,
                              Flags = Flags,
                              MinVersion = MinVersion,
                              Name = Name,
                              m_signature = (byte[]) m_signature.Clone(),
                              Type = Type,
                              Version = Version,
                              FileVersion = FileVersion
                          };
            return rch;
        }

        #endregion
    }
}