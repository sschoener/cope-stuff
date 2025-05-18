using System.IO;

namespace cope.FileSystem
{
	public static class FileSystemEntryExt
	{
		/// <summary>
		/// Returns the name of the given IFileSystemEntry.
		/// </summary>
		/// <param name="entry"></param>
		/// <returns></returns>
		public static string GetName (this IFileSystemEntry entry)
		{
			return Path.GetFileName (entry.GetPath ());
		}
	}
}