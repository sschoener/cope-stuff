using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cope.Extensions;
using cope.FileSystem;
using cope.Relic.RelicAttribute;
using cope.Relic.RelicChunky;
using cope.Relic.RelicChunky.ChunkTypes.GameDataChunk;
using cope.Relic.RelicChunky.ChunkTypes.KeysChunk;

namespace RGDConv
{
    class Program
    {
        private static readonly HashSet<Tuple<ulong, string>> s_newHashes = new HashSet<Tuple<ulong, string>>();
        private static readonly HashSet<Tuple<ulong, string>> s_unknownHashes = new HashSet<Tuple<ulong, string>>();

        private static string s_strHashFile;
        private static string s_strInput;
        private static string s_strOutput;
        private static bool s_bUseXml;
        private static bool s_bUseJSON;
        private static bool s_bUseText;
        private static uint s_iVersion = 1;
        private static bool s_bIsFileMode;

        private static string s_currentPath;

        private static RGDDictionary s_rgdDict;

        static void Main(string[] args)
        {
            //args = new[] {"hash_dict.txt", "./", "./", "-j"};
            Console.WriteLine("cope's RGDConv v1.3 - 04/30/17");
            // parse arguments
            if (args.Length < 3)
            {
                Console.Error.WriteLine("Usage: rgdConv.exe <hashdict> <inputdir> <outputdir> [-x] [-r|-t|-j]");
                Console.Error.WriteLine("-x   convert to/from XML instead of text");
                Console.Error.WriteLine("-r   convert RGDs to text (/XML)");
                Console.Error.WriteLine("-t   convert text (/XML) to RGD");
                Console.Error.WriteLine("-j   convert RGDs to JSON (no RGD to JSON available!)");
                Console.Error.WriteLine("-d   use DoW3 RGD version");
                Console.Error.WriteLine("if none of the above is specified, both conversions will be applied");
                return;
            }
            s_strHashFile = Path.GetFullPath(args[0]);
            s_strInput = Path.GetFullPath(args[1]);
            s_strOutput = Path.GetFullPath(args[2]);
            bool onlyText = false;
            bool onlyRgds = false;
            s_bUseXml = false;
            
            if (args.Length > 3)
            {
                var slice = args.Slice(3);

                if (slice.Contains("-d"))
                    s_iVersion = 2;

                if (slice.Contains("-r"))
                    onlyRgds = true;
                else if (slice.Contains("-t"))
                    onlyText = true;


                if (slice.Contains("-x"))
                    s_bUseXml = true;
                else if (slice.Contains("-j"))
                {
                    onlyRgds = true;
                    onlyText = false;
                    s_bUseJSON = true;
                }
                else
                    s_bUseText = true;
            }

            // check directories & files exist etc.
            if (!ValidateArguments())
                return;

            // Read RGD dictionary
            if (!ReadHashDict())
                return;

            int numFiles = 0;
            int numSuccess = 0;
            string textExt = s_bUseXml ? ".xml" : (s_bUseJSON ? ".json" : ".txt");
            if (s_bIsFileMode)
            {
                var file = new RealFileDescriptor(s_strInput);
                numFiles = 1;
                if (ConvertFile(file, textExt))
                    numSuccess = 1;
            } else
            {
                // crawl through the files
                LocalFileSystem lfs = new LocalFileSystem(s_strInput);
                FileWalker fw = new FileWalker(lfs.GetRoot());
                IEnumerable<IFileDescriptor> files = fw;

                if (onlyText)
                    files = fw.Where(f => f.GetPath().EndsWith(textExt));
                else if (onlyRgds)
                    files = fw.Where(f => f.GetPath().EndsWith(".rgd"));
                else
                    files = fw.Where(f => f.GetPath().EndsWithAny(".rgd", textExt));
                files = files.Where(f => f.GetSize() > 0);


                foreach (var file in files)
                {
                    numFiles++;
                    if (ConvertFile(file, textExt))
                        numSuccess++;
                }
            }

            // has the RGD-dict been changed?
            if (s_newHashes.Count != 0)
            {
                Console.WriteLine("Updating RGD dictionary...");
                UpdateHashDict();
            }
                

            // any unknown hashes?
            if (s_unknownHashes.Count != 0)
                UpdateUnknownHashes();
            Console.WriteLine("Successfully converted " + numSuccess + " of " + numFiles + " files.");
        }

        static void rgdDict_OnUnknownHash(ulong hash)
        {

            if (!s_unknownHashes.Any(t => t.Item1 == hash))
            {
                var t = new Tuple<ulong, string>(hash, s_currentPath);
                s_unknownHashes.Add(t);
            }
                
        }

        static void rgdDict_OnNewKey(ulong hash, string key)
        {
            var tp = new Tuple<ulong, string>(hash, key);
            if (!s_newHashes.Contains(tp))
                s_newHashes.Add(tp);
        }

        static bool ValidateArguments()
        {
            if (!File.Exists(s_strHashFile))
            {
                Console.Error.WriteLine("The specified RGD-hash dictionary does not exist.");
                return false;
            }

            if (File.Exists(s_strInput))
            {
                s_bIsFileMode = true;
                return true;
            }
                

            if (!Directory.Exists(s_strInput))
            {
                Console.Error.WriteLine("Input directory does not exist");
                return false;
            }

            if (!Directory.Exists(s_strOutput))
            {
                try
                {
                    Directory.CreateDirectory(s_strOutput);
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

        static bool ReadHashDict()
        {
            TextReader hashReader = null;
            try
            {
                hashReader = File.OpenText(s_strHashFile);
                s_rgdDict = new RGDDictionary(RGDDictionaryReader.Read(hashReader));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to read RGD-hash dictionary!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return false;
            }
            finally
            {
                if (hashReader != null)
                    hashReader.Close();
            }
            s_rgdDict.OnNewKey += rgdDict_OnNewKey;
            s_rgdDict.OnUnknownHash += rgdDict_OnUnknownHash;
            return true;
        }

        static void UpdateHashDict()
        {
            StreamWriter hashDictStr = null;
            try
            {
                hashDictStr = File.AppendText(s_strHashFile);
                foreach (var tp in s_newHashes)
                    hashDictStr.WriteLine("0x" + tp.Item1.ToString("X16") + "=" + tp.Item2);
                hashDictStr.Flush();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to update RGD dictionary with new keys.");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
            }
            finally
            {
                if (hashDictStr != null)
                    hashDictStr.Close();
            }
            
        }

        static void UpdateUnknownHashes()
        {
            FileStream hashDictStr = null;
            try
            {
                var hashes = new HashSet<Tuple<ulong, string>>();
                if (File.Exists("unknown_hashes.txt"))
                {
                    string[] lines = File.ReadAllLines("unknown_hashes.txt");
                    hashes.AddMultiple(lines.Map(x => new Tuple<ulong, string>(Convert.ToUInt64(x.SubstringBeforeFirst('#').Trim(), 16), string.Empty)));
                }
                bool changes = false;
                foreach (var tuple in s_unknownHashes)
                {
                    ulong hash = tuple.Item1;
                    if (!hashes.Any(t => t.Item1 == hash))
                    {
                        changes = true;
                        hashes.Add(tuple);
                    }
                }
                if (!changes)
                    return;
                Console.WriteLine("Updating unknown hashes...");
                hashDictStr = File.Open("unknown_hashes.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                StreamWriter sw = new StreamWriter(hashDictStr);
                foreach (var tuple in hashes)
                {
                    string toWrite = "0x" + tuple.Item1.ToString("X8");
                    if (!string.IsNullOrEmpty(tuple.Item2))
                        toWrite += " # " + tuple.Item2;
                    sw.WriteLine(toWrite);
                }
                    
                sw.Flush();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to write out unknown hashes.");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
            }
            finally
            {
                if (hashDictStr != null)
                    hashDictStr.Close();
            }

        }

        static bool ConvertFile(IFileDescriptor file, string textExt)
        {
            string path = file.GetPath();
            string directory = path.SubstringBeforeLast('\\');
            s_currentPath = path;

            // create directory if necessary
            if (path != directory)
            {
                string outDir = Path.Combine(s_strOutput, directory);
                if (!Directory.Exists(outDir))
                {
                    try
                    {
                        Directory.CreateDirectory(outDir);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine("Failed to create directory " + outDir);
                        Console.Error.WriteLine(ex.GetInfo().Collapse());
                        return false;
                    }
                }
            }

            // now for the actual conversion
            Stream stream = null;
            try
            {
                stream = file.Open();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to open " + Path.Combine(s_strInput, path));
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                if (stream != null)
                    stream.Close();
                return false;
            }

            string outpath = s_strOutput;
            if (!s_bIsFileMode) { 
                string pathProperExt;
                if (path.EndsWith(".rgd"))
                    pathProperExt = path.SubstringBeforeLast('.') + textExt;
                else
                    pathProperExt = path.SubstringBeforeLast('.') + ".rgd";
                outpath = Path.Combine(s_strOutput, pathProperExt);
            }
            if (path.EndsWith(textExt))
            {
                AttributeStructure attrib;
                if (s_bUseXml)
                    attrib = Read(stream, AttributeXmlReader.Read);
                else
                    attrib = Read(stream, CorsixStyleConverter.ReadFirst);
                if (attrib != null)
                    WriteRGD(attrib.Root.Data as AttributeTable, outpath, s_rgdDict);
            }
            else if (path.EndsWith(".rgd"))
            {
                AttributeStructure attrib = ReadRGD(stream, s_rgdDict);
                if (attrib == null)
                    return false;
                if (s_bUseXml)
                    Write(attrib, outpath, AttributeXmlWriter.Write);
                else if (s_bUseJSON)
                    Write(attrib, outpath, AttributeJSONWriter.Write);
                else
                    Write(attrib, outpath, CorsixStyleConverter.Write);
            }
            return true;
        }

        private static byte[] GetKeyChunkData(AttributeTable table)
        {
            var keys = AttributeIterator.DoForAll(table, new CollectKeys());
            Dictionary<ulong, string> keyHashes = new Dictionary<ulong, string>();
            foreach (var key in keys)
                keyHashes[s_rgdDict.KeyToHash(key)] = key;
            using (var ms = new MemoryStream()) {
                KeysWriter.Write(ms, keyHashes);
                ms.Flush();
                var buffer = ms.GetBuffer();
                int pos = (int)ms.Position;
                var keyData = new byte[pos];
                Array.Copy(buffer, keyData, pos);
                return keyData;
            }
        }

        private class CollectKeys : IAttributeProcessor<string>
        {
            public string Process(AttributeValue data)
            {
                return data.Key;
            }
        }

        static void WriteRGD(AttributeTable table, string path, IRGDKeyConverter hashDict)
        {
            FileStream outStream = null;
            try
            {
                outStream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read);
                byte[] rgdData;
                using (var ms = new MemoryStream())
                {
                    RGDWriter.Write(ms, table, hashDict, s_iVersion);
                    ms.Flush();
                    rgdData = ms.ToArray();
                }
                DataChunk rgdChunk = new DataChunk(string.Empty, "AEGD", rgdData);
                if (s_iVersion == 1)
                    ChunkyFileWriter.Write(outStream, rgdChunk, ChunkWriter.CompanyOfHeroes2Chunk);
                else if (s_iVersion == 2)
                {
                    byte[] keyData = GetKeyChunkData(table);
                    DataChunk keyChunk = new DataChunk(string.Empty, "KEYS", keyData);
                    var keyChunkInfo = new ChunkWriter.ChunkInfo()
                    {
                        Version = 1
                    };
                    ChunkyFileWriter.Write(outStream, new[] { rgdChunk, keyChunk }, new[] { ChunkWriter.DawnOfWar3Chunk, keyChunkInfo });
                }
                
                outStream.Flush();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to write file as RGD: " + path);
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }
            finally
            {
                if (outStream != null)
                    outStream.Close();
            }
        }

        static AttributeStructure Read(Stream str, Func<Stream, AttributeStructure> reader)
        {
            try
            {
                return reader(str);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to read file in Corsix' Style Syntax/XML!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return null;
            }
            finally
            {
                str.Close();
            }
        }

        static AttributeStructure ReadRGD(Stream str, IRGDKeyConverter hashDict)
        {
            try
            {
                var chunks = ChunkyFileReader.Read(str);

                if (chunks.Count > 1)
                {
                    var keyChunk = (DataChunk)chunks[1];
                    Dictionary<ulong, string> newKeys;
                    using (var ms = new MemoryStream(keyChunk.GetData()))
                        newKeys = KeysReader.Read(ms);
                    foreach (var kvp in newKeys)
                        s_rgdDict.AddEntry(kvp.Key, kvp.Value);
                }

                DataChunk chunk = (DataChunk) chunks[0];
                AttributeStructure attrib;
                using (var ms = new MemoryStream(chunk.GetData()))
                    attrib = RGDReader.Read(ms, hashDict, chunk.OriginalHeader.Version);
                return attrib;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to read file as RGD");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return null;
            }
            finally
            {
                str.Close();
            }
        }

        static void Write(AttributeStructure attrib, string path, Action<Stream, AttributeStructure> writer)
        {

            FileStream outStream = null;
            try
            {
                outStream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read);
                writer(outStream, attrib);
                outStream.Flush();
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Failed to write file in Corsix Style Syntax/XML");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }
            finally
            {
                if (outStream != null)
                    outStream.Close();
            }
        }
    }
}
