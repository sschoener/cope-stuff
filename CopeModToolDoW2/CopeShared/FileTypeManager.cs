/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using cope;
using cope.DawnOfWar2;
using cope.Extensions;
using ModTool.Core.PlugIns;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace ModTool.Core
{
    static public class FileTypeManager
    {
        /// <summary>
        /// This dictionary holds KeyValuePairs consisting of the extension of a file and the class type which handles that specific file extension.
        /// </summary>
        static readonly Dictionary<string, FileTypePlugin> s_fileTypes = new Dictionary<string, FileTypePlugin>();

        static public Dictionary<string, FileTypePlugin> FileTypes
        {
            get
            {
                return s_fileTypes;
            }
        }

        /// <summary>
        /// Launches an instance of the plugin associated with the extension of the specified file name.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="infile"></param>
        /// <exception cref="CopeException">Will throw an exception if there's no plugin for this file type.</exception>
        /// <returns></returns>
        static public FileTool LaunchFromExt(string fileName, UniFile infile)
        {
            string ext = fileName.SubstringAfterLast('.').ToLower();
            if (!s_fileTypes.ContainsKey(ext))
                throw new CopeException("Unknown file extension: " + ext);

            FileTypePlugin plugin = s_fileTypes[ext];

            try
            {
                return plugin.LoadFile(infile);
            }
            catch (Exception e)
            {
                Type t = plugin.GetType();
                LoggingManager.SendMessage("FileTypeManager - Error launching plugin " + t.FullName);
                LoggingManager.HandleException(e);
                throw new CopeException(e, "Error launching plugin " + t.FullName + ": " + e.Message);
            }
        }

        static public StringCollection GetSettings()
        {
            var target = new StringCollection();
            foreach (KeyValuePair<string, FileTypePlugin> kvp in s_fileTypes)
            {
                target.Add(kvp.Key + '=' + kvp.Value.GetType().FullName);
            }
            return target;
        }

        static public string GetPluginListing()
        {
            var plugins = new StringBuilder(s_fileTypes.Count * 32);
            foreach (FileTypePlugin ftp in s_fileTypes.Values)
            {
                Type t = ftp.GetType();
                plugins.AppendLine(t.AssemblyQualifiedName + " - " + t.FullName);
            }
            return plugins.ToString();
        }
    }
}