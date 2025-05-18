#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace cope.FileSystem
{
	/// <summary>
	/// DirectoryDescriptor used by the LocalFileSystem class.
	/// </summary>
	public class LocalDirectoryDescriptor : IDirectoryDescriptor
	{
		protected readonly LocalFileSystem m_fileSystem;
		protected readonly string m_sPath;

		public LocalDirectoryDescriptor (LocalFileSystem fileSystem, string path)
		{
			m_fileSystem = fileSystem;
			m_sPath = path;
		}

		/// <summary>
		/// Returns the full, absolute path of this LocalDirectoryDescriptor.
		/// </summary>
		public string FullPath {
			get { return Path.Combine (m_fileSystem.BasePath, m_sPath); }
		}

        #region IDirectoryDescriptor Members

		/// <summary>
		/// Compares this entry's path to another entry's path.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo (IFileSystemEntry other)
		{
			return GetPath ().CompareTo (other.GetPath ());
		}

		/// <summary>
		/// Returns the relative path of this directory.
		/// </summary>
		/// <returns></returns>
		public string GetPath ()
		{
			return m_sPath;
		}

		public FileSystemPossibility GetPossibleActions ()
		{
			return FileSystemPossibility.Read | FileSystemPossibility.Delete | FileSystemPossibility.Write;
		}

		/// <summary>
		/// Creates a file with the specified name in this directory.
		/// May throw exceptions.
		/// </summary>
		/// <param name="name"></param>
		public void CreateFile (string name)
		{
			File.Create (Path.Combine (FullPath, name)).Close ();
		}

		/// <summary>
		/// Creates a directory with the specified name in this directory.
		/// May throw exceptions.
		/// </summary>
		/// <param name="name"></param>
		public void CreateDirectory (string name)
		{
			Directory.CreateDirectory (Path.Combine (FullPath, name));
		}

		/// <summary>
		/// Deletes the entry with the specified name. Returns whether there was an entry with that name prior to deletion.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool Delete (string name)
		{
			if (HasDirectory (name)) {
				Directory.Delete (Path.Combine (FullPath, name), true);
				return true;
			}
			if (HasFile (name)) {
				File.Delete (Path.Combine (FullPath, name));
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns whether there is a file with the specified name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual bool HasFile (string name)
		{
			return File.Exists (Path.Combine (FullPath, name));
		}

		/// <summary>
		/// Returns whether there is a directory with the specified name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual bool HasDirectory (string name)
		{
			return Directory.Exists (Path.Combine (FullPath, name));
		}

		/// <summary>
		/// Returns the file with the specified name or null if there is no such file.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual IFileDescriptor GetFile (string name)
		{
			return m_fileSystem.GetFile (Path.Combine (m_sPath, name));
		}

		/// <summary>
		/// Returns the directory with the specified name or null if there is no such directory.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual IDirectoryDescriptor GetDirectory (string name)
		{
			return m_fileSystem.GetDirectory (Path.Combine (m_sPath, name));
		}

		public virtual IEnumerable<IDirectoryDescriptor> GetDirectories ()
		{
			var dirs = Directory.EnumerateDirectories (FullPath);
			return dirs.Select (m_fileSystem.GetDirectoryAbs);
		}

		public virtual IEnumerable<IFileDescriptor> GetFiles ()
		{
			var files = Directory.EnumerateFiles (FullPath);
			return files.Select (m_fileSystem.GetFileAbs);
		}

		public virtual IEnumerable<IFileSystemEntry> GetAll ()
		{
			var files = Directory.EnumerateFiles (FullPath);
			var fEntries = files.Select (m_fileSystem.GetElement);
			var dirs = Directory.EnumerateDirectories (FullPath);
			return fEntries.Concat (dirs.Select (m_fileSystem.GetElement));
		}

        #endregion
	}
}