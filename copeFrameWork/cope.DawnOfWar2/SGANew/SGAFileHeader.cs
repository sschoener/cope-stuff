#region

using System.IO;
using System.Linq;
using cope.Extensions;

#endregion

namespace cope.DawnOfWar2.SGANew
{
    internal class SGAFileHeader
    {
        private const uint PLATFORM_X86 = 1; // if platform > 255 -> invert endianess!
        private static readonly byte[] s_stdSignature = "_ARCHIVE".ToByteArray(true);

        private readonly SGAVersion m_version;
        private byte[] m_contentChecksum;

        private string m_sName; // UNICODE STRING!

        private uint m_uFlags;
        private uint m_uUnixTimestamp;

        public SGAFileHeader(SGAVersion version)
        {
            m_version = version;
        }

        #region properties

        public SGAVersion Version
        {
            get { return m_version; }
        }

        public byte[] DataHeaderChecksum { get; set; }

        public uint DataHeaderOffset { get; set; }

        public uint DataHeaderSize { get; set; }

        public uint DataOffset { get; set; }

        #endregion

        public static SGAVersion GetVersion(ushort upperVersion, ushort lowerVersion)
        {
            if (upperVersion == 5)
            {
                if (lowerVersion == 1)
                    return SGAVersion.Version5_1;
                if (lowerVersion == 0)
                    return SGAVersion.Version5_0;
            }
            else if (upperVersion == 4)
            {
                if (lowerVersion == 1)
                    return SGAVersion.Version4_1;
                if (lowerVersion == 0)
                    return SGAVersion.Version4_0;
            }
            return SGAVersion.VersionInvalid;
        }

        /// <exception cref="CopeDoW2Exception"><c>CopeDoW2Exception</c>.</exception>
        public static SGAFileHeader Read(BinaryReader reader)
        {
            byte[] signature = reader.ReadBytes(8);
            if (!signature.SequenceEqual(s_stdSignature))
                throw new CopeDoW2Exception("This file does not seem to be an SGA file. Unknown signature: " +
                                            signature.ToString(true));
            ushort versionUpper = reader.ReadUInt16();
            ushort versionLower = reader.ReadUInt16();
            SGAVersion version = GetVersion(versionUpper, versionLower);
            if (version == SGAVersion.VersionInvalid)
                throw new CopeDoW2Exception("Invalid/unsupported SGA version encountered: " + versionUpper + "." +
                                            versionLower);
            SGAFileHeader header = new SGAFileHeader(version);

            header.m_contentChecksum = reader.ReadBytes(16);
            header.m_sName = reader.ReadUnicodeString(128).SubstringBeforeFirst('\0');
            header.DataHeaderChecksum = reader.ReadBytes(16);
            header.DataHeaderSize = reader.ReadUInt32();
            header.DataOffset = reader.ReadUInt32();

            if (version == SGAVersion.Version5_0)
                header.DataHeaderOffset = reader.ReadUInt32();
            uint platform = reader.ReadUInt32();
            if (platform != PLATFORM_X86)
                throw new CopeDoW2Exception("Invalid/unsupported SGA platform: " + platform);

            if (version == SGAVersion.Version5_0)
            {
                header.m_uFlags = reader.ReadUInt32();
                header.m_uUnixTimestamp = reader.ReadUInt32();
            }
            if (version != SGAVersion.Version5_0)
                header.DataHeaderOffset = (uint) reader.BaseStream.Position;
            return header;
        }

        public static void Write(BinaryWriter writer, SGAFileHeader header)
        {
            writer.Write(s_stdSignature);

            switch (header.m_version)
            {
                case SGAVersion.Version4_0:
                    writer.Write((ushort) 4);
                    writer.Write((ushort) 0);
                    break;
                case SGAVersion.Version4_1:
                    writer.Write((ushort) 4);
                    writer.Write((ushort) 1);
                    break;
                case SGAVersion.Version5_0:
                    writer.Write((ushort) 5);
                    writer.Write((ushort) 0);
                    break;
                case SGAVersion.Version5_1:
                    writer.Write((ushort) 5);
                    writer.Write((ushort) 1);
                    break;
            }

            writer.Write(header.m_contentChecksum);
            long currentPos = writer.BaseStream.Position;
            writer.Write(header.m_sName.ToByteArray(false));
            writer.BaseStream.Position = currentPos + 128;
            writer.Write(header.DataHeaderChecksum);
            writer.Write(header.DataHeaderSize);
            writer.Write(header.DataOffset);
            if (header.m_version == SGAVersion.Version5_0)
                writer.Write(header.DataHeaderOffset);

            writer.Write(PLATFORM_X86);
            if (header.m_version == SGAVersion.Version5_0)
            {
                writer.Write(header.m_uFlags);
                writer.Write(header.m_uUnixTimestamp);
            }
        }
    }
}