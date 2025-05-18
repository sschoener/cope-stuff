namespace cope.Relic.SGA
{
    /// <summary>
    /// Contains information needed to construct a SGA file.
    /// </summary>
    public class SGAWriterSettings
    {
        public SGAWriterSettings(string archiveName, string entryPointName, string entryPointType, bool compress = false)
        {
            ArchiveName = archiveName;
            EntryPointName = entryPointName;
            EntryPointType = entryPointType;
            UseCompression = compress;
        }

        public string ArchiveName { get; private set; }
        public string EntryPointName { get; private set; }

        /// <summary>
        /// Either Attrib or Data -- but there may be other types...
        /// </summary>
        public string EntryPointType { get; private set; }

        public bool UseCompression { get; private set; }
    }
}