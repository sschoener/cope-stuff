namespace cope.FileSystem
{
    public static class FileSystemExt
    {
        public static FileWalker GetFileWalker(this IFileSystem fs, bool sortValues = false)
        {
            return new FileWalker(fs.GetRoot(), sortValues);
        }

        public static FileSystemWalker GetFileSystemWalker(this IFileSystem fs, bool sortValues = false)
        {
            return new FileSystemWalker(fs.GetRoot(), sortValues);
        }
    }
}