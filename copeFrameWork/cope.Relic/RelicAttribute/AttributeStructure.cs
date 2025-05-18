namespace cope.Relic.RelicAttribute
{
    /// <summary>
    /// Represents the attribute structure held by RBF files.
    /// </summary>
    public class AttributeStructure
    {
        #region fields

        private readonly AttributeValue m_root;

        #endregion

        public AttributeStructure(AttributeValue root)
        {
            m_root = root;
        }

        #region methods

        #endregion

        #region Properties

        ///<summary>
        /// Gets the root value of this instance.
        ///</summary>
        public AttributeValue Root
        {
            get { return m_root; }
        }

        #endregion
    }
}