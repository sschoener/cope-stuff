#region

using System;
using System.IO;
using cope.Extensions;

#endregion

namespace cope.Relic.RelicChunky
{
    public class ChunkHeader : IGenericClonable<ChunkHeader>
    {
        #region fields

        /// <summary>
        /// The signature of this ChunkHeader, e.g. ACTN
        /// </summary>
        protected byte[] m_signature;

        #endregion

        #region constructors

        /// <summary>
        /// Constructs a new ChunkHeader which is totally uninitialized.
        /// </summary>
        public ChunkHeader(uint fileVersion)
        {
            FileVersion = fileVersion;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the length of the Header.
        /// </summary>
        public int Length
        {
            get { return ComputeHeaderLength((int) FileVersion, Name); }
        }

        /// <summary>
        /// Gets the length of the Header without the name of the chunk.
        /// </summary>
        public int LengthWithoutName
        {
            get { return ComputeHeaderLength((int) FileVersion, string.Empty); }
        }

        /// <summary>
        /// Gets or sets the name of this ChunkHeader.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the signature of this ChunkHeader.
        /// </summary>
        public string Signature
        {
            get { return m_signature.RemoveComparable((byte) 0x00).ToString(true); }
            set
            {
                if (value == null)
                {
                    var excep = new RelicException("Invalid Signature for ChunkHeader: null!");
                    excep.Data["ChunkHeader"] = this;
                    excep.Data["Signature"] = "null";
                    throw excep;
                }
                if (value.Length > 4)
                {
                    var excep = new RelicException("Invalid Signature for ChunkHeader: too long!");
                    excep.Data["ChunkHeader"] = this;
                    excep.Data["Signature"] = value;
                    throw excep;
                }
                if (value.Length < 4)
                {
                    int missing = 4 - value.Length;
                    m_signature = value.Append(" ", missing).ToByteArray(true);
                }
                m_signature = value.ToByteArray(true);
            }
        }

        /// <exception cref="RelicException">Invalid Chunk Identifier: Wrong size! Must be 4 Bytes!</exception>
        public byte[] SignatureAsByte
        {
            get { return m_signature; }
            set
            {
                if (value.Length != 4)
                    throw new RelicException("Invalid Chunk Identifier: Wrong size! Must be 4 Bytes!");
                m_signature = value;
            }
        }

        /// <summary>
        /// Gets or sets the MinVersion of this ChunkHeader.
        /// </summary>
        public int MinVersion { get; set; }

        /// <summary>
        /// Gets or sets the chunkSize of this ChunkHeader. Be careful with setting it!
        /// </summary>
        public uint ChunkSize { get; set; }

        /// <summary>
        /// Gets or sets the Flags of this ChunkHeader.
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// Gets or sets the version of this ChunkHeader.
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// Gets or sets the type of this ChunkHeader (either DATA or FOLD).
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

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return TypeString + Signature;
            return TypeString + Signature + " - " + Name;
        }

        #region IStreamExtBinaryCompatible<ChunkHeader> Member

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
            if (!string.IsNullOrEmpty(Name))
                bw.Write(Name.Length + 1);
            else
                bw.Write(0);
            if (FileVersion == 2 || FileVersion == 3)
            {
                bw.Write(MinVersion);
                if (FileVersion == 3)
                    bw.Write(Flags);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                bw.Write(Name.ToByteArray(true));
                bw.Write((byte) 0x00);
            }
                
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
            Version = br.ReadUInt32();
            ChunkSize = br.ReadUInt32();
            uint nameLength = br.ReadUInt32();
            if (FileVersion == 2 || FileVersion == 3)
            {
                MinVersion = br.ReadInt32();
                if (FileVersion == 3)
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

        #region IGenericClonable<ChunkHeader> Member

        public ChunkHeader GClone()
        {
            var rch = new ChunkHeader(FileVersion)
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

        public static int ComputeHeaderLength(int fileversion, string name )
        {
            int length = 5 * sizeof(uint);
            if (!string.IsNullOrEmpty(name))
                length += name.Length + 1;
            if (fileversion == 2 || fileversion == 3)
            {
                length += sizeof(uint);
                if (fileversion == 3)
                    length += sizeof(uint);
            }
            return length;
        }

        public static ChunkHeader FromChunkInfo(ChunkWriter.ChunkInfo info, string name, string signature, ChunkType type)
        {
            var header = new ChunkHeader(info.FileVersion)
                             {
                                 Flags = info.Flags,
                                 MinVersion = info.MinVersion,
                                 Name = name,
                                 Signature = signature,
                                 Version = info.Version,
                                 Type = type
                             };
            return header;
        }
    }
}