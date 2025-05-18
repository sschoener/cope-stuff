#region

using System.Collections.Generic;
using System.IO;
using cope.Extensions;

#endregion

namespace cope.Relic.RelicChunky.ChunkTypes.ActionChunk
{
    /// <summary>
    /// Helper class for writing Action from DataChunks.
    /// </summary>
    public static class ActionWriter
    {
        /// <summary>
        /// Writes a given list of Actions to a stream in binary format.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="actions"></param>
        public static void Write(Stream str, List<Action> actions)
        {
            BinaryWriter bw = new BinaryWriter(str);
            bw.Write(actions.Count);
            foreach (Action actn in actions)
                WriteAction(bw, actn);
        }

        private static void WriteAction(BinaryWriter bw, Action action)
        {
            bw.Write(action.Name.Length);
            bw.Write(action.Name.ToByteArray(true));
            bw.Write(action.Params.Count);
            foreach (KeyValuePair<string, string> kvp in action.Params)
            {
                bw.Write(kvp.Key.Length);
                bw.Write(kvp.Key.ToByteArray(true));
                bw.Write(kvp.Value.Length);
                bw.Write(kvp.Value.ToByteArray(true));
            }
            bw.Write(action.Delay);
        }

        /// <summary>
        /// Writes a list of Actions to a steam in text format (ASCII). Using the indentation parameter you
        /// can influence the type of output produced (default is double whitespace).
        /// </summary>
        /// <param name="str"></param>
        /// <param name="actions"></param>
        /// <param name="indentation"></param>
        public static void WriteText(Stream str, List<Action> actions, string indentation = "  ")
        {
            TextWriter tw = new StreamWriter(str, System.Text.Encoding.ASCII);
            foreach (Action act in actions)
                tw.WriteLines(GetLines(act, indentation));
            tw.Flush();
        }

        /// <summary>
        /// Converts an action to its textual representation and returns the lines as an array of strings.
        /// You may also provide a string that shall be used when indenting anything (default is double whitespace).
        /// </summary>
        /// <param name="action"></param>
        /// <param name="indentation"></param>
        /// <returns></returns>
        public static string[] GetLines(Action action, string indentation = "  ")
        {
            int length = action.Params.Count + 6;
            string[] lines = new string[length];
            lines[0] = "action: {";
            lines[1] = indentation + "name: " + action.Name + ';';
            lines[2] = indentation + "delay: " + action.Delay + ';';
            lines[3] = indentation + "params: {";
            int idx = 4;
            string doubleIndent = indentation + indentation;

            // action files only have strings
            foreach(var kvp in action)
                lines[idx++] = doubleIndent + kvp.Key + ": \"" + kvp.Value + "\";";

            lines[length - 2] = indentation + "};";
            lines[length - 1] = "};";
            return lines;
        }
    }
}