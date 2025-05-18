namespace cope.Relic.SGA
{
    internal struct RawDirectoryDescriptor
    {
        public RawDirectoryDescriptor(string path, int dirFirst, int dirLast, int fileFirst, int fileLast)
        {
            Path = path;
            IndexOfFirstDirectory = dirFirst;
            IndexOfLastDirectory = dirLast;
            IndexOfFirstFile = fileFirst;
            IndexOfLastFile = fileLast;
            ThisDescriptorOffset = 0L;
            ThisPathOffset = 0L;
        }

        public string Path;

        /// <summary>
        /// Gets or sets the index of the first directory contained by this directory.
        /// </summary>
        public int IndexOfFirstDirectory;

        /// <summary>
        /// Gets or sets the index of the first directory not contained by this directory anymore.
        /// </summary>
        public int IndexOfLastDirectory;

        /// <summary>
        /// Gets or sets the index of the first file contained by this directory.
        /// </summary>
        public int IndexOfFirstFile;

        /// <summary>
        /// Gets or sets the index of the first file not contained by this directory anymore.
        /// </summary>
        public int IndexOfLastFile;

        /// <summary>
        /// Gets or sets the offset of this descriptor within the archive.
        /// </summary>
        public long ThisDescriptorOffset;

        /// <summary>
        /// Gets or sets the offset of the path of this folder within the archive. 
        /// </summary>
        public long ThisPathOffset;
    }
}