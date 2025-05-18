using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using cope.DawnOfWar2;
using cope.DawnOfWar2.RelicBinary;
using cope.DawnOfWar2.SGA;
using cope.Helper;
using cope;
using cope.Script.LUA;
using LuaInterface;
using LuaTable = LuaInterface.LuaTable;

namespace RBFCompiler
{
    public class RBFCompiler
    {
        private const string VERSION = "1.23";
        protected LogSystem m_logger;
        protected ModuleFile m_baseModuleFile;
        protected Lua m_luaState;
        protected Dictionary<string, SGAFile> m_modArchives;
        protected bool m_bIgnoreArchives;
        protected bool m_bIgnoreDirectories;
        protected bool m_bReadSGAOnDemand;
        protected string m_sBaseMod;
        protected string m_sSourceDir;
        protected string m_sStartFile;
        protected string m_sTargetDir;
        protected string m_sWorkingDir = Environment.CurrentDirectory + '\\';

        public RBFCompiler(string baseMod, string targetDir, string sourceDir, string startFile)
        {
            m_sBaseMod = MakeAbsolutePath(m_sWorkingDir, baseMod);
            m_sTargetDir = MakeAbsolutePath(m_sWorkingDir, targetDir);
            m_sSourceDir = MakeAbsolutePath(m_sWorkingDir, sourceDir);
            m_sStartFile = MakeAbsolutePath(m_sSourceDir, startFile);
            m_logger = new LogSystem();
            m_logger.OnLog += Log;
            if (!m_sSourceDir.EndsWith('\\'))
            {
                m_sSourceDir = m_sSourceDir + '\\';
            }
            if (!m_sTargetDir.EndsWith('\\'))
            {
                m_sTargetDir = m_sTargetDir + '\\';
            }
        }

        //public event LogEventHandler OnLog;
        public event GenericProcessorMethod<string> OnLog;

        private void Log(string s)
        {
            if (OnLog != null)
                OnLog(s);
        }

        private void CloseAllSgas()
        {
            if ((m_modArchives != null))
            {
                foreach (SGAFile file in m_modArchives.Values)
                {
                    file.Close();
                }
            }
        }

        public bool CompileTable(LuaTable luaTable, string outputPath)
        {
            LuaValue value;
            ListDictionary tableDict = m_luaState.GetTableDict(luaTable);
            if (!IsTableValidForCompilation(tableDict))
            {
                return false;
            }
            try
            {
                value = LuaInterfaceBridge.ToCopeLua(m_luaState, luaTable);
            }
            catch (Exception ex)
            {
                m_logger.SendError("Failed to parse LUA table");
                m_logger.HandleException(ex);
                return false;
            }
            var file = new RelicBinaryFile(value);
            try
            {
                outputPath = MakeAttribPathOutput(outputPath);
                file.WriteDataTo(outputPath);
            }
            catch (Exception ex)
            {
                m_logger.SendError("Failed to save RBF file!");
                m_logger.HandleException(ex);
                return false;
            }
            m_logger.SendMessage("Saved RBF file: \"{0}\"", outputPath);
            return true;
        }

        /// <exception cref="Exception">Execution stopped by user!</exception>
        public void CompilerEnd()
        {
            throw new Exception("Execution stopped by user!");
        }

        protected bool ExecuteReader(TextReader textReader, string chunkName)
        {
            string str;
            PrepareLuaState();
            while ((str = textReader.ReadLine()) == null)
            {
                try
                {
                    m_luaState.DoString(str, chunkName);
                    continue;
                }
                catch (LuaException ex)
                {
                    m_logger.SendError("Failed to execute: " + str);
                    m_logger.HandleException(ex);
                    textReader.Close();
                    return false;
                }
            }
            textReader.Close();
            return true;
        }

        public string GetOutputDir()
        {
            return m_sTargetDir;
        }

        public string GetSourceDir()
        {
            return m_sSourceDir;
        }

        public string GetWorkingDir()
        {
            return m_sWorkingDir;
        }

        public void IgnoreArchives(bool enable)
        {
            m_bIgnoreArchives = enable;
        }

        public void IgnoreFolders(bool enable)
        {
            m_bIgnoreDirectories = enable;
        }

        private bool IsTableValidForCompilation(ListDictionary table)
        {
            if (table == null)
            {
                m_logger.SendError("The specified Lua table is invalid or does not exist at all!");
                return false;
            }
            if (table.Keys.Count == 0)
            {
                m_logger.SendError("The specified Lua table does not have any entries!");
                return false;
            }
            if (!table.Contains("GameData"))
            {
                m_logger.SendError("The specified Lua table misses a GameData table!");
                return false;
            }
            return true;
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        protected RelicBinaryFile LoadBaseRBF(string path)
        {
            if (!path.EndsWith(".rbf"))
            {
                path = path + ".rbf";
            }
            string[] strArray;
            if (path.StartsWith("::"))
            {
                strArray = MakePotentialAttribPaths(path);
            }
            else if (path.Contains(':'))
            {
                strArray = new[] {path};
            }
            else
            {
                strArray = new[] {m_sSourceDir + path};
            }
            foreach (string str in strArray)
            {
                if (File.Exists(str))
                {
                    try
                    {
                        var file = new RelicBinaryFile(str);
                        file.ReadData();
                        file.Close();
                        return file;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(string.Format("Found a base file {0} but can't read it! Error: {1}", str,
                                                          exception.Message));
                    }
                }
                if (str.StartsWith("::"))
                {
                    return LoadBaseRBFSGA(str);
                }
            }
            return null;
        }

        protected RelicBinaryFile LoadBaseRBFSGA(string sgaFilePath)
        {
            string path = sgaFilePath.SubstringAfterFirst("::");
            foreach (SGAFile file in m_modArchives.Values)
            {
                if (!((bool) file.Tag))
                {
                    m_logger.SendMessage("Reading archive \"" + file.FilePath + "\" ...");
                    try
                    {
                        file.ReadData();
                    }
                    catch (Exception ex)
                    {
                        file.Close();
                        m_logger.SendError("Failed to read archive \"" + file.FilePath + "\"!)");
                        m_logger.HandleException(ex);
                        return null;
                    }
                    file.Tag = true;
                }
                foreach (SGAEntryPoint point in file)
                {
                    if (point.Alias == "Attributes")
                    {
                        SGAStoredFile fileFromPath = point.GetFileFromPath(path);
                        if (fileFromPath != null)
                        {
                            byte[] data = file.ExtractFile(fileFromPath, file.Stream);
                            try
                            {
                                var file2 = new RelicBinaryFile(data);
                                file2.ReadData();
                                file2.Close();
                                return file2;
                            }
                            catch (Exception ex)
                            {
                                m_logger.SendError("Found base file \"" + path + "\" in archive \"" + file.FilePath + "\" but couldn't read it (info below). Continuing the search anyway...");
                                m_logger.HandleException(ex);
                                return null;
                            }
                        }
                    }
                }
            }
            m_logger.SendError("Couldn't find file \"" + path + "\" in any archive!");
            return null;
        }

        public LuaTable LoadRBF(string path)
        {
            RelicBinaryFile file = LoadBaseRBF(path);
            if (file == null)
            {
                return null;
            }
            return LuaInterfaceBridge.ToInterface(m_luaState, file.Root, "___cope___rbf___");
        }

        private Dictionary<string, SGAFile> LoadSGAArchives()
        {
            if (m_bIgnoreArchives)
            {
                return new Dictionary<string, SGAFile>(0);
            }
            var sectionByName = (ModuleFile.ModuleSectionFileList) m_baseModuleFile["attrib:common"];
            var dictionary = new Dictionary<string, SGAFile>(sectionByName.ArchiveCount);
            for (int i = 0; i < sectionByName.ArchiveCount; i++)
            {
                SGAFile file;
                string filePath = m_sBaseMod.SubstringBeforeLast('\\', true) + sectionByName.GetArchiveByIndex(i);
                m_logger.SendMessage("Loading archive \"" + filePath + "\"");
                try
                {
                    file = new SGAFile(filePath);
                }
                catch (Exception ex)
                {
                    m_logger.SendError("Failed to load archive \"" + filePath +"\"");
                    m_logger.HandleException(ex);
                    return null;
                }
                try
                {
                    if (!m_bReadSGAOnDemand)
                    {
                        m_logger.SendMessage("Reading archive \"" + filePath + "\"");
                        file.ReadData();
                        file.Tag = true;
                    }
                    else
                    {
                        file.Tag = false;
                    }
                }
                catch (Exception ex)
                {
                    m_logger.SendError("Failed to read archive \"" + filePath + "\"");
                    m_logger.HandleException(ex);
                    return null;
                }
                dictionary.Add(sectionByName.GetArchiveByIndex(i), file);
            }
            m_logger.SendMessage("Finished loading base mod's archives: " + dictionary.Count + " archive(s) loaded");
            return dictionary;
        }

        protected string MakeAbsolutePath(string anchor, string path)
        {
            if (!path.Contains(':'))
            {
                path = anchor + path;
            }
            return path;
        }

        protected string MakeAttribPathOutput(string filePath)
        {
            if (!filePath.EndsWith(".rbf"))
            {
                filePath = filePath + ".rbf";
            }
            if (filePath.Contains(':'))
            {
                return filePath;
            }
            return (m_sTargetDir + filePath);
        }

        protected string[] MakePotentialAttribPaths(string filePath)
        {
            if (!filePath.EndsWith(".rbf"))
            {
                filePath = filePath + ".rbf";
            }
            var sectionByName = (ModuleFile.ModuleSectionFileList) m_baseModuleFile["attrib:common"];
            int index = 1;
            if (!m_bIgnoreDirectories)
            {
                index += sectionByName.FolderCount;
            }
            var strArray = new string[index];
            index = 0;
            if (!m_bIgnoreDirectories)
            {
                for (int i = 0; i < sectionByName.FolderCount; i++)
                {
                    string[] strArray3;
                    IntPtr ptr2;
                    strArray[index] = m_sBaseMod.SubstringBeforeLast('\\', true) + sectionByName.GetFolderByIndex(i);
                    if (!strArray[index].EndsWith('\\'))
                    {
                        string[] strArray2;
                        IntPtr ptr;
                        (strArray2 = strArray)[(int) (ptr = (IntPtr) index)] = strArray2[(int) ptr] + '\\';
                    }
                    (strArray3 = strArray)[(int) (ptr2 = (IntPtr) index)] = strArray3[(int) ptr2] +
                                                                            filePath.SubstringAfterFirst("::");
                    index++;
                }
            }
            strArray[index] = filePath;
            return strArray;
        }

        private void PrepareLuaState()
        {
            m_luaState.RegisterFunction("print", this, GetType().GetMethod("Print"));
            m_luaState.RegisterFunction("compiler_end", this, GetType().GetMethod("CompilerEnd"));
            m_luaState.RegisterFunction("compiler_ignore_folders", this, GetType().GetMethod("IgnoreFolders"));
            m_luaState.RegisterFunction("compiler_load_rbf", this, GetType().GetMethod("LoadRBF"));
            m_luaState.RegisterFunction("compiler_make_rbf", this, GetType().GetMethod("CompileTable"));
            m_luaState.RegisterFunction("compiler_rbf_to_lua", this, GetType().GetMethod("WriteRBFLUAToPath"));
            m_luaState.RegisterFunction("compiler_set_working_dir", this, GetType().GetMethod("SetWorkingDir"));
            m_luaState.RegisterFunction("compiler_set_output_dir", this, GetType().GetMethod("SetOutputDir"));
            m_luaState.RegisterFunction("compiler_set_base_mod", this, GetType().GetMethod("SetBaseMod"));
            m_luaState.RegisterFunction("compiler_get_output_dir", this, GetType().GetMethod("GetOutputDir"));
            m_luaState.RegisterFunction("compiler_get_source_dir", this, GetType().GetMethod("GetSourceDir"));
            m_luaState.RegisterFunction("compiler_get_working_dir", this, GetType().GetMethod("GetWorkingDir"));
        }

        private ModuleFile PrepareModuleFile()
        {
            ModuleFile file = null;
            try
            {
                file = new ModuleFile(m_sBaseMod);
            }
            catch (Exception ex)
            {
                if (file != null) file.Close();
                m_logger.SendError("Could not open specified module file: \"" + m_sBaseMod + "\"");
                m_logger.HandleException(ex);
                return null;
            }
            file.Close();
            if (!file.HasSection("attrib:common"))
            {
                m_logger.SendError("The specified module file doesn't have an [attrib:common] section!");
                return null;
            }
            return file;
        }

        public void Print(object o)
        {
            Log(o.ToString());
        }

        /// <exception cref="Exception">ERROR - execution stopped!</exception>
        public void SetBaseMod(string path)
        {
            path = MakeAbsolutePath(m_sWorkingDir, path);
            if (!File.Exists(path))
            {
                m_logger.SendError("Setting the base mod's module file to \"" + path + "\" failed -- bad path!");
                throw new Exception(@"ERROR - execution stopped!");
            }
            m_logger.SendMessage("Setting the base mod's module file to \"" + path + "\" and reloading the archives");
            m_sBaseMod = path;
            CloseAllSgas();
            PrepareModuleFile();
            m_modArchives = LoadSGAArchives();
        }

        /// <exception cref="Exception">ERROR - execution stopped!</exception>
        public void SetOutputDir(string directoryPath)
        {
            if (!directoryPath.EndsWith('\\'))
            {
                directoryPath = directoryPath + '\\';
            }
            if (!directoryPath.Contains(':'))
            {
                directoryPath = m_sWorkingDir + directoryPath;
            }
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    m_logger.SendMessage("The specified output directory does not exists, creating it: \"" + m_sTargetDir + "\"");
                    Directory.CreateDirectory(directoryPath);
                }
            }
            catch (Exception ex)
            {
                m_logger.SendError("Setting the output directory to \"" + directoryPath + "\" failed -- bad path!");
                m_logger.HandleException(ex);
                throw new Exception("ERROR - execution stopped!");
            }
            m_sTargetDir = directoryPath;
        }

        /// <exception cref="Exception">ERROR - execution stopped!</exception>
        public void SetWorkingDir(string directoryPath)
        {
            if (!directoryPath.EndsWith('\\'))
            {
                directoryPath = directoryPath + '\\';
            }
            if (!directoryPath.Contains(':'))
            {
                m_logger.SendError("Setting the working directory to \"" + directoryPath + "\" failed -- bad path!");
                throw new Exception("ERROR - execution stopped!");
            }
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }
            catch (Exception ex)
            {
                m_logger.SendError("Setting the working directory to \"" + directoryPath + "\" failed -- bad path!");
                m_logger.HandleException(ex);
                throw new Exception("ERROR - execution stopped!");
            }
            m_sWorkingDir = directoryPath;
        }

        /// <exception cref="Exception">No logging system initialized!</exception>
        public bool Start()
        {
            if (OnLog == null)
            {
                throw new Exception("No logging system initialized!");
            }
            m_logger.SendMessage("Cope's RBF Compiler v" + VERSION + " -- starting");
            bool flag = false;
            if (!Directory.Exists(m_sSourceDir))
            {
                m_logger.SendError("The specified source directory could not be found: \"" + m_sSourceDir + "\"");
                flag = true;
            }
            if (!File.Exists(m_sBaseMod))
            {
                m_logger.SendError("The specified base module file could not be found: \"" + m_sBaseMod +"\"");
                flag = true;
            }
            if (flag)
            {
                m_logger.SendError("Execution stopped!");
                return false;
            }
            if (!Directory.Exists(m_sTargetDir))
            {
                m_logger.SendWarning("The specified output directory does not exists, creating it: \"" + m_sTargetDir + "\"");
                Directory.CreateDirectory(m_sTargetDir);
            }
            DateTime beginning = DateTime.Now;
            m_logger.SendMessage("Beginning execution");
            m_luaState = new Lua();
            PrepareLuaState();
            if ((((m_baseModuleFile = PrepareModuleFile()) == null) || ((m_modArchives = LoadSGAArchives()) == null)) ||
                !StartLuaReader())
            {
                CloseAllSgas();
                m_logger.SendError("Execution stopped!");
                return false;
            }
            CloseAllSgas();
            TimeSpan span = (DateTime.Now - beginning);
            m_logger.SendMessage("Finished compilation! Time needed: " + span.TotalSeconds + " seconds");
            return true;
        }

        protected bool StartLuaReader()
        {
            try
            {
                m_luaState.DoFile(m_sStartFile);
            }
            catch (Exception ex)
            {
                m_logger.SendError("Could not execute Lua script");
                m_logger.HandleException(ex);
                return false;
            }
            return true;
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public void WriteRBFLUAToPath(string rbfPath, string outputFile)
        {
            RelicBinaryFile file = LoadBaseRBF(rbfPath);
            if (file == null)
            {
                throw new Exception("Could not find the requested file \"" + rbfPath + "\"");
            }
            if (!outputFile.Contains(':'))
            {
                outputFile = m_sTargetDir + outputFile;
            }
            if (!rbfPath.EndsWith(".rbf"))
            {
                rbfPath = rbfPath + ".rbf";
            }
            List<string> list =
                file.Root.GetLuaStringRecursive(rbfPath.SubstringAfterLast('\\').SubstringBeforeLast('.', true), string.Empty,
                                                true);
            list.Insert(0, rbfPath.SubstringAfterLast('\\').SubstringBeforeLast('.') + " = { }");
            list.Insert(1, rbfPath.SubstringAfterLast('\\').SubstringBeforeLast('.', true) + "_TYPE = { }");
            list.Insert(0, "-- LUA dump of " + rbfPath);
            list.Insert(0, "-- generated by Cope's RBF Compiler");
            list.Sort();
            File.WriteAllLines(outputFile, list.ToArray());
            m_logger.SendMessage("Converted RBF to LUA file: \"" + outputFile + "\"");
        }
    }
}