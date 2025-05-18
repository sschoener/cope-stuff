using System;
using System.IO;
using cope.Extensions;

namespace cope.Relic
{
    /// <summary>
    /// Helper class to read FLB files and create FieldNameStorage objects from them.
    /// </summary>
    public static class FLBFileReader
    {
        /// <summary>
        /// Tries to read a FieldNameStorage object encoded in FLB format from the given stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="RelicException">Failed to read FieldNameStorage object from an FLB file. See inner exception for more information.</exception>
        public static FieldNameStorage Read(Stream stream)
        {
            try
            {
                var br = new BinaryReader(stream);

                var numKeys = (int) br.ReadUInt32();

                long baseOffset = stream.Position;
                int[] offsets = new int[numKeys];
                for (int i = 0; i < numKeys; i++)
                {
                    offsets[i] = (int) br.ReadUInt32();
                }

                string[] keys = new string[numKeys];
                for (int i = 0; i < numKeys; i++)
                {
                    stream.Position = baseOffset + offsets[i];
                    string key = br.ReadCString();
                    keys[i] = key;
                }
                return new FieldNameStorage(keys);
            }
            catch (Exception ex)
            {
                var newException = new RelicException(ex,
                                                      "Failed to read FieldNameStorage object from an FLB file." +
                                                      " See inner exception and data for more information.");
                newException.Data["Stream"] = stream;
                throw newException;
            }
        }
    }
}