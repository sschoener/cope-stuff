namespace cope
{
    /// <summary>
    /// Provides options for the usage of <c>KeyValueSyntaxWriter</c>.
    /// </summary>
    public sealed class KeyValueSyntaxWriterOptions
    {
        private int m_iIndentWidth = 2;
        private bool m_bUsePipes = true;
        private bool m_bSortEntries = true;

        /// <summary>
        /// Sets the width of the indentation. 0 is no indentation, values less than zero are truncated to 0.
        /// 2 by default.
        /// </summary>
        public int IndentWidth
        {
            get { return m_iIndentWidth; }
            set
            {
                if (value < 0)
                    value = 0;
                m_iIndentWidth = value;
            }
        }

        /// <summary>
        /// Set to false to disable the usage of pipes. True by default.
        /// </summary>
        public bool UsePipes
        {
            get { return m_bUsePipes; }
            set { m_bUsePipes = value; }
        }

        /// <summary>
        /// Set to true to use tabstops instead of spaces for spacing. False by default.
        /// </summary>
        public bool UseTabIndentation
        {
            get;
            set;
        }

        /// <summary>
        /// Set to true to sort table-entries by key. True by default.
        /// </summary>
        public bool SortEntries
        {
            get { return m_bSortEntries; }
            set { m_bSortEntries = value; }
        }
    }
}