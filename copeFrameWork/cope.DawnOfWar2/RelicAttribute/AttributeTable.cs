#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace cope.DawnOfWar2.RelicAttribute
{
    public sealed class AttributeTable : IEnumerable<AttributeValue>, IGenericClonable<AttributeTable>
    {
        private readonly List<AttributeValue> m_values;
        private AttributeValue m_owner;

        public AttributeTable()
        {
            m_values = new List<AttributeValue>();
        }

        #region children

        public bool AddValue(AttributeValue value)
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

        #endregion

        #region properties

        public AttributeValue Owner
        {
            get { return m_owner; }
            internal set { m_owner = value; }
        }

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

        public AttributeTable GClone()
        {
            AttributeTable table = new AttributeTable();
            foreach (var attributeValue in m_values)
            {
                table.AddValue(attributeValue.GClone());
            }
            return table;
        }

        #endregion
    }
}