using System.Collections.Generic;

namespace cope
{
    /// <summary>
    /// Class used for parsing arguments to a command line application.
    /// </summary>
    static public class ArgParser
    {
        /// <summary>
        /// Parses a set of arguments as received as a parameter to the Main method of command line programs.
        /// This function will do very basic parsing, so you get a table representing the different arguments and their values as a string.
        /// An empty string implies that the argument is present but does not have a value.
        /// Arguments start with '-', e.g. "-x clap -t -o"
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static public IDictionary<string, string> ParseArguments(string[] args)
        {
            var result = new Dictionary<string, string>(args.Length);

            for (int i = 0; i < args.Length; i++)
            {
                string current = args[i];
                if (current.Length > 1 && current[0] == '-')
                {
                    string key = current.Substring(1);
                    int nextIdx = i + 1;
                    if (nextIdx < args.Length && args[nextIdx].Length > 1 && args[nextIdx][0] != '-')
                        result[key] = args[nextIdx];
                    else
                        result[key] = string.Empty;
                }
            }
            return result;
        }

        static public IDictionary<string, string> ParseArguments(IEnumerable<string> args)
        {
            var result = new Dictionary<string, string>();

            string key = null;
            string value = string.Empty;
            foreach (string s in args)
            {
                if (string.IsNullOrEmpty(s))
                    continue;

                if (s[0] == '-')
                {
                    if (key != null && !result.ContainsKey(key))
                        result.Add(key, value);
                    
                    key = s.Substring(1);
                    value = string.Empty;
                }
                else if (key != null)
                    value = s;
            }
            return result;
        }
    }
}
