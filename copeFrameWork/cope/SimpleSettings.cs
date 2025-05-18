#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using cope.Extensions;

#endregion

namespace cope
{
    /// <summary>
    /// XML-based simple settings format.
    /// </summary>
    public class SimpleSettings
    {
        #region SettingType enum

        public enum SettingType
        {
            Bool = 0,
            Integer = 1,
            UnsignedInteger = 2,
            Char = 3,
            String = 4,
            Float = 5,
            Double = 6,
            Decimal
        }

        #endregion

        private Dictionary<string, Setting> m_settings;

        public Setting this[string name]
        {
            get
            {
                Setting s;
                if (m_settings.TryGetValue(name, out s))
                    return s;
                return null;
            }
            set { m_settings[name] = value; }
        }

        public Setting GetSetting(string name)
        {
            Setting s;
            if (m_settings.TryGetValue(name, out s))
                return s;
            return null;
        }

        public void SetSetting(string name, object value)
        {
            Setting s;
            if (m_settings.TryGetValue(name, out s))
                s.Value = value;
            else
                m_settings.Add(name, new Setting(value));
        }

        public void Save(string to)
        {
            if (!Directory.Exists(to.SubstringBeforeLast('\\')))
                Directory.CreateDirectory(to.SubstringBeforeLast('\\'));
            var xdoc = new XmlDocument();
            xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", string.Empty, string.Empty));
            XmlNode xMain = xdoc.CreateElement("settings");
            xdoc.AppendChild(xMain);
            foreach (var s in from KeyValuePair<string, Setting> s in m_settings where s.Value != null select s)
            {
                var xSetting = xdoc.CreateElement("setting");
                var xKey = xdoc.CreateAttribute("key");
                xKey.Value = s.Key;
                xSetting.Attributes.Append(xKey);
                var xType = xdoc.CreateAttribute("type");
                xType.Value = Setting.TypeNames[(int) s.Value.Type];
                xSetting.Attributes.Append(xType);
                var xVal = xdoc.CreateAttribute("value");
                xVal.Value = s.Value.ToString();
                xSetting.Attributes.Append(xVal);

                xMain.AppendChild(xSetting);
            }

            xdoc.Save(to);
        }

        public bool Load(string from)
        {
            m_settings = new Dictionary<string, Setting>();
            if (!File.Exists(from))
                return false;

            var xdoc = new XmlDocument();
            xdoc.Load(from);

            var xMain = xdoc["settings"] ?? (XmlNode) xdoc;

            foreach (XmlNode n in from XmlNode n in xMain.ChildNodes where n.Name == "setting" select n)
            {
                try
                {
                    string key;
                    var set = new Setting(n, out key);
                    if (!m_settings.ContainsKey(key))
                        m_settings.Add(key, set);
                }
                catch
                {
                }
            }
            return true;
        }

        #region Nested type: Setting

        public sealed class Setting : IGenericClonable<Setting>
        {
            public static readonly string[] TypeNames = new[]
                                                            {
                                                                "bool", "int", "uint", "char", "string", "float", "double",
                                                                "decimal"
                                                            };

            private object m_value;

            public Setting(SettingType type)
            {
                Type = type;
            }

            public Setting(SettingType type, string value)
            {
                Type = type;
                SetFromString(value);
            }

            public Setting(object value)
            {
                SetFromObject(value);
            }

            /// <exception cref="Exception">Tried to read Setting from XmlNode but there's no Attributes node!</exception>
            public Setting(XmlNode xmlNode, out string key)
            {
                key = null;
                if (xmlNode.Attributes == null)
                    throw new Exception("Tried to read Setting from XmlNode but there's no Attributes node!");
                key = xmlNode.Attributes["key"].Value;
                var type = xmlNode.Attributes["type"];
                if (type == null)
                    Type = SettingType.String;
                else
                {
                    int i = TypeNames.IndexOf(type.Value);
                    if (i >= 0)
                        Type = (SettingType) i;
                    else
                        throw new Exception("Tried to read Setting from XmlNode but the Type-attribute is invalid: '" +
                                            type.Value + "'");
                }
                SetFromString(xmlNode.Attributes["value"].Value);
            }

            public SettingType Type { get; private set; }

            public object Value
            {
                get { return m_value; }
                set
                {
                    if (IsOfRightType(value))
                        m_value = value;
                    throw new Exception("Tried to assign a value to Setting-object but the value is of the wrong type!");
                }
            }

            #region IGenericClonable<Setting> Members

            public Setting GClone()
            {
                return new Setting(Type) {m_value = m_value};
            }

            #endregion

            private void SetFromObject(object o)
            {
                m_value = o;
                if (o is bool)
                    Type = SettingType.Bool;
                else if (o is char)
                    Type = SettingType.Char;
                else if (o is decimal)
                    Type = SettingType.Decimal;
                else if (o is double)
                    Type = SettingType.Double;
                else if (o is float)
                    Type = SettingType.Float;
                else if (o is int)
                    Type = SettingType.Integer;
                else if (o is string)
                    Type = SettingType.String;
                else if (o is uint)
                    Type = SettingType.UnsignedInteger;
                else
                    throw new Exception("Invalid type for value of Setting-object!");
            }

            private void SetFromString(string value)
            {
                switch (Type)
                {
                    case SettingType.Bool:
                        m_value = bool.Parse(value);
                        return;
                    case SettingType.Char:
                        m_value = !string.IsNullOrEmpty(value) ? value[0] : '\0';
                        return;
                    case SettingType.Decimal:
                        m_value = decimal.Parse(value);
                        return;
                    case SettingType.Double:
                        m_value = double.Parse(value);
                        return;
                    case SettingType.Float:
                        m_value = float.Parse(value);
                        return;
                    case SettingType.Integer:
                        m_value = int.Parse(value);
                        return;
                    case SettingType.String:
                        m_value = value;
                        return;
                    case SettingType.UnsignedInteger:
                        m_value = uint.Parse(value);
                        return;
                }
            }

            public void Set(object o)
            {
                Value = o;
            }

            public bool IsOfRightType(object o)
            {
                switch (Type)
                {
                    case SettingType.Bool:
                        if (o is bool)
                            return true;
                        return false;
                    case SettingType.Char:
                        if (o is char)
                            return true;
                        return false;
                    case SettingType.String:
                        if (o is string || o is char)
                            return true;
                        return false;
                    default:
                        return IsNumber(o);
                }
            }

            public override string ToString()
            {
                return Value.ToString();
            }

            public override bool Equals(object obj)
            {
                return Value.Equals(obj);
            }

            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }

            public bool IsNumber()
            {
                return Type == SettingType.Decimal || Type == SettingType.Double ||
                       Type == SettingType.Float || Type == SettingType.Integer ||
                       Type == SettingType.UnsignedInteger;
            }

            private static bool IsNumber(object o)
            {
                return o is float || o is double || o is int || o is uint || o is decimal || o is byte || o is short ||
                       o is ushort;
            }

            #region custom casts

            // FROM SETTING
            public static implicit operator bool(Setting s)
            {
                if (s == null)
                    throw new Exception("Setting is null!");
                if (s.Type == SettingType.Bool)
                    return (bool) s.Value;
                throw new Exception("Tried to get setting as bool but it is not convertible!");
            }

            public static implicit operator float(Setting s)
            {
                if (s == null)
                    throw new Exception("Setting is null!");
                if (s.IsNumber())
                    return (float) s.Value;
                throw new Exception("Tried to get setting as float but it is not convertible!");
            }

            public static implicit operator double(Setting s)
            {
                if (s == null)
                    throw new Exception("Setting is null!");
                if (s.IsNumber())
                    return (double) s.Value;
                throw new Exception("Tried to get setting as double but it is not convertible!");
            }

            public static implicit operator decimal(Setting s)
            {
                if (s == null)
                    throw new Exception("Setting is null!");
                if (s.IsNumber())
                    return (decimal) s.Value;
                throw new Exception("Tried to get setting as decimal but it is not convertible!");
            }

            public static implicit operator uint(Setting s)
            {
                if (s == null)
                    throw new Exception("Setting is null!");
                if (s.IsNumber())
                    return (uint) s.Value;
                throw new Exception("Tried to get setting as uint but it is not convertible!");
            }

            public static implicit operator int(Setting s)
            {
                if (s.IsNumber())
                    return (int) s.Value;
                throw new Exception("Tried to get setting as int but it is not convertible!");
            }

            public static implicit operator char(Setting s)
            {
                if (s == null)
                    throw new Exception("Setting is null!");
                if (s.Type == SettingType.Char)
                    return (char) s.Value;
                if (s.Type == SettingType.String)
                {
                    var str = s.Value as string;
                    if (!string.IsNullOrEmpty(str))
                        return str[0];
                }
                throw new Exception("Tried to get setting as char but it is not convertible!");
            }

            public static implicit operator string(Setting s)
            {
                if (s == null)
                    return null;
                if (s.Type == SettingType.String)
                    return s.Value as string;
                return s.Value.ToString();
            }

            // TO SETTING
            public static implicit operator Setting(bool o)
            {
                return new Setting(o);
            }

            public static implicit operator Setting(char o)
            {
                return new Setting(o);
            }

            public static implicit operator Setting(decimal o)
            {
                return new Setting(o);
            }

            public static implicit operator Setting(double o)
            {
                return new Setting(o);
            }

            public static implicit operator Setting(float o)
            {
                return new Setting(o);
            }

            public static implicit operator Setting(int o)
            {
                return new Setting(o);
            }

            public static implicit operator Setting(string o)
            {
                if (o == null)
                    return null;
                return new Setting(o);
            }

            public static implicit operator Setting(uint o)
            {
                return new Setting(o);
            }

            #endregion
        }

        #endregion
    }
}