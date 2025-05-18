using System.IO;

namespace cope.Relic.SGA.Patching
{
    public static class SGAPatchWriter
    {
        public static void Write(Stream str, SGAPatch sgaPatch)
        {
            var bw = new BinaryWriter(str);
            bw.Write("SGAPATCH".ToByteArray(true));
            bw.Write(sgaPatch.Name.Length);
            bw.Write(sgaPatch.Name.ToByteArray(true));
            bw.Write(sgaPatch.SGAFileName.Length);
            bw.Write(sgaPatch.SGAFileName.ToByteArray(true));
            bw.Write(sgaPatch.FilePatches.Length);
            foreach (var patch in sgaPatch.FilePatches)
                WriteFilePatch(bw, patch);
            bw.Flush();
        }

        private static void WriteFilePatch(BinaryWriter bw, SGAFilePatch patch)
        {
            bw.Write(patch.FileName.Length);
            bw.Write(patch.FileName.ToByteArray(true));
            bw.Write(patch.Compressed);
            bw.Write(patch.UncompressedSize);
            bw.Write(patch.ReplaceWith.Length);
            bw.Write(patch.ReplaceWith);
        }
    }
}