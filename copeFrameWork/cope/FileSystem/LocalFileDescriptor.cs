#region

using System;
using System.IO;

#endregion

namespace cope.FileSystem
{
	/// <summary>
	/// FileDescriptor used by the LocalFileSystem class.
	/// </summary>
	public class LocalFileDescriptor : IFileDescriptor
	{
		protected readonly LocalFileSystem m_fileSystem;
		protected readonly string m_sPath;

		internal LocalFileDescriptor (string path, LocalFileSystem fileSystem)
		{
			m_sPath = path;
			m_fileSystem = fileSystem;
		}

		/// <summary>
		/// Returns the full, absolute path of this LocalFileDescriptor
		/// </summary>
		public string FullPath {
			get { return Path.Combine (m_fileSystem.BasePath, m_sPath); }
		}

		#region IFileDescriptor Members

		/// <summary>
		/// Compars this instance's path to another instance's path.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo (IFileSystemEntry other)
		{
			return GetPath ().CompareTo (other.GetPath ());
		}

		/// <summary>
		/// Returns the relative path of this file.
		/// </summary>
		/// <returns></returns>
		public string GetPath ()
		{
			return m_sPath;
		}

		public virtual FileSystemPossibility GetPossibleActions ()
		{
			return FileSystemPossibility.Read | FileSystemPossibility.Delete | FileSystemPossibility.Write;
		}

		/// <summary>
		/// Opens this file using the default file modes from the LocalFileSystem this descriptor belongs to.
		/// </summary>
		/// <returns></returns>
		public virtual Stream Open ()
		{
			return File.Open (FullPath, m_fileSystem.DefaultFileMode, m_fileSystem.DefaultFileAccess,
							 m_fileSystem.DefaultFileShare);
		}

		/// <summary>
		/// Opens this file using the specified FileOpenOptions.
		/// </summary>
		/// <param name="opt"></param>
		/// <returns></returns>
		public virtual Stream Open (FileOpenOptions opt)
		{
			return File.Open (FullPath, opt.Mode, opt.Access, opt.Share);
		}

		/// <summary>
		/// Returns whether this file supports file opening options. LocalFileDescriptors usually do.
		/// </summary>
		/// <returns></returns>
		public virtual bool SupportsFileOpenOptions ()
		{
			return true;
		}

		/// <summary>
		/// Retuns the size of this file in bytes.
		/// </summary>
		/// <returns></returns>
		public virtual long GetSize ()
		{
			return new FileInfo (FullPath).Length;
		}

		/// <summary>
		/// Returns the time this file has last been modified.
		/// </summary>
		/// <returns></returns>
		public virtual DateTime GetLastModified ()
		{
			return File.GetLastWriteTimeUtc (FullPath);
		}

		#endregion
	}

}