#region

using System;
using System.Collections.Generic;

#endregion

namespace cope.Relic.RelicChunky.ChunkTypes.GameDataChunk
{
    public class RGDDictionary : IRGDKeyConverter
    {
        public readonly Dictionary<ulong, string> HashDict;
        public readonly Dictionary<string, ulong> HashedStrings;
        private readonly HashSet<ulong> m_unknownHashes;

        public RGDDictionary(Dictionary<ulong, string> dict)
        {
            HashDict = new Dictionary<ulong, string>(dict);
            HashedStrings = new Dictionary<string, ulong>();
            foreach (var kvp in HashDict)
                HashedStrings[kvp.Value] = kvp.Key;
            m_unknownHashes = new HashSet<ulong>();
        }

        #region IRGDKeyConverter Members

        public string HashToKey(ulong hash)
        {
            string key;
            if (HashDict.TryGetValue(hash, out key))
                return key;
            if (!m_unknownHashes.Contains(hash))
            {
                m_unknownHashes.Add(hash);
                if (OnUnknownHash != null)
                    OnUnknownHash(hash);
            }
                
            return "0x" + hash.ToString("X2");
        }

        public ulong KeyToHash(string key)
        {
            if (key.StartsWith("0x"))
            {
                string newKey = key.ToLowerInvariant().Substring(2);
                return uint.Parse(newKey, System.Globalization.NumberStyles.HexNumber);
            }
            ulong hash;
            if (HashedStrings.TryGetValue(key, out hash))
                return hash;
            hash = RGDHasher.ComputeHash(key);
            if (!HashDict.ContainsKey(hash))
            {
                HashDict.Add(hash, key);
                if (OnNewKey != null)
                    OnNewKey(hash, key);
            }
                
            return hash;
        }

        #endregion

        public void AddEntry(ulong hash, string name)
        {
            if (HashDict.ContainsKey(hash))
                return;
            HashDict[hash] = name;
            HashedStrings[name] = hash;
            if (OnNewKey != null)
                OnNewKey(hash, name);
        }

        public event Action<ulong, string> OnNewKey;

        public event Action<ulong> OnUnknownHash;

        public IEnumerable<ulong> UnknownHashes
        {
            get { return m_unknownHashes; }
        }

        public int NumHashes
        {
            get { return HashDict.Count; }
        }
    }
}