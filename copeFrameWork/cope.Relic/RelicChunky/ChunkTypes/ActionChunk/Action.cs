#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Relic.RelicChunky.ChunkTypes.ActionChunk
{
    public class Action : IGenericClonable<Action>, IEnumerable<KeyValuePair<string, string>>
    {
        #region fields

        /// <summary>
        /// The parameters used for this action.
        /// </summary>
        private readonly Dictionary<string, string> m_params;

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

        public Dictionary<string, string> Params
        {
            get { return m_params; }
        }

        #endregion

        #region ctors

        public Action(string name, float delay, Dictionary<string, string> parameters)
        {
            Name = name;
            Delay = delay;
            m_params = new Dictionary<string, string>(parameters);
        }

        public Action(string name)
        {
            Name = name;
            m_params = new Dictionary<string, string>();
        }

        #endregion

        #region methods

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return m_params.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region IGenericClonable<ACTNAction> Member

        public Action GClone()
        {
            var tmp = new Action(Name, Delay, m_params);
            return tmp;
        }

        #endregion
    }
}