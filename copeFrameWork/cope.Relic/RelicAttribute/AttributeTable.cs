#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.Relic.RelicAttribute
{
    public class AttributeTable : IEnumerable<AttributeValue>, IGenericClonable<AttributeTable>
    {
        protected readonly List<AttributeValue> m_values;

        public AttributeTable()
        {
            m_values = new List<AttributeValue>();
        }

        public virtual AttributeValueType Value
        {
            get { return AttributeValueType.Table; }
        }

        #region children

        public virtual bool AddValue(AttributeValue value)
        {
            if (!m_values.Contains(value))
            {
                if (value.Parent != null)
                    value.Parent.RemoveValue(value);
                value.SetParent(this);
                m_values.Add(value);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            foreach (var child in m_values)
                child.SetParent(null);
            m_values.Clear();
        }

        public bool Contains(AttributeValue value)
        {
            return m_values.Contains(value);
        }

        public bool RemoveValue(AttributeValue value)
        {
            if (m_values.Remove(value))
            {
                if (value.Parent != null)
                    value.SetParent(null);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a copy of the list containing the children of this AttributeTable.
        /// </summary>
        /// <returns></returns>
        public List<AttributeValue> GetValues()
        {
            return new List<AttributeValue>(m_values);
        }

        #endregion

        #region properties

        public AttributeValue Owner { get; internal set; }

        public int ChildCount
        {
            get { return m_values.Count; }
        }

        #endregion

        #region IEnumerable<AttributeValue> Members

        public IEnumerator<AttributeValue> GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        #endregion

        #region IGenericClonable<AttributeTable> Members

        public virtual AttributeTable GClone()
        {
            AttributeTable table = new AttributeTable();
            foreach (var attributeValue in m_values)
                table.AddValue(attributeValue.GClone());
            return table;
        }

        #endregion

        public override string ToString()
        {
            return "Attrib Table, " + ChildCount + " entries";
        }
    }
}