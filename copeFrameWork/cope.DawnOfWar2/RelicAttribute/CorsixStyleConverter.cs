#region

using System;
using System.Collections.Generic;
using System.Text;
using cope.Extensions;

#endregion

namespace cope.DawnOfWar2.RelicAttribute
{
    public static class CorsixStyleConverter
    {
        #region from Corsix-style

        private const char STD_SEPERATOR = '|';
        private const string COMMENT_START = "--";
        private const char ASSIGNMENT_OPERATOR = ':';
        private const char LINE_TERMINATOR = ';';

        private static readonly char[] s_cLineSeparators = new[] {'\n'};

        public static List<AttributeValue> Parse(string lines)
        {
            return Parse(lines.Split(s_cLineSeparators, StringSplitOptions.RemoveEmptyEntries), STD_SEPERATOR);
        }

        public static List<AttributeValue> Parse(string lines, char separator)
        {
            return Parse(lines.Split(s_cLineSeparators, StringSplitOptions.RemoveEmptyEntries), separator);
        }

        public static List<AttributeValue> Parse(string[] lines)
        {
            return Parse(lines, STD_SEPERATOR);
        }

        /// <exception cref="CopeDoW2Exception"><c>CopeDoW2Exception</c>.</exception>
        public static List<AttributeValue> Parse(string[] lines, char separator)
        {
            int currentLineIndex = 0;
            var values = new List<AttributeValue>();

            Func<string> getNextLine = () => currentLineIndex >= lines.Length ? null : lines[currentLineIndex++];

            while (currentLineIndex < lines.Length)
            {
                string s = getNextLine();
                if (s.StartsWith(COMMENT_START) || s == string.Empty || s == "\n")
                    continue;

                string key, value;
                SplitIntoKeyAndValue(s, out key, out value, separator);
                try
                {
                    AttributeValue temp = ProcessKeyValue(key, value, getNextLine, separator);
                    values.Add(temp);
                }
                catch (Exception ex)
                {
                    throw new CopeDoW2Exception(ex,
                                                "Error in line " + currentLineIndex +
                                                " while parsing Corsix-style text. See inner exception for further infomration.");
                }
            }
            return values;
        }

        /// <summary>
        /// Takes a line and splits it into a key and a value string.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        private static void SplitIntoKeyAndValue(string line, out string key, out string value, char separator)
        {
            // delete possible comments
            line = line.SubstringBeforeFirst(COMMENT_START);
            // where does the actual key start?
            int i = 0;
            for (; i < line.Length; i++)
                if (line[i] != separator && !line[i].IsCharOfType(CharType.Whitespace))
                    break;
            line = line.Remove(0, i);

            // get the key & value
            key = line.SubstringBeforeFirst(ASSIGNMENT_OPERATOR).Trim();
            value = line.SubstringAfterFirst(ASSIGNMENT_OPERATOR).SubstringBeforeLast(LINE_TERMINATOR).Trim();
        }

        /// <exception cref="CopeDoW2Exception">Error parsing Corsix-style text: Unexpected end of file. Expected '};' instead!</exception>
        private static AttributeValue ProcessKeyValue(string key, string value, Func<string> getNextLine, char separator)
        {
            AttributeDataType type;
            object data = null;
            if (key == "{" || value == "{")
            {
                type = AttributeDataType.Table;
                if (key == "{")
                    key = string.Empty;
                data = new AttributeTable();
            }
            else if (value.StartsWith('"'))
            {
                type = AttributeDataType.String;
                value = value.RemoveLast(1).RemoveFirst(1); // get rid of ""
            }
            else if (value.EndsWith('f'))
            {
                type = AttributeDataType.Float;
                value = value.Remove(value.Length - 1, 1);
            }
            else if (value.ToLower() == "true" || value.ToLower() == "false")
                type = AttributeDataType.Boolean;
            else
                type = AttributeDataType.Integer;

            if (data == null)
                data = AttributeValue.ConvertStringToData(value, type);

            var temp = new AttributeValue(type, key, data);

            if (type == AttributeDataType.Table)
            {
                var table = data as AttributeTable;
                while (true)
                {
                    string next = getNextLine();
                    if (next == null)
                        throw new CopeDoW2Exception(
                            "Error parsing Corsix-style text: Unexpected end of file. Expected '};' instead!");
                    string nextKey, nextValue;
                    SplitIntoKeyAndValue(next, out nextKey, out nextValue, separator);
                    if (nextKey == "};" || nextValue == "};")
                        break;
                    table.AddValue(ProcessKeyValue(nextKey, nextValue, getNextLine, separator));
                }
                temp.Data = table;
            }
            return temp;
        }

        #endregion

        #region to CorsixStyle

        public static string ToCorsixStyle(AttributeValue attribute, char separator = '|')
        {
            StringBuilder sb = new StringBuilder();
            ToCorsixStyle(attribute, sb, separator, 0);
            return sb.ToString();
        }

        private static void ToCorsixStyle(AttributeValue attribute, StringBuilder sb, char separator, int depth)
        {
            if (sb.Length != 0)
                sb.Append('\n');
            if (depth > 0)
                sb.Append(separator + " ", depth);
            sb.Append(attribute.Key);
            sb.Append(": ");
            switch (attribute.DataType)
            {
                case AttributeDataType.Boolean:
                    sb.Append(attribute.Data.ToString().ToLower());
                    sb.Append(';');
                    break;
                case AttributeDataType.Float:
                    sb.Append(attribute.Data.ToString().Replace(',', '.'));
                    sb.Append("f;");
                    break;
                case AttributeDataType.Integer:
                    sb.Append(attribute.Data.ToString());
                    sb.Append(';');
                    break;
                case AttributeDataType.String:
                    sb.Append('"');
                    sb.Append(attribute.Data as string);
                    sb.Append("\";");
                    break;
                case AttributeDataType.Table:
                    sb.Append("{");
                    foreach (AttributeValue value in attribute.Data as AttributeTable)
                        ToCorsixStyle(value, sb, separator, depth + 1);
                    sb.Append('\n');
                    sb.Append(separator + " ", depth);
                    sb.Append("};");
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}