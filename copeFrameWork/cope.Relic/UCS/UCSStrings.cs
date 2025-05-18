#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Relic.UCS
{
    ///<summary>
    /// Manages UCSStrings.
    ///</summary>
    public class UCSStrings : IEnumerable<KeyValuePair<uint, string>>
    {
        #region fields

        private readonly Dictionary<uint, string> m_strings;

        #endregion

        #region events

        ///<summary>
        /// Invoked whenever a string has been added to the collection.
        ///</summary>
        public event Action<uint, string> StringAdded;

        ///<summary>
        /// Invoked whenever a string has been removed from the collection.
        ///</summary>
        public event Action<uint> StringRemoved;

        ///<summary>
        /// Invoked whenever a string from the collection has been modified.
        ///</summary>
        public event Action<uint, string> StringModified;

        #endregion

        #region ctors

        internal UCSStrings(Dictionary<uint, string> strings, uint maxIndex)
        {
            m_strings = strings;
            MaxIndex = maxIndex;
            NextIndex = maxIndex + 1;
        }

        public UCSStrings()
        {
            m_strings = new Dictionary<uint, string>();
        }

        #endregion

        #region methods

        public IEnumerator<KeyValuePair<uint, string>> GetEnumerator()
        {
            return m_strings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        ///<summary>
        /// Returns whether or not a string with a certain index exists.
        ///</summary>
        ///<param name="index"></param>
        ///<returns></returns>
        public bool HasString(uint index)
        {
            return m_strings.ContainsKey(index);
        }

        ///<summary>
        /// Removes the string with the specified index. Returns false if there's no such string.
        ///</summary>
        ///<param name="index"></param>
        ///<returns></returns>
        public bool RemoveString(uint index)
        {
            if (m_strings.Remove(index))
            {
                if (StringRemoved != null)
                    StringRemoved(index);
                return true;
            }
            return false;
        }

        ///<summary>
        /// Tries to add a string with the specified index. Returns false if there already is a string with that index.
        ///</summary>
        ///<param name="index"></param>
        ///<param name="text"></param>
        ///<returns></returns>
        public bool AddString(uint index, string text)
        {
            if (m_strings.ContainsKey(index))
                return false;
            m_strings[index] = text;
            if (index > MaxIndex)
                MaxIndex = index;
            if (StringAdded != null)
                StringAdded(index, text);
            return true;
        }

        ///<summary>
        /// Adds a string to the collection of UCS strings and uses just the next available index.
        /// Returns the index of the new string.
        ///</summary>
        ///<param name="text"></param>
        public uint AddString(string text)
        {
            uint index = NextIndex++;
            AddString(index, text);
            return index;
        }

        /// <summary>
        /// Modifies the string at the specified index to be 'newText'.
        /// Returns false if there's no such string.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newText"></param>
        /// <returns></returns>
        public bool ModifyString(uint index, string newText)
        {
            if (m_strings.ContainsKey(index))
            {
                m_strings[index] = newText;
                if (StringModified != null)
                    StringModified(index, newText);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to modify the string at the specified index, if there's no string with that index
        /// it'll add a new string with the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="text"></param>
        public void ModifyOrAdd(uint index, string text)
        {
            if (m_strings.ContainsKey(index))
            {
                m_strings[index] = text;
                if (StringModified != null)
                    StringModified(index, text);
                return;
            }
            m_strings[index] = text;
            if (StringAdded != null)
                StringAdded(index, text);
        }

        /// <summary>
        /// Tries to get the string with the specified index. Returns true on success, otherwise false.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool TryGetValue(uint index, out string text)
        {
            return TryGetValue(index, out text);
        }

        #endregion

        #region properties

        /// <summary>
        /// Returns the maximum index currently in use.
        /// </summary>
        public uint MaxIndex { get; private set; }

        /// <summary>
        /// Gets or sets the next index to be used.
        /// </summary>
        public uint NextIndex { get; set; }

        /// <summary>
        /// Behaves just as the ModifyOrAdd method.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[uint index]
        {
            get
            {
                string text;
                if (m_strings.TryGetValue(index, out text))
                    return text;
                return null;
            }
            set { ModifyOrAdd(index, value); }
        }

        /// <summary>
        /// Returns the number of strings held by this instance of UCSStrings.
        /// </summary>
        public int StringCount
        {
            get { return m_strings.Count; }
        }

        #endregion
    }
}