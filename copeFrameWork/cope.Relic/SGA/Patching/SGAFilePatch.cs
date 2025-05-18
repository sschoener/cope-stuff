namespace cope.Relic.SGA.Patching
{
    public class SGAFilePatch
    {
        public SGAFilePatch(string fileName, byte[] replaceWith, uint uncompressedSize, bool compressed)
        {
            FileName = fileName;
            ReplaceWith = new byte[replaceWith.Length];
            replaceWith.CopyTo(ReplaceWith, 0);
            UncompressedSize = uncompressedSize;
            Compressed = compressed;
        }

        /// <summary>
        /// Gets the file name to be patched in the SGA.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the data which should replace the file specified via FileName.
        /// </summary>
        public byte[] ReplaceWith { get; private set; }

        /// <summary>
        /// Gets the uncompressed size of the data.
        /// </summary>
        public uint UncompressedSize { get; private set; }

        /// <summary>
        /// Gets whether this file is compressed.
        /// </summary>
        public bool Compressed { get; private set; }

        public Either<string, bool> IsApplicable(SGAEntryPoint sga)
        {
            if (!sga.DoesFileExist(FileName))
                return new EitherLeft<string, bool>(FileName + " does not exist!");
            bool rightSize = sga.GetFile(FileName).GetSize() == UncompressedSize;
            if (!rightSize)
            {
                return new EitherLeft<string, bool>("The size of the file to patch do not match!");
            }
            if (Compressed && ReplaceWith.Length != sga.GetSizeInArchive(FileName))
            {
                return new EitherLeft<string, bool>(
                        "The size of the compressed file to patch and the compressed patch do not match!");
            }
            return new EitherRight<string, bool>(true);
        }
    }
}