#region

using System;
using System.IO;
using cope.DawnOfWar2.RelicBinary;
using cope.Extensions;

#endregion

namespace cope.DawnOfWar2
{
    // used to extract RB2 files
    public class RB2FileExtractor : UniFile
    {
        private const uint SIGNATURE = 305419896;

        private readonly FieldNameFile m_fieldNames;
        private byte[][] m_files;
        private int[] m_iOffsets;
        private long m_lBaseOffset;
        private string[] m_sFileNames;

        public RB2FileExtractor(string path, FieldNameFile flb)
            : base(path)
        {
            m_fieldNames = flb;
        }

        ///<summary>
        /// Gets the number of files in this RB2File.
        ///</summary>
        public int NumFiles
        {
            get { return m_sFileNames.Length; }
        }

        ///<summary>
        /// Get or sets whether to convert the RBF files in this RB2 file to pre-Retribution format.
        ///</summary>
        public bool PerformConversion { get; set; }

        /// <exception cref="Exception">File is not a RB2 file! Invalid signature found.</exception>
        protected override void Read(Stream stream)
        {
            var br = new BinaryReader(stream);

            try
            {
                uint signature = br.ReadUInt32();
                if (signature != SIGNATURE)
                    throw new Exception("File is not a RB2 file! Invalid signature found.");
                uint numFiles = br.ReadUInt32();
                m_sFileNames = new string[numFiles];

                // read strings
                int fileNamesRead = 0;
                for (; fileNamesRead < numFiles; fileNamesRead++)
                {
                    var fileNameLength = (int) br.ReadUInt32();
                    m_sFileNames[fileNamesRead] = new string(br.ReadChars(fileNameLength));
                }

                // read offsets
                m_lBaseOffset = stream.Position;
                m_iOffsets = new int[numFiles];
                m_files = new byte[numFiles][];
                for (int i = 0; i < numFiles; i++)
                {
                    m_iOffsets[i] = (int) (stream.Position - m_lBaseOffset);
                    int fileSize = (int) br.ReadUInt32();
                    m_files[i] = br.ReadBytes(fileSize);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while trying to read " + FilePath + " as RB2 file!", e);
            }
        }

        public void ExtractAll(string outputPath, Action<int> progressCallback)
        {
            if (!outputPath.EndsWith('\\'))
                outputPath += '\\';
            int numFiles = m_sFileNames.Length;

            for (int i = 0; i < numFiles; i++)
            {
                byte[] buffer = m_files[i];

                string filePath = outputPath + m_sFileNames[i] + ".rbf";
                if (!PerformConversion)
                {
                    string path = filePath.SubstringBeforeLast('\\');
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    FileStream extract = File.Create(filePath);
                    extract.Write(buffer, 0, buffer.Length);
                    extract.Flush();
                    extract.Close();
                }
                else
                {
                    var rbf = new RelicBinaryFile(buffer) {KeyProvider = m_fieldNames, FilePath = filePath};
                    rbf.ReadData();
                    rbf.WriteDataTo(rbf.FilePath);
                }
                if (progressCallback != null)
                    progressCallback(i);
            }
        }
    }
}