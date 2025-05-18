using System;
using System.IO;

namespace cope.Relic
{
    /// <summary>
    /// Helper class to write FLB files.
    /// </summary>
    public static class FLBFileWriter
    {
        /// <summary>
        /// Writes the specified FieldNameStorage to a stream using the FLB format.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="flb"></param>
        /// <exception cref="RelicException">Failed to save FieldNameStorage object to stream in FLB format. See inner exception and data for more information.</exception>
        public static void Write(Stream stream, FieldNameStorage flb)
        {
            try
            {
                var bw = new BinaryWriter(stream);
                MemoryStream offsets = new MemoryStream();
                BinaryWriter offsetWriter = new BinaryWriter(offsets);
                MemoryStream keys = new MemoryStream();
                BinaryWriter keyWriter = new BinaryWriter(keys);

                // first: number of keys in this FLB file
                uint numKeys = (uint) flb.NumKeys;
                bw.Write(numKeys);

                // offset array and key array are written simultanously
                uint offset = numKeys * sizeof (uint);
                foreach (string s in flb)
                {
                    offsetWriter.Write(offset);
                    keyWriter.Write(s.ToByteArray(true));
                    keyWriter.Write(false); // zero terminated string
                    offset += (uint) s.Length + 0x1;
                }
                offsets.Flush();
                keys.Flush();
                offsets.WriteTo(stream);
                keys.WriteTo(stream);
                offsets.Close();
                keys.Close();
                stream.Flush();
            }
            catch (Exception ex)
            {
                var newException = new RelicException(ex,
                                                      "Failed to save FieldNameStorage object to stream in FLB format." +
                                                      " See inner exception and data for more information.");
                newException.Data["FLB"] = flb;
                newException.Data["Stream"] = stream;
                throw newException;
            }
        }
    }
}