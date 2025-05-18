#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace cope.DawnOfWar2.RelicChunky.Chunks
{
    public class ACTNChunk : DataChunk, IEnumerable<ACTNAction>
    {
        #region fields

        protected Dictionary<ACTNAction, ACTNAction> m_actions = new Dictionary<ACTNAction, ACTNAction>();

        #endregion

        #region ctors

        public ACTNChunk(byte[] rawData)
        {
            m_rawData = rawData;
        }

        public ACTNChunk(Stream str)
            : base(str)
        {
        }

        public ACTNChunk(BinaryReader br)
            : base(br)
        {
        }

        #endregion

        #region methods

        public override void InterpretRawData()
        {
            var ms = new MemoryStream(m_rawData);
            var br = new BinaryReader(ms);

            m_actions.Clear();
            uint numOfTables = br.ReadUInt32();
            for (uint i = 0; i < numOfTables; i++)
            {
                var tmp = new ACTNAction();
                tmp.GetFromStream(br);
                m_actions.Add(tmp, tmp);
            }
        }

        public int GetLength()
        {
            return 4 + m_actions.Values.Sum(act => act.Length);
        }

        #endregion

        #region properties

        public Dictionary<ACTNAction, ACTNAction> Actions
        {
            get { return m_actions; }
        }

        #endregion

        #region IStreamExtBinaryCompatible<ACTNChunk> Member

        public override void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public override void WriteToStream(BinaryWriter bw)
        {
            /*long baseOffset = bw.BaseStream.Position;
            bw.BaseStream.Position += _header.Length;*/

            bw.Write(m_actions.Count);
            foreach (ACTNAction actn in m_actions.Keys)
            {
                actn.WriteToStream(bw);
            }

            /*_header.ChunkSize = (uint)(bw.BaseStream.Position - baseOffset);
            bw.BaseStream.Position = baseOffset;
            _header.WriteToStream(bw);*/
        }

        public override void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        public override void GetFromStream(BinaryReader br)
        {
            base.GetFromStream(br);
            InterpretRawData();
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,Dictionary<string,string>>> Member

        public IEnumerator<ACTNAction> GetEnumerator()
        {
            return m_actions.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_actions.Values.GetEnumerator();
        }

        #endregion

        /*
         * FORMAT INFO:
         * 
         * 0x04 uint - action Count
         * Rest: Actions
         *
         * ACTNAction:
         * 0x04 uint - Length of action name
         * 0x^^ ascii string - action name
         * 0x04 uint - Number of Key-Value entries in action
         * Rest: KV-Entries
         * 0x04 float - delay of the action
         *
         * KV-Entry:
         * 0x04 uint Length of Key
         * 0x^^ ascii string - Key
         * 0x04 uint Length of Value
         * 0x^^ ascii string - Value
         * 
         */
    }
}