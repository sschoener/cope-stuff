using System;
using System.Collections;
using System.Collections.Generic;

namespace cope.DawnOfWar2
{
    public class UCSStrings : IEnumerable<KeyValuePair<uint, string>>, IEnumerable
    {
        private readonly Dictionary<uint, string> m_strings;

        // Events
        public event Action<uint, string> StringAdded;
        public event Action<uint, string> StringModified;
        public event Action<uint> StringRemoved;

        public UCSStrings()
        {
            this.m_strings = new Dictionary<uint, string>();
        }

        internal UCSStrings(Dictionary<uint, string> strings, uint maxIndex)
        {
            this.m_strings = strings;
            this.MaxIndex = maxIndex;
            this.NextIndex = maxIndex + 1;
        }

        public uint MaxIndex { get; private set; }
        public uint NextIndex { get; set; }

        public uint AddString(string text)
        {
            uint num2;
            this.NextIndex = (num2 = this.NextIndex) + 1;
            uint index = num2;
            this.AddString(index, text);
            return index;
        }

        public bool AddString(uint index, string text)
        {
            if (this.m_strings.ContainsKey(index))
            {
                return false;
            }
            this.m_strings[index] = text;
            if (index > this.MaxIndex)
            {
                this.MaxIndex = index;
            }
            if (index >= this.NextIndex)
            {
                this.NextIndex = index + 1;
            }
            if (this.StringAdded != null)
            {
                this.StringAdded(index, text);
            }
            return true;
        }

        public IEnumerator<KeyValuePair<uint, string>> GetEnumerator()
        {
            return this.m_strings.GetEnumerator();
        }

        public bool HasString(uint index)
        {
            return this.m_strings.ContainsKey(index);
        }

        public void ModifyOrAdd(uint index, string text)
        {
            if (this.m_strings.ContainsKey(index))
            {
                this.m_strings[index] = text;
                if (this.StringModified != null)
                {
                    this.StringModified(index, text);
                }
            }
            else
            {
                this.m_strings[index] = text;
                if (this.StringAdded != null)
                {
                    this.StringAdded(index, text);
                }
            }
        }

        public bool ModifyString(uint index, string newText)
        {
            if (!this.m_strings.ContainsKey(index))
            {
                return false;
            }
            this.m_strings[index] = newText;
            if (this.StringModified != null)
            {
                this.StringModified(index, newText);
            }
            return true;
        }

        public bool RemoveString(uint index)
        {
            if (!this.m_strings.Remove(index))
            {
                return false;
            }
            if (this.StringRemoved != null)
            {
                this.StringRemoved(index);
            }
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool TryGetValue(uint index, out string text)
        {
            return this.TryGetValue(index, out text);
        }

        // Properties
        public string this[uint index]
        {
            get
            {
                string str;
                if (this.m_strings.TryGetValue(index, out str))
                {
                    return str;
                }
                return null;
            }
            set
            {
                this.ModifyOrAdd(index, value);
            }
        }

        public int StringCount
        {
            get
            {
                return this.m_strings.Count;
            }
        }

    }
}
