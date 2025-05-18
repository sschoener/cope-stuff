/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using cope;
using cope.DawnOfWar2;
using cope.DawnOfWar2.RelicAttribute;
using cope.Extensions;
using ModTool.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace RBFPlugin
{
    public class RBFLibEntry : IComparable<RBFLibEntry>
    {
        public string Name;
        public string Submenu;
        public string[] Tags;
        public string[] TagGroups;
        public List<AttributeValue> Values;

        public int CompareTo(RBFLibEntry other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    // Todo: clean up!
    public static class RBFLibrary
    {
        static RBFLibraryEditor s_libraryForm;

        // Key: tags
        // Value: key = name of the value, value = corresponding RBFValue
        private static readonly Dictionary<string, SortedDictionary<string, RBFLibEntry>> s_library = new Dictionary<string, SortedDictionary<string, RBFLibEntry>>();
        private static readonly SortedDictionary<string, RBFLibEntry> s_values = new SortedDictionary<string, RBFLibEntry>();
        private static readonly Dictionary<string, string[]> s_tagGroups = new Dictionary<string, string[]>();
        private static readonly char[] s_tagSeperator = new[] { ',' };

        public static event GenericHandler<RBFLibEntry> EntryAdded;
        public static event GenericHandler<RBFLibEntry> EntryRemoved;

        public static void Init()
        {
            ModManager.ApplicationExit += ModManagerApplicationExit;

            // try to read tag file
            string tagPath = Directory.GetCurrentDirectory() + "\\rbf_library_tags.xml";
            if (File.Exists(tagPath))
            {
                Stream tagFile = null;
                try
                {
                    LoggingManager.SendMessage("RBFLib - Reading RBF-Library tag groups");
                    tagFile = File.Open(tagPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    ReadTags(tagFile);
                }
                catch (Exception ex)
                {
                    LoggingManager.SendError("RBFLib - Could not read or open RBF-Library tag file");
                    LoggingManager.HandleException(ex);
                     UIHelper.ShowError("Could not read RBF-Library! See log file for further information.");
                }
                finally
                {
                    if (tagFile != null)
                        tagFile.Close();
                }

            } else
                LoggingManager.SendWarning("RBFLib - Could not find RBF-Library tag groups file!");

            // try to read library
            string libraryPath = Directory.GetCurrentDirectory() + "\\rbf_library.xml";
            if (File.Exists(libraryPath))
            {
                Stream libraryFile = null;
                try
                {
                    LoggingManager.SendMessage("RBFLib - Reading RBF-Library");
                    libraryFile = File.Open(libraryPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    ReadLibrary(libraryFile);
                }
                catch (Exception ex)
                {
                    LoggingManager.SendError("RBFLib - Could not read or open RBF-Library");
                    LoggingManager.HandleException(ex);
                     UIHelper.ShowError("Could not read RBF-Library! See log file for further information.");
                }
                finally
                {
                    if (libraryFile != null)
                        libraryFile.Close();
                }
                return;
            }
            LoggingManager.SendWarning("RBFLib - Could not find RBF-Library");

            // try to read legacy library
            string legacyPath = Directory.GetCurrentDirectory() + "\\rbf_library.txt";
            if (!File.Exists(legacyPath))
            {
                LoggingManager.SendWarning("RBFLib - Could not find legacy RBF-Library either");
                return;
            }
            StreamReader legacyReader = null;
            try
            {
                LoggingManager.SendMessage("RBFLib - Reading RBF-Library (legacy)");
                legacyReader = File.OpenText(legacyPath);
                ReadLibraryLegacy(legacyReader);
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("RBFLib - Could not read or open RBF-Library (legacy)");
                LoggingManager.HandleException(ex);
                 UIHelper.ShowError("Could not read RBF-Library! See log file for further information.");
            }
            finally
            {
                if (legacyReader != null)
                    legacyReader.Close();
            }

            if (s_values.Count > 0)
            {
                foreach (RBFLibEntry rle in s_values.Values)
                {
                    if (rle.Tags != null)
                    {
                        foreach (string tag in rle.Tags)
                        {
                            if (!s_library.ContainsKey(tag))
                                s_library.Add(tag, new SortedDictionary<string, RBFLibEntry>());
                            s_library[tag].Add(rle.Name, rle);
                        }
                    }
                    if (rle.TagGroups == null)
                        continue;
                    foreach (string tagGroup in rle.TagGroups)
                    {
                        string[] tags;
                        if (s_tagGroups.TryGetValue(tagGroup, out tags))
                        {
                            foreach (string tag in tags)
                            {
                                if (!s_library.ContainsKey(tag))
                                    s_library.Add(tag, new SortedDictionary<string, RBFLibEntry>());
                                if (!s_library[tag].ContainsKey(rle.Name))
                                    s_library[tag].Add(rle.Name, rle);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns null if there are no entries.
        /// </summary>
        /// <returns></returns>
        static public SortedDictionary<string, RBFLibEntry> GetEntriesForTag(string tag)
        {
            if (s_library.ContainsKey(tag))
                return s_library[tag];
            return null;
        }

        static public RBFLibEntry GetEntry(string name)
        {
            if (s_values.ContainsKey(name))
                return s_values[name];
            return null;
        }

        static public void RemoveEntryFromTagGroup(RBFLibEntry entry, string taggroup)
        {
            if (taggroup == string.Empty || !s_tagGroups.ContainsKey(taggroup))
                return;
            string[] tags = s_tagGroups[taggroup];
            foreach (string tag in tags)
                RemoveEntryFromTag(entry, tag);
        }

        static public void AddEntryToTagGroup(RBFLibEntry entry, string taggroup)
        {
            if (taggroup == string.Empty || !s_tagGroups.ContainsKey(taggroup))
                return;
            string[] tags = s_tagGroups[taggroup];
            foreach (string tag in tags)
                AddEntryToTag(entry, tag);
        }

        static public void RemoveEntryFromTag(RBFLibEntry entry, string tag)
        {
            if (tag == string.Empty || !s_library.ContainsKey(tag))
                return;
            SortedDictionary<string, RBFLibEntry> entries = s_library[tag];
            if (entries.ContainsKey(entry.Name))
                entries.Remove(entry.Name);
        }

        static public void AddEntryToTag(RBFLibEntry entry, string tag)
        {
            if (tag == string.Empty)
                return;
            SortedDictionary<string, RBFLibEntry> entries;
            if (!s_library.ContainsKey(tag))
            {
                entries = new SortedDictionary<string, RBFLibEntry>();
                s_library.Add(tag, entries);
            }
            else
                entries = s_library[tag];
            if (!entries.ContainsKey(entry.Name))
                entries.Add(entry.Name, entry);
        }

        static public void AddEntry(RBFLibEntry entry)
        {
            s_values.Add(entry.Name, entry);
            foreach (string tag in entry.Tags)
                AddEntryToTag(entry, tag);
            foreach (string taggroup in entry.TagGroups)
                AddEntryToTagGroup(entry, taggroup);
            if (EntryAdded != null)
                EntryAdded(null, entry);
        }

        static public void RemoveEntry(RBFLibEntry entry)
        {
            s_values.Remove(entry.Name);
            foreach (string tag in entry.Tags)
                RemoveEntryFromTag(entry, tag);
            foreach (string taggroup in entry.TagGroups)
                RemoveEntryFromTagGroup(entry, taggroup);
            if (EntryRemoved != null)
                EntryRemoved(null, entry);
        }

        static public void RemoveEntry(string entryName)
        {
            RBFLibEntry entry;
            if (s_values.TryGetValue(entryName, out entry))
            {
                RemoveEntry(entry);
            }
        }

        static public SortedDictionary<string, RBFLibEntry> GetAllEntries()
        {
            return s_values;
        }

        static void ModManagerApplicationExit()
        {
            Stream libraryFile = null;
            Stream tagFile = null;
            try
            {
                LoggingManager.SendMessage("RBFLib - Writing RBF-Library");
                string libraryPath = Directory.GetCurrentDirectory() + "\\rbf_library.xml";
                if (File.Exists(libraryPath))
                    File.Delete(libraryPath);
                libraryFile = File.Create(libraryPath);
                WriteLibrary(libraryFile);

                if (s_tagGroups.Count > 0)
                {
                    LoggingManager.SendMessage("RBFLib - Writing RBF-Library tag groups");
                    string tagPath = Directory.GetCurrentDirectory() + "\\rbf_library_tags.xml";
                    if (File.Exists(tagPath))
                        File.Delete(tagPath);
                    tagFile = File.Create(tagPath);
                    WriteTags(tagFile);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("RBFLib - Could not write RBF-Library!");
                LoggingManager.HandleException(ex);
                 UIHelper.ShowError("Could not write RBF-Library! See log file for further information.");
            }
            finally
            {
                if (libraryFile != null)
                    libraryFile.Close();
                if (tagFile != null)
                    tagFile.Close();
            }
        }

        static void ReadLibraryLegacy(StreamReader reader)
        {
            string line = reader.ReadLine();
            string value = string.Empty;
            RBFLibEntry currentEntry = null;
            while (line != null)
            {
                if (line.StartsWith("//"))
                {
                }
                else if (line == string.Empty)
                {
                    if (value != string.Empty && currentEntry != null)
                    {
                        currentEntry.Values = CorsixStyleConverter.Parse(value);
                    }
                    value = string.Empty;
                }
                else if (line.StartsWith("[name="))
                {
                    line = line.Remove(line.Length - 1, 1).Remove(0, 6);
                    if (currentEntry != null)
                    {
                        if (currentEntry.TagGroups == null)
                            currentEntry.TagGroups = new string[0];
                        if (currentEntry.Tags == null)
                            currentEntry.Tags = new string[0];
                        s_values.Add(currentEntry.Name, currentEntry);
                    }
                        
                    currentEntry = new RBFLibEntry {Name = line};
                }
                else if (line.StartsWith("[tags="))
                {
                    line = line.Remove(line.Length - 1, 1).Remove(0, 6);
                    if (line.Length > 0 && currentEntry != null)
                        currentEntry.Tags = line.Split(s_tagSeperator, StringSplitOptions.RemoveEmptyEntries);
                        
                }
                else if (line.StartsWith("[taggroups="))
                {
                    line = line.RemoveLast(1).Remove(0, 11);
                    if (currentEntry != null)
                        currentEntry.TagGroups = line.Split(s_tagSeperator, StringSplitOptions.RemoveEmptyEntries);
                }
                else if (line.StartsWith("[sub="))
                {
                    line = line.Remove(line.Length - 1, 1).Remove(0, 5);
                    if (currentEntry != null)
                        currentEntry.Submenu = line;
                }
                else
                {
                    value += line;
                    value += '\n';
                }
                line = reader.ReadLine();
            }
            if (currentEntry != null)
            {
                if (value != string.Empty)
                    currentEntry.Values = CorsixStyleConverter.Parse(value);
                if (!s_values.ContainsKey(currentEntry.Name))
                    s_values.Add(currentEntry.Name, currentEntry);
            }
        }

        static void WriteLibrary(Stream stream)
        {
            var settings = new XmlWriterSettings {Indent = true};
            XmlWriter writer = XmlWriter.Create(stream, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("RBFLibrary");

            foreach (var entry in s_values.Values)
            {
                if (entry.Values.Count == 0)
                    continue;
                writer.WriteStartElement("Entry");
                writer.WriteElementString("Name", entry.Name);
                if (entry.Tags != null && entry.Tags.Length != 0)
                {
                    writer.WriteStartElement("Tags");
                    foreach (var tag in entry.Tags)
                        writer.WriteElementString("Tag", tag);
                    writer.WriteFullEndElement();
                }
                if (entry.TagGroups != null && entry.TagGroups.Length != 0)
                {
                    writer.WriteStartElement("TagGroups");
                    foreach (var taggroup in entry.TagGroups)
                        writer.WriteElementString("TagGroup", taggroup);
                    writer.WriteFullEndElement();
                }
                if (entry.Submenu != null)
                {
                    writer.WriteElementString("Submenu", entry.Submenu);
                }
                writer.WriteStartElement("Values");
                AttributeXmlWriter.Write(writer, entry.Values);
                writer.WriteFullEndElement();
                writer.WriteFullEndElement();
            }

            writer.WriteFullEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        /// <exception cref="CopeException">Expected a XmlDeclaration at the beginning of the RBF library.</exception>
        static void ReadLibrary(Stream stream)
        {
            var settings = new XmlReaderSettings{IgnoreComments = true, IgnoreWhitespace = true};
            XmlReader reader = XmlReader.Create(stream, settings);
            if (!reader.Read() || reader.NodeType != XmlNodeType.XmlDeclaration)
                throw new CopeException("Expected a XmlDeclaration at the beginning of the RBF library.");
            if (!reader.Read() || reader.NodeType != XmlNodeType.Element || reader.Name != "RBFLibrary")
                throw new CopeException("Expected a node with name 'RBFLibrary' as the main node.");
            while (reader.Read() && reader.MoveToContent() == XmlNodeType.Element)
            {
                if (reader.Name == "Entry")
                {
                    string name = null;
                    string submenu = null;
                    List<string> tags = new List<string>();
                    List<string> taggroups = new List<string>();
                    IEnumerable<AttributeValue> values = null;

                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        if (reader.Name == "Name")
                            name = reader.ReadElementContentAsString();
                        else if (reader.Name == "Submenu")
                            submenu = reader.ReadElementContentAsString();
                        else if (reader.Name == "Tags")
                        {
                            reader.Read();
                            while (reader.Name == "Tag")
                            {
                                tags.Add(reader.ReadElementContentAsString());
                            }
                            reader.ReadEndElement();
                        }
                        else if (reader.Name == "TagGroups")
                        {
                            reader.Read();
                            while (reader.Name == "TagGroup")
                            {
                                taggroups.Add(reader.ReadElementContentAsString());
                            }
                            reader.ReadEndElement();
                        }
                        else if (reader.Name == "Values")
                        {
                            reader.Read();
                            values = AttributeXmlReader.ReadData(reader, null);
                            reader.ReadEndElement();
                        }
                        else reader.Read();
                    }
                    if (name != null && values != null && (tags.Count != 0 || taggroups.Count != 0))
                    {
                        RBFLibEntry entry = new RBFLibEntry
                                                {
                                                    Name = name,
                                                    Submenu = submenu,
                                                    Tags = tags.ToArray(),
                                                    TagGroups = taggroups.ToArray(),
                                                    Values = new List<AttributeValue>(values)
                                                };
                        AddEntry(entry);
                    }
                }
            }
        }

        /// <exception cref="CopeException">Expected a Document node at the start of the tag file.</exception>
        static void ReadTags(Stream stream)
        {
            var settings = new XmlReaderSettings {IgnoreComments = true, IgnoreWhitespace = true};
            XmlReader reader = XmlReader.Create(stream, settings);
            if (!reader.Read() || reader.NodeType != XmlNodeType.XmlDeclaration)
                throw new CopeException("Expected a XmlDeclaration at the start of the tag file.");
            if (!reader.Read() || reader.NodeType != XmlNodeType.Element || reader.Name != "TagGroups")
                throw new CopeException("Expected a node with name 'TagGroups' as main node.");
            while (reader.Read() && reader.MoveToContent() == XmlNodeType.Element)
            {
                if (reader.Name == "TagGroup")
                {
                    string key = null;
                    List<string> tags = new List<string>();
                    reader.Read();
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        if (reader.Name == "Key")
                        {
                            key = reader.ReadElementContentAsString();
                        }
                        else if (reader.Name == "Tags")
                        {
                            reader.Read();
                            while (reader.Name == "Tag")
                            {
                                tags.Add(reader.ReadElementContentAsString());
                            }
                            reader.ReadEndElement();
                        }
                        else reader.Read();
                    }
                    if (key != null)
                    {
                        tags.Sort();
                        s_tagGroups.Add(key, tags.ToArray());
                    }
                }
            }
        }

        static void WriteTags(Stream stream)
        {
            var settings = new XmlWriterSettings {Indent = true};
            XmlWriter writer = XmlWriter.Create(stream, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("TagGroups");
            foreach (var kvp in s_tagGroups)
            {
                if (kvp.Value.Length == 0)
                    continue;
                writer.WriteStartElement("TagGroup");
                writer.WriteElementString("Key", kvp.Key);
                writer.WriteStartElement("Tags");
                foreach (string tag in kvp.Value)
                    writer.WriteElementString("Tag", tag);
                writer.WriteFullEndElement();
                writer.WriteFullEndElement();
            }
            writer.WriteFullEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        static public IEnumerable<string> GetTagGroupNames()
        {
            return s_tagGroups.Keys;
        }

        static public IEnumerable<string> GetTagGroup(string name)
        {
            string[] tags;
            if (s_tagGroups.TryGetValue(name, out tags))
                return tags;
            return null;
        }

        static public void AddTagsToGroup(string groupName, IEnumerable<string> tags)
        {
            string[] taggroup;
            if (!s_tagGroups.TryGetValue(groupName, out taggroup))
                taggroup = tags.ToArray();
            else
                taggroup.Append(tags.ToArray());
            s_tagGroups[groupName] = taggroup;
        }

        static public void ShowLibraryForm()
        {
            if (s_libraryForm == null || s_libraryForm.IsDisposed)
                s_libraryForm = new RBFLibraryEditor();
            s_libraryForm.Show();
        }
    }
}