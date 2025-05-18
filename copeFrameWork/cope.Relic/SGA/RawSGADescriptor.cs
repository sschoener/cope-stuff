namespace cope.Relic.SGA
{
    internal class RawSGADescriptor
    {
        public RawSGADescriptor(SGAFileHeader fileHeader, SGADataHeader dataHeader, RawEntryPoint[] entryPoints, RawFileDescriptor[] files, RawDirectoryDescriptor[] directories)
        {
            FileHeader = fileHeader;
            DataHeader = dataHeader;
            EntryPoints = entryPoints;
            Files = files;
            Directories = directories;
        }

        public SGAFileHeader FileHeader { get; private set; }
        public SGADataHeader DataHeader { get; private set; }
        public RawEntryPoint[] EntryPoints { get; private set; }
        public RawFileDescriptor[] Files { get; private set; }
        public RawDirectoryDescriptor[] Directories { get; private set; }
    }
}