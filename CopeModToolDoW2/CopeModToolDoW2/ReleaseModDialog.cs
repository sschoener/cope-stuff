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
using Microsoft.CSharp;
using ModTool.Core;
using ModTool.FE.Properties;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ModTool.FE
{
    // NOTE: this file still is incredibly messy. Clean it up!
    public partial class ReleaseModDialog : Form
    {
        public ReleaseModDialog()
        {
            InitializeComponent();
            if (Settings.Default.sLastReleaseDir != null)
                m_tbxOutputDir.Text = Settings.Default.sLastReleaseDir;
        }

        private void BtnSelectOutputDirClick(object sender, EventArgs e)
        {
            if (m_dlgSelectOutputDir.ShowDialog() == DialogResult.OK)
            {
                m_tbxOutputDir.Text = m_dlgSelectOutputDir.SelectedPath;
            }
        }

        private void SetStatusCallback(string status)
        {
            if (InvokeRequired)
                Invoke(new Action<string>(SetStatus), status);
            else
                SetStatus(status);
        }

        private void SetStatus(string status)
        {
            m_labStatus.Text = status;
        }

        private void BtnReleaseClick(object sender, EventArgs e)
        {
            Settings.Default.sLastReleaseDir = m_tbxOutputDir.Text;
            var releaser = new ModReleaseUI(m_tbxOutputDir.Text, m_tbxIconPath.Text, m_chkbxInstallInstructions.Checked,
                                            m_chkbxCreateLauncher.Checked, m_radRetribution.Checked);
            bool success = releaser.ReleaseMod(SetStatusCallback);
            m_labStatus.Text = @"Ready";
            if (!success)
            {
                return;
            }
            DialogResult openOutputDir = UIHelper.ShowYNQuestion("Finished!", "Mod packed! Open the output directory now?");
            if (openOutputDir == DialogResult.Yes)
                Process.Start(m_tbxOutputDir.Text);
            Close();
            Dispose();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }

        private void ChkbxCreateLauncherCheckedChanged(object sender, EventArgs e)
        {
            m_tbxIconPath.Enabled = !m_tbxIconPath.Enabled;
            m_btnSelectIcon.Enabled = !m_btnSelectIcon.Enabled;
        }

        private void BtnSelectIconClick(object sender, EventArgs e)
        {
            if (m_dlgSelectIcon.ShowDialog() == DialogResult.OK)
                m_tbxIconPath.Text = m_dlgSelectIcon.FileName;
        }
    }

    class ModReleaser
    {
        private readonly string m_sArchiveDirectory;
        private readonly string m_sIconPath;
        private readonly string m_sOutputDirectory;
        private readonly string m_sModName;
        private readonly string m_sModFolderName;

        public ModReleaser(string modName, string modFolderName, string archivePath, string outputPath, string iconPath)
        {
            m_sModName = modName;
            m_sModFolderName = modFolderName;
            m_sArchiveDirectory = archivePath;
            m_sIconPath = iconPath;
            m_sOutputDirectory = outputPath;
        }

        public void CreateDirectories()
        {
            try
            {
                if (!Directory.Exists(m_sArchiveDirectory))
                    Directory.CreateDirectory(m_sArchiveDirectory);
                if (!Directory.Exists(m_sOutputDirectory))
                    Directory.CreateDirectory(m_sOutputDirectory);
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("ModPacker - Failed to create mod directories");
                LoggingManager.HandleException(ex);
            }
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public void CreateLauncher(bool retributionLauncher)
        {
            LoggingManager.SendMessage("ModPacker - Generating launcher for mod " + ModManager.ModName);

            string outputPath = m_sOutputDirectory + "Launch " + ModManager.ModName + ".exe";
            CodeDomProvider provider = null;
            try
            {
                var provOpts = new Dictionary<string, string> { { "CompilerVersion", "v2.0" } };
                provider = new CSharpCodeProvider(provOpts);
                var compilerParams = new CompilerParameters
                {
                    GenerateExecutable = true,
                    IncludeDebugInformation = false,
                    OutputAssembly = outputPath
                };
                compilerParams.ReferencedAssemblies.Add("System.dll");

                // set an icon if the user wishes to include one
                if (m_sIconPath != string.Empty && File.Exists(m_sIconPath))
                    compilerParams.CompilerOptions = "/win32icon:\"" + m_sIconPath + "\"";
                string tmp = Resources.CrudeLauncherSource.Replace("_MODNAME_", ModManager.ModName);
                tmp = tmp.Replace("_APPID_", retributionLauncher ? GameConstants.RETRIBUTION_APP_ID : GameConstants.CR_APP_ID);
                provider.CompileAssemblyFromSource(compilerParams, tmp);
                provider.Dispose();
            }
            catch (Exception ex)
            {
                if (provider != null)
                    provider.Dispose();
                LoggingManager.SendMessage("ModPacker - Failed to create launcher for mod " + ModManager.ModName);
                LoggingManager.HandleException(ex);
                throw;
            }
            LoggingManager.SendMessage("ModPacker - Successfully created launcher for mod " + ModManager.ModName);
        }

        public void IncludeAdditionalArchives()
        {
            // see if there are any more archives in the current mod directory and copy them to the output path
            LoggingManager.SendMessage("ModPacker - Looking for additional archives...");
            string currentArchiveDirectory = ModManager.GameDirectory + ModManager.ModFolder + "\\Archives\\";

            try
            {
                if (Directory.Exists(currentArchiveDirectory))
                {
                    IEnumerable<string> archives = Directory.EnumerateFiles(currentArchiveDirectory);
                    foreach (string archive in archives)
                    {
                        string outputArchivePath = m_sArchiveDirectory + archive.SubstringAfterLast('\\');

                        // check if the archive already exists in the output directory and only continue if it is outdated
                        if (File.Exists(outputArchivePath) &&
                            File.GetLastWriteTime(outputArchivePath) > File.GetLastWriteTime(archive))
                            continue;

                        File.Copy(archive, outputArchivePath, true);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("ModPacker - failed to copy additional archives to output directory");
                LoggingManager.HandleException(ex);
                throw ex;
            }
        }

        public void WriteModuleFile()
        {
            ModuleFile releaseModule = ModManager.ModuleFile.GClone();

            if (File.Exists(GetAttribArchivePath()))
            {
                string attribPath = GetAttribArchivePath().SubstringAfterFirst(m_sOutputDirectory);
                AddArchiveToSection(releaseModule, "attrib:common", attribPath);
            }

            if (File.Exists(GetDataArchivePath()))
            {
                string dataPath = GetDataArchivePath().SubstringAfterFirst(m_sOutputDirectory);
                AddArchiveToSection(releaseModule, "data:common", dataPath);
            }

            releaseModule.WriteDataTo(m_sOutputDirectory + m_sModName + ".module");
        }
        
        public void WriteInstallationInstructions(bool retributionLauncher)
        {
            LoggingManager.SendMessage("ModPacker - Generating installation instructions for mod " + m_sModName);
            StreamWriter installInstructions = null;
            try

            {
                installInstructions = File.CreateText(m_sOutputDirectory + "Installation.txt");
                installInstructions.WriteLine(string.Format(Resources.InstallationInstructions, m_sModName,
                                                            m_sModFolderName,
                                                            retributionLauncher ? "Retribution" : string.Empty,
                                                            retributionLauncher ? "ii - retribution" : "2"));
                installInstructions.Flush();
                installInstructions.Close();
                installInstructions.Dispose();
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("ModPacker - Failed to create installation instructions for mod " +
                                           m_sModName);
                LoggingManager.HandleException(ex);
                if (installInstructions != null)
                {
                    installInstructions.Close();
                    installInstructions.Dispose();
                }
                throw ex;
            }
            LoggingManager.SendMessage("ModPacker - Successfully created installation instructions for mod " +
                                        m_sModName);
        }

        public string GetAttribArchivePath()
        {
            return m_sArchiveDirectory + m_sModName + "_attrib.sga";
        }

        public string GetDataArchivePath()
        {
            return m_sArchiveDirectory + m_sModName + "_data.sga";
        }

        #region static helpers

        private static void AddArchiveToSection(ModuleFile moduleFile, string sectionName, string archivePath)
        {
            ModuleFile.ModuleSectionFileList moduleSection;
            if (!moduleFile.HasSection(sectionName))
            {
                moduleSection = new ModuleFile.ModuleSectionFileList(sectionName);
                moduleFile.AddSection(moduleSection);
            }
            else
                moduleSection = moduleFile[sectionName] as ModuleFile.ModuleSectionFileList;
            moduleSection.AddArchive(archivePath, true);
        }

        #endregion
    }

    class ModReleaseUI
    {
        private string m_sOutputDirectory;
        private readonly string m_sIconPath;
        private readonly string m_sArchiveDirectory;
        private readonly string m_sModFolderName;
        private readonly bool m_bCreateInstructions;
        private readonly bool m_bCreateLauncher;
        private readonly bool m_bLauncherForRetribution;
        private readonly ModReleaser m_releaser;

        private readonly object m_packerLock = new object();
        private bool m_bAttribPackerFinished;
        private bool m_bDataPackerFinished;

        public ModReleaseUI(string outputDirectory, string iconPath, bool createInstructions, bool createLauncher, bool retributionLauncher)
        {
            m_sOutputDirectory = outputDirectory;
            if (!m_sOutputDirectory.EndsWith('\\'))
                m_sOutputDirectory += '\\';
            m_sIconPath = iconPath;
            m_bCreateInstructions = createInstructions;
            m_bCreateLauncher = createLauncher;
            m_bLauncherForRetribution = retributionLauncher;
            m_sModFolderName = (ModManager.ModuleFile["global"] as ModuleFile.ModuleSectionKeyValue)["ModFolder"];
            m_sArchiveDirectory = m_sOutputDirectory + m_sModFolderName + "\\Archives\\";
            m_releaser = new ModReleaser(ModManager.ModName, m_sModFolderName, m_sArchiveDirectory, m_sOutputDirectory,
                                         m_sOutputDirectory);
        }

        private bool ArePathsValid()
        {
            if (m_sOutputDirectory == string.Empty)
            {
                 UIHelper.ShowError("Invalid output path!");
                return false;
            }
            if (m_sIconPath != string.Empty && !File.Exists(m_sIconPath))
            {
                 UIHelper.ShowError("The selected icon file does not exist!");
                return false;
            }
            if (!m_sOutputDirectory.EndsWith('\\'))
                m_sOutputDirectory += '\\';
            return true;
        }

        public bool ReleaseMod(Action<string> statusCallback)
        {
            if (!ArchiveToolHelper.IsArchiveToolPresent())
            {
                LoggingManager.SendError("ModPacker - Could not find the archive tool which should be located at " +
                                         ArchiveToolHelper.GetArchiveToolPath());
                 UIHelper.ShowError("Could not find the archive tool which should be located at  " +
                                          ArchiveToolHelper.GetArchiveToolPath());
                return false;
            }
            if (!PrepareRelease(statusCallback))
                return false;
            if (!PackArchives(statusCallback))
                return false;
            if (!WriteModuleFile(statusCallback))
                return false;
            if (!WriteInstallationInstructions(statusCallback))
                return false;
            if (!WriteLauncher(statusCallback))
                return false;
            if (!IncludeAdditionalArchives(statusCallback))
                return false;
            return true;
        }

        private bool PrepareRelease(Action<string> statusCallback)
        {
            if (!ArePathsValid())
                return false;
            statusCallback(@"Preparing mod");
            LoggingManager.SendMessage("ModPacker - Releasing mod " + ModManager.ModName + " into directory " + m_sOutputDirectory);

            try
            {
                m_releaser.CreateDirectories();
            }
            catch (Exception ex)
            {
                 UIHelper.ShowError("Failed to create mod directories: " + ex.Message);
            }

            return true;
        }

        private bool PackArchives(Action<string> statusCallback)
        {
            statusCallback(@"Starting packing processess");
            Application.DoEvents(); // needed to update the UI while running the method
            ArchivePacker attribPacker = new ArchivePacker(ModManager.ModName + "_data.sga", ModManager.ModDataDirectory, false);
            ArchivePackerInfo attribInfo;
            ArchivePacker dataPacker = new ArchivePacker(ModManager.ModName + "_attrib.sga", ModManager.ModAttribDirectory, true);
            ArchivePackerInfo dataInfo;
            try
            {
                dataInfo = dataPacker.PackArchive(m_releaser.GetDataArchivePath().SubstringBeforeLast('\\', true));
                attribInfo = attribPacker.PackArchive(m_releaser.GetAttribArchivePath().SubstringBeforeLast('\\', true));
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("ModPacker - failed to start packer(s)!");
                LoggingManager.HandleException(ex);
                 UIHelper.ShowError("Failed to start archive.exe: " + ex.Message);
                return false;
            }

            statusCallback(@"Waiting for packing-processess to finish...");

            LoggingManager.SendMessage("ModPacker - waiting for packers to finish");
            while (!(dataInfo.IsDone() && attribInfo.IsDone()))
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
            OnPackersFinished();
            return true;
        }

        private bool WriteModuleFile(Action<string> statusCallback)
        {
            try
            {
                m_releaser.WriteModuleFile();
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("ModPacker - Failed to generate module file");
                LoggingManager.HandleException(ex);
                 UIHelper.ShowError("Failed to create module file: " + ex.Message);
                return false;
            }
            LoggingManager.SendMessage("ModPacker - Successfully generated module file for mod " + ModManager.ModName);
            return true;
        }

        private bool WriteInstallationInstructions(Action<string> statusCallback)
        {
            // create installation instructions
            if (m_bCreateInstructions)
            {
                try
                {
                    m_releaser.WriteInstallationInstructions(m_bLauncherForRetribution);
                }
                catch (Exception ex)
                {
                     UIHelper.ShowError("Failed to create installation instructions: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        private bool WriteLauncher(Action<string> statusCallback)
        {
            if (m_bCreateLauncher)
            {
                statusCallback(@"Generating launcher...");
                try
                {
                    m_releaser.CreateLauncher(m_bLauncherForRetribution);
                }
                catch (Exception ex)
                {
                     UIHelper.ShowError("Failed to create launcher: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        private bool IncludeAdditionalArchives(Action<string> statusCallback)
        {
            statusCallback(@"Adding additional archives");
            try
            {
                m_releaser.IncludeAdditionalArchives();
            }
            catch (Exception ex)
            {
                 UIHelper.ShowError("Couldn't copy additional archives used by the mod: " + ex);
                return false;
            }
            LoggingManager.SendMessage("ModPacker - Successfully released mod " + ModManager.ModName);
            return true;
        }

        #region 

        private void OnPackersFinished()
        {
            if (!File.Exists(m_releaser.GetAttribArchivePath()) || !File.Exists(m_releaser.GetDataArchivePath()))
            {
                LoggingManager.SendError("ModPacker - Failed to create archive/s");
                 UIHelper.ShowError(
                    "Failed to create archive/s, take a look at the Archive.exe logfiles (in the 'Tools' subdirectory of this tool) to get further information.");
                return;
            }
            LoggingManager.SendMessage("ModPacker - Successfully created archives for mod " + ModManager.ModName);
        }

        void AttribPackerFinished()
        {
            lock (m_packerLock)
            {
                m_bAttribPackerFinished = true;
                if (m_bDataPackerFinished)
                    OnPackersFinished();
            }
        }

        void DataPackerFinished()
        {
            lock (m_packerLock)
            {
                m_bDataPackerFinished = true;
                if (m_bAttribPackerFinished)
                    OnPackersFinished();
            }
        }

        #endregion
    }
}