using System;

namespace cope.FileSystem
{
    /// <summary>
    /// Represents the different possible actions for an entry of a virtual file system.
    /// Not to be confused with file permissions.
    /// </summary>
    [Flags]
    public enum FileSystemPossibility
    {
        Read = 0x01,
        Write = 0x02,
        Delete = 0x4
    }
}