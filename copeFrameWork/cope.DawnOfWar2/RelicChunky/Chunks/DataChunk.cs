#region

using System.IO;

#endregion

namespace cope.DawnOfWar2.RelicChunky.Chunks
{
    public class DataChunk : RelicChunk
    {
        #region constructors

        internal DataChunk()
        {
        }

        public DataChunk(Stream str)
            : base(str)
        {
        }

        public DataChunk(BinaryReader br)
            : base(br)
        {
        }

        #endregion
    }
}