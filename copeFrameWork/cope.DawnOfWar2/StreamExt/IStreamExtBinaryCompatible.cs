namespace cope.IO.StreamExt
{
    ///<summary>
    ///</summary>
    public interface IStreamExtBinaryCompatible
    {
        /// <summary>
        /// Write the Data of this instance into a binary Stream.
        /// </summary>
        /// <param name="str">The Stream to write into.</param>
        void WriteToStream(System.IO.Stream str);

        /// <summary>
        /// Write the Data of this instance into a binary Stream using a specified BinaryWriter.
        /// </summary>
        /// <param name="bw">The BinaryWriter that is to be used.</param>
        void WriteToStream(System.IO.BinaryWriter bw);

        /// <summary>
        /// Get the Data for this instance from a binary Stream.
        /// </summary>
        /// <param name="str">The Stream to get the Data from.</param>
        /// <returns>Returns the object read.</returns>
        void GetFromStream(System.IO.Stream str);

        /// <summary>
        /// Get the Data for this instance from a binary Stream using a specified BinaryReader.
        /// </summary>
        /// <param name="bw">The BinaryWriter that is to be used.</param>
        void GetFromStream(System.IO.BinaryReader bw);
    }

    /// <summary>
    /// Interface for writing/getting data from a textstream. Uses UTF8 encoding.
    /// </summary>
    public interface IStreamExtTextCompatible
    {
        /// <summary>
        /// Write the Data of this instance into a text Stream.
        /// </summary>
        /// <param name="str">The Stream to write into.</param>
        void WriteToTextStream(System.IO.Stream str);

        /// <summary>
        /// Write the Data of this instance into a text Stream using a specified TextWriter.
        /// </summary>
        /// <param name="tw">TextWriter that is to be used.</param>
        void WriteToTextStream(System.IO.TextWriter tw);

        /// <summary>
        /// Get the Data for this instance from a text Stream.
        /// </summary>
        /// <param name="str">The Stream to get the Data from.</param>
        /// <returns>Returns the object read.</returns>
        void GetFromTextStream(System.IO.Stream str);

        /// <summary>
        /// Get the Data for this instance from a text Stream using a specified TextReader.
        /// </summary>
        /// <param name="tr">TextReader that is to be used.</param>
        void GetFromTextStream(System.IO.TextReader tr);
    }
}