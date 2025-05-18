using System.IO;

namespace cope.Relic.SGA.Patching
{
    public static class SGAPatchReader
    {
        /// <exception cref="RelicException">SGA patch files are expected to start with SGAPATCH, but this one does not!</exception>
        public static SGAPatch Read(Stream str)
        {
            var br = new BinaryReader(str);
            var identifier = br.ReadBytes(8);
            if (identifier.ToString(true) != "SGAPATCH")
                throw new RelicException("SGA patch files are expected to start with SGAPATCH, but this one does not!");

            int nameLength = br.ReadInt32();
            var patchName = br.ReadBytes(nameLength).ToString(true);

            int sgaNameLength = br.ReadInt32();
            var sgaName = br.ReadBytes(sgaNameLength).ToString(true);

            int numFilePatches = br.ReadInt32();
            var filePatches = new SGAFilePatch[numFilePatches];
            for (int i = 0; i < numFilePatches; i++)
                filePatches[i] = ReadFilePatch(br);
            return new SGAPatch(patchName, sgaName, filePatches);
        }

        private static SGAFilePatch ReadFilePatch(BinaryReader br)
        {
            var fileNameLength = br.ReadInt32();
            var fileName = br.ReadBytes(fileNameLength).ToString(true);
            var compressed = br.ReadBoolean();
            var uncompressedSize = br.ReadUInt32();
            var patchSize = br.ReadUInt32();
            var patch = br.ReadBytes((int)patchSize);
            return new SGAFilePatch(fileName, patch, uncompressedSize, compressed);
        }
    }
}