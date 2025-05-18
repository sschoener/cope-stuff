#region

using System;
using System.Collections.Generic;
using System.Globalization;
using cope.Extensions;

#endregion

namespace cope
{
    /// <summary>
    /// This class implements a basic parser for a JSON-like-syntax which allows the reprenstation of 
    /// key-value data in a tree like structure.
    /// Each key-value pair looks like this: _key_: _value_ ;
    /// There are several primitive data types: integers, floats, strings, booleans and tables.
    /// Tables are similar to associative arrays, but they may hold more than one value with the same key.
    /// Comments are supported, everything after a -- will be interpreted as a comment (until the end of the line).
    /// There is a possibility to add MetaData, which works similar to comments. MetaData will be carried into the internal representation
    /// (in contrast to comments). MetaData is started by -$ and closed by $- (optional) and must come right after a value (following the semicolon).
    /// MetaData may not contain linebreaks.
    /// 
    /// Examples:
    /// string: "this is a string";
    /// float: 1.0f; -- mind the 'f' that indicates the floatingpoint-ness of the value
    /// int: 1;
    /// bool: true; -- or false
    /// table: {
    /// |    entry1: "value";
    /// }; 
    /// 
    /// The parser will ignore all occurences of |. | is totally optional.
    /// 
    /// </summary>
    public static class KeyValueSyntaxParser
    {
        public static List<KeyedValue> Parse(string content)
        {
            return Parse(content.Split(StringSplitOptions.RemoveEmptyEntries, '\n'));
        }

        /// <summary>
        /// Parses a set of lines to a list of KeyedValues.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        public static List<KeyedValue> Parse(string[] lines)
        {
            Lexeme[] lexemes;

            try
            {
                List<Lexeme>[] lexedLines = lines.Map(LexicalAnalysis);
                lexemes = lexedLines.Flatten();
            }
            catch (Exception ex)
            {
                var excep = new CopeException(ex, "Error while lexing Key-Value-Syntax.");
                excep.Data["Input"] = lines;
                throw excep;
            }

            List<KeyedValue> values = new List<KeyedValue>();
            int index = 0;
            try
            {
                while (index < lexemes.Length)
                {
                    EatComments(lexemes, ref index);
                    if (lexemes[index].Type == LexemeType.CurlyBraceOpen)
                    {
                        string metaData;
                        var val = new KeyedValue(KeyValueType.Table, string.Empty, ParseTable(lexemes, ref index, out metaData));
                        if (index > lexemes.Length)
                            throw new CopeException("Unexpected end of lexemes! Expected semicolon.");
                        if (lexemes[index].Type != LexemeType.SemicolonOperator)
                            throw new CopeException("Error while parsing: Expected semicolon but got " + lexemes[index]);
                        index++;
                        if (index < lexemes.Length && lexemes[index].Type == LexemeType.MetaData)
                        {
                            if (string.IsNullOrWhiteSpace(metaData))
                                metaData = lexemes[index].Value as string;
                            index++;
                        }
                        val.MetaData = metaData;
                        values.Add(val);
                    }
                    else if (lexemes[index].Type == LexemeType.Identifier)
                        values.Add(ParseValue(lexemes, ref index));
                }
            }
            catch (Exception ex)
            {
                var excep = new CopeException(ex, "Error while parsing Key-Value-Syntax.");
                excep.Data["Lexemes"] = lexemes;
                excep.Data["Index"] = index;
                throw excep;
            }
            return values;
        }

        private static KeyValueTable ParseTable(Lexeme[] lexemes, ref int index, out string metaData)
        {
            KeyValueTable table = new KeyValueTable();
            index++; // skip opening brace

            if (lexemes[index].Type == LexemeType.MetaData)
            {
                metaData = lexemes[index].Value as string;
                index++;
            }
            else
                metaData = string.Empty;

            while (lexemes[index].Type != LexemeType.CurlyBraceClose)
            {
                EatComments(lexemes, ref index);
                table.AddValue(ParseValue(lexemes, ref index));
            }
            index++;
            return table;
        }

        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        private static KeyedValue ParseValue(Lexeme[] lexemes, ref int index)
        {
            EatComments(lexemes, ref index);
            if (lexemes[index].Type != LexemeType.Identifier)
                throw new CopeException("Error while parsing: Expected an identifier but got " + lexemes[index]);
            string id = (string) lexemes[index].Value;
            index++;
            EatComments(lexemes, ref index);
            if (lexemes[index].Type != LexemeType.AssignmentOperator)
                throw new CopeException("Error while parsing: Expected an assignment operator but got " + lexemes[index]);
            index++;
            EatComments(lexemes, ref index);
            object value;
            KeyValueType type;

            string metaData = string.Empty;
            switch (lexemes[index].Type)
            {
                case LexemeType.Boolean:
                    value = lexemes[index].Value;
                    type = KeyValueType.Boolean;
                    index++;
                    break;
                case LexemeType.Float:
                    value = lexemes[index].Value;
                    type = KeyValueType.Float;
                    index++;
                    break;
                case LexemeType.Integer:
                    value = lexemes[index].Value;
                    type = KeyValueType.Integer;
                    index++;
                    break;
                case LexemeType.String:
                    value = lexemes[index].Value;
                    type = KeyValueType.String;
                    index++;
                    break;
                case LexemeType.CurlyBraceOpen:
                    value = ParseTable(lexemes, ref index, out metaData);
                    type = KeyValueType.Table;
                    break;
                default:
                    throw new CopeException("Error while parsing: Expected any kind of value but got " + lexemes[index]);
            }
            EatComments(lexemes, ref index);
            if (lexemes[index].Type != LexemeType.SemicolonOperator)
                throw new CopeException("Error while parsing: Expected semicolon but got " + lexemes[index]);
            index++;

            if (lexemes[index].Type == LexemeType.MetaData)
            {
                if (string.IsNullOrWhiteSpace(metaData))
                    metaData = lexemes[index].Value as string;
                index++;
            }
            return  new KeyedValue(type, id, value) {MetaData = metaData};
        }

        private static int EatComments(Lexeme[] lexemes, ref int index)
        {
            int c = 0;
            while (lexemes[index + c].Type == LexemeType.Comment)
                c++;
            index += c;
            return c;
        }

        /// <exception cref="CopeException">End of line reached: Missing string delimiter!</exception>
        private static List<Lexeme> LexicalAnalysis(string line)
        {
            var lexemes = new List<Lexeme>();
            string todo = line.TrimStart(' ', '|', '\t');
            for (int idx = 0; idx < todo.Length; idx++)
            {
                try
                {
                    #region lexing
                    if (todo[idx] == '"')
                    {
                        if (idx + 1 > todo.Length)
                        {
                            var excep = new CopeException("Error at end of line: Missing string delimiter to close a string!");
                            excep.Data["Line"] = line;
                            throw excep;
                        }

                        int closeIdx = todo.IndexOf('"', idx + 1);
                        if (closeIdx <= 0)
                        {
                            var excep = new CopeException("Error: Missing string delimiter to close a string!");
                            excep.Data["Line"] = line;
                            throw excep;
                        }
                        lexemes.Add(new Lexeme { Type = LexemeType.String, Value = todo.Substring(idx + 1, closeIdx - (idx + 1)) });
                        idx = closeIdx;
                    }
                    else if (todo[idx] == '{')
                        lexemes.Add(new Lexeme { Type = LexemeType.CurlyBraceOpen });
                    else if (todo[idx] == '}')
                        lexemes.Add(new Lexeme { Type = LexemeType.CurlyBraceClose });
                    else if (todo[idx] == ';')
                        lexemes.Add(new Lexeme { Type = LexemeType.SemicolonOperator });
                    else if (todo[idx] == ':')
                        lexemes.Add(new Lexeme { Type = LexemeType.AssignmentOperator });
                    else if (todo[idx] == '-' && todo.SubstringSafe(idx, 2) == "--")
                    {
                        lexemes.Add(new Lexeme { Type = LexemeType.Comment, Value = todo.SubstringSafe(idx + 2).Trim() });
                        break;
                    }
                    else if (todo[idx] == '-' && todo.SubstringSafe(idx, 2) == "-$")
                    {
                        int metaEndIdx = todo.IndexOf("$-", idx + 2);
                        if (metaEndIdx < 0)
                        {
                            lexemes.Add(new Lexeme { Type = LexemeType.MetaData, Value = todo.SubstringSafe(idx + 2).Trim() });
                            break;
                        }
                        int metaLength = metaEndIdx - (idx + 2);
                        lexemes.Add(new Lexeme { Type = LexemeType.MetaData, Value = todo.SubstringSafe(idx + 2, metaLength).Trim() });
                        idx += metaLength + 3;
                    }
                    else if (todo[idx] == 't' && todo.HasCharAt(idx + 4, c => !(c.IsCharOfType(CharType.Letter, CharType.Digit) || c == '_')) &&
                             todo.SubstringSafe(idx, 4) == "true")
                    {
                        lexemes.Add(new Lexeme { Type = LexemeType.Boolean, Value = true });
                        idx += 3;
                    }
                    else if (todo[idx] == 'f' && todo.HasCharAt(idx + 5, c => !(c.IsCharOfType(CharType.Letter, CharType.Digit) || c == '_')) &&
                             todo.SubstringSafe(idx, 5) == "false")
                    {
                        lexemes.Add(new Lexeme { Type = LexemeType.Boolean, Value = false });
                        idx += 4;
                    }
                    else if (todo[idx].IsCharOfType(CharType.Letter) || todo[idx] == '_')
                    {
                        int len = todo.CountWhile(c => (c.IsCharOfType(CharType.Letter, CharType.Digit) || c == '_'), idx);
                        lexemes.Add(new Lexeme { Type = LexemeType.Identifier, Value = todo.Substring(idx, len) });
                        idx += len - 1;
                    }
                    else // now for numbers
                    {
                        if (todo[idx] == '-')
                        {
                            if (idx + 1 >= todo.Length)
                            {
                                var excep = new CopeException("Error: Unexpected end of number. Got a - sign and no number.");
                                excep.Data["Line"] = line;
                                throw excep;
                            }

                            int length;
                            lexemes.Add(LexNumber(todo, idx + 1, true, out length));
                            idx += length;
                        }
                        else if (todo[idx].IsCharOfType(CharType.Digit))
                        {
                            int length;
                            lexemes.Add(LexNumber(todo, idx, false, out length));
                            idx += length - 1;
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    var excep = new CopeException(ex, "Error while lexing line!");
                    excep.Data["Line"] = todo;
                    excep.Data["Row"] = idx;
                    throw excep;
                }
            }
            return lexemes;
        }

        /// <summary>
        /// Performs lexical analysis for a number in the given string which begins at the given index.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="negative"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static Lexeme LexNumber(string str, int startIndex, bool negative, out int length)
        {
            int numDigits = str.CountWhile(c => c.IsCharOfType(CharType.Digit) || c == '.', startIndex);
            length = numDigits;
            if (str.HasCharAt(startIndex + numDigits, 'f'))
            {
                length++;
                float fvalue = (negative ? (-1) : 1) *
                               float.Parse(str.Substring(startIndex, numDigits), CultureInfo.InvariantCulture);
                return new Lexeme {Type = LexemeType.Float, Value = fvalue};
            }
            int ivalue = (negative ? (-1) : 1) *
                         int.Parse(str.Substring(startIndex, numDigits), CultureInfo.InvariantCulture);
            return new Lexeme {Type = LexemeType.Integer, Value = ivalue};
        }

        #region Nested type: Lexeme

        private sealed class Lexeme
        {
            public LexemeType Type { get; set; }

            public object Value { get; set; }

            public override string ToString()
            {
                return "Lexem, Type: " + Type + ", Value: " + ((Value == null) ? "null" : Value.ToString());
            }
        }

        #endregion

        #region Nested type: LexemeType

        private enum LexemeType
        {
            AssignmentOperator,
            Boolean,
            Comment,
            CurlyBraceOpen,
            CurlyBraceClose,
            Float,
            Identifier,
            Integer,
            MetaData,
            SemicolonOperator,
            String
        }

        #endregion
    }
}