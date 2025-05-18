using System.Collections.Generic;
using System.IO;
using System.Linq;
using cope.Extensions;

namespace cope.Relic.Module
{
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
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public string GetByKey(string key)
        {
            if (KeyExists(key) && !(m_keyList.IndexOf(key) > m_valueList.Count))
                return m_valueList[m_keyList.IndexOf(key)];
            throw new RelicException("Section" + m_sectionName + " does not contain a value with key " + key + "!");
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

        public override void WriteToTextStream(TextWriter tw)
        {
            // the string array needs to be just one string bigger than the key array as we need to add the section name
            var sectionString = new string[m_keyList.Count + 1];

            sectionString[0] = '[' + m_sectionName + ']';
            for (int i = 0; i < sectionString.Length - 1; i++)
            {
                if (m_keyList[i].Equals("comment"))
                    sectionString[i + 1] = ";" + m_valueList[i];
                else
                    sectionString[i + 1] = m_keyList[i] + m_valueList[i];
            }
            foreach (string s in sectionString)
                tw.WriteLine(s);
            tw.WriteLine(); // an empty line signals the end of the section
        }

        public override void GetFromTextStream(TextReader tr)
        {
            while (!tr.IsAtEnd())
            {
                string line = tr.ReadLine();
                if (line == "\n" || string.IsNullOrEmpty(line))
                    break;
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

        #endregion methods

        public override ModuleSection GClone()
        {
            var mskv = new ModuleSectionKeyValue(m_sectionName);
            mskv.m_keyList = new List<string>(m_keyList);
            mskv.m_valueList = new List<string>(m_valueList);
            return mskv;
        }
    }
}