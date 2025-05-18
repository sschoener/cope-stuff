#region

using System.IO;

#endregion

namespace cope.DawnOfWar2.BAF
{
    public class BAFHeader
    {
        #region fields

        private static readonly byte[] s_stdSignature = "BAF ".ToByteArray(true);

        #endregion

        #region ctors

        public BAFHeader()
        {
        }

        public BAFHeader(BinaryReader reader)
        {
            GetFromStream(reader);
        }

        #endregion

        #region properties

        /// <summary>
        /// Returns the byte length of a RelicBinaryFile Header.
        /// </summary>
        public uint Length
        {
            get
            {
                const int size = 10 * sizeof (uint) + 5;
                return size;
            }
        }

        public uint TableSectionOffset { get; set; }
        public uint TableCount { get; set; }
        public uint DataSectionOffset { get; set; }
        public uint DataCount { get; set; }
        public uint StringIndexCount { get; set; }
        public uint StringIndexSectionOffset { get; set; }
        public uint StringSectionOffset { get; set; }
        public byte[] CRC32Hash { get; set; }

        #endregion

        #region methods

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public void GetFromStream(BinaryReader br)
        {
            byte[] signature = br.ReadBytes(4);
            if (signature.Equals(s_stdSignature) || br.ReadInt32() != 0x64)
            {
                throw new CopeDoW2Exception("Unknwon file signature! This is no ATTR_PC file: " + signature.ToString(true));
            }
            CRC32Hash = br.ReadBytes(4);
            br.BaseStream.Position += 5;
            TableSectionOffset = br.ReadUInt32();
            TableCount = br.ReadUInt32();
            DataSectionOffset = br.ReadUInt32();
            DataCount = br.ReadUInt32();
            StringIndexCount = br.ReadUInt32();
            StringIndexSectionOffset = br.ReadUInt32();
            StringSectionOffset = br.ReadUInt32();
        }

        public void WriteToStream(BinaryWriter bw)
        {
            bw.Write(s_stdSignature);
            bw.Write(0x64);
            bw.Write(CRC32Hash);
            bw.BaseStream.Position += 5;
            bw.Write(TableSectionOffset);
            bw.Write(TableCount);
            bw.Write(DataSectionOffset);
            bw.Write(DataCount);
            bw.Write(StringIndexCount);
            bw.Write(StringIndexSectionOffset);
            bw.Write(StringSectionOffset);
        }

        #endregion
    }
}