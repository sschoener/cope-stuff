#region

using System;
using System.Collections.Generic;
using System.IO;
using cope.Extensions;

#endregion

namespace cope.Relic.RelicChunky.ChunkTypes.GameDataChunk
{
    /// <summary>
    /// Helper class to read RGD dictionaries.
    /// </summary>
    public static class RGDDictionaryReader
    {
        public static Dictionary<ulong, string> Read(TextReader tr)
        {
            var dict = new Dictionary<ulong, string>();
            while (true)
            {
                string line = tr.ReadLine();
                if (line == null)
                    break;
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (line.StartsWith("#"))
                    continue;
                string hashStr = line.SubstringBeforeFirst('=').Trim();
                ulong hash = Convert.ToUInt64(hashStr, 16);
                string key = line.SubstringAfterLast('=').Trim();
                if (!dict.ContainsKey(hash))
                    dict.Add(hash, key);
            }
            return dict;
        }
    }

    /// <summary>
    /// Helper class to write RGD dictionaries.
    /// </summary>
    public static class RGDDictionaryWriter
    {
        public static void Write(TextWriter tw, Dictionary<ulong, string> dict)
        {
            tw.WriteLine("# RGD Dict");
            foreach (var kvp in dict)
                tw.WriteLine("0x" + kvp.Key.ToString("X16") + "=" + kvp.Value);
            tw.Flush();
        }

        public static void Write(TextWriter tw, RGDDictionary dict)
        {
            Write(tw, dict.HashDict);
        }
    }
}