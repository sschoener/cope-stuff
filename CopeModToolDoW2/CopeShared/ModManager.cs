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
using cope.DawnOfWar2.RelicBinary;
using cope.DawnOfWar2.SGA;
using cope.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ModTool.Core
{
    static public class ModManager
    {
        #region fields

        private static string s_sGameDir;
        private static string s_sModFolder;
        private static string s_sLanguage;

        static ModManager()
        {
            ModAttribArchives = new List<SGAFile>();
            ModDataArchives = new List<SGAFile>();
            Application.ApplicationExit += OnApplicationExit;
        }

        #endregion fields

        #region eventhandlers

        static void OnApplicationExit(object sender, EventArgs e)
        {
            if (ApplicationExit != null)
                ApplicationExit();
            CloseAll();
        }

        #endregion

        #region events

        public static event NotifyEventHandler GameDirectoryChanged;
        public static event NotifyEventHandler ModLoaded;
        public static event NotifyEventHandler LoadingFailed;
        public static event NotifyEventHandler ModUnloaded;
        public static event NotifyEventHandler SGALoaded;
        public static event GenericHandler<string> ModLanguageChanged;

        public static event NotifyEventHandler ApplicationExit;
        public static event GenericHandler<string> ApplicationExitRequested;

        #endregion events

        #region properties

        public static IRBFKeyProvider RBFKeyProvider { get; internal set; }

        public static string ModAttribDirectory { get; internal set; }

        public static string ModDataDirectory { get; internal set; }

        public static string GameDirectory
        {
            get
            {
                return s_sGameDir;
            }
            set
            {
                if (!value.EndsWith('\\'))
                    value += '\\';
                s_sGameDir = value;
                LoggingManager.SendMessage("ModManager - GameDirectory changed to " + value);
                ToolSettings.IsInRetributionMode = value.ToLowerInvariant().Contains("retribution");
                if (GameDirectoryChanged != null)
                    GameDirectoryChanged();
            }
        }

        public static ModuleFile ModuleFile { get; private set; }

        public static string ModName { get; set; }

        public static string ModFolder
        {
            get
            {
                return s_sModFolder;
            }
        }

        public static List<SGAFile> ModDataArchives { get; internal set; }

        public static List<SGAFile> ModAttribArchives { get; internal set; }

        public static string Language
        {
            get { return s_sLanguage; }
            set
            {
                if (value != s_sLanguage)
                {
                    s_sLanguage = value;
                    LoggingManager.SendMessage("ModManager - Language set to " + value);
                    if (ModLanguageChanged != null)
                        ModLanguageChanged(null, value);
                }
            }
        }

        public static bool IsModLoaded
        {
            get
            {
                return ModuleFile != null;
            }
        }

        public static bool IsAnythingLoaded
        {
            get
            {
                return ModuleFile != null ||
                    (ModAttribArchives != null && ModAttribArchives.Count != 0) || 
                    (ModDataArchives != null && ModDataArchives.Count != 0);
            }
        }

        #endregion properties

        #region methods

        // Todo: clean up this method! it's a mess!
        /// <exception cref="CopeDoW2Exception">The global section of the selected module file is invalid!</exception>
        static public void LoadModule(string path)
        {
            
            if (IsAnythingLoaded)
                CloseAll();
            LoggingManager.SendMessage("ModManager - Loading module file " + path);
            GameDirectory = path.SubstringBeforeLast('\\', true);
            DateTime t1 = DateTime.Now;

            ModuleFile = new ModuleFile(path);

            // get basic mod information from module file
            try
            {
                ModAttribDirectory = path.SubstringBeforeLast('\\', true) +
                                     ((ModuleFile.ModuleSectionFileList) ModuleFile["attrib:common"]).GetFolderByIndex(0) +
                                     '\\';
                ModDataDirectory = path.SubstringBeforeLast('\\', true) +
                                   ((ModuleFile.ModuleSectionFileList) ModuleFile["data:common"]).GetFolderByIndex(0) +
                                   '\\';
                var globalSection = ModuleFile["global"] as ModuleFile.ModuleSectionKeyValue;
                if (globalSection != null)
                {
                    ModName = globalSection["Name"];
                    s_sModFolder = globalSection["ModFolder"];
                    if (globalSection.KeyExists("UCSBaseIndex"))
                        UCSManager.NextIndex = uint.Parse(globalSection["UCSBaseIndex"]);
                }
                else throw new CopeDoW2Exception("The global section of the selected module file is invalid!");
            }
            catch (CopeDoW2Exception e)
            {
                LoggingManager.SendError("ModManager - Error loading module file: " + e.Message);
                LoggingManager.HandleException(e);
                UIHelper.ShowError("Error loading module file " + path.SubstringAfterLast('\\') + ": " +
                                          e.Message + ". See log file for more information.");
                ModuleFile.Close();
                if (LoadingFailed != null)
                    LoadingFailed();
                return;
            }

            if (ModAttribDirectory == ModDataDirectory)
            {
                LoggingManager.SendError(
                    "ModManager - The data:common directory and the attrib:common directory of the mod overlap! May cause serious trouble!");
                UIHelper.ShowError(
                    "The data:common directory and the attrib:common directory of the mod overlap! This tool does NOT support such module files." +
                    "Please correct the module file before loading it, read the user guide for help.");
                ModuleFile.Close();
                if (LoadingFailed != null)
                    LoadingFailed();
                return;
            }

            // load mod data
            if (ModDataArchives == null)
                ModDataArchives = new List<SGAFile>();
            else
                ModDataArchives.Clear();

            if (ModAttribArchives == null)
                ModAttribArchives = new List<SGAFile>();
            else
                ModAttribArchives.Clear();

            LoggingManager.SendMessage("ModManager - Loading mod resources");
            List<SGAFile> currentArchives;
            foreach (ModuleFile.ModuleSection ms in ModuleFile)
            {
                if (ms is ModuleFile.ModuleSectionFileList && ms.SectionName == "attrib:common")
                {
                    currentArchives = ModAttribArchives;
                }
                else if (ms is ModuleFile.ModuleSectionFileList && ms.SectionName == "data:common")
                {
                    currentArchives = ModDataArchives;
                }
                else
                    continue;

                var tmp = (ModuleFile.ModuleSectionFileList)ms;
                for (int i = 0; i < tmp.ArchiveCount; i++)
                {
                    if (!File.Exists(ModuleFile.FilePath.SubstringBeforeLast('\\', true) + tmp.GetArchiveByIndex(i)))
                        continue;

                    try
                    {
                        currentArchives.Add(
                            new SGAFile(ModuleFile.FilePath.SubstringBeforeLast('\\', true) + tmp.GetArchiveByIndex(i),
                                        FileAccess.Read, FileShare.Read));
                    }
                    catch (Exception e)
                    {
                        LoggingManager.SendError("ModManager - Error loading SGA-file '" + tmp.GetArchiveByIndex(i) +
                                                   "':" + e.Message);
                        LoggingManager.HandleException(e);
                        UIHelper.ShowError("Error loading SGA-files: " + e.Message +
                                                  " See log file for more information.");
                        ModuleFile.Close();
                        if (LoadingFailed != null)
                            LoadingFailed();
                        return;
                    }
                }
            }

            ModuleFile.Close();
            DateTime t2 = DateTime.Now;
            LoggingManager.SendMessage("ModManager - Module file successfully loaded in " + (t2 - t1).TotalSeconds +
                                       " seconds");
            FileManager.FillTrees();

            TryToGetKeyProvider();
            if (ModLoaded != null)
                ModLoaded();
        }

        static private void TryToGetKeyProvider()
        {
            const string flbPath = "simulation\\attrib\\fieldnames.flb";
            if (!ToolSettings.IsInRetributionMode)
            {
                LoggingManager.SendMessage("ModManager - not in Retribuion-mode, no need to load KeyProvider");
                return;
            }
            LoggingManager.SendMessage("ModManager - Trying to load the KeyProvider (FLB-file)");
            try
            {
                FSNodeFile file = FileManager.AttribTree.RootNode.GetFileByPath(flbPath);
                if (file == null)
                {
                    UIHelper.ShowWarning("Unable to find the FLB file! You probably won't be able to open RBFs.");
                    LoggingManager.SendWarning("ModManager - Failed to load FLB-file from path " + flbPath);
                    return;
                }
                UniFile uni = file.GetUniFile();
                var flbFile = new FieldNameFile(uni);
                flbFile.ReadData();
                RBFKeyProvider = flbFile;
            }
            catch (Exception ex)
            {
                UIHelper.ShowError("Unable to open or read the FLB file! You probably won't be able to open RBFs.");
                LoggingManager.SendError("ModManager - Error while loading FLB-file");
                LoggingManager.HandleException(ex);
                return;
            }
            LoggingManager.SendMessage("ModManager - Successfully loaded KeyProvider from " + flbPath);
        }

        static public void LoadSGAFile(string path)
        {
            CloseAll();

            LoggingManager.SendMessage("ModManager - Loading SGA file " + path);
            DateTime t1 = DateTime.Now;

            // the tool needs some paths to work with but SGAs of course don't provide these
            // so you need to set them up manually
            ModAttribDirectory = path.SubstringBeforeLast('.') + "\\ATTRIB\\";
            ModDataDirectory = path.SubstringBeforeLast('.') + "\\DATA\\";
            SGAFile sga;
            try
            {
                sga = new SGAFile(path, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                LoggingManager.SendError("ModManager - Failed to load SGA file!");
                LoggingManager.HandleException(e);
                UIHelper.ShowError("Can't open SGA file: " + e.Message + " See log file for more information");
                if (LoadingFailed != null)
                    LoadingFailed();
                return;
            }

            ModAttribArchives.Add(sga);
            ModDataArchives.Add(sga);
            ModName = path.SubstringAfterLast('\\');

            DateTime t2 = DateTime.Now;
            LoggingManager.SendMessage("ModManager - SGA file successfully loaded in " + (t2 - t1).TotalSeconds + " seconds");
            FileManager.FillTrees();

            TryToGetKeyProvider();
            if (SGALoaded != null)
                SGALoaded();
        }

        static public void CloseAll()
        {
            LoggingManager.SendMessage("ModManager - Closing current mod (if any is loaded)");
            if (ModuleFile != null)
            {
                ModuleFile.Close();
                ModuleFile = null;
            }

            if (ModAttribArchives != null)
            {
                foreach (SGAFile sga in ModAttribArchives)
                    sga.Close();
                ModAttribArchives.Clear();
            }

            if (ModDataArchives != null)
            {
                foreach (SGAFile sga in ModDataArchives)
                    sga.Close();
                ModDataArchives.Clear();
            }

            FileManager.ReleaseFileTrees();
            if (ModUnloaded != null)
                ModUnloaded();

            ModName = null;
            // normally it isn't a good idea to manually invoke the GarbageCollection but in this case we're releasing
            // gigabytes of resources; the overall performance greatly benefits from this 
            LoggingManager.SendMessage("ModManager - Manual GarbageCollection in all generations in progess...");
            GC.Collect();
        }

        static public void RequestAppExit(string reason)
        {
            if (ApplicationExitRequested != null)
                ApplicationExitRequested(null, reason);
        }

        static public string GetDebugInfo()
        {
            var info = new StringBuilder(300);

            if (ModuleFile == null)
                return "No mod loaded";
            info.AppendLine("Mod DATA directory: " + ModDataDirectory);
            info.AppendLine("Mod ATTRIB directory: " + ModAttribDirectory);

            if (ModDataArchives != null)
            {
                info.AppendLine("Mod DATA archives:");
                foreach (SGAFile sga in ModDataArchives)
                {
                    info.AppendLine(sga.FileName);
                }
            }
            else
                info.AppendLine("No DATA archives");

            if (ModAttribArchives != null)
            {
                info.AppendLine("Mod ATTRIB archives:");
                foreach (SGAFile sga in ModAttribArchives)
                {
                    info.AppendLine(sga.FileName);
                }
            }
            else
                info.AppendLine("No ATTRIB archives");
            return info.ToString();
        }

        #endregion methods
    }
}