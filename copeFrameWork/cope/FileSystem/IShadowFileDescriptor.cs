namespace cope.FileSystem
{
    /// <summary>
    /// Interface for shadowed files of a virtual file system.
    /// A shadow-file has a read-only copy (a 'shadow') attached to it, which may be used instead of the original file.
    /// </summary>
    public interface IShadowFileDescriptor : IFileDescriptor
    {
        /// <summary>
        /// Returns the shadow of this file.
        /// </summary>
        /// <returns></returns>
        IFileDescriptor GetShadow();
    }
}