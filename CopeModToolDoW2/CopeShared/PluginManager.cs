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
using cope.Extensions;
using ModTool.Core.PlugIns;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ModTool.Core
{
    /// <summary>
    /// Manager for file-type plugins.
    /// </summary>
    static public class PluginManager
    {
        static string s_path;
        static internal Dictionary<string, List<FileTypePlugin>> FileTypePlugins;

        static public void LoadPlugins()
        {
            LoggingManager.SendMessage("PluginManager - Attempting to load plugins from plugins-folder " + s_path);
            FileTypePlugins = new Dictionary<string, List<FileTypePlugin>>();
            if (!Directory.Exists(s_path))
                return;

            var fileTypeDir = new DirectoryInfo(s_path + "filetypes");

            foreach (FileInfo file in fileTypeDir.GetFiles("*.dll"))
            {
                try
                {
                    AssemblyName.GetAssemblyName(file.FullName);
                    Assembly assembly = Assembly.LoadFile(file.FullName);
                    LoadPlugin(assembly);
                }
                catch (BadImageFormatException) // for non-.NET DLLs
                {
                    continue;
                }
                catch (Exception e)
                {
                    LoggingManager.SendMessage("PluginManager - Could not load plugin '" + file.FullName + "'!");
                    LoggingManager.HandleException(e);
                     UIHelper.ShowError("Could not load plugin '" + file.FullName + "': " + e.Message);
                }
            }

            foreach (var kvp in FileTypePlugins)
            {
                if (kvp.Value.Count == 1)
                    FileTypeManager.FileTypes[kvp.Key] = kvp.Value[0];
            }
            LoggingManager.SendMessage("PluginManager - Plugins successfully loaded!");
        }

        static private void LoadPlugin(Assembly assembly)
        {
            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (!type.IsSubclassOf(typeof(ModToolPlugin)))
                        continue;
                    var plugin = type.GetConstructor(Type.EmptyTypes).Invoke(null) as ModToolPlugin;
                    if (type.IsSubclassOf(typeof(FileTypePlugin)))
                    {
                        var ftp = plugin as FileTypePlugin;
                        string[] exts = ftp.FileExtensions;
                        foreach (string ext in exts)
                        {
                            if (!FileTypePlugins.ContainsKey(ext.ToLower()))
                                FileTypePlugins.Add(ext.ToLower(), new List<FileTypePlugin>());
                            FileTypePlugins[ext.ToLower()].Add(ftp);
                        }
                    }

                    var pluginMenu = new ToolStripMenuItem(plugin.PluginName) { Name = type.FullName };
                    plugin.Init(new PluginEnvironment(pluginMenu.DropDownItems,
                                ModToolEnvironment.CombinedFileTreeContext,
                                ModToolEnvironment.VirtualFileTreeContext));
                    if (pluginMenu.DropDownItems.Count > 0)
                        ModToolEnvironment.PluginMenu.Add(pluginMenu);
                    else
                        pluginMenu.Dispose();
                }
            }
            catch (ReflectionTypeLoadException e)
            {
                LoggingManager.SendMessage("PluginManager - Could not load plugin!");
                LoggingManager.SendMessage("LoaderExceptions: ");
                foreach (Exception exc in e.LoaderExceptions)
                    LoggingManager.HandleException(exc);
                LoggingManager.HandleException(e);
                 UIHelper.ShowError("Could not load plugin " + assembly.FullName + ": " +  e.Message);

            }
            catch (Exception e)
            {
                LoggingManager.SendMessage("PluginManager - Could not load plugin!");
                LoggingManager.HandleException(e);
                 UIHelper.ShowError("Could not load plugin " + assembly.FullName + ": " + e.Message);
            }
        }

        /// <summary>
        /// Loads the file-type settings from a specified location.
        /// </summary>
        /// <param name="source"></param>
        static public void LoadFileTypeSettings(StringCollection source)
        {
            if (FileTypePlugins == null)
                FileTypePlugins = new Dictionary<string, List<FileTypePlugin>>();
            if (source == null)
                return;

            foreach (string str in source)
            {
                string key = str.SubstringBeforeFirst('=');
                if (!FileTypePlugins.ContainsKey(key))
                    continue;

                string value = str.SubstringAfterFirst('=');

                foreach (FileTypePlugin tp in FileTypePlugins[key])
                {
                    if (tp.GetType().FullName.Equals(value))
                    {
                        FileTypeManager.FileTypes[key] = tp;
                        continue;
                    }
                }
            }
        }

        #region properties

        /// <summary>
        /// Paths in which to look for the plugins.
        /// </summary>
        static public string Path
        {
            get
            {
                return s_path;
            }
            set
            {
                s_path = value;
            }
        }

        #endregion properties
    }
}