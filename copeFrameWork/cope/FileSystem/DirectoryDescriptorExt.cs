namespace cope.FileSystem
{
    public static class DirectoryDescriptorExt
    {
        public static FileWalker GetFileWalker(this IDirectoryDescriptor dd, bool sortValues = false)
        {
            return new FileWalker(dd, sortValues);
        }

        public static FileSystemWalker GetFileSystemWalker(this IDirectoryDescriptor dd, bool sortValues = false)
        {
            return new FileSystemWalker(dd, sortValues);
        }
    }
}