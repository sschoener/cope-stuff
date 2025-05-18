#region

using System.Collections.Generic;
using System.IO;
using cope.Extensions;
using cope.IO.StreamExt;

#endregion

namespace cope.DawnOfWar2.RelicChunky.Chunks
{
    public class ACTNAction : IStreamExtBinaryCompatible, IGenericClonable<ACTNAction>
    {
        #region fields

        /// <summary>
        /// The parameters used for this action.
        /// </summary>
        private Dictionary<string, string> m_params;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the Delay of this action.
        /// </summary>
        public float Delay { get; set; }

        /// <summary>
        /// Gets the name of this action
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the length of the action in bytes.
        /// </summary>
        public int Length
        {
            get
            {
                int length = 12; // for the amount of items in the table, the length of the name + the delay
                length += Name.Length;
                foreach (KeyValuePair<string, string> kvp in m_params)
                {
                    length += 8; // for both lengths
                    length += kvp.Key.Length;
                    length += kvp.Value.Length;
                }
                return length;
            }
        }

        public Dictionary<string, string> Params
        {
            get { return m_params; }
        }

        #endregion

        #region ctors

        public ACTNAction()
        {
            m_params = new Dictionary<string, string>();
        }

        #endregion

        #region methods

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region IStreamExtBinaryCompatible<ACTNAction> Member

        public void WriteToStream(Stream str)
        {
            var bw = new BinaryWriter(str);
            WriteToStream(bw);
        }

        public void WriteToStream(BinaryWriter bw)
        {
            bw.Write(Name.Length);
            bw.Write(Name.ToByteArray(true));
            bw.Write(m_params.Count);
            foreach (KeyValuePair<string, string> kvp in m_params)
            {
                bw.Write(kvp.Key.Length);
                bw.Write(kvp.Key.ToByteArray(true));
                bw.Write(kvp.Value.Length);
                bw.Write(kvp.Value.ToByteArray(true));
            }
            bw.Write(Delay);
        }

        public void GetFromStream(Stream str)
        {
            var br = new BinaryReader(str);
            GetFromStream(br);
        }

        public void GetFromStream(BinaryReader br)
        {
            uint nLength = br.ReadUInt32();
            Name = br.ReadBytes((int) nLength).ToString(true);
            uint eCount = br.ReadUInt32();
            for (uint i = 0; i < eCount; i++)
            {
                nLength = br.ReadUInt32();
                string tmpName = (nLength == 0) ? string.Empty : br.ReadBytes((int) nLength).ToString(true);
                uint vLength = br.ReadUInt32();
                string tmpValue = (vLength == 0) ? string.Empty : br.ReadBytes((int) vLength).ToString(true);
                m_params.Add(tmpName, tmpValue);
            }
            if (!br.IsAtEnd())
                Delay = br.ReadSingle();
        }

        #endregion

        #region IGenericClonable<ACTNAction> Member

        public ACTNAction GClone()
        {
            var tmp = new ACTNAction
                          {
                              Delay = Delay,
                              Name = Name,
                              m_params = new Dictionary<string, string>(m_params)
                          };
            return tmp;
        }

        #endregion
    }
}