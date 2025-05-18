#region

using System.IO;
using System.Linq;
using cope.Extensions;

#endregion

namespace cope.Relic.SGA
{
	public sealed class SGAFileHeader
	{
		private const uint PLATFORM_X86 = 1; // if platform > 255 -> invert endianess!
		private static readonly byte[] s_stdSignature = "_ARCHIVE".ToByteArray (true);

		private readonly SGAVersion m_version;
		private byte[] m_contentChecksum;

		private uint m_uFlags;
		private uint m_uUnixTimestamp;

		public SGAFileHeader (SGAVersion version)
		{
			m_version = version;
		}

		#region properties

		public SGAVersion Version {
			get { return m_version; }
		}

		public byte[] DataHeaderChecksum { get; set; }

		/// <summary>
		/// Offset of the data header.
		/// </summary>
		public uint DataHeaderOffset { get; set; }

		/// <summary>
		/// Size of the data header.
		/// </summary>
		public uint DataHeaderSize { get; set; }

		/// <summary>
		/// Offset of the data part of the file.
		/// </summary>
		public uint DataOffset { get; set; }

		/// <summary>
		/// Unicode string containing the archive name.
		/// </summary>
		public string ArchiveName { get; set; }

		#endregion

		public static SGAVersion GetVersion (ushort upperVersion, ushort lowerVersion)
		{
            if (upperVersion == 9)
            {
                if (lowerVersion == 0)
                    return SGAVersion.Version9_0;
            }
            if (upperVersion == 7)
            {
                if (lowerVersion == 0)
                    return SGAVersion.Version7_0;
            }
            else if (upperVersion == 6)
            {
                if (lowerVersion == 0)
                    return SGAVersion.Version6_0;
            }
            else if (upperVersion == 5)
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

		/// <exception cref="RelicException"><c>RelicException</c>.</exception>
		public static SGAFileHeader Read (BinaryReader reader, SGAVersion forceVersion = SGAVersion.VersionInvalid)
		{
			byte[] signature = reader.ReadBytes (8);
			if (!signature.SequenceEqual (s_stdSignature))
				throw new RelicException ("This file does not seem to be an SGA file. Unknown signature: " +
					signature.ToString (true));
			ushort versionUpper = reader.ReadUInt16 ();
			ushort versionLower = reader.ReadUInt16 ();
			SGAVersion version = GetVersion (versionUpper, versionLower);
			if (version == SGAVersion.VersionInvalid && forceVersion == SGAVersion.VersionInvalid)
                throw new RelicException("Invalid/unsupported SGA version encountered: " + versionUpper + "." + versionLower);
            if (forceVersion != SGAVersion.VersionInvalid)
				version = forceVersion;

			SGAFileHeader header = new SGAFileHeader (version);

            if (SGAReader.UsesHeaderHashes(version))
				header.m_contentChecksum = reader.ReadBytes (16);

			header.ArchiveName = reader.ReadUnicodeString (128).SubstringBeforeFirst ('\0');

            if (SGAReader.UsesHeaderHashes(version))
				header.DataHeaderChecksum = reader.ReadBytes (16);
            if (version == SGAVersion.Version9_0)
            {
                header.DataHeaderOffset = reader.ReadUInt32();
                reader.ReadUInt32();
                reader.ReadUInt32();
                header.DataOffset = reader.ReadUInt32();
                //header.DataSize = reader.ReadUInt32();
            }
            else
            {
                header.DataHeaderSize = reader.ReadUInt32();
                header.DataOffset = reader.ReadUInt32();
                if (version == SGAVersion.Version5_0)
                    header.DataHeaderOffset = reader.ReadUInt32();
                uint platform = reader.ReadUInt32();
                if (platform != PLATFORM_X86)
                    throw new RelicException("Invalid/unsupported SGA platform: " + platform);

                if (version == SGAVersion.Version5_0)
                {
                    header.m_uFlags = reader.ReadUInt32();
                    header.m_uUnixTimestamp = reader.ReadUInt32();
                }
                if (version != SGAVersion.Version5_0)
                    header.DataHeaderOffset = (uint)reader.BaseStream.Position;
            }
			return header;
		}

		public static void Write (BinaryWriter writer, SGAFileHeader header)
		{
			writer.Write (s_stdSignature);

			switch (header.m_version) {
			case SGAVersion.Version4_0:
				writer.Write ((ushort)4);
				writer.Write ((ushort)0);
				break;
			case SGAVersion.Version4_1:
				writer.Write ((ushort)4);
				writer.Write ((ushort)1);
				break;
			case SGAVersion.Version5_0:
				writer.Write ((ushort)5);
				writer.Write ((ushort)0);
				break;
			case SGAVersion.Version5_1:
				writer.Write ((ushort)5);
				writer.Write ((ushort)1);
				break;
			case SGAVersion.Version6_0:
				writer.Write((ushort)6);
				writer.Write((ushort)0);
				break;
			}

			if (header.Version != SGAVersion.Version6_0)
				writer.Write (header.m_contentChecksum);
			long currentPos = writer.BaseStream.Position;
			writer.Write(header.ArchiveName.ToByteArray(false));
			writer.BaseStream.Position = currentPos + 128;
			if (header.Version != SGAVersion.Version6_0)
				writer.Write (header.DataHeaderChecksum);
			writer.Write (header.DataHeaderSize);
			writer.Write (header.DataOffset);
			if (header.m_version == SGAVersion.Version5_0)
				writer.Write (header.DataHeaderOffset);

			writer.Write (PLATFORM_X86);
			if (header.m_version == SGAVersion.Version5_0) {
				writer.Write (header.m_uFlags);
				writer.Write (header.m_uUnixTimestamp);
			}
		}
	}
}