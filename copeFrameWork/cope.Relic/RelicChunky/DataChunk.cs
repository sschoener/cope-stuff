namespace cope.Relic.RelicChunky
{
    public sealed class DataChunk : Chunk
    {
        private byte[] m_data;

        public DataChunk(string name, string signature, byte[] data)
            : base(name, signature)
        {
            SetData(data);
        }

        public DataChunk(ChunkHeader header, byte[] data) : base(header)
        {
            SetData(data);
        }

        public DataChunk(byte[] data)
        {
            SetData(data);
        }

        /// <summary>
        /// Returns a copy of the data held by this instance of DataChunk.
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            return m_data;
        }

        /// <summary>
        /// Sets this DataChunk's data to a copy of the input array.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(byte[] data)
        {
            m_data = new byte[data.Length];
            data.CopyTo(m_data, 0);
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return "DATA" + Signature;
            return "DATA" + Signature + " - " + Name;
        }
    }
}