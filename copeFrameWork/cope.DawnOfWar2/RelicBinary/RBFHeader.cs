#region

using System.IO;

#endregion

namespace cope.DawnOfWar2.RelicBinary
{
    public class RBFHeader
    {
        #region fields

        private static readonly byte[] s_stdSignature = "RBF V0.1".ToByteArray(true);

        #endregion

        #region ctors

        /// <summary>
        /// Returns a totally uninitalized RelicBinaryFileHeader.
        /// </summary>
        public RBFHeader()
        {
        }

        /// <summary>
        /// Returns a new instance of RelicBinaryFileHeader which got it's data from the specified stream.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isInRb2Mode"></param>
        public RBFHeader(Stream str, bool isInRb2Mode = false)
        {
            GetFromStream(str, isInRb2Mode);
        }

        /// <summary>
        /// Returns a new instance of RelicBinaryFileHeader which got it's data from the specified BinaryReader.
        /// </summary>
        /// <param name="br"></param>
        /// <param name="isInRb2Mode"></param>
        public RBFHeader(BinaryReader br, bool isInRb2Mode = false)
        {
            GetFromStream(br, isInRb2Mode);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the byte length of a RelicBinaryFile Header.
        /// </summary>
        public uint Length
        {
            get
            {
                int size = s_stdSignature.Length;
                size += 10 * sizeof (uint);
                return (uint) size;
            }
        }

        /// <summary>
        /// Gets or sets the offset of the TableArray of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint TableArrayOffset { get; set; }

        /// <summary>
        /// Gets or sets the entry-count of the TableArray of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint TableArrayCount { get; set; }

        /// <summary>
        /// Gets or sets the offset of the KeyArray of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint KeyArrayOffset { get; set; }

        /// <summary>
        /// Gets or sets the entry-count of the KeyArray of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint KeyArrayCount { get; set; }

        /// <summary>
        /// Gets or sets the offset of the DataIndexArray of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint DataIndexArrayOffset { get; set; }

        /// <summary>
        /// Gets or sets the entry-count of the DataIndexArray of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint DataIndexArrayCount { get; set; }

        /// <summary>
        /// Gets or sets the offset of the DataArray of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint DataArrayOffset { get; set; }

        /// <summary>
        /// Gets or sets the entry-count of the DataArray of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint DataArrayCount { get; set; }

        /// <summary>
        /// Gets or sets the offset of the StringSection of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint StringSectionOffset { get; set; }

        /// <summary>
        /// Gets or sets the byte-length of the StringSection of this instance of RelicBinaryFileHeader.
        /// </summary>
        public uint StringSectionLength { get; set; }

        #endregion

        #region Read/Write

        public void WriteToStream(Stream str, bool isInRetributionMode = false)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw, isInRetributionMode);
        }

        public void WriteToStream(BinaryWriter bw, bool isInRetributionMode = false)
        {
            bw.Write(s_stdSignature);
            bw.Write(TableArrayOffset);
            bw.Write(TableArrayCount);
            if (!isInRetributionMode)
            {
                bw.Write(KeyArrayOffset);
                bw.Write(KeyArrayCount);
            }
            bw.Write(DataIndexArrayOffset);
            bw.Write(DataIndexArrayCount);
            bw.Write(DataArrayOffset);
            bw.Write(DataArrayCount);
            bw.Write(StringSectionOffset);
            bw.Write(StringSectionLength);
        }

        public void GetFromStream(Stream str, bool isInRetributionMode = false)
        {
            var br = new BinaryReader(str);
            GetFromStream(br, isInRetributionMode);
        }


        /// <exception cref="CopeDoW2Exception"><c>CopeDoW2Exception</c>.</exception>
        public void GetFromStream(BinaryReader br, bool isInRetributionMode = false)
        {
            byte[] signature = br.ReadBytes(8);
            if (signature.Equals(s_stdSignature))
            {
                throw new CopeDoW2Exception("Unknwon file signature! This is no RelicBinaryFile: " +
                                                   signature.ToString(true));
            }
            TableArrayOffset = br.ReadUInt32();
            TableArrayCount = br.ReadUInt32();
            if (!isInRetributionMode)
            {
                KeyArrayOffset = br.ReadUInt32();
                KeyArrayCount = br.ReadUInt32();
            }
            DataIndexArrayOffset = br.ReadUInt32();
            DataIndexArrayCount = br.ReadUInt32();
            DataArrayOffset = br.ReadUInt32();
            DataArrayCount = br.ReadUInt32();
            StringSectionOffset = br.ReadUInt32();
            StringSectionLength = br.ReadUInt32();
        }

        #endregion
    }
}