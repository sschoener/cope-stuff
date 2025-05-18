#region

using System.Collections.Generic;
using System.IO;
using cope.DawnOfWar2.RelicChunky.Chunks;

#endregion

namespace cope.DawnOfWar2.RelicChunky
{
    /// <summary>
    /// FileFormat for RelicChunkyFiles.
    /// </summary>
    public class RelicChunkyFile : UniFile
    {
        #region constructors

        public RelicChunkyFile()
        {
        }

        public RelicChunkyFile(UniFile file)
        {
            Stream = file.Stream;
            Tag = file.Tag;
            m_filePath = file.FilePath;
        }

        public RelicChunkyFile(string path)
            : base(path)
        {
        }

        /// <summary>
        /// Constructs a new RelicChunkyFile.
        /// </summary>
        public RelicChunkyFile(Stream str)
        {
            FileHeader = new RelicChunkyFileHeader(str);
            Chunks = new List<RelicChunk>();
            var rc = new RelicChunk(str);
            Chunks.Add(rc);
        }

        public RelicChunkyFile(RelicChunkyFileHeader header)
        {
            FileHeader = header;
        }

        /// <summary>
        /// Constructs a new RelicChunkyFile and initializes it.
        /// </summary>
        /// <param name="header">The header of the RelicChunkyFile.</param>
        /// <param name="chunk">The chunk hold by the RelicChunkyFile.</param>
        public RelicChunkyFile(RelicChunkyFileHeader header, RelicChunk chunk)
        {
            FileHeader = header;
            Chunks = new List<RelicChunk> {chunk};
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the FileHeader of this RelicChunkyFile.
        /// </summary>
        public RelicChunkyFileHeader FileHeader { get; set; }

        /// <summary>
        /// Gets or sets the Chunks of this RelicChunkyFile.
        /// </summary>
        public List<RelicChunk> Chunks { get; set; }

        #endregion

        #region IStreamExtBinaryCompatible<RelicChunkyFile> Member

        public void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(BinaryWriter bw)
        {
            long baseOffset = bw.BaseStream.Position;
            bw.BaseStream.Position += FileHeader.FileHeaderSize;
            foreach (RelicChunk rc in Chunks)
            {
                rc.WriteToStream(bw);
            }
            //_chunks.WriteToStream(bw);
            bw.BaseStream.Position = baseOffset;
            FileHeader.ChunkHeaderSize = (uint) Chunks[0].ChunkHeader.LengthWithoutName;
            FileHeader.WriteToStream(bw);
        }

        public RelicChunkyFile GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
            return this;
        }

        public RelicChunkyFile GetFromStream(BinaryReader br)
        {
            FileHeader = new RelicChunkyFileHeader(br);
            if (Chunks == null)
                Chunks = new List<RelicChunk>();
            while (true)
            {
                try
                {
                    RelicChunk chunk = null;
                    var hdr = new RelicChunkHeader(br, FileHeader.Version);
                    switch (hdr.Type)
                    {
                        case ChunkType.DATA:
                            chunk = new DataChunk();
                            break;
                        case ChunkType.FOLD:
                            chunk = new FoldChunk();
                            break;
                    }
                    chunk.ChunkHeader = hdr;
                    // the GetFromStream method skips the header if it's already present
                    chunk.GetFromStream(br);
                    if (hdr.Type == ChunkType.FOLD)
                        chunk.InterpretRawData();
                    Chunks.Add(chunk);
                }
                catch (EndOfStreamException)
                {
                    break;
                }
            }
            return this;
        }

        #endregion

        protected override void Write(Stream str)
        {
            WriteToStream(str);
        }

        protected override void Read(Stream str)
        {
            GetFromStream(str);
        }
    }
}