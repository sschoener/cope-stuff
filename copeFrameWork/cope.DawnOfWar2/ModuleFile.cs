#region

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cope.Extensions;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2
{
    /// <summary>
    /// Class for operating with .module files
    /// </summary>
    public class ModuleFile : UniFile, IStreamExtTextCompatible, IEnumerable<ModuleFile.ModuleSection>,
                              IGenericClonable<ModuleFile>
    {
        #region fields

        private readonly Dictionary<string, ModuleSection> m_sections = new Dictionary<string, ModuleSection>();

        #endregion fields

        #region ctors

        public ModuleFile()
        {
        }

        public ModuleFile(UniFile file)
        {
            Stream = file.Stream;
            Tag = file.Tag;
            m_filePath = file.FilePath;
        }

        /// <summary>
        /// Load a module file and get it's data from the stream.
        /// </summary>
        /// <param name="str"></param>
        public ModuleFile(Stream str)
        {
            GetFromTextStream(str);
        }

        public ModuleFile(TextReader tr)
        {
            GetFromTextStream(tr);
        }

        public ModuleFile(string path)
            : base(path)
        {
            GetFromTextStream(Stream);
        }

        #endregion ctors

        #region methods

        /// <summary>
        /// Creates a new section in the module file
        /// </summary>
        /// <param name="name">Name of the section, e.g. [global]</param>
        /// <param name="type">Set this to 0 to create a Key-Value section or to 1 to create a FileList section</param>
        /// <exception cref="CopeDoW2Exception">Module file {0} already contains a section called {1}!</exception>
        public void CreateNewSection(string name, byte type)
        {
            if (m_sections.ContainsKey(name))
                throw new CopeDoW2Exception("Module file {0} already contains a section called {1}!", FileName, name);
            if (type == 0)
                m_sections.Add(name, new ModuleSectionKeyValue(name));
            else if (type == 1)
                m_sections.Add(name, new ModuleSectionFileList(name));
        }

        /// <summary>
        /// Adds a new entry to a section
        /// </summary>
        /// <param name="sectionName">The name of the section to add the new entry to</param>
        /// <param name="key">The Key of the value; for File List sections set this either to folder or to archive to add a folder/archive</param>
        /// <param name="value">The value / name of the folder / name of the archive to add</param>
        /// <exception cref="CopeDoW2Exception">Module file {0} does not contain a section {1}!</exception>
        public void AddToSection(string sectionName, string key, string value)
        {
            if (m_sections.ContainsKey(sectionName))
                m_sections[sectionName].Add(key, value);
            throw new CopeDoW2Exception("Module file {0} does not contain a section {1}!", FileName, sectionName);
        }

        public void AddSection(ModuleSection ms)
        {
            m_sections.Add(ms.SectionName, ms);
        }

        /// <summary>
        /// Removes the specified section from the module file.
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public bool RemoveSection(string sectionName)
        {
            return m_sections.Remove(sectionName);
        }

        /// <summary>
        /// Returns whether a section with the specified does exist.
        /// </summary>
        /// <param name="sectionName">Name of the section to search for.</param>
        /// <returns></returns>
        public bool HasSection(string sectionName)
        {
            return m_sections.ContainsKey(sectionName);
        }

        protected override void Write(Stream str)
        {
            WriteToTextStream(str);
        }

        protected override void Read(Stream str)
        {
            GetFromTextStream(str);
        }

        #endregion methods

        #region properties

        /// <exception cref="CopeDoW2Exception">Module file {0} does not contain a section {1}!</exception>
        public ModuleSection this[string sectionName]
        {
            get
            {
                if (m_sections.ContainsKey(sectionName))
                    return m_sections[sectionName];
                throw new CopeDoW2Exception("Module file {0} does not contain a section {1}!", FileName, sectionName);
            }
            set { m_sections[sectionName] = value; }
        }

        public int SectionCount
        {
            get { return m_sections.Count; }
        }

        #endregion properties

        #region SectionType enum

        ///<summary>
        ///</summary>
        public enum SectionType
        {
            ST_KeyValue,
            ST_FileList
        }

        #endregion

        #region Nested type: ModuleSection

        public abstract class ModuleSection : IStreamExtTextCompatible, IGenericClonable<ModuleSection>
        {
            #region fields

            protected string m_sectionName;
            protected SectionType m_type;

            #endregion fields

            #region methods

            public abstract bool Exists(string key, string value);

            public abstract void Add(string key, string value);

            public override string ToString()
            {
                return m_sectionName;
            }

            #endregion methods

            #region properties

            public SectionType SectionType
            {
                get { return m_type; }
            }

            public string SectionName
            {
                get { return m_sectionName; }
            }

            #endregion properties

            #region IStreamExtTextCompatible<ModuleSection> Member

            public abstract void WriteToTextStream(Stream str);

            public abstract void WriteToTextStream(TextWriter tw);

            public abstract void GetFromTextStream(Stream str);

            public abstract void GetFromTextStream(TextReader tr);

            #endregion IStreamExtTextCompatible<ModuleSection> Member

            #region IGenericClonable<ModuleSection> Member

            public abstract ModuleSection GClone();

            #endregion IGenericClonable<ModuleSection> Member
        }

        #endregion

        #region Nested type: ModuleSectionFileList

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
                m_type = SectionType.ST_FileList;
            }

            public ModuleSectionFileList(string sectionName, TextReader tr)
            {
                m_sectionName = sectionName;
                m_type = SectionType.ST_FileList;
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

            public override void WriteToTextStream(Stream str)
            {
                TextWriter tw = new StreamWriter(str);
                WriteToTextStream(tw);
            }

            public override void WriteToTextStream(TextWriter tw)
            {
                // 2 strings more than folders + archives + comments: an empty line and the section-name
                var sectionString = new string[m_folders.Count + m_archives.Count + m_comments.Count + 2];
                var tmpStrB = new StringBuilder();

                sectionString[0] = '[' + m_sectionName + ']';

                int j = 1;

                // add the folders, j is just an indexing var to store where we are in the sectionString array
                for (int i = 0; i < m_folders.Count; j++)
                {
                    // is this line a comment?
                    bool comment;
                    int commentIndex;

                    CommentInLine(j, out comment, out commentIndex);

                    if (comment)
                    {
                        sectionString[j] = tmpStrB.SetString(";{0}", m_comments[commentIndex]).ToString();
                        continue;
                    }

                    // if not - get the right folder to add!
                    if (i < 9)
                    {
                        sectionString[j] = tmpStrB.SetString("folder.0{0} = {1}", (i + 1), m_folders[i]).ToString();
                        i++;
                    }
                    else
                    {
                        sectionString[j] = tmpStrB.SetString("folder.{0} = {1}", (i + 1), m_folders[i]).ToString();
                        i++;
                    }
                }

                // add the archives
                for (int i = 0; i < m_archives.Count; j++)
                {
                    // is this line a comment?
                    bool comment;
                    int commentIndex;

                    CommentInLine(j, out comment, out commentIndex);

                    if (comment)
                    {
                        sectionString[j] = tmpStrB.SetString(";{0}", m_comments[commentIndex]).ToString();
                        continue;
                    }

                    if (i < 9)
                    {
                        sectionString[j] = tmpStrB.SetString("archive.0{0} = {1}", (i + 1), m_archives[i]).ToString();
                        i++;
                    }
                    else
                    {
                        sectionString[j] = tmpStrB.SetString("archive.{0} = {1}", (i + 1), m_archives[i]).ToString();
                        i++;
                    }
                }
                foreach (string s in sectionString)
                    tw.WriteLine(s);
            }

            public override void GetFromTextStream(Stream str)
            {
                TextReader tr = new StreamReader(str);
                GetFromTextStream(tr);
            }

            public override void GetFromTextStream(TextReader tr)
            {
                int i = 0;
                string line;
                while (true)
                {
                    try
                    {
                        // add some point it'll reach the end and throw an exception
                        line = tr.ReadLine();
                    }
                    catch
                    {
                        break;
                    }

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

            /// <exception cref="CopeDoW2Exception">Section {0} does not have any archive at index {1}!</exception>
            public string GetArchiveByIndex(int index)
            {
                if (index >= m_archives.Count)
                    throw new CopeDoW2Exception("Section {0} does not have any archive at index {1}!", m_sectionName,
                                                index);
                return m_archives[index];
            }

            /// <exception cref="CopeDoW2Exception">Section {0} does not have any archive at index {1}!</exception>
            public string GetFolderByIndex(int index)
            {
                if (index >= m_folders.Count)
                    throw new CopeDoW2Exception("Section {0} does not have any archive at index {1}!", m_sectionName,
                                                index);
                return m_folders[index];
            }

            /// <exception cref="CopeDoW2Exception">Section {0} does not have any archive at index {1}!</exception>
            public string GetCommentAtIndex(int index)
            {
                if (index >= m_comments.Count)
                    throw new CopeDoW2Exception("Section {0} does not have any archive at index {1}!", m_sectionName,
                                                index);
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

        #endregion

        #region IStreamExtTextCompatible<DawnOfWar2Module> Member

        public void WriteToTextStream(Stream str)
        {
            TextWriter tw = new StreamWriter(str);
            WriteToTextStream(tw);
        }

        public void WriteToTextStream(TextWriter tw)
        {
            foreach (ModuleSection ms in m_sections.Values)
            {
                ms.WriteToTextStream(tw);
            }
            tw.Flush();
        }

        public void GetFromTextStream(Stream str)
        {
            TextReader tr = new StreamReader(str);
            GetFromTextStream(tr);
        }

        public void GetFromTextStream(TextReader tr)
        {
            m_sections.Clear();
            string currentSection = null;
            var lines = new List<string>();
            while (true)
            {
                string line = tr.ReadLine();

                // create a new section if needed
                if (line == null || line.StartsWith('['))
                {
                    if (currentSection != null)
                    {
                        if (lines.Count > 1)
                        {
                            if (currentSection == "[global]" || currentSection == "[CML]")
                            {
                                var mskv = new ModuleSectionKeyValue(lines);
                                m_sections.Add(mskv.SectionName, mskv);
                            }
                            else
                            {
                                var msfl = new ModuleSectionFileList(lines);
                                m_sections.Add(msfl.SectionName, msfl);
                            }

                            lines.Clear();
                        }
                    }
                    currentSection = line;
                }

                if (line != null)
                    lines.Add(line);
                else
                    break;
            }
        }

        #endregion IStreamExtTextCompatible<DawnOfWar2Module> Member

        #region IEnumerable<ModuleSection> Member

        public IEnumerator<ModuleSection> GetEnumerator()
        {
            return m_sections.Values.GetEnumerator();
        }

        #endregion IEnumerable<ModuleSection> Member

        #region IEnumerable Member

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_sections.Values.GetEnumerator();
        }

        #endregion IEnumerable Member

        #region IGenericClonable<ModuleFile> Member

        public ModuleFile GClone()
        {
            var newModule = new ModuleFile {m_filePath = m_filePath, Tag = Tag, Stream = Stream};

            foreach (ModuleSection ms in m_sections.Values)
            {
                newModule.AddSection(ms.GClone());
            }

            return newModule;
        }

        #endregion IGenericClonable<ModuleFile> Member

        #region Nested type: ModuleSectionKeyValue

        /// <summary>
        /// A section of a Module file that bases on a Key-Value scheme like the [global]-section
        /// </summary>
        public sealed class ModuleSectionKeyValue : ModuleSection
        {
            #region fields

            private List<string> m_keyList = new List<string>();
            private List<string> m_valueList = new List<string>();

            #endregion fields

            #region ctors

            /// <summary>
            /// Creates a new Module file section with a Key-Value scheme
            /// </summary>
            /// <param name="sectionName">Name of the new section</param>
            public ModuleSectionKeyValue(string sectionName)
                : this()
            {
                m_sectionName = sectionName;
            }

            public ModuleSectionKeyValue(string sectionName, TextReader tr)
                : this(sectionName)
            {
                GetFromTextStream(tr);
            }

            public ModuleSectionKeyValue()
            {
                m_type = SectionType.ST_KeyValue;
            }

            public ModuleSectionKeyValue(List<string> lines)
                : this()
            {
                m_sectionName = lines[0];
                m_sectionName = m_sectionName.Remove(m_sectionName.Length - 1, 1).Remove(0, 1);
                string line;
                for (int i = 1; i < lines.Count; i++)
                {
                    line = lines[i];
                    if (line == string.Empty || line == "\n")
                        continue;
                    if (line.StartsWith(';'))
                        SetValue("comment", line.SubstringAfterFirst(';'));
                    else
                    {
                        string key = line.SubstringBeforeFirst(CharType.Whitespace);
                        string value = line.SubstringAfterFirst('=').SubstringAfterFirst(CharType.Whitespace);
                        // add the first part of the line as a key and the second part as a value
                        SetValue(key, value);
                    }
                }
            }

            #endregion ctors

            #region properties

            public string this[string key]
            {
                get { return GetByKey(key); }
                set { SetValue(key, value); }
            }

            #endregion properties

            #region methods

            /// <summary>
            /// Adds a Key-Value pair to the section or overrides an existing
            /// </summary>
            /// <param name="key">The key to be added as a string; use "comment" to add a comment</param>
            /// <param name="value">The value connected to the key to be added as a string</param>
            public void SetValue(string key, string value)
            {
                if (key.Equals("comment"))
                {
                    m_keyList.Add(key);
                    m_valueList.Add(value);
                    return;
                }

                if (!KeyExists(key))
                {
                    m_keyList.Add(key);
                    m_valueList.Add(value);
                }
                else
                {
                    int position = m_keyList.IndexOf(key);
                    m_valueList[position] = value;
                }
            }

            /// <summary>
            /// Removes a Key-Value from the section
            /// </summary>
            /// <param name="key">Name of the Key to remove</param>
            public void RemoveKey(string key)
            {
                int position = m_keyList.IndexOf(key);
                m_keyList.RemoveAt(position);
                m_valueList.RemoveAt(position);
            }

            /// <summary>
            /// Gets a value by it's key, throws a KeyNotFoundException if the specified key does not exist.
            /// </summary>
            /// <param name="key">Key to search for.</param>
            /// <returns></returns>
            /// <exception cref="CopeDoW2Exception">Section {0} does not contain a value with key {1}!</exception>
            public string GetByKey(string key)
            {
                if (KeyExists(key) && !(m_keyList.IndexOf(key) > m_valueList.Count))
                    return m_valueList[m_keyList.IndexOf(key)];
                throw new CopeDoW2Exception("Section {0} does not contain a value with key {1}!", m_sectionName, key);
            }

            /// <summary>
            /// Checks if a Key already exists
            /// </summary>
            /// <param name="keySearch">Key to search for</param>
            /// <returns></returns>
            public bool KeyExists(string keySearch)
            {
                return m_keyList.Any(keySearch.Equals);
            }

            /// <summary>
            /// Checks whether the input value already exists
            /// </summary>
            /// <returns></returns>
            public override bool Exists(string key, string value)
            {
                if (key.ToLower().Equals("folder") || key.ToLower().Equals("archive"))
                    return false;

                return KeyExists(key);
            }

            /// <summary>
            /// Add a key/value pair to the section
            /// </summary>
            /// <param name="key">Key</param>
            /// <param name="value">Value</param>
            public override void Add(string key, string value)
            {
                SetValue(key, value);
            }

            public override void WriteToTextStream(Stream str)
            {
                TextWriter tw = new StreamWriter(str);
                WriteToTextStream(tw);
            }

            public override void WriteToTextStream(TextWriter tw)
            {
                // the string array needs to be just two strings bigger than the key array as we need to add the section name and an empty line
                var sectionString = new string[m_keyList.Count + 2];
                var tmpStrB = new StringBuilder();

                sectionString[0] = '[' + m_sectionName + ']';

                for (int i = 0; i < sectionString.Length - 2; i++)
                {
                    if (m_keyList[i].Equals("comment"))
                    {
                        tmpStrB.SetString(';');
                        tmpStrB.Append(m_valueList[i]);
                        sectionString[i + 1] = tmpStrB.ToString();
                    }
                    else
                    {
                        tmpStrB.SetString("{0} = {1}", m_keyList[i], m_valueList[i]);
                        sectionString[i + 1] = tmpStrB.ToString();
                    }
                }
                foreach (string s in sectionString)
                    tw.WriteLine(s);
            }

            public override void GetFromTextStream(Stream str)
            {
                TextReader tr = new StreamReader(str);
                GetFromTextStream(tr);
            }

            public override void GetFromTextStream(TextReader tr)
            {
                while (true)
                {
                    string line;
                    try
                    {
                        // add some point it'll reach the end and throw an exception
                        line = tr.ReadLine();
                    }
                    catch
                    {
                        break;
                    }

                    if (line == "\n" || string.IsNullOrEmpty(line))
                        break;
                    else if (line.StartsWith(';'))
                        SetValue("comment", line.SubstringAfterFirst(';'));
                    else
                    {
                        string key = line.SubstringBeforeFirst(CharType.Whitespace);
                        string value = line.SubstringAfterFirst('=').SubstringAfterFirst(CharType.Whitespace);
                        // add the first part of the line as a key and the second part as a value
                        SetValue(key, value);
                    }
                }
            }

            #endregion methods

            public override ModuleSection GClone()
            {
                var mskv = new ModuleSectionKeyValue(m_sectionName);
                mskv.m_keyList = new List<string>(m_keyList);
                mskv.m_valueList = new List<string>(m_valueList);
                mskv.m_type = m_type;
                return mskv;
            }
        }

        #endregion
    }
}