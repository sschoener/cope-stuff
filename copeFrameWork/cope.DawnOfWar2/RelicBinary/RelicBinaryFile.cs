#region

using System;
using cope.DawnOfWar2.RelicAttribute;

#endregion

namespace cope.DawnOfWar2.RelicBinary
{
    public class RelicBinaryFile : UniFile
    {
        #region fields

        private AttributeStructure m_attributeStructure;
        private IRBFKeyProvider m_keyProvider;

        #endregion

        #region ctors

        public RelicBinaryFile(string filepath) : base(filepath)
        {
        }

        public RelicBinaryFile(byte[] buffer)
            : base(buffer)
        {
        }

        public RelicBinaryFile(AttributeStructure attributeStructure)
        {
            m_attributeStructure = attributeStructure;
        }

        public RelicBinaryFile(UniFile file)
            : base(file)
        {
        }

        #endregion

        #region methods

        /// <exception cref="CopeDoW2Exception">Trying to read RBF-file using a key provider but no key provider has been specified!</exception>
        protected override void Read(System.IO.Stream stream)
        {
            try
            {
                if (UseKeyProvider)
                {
                    if (m_keyProvider == null)
                        throw new CopeDoW2Exception(
                            "Trying to read RBF-file using a key provider but no key provider has been specified!");
                    m_attributeStructure = RBFReader.Read(stream, m_keyProvider);
                }
                else
                    m_attributeStructure = RBFReader.Read(stream);
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex, "Error while trying to read RBF data! See inner exception.");
            }
        }

        /// <exception cref="CopeDoW2Exception">Error while trying to write RBF data! See inner exception.</exception>
        protected override void Write(System.IO.Stream stream)
        {
            try
            {
                if (m_attributeStructure == null)
                    throw new CopeDoW2Exception("Trying to write RBF data to Stream but there's no data available!");
                if (UseKeyProvider)
                {
                    if (m_keyProvider == null)
                        throw new CopeDoW2Exception(
                            "Trying to write RBF-file using a key provider but no key provider has been specified!");
                    RBFWriter.Write(stream, m_attributeStructure, m_keyProvider);
                }
                else
                    RBFWriter.Write(stream, m_attributeStructure);
            }
            catch (Exception ex)
            {
                throw new CopeDoW2Exception(ex, "Error while trying to write RBF data! See inner exception.");
            }
        }

        #endregion

        #region properties

        public AttributeStructure AttributeStructure
        {
            get { return m_attributeStructure; }
        }

        public IRBFKeyProvider KeyProvider
        {
            get { return m_keyProvider; }
            set
            {
                if (value != null)
                    UseKeyProvider = true;
                m_keyProvider = value;
            }
        }

        public bool UseKeyProvider { get; set; }

        #endregion
    }
}