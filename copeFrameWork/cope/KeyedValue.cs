#region

using System;
using System.Collections;
using System.Collections.Generic;
using cope.Extensions;

#endregion

namespace cope
{
    /// <summary>
    /// Represents a Value with a Key as used in the KeyValueSyntaxParser.
    /// </summary>
    public sealed class KeyedValue : IComparable<KeyedValue>, IGenericClonable<KeyedValue>, IEnumerable<KeyedValue>
    {
        private KeyValueType m_type;
        private object m_value;

        public KeyedValue(KeyValueType type, string name, object value)
        {
            Key = name;
            Type = type;
            Value = value;
        }

        #region properties

        /// <summary>
        /// Gets the key of this instance of KeyedValue.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the type of this instance of KeyedValue. Changing the type will clear the value.
        /// </summary>
        public KeyValueType Type
        {
            get { return m_type; }
            set
            {
                if (value == m_type)
                    return;
                if (m_value is KeyValueTable)
                    (m_value as KeyValueTable).Owner = null;
                else
                    m_value = null;
                m_type = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of this instance of KeyedValue.
        /// </summary>
        /// <exception cref="CopeException">Thrown when a value is passed which does not fit the KeyValueType in Type.</exception>
        public object Value
        {
            get { return m_value; }
            set
            {
                if (value == null || IsOfRightType(value, m_type))
                {
                    if (m_value != null && m_value is KeyValueTable)
                        (m_value as KeyValueTable).Owner = null;
                    if (value != null && value is KeyValueTable)
                    {
                        var table = value as KeyValueTable;
                        if (table.Owner != null)
                            table.Owner.RemoveData();
                        table.Owner = this;
                        m_value = value;
                    }
                    else
                        m_value = value;
                    return;
                }
                throw new CopeException("Trying to set Value of " + GetPath() + " to '" + value + "' of type " +
                                        value.GetType().FullName + " which is not compatible with " + m_type +
                                        ".");
            }
        }

        /// <summary>
        /// Gets the parent (the KeyValueTable holding this value) of this instance of KeyedValue.
        /// </summary>
        public KeyValueTable Parent { get; private set; }

        /// <summary>
        /// Gets or sets MetaData for this instance of KeyedValue.
        /// </summary>
        public string MetaData { get; set; }

        /// <summary>
        /// A string which will automatically be inserted as a comment when this instance of KeyedValue is serialized.
        /// </summary>
        public string AutoComment { get; set; }

        #endregion

        #region methods

        #region IComparable<KeyedValue> Members

        public int CompareTo(KeyedValue other)
        {
            if (other == null)
                return 1;
            return Key.CompareTo(other.Key);
        }

        #endregion

        #region IEnumerable<KeyedValue> Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyedValue> GetEnumerator()
        {
            return new KeyedValueEnumerator(this);
        }

        #endregion

        #region IGenericClonable<KeyedValue> Members

        public KeyedValue GClone()
        {
            object data = Value;
            if (data is KeyValueTable)
                data = (data as KeyValueTable).GClone();
            return new KeyedValue(Type, Key, data);
        }

        #endregion

        /// <summary>
        /// Returns the path of this instance of KeyedValue.
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            string tmp = string.Empty;
            if (Parent == null)
            {
                return "/";
            }
            tmp += Parent.Owner.GetPath();
            return tmp + '/' + Key;
        }

        internal void SetParent(KeyValueTable table)
        {
            Parent = table;
        }

        internal void RemoveData()
        {
            m_value = null;
        }

        /*public override bool Equals(object obj)
        {
            if (obj == null || !(obj is KeyedValue))
                return false;
            KeyedValue other = obj as KeyedValue;
            return Type == other.Type && Key == other.Key && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode() ^ Type.GetHashCode() ^ Value.GetHashCode();
        }*/

        public override string ToString()
        {
            return Key + " (" + Type + "): " + Value;
        }

        #endregion

        #region Statics

        ///<summary>
        /// Checks whether the specified Data-object is of the specified type.
        ///</summary>
        ///<param name="data"></param>
        ///<param name="type"></param>
        ///<returns></returns>
        public static bool IsOfRightType(object data, KeyValueType type)
        {
            switch (type)
            {
                case KeyValueType.Boolean:
                    if (data is bool)
                        return true;
                    break;
                case KeyValueType.Float:
                    if (data is float)
                        return true;
                    break;
                case KeyValueType.Integer:
                    if (data is int)
                        return true;
                    break;
                case KeyValueType.String:
                    if (data is string)
                        return true;
                    break;
                case KeyValueType.Table:
                    if (data is KeyValueTable)
                        return true;
                    break;
            }
            return false;
        }

        ///<summary>
        /// Returns the KeyValueType suitable for the specified Data-object.
        ///</summary>
        ///<param name="data"></param>
        ///<returns></returns>
        public static KeyValueType GetTypeForData(object data)
        {
            if (data is bool)
                return KeyValueType.Boolean;
            if (data is float)
                return KeyValueType.Float;
            if (data is int)
                return KeyValueType.Integer;
            if (data is string)
                return KeyValueType.String;
            if (data is KeyValueTable)
                return KeyValueType.Table;
            return KeyValueType.Invalid;
        }

        /// <summary>
        /// Tries to convert the given string to a proper Value-object using the specified KeyValueType. Throws an exception if it fails
        /// to convert the value or returns null there's no suitable conversion available.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        public static object ConvertStringToData(string value, KeyValueType type)
        {
            try
            {
                switch (type)
                {
                    case KeyValueType.Boolean:
                        if (value == string.Empty)
                            return false;
                        return bool.Parse(value);
                    case KeyValueType.Float:
                        if (value == string.Empty)
                            return 0f;
                        if (value.Contains(','))
                            value = value.Replace(',', '.');
                        return Parser.ParseFloatSave(value);
                    case KeyValueType.Integer:
                        if (value == string.Empty)
                            return 0;
                        return int.Parse(value);
                    case KeyValueType.String:
                        return value;
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw new CopeException(ex,
                                        "Failed to convert string '" + value +
                                        "' to a proper Value-object using type '" + type +
                                        "'. See inner exception.");
            }
        }

        ///<summary>
        /// Returns the DataType corresponding to/described by the given string.
        ///</summary>
        ///<param name="s"></param>
        ///<returns></returns>
        public static KeyValueType ConvertStringToType(string s)
        {
            s = s.ToLowerInvariant();
            if (s == "bool" || s == "boolean")
                return KeyValueType.Boolean;
            if (s == "float")
                return KeyValueType.Float;
            if (s == "string")
                return KeyValueType.String;
            if (s == "table")
                return KeyValueType.Table;
            if (s == "integer" || s == "int")
                return KeyValueType.Integer;
            return KeyValueType.Invalid;
        }

        /// <summary>
        /// Converts the value of the specified KeyedValue to its representation as a string.
        /// </summary>
        /// <param name="attribValue"></param>
        /// <returns></returns>
        public static string ConvertDataToString(KeyedValue attribValue)
        {
            if (attribValue == null)
                return string.Empty;
            if (attribValue.Type == KeyValueType.Table)
            {
                if (attribValue.Value == null)
                    return "{ }";
                return "{" + (attribValue.Value as KeyValueTable).ChildCount + " }";
            }
            if (attribValue.Value != null)
                return attribValue.Value.ToString();
            switch (attribValue.Type)
            {
                case KeyValueType.Boolean:
                    return "false";
                case KeyValueType.Float:
                    return "0";
                case KeyValueType.Integer:
                    return "0";
                case KeyValueType.String:
                    return string.Empty;
            }
            return string.Empty;
        }

        #endregion
    }
}