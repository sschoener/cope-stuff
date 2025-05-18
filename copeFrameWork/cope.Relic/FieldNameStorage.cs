#region

using System;
using System.Collections;
using System.Collections.Generic;
using cope.Extensions;
using cope.Relic.RelicBinary;

#endregion

namespace cope.Relic
{
    /// <summary>
    /// Represents a collection of field names such as the data stored in FLB files.
    /// Every field name gets an index so data can refer to the name using these instead of having to write out the name all the time.
    /// </summary>
    public class FieldNameStorage : IRBFKeyProvider, IEnumerable<string>
    {
        private readonly Dictionary<string, int> m_keyToIndex;
        private readonly List<string> m_sNewKeys;
        private string[] m_sNames;

        /// <summary>
        /// Constructs a new empty FieldNameStorage instance.
        /// </summary>
        public FieldNameStorage()
        {
            m_sNewKeys = new List<string>();
            m_keyToIndex = new Dictionary<string, int>();
            m_sNames = new string[0];
        }

        /// <summary>
        /// Constructs a new FieldNameStorage instance containing the names from the given array.
        /// The index of a string in the array will also be the index used in the FieldNameStorage.
        /// </summary>
        /// <param name="keys"></param>
        public FieldNameStorage(string[] keys)
        {
            m_sNewKeys = new List<string>();
            m_keyToIndex = new Dictionary<string, int>(keys.Length);
            for (int idx = 0; idx < keys.Length; idx++)
                m_keyToIndex.Add(keys[idx], idx);
            m_sNames = new string[keys.Length];
            Array.Copy(keys, m_sNames, keys.Length);
        }

        /// <summary>
        /// Returns the number of keys in the FieldNameStorage object - without those which have been added after the last update.
        /// </summary>
        public int NumKeys
        {
            get { return m_sNames.Length; }
        }

        #region IEnumerable<string> Members

        /// <summary>
        /// Returns an enumerator which enumerates all the keys in this FieldNameStorage object (without those added after the last update).
        /// The keys are enumerated in the same order as their indices.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return (IEnumerator<string>) m_sNames.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IRBFKeyProvider Members

        /// <summary>
        /// Gets the key belonging to the specified index. May throw exceptions if there is no such index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public string GetNameByIndex(int index)
        {
            if (m_sNames.Length > index)
                return m_sNames[index];

            int idx = index - m_sNames.Length;
            if (idx < m_sNewKeys.Count)
                return m_sNewKeys[idx];
            throw new RelicException("Trying to get key for index " + index + " but the highest available index is " +
                                     (m_sNames.Length - 1));
        }

        /// <summary>
        /// Tries to get the key belonging to the specified index. Returns true on success.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGetName(int index, out string name)
        {
            name = null;
            if (m_sNames.Length > index)
            {
                name = m_sNames[index];
                return true;
            }
            int idx = index - m_sNames.Length;
            if (idx >= m_sNewKeys.Count)
                return false;
            name = m_sNewKeys[idx];
            return true;
        }

        /// <summary>
        /// Gets the index for a specified key. Returns -1 if there's no such key.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetIndexForName(string name)
        {
            int index;
            if (m_keyToIndex.TryGetValue(name, out index))
                return index;
            return -1;
        }

        /// <summary>
        /// Adds a new key and returns the index. If you add a new key, the FieldNameStorage object will be flagged as changed.
        /// </summary>
        /// <returns></returns>
        public int AddKey(string key)
        {
            int index = GetIndexForName(key);
            if (index != -1)
                return index;
            index = m_sNames.Length + m_sNewKeys.Count;
            m_sNewKeys.Add(key);
            m_keyToIndex.Add(key, index);
            return index;
        }

        /// <summary>
        /// Returns whether or not this instance of FieldNameStorage got any new keys since the last update.
        /// </summary>
        /// <returns></returns>
        public bool NeedsUpdate()
        {
            return m_sNewKeys.Count > 0;
        }

        /// <summary>
        /// Unsets the NeedsUpdate-flag and updates the internals.
        /// </summary>
        public void Update()
        {
            // update string array
            if (m_sNames != null)
                m_sNames = m_sNames.Append(m_sNewKeys.ToArray());
            else
                m_sNames = m_sNewKeys.ToArray();
            m_sNewKeys.Clear();
        }

        #endregion
    }
}