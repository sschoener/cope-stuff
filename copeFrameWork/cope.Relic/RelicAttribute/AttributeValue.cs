#region

using System;
using System.Collections;
using System.Collections.Generic;
using cope.Extensions;

#endregion

namespace cope.Relic.RelicAttribute
{
    ///<summary>
    /// This class represents a single value within a RelicAttribute structure.
    /// It always contains a Key, a Value (Data) and a DataType.
    ///</summary>
    public sealed class AttributeValue : IGenericClonable<AttributeValue>, IEnumerable<AttributeValue>,
                                         IComparable<AttributeValue>
    {
        private object m_data;
        private AttributeValueType m_dataType;

        ///<summary>
        /// Constructs a new AttributeValue given a type, a key and some data.
        ///</summary>
        ///<param name="type"></param>
        ///<param name="key"></param>
        ///<param name="data"></param>
        public AttributeValue(AttributeValueType type, string key, object data)
        {
            Key = key;
            m_dataType = type;
            Data = data;
        }

        ///<summary>
        /// Constructs a new AttributeValue given a key and data. Tries to deduct the type of data and sets
        /// the DataType accordingly.
        ///</summary>
        ///<param name="key"></param>
        ///<param name="data"></param>
        public AttributeValue(string key, object data)
            : this(GetTypeForData(data), key, data)
        {
        }

        #region IComparable<AttributeValue> Members

        public int CompareTo(AttributeValue other)
        {
            if (other == null)
                return 1;
            return Key.CompareTo(other.Key);
        }

        #endregion

        #region IGenericClonable<AttributeValue> Members

        /// <summary>
        /// Returns a deep-copy of this instance of AttributeValue.
        /// </summary>
        /// <returns></returns>
        public AttributeValue GClone()
        {
            object data = Data;
            if (data is AttributeTable)
                data = (Data as AttributeTable).GClone();
            return new AttributeValue(DataType, Key, data);
        }

        #endregion

        #region Statics

        ///<summary>
        /// Checks whether the specified Data-object is of the specified type.
        ///</summary>
        ///<param name="data"></param>
        ///<param name="type"></param>
        ///<returns></returns>
        public static bool IsOfRightType(object data, AttributeValueType type)
        {
            switch (type)
            {
                case AttributeValueType.Boolean:
                    if (data is bool)
                        return true;
                    break;
                case AttributeValueType.Float:
                    if (data is float)
                        return true;
                    break;
                case AttributeValueType.Integer:
                    if (data is int)
                        return true;
                    break;
                case AttributeValueType.String:
                    if (data is string)
                        return true;
                    break;
                case AttributeValueType.Table:
                    if (data is AttributeTable)
                        return true;
                    break;
                case AttributeValueType.List:
                    if (data is AttributeList)
                        return true;
                    break;
            }
            return false;
        }

        ///<summary>
        /// Returns the AttributeDataType suitable for the specified Data-object.
        ///</summary>
        ///<param name="data"></param>
        ///<returns></returns>
        public static AttributeValueType GetTypeForData(object data)
        {
            if (data is bool)
                return AttributeValueType.Boolean;
            if (data is float)
                return AttributeValueType.Float;
            if (data is int)
                return AttributeValueType.Integer;
            if (data is string)
                return AttributeValueType.String;
            if (data is AttributeList)
                return AttributeValueType.List;
            if (data is AttributeTable)
                return AttributeValueType.Table;
            return AttributeValueType.Invalid;
        }

        /// <summary>
        /// Tries to convert the given string to a proper Data-object using the specified AttributeDataType. Throws an exception if it fails
        /// to convert the value or returns null there's no suitable conversion available.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public static object ConvertStringToData(string value, AttributeValueType type)
        {
            try
            {
                switch (type)
                {
                    case AttributeValueType.Boolean:
                        if (value == string.Empty)
                            return false;
                        return bool.Parse(value);
                    case AttributeValueType.Float:
                        if (value == string.Empty)
                            return 0f;
                        if (value.Contains(','))
                            value = value.Replace(',', '.');
                        return Parser.ParseFloatSave(value);
                    case AttributeValueType.Integer:
                        if (value == string.Empty)
                            return 0;
                        return int.Parse(value);
                    case AttributeValueType.String:
                        return value;
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw new RelicException(ex,
                                         "Failed to convert string '" + value +
                                         "' to a proper Data-object using type '" + type +
                                         "'. See inner exception.");
            }
        }

        ///<summary>
        /// Returns the DataType corresponding to/described by the given string.
        ///</summary>
        ///<param name="s"></param>
        ///<returns></returns>
        public static AttributeValueType ConvertStringToType(string s)
        {
            s = s.ToLowerInvariant();
            if (s == "bool" || s == "boolean")
                return AttributeValueType.Boolean;
            if (s == "float")
                return AttributeValueType.Float;
            if (s == "string")
                return AttributeValueType.String;
            if (s == "table")
                return AttributeValueType.Table;
            if (s == "integer" || s == "int")
                return AttributeValueType.Integer;
            if (s == "list")
                return AttributeValueType.List;
            return AttributeValueType.Invalid;
        }

        /// <summary>
        /// Converts the value of the specified AttributeValue to its representation as a string.
        /// </summary>
        /// <param name="attribValue"></param>
        /// <returns></returns>
        public static string ConvertDataToString(AttributeValue attribValue)
        {
            if (attribValue == null)
                return string.Empty;
            if (attribValue.DataType == AttributeValueType.Table)
            {
                if (attribValue.Data == null)
                    return "{ }";
                return "{" + (attribValue.Data as AttributeTable).ChildCount + " }";
            }
            if (attribValue.Data != null)
                return attribValue.Data.ToString();
            switch (attribValue.DataType)
            {
                case AttributeValueType.Boolean:
                    return "false";
                case AttributeValueType.Float:
                    return "0";
                case AttributeValueType.Integer:
                    return "0";
                case AttributeValueType.String:
                    return string.Empty;
            }
            return string.Empty;
        }

        #endregion

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<AttributeValue> GetEnumerator()
        {
            return new AttributeValueEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        internal void SetParent(AttributeTable table)
        {
            Parent = table;
        }

        internal void RemoveData()
        {
            m_data = null;
        }

        /// <summary>
        /// Returns the path of this instance of AttributeValue.
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            string tmp = string.Empty;
            if (Parent == null)
            {
                return "GameData";
            }
            tmp += Parent.Owner.GetPath();
            return tmp + '\\' + Key;
        }

        #region Properties

        /// <summary>
        /// Get or sets the DataType of this instance. Changing the DataType will clear the Data-field.
        /// </summary>
        public AttributeValueType DataType
        {
            get { return m_dataType; }
            set
            {
                if (value == m_dataType)
                    return;
                if (m_data is AttributeTable)
                    (m_data as AttributeTable).Owner = null;
                else
                    m_data = null;
                m_dataType = value;
            }
        }

        /// <summary>
        /// Gets or sets the Data held by this instance. Make sure to set the DataType to the proper type or you'll get an exception.
        /// </summary>
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public object Data
        {
            get { return m_data; }
            set
            {
                if (value == null || IsOfRightType(value, m_dataType))
                {
                    if (m_data != null && m_data is AttributeTable)
                        (m_data as AttributeTable).Owner = null;
                    if (value != null && value is AttributeTable)
                    {
                        var table = value as AttributeTable;
                        if (table.Owner != null)
                            table.Owner.RemoveData();
                        table.Owner = this;
                        m_data = value;
                    }
                    else
                        m_data = value;
                    return;
                }
                throw new RelicException("Trying to set Data of " + GetPath() + " to '" + value + "' of type " +
                                         value.GetType().FullName + " which is not compatible with " + m_dataType +
                                         ".");
            }
        }

        /// <summary>
        /// Gets or sets the Key of this instance.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the parent (the AttributeTable holding this value) of this instance of AttributeValue.
        /// </summary>
        public AttributeTable Parent { get; private set; }

        #endregion

        public override string ToString()
        {
            string data = Data == null ? "null" : Data.ToString();
            return "Attrib Value: " + Key + " - " + DataType + " - " + data;
        }
    }
}