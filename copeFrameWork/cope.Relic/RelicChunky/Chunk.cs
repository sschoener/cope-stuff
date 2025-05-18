namespace cope.Relic.RelicChunky
{
    /// <summary>
    /// Class representing a Chunk held by a Relic-ChunkyFile
    /// </summary>
    public abstract class Chunk
    {
        #region constructors

        protected Chunk(ChunkHeader header)
        {
            OriginalHeader = header;
            Name = header.Name;
            Signature = header.Signature;
        }

        protected Chunk(string name, string signature)
        {
            Name = name;
            Signature = signature;
        }

        protected Chunk(){}

        #endregion

        public FolderChunk Parent { get; internal set; }

        public string Name { get; protected set; }

        /// <summary>
        /// Four byte long signature of this chunk.
        /// </summary>
        public string Signature { get; protected set; }

        /// <summary>
        /// Gets the path of this chunk in the chunk-tree.
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            string nameString;
            if (this is FolderChunk)
                nameString = "FOLD" + Signature;
            else
                nameString = "DATA" + Signature;
            if (Parent == null)
            {
                return nameString;
            }
            return Parent.GetPath() + '\\' + nameString;
        }

        /// <summary>
        /// The original header may contain important data, so we better keep it - in case it was provided.
        /// </summary>
        public ChunkHeader OriginalHeader { get; private set; }
    }
}