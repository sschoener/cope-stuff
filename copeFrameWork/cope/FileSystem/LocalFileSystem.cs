#region

using System.IO;
using cope.Extensions;

#endregion

namespace cope.FileSystem
{
	/// <summary>
	/// Class which implements the IFileSystem-interface and provides access to the local file system.
	/// </summary>
	public class LocalFileSystem : IFileSystem
	{
		public readonly static char PATH_SEPARATOR = '\\';

		/// <summary>
		/// Constructs a new LocalFileSystem starting at a given base path.
		/// </summary>
		/// <param name="basePath"></param>
		/// <exception cref="CopeException">The specified base path does not exist!</exception>
		public LocalFileSystem (string basePath)
		{
			if (!Directory.Exists (basePath))
				throw new CopeException ("The specified base path does not exist!");
			BasePath = basePath;
			DefaultFileAccess = FileAccess.Read;
			DefaultFileMode = FileMode.Open;
			DefaultFileShare = FileShare.Read;
		}

		/// <summary>
		/// Gets or sets the base path for this instance of LocaFileSystem.
		/// </summary>
		public string BasePath { get; protected set; }

		/// <summary>
		/// Gets or sets the default file access mode to be used when opening files without providing options.
		/// Default is Read.
		/// </summary>
		public FileAccess DefaultFileAccess { get; set; }

		/// <summary>
		/// Gets or sets the default file mode to be used when opening files without providing options.
		/// Default is Open.
		/// </summary>
		public FileMode DefaultFileMode { get; set; }

		/// <summary>
		/// Gets or sets the default file share mode to be used when opening files without providing options.
		/// Default is Read.
		/// </summary>
		public FileShare DefaultFileShare { get; set; }

        #region IStaticFileSystem Members

		/// <summary>
		/// Returns whether a file exists at the specified path.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public virtual bool DoesFileExist (string path)
		{
			path = path.Trim (PATH_SEPARATOR);
			return File.Exists (Path.Combine (BasePath, path));
		}

		/// <summary>
		/// Returns whether a directory exists at the specified path.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public virtual bool DoesDirectoryExist (string path)
		{
			path = path.Trim (PATH_SEPARATOR);
			return Directory.Exists (Path.Combine (BasePath, path));
		}

		/// <summary>
		/// Returns the entry at the specified path or null if there is no entry.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public virtual IFileSystemEntry GetElement (string path)
		{
			path = path.Trim (PATH_SEPARATOR);
			IFileSystemEntry entry = GetDirectory (path);
			return entry ?? GetFile (path);
		}

		/// <summary>
		/// Returns the file at the specified path or null if there is no file.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public virtual IFileDescriptor GetFile (string path)
		{
			path = path.Trim (PATH_SEPARATOR);
			if (!DoesFileExist (path))
				return null;
			return new LocalFileDescriptor (path, this);
		}

		/// <summary>
		/// Returns the directory at the specified path or null if there is no directory.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public virtual IDirectoryDescriptor GetDirectory (string path)
		{
			path = path.Trim (PATH_SEPARATOR);
			if (!DoesDirectoryExist (path))
				return null;
			return new LocalDirectoryDescriptor (this, path);
		}

		/// <summary>
		/// Returns the root directory.
		/// </summary>
		/// <returns></returns>
		public IDirectoryDescriptor GetRoot ()
		{
			return GetDirectory (string.Empty);
		}

        #endregion

		/// <summary>
		/// Returns a directory given an absolute path.
		/// </summary>
		/// <param name="abspath"></param>
		/// <returns></returns>
		internal IDirectoryDescriptor GetDirectoryAbs (string abspath)
		{
			string newPath = abspath.SubstringAfterLast (BasePath);
			if (newPath.Length == abspath.Length)
				return null;
			return GetDirectory (newPath);
		}

		/// <summary>
		/// Returns a file given an absolute path.
		/// </summary>
		/// <param name="abspath"></param>
		/// <returns></returns>
		internal IFileDescriptor GetFileAbs (string abspath)
		{
			string newPath = abspath.SubstringAfterLast (BasePath);
			if (newPath.Length == abspath.Length)
				return null;
			return GetFile (newPath);
		}
	}
}