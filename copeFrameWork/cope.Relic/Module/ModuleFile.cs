#region

using System.Collections;
using System.Collections.Generic;
using System.IO;
using cope.Extensions;

#endregion

namespace cope.Relic.Module
{
    /// <summary>
    /// Class for operating with .module files
    /// </summary>
    public sealed class ModuleFile : IEnumerable<ModuleSection>, IGenericClonable<ModuleFile>
    {
        #region fields

        private readonly Dictionary<string, ModuleSection> m_sections = new Dictionary<string, ModuleSection>();

        #endregion fields

        #region ctors

        public ModuleFile()
        {
            m_sections = new Dictionary<string, ModuleSection>();
        }

        #endregion ctors

        #region methods

        /// <summary>
        /// Creates a new section in the module file
        /// </summary>
        /// <param name="name">Name of the section, e.g. [global]</param>
        /// <param name="type">Set this to 0 to create a Key-Value section or to 1 to create a FileList section</param>
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public void CreateNewSection(string name, byte type)
        {
            if (m_sections.ContainsKey(name))
                throw new RelicException("Module file already contains a section called " + name + "!");
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
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public void AddToSection(string sectionName, string key, string value)
        {
            if (m_sections.ContainsKey(sectionName))
                m_sections[sectionName].Add(key, value);
            throw new RelicException("Module file does not contain a section with name " + sectionName + "!");
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

        #endregion methods

        #region properties

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public ModuleSection this[string sectionName]
        {
            get
            {
                if (m_sections.ContainsKey(sectionName))
                    return m_sections[sectionName];
                throw new RelicException("Module file does not contain a section with name " + sectionName + "!");
            }
            set { m_sections[sectionName] = value; }
        }

        public int SectionCount
        {
            get { return m_sections.Count; }
        }

        #endregion properties

        public void Write(TextWriter tw)
        {
            foreach (ModuleSection ms in m_sections.Values)
            {
                ms.WriteToTextStream(tw);
            }
            tw.Flush();
        }

        public void Read(TextReader tr)
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
                            if (currentSection == "[global]")
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
            var newModule = new ModuleFile();

            foreach (ModuleSection ms in m_sections.Values)
            {
                newModule.AddSection(ms.GClone());
            }

            return newModule;
        }

        #endregion IGenericClonable<ModuleFile> Member
    }
}