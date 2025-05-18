using System.Collections.Generic;
using System.IO;
using System.Linq;
using cope.Extensions;

namespace cope.Relic.Module
{
    /// <summary>
    /// A section of Module file that consists of a folder/archive list
    /// </summary>
    public sealed class ModuleSectionFileList : ModuleSection
    {
        #region fields

        private List<string> m_archives = new List<string>();
        private List<int> m_commentPositions = new List<int>();
        private List<string> m_comments = new List<string>();
        private List<string> m_folders = new List<string>();

        #endregion fields

        #region ctors

        /// <summary>
        /// Creates a new Module file section with a List scheme
        /// </summary>
        /// <param name="sectionName">The name of the new section</param>
        public ModuleSectionFileList(string sectionName)
        {
            m_sectionName = sectionName;
        }

        public ModuleSectionFileList(string sectionName, TextReader tr)
        {
            m_sectionName = sectionName;
            GetFromTextStream(tr);
        }

        public ModuleSectionFileList()
        {
        }

        public ModuleSectionFileList(List<string> lines)
        {
            m_sectionName = lines[0];
            m_sectionName = m_sectionName.Remove(m_sectionName.Length - 1, 1).Remove(0, 1);
            string line;
            for (int i = 1; i < lines.Count; i++)
            {
                line = lines[i];
                if (line == string.Empty || line == "\n")
                    continue;
                if (line.StartsWith("archive."))
                    AddArchive(line.SubstringAfterLast('=').SubstringAfterLast(CharType.Whitespace), false);
                else if (line.StartsWith("folder."))
                    AddFolder(line.SubstringAfterLast('=').SubstringAfterLast(CharType.Whitespace), false);
                else if (line.StartsWith(";"))
                    AddComment(line.SubstringAfterFirst(';'), i);
            }
        }

        #endregion ctors

        #region methods

        /// <summary>
        /// Adds an archive to the section
        /// </summary>
        /// <param name="archivePath">Path to the archive relative to the DoW2 directory, e.g. GameAssets\\Archives\\test.sga</param>
        /// <param name="top"></param>
        public void AddArchive(string archivePath, bool top)
        {
            if (!ArchiveExists(archivePath))
                if (top)
                    m_archives.Insert(0, archivePath);
                else
                    m_archives.Add(archivePath);
        }

        /// <summary>
        /// Adds a folder to the section
        /// </summary>
        /// <param name="folderPath">Path to the folder relative to the DoW2 directory, e.g. GameAssets\\Data\\Test</param>
        /// <param name="top"></param>
        public void AddFolder(string folderPath, bool top)
        {
            if (!FolderExists(folderPath))
                if (top)
                    m_folders.Insert(0, folderPath);
                else
                    m_folders.Add(folderPath);
        }

        /// <summary>
        /// Adds a comment to the section
        /// </summary>
        /// <param name="comment">String representing the comment</param>
        /// <param name="position">Line in which to add the comment relative to the beginning of the section ([sectionname] = line 0) thus position can NOT be 0</param>
        public void AddComment(string comment, int position)
        {
            m_comments.Add(comment);
            m_commentPositions.Add(position);
        }

        /// <summary>
        /// Removes an archive with a specific path from the section
        /// </summary>
        /// <param name="archivePath">Path of the archive to be removed, e.g. GameAssets\\Archives\\test.sga</param>
        public void RemoveArchive(string archivePath)
        {
            m_archives.Remove(archivePath);
        }

        public void RemoveArchiveAtIndex(int index)
        {
            if (index < m_archives.Count)
                m_archives.RemoveAt(index);
        }

        /// <summary>
        /// Removes a folder with a specific path from the section
        /// </summary>
        /// <param name="folderPath">Path of the folder to be removed, e.g. GameAssets\\Data\\Test</param>
        public void RemoveFolder(string folderPath)
        {
            m_folders.Remove(folderPath);
        }

        public void RemoveFolderAtIndex(int index)
        {
            if (index < m_folders.Count)
                m_folders.RemoveAt(index);
        }

        public void RemoveAllFolders()
        {
            m_folders.Clear();
        }

        /// <summary>
        /// Returns if an archive with that name already exists in that section of the file
        /// </summary>
        /// <param name="archiveName">Name of the archive to check for existence</param>
        /// <returns></returns>
        private bool ArchiveExists(string archiveName)
        {
            return m_archives.Any(archiveName.Equals);
        }

        /// <summary>
        /// Returns if a folder with that name already exists in that section of the file
        /// </summary>
        /// <param name="folderName">Name of the folder to check for existence</param>
        /// <returns></returns>
        private bool FolderExists(string folderName)
        {
            return m_folders.Any(folderName.Equals);
        }

        /// <summary>
        /// Checks if there's a comment for the specified line
        /// </summary>
        /// <param name="line">The index of the line to check</param>
        /// <param name="commentExists">OUT parameter - true if there is a comment for the specified line</param>
        /// <param name="commentIndex">OUT parameter - index of the comment in comments-list</param>
        private void CommentInLine(int line, out bool commentExists, out int commentIndex)
        {
            commentExists = false;
            commentIndex = 0;

            foreach (int position in m_commentPositions)
            {
                if (line == position)
                {
                    commentExists = true;
                    break;
                }
                commentIndex++;
            }
        }

        /// <summary>
        /// Checks whether the input value already exists.
        /// </summary>
        /// <param name="type">Either 'folder' or 'archive'.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns></returns>
        public override bool Exists(string type, string value)
        {
            if (type.ToLower().Equals("folder"))
                return ArchiveExists(value);
            if (type.ToLower().Equals("archive"))
                return FolderExists(value);

            return false;
        }

        /// <summary>
        /// Add an archive or folder to the section
        /// </summary>
        /// <param name="key">Set to 'folder' (without '') for adding a folder (or to 'archive' (without '') to add an archive)</param>
        /// <param name="value">Path to the folder/archive relative to the DoW2 directory, e.g. GameAssets\\Data\\Test(.sga if it's an archive)</param>
        public override void Add(string key, string value)
        {
            if (key.ToLower().Equals("folder"))
                AddFolder(value, false);
            else if (key.ToLower().Equals("archive"))
                AddArchive(value, false);
        }

        /// <summary>
        /// Returns the last line in use by this section relative to the first line of the section. This is useful when adding a comment to the end of the section.
        /// </summary>
        /// <returns></returns>
        public int GetLastLine()
        {
            // one line for the section-name
            int lastLine = 1 + m_archives.Count + m_folders.Count + m_comments.Count;

            return lastLine;
        }

        public override void WriteToTextStream(TextWriter tw)
        {
            // 2 strings more than folders + archives + comments: an empty line and the section-name
            var sectionString = new string[m_folders.Count + m_archives.Count + m_comments.Count + 2];

            sectionString[0] = '[' + m_sectionName + ']';

            int lineCounter = 1;
            // add the folders, j is just an indexing var to store where we are in the sectionString array
            for (int idx = 1; idx < m_folders.Count; lineCounter++)
            {
                // is this line a comment?
                bool comment;
                int commentIndex;

                CommentInLine(lineCounter, out comment, out commentIndex);

                if (comment)
                {
                    sectionString[lineCounter] = ';' + m_comments[commentIndex];
                    continue;
                }

                idx++;
                sectionString[lineCounter] = string.Format("folder.{0:D2}" + m_folders[idx], idx);
            }

            // add the archives
            for (int idx = 0; idx < m_archives.Count; lineCounter++)
            {
                // is this line a comment?
                bool comment;
                int commentIndex;

                CommentInLine(lineCounter, out comment, out commentIndex);

                if (comment)
                {
                    sectionString[lineCounter] = ';' + m_comments[commentIndex];
                    continue;
                }

                idx++;
                sectionString[lineCounter] = string.Format("archive.{0:D2}" + m_archives[idx], idx);
            }
            foreach (string s in sectionString)
                tw.WriteLine(s);
        }

        public override void GetFromTextStream(TextReader tr)
        {
            int i = 0;
            while (!tr.IsAtEnd())
            {
                string line = tr.ReadLine();
                if (line == "\n" || string.IsNullOrEmpty(line))
                    break;
                if (line.StartsWith("archive."))
                    AddArchive(line.SubstringAfterLast('=').SubstringAfterLast(CharType.Whitespace), false);
                else if (line.StartsWith("folder."))
                    AddFolder(line.SubstringAfterLast('=').SubstringAfterLast(CharType.Whitespace), false);
                else if (line.StartsWith(";"))
                    AddComment(line.SubstringAfterFirst(';'), i);
                i++;
            }
        }

        #endregion methods

        #region properties

        public int FolderCount
        {
            get { return m_folders.Count; }
        }

        public int ArchiveCount
        {
            get { return m_archives.Count; }
        }

        public int CommentCount
        {
            get { return m_comments.Count; }
        }

        public IEnumerable<string> Archives
        {
            get { return m_archives; }
        }

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public string GetArchiveByIndex(int index)
        {
            if (index >= m_archives.Count)
                throw new RelicException("Section " + m_sectionName + " does not have any archive at index " + index);
            return m_archives[index];
        }

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public string GetFolderByIndex(int index)
        {
            if (index >= m_folders.Count)
                throw new RelicException("Section " + m_sectionName + " does not have any folder at index " + index);
            return m_folders[index];
        }

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public string GetCommentAtIndex(int index)
        {
            if (index >= m_comments.Count)
                throw new RelicException("Section " + m_sectionName + " does not have any comment at index " + index);
            return m_comments[index];
        }

        #endregion properties

        public override ModuleSection GClone()
        {
            var msfl = new ModuleSectionFileList(m_sectionName)
                           {
                               m_archives = new List<string>(m_archives),
                               m_commentPositions = new List<int>(m_commentPositions),
                               m_comments = new List<string>(m_comments),
                               m_folders = new List<string>(m_folders)
                           };
            return msfl;
        }
    }
}