#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cope.Extensions;
using cope.FileSystem;
using cope.Helper;

#endregion

namespace cope.Relic.SGA
{
	public class SGAReader
	{
		private const char PATH_SEPARATOR = '\\';
		private SGADataHeader m_dataHeader;
		private SGAFileHeader m_fileHeader;

		private long m_lBaseOffset;
		private long m_lDataHeaderOffset;
		private long m_lStringsOffset;
		private BinaryReader m_reader;
		private Dictionary<uint, string> m_stringsByOffset;
		private SGAVersion m_forceVersion = SGAVersion.VersionInvalid;

		private SGAReader ()
		{
		}

		/// <summary>
		/// Reads a SGA file and returns all SGAEntryPoints.
		/// The Stream will be used by the EntryPoint to actually read the filedata, so do _not_ close ist.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static SGAEntryPoint[] Read (Stream str)
		{
			SGAReader reader = new SGAReader();
			return  reader.ConstructEntryPoints(str, reader.ReadHelper(str));
		}

		/// <summary>
		/// Reads a SGA file and returns all SGAEntryPoints. Forces the reader to try to use
		/// a specific verison, not matter what the SGA looks like.
		/// The Stream will be used by the EntryPoint to actually read the filedata, so do _not_ close ist.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="forceVersion">The SGA version to use.</param>
		/// <returns></returns>
		public static SGAEntryPoint[] Read(Stream str, SGAVersion forceVersion)
		{
			SGAReader reader = new SGAReader {m_forceVersion = forceVersion};
			return reader.ConstructEntryPoints(str, reader.ReadHelper(str));
		}

		public static void DumpSGAInfo(Stream sgaStream, TextWriter dumpWriter)
		{
            SGAReader reader = new SGAReader();
            var rawSGA = reader.ReadHelper(sgaStream);
		    var eps = reader.ConstructEntryPoints(sgaStream, rawSGA);

		    var firstEp = eps[0];
		    var filepaths = new string[rawSGA.Files.Length];
            foreach (var entry in firstEp.GetFileWalker())
            {
                if (entry is SGAStoredFile)
                {
                    var sgaFile = entry as SGAStoredFile;
                    var idx = sgaFile.FileEntry.FileIndex;
                    filepaths[idx] = sgaFile.GetPath();
                }
            }

			SGADumper.DumpInfo(rawSGA, dumpWriter, filepaths);
		}

		internal static RawSGADescriptor ReadRaw(Stream str)
		{
			SGAReader reader = new SGAReader();
			return reader.ReadHelper(str);
		}

		internal static RawSGADescriptor ReadRaw(Stream str, SGAVersion forceVersion)
		{
			SGAReader reader = new SGAReader { m_forceVersion = forceVersion };
			return reader.ReadHelper(str);
		}

		internal static bool Uses32BitEntries(SGAVersion version)
		{
			return version == SGAVersion.Version5_0_CoH2 || version == SGAVersion.Version5_1 ||
				   version == SGAVersion.Version6_0 || version == SGAVersion.Version7_0 || version == SGAVersion.Version9_0;
		}

        internal static bool HasFileCRCs(SGAVersion version)
        {
            return version == SGAVersion.Version6_0 || version == SGAVersion.Version7_0;
        }

        internal static bool HasStringEncryption(SGAVersion version)
        {
            return version == SGAVersion.Version4_1 || version == SGAVersion.Version5_1;
        }

        internal static bool UsesHeaderHashes(SGAVersion version)
        {
            return new[] {SGAVersion.Version4_0, SGAVersion.Version4_1, SGAVersion.Version5_0, SGAVersion.Version5_0_CoH2, SGAVersion.Version5_1}.Contains(version);
        }

		/// <exception cref="RelicException">Error while reading data header of SGA file.</exception>
		private RawSGADescriptor ReadHelper (Stream str)
		{
			m_lBaseOffset = str.Position;
			m_reader = new BinaryReader (str);

			// read file header
			try {
				m_fileHeader = SGAFileHeader.Read (m_reader, m_forceVersion);
			} catch (Exception ex) {
				throw new RelicException (ex, "Error while reading header of SGA file.");
			}

			m_lDataHeaderOffset = m_lBaseOffset + m_fileHeader.DataHeaderOffset;
			//CheckDataHeaderHash();

			// initialize data header
			try {
				m_reader.BaseStream.Position = m_lBaseOffset + m_fileHeader.DataHeaderOffset;
				m_dataHeader = SGADataHeader.Read (m_reader, m_fileHeader.Version);
			} catch (Exception ex) {
				throw new RelicException (ex, "Error while reading data header of SGA file.");
			}

			// read strings, etc.

			m_lStringsOffset = m_lBaseOffset + m_fileHeader.DataHeaderOffset + m_dataHeader.StringSectionOffset;
			try {
				m_stringsByOffset = GetCStrings (ReadStringSection ());
			} catch (Exception ex) {
				throw new RelicException (ex, "Failed to read strings from SGA file.");
			}

			RawEntryPoint[] entryPoints;
			try {
				entryPoints = ReadEntryPoints ();
			} catch (Exception ex) {
				throw new RelicException (ex, "Failed to read entry points from SGA file.");
			}

			RawDirectoryDescriptor[] directories;
			try {
				directories = ReadDirectories ();
			} catch (Exception ex) {
				throw new RelicException (ex, "Failed to read directory descriptors from SGA file.");
			}

			RawFileDescriptor[] files;
			try {
				files = ReadFiles ();
			} catch (Exception ex) {
				throw new RelicException (ex, "Failed to read file descriptors from SGA file.");
			}
			return new RawSGADescriptor(m_fileHeader, m_dataHeader, entryPoints, files, directories);
		}

		private SGAEntryPoint[] ConstructEntryPoints(Stream str, RawSGADescriptor rawSGA)
		{
			BinaryReader fileData = new BinaryReader(str);
			fileData.BaseStream.Position = m_lBaseOffset + m_fileHeader.DataOffset;

			// create the file systems, it is here that the files/directories are actually connected
			var entryPoints = rawSGA.EntryPoints;
			var fileSystems = new SGAEntryPoint[entryPoints.Length];
			for (int eid = 0; eid < entryPoints.Length; eid++)
			{
				var ep = entryPoints[eid];
				var fs = ConstructFileSystem(ep, rawSGA.Files, rawSGA.Directories);
				fileSystems[eid] = new SGAEntryPoint(ep.Name, ep.Alias, fileData, fs);
			}
			return fileSystems;
   
		}

		private static Dictionary<string, Dictionary<string, SGAEntryPoint.Entry>> ConstructFileSystem (RawEntryPoint ep,
																									   RawFileDescriptor[] files,
																									   RawDirectoryDescriptor[] dirs)
		{
			int numDirectories = (int)(ep.IndexOfLastDirectory - ep.IndexOfFirstDirectory);
			var fileSystem = new Dictionary<string, Dictionary<string, SGAEntryPoint.Entry>> (numDirectories);

			int limit = (int)ep.IndexOfLastDirectory;
			for (int di = (int)ep.IndexOfFirstDirectory; di < limit; di++) {
				fileSystem.Add (dirs [di].Path, ConstructDirectory (dirs [di], files, dirs));
			}
				

			return fileSystem;
		}

		private static Dictionary<string, SGAEntryPoint.Entry> ConstructDirectory (RawDirectoryDescriptor currentDir,
																				  RawFileDescriptor[] files,
																				  RawDirectoryDescriptor[] dirs)
		{
			int numDirectories = currentDir.IndexOfLastDirectory - currentDir.IndexOfFirstDirectory;
			int numFiles = currentDir.IndexOfLastFile - currentDir.IndexOfFirstFile;
			int numEntries = numDirectories + numFiles;
			var dict = new Dictionary<string, SGAEntryPoint.Entry> (numEntries);

			for (int di = currentDir.IndexOfFirstDirectory; di < currentDir.IndexOfLastDirectory; di++) {
				var dir = dirs [di];
				string dirName = dir.Path.SubstringAfterLast (PATH_SEPARATOR);
				dict.Add (dirName, new SGAEntryPoint.DirectoryEntry (dir.Path));
			}
			for (int fi = currentDir.IndexOfFirstFile; fi < currentDir.IndexOfLastFile; fi++) {
				var file = files [fi];
				var entry = new SGAEntryPoint.FileEntry(file.Name, file.DataOffset, file.CompressedSize,
														file.UncompressedSize, file.UnixTimeStamp, file.Flags, fi);
				dict.Add (file.Name, entry);
			}
			return dict;
		}

		/// <summary>
		/// Checks the hash of the data header and throws an exception if it does not match the stored hash.
		/// </summary>
		/// <exception cref="RelicException">Error while reading data header of SGA file for hashing.</exception>
		private void CheckDataHeaderHash ()
		{
			m_reader.BaseStream.Position = m_lDataHeaderOffset;
			byte[] dataHeaderBytes = null;
			try {
				dataHeaderBytes = m_reader.ReadBytes ((int)m_fileHeader.DataHeaderSize);
			} catch (Exception ex) {
				throw new RelicException (ex, "Error while reading data header of SGA file for hashing.");
			}

			// compare hashes
			MD5Hash dataHeaderHasher = new MD5Hash ();
			dataHeaderHasher.Update (SGAConstants.DATA_HEADER_SECURITY_KEY);
			dataHeaderHasher.Update (dataHeaderBytes);
			byte[] dataHeaderHash = dataHeaderHasher.FinalizeHash ();
			if (!dataHeaderHash.SequenceEqual (m_fileHeader.DataHeaderChecksum)) {
				var excep = new RelicException ("DataHeader Hash mismatch! Computed hash does not equal stored hash.");
				excep.Data ["stored hash"] = m_fileHeader.DataHeaderChecksum.ToHexString (false);
				excep.Data ["computed hash"] = dataHeaderHash.ToHexString (false);
				throw excep;
			}
		}

		private RawEntryPoint[] ReadEntryPoints ()
		{
			long entryPointOffset = m_lDataHeaderOffset + m_dataHeader.EntryPointSectionOffset;
			m_reader.BaseStream.Position = entryPointOffset;
			var entryPoints = new RawEntryPoint[m_dataHeader.EntryPointCount];
			for (int i = 0; i < m_dataHeader.EntryPointCount; i++)
			{
				long currentPos = m_reader.BaseStream.Position;
				entryPoints[i] = RawEntryPoint.Read(m_reader, m_fileHeader.Version);
				entryPoints[i].ThisDescriptorOffset = currentPos;
			}
				
			return entryPoints;
		}

		/// <summary>
		/// Reads the directory descriptors.
		/// </summary>
		/// <returns></returns>
		private RawDirectoryDescriptor[] ReadDirectories ()
		{
			long directoryOffset = m_lDataHeaderOffset + m_dataHeader.DirectorySectionOffset;
			m_reader.BaseStream.Position = directoryOffset;

			var dirs = new RawDirectoryDescriptor[m_dataHeader.DirectoryCount];
			const int descriptorSize = sizeof(ushort) * 4 + sizeof(uint);
			const int descriptorSizeV51 = sizeof(uint) * 5;
			int toRead;
			if (Uses32BitEntries(m_fileHeader.Version))
				toRead = (int)(descriptorSizeV51 * m_dataHeader.DirectoryCount);
			else
				toRead = (int)(descriptorSize * m_dataHeader.DirectoryCount);

			long baseOffset = m_reader.BaseStream.Position;
			// read all the data at once
			byte[] dirData = m_reader.ReadBytes (toRead);
			int idx = 0;
			for (int n = 0; n < m_dataHeader.DirectoryCount; n++) {
				uint nameOffset = BitConverter.ToUInt32 (dirData, idx);
				int dirFirst, dirLast, fileFirst, fileLast;

				long descriptorOffset = baseOffset + idx;
				if (Uses32BitEntries(m_fileHeader.Version))
				{
					dirFirst = (int)BitConverter.ToUInt32 (dirData, idx + 4);
					dirLast = (int)BitConverter.ToUInt32 (dirData, idx + 8);
					fileFirst = (int)BitConverter.ToUInt32 (dirData, idx + 12);
					fileLast = (int)BitConverter.ToUInt32 (dirData, idx + 16);
					idx += descriptorSizeV51;
				} else {
					dirFirst = BitConverter.ToUInt16 (dirData, idx + 4);
					dirLast = BitConverter.ToUInt16 (dirData, idx + 6);
					fileFirst = BitConverter.ToUInt16 (dirData, idx + 8);
					fileLast = BitConverter.ToUInt16 (dirData, idx + 10);
					idx += descriptorSize;
				}
				string name = m_stringsByOffset [nameOffset];
				dirs [n] = new RawDirectoryDescriptor (name, dirFirst, dirLast, fileFirst, fileLast);
				dirs[n].ThisPathOffset = nameOffset + m_lStringsOffset;
				dirs[n].ThisDescriptorOffset = descriptorOffset;
			}
			return dirs;
		}

		private RawFileDescriptor[] ReadFiles ()
		{
			long filesOffset = m_lDataHeaderOffset + m_dataHeader.FileSectionOffset;
			m_reader.BaseStream.Position = filesOffset;

			var files = new RawFileDescriptor[m_dataHeader.FileCount];
			const int descriptorSize = 5 * sizeof(uint) + sizeof(ushort);
			const int descriptorSizeV60 = descriptorSize + sizeof(uint); // additional 4 bytes of CRC32
            const int descriptorSizeV70 = descriptorSizeV60 + sizeof(uint); // additional 4 bytes, unknown, set to 0
            const int descriptorSizeV90 = 34;

		    int descSize = descriptorSize;
            if (m_fileHeader.Version == SGAVersion.Version6_0)
                descSize = descriptorSizeV60;
            else if (m_fileHeader.Version == SGAVersion.Version7_0)
                descSize = descriptorSizeV70;
            else if (m_fileHeader.Version == SGAVersion.Version9_0)
                descSize = descriptorSizeV90;

			long baseOffset = m_reader.BaseStream.Position;
			byte[] fileData = m_reader.ReadBytes ((int)(descSize * m_dataHeader.FileCount));
			int idx = 0;
			for (int n = 0; n < m_dataHeader.FileCount; n++) {

				uint nameOffset = BitConverter.ToUInt32 (fileData, idx);
                uint dataOffset, compressedSize, uncompressedSize, timeStamp;
                ushort flags;
                if (m_fileHeader.Version == SGAVersion.Version9_0)
                {
                    dataOffset = BitConverter.ToUInt32(fileData, idx + 8);
                    compressedSize = BitConverter.ToUInt32(fileData, idx + 16);
                    uncompressedSize = BitConverter.ToUInt32(fileData, idx + 20);
                    timeStamp = BitConverter.ToUInt32(fileData, idx + 24);
                    flags = BitConverter.ToUInt16(fileData, idx + 28);
                }
                else
                {
                    dataOffset = BitConverter.ToUInt32(fileData, idx + 4);
                    compressedSize = BitConverter.ToUInt32(fileData, idx + 8);
                    uncompressedSize = BitConverter.ToUInt32(fileData, idx + 12);
                    timeStamp = BitConverter.ToUInt32(fileData, idx + 16);
                    flags = BitConverter.ToUInt16(fileData, idx + 20);
                }

                /*
				uint crc32 = 0;
				if (m_fileHeader.Version == SGAVersion.Version6_0)
					crc32 = BitConverter.ToUInt32(fileData, idx + 24);
                 */

				string name = m_stringsByOffset [nameOffset];
				files [n] = new RawFileDescriptor (name, (uint)m_lBaseOffset + m_fileHeader.DataOffset + dataOffset, compressedSize, uncompressedSize, timeStamp, flags);
				files[n].ThisDescriptorOffest = baseOffset + idx;
				files[n].ThisNameOffset = nameOffset + m_lStringsOffset;
				//files[n].CRC32 = crc32;

				idx += descSize;
			}
			return files;
		}

		/// <summary>
		/// Gets the calculated string section size.
		/// Based on the assumption that the string section immediatly follows the DataHeader (which so far has always been the case).
		/// </summary>
		/// <returns></returns>
		private int GetStringSectionSize ()
		{
            if (m_dataHeader.StringBlobSize > 0)
                return (int) m_dataHeader.StringBlobSize;
            return (int)(m_fileHeader.DataHeaderSize - m_dataHeader.StringSectionOffset);
		}

		/// <summary>
		/// Reads the string section and decrypts it if needed.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="RelicException">Failed to acquire crypt context!</exception>
		private byte[] ReadStringSection ()
		{
			long stringsOffset = m_lBaseOffset + m_fileHeader.DataHeaderOffset + m_dataHeader.StringSectionOffset;
			m_reader.BaseStream.Position = stringsOffset;
			byte[] strings = null;
			// decrypt for archive versions v4.1 / v5.1, thanks to Corsix ( http://code.google.com/p/modstudio2/source/browse/trunk/Rainman2/archive.cpp )
            if (HasStringEncryption(m_fileHeader.Version))
            {
				#region decryption

				uint keyLength = m_reader.ReadUInt32 ();
				byte[] key = m_reader.ReadBytes ((int)keyLength);
				uint stringsLength = m_reader.ReadUInt32 ();
				strings = m_reader.ReadBytes ((int)stringsLength);

				IntPtr cryptProvider = IntPtr.Zero;
				IntPtr cryptKey = IntPtr.Zero;
				if (!SGACrypt.CryptAcquireContext (ref cryptProvider, null,
												  "Microsoft Enhanced Cryptographic Provider v1.0",
												  SGACrypt.PROV_RSA_FULL,
												  SGACrypt.CRYPT_VERIFYCONTEXT)) {
					SGACrypt.CryptReleaseContext (cryptProvider, 0);
					throw new RelicException ("Failed to acquire crypt context!");
				}
				if (!SGACrypt.CryptImportKey (cryptProvider, key, keyLength, IntPtr.Zero, 0, ref cryptKey)) {
					SGACrypt.CryptDestroyKey (cryptKey);
					SGACrypt.CryptReleaseContext (cryptProvider, 0);
					throw new RelicException ("Failed to import RSA key from archive!");
				}
				if (!SGACrypt.CryptDecrypt (cryptKey, IntPtr.Zero, 1, 0, strings, ref stringsLength)) {
					SGACrypt.CryptDestroyKey (cryptKey);
					SGACrypt.CryptReleaseContext (cryptProvider, 0);
					throw new RelicException ("Failed to decrypt strings from archive!");
				}
				SGACrypt.CryptDestroyKey (cryptKey);
				SGACrypt.CryptReleaseContext (cryptProvider, 0);

				#endregion
			} else
				strings = m_reader.ReadBytes (GetStringSectionSize ());
			return strings;
		}

		/// <summary>
		/// Returns a dictionary containing all zero-terminated strings from the raw data keyed by their index in the raw data.
		/// </summary>
		/// <param name="rawData"></param>
		/// <returns></returns>
		private Dictionary<uint, string> GetCStrings (byte[] rawData)
		{
			MemoryStream ms = new MemoryStream (rawData);
			BinaryReader br = new BinaryReader (ms);
			var dict = new Dictionary<uint, string> ();
			uint idx = 0;
            while (idx < rawData.Length) { 
				string str = br.ReadCString ();
				dict [idx] = str;
				idx += (uint)str.Length + 1;
			}
			return dict;
		}
	}
}