using System.IO;

namespace cope.FileSystem
{
    /// <summary>
    /// FileDescriptor used by the ShadowedLocalFileSystem class.
    /// </summary>
    public class ShadowedLocalFileDescriptor : LocalFileDescriptor, IShadowFileDescriptor
    {
        private readonly IFileDescriptor m_shadow;

        internal ShadowedLocalFileDescriptor(string path, LocalFileSystem fileSystem, IFileDescriptor shadow = null) : base(path, fileSystem)
        {
            m_shadow = shadow;
        }

        /// <summary>
        /// Returns the shadow of this instance.
        /// </summary>
        /// <returns></returns>
        public IFileDescriptor GetShadow()
        {
            return m_shadow;
        }

        /// <summary>
        /// Returns whether this shadowed file has a real file.
        /// </summary>
        /// <returns></returns>
        public bool HasRealFile()
        {
            return File.Exists(FullPath);
        }
    }
}