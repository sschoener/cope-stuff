using System.IO;

namespace cope.Extensions
{
    public static class TextWriterExt
    {
        /// <summary>
        /// Writes an array of lines to this instance of TextWriter.
        /// </summary>
        /// <param name="tw"></param>
        /// <param name="lines"></param>
        public static void WriteLines(this TextWriter tw, params string[] lines)
        {
            if (lines == null)
                return;
            foreach (string l in lines)
                tw.WriteLine(l);
        }
    }
}