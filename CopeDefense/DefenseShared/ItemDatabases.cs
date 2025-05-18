using System.Collections.Generic;
using System.IO;
using System.Text;
using cope;
using cope.Extensions;

namespace DefenseShared
{
    /// <summary>
    /// Helper class which grants access to descriptions and names of unlocks, upgrades and wargear.
    /// </summary>
    public static class ItemDatabases
    {
        public static ItemStore Wargear { get; set; }
        public static ItemStore Upgrades { get; set; }
        public static ItemStore Unlocks { get; set; }

        class Entry
        {
            public int Id;
            public string Name;
            public string Description;
            public readonly Dictionary<string, string> AdditionalInformation = new Dictionary<string, string>();

            public override string ToString()
            {
                return "Id - " + (Name ?? "no name");
            }
        }

        public class ItemStore
        {
            private readonly Dictionary<int, Entry> m_entries = new Dictionary<int, Entry>();

            public string GetName(int id)
            {
                Entry entry;
                if (!m_entries.TryGetValue(id, out entry) || string.IsNullOrWhiteSpace(entry.Name))
                    return "no name (" + id + ")";
                return entry.Name;

            }

            public string GetDesc(int id)
            {
                Entry entry;
                if (!m_entries.TryGetValue(id, out entry) || string.IsNullOrWhiteSpace(entry.Description))
                    return "no desc (" + id + ")";
                return entry.Description;
            }

            public string GetOther(int id, string key)
            {
                Entry entry;
                if (!m_entries.TryGetValue(id, out entry))
                    return string.Empty;
                string info;
                if (!entry.AdditionalInformation.TryGetValue(key, out info))
                    return string.Empty;
                return info;
            }

            public IDictionary<string,string> GetOther(int id)
            {
                Entry entry;
                if (!m_entries.TryGetValue(id, out entry))
                    return new Dictionary<string, string>(0);
                return entry.AdditionalInformation;
            }

            public void Update(int id, string name, string desc)
            {
                if (string.IsNullOrWhiteSpace(name) || name.StartsWith("no name"))
                    name = null;
                if (string.IsNullOrWhiteSpace(desc) || desc.StartsWith("no desc"))
                    desc = null;
                Entry entry;
                if (!m_entries.TryGetValue(id, out entry))
                {
                    entry = new Entry { Id = id, Name = name};
                    if (desc != null)
                        ScanForAdditionalInformation(entry, desc);
                    m_entries[id] = entry;
                }
                    
                else
                {
                    entry.Name = name;
                    if (desc != null)
                        ScanForAdditionalInformation(entry, desc);
                }
            }

            public void UpdateOther(int id, string name, string value)
            {
                Entry entry;
                if (m_entries.TryGetValue(id, out entry))
                    entry.AdditionalInformation[name] = value;
            }

            public bool Remove(int id)
            {
                return m_entries.Remove(id);
            }

            public void WriteDatabase(Stream str)
            {
                TextWriter tw = new StreamWriter(str, Encoding.UTF8);
                foreach (var kvp in m_entries)
                {
                    var entry = kvp.Value;
                    if (entry.Name != null)
                    {
                        tw.WriteLine("#" + kvp.Key + " " + entry.Name);
                        foreach (var addInfo in kvp.Value.AdditionalInformation)
                        {
                            tw.Write("[[");
                            tw.Write(addInfo.Key);
                            tw.Write(" = ");
                            tw.Write(addInfo.Value);
                            tw.WriteLine("]]");
                        }
                        tw.WriteLine(entry.Description ?? string.Empty);
                    }
                    tw.WriteLine();
                }
                tw.Flush();
            }

            public static ItemStore ReadDatabase(Stream str)
            {
                ItemStore db = new ItemStore();
                TextReader tr = new StreamReader(str, Encoding.UTF8);
                while (!tr.IsAtEnd())
                {
                    var entry = ReadEntry(tr);
                    if (entry == null)
                        break;
                    db.m_entries.Add(entry.Id, entry);
                }
                return db;
            }

            /// <summary>
            /// Scan's the specified description for embedded additional information, e.g. [[TEST=VALUE]], and adds them to the
            /// dictionary of the given entry. The entry's description is also updated. Returns the description string without the additional information.
            /// </summary>
            /// <param name="entry"></param>
            /// <param name="description"></param>
            private static string ScanForAdditionalInformation(Entry entry, string description)
            {
                entry.AdditionalInformation.Clear();
                var lines = description.Split("\r\n", "\n");
                StringBuilder newDescription = new StringBuilder(description.Length);
                foreach (string line in lines)
                {
                    if (line.StartsWith("[[") && line.EndsWith("]]"))
                    {
                        string key, value;
                        line.SplitAtFirst('=', out key, out value);
                        key = key.RemoveFirst(2).Trim();
                        value = value.RemoveLast(2).Trim();
                        entry.AdditionalInformation[key] = value;
                    }
                    else
                        newDescription.AppendLine(line);
                }
                string newDesc = newDescription.ToString().Trim('\t', '\n', '\r', ' ');
                entry.Description = newDesc;
                return newDesc;
            }

            private static Entry ReadEntry(TextReader tr)
            {
                string line;
                do
                {
                    line = tr.ReadLine();
                    if (line == null)
                        return null;
                } while (!line.StartsWith('#'));
                string idPart = line.SubstringBeforeFirst(CharType.Whitespace).RemoveFirst(1);
                int id = int.Parse(idPart);
                string name = line.SubstringAfterFirst(CharType.Whitespace);
                string desc = tr.ReadUntil('#').Trim('\t', '\n', '\r', ' ');
                var entry = new Entry { Id = id, Name = name, Description = desc };
                ScanForAdditionalInformation(entry, desc);
                return entry;
            }

        }
    }
}