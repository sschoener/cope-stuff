using System;

namespace cope.FileSystem
{
    /// <summary>
    /// Provides options for the FileSystem enumerators.
    /// </summary>
    [Flags]
    public enum EnumerationOptions
    {
        EnumerateFiles = 0x1,
        EnumerateDirectories = 0x2
    }
}