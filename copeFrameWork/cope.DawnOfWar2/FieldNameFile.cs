#region

using System;
using System.Collections.Generic;
using System.IO;
using cope.DawnOfWar2.RelicBinary;
using cope.Extensions;

#endregion

namespace cope.DawnOfWar2
{
    // used to read FLB-files
    public class FieldNameFile : UniFile, IRBFKeyProvider
    {
        private readonly List<string> m_sNewKeys;
        private Dictionary<string, int> m_keyToIndex;
        private string[] m_sKeys;

        public FieldNameFile(string path)
            : base(path, FileAccess.Read, FileShare.Read)
        {
            m_sNewKeys = new List<string>();
        }

        public FieldNameFile(UniFile file) : base(file)
        {
            m_sNewKeys = new List<string>();
        }

        #region IRBFKeyProvider Members

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public string GetKeyByIndex(int index)
        {
            if (m_sKeys.Length > index)
                return m_sKeys[index];

            int idx = index - m_sKeys.Length;
            if (idx < m_sNewKeys.Count)
                return m_sNewKeys[idx];
            throw new Exception("Trying to get key for index " + index + " but the highest available index is " +
                                (m_sKeys.Length - 1));
        }

        /// <summary>
        /// Gets the index for a specified key. Returns -1 if there's no such key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetIndexForKey(string key)
        {
            int index;
            if (m_keyToIndex.TryGetValue(key, out index))
                return index;
            return -1;
        }

        /// <summary>
        /// Adds a new key and returns the index.
        /// </summary>
        /// <returns></returns>
        public int AddKey(string key)
        {
            int index = GetIndexForKey(key);
            if (index != -1)
                return index;
            index = m_sKeys.Length + m_sNewKeys.Count;
            m_sNewKeys.Add(key);
            m_keyToIndex.Add(key, index);
            return index;
        }

        public bool NeedsUpdate()
        {
            return m_sNewKeys.Count > 0;
        }

        public void Update()
        {
            WriteDataTo(m_filePath);
        }

        #endregion

        protected override void Read(Stream stream)
        {
            var br = new BinaryReader(stream);

            var numKeys = (int) br.ReadUInt32();

            long baseOffset = stream.Position;
            int[] offsets = new int[numKeys];
            for (int i = 0; i < numKeys; i++)
            {
                offsets[i] = (int) br.ReadUInt32();
            }

            m_keyToIndex = new Dictionary<string, int>(numKeys);
            m_sKeys = new string[numKeys];
            for (int i = 0; i < numKeys; i++)
            {
                stream.Position = baseOffset + offsets[i];
                string key = br.ReadCString();
                m_sKeys[i] = key;
                m_keyToIndex.Add(key, i);
            }
        }

        protected override void Write(Stream stream)
        {
            var bw = new BinaryWriter(stream);
            MemoryStream offsets = new MemoryStream();
            BinaryWriter offsetWriter = new BinaryWriter(offsets);
            MemoryStream keys = new MemoryStream();
            BinaryWriter keyWriter = new BinaryWriter(keys);

            // first: number of keys in this FLB file
            uint numKeys = (uint) (m_sKeys.Length + m_sNewKeys.Count);
            bw.Write(numKeys);

            // update string array
            if (m_sKeys != null)
                m_sKeys = m_sKeys.Append(m_sNewKeys.ToArray());
            else
                m_sKeys = m_sNewKeys.ToArray();
            m_sNewKeys.Clear();

            // offset array and key array are written simultanously
            uint offset = numKeys * sizeof (uint);
            foreach (string s in m_sKeys)
            {
                offsetWriter.Write(offset);
                keyWriter.Write(s.ToByteArray(true));
                keyWriter.Write(false); // zero terminated string
                offset += (uint) s.Length + 0x1;
            }
            offsets.Flush();
            keys.Flush();
            offsets.WriteTo(stream);
            keys.WriteTo(stream);
            offsets.Close();
            keys.Close();
        }
    }
}