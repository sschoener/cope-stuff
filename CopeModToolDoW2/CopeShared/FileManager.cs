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
using ModTool.Core.PlugIns;
using ModTool.Core.PlugIns.RelicChunky;
using ModTool.Core.PlugIns.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ModTool.Core
{
    public enum FileActionType
    {
        Save,
    }
    public class FileActionEventArgs
    {
        public FileActionEventArgs(FileActionType action, UniFile file, object tag = null)
        {
            Action = action;
            File = file;
            Tag = tag;
        }

        public object Tag
        {
            get;
            private set;
        }

        public UniFile File
        {
            get;
            private set;
        }

        public FileActionType Action { get; private set; }
    }

    public delegate void FileLoadedEventHandler(UniFile file, FileTool plugin);

    public delegate void FileActionEventHandler(object sender, FileActionEventArgs e);

    static public class FileManager
    {
        #region fields

        static FileTree s_attribTree;
        static FileTree s_dataTree;
        static readonly Dictionary<string, FileTool> s_openTools = new Dictionary<string, FileTool>();
        static bool s_allowOpeningFilesTwice = true;

        #endregion fields

        #region events

        public static event FileActionEventHandler FileSaved;
        public static event FileLoadedEventHandler FileLoaded;
        public static event NotifyEventHandler FileTreesChanged;

        #endregion events

        #region methods

        static internal void FillTrees()
        {
            LoggingManager.SendMessage("FileManager - Filling file trees");
            DateTime t1 = DateTime.Now;
            if (ModManager.ModDataArchives != null)
                s_dataTree = new FileTree(ModManager.ModDataDirectory, ModManager.ModDataArchives, "data");
            if (ModManager.ModAttribArchives != null)
                s_attribTree = new FileTree(ModManager.ModAttribDirectory, ModManager.ModAttribArchives, "attrib");
            DateTime t2 = DateTime.Now;
            LoggingManager.SendMessage("FileManager - File trees filled in" + (t2 - t1).TotalSeconds + " seconds");
            if (FileTreesChanged != null)
                FileTreesChanged();
        }

        /// <summary>
        /// Tries to load the specified UniFile and returns the proper FileTool.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="forceText">Set this to true to always use the Text-editor.</param>
        /// <returns></returns>
        static public FileTool LoadFile(UniFile file, bool forceText = false)
        {
            if (!AllowOpeningFilesTwice && s_openTools.ContainsKey(file.FilePath))
                return s_openTools[file.FilePath];

            FileTool tmp;
            byte[] stream = file.ConsumeStream();
            UniFile filecopy = new UniFile(stream);
            filecopy.FilePath = file.FilePath;

            if (forceText)
            {
                try
                {
                    tmp = new TextEditor(filecopy);
                }
                catch (Exception e)
                {
                    UIHelper.ShowError("Can't open the selected file " + file.FileName  +" as Text: " + e.Message);
                    return null;
                }
            }
            else if (FileTypeManager.FileTypes.ContainsKey(filecopy.FileExtension))
                tmp = FileTypeManager.LaunchFromExt(filecopy.FileName, filecopy);
            else
            {
                try
                {
                    tmp = new RelicChunkyViewer(filecopy);
                }
                catch
                {
                    try
                    {
                        filecopy = new UniFile(stream);
                        file.Stream.Position = 0;
                        tmp = new TextEditor(filecopy);
                    }
                    catch(Exception ex)
                    {
                        LoggingManager.SendError("Failed to open file");
                        LoggingManager.HandleException(ex);
                        file.Close();
                        UIHelper.ShowError("Can't open the selected file " + file.FileName + ", no suitable plugin found!");
                        return null;
                    }
                }
            }
            tmp.OnSaved += FileTool_OnSaved;
            if (!s_openTools.ContainsKey(file.FilePath))
                s_openTools.Add(file.FilePath, tmp);
            if (FileLoaded != null)
                FileLoaded(file, tmp);
            return tmp;
        }

        /// <summary>
        /// Loads a file from the specified path and returns the proper FileTool.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public FileTool LoadFile(string path)
        {
            if (!File.Exists(path))
                return null;
            var file = new UniFile(path, FileAccess.ReadWrite, FileShare.Read);
            return LoadFile(file);
        }

        static public FileTool LoadFile(string path, FileTypePlugin plugin)
        {
            if (!File.Exists(path))
                return null;
            var file = new UniFile(path);
            return LoadFile(file, plugin);
        }

        static public FileTool LoadFile(UniFile file, FileTypePlugin plugin)
        {
            if (!AllowOpeningFilesTwice && s_openTools.ContainsKey(file.FilePath))
                return s_openTools[file.FilePath];
            FileTool tool;
            try
            {
                tool = plugin.LoadFile(file);
            }
            catch
            {
                UIHelper.ShowError("Can't open the selected file " + file.FileName + " using " + plugin.PluginName + ", version: " + plugin.Version + ".");
                return null;
            }
            tool.OnSaved += FileTool_OnSaved;
            if (s_openTools.ContainsKey(file.FilePath))
                s_openTools.Add(file.FilePath, tool);
            if (FileLoaded != null)
                FileLoaded(file, tool);
            return tool;
        }

        static public bool CloseTool(FileTool tool)
        {
            if (tool.File == null)
            {
                LoggingManager.SendMessage(LogSystemMessageType.Error, "FileManager - The File-Property of the plugin you're trying to close returned NULL! Plugin: {0}", tool.GetType().FullName);
                UIHelper.ShowError("The File-Property of the plugin you're trying to clsoe returned NULL! Contact the creator of the plugin.");
                ModManager.RequestAppExit("FileManager - Error in plugin");
                return false;
            }
            string path = tool.File.FilePath;
            if (tool.HasChanges)
            {
                DialogResult dr = UIHelper.ShowYNQuestion("Warning", "The current file has not been saved since the last changes have been made! Save it now?");
                if (dr == DialogResult.Yes)
                {
                    tool.SaveFile();
                }
            }
            if (!tool.Close())
            {
                UIHelper.ShowWarning("The PlugIn could not be closed!");
                return false;
            }
            s_openTools.Remove(path);
            return true;
        }

        static internal void ReleaseFileTrees()
        {
            LoggingManager.SendMessage("FileManager - Releasing file trees");
            if (s_attribTree != null)
                s_attribTree.ShutDown();
            s_attribTree = null;
            if (s_dataTree != null)
                s_dataTree.ShutDown();
            s_dataTree = null;
        }

        static public FileTool GetToolByFilePath(string path)
        {
            FileTool tool;
            s_openTools.TryGetValue(path, out tool);
            return tool;
        }

        #endregion methods

        #region properties

        /// <summary>
        /// Gets the FileTree containing all the files from the Attrib-section.
        /// </summary>
        static public FileTree AttribTree
        {
            get
            {
                return s_attribTree;
            }
        }

        /// <summary>
        /// Gets the FileTree containing all the files from the Data-section.
        /// </summary>
        static public FileTree DataTree
        {
            get
            {
                return s_dataTree;
            }
        }

        /// <summary>
        /// Returns an all currently opened files.
        /// </summary>
        static public IEnumerable<string> OpenFiles
        {
            get
            {
                return s_openTools.Keys;
            }
        }

        static public bool AllowOpeningFilesTwice
        {
            get
            {
                return s_allowOpeningFilesTwice;
            }
            set
            {
                s_allowOpeningFilesTwice = value;
            }
        }

        #endregion properties

        #region eventhandlers

        static void FileTool_OnSaved(object sender, FileActionEventArgs e)
        {
            if (FileSaved != null)
                FileSaved(sender, e);
        }

        #endregion eventhandlers
    }
}