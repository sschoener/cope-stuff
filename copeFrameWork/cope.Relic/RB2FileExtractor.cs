#region

using System;
using System.IO;
using cope.Extensions;
using cope.Relic.RelicBinary;

#endregion

namespace cope.Relic
{
    /// <summary>
    /// Helper class to extract (and optionally convert) RBF files from RB2 files.
    /// </summary>
    public class RB2FileExtractor
    {
        private readonly byte[][] m_files;
        private readonly string[] m_sFileNames;

        internal RB2FileExtractor(byte[][] files, string[] filenames)
        {
            m_files = files;
            m_sFileNames = filenames;
        }

        ///<summary>
        /// Gets the number of files in this RB2File.
        ///</summary>
        public int NumFiles
        {
            get { return m_sFileNames.Length; }
        }

        /// <summary>
        /// Gets or sets the FieldNameStorage object to use when converting the files to pre-Retribution format.
        /// Pre-Retribution format does not use a FLB file to hold the name of the fields.
        /// </summary>
        public FieldNameStorage FieldNames
        {
            get;
            set;
        }

        ///<summary>
        /// Get or sets whether to convert the RBF files in this RB2 file to pre-Retribution format upon extraction.
        /// If this option is set, you _need_ to provide a FieldNameStorage via FieldNames.
        ///</summary>
        public bool PerformConversion { get; set; }

        /// <summary>
        /// Extracts all files to a given path. If PerformConversion is set to true and a FieldNameStorage file has been provided, it
        /// will also perform the conversion to pre-Retribution format.
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="progressCallback"></param>
        /// <exception cref="RelicException">Failed to extract files from RB2 file. See inner exception.</exception>
        public void ExtractAll(string outputPath, Action<int> progressCallback)
        {
            try
            {
                if (!outputPath.EndsWith('\\'))
                    outputPath += '\\';
                int numFiles = m_sFileNames.Length;

                for (int i = 0; i < numFiles; i++)
                {
                    byte[] buffer = m_files[i];
                    string filePath = outputPath + m_sFileNames[i] + ".rbf";

                    FileStream extract = File.Create(filePath);
                    if (!PerformConversion)
                    {
                        string path = filePath.SubstringBeforeLast('\\');
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        extract.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        var attribStructure = RBFReader.Read(new MemoryStream(buffer), FieldNames);
                        RBFWriter.Write(extract, attribStructure, FieldNames);
                    }
                    if (progressCallback != null)
                        progressCallback(i);
                    extract.Flush();
                    extract.Close();
                }
            }
            catch (Exception e)
            {
                var excp = new RelicException(e, "Failed to extract files from RB2 file. See inner exception.");
                excp.Data["OutputPath"] = outputPath;
                excp.Data["Callback"] = progressCallback;
                excp.Data["RB2"] = this;
                throw excp;
            }
        }

        public override string  ToString()
        {
            string fields = "FieldNames: " + ((FieldNames == null) ? "none" : FieldNames.ToString());
            return "NumFiles: " + NumFiles + ", Convert: " + PerformConversion + ", " + fields;
        }
    }
}