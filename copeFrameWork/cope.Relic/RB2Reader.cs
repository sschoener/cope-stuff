using System;
using System.IO;

namespace cope.Relic
{
    /// <summary>
    /// Helper class to read RB2 files.
    /// </summary>
    public static class RB2Reader
    {
        const uint SIGNATURE = 0x12345678;// yes, that's the real signature.

        /// <summary>
        /// Reads a RB2 file from a stream and constructs an RB2FileExtractor object which can be used to get single RBF files.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="RelicException">File is not a RB2 file! Invalid signature found.</exception>
        public static RB2FileExtractor Read(Stream stream)
        {
            try
            {
                var br = new BinaryReader(stream);
                uint signature = br.ReadUInt32();
                if (signature != SIGNATURE)
                    throw new RelicException("File is not a RB2 file! Invalid signature found.");
                uint numFiles = br.ReadUInt32();
                string[] fileNames = new string[numFiles];

                // read strings
                for (int fileNamesRead = 0; fileNamesRead < numFiles; fileNamesRead++)
                {
                    var fileNameLength = (int)br.ReadUInt32();
                    fileNames[fileNamesRead] = new string(br.ReadChars(fileNameLength));
                }

                byte[][] files = new byte[numFiles][];
                for (int i = 0; i < numFiles; i++)
                {
                    int fileSize = (int)br.ReadUInt32();
                    files[i] = br.ReadBytes(fileSize);
                }
                return new RB2FileExtractor(files, fileNames);
            }
            catch (Exception e)
            {
                var excp = new RelicException(e, "Error while trying to read RB2 file! See inner exception.");
                excp.Data["Stream"] = stream;
                throw excp;
            }
        }
    }
}