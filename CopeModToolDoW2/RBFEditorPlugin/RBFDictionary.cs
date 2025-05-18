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
using cope.Extensions;
using ModTool.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace RBFPlugin
{
    public enum SearchPathRoot
    {
        Attrib,
        Data
    }

    public class SearchPathInfo
    {
        public SearchPathRoot Root;
        public string Path;
        public string RemoveFromFront;
        public string[] ExtensionsToInclude;
        public string[] EndingsToExclude;
    }

    // needs to be public to be serializable
    public class RBFDictEntry
    {
        public HashSet<string> Options = new HashSet<string>();
        public string Key;

        public RBFDictEntry(string key)
        {
            Key = key;
        }

        public RBFDictEntry()
        { }
    }

    public static class RBFDictionary
    {
        private const string XML_IDENTIFIER = "RBFDictionary";
        private static readonly Dictionary<string, RBFDictEntry> s_dictionary = new Dictionary<string, RBFDictEntry>();
        private static readonly Dictionary<string, HashSet<SearchPathInfo>> s_searchpaths = new Dictionary<string, HashSet<SearchPathInfo>>();

        public static void Init()
        {
            ModManager.ApplicationExit += ModManagerApplicationExit;
            string dictionaryPath = Directory.GetCurrentDirectory() + "\\rbf_dictionary.xml";
            if (File.Exists(dictionaryPath))
            {
                Stream dictfile = null;
                try
                {
                    dictfile = File.Open(dictionaryPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    LoggingManager.SendMessage("RBFDict - Reading RBF-Dictionary");
                    ReadFile(dictfile);
                }
                catch (Exception ex)
                {
                    LoggingManager.SendError("RBFDict - Could not read RBF-Dictionary");
                    LoggingManager.HandleException(ex);
                     UIHelper.ShowError("Could not read RBF-Dictionary!");
                }
                finally
                {
                    if (dictfile != null)
                        dictfile.Close();
                }
                return;
            }
            LoggingManager.SendWarning("RBFDict - Could not find RBF-Dictionary");

            string legacyFilePath = Directory.GetCurrentDirectory() + "\\rbf_dictionary.txt";
            if (!File.Exists(legacyFilePath))
            {
                LoggingManager.SendError("RBFDict - Could not find legacy RBF-Dictionary either");
                return;
            }
            StreamReader reader = null;

            try
            {
                reader = File.OpenText(legacyFilePath);
                LoggingManager.SendMessage("RBFDict - Reading RBF-Dictionary (legacy)");
                ReadFileLegacy(reader);
            }
            catch (Exception e)
            {
                LoggingManager.SendError("RBFDict - Could not read RBF-Dictionary (legacy)");
                LoggingManager.HandleException(e);
                 UIHelper.ShowError("Could not read RBF-Dictionary!");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// Returns the entries in the RBF-dictionary for a specified key or NULL if they key does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] GetDictEntries(string key)
        {
            if (!s_dictionary.ContainsKey(key))
                return new string[0];
            HashSet<string> set = s_dictionary[key].Options;
            return set.ToArray();
        }

        /// <summary>
        /// Returns the searchpaths for the specified key or NULL if the key does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static HashSet<SearchPathInfo> GetPaths(string key)
        {
            if (!s_searchpaths.ContainsKey(key))
                return null;
            return s_searchpaths[key];
        }

        /// <summary>
        /// Adds an entry to the RBF-dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="option"></param>
        public static void AddDictEntry(string key, string option)
        {
            if (!s_dictionary.ContainsKey(key))
                s_dictionary.Add(key, new RBFDictEntry(key));
            if (s_dictionary[key].Options.Contains(option))
                return;
            s_dictionary[key].Options.Add(option);
        }

        public static void AddDictEntry(string key, IEnumerable<string> options)
        {
            if (!s_dictionary.ContainsKey(key))
                s_dictionary.Add(key, new RBFDictEntry(key));
            foreach (string option in options)
            {
                if (s_dictionary[key].Options.Contains(option))
                    continue;
                s_dictionary[key].Options.Add(option);
            }
        }

        /// <summary>
        /// This function adds a searchpath to the dictionary given an input string describing the searchpath
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pathLine"></param>
        private static void AddSearchpath(string key, string pathLine)
        {
            string[] roughParts = pathLine.Split('?');
            if (roughParts.Length < 2)
                return;

            if (!s_searchpaths.ContainsKey(key))
                s_searchpaths.Add(key, new HashSet<SearchPathInfo>());
            var spi = new SearchPathInfo();
            string[] fineParts = roughParts[1].Split(':');
            spi.Root = fineParts[0].ToLower() == "data" ? SearchPathRoot.Data : SearchPathRoot.Attrib;

            spi.Path = fineParts[1];

            if (roughParts.Length > 2)
                spi.RemoveFromFront = roughParts[2] == string.Empty ? null : roughParts[2];
            else
                spi.RemoveFromFront = null;

            if (roughParts.Length > 3 && roughParts[3] != string.Empty)
            {
                string[] exts = roughParts[3].Split(':');
                spi.ExtensionsToInclude = exts;
            }
            else
                spi.ExtensionsToInclude = null;

            if (roughParts.Length > 4 && roughParts[4] != string.Empty)
            {
                string[] exclude = roughParts[4].Split(':');
                spi.EndingsToExclude = exclude;
            }
            else
                spi.EndingsToExclude = null;

            s_searchpaths[key].Add(spi);
        }

        public static bool HasSearchpath(string key)
        {
            return s_searchpaths.ContainsKey(key);
        }

        public static bool RemoveSearchpath(string key)
        {
            return s_searchpaths.Remove(key);
        }

        static void ReadFile(Stream stream)
        {
            var settings = new XmlReaderSettings{IgnoreWhitespace = true, IgnoreComments = true};
            XmlReader reader = XmlReader.Create(stream, settings);

            while (reader.MoveToContent() == XmlNodeType.Element)
            {
                if (reader.Name == "Dictionary")
                    ReadDictionary(reader);
                else if (reader.Name == "Searchpaths")
                    ReadSearchPaths(reader);
                reader.Read();
            }
        }

        static void ReadDictionary(XmlReader reader)
        {
            XmlSerializer xs = new XmlSerializer(typeof (RBFDictEntry));
            reader.Read();
            do
            {
                RBFDictEntry de = (RBFDictEntry) xs.Deserialize(reader);
                s_dictionary.Add(de.Key, de);
            } while (reader.IsStartElement() && reader.Name == "RBFDictEntry");
        }

        static void ReadSearchPaths(XmlReader reader)
        {
            XmlSerializer xs = new XmlSerializer(typeof (SearchPathInfo));
            reader.Read();
            do
            {
                reader.ReadToDescendant("Key");
                string key = reader.ReadElementContentAsString();

                reader.Read();
                var pathInfo = new HashSet<SearchPathInfo>();
                do
                {
                    var spi = (SearchPathInfo) xs.Deserialize(reader);
                    pathInfo.Add(spi);
                } while (reader.IsStartElement() && reader.Name == "SearchPathInfo");
                s_searchpaths.Add(key, pathInfo);
                reader.ReadEndElement();
                reader.ReadEndElement();
            } while (reader.IsStartElement() && reader.Name == "Entry");
        }

        static void ReadFileLegacy(StreamReader reader)
        {
            string s = reader.ReadLine();
            string currentKey = string.Empty;
            while (s != null)
            {
                if (s == string.Empty || s.StartsWith("//"))
                {
                    s = reader.ReadLine();
                    continue;
                }
                if (s.StartsWith('['))
                {
                    s = s.Remove(s.Length - 1, 1).Remove(0, 1);
                    currentKey = s;
                }
                else if (s.StartsWith('?'))
                {
                    AddSearchpath(currentKey, s);
                }
                else
                {
                    if (!s_dictionary.ContainsKey(currentKey))
                        s_dictionary.Add(currentKey, new RBFDictEntry(currentKey));
                    s_dictionary[currentKey].Options.Add(s);
                }
                s = reader.ReadLine();
            }
        }

        static void WriteFile(Stream stream)
        {
            var settings = new XmlWriterSettings {Indent = true};
            XmlWriter writer = XmlWriter.Create(stream, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement(XML_IDENTIFIER);
            WriteDictionary(writer);
            WriteSearchpaths(writer);


            writer.WriteFullEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        static void WriteDictionary(XmlWriter writer)
        {
            XmlSerializer xs = new XmlSerializer(typeof (RBFDictEntry));
            writer.WriteStartElement("Dictionary");
            foreach (var kvp in s_dictionary)
            {
                if (kvp.Value.Options.Count == 0)
                    continue;
                xs.Serialize(writer, kvp.Value);
            }
            writer.WriteFullEndElement();
        }

        static void WriteSearchpaths(XmlWriter writer)
        {
            XmlSerializer xs = new XmlSerializer(typeof (SearchPathInfo));
            writer.WriteStartElement("Searchpaths");
            foreach (var kvp in s_searchpaths)
            {
                if (kvp.Value.Count == 0)
                    continue;
                writer.WriteStartElement("Entry");
                writer.WriteElementString("Key", kvp.Key);
                writer.WriteStartElement("Paths");
                foreach (var path in kvp.Value)
                    xs.Serialize(writer, path);
                writer.WriteEndElement();
                writer.WriteFullEndElement();
            }
            writer.WriteFullEndElement();
        }

        static void ModManagerApplicationExit()
        {
            LoggingManager.SendMessage("RBFDict - Writing RBF-Dictionary");
            Stream dictWriter = null;
            try
            {
                string filePath = Directory.GetCurrentDirectory() + "\\rbf_dictionary.xml";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                dictWriter = File.Create(filePath);
                WriteFile(dictWriter);
            }
            catch (Exception e)
            {
                LoggingManager.SendError("RBFDict - Could not write RBF-Dictionary!");
                LoggingManager.HandleException(e);
                 UIHelper.ShowError("Could not write RBF-Dictionary! See logfile for further information.");
            }
            finally
            {
                if (dictWriter != null)
                    dictWriter.Close();
            }
        }
    }
}