namespace cope.Relic.SGA
{
    internal struct RawFileDescriptor
    {
        public RawFileDescriptor(string name, uint offset, uint compressedSize, uint uncompressedSize, uint unixTimeStamp, ushort flags)
        {
            Name = name;
            DataOffset = offset;
            CompressedSize = compressedSize;
            UncompressedSize = uncompressedSize;
            UnixTimeStamp = unixTimeStamp;
            Flags = flags;
            ThisDescriptorOffest = 0L;
            ThisNameOffset = 0L;
            CRC32 = 0;
        }

        public string Name;

        /// <summary>
        /// Gets or sets the offset of the data of the file relative to the offset of the DataSection (see SGA FileHeader).
        /// </summary>
        public uint DataOffset;

        /// <summary>
        /// Gets or sets the compressed size (in bytes) of the data of the file.
        /// </summary>
        public uint CompressedSize;

        /// <summary>
        /// Gets or sets the uncompressed size (in bytes) of the data of the file.
        /// </summary>
        public uint UncompressedSize;

        /// <summary>
        /// Gets or sets the timestamp for the file.
        /// </summary>
        public uint UnixTimeStamp;

        /// <summary>
        /// Gets or sets the flags for this file descriptor. 0x0200 seems to be a flag indicating compression.
        /// </summary>
        public ushort Flags;

        /// <summary>
        /// Gets or sets the CRC32 for this file descriptor.
        /// </summary>
        public uint CRC32;

        /// <summary>
        /// Gets or sets the offset of this descriptor within the archive.
        /// </summary>
        public long ThisDescriptorOffest;

        /// <summary>
        /// Gets or sets the offset of the name of this file within the archive. 
        /// </summary>
        public long ThisNameOffset;
    }
}