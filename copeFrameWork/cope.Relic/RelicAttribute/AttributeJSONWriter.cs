using System;
using System.IO;
using System.Text;
using cope.Extensions;

namespace cope.Relic.RelicAttribute
{
    public class AttributeJSONWriter
    {
        #region to CorsixStyle

        public static string ToJSON(AttributeValue attribute, bool asObject = true)
        {
            StringBuilder sb = new StringBuilder();
            ToJSON(attribute, sb, "    ", 0, asObject);
            return sb.ToString();
        }

        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public static void Write(Stream stream, AttributeStructure attribute)
        {
            try
            {
                TextWriter wr = new StreamWriter(stream);
                wr.Write(ToJSON(attribute.Root));
                wr.Flush();
            }
            catch (Exception)
            {
                var excep = new RelicException("Failed to write Attribute to Stream as JSON.");
                excep.Data["Stream"] = stream;
                excep.Data["Attribute"] = attribute;
                throw excep;
            }
        }

        private static string Escape(string s)
        {
            return s.Replace("\\", "\\\\");
        }

        private static void ToJSON(AttributeValue attribute, StringBuilder sb, string separator, int depth, bool skipKey = false)
        {
            if (sb.Length != 0)
                sb.Append('\n');
            if (depth > 0)
                sb.Append(separator, depth);
            if (!skipKey)
            {
                sb.Append('"' + attribute.Key + '"');
                sb.Append(": ");
            }
            switch (attribute.DataType)
            {
                case AttributeValueType.Boolean:
                    sb.Append(attribute.Data.ToString().ToLower());
                    break;
                case AttributeValueType.Float:
                    sb.Append(attribute.Data.ToString().Replace(',', '.'));
                    break;
                case AttributeValueType.Integer:
                    sb.Append(attribute.Data);
                    break;
                case AttributeValueType.String:
                    sb.Append('"');
                    sb.Append(Escape(attribute.Data as string));
                    sb.Append('"');
                    break;
                case AttributeValueType.Table:
                    sb.Append("{");
                    AttributeTable t = attribute.Data as AttributeTable;
                    var children = t.GetValues();
                    children.Sort();
                    if (children.Count > 0)
                    {
                        foreach (AttributeValue value in children)
                        {
                            ToJSON(value, sb, separator, depth + 1);
                            sb.Append(',');
                        }
                        sb.Length -= 1;
                        sb.Append('\n');
                        sb.Append(separator, depth);
                    }
                    sb.Append("}");
                    break;
                case AttributeValueType.List:
                    sb.Append("[");
                    AttributeList l = attribute.Data as AttributeList;
                    children = l.GetValues();

                    if (children.Count > 0)
                    {
                        foreach (AttributeValue value in children)
                        {
                            ToJSON(value, sb, separator, depth + 1, true);
                            sb.Append(',');
                        }
                        sb.Length -= 1;
                        sb.Append('\n');
                        sb.Append(separator, depth);
                    }
                    sb.Append("]");
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
