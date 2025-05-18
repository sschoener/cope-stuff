using System.Collections.Generic;
using System.Linq;

namespace cope.Extensions
{
    /// <summary>
    /// Static class offering some useful extensions for string arrays.
    /// </summary>
    public static class StringArrayExt
    {
        public static string Collapse(this IEnumerable<string> enumer, string delimiter = "\n")
        {
            return enumer.Aggregate((s1, s2) => s1 + delimiter + s2);
        }
    }
}
