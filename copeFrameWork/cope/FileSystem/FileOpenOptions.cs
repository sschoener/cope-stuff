#region

using System.IO;

#endregion

namespace cope.FileSystem
{
    /// <summary>
    /// Provides different Options for opening files
    /// Not all implementations need to support these.
    /// </summary>
    public struct FileOpenOptions
    {
        public FileAccess Access;
        public FileMode Mode;
        public FileShare Share;
    }
}