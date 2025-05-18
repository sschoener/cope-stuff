using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cope.Extensions;
using cope.FileSystem;
using cope.Relic.RelicAttribute;
using cope.Relic.RelicChunky;
using cope.Relic.RelicChunky.ChunkTypes.GameDataChunk;

namespace RGDCrawler
{
    class Program
    {
        private static string s_sHashDict;
        private static string s_sInputDir;
        private static string s_sOutputDir;
        private static bool s_bCollectAll;

        static void Main(string[] args)
        {
            Console.WriteLine("cope's RGD crawler v1.0 - 12/19/12");
            if (args.Length < 3)
            {
                Console.Error.WriteLine("Usage: rgdCrawl.exe <hashdict> <inputdir> <outputdir> [-a]");
                Console.Error.WriteLine("-a   dump all occurences of tables with $REF, " +
                                        "even if there already is a table of that kind");
                return;
            }

            s_sHashDict = Path.GetFullPath(args[0]);
            s_sInputDir = Path.GetFullPath(args[1]);
            s_sOutputDir = Path.GetFullPath(args[2]);
            if (!CheckDirectories())
                return;
            s_bCollectAll = args.Length > 3 && args[3] == "-a";

            RGDDictionary dict = ReadDictionary();
            if (dict == null)
                return;
            var results = CrawlFiles(dict);
            int numDumped = DumpResults(results, dict);
            Console.WriteLine("Dumped " + numDumped + " entries!");
        }

        private static bool CheckDirectories()
        {
            if (!File.Exists(s_sHashDict))
            {
                Console.Error.WriteLine("Specified hash dictionary does not exist.");
                return false;
            }
            if (!Directory.Exists(s_sInputDir))
            {
                Console.Error.WriteLine("Input directory does not exist.");
                return false;
            }
            if (!Directory.Exists(s_sOutputDir))
            {
                try
                {
                    Directory.CreateDirectory(s_sOutputDir);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Failed to create output directory.");
                    Console.Error.WriteLine(ex.GetInfo().Collapse());
                    return false;
                }
            }
            return true;
        }

        private static RGDDictionary ReadDictionary()
        {
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(s_sHashDict);
                var dict = RGDDictionaryReader.Read(sr);
                return new RGDDictionary(dict);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Failed to read hash dictionary.");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return null;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        private static Dictionary<string,List<AttributeValue>> CrawlFiles(RGDDictionary dict)
        {
            LocalFileSystem lfs = new LocalFileSystem(s_sInputDir);
            FileWalker fw = new FileWalker(lfs.GetRoot());
            var files = fw.Where(f => f.GetSize() > 0 && f.GetName().EndsWith(".rgd"));

            RefCollector collector = new RefCollector(s_bCollectAll);
            foreach (var f in files)
            {
                Stream str = null;

                AttributeStructure attrib;
                try
                {
                    str = f.Open();
                    attrib = RGDFileHelper.ReadRGD(str, dict);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Could not open file " + f.GetPath());
                    Console.Error.WriteLine(ex.GetInfo().Collapse());
                    if (str != null)
                        str.Close();
                    continue;
                }
                AttributeIterator.DoForAll(attrib.Root, collector);
            }

            return collector.Dictionary;
        }

        private static int DumpResults(Dictionary<string, List<AttributeValue>> results, RGDDictionary dict)
        {
            int counter = 0;
            foreach (var kvp in results)
            {
                string key = kvp.Key;
                string path = Path.Combine(s_sOutputDir, key);
                if (key.Contains('.'))
                {
                    string newPath = path.SubstringBeforeLast('.');
                    path = Path.Combine(s_sOutputDir, newPath);
                }
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                int i = 0;
                foreach (var attribVal in kvp.Value)
                {
                    string outpath = path + (i > 0 ? "_" + i : "") + ".rgd";
                    Stream fs = null;

                    var table = attribVal.Data as AttributeTable;
                    try
                    {
                        fs = File.Open(outpath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                        AttributeTable wrap = new AttributeTable();
                        wrap.AddValue(attribVal);
                        RGDFileWriter.WriteRGD(wrap, fs, dict, ChunkWriter.CompanyOfHeroes2Chunk);
                        fs.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine("Failed to save result to " + outpath);
                        Console.Error.WriteLine(ex.GetInfo().Collapse());
                        continue;
                    }
                    finally
                    {
                        if (fs != null)
                            fs.Close();
                    }
                    if (table.ChildCount == 1)
                    {
                        // prevent mass dumps of trivial tables
                        break;
                    }
                        
                    i++;
                    counter++;
                }
            }
            return counter;
        }
    }

    class RefCollector : IAttributeProcessor
    {
        private readonly Dictionary<string, List<AttributeValue>> m_dict;
        private readonly bool m_bAllowDuplicates;

        public RefCollector(bool allowDuplicates)
        {
            m_bAllowDuplicates = allowDuplicates;
            m_dict = new Dictionary<string, List<AttributeValue>>();
        }

        public Dictionary<string,List<AttributeValue>> Dictionary
        {
            get { return m_dict; }
        }

        #region Implementation of IAttributeProcessor

        public void Process(AttributeValue data)
        {
            if (data.DataType != AttributeValueType.Table)
                return;
            AttributeTable table = data.Data as AttributeTable;
            if (table == null)
                return;

            foreach (var v in table)
            {
                if (v.DataType != AttributeValueType.String)
                    continue;
                if (v.Key == "$REF")
                {
                    string path = v.Data as string;
                    if (string.IsNullOrEmpty(path))
                        continue;

                    List<AttributeValue> matches;
                    if (!m_dict.TryGetValue(path, out matches))
                    {
                        matches = new List<AttributeValue>();
                        m_dict.Add(path, matches);
                    }
                    else if (!m_bAllowDuplicates)
                        continue;
                    matches.Add(data);
                }
            }
        }

        #endregion
    }
}

