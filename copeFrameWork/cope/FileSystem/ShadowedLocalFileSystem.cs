using System;
using cope.Extensions;

namespace cope.FileSystem
{
    /// <summary>
    /// Class which implements the IFileSystem-interface and provides access to the local file system.
    /// This class provides support for shadowing.
    /// </summary>
    public class ShadowedLocalFileSystem : LocalFileSystem
    {
        private readonly ShadowedLocalDirectoryDescriptor m_shadowRoot;

        /// <summary>
        /// Constructs a new LocalFileSystem starting at a given base path.
        /// </summary>
        /// <param name="basePath"></param>
        /// <exception cref="CopeException">The specified base path does not exist!</exception>
        public ShadowedLocalFileSystem(string basePath) : base(basePath)
        {
            m_shadowRoot = new ShadowedLocalDirectoryDescriptor(this, string.Empty);
        }

        /// <summary>
        /// Returns the shadowed directory at the given path or null if there is no such directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IDirectoryDescriptor GetDirectoryShadowed(string path)
        {
            path = path.Trim(PATH_SEPARATOR);
            var parts = path.Split(StringSplitOptions.RemoveEmptyEntries, PATH_SEPARATOR);
            IDirectoryDescriptor currentDir = m_shadowRoot;
            foreach (string s in parts)
            {
                currentDir = currentDir.GetDirectory(s);
                if (currentDir == null)
                    return null;
            }
            if (currentDir != null)
                return currentDir;
            return null;
        }

        /// <summary>
        /// Returns whether there exists a directory at the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public override bool DoesDirectoryExist(string path)
        {
            return GetDirectoryShadowed(path) != null || base.DoesDirectoryExist(path);
        }

        /// <summary>
        /// Returns the directory at the specified path or null if there is no such directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public override IDirectoryDescriptor GetDirectory(string path)
        {
            var dir = GetDirectoryShadowed(path);
            return dir ?? base.GetDirectory(path);
        }

        /// <summary>
        /// Returns the shadowed file at the specified path or null if there is no such file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IFileDescriptor GetFileShadowed(string path)
        {
            path = path.Trim(PATH_SEPARATOR);
            string fileName, dirPath;
            if (!path.SplitAtLast(PATH_SEPARATOR, out dirPath, out fileName))
                return m_shadowRoot.GetFile(path);
            var currentDir = GetDirectoryShadowed(dirPath);
            if (currentDir != null)
                return currentDir.GetFile(fileName);
            return null;
        }

        /// <summary>
        /// Returns whether a file exists at the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public override bool DoesFileExist(string path)
        {
            return GetFileShadowed(path) != null || base.DoesFileExist(path);
        }

        /// <summary>
        /// Returns the file at the specified path or null if there is no such file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public override IFileDescriptor GetFile(string path)
        {
            return GetFileShadowed(path) ?? base.GetFile(path);
        }

        /// <summary>
        /// Adds the specified shadow file at the given path.
        /// Returns true if adding a shadow succeeded, otherwise false. It will not succeed, if there already is a shadow for the specified path.
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="shadowFile"></param>
        public bool AddShadow(string relativePath, IFileDescriptor shadowFile)
        {
            string dirPath, fileName;
            ShadowedLocalDirectoryDescriptor dir;
            if (!relativePath.SplitAtLast(PATH_SEPARATOR, out dirPath, out fileName))
                dir = m_shadowRoot;
            else
                dir = GetOrCreateShadowDir(dirPath);
            if (!dir.HasShadow(fileName))
            {
                dir.AddShadowFile(fileName, shadowFile);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to get the shadow directory at the specified path or creates it if it does not exist.
        /// </summary>
        /// <param name="relativePath"></param>
        private ShadowedLocalDirectoryDescriptor GetOrCreateShadowDir(string relativePath)
        {
            relativePath = relativePath.Trim(PATH_SEPARATOR);
            var parts = relativePath.Split(StringSplitOptions.RemoveEmptyEntries, PATH_SEPARATOR);
            ShadowedLocalDirectoryDescriptor currentDir = m_shadowRoot;
            foreach (string s in parts)
            {
                var nextDir = currentDir.GetShadowDir(s) ?? currentDir.CreateShadowDir(s);
                currentDir = nextDir;
            }
            return currentDir;
        }
    }
}