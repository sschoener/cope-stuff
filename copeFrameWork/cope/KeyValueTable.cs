using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace cope
{
    public class KeyValueTable : IGenericClonable<KeyValueTable>, IEnumerable<KeyedValue>
    {
        private readonly Dictionary<string, List<KeyedValue>> m_entries;

        public KeyValueTable()
        {
            m_entries = new Dictionary<string, List<KeyedValue>>();
        }

        public void AddValue(KeyedValue value)
        {
            List<KeyedValue> values;
            if (m_entries.TryGetValue(value.Key, out values))
            {
                values.Add(value);
                return;
            }
            values = new List<KeyedValue> {value};
            m_entries[value.Key] = values;
        }

        /// <summary>
        /// Removes the specified value from the entries.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool RemoveValue(KeyedValue value)
        {
            List<KeyedValue> values;
            if (!m_entries.TryGetValue(value.Key, out values))
                return false;
            return values.Remove(value);
        }

        /// <summary>
        /// Removes all values with the given key from the collection.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveValues(string name)
        {
            return m_entries.Remove(name);
        }

        public bool ContainsValue(KeyedValue value)
        {
            List<KeyedValue> values;
            if (m_entries.TryGetValue(value.Key, out values))
                return false;
            return values.Contains(value);
        }

        public bool ContainsValues(string name)
        {
            return m_entries.ContainsKey(name);
        }

        public KeyValueTable GClone()
        {
            KeyValueTable table = new KeyValueTable();
            foreach (var kvp in m_entries)
                table.m_entries[kvp.Key] = kvp.Value.Select(x => x.GClone()).ToList();
            return table;
        }

        public int ChildCount
        {
            get
            {
                return m_entries.Sum(kvp => kvp.Value.Count);
            }
        }

        public KeyedValue Owner
        {
            get;
            internal set;
        }

        public IEnumerator<KeyedValue> GetEnumerator()
        {
            return new KeyValueTableEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class KeyValueTableEnumerator : IEnumerator<KeyedValue>
        {
            private readonly Queue<List<KeyedValue>> m_todo;
            private IEnumerator<KeyedValue> m_currentEnumerator;

            public KeyValueTableEnumerator(KeyValueTable kvt)
            {
                m_todo = new Queue<List<KeyedValue>>();
                foreach (var kvp in kvt.m_entries)
                    m_todo.Enqueue(kvp.Value);
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (m_currentEnumerator == null)
                {
                    if (m_todo.Count == 0)
                        return false;
                    m_currentEnumerator = m_todo.Dequeue().GetEnumerator();
                }
                if (!m_currentEnumerator.MoveNext())
                {
                    m_currentEnumerator = null;
                    return MoveNext();
                }
                Current = m_currentEnumerator.Current;
                return true;
            }

            /// <exception cref="InvalidOperationException">Not supported.</exception>
            public void Reset()
            {
                throw new InvalidOperationException("Not supported.");
            }

            public KeyedValue Current { get; protected set; }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}