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
using cope.DawnOfWar2.SGA;
using cope.Extensions;
using ModTool.Core;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ModTool.FE
{
    // Todo: this file still is incredibly messy. Clean it up! Abstraction between module files and mods!
    class ModCreator
    {
        private readonly string m_sBaseModulePath;
        private readonly string m_sModName;
        private readonly string m_sDisplayedModName;
        private readonly string m_sModVersion;
        private readonly string m_sModDescription;
        private readonly uint m_uUCSBaseIndex;
        private readonly bool m_bRepackAttribArchive;

        public ModCreator(string baseModulePath, string modName, string uiModName, string modVersion, string description, uint ucsBaseIndex, bool repackAttribArchive)
        {
            m_sBaseModulePath = baseModulePath;
            m_sModDescription = description;
            m_sModName = modName;
            m_sModVersion = modVersion;
            m_sDisplayedModName = uiModName;
            m_uUCSBaseIndex = ucsBaseIndex;
            m_bRepackAttribArchive = repackAttribArchive;
        }

        public string ModulePath
        {
            get;
            private set;
        }

        public void WriteMod()
        {
            LoggingManager.SendMessage("ModCreator - Creating new mod...");

            ModManager.GameDirectory = m_sBaseModulePath.SubstringBeforeLast('\\');
            var baseModule = new ModuleFile(m_sBaseModulePath);
            baseModule.Close();

            if (!baseModule.HasSection("attrib:common"))
            {
                 UIHelper.ShowError(
                    "The selected base module file " + baseModule.FileName + " does not have a [attrib:common] section! Fix it first!");
                return;
            }

            if (!baseModule.HasSection("data:common"))
            {
                 UIHelper.ShowError(
                    "The selected module file " + baseModule.FileName + " does not have a [data:common] section! Fix it first!");
                return;
            }

            if (!baseModule.HasSection("global"))
            {
                 UIHelper.ShowError(
                    "The selected module file " + baseModule.FileName + " does not have a [global] section! Fix it first!");
                return;
            }

            string modulePath = ModManager.GameDirectory + m_sModName + ".module";
            if (File.Exists(modulePath))
            {
                if (
                    UIHelper.ShowYNQuestion("Warning",
                                            "There already exists a module file with the specified name " + m_sModName +
                                            ".module. Overwrite it?") == DialogResult.No)
                {
                     UIHelper.ShowError("Mod creation failed!");
                    return;
                }
            }



            LoggingManager.SendMessage("ModCreator - Generating module file for new mod");
            ModuleFile newMod = baseModule.GClone();
            var globalSection = (ModuleFile.ModuleSectionKeyValue)newMod["global"];
            globalSection["Name"] = m_sModName;
            globalSection["UIName"] = m_sDisplayedModName;
            globalSection["ModVersion"] = m_sModVersion;
            globalSection["ModFolder"] = m_sModName;
            globalSection["Description"] = m_sModDescription;
            globalSection["UCSBaseIndex"] = m_uUCSBaseIndex.ToString();

            var attribSection = (ModuleFile.ModuleSectionFileList)newMod["attrib:common"];
            attribSection.AddFolder(m_sModName + "\\DataAttrib", true);
            Directory.CreateDirectory(ModManager.GameDirectory + m_sModName + "\\DataAttrib");

            var dataSection = (ModuleFile.ModuleSectionFileList)newMod["data:common"];
            dataSection.AddFolder(m_sModName + "\\Data", true);
            Directory.CreateDirectory(ModManager.GameDirectory + m_sModName + "\\Data");

            if (m_bRepackAttribArchive)
            {
                string attribArchivePath = ModManager.GameDirectory + "GameAssets\\Archives\\GameAttrib.sga";
                string newArchivePath = m_sModName + "\\Archives\\GameAttrib_orig.sga";
                
                if (!File.Exists(attribArchivePath))
                {
                     UIHelper.ShowError("Tried to repack GameAttrib.sga but could not find it! Search at: " +
                                              attribArchivePath);
                }
                else
                {
                    if (RepackAttribArchive(attribArchivePath))
                    {
                        attribSection.RemoveArchive("GameAssets\\Archives\\GameAttrib.sga");
                        attribSection.AddArchive(newArchivePath, true);
                    }
                    else
                         UIHelper.ShowError("Failed to repack GameAttrib.sga.");
                }
            }

            newMod.WriteDataTo(modulePath);
            ModulePath = modulePath;

            LoggingManager.SendMessage("ModCreator - New mod successfully created!");
        }

        /// <exception cref="CopeException">Extraction successful but still cannot find the RB2 file!</exception>
        private bool RepackAttribArchive(string filePath)
        {
            const string RB2_PATH = "simulation\\attrib\\attribmegabinary.rb2";
            const string FLB_PATH = "simulation\\attrib\\fieldnames.flb";
            string tmpDir = Application.StartupPath + "\\temp\\";
            SGAFile attribSga;
            try
            {
                attribSga = new SGAFile(filePath, FileAccess.Read, FileShare.Read);
                foreach (var ep in attribSga)
                {
                    foreach (var f in ep.StoredFiles)
                    {
                        byte[] b = attribSga.ExtractFile(f.Value, attribSga.Stream);
                        string dir = tmpDir + f.Key.SubstringBeforeLast('\\');
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);
                        var stream = File.Create(tmpDir + f.Key);
                        stream.Write(b, 0, b.Length);
                        stream.Flush();
                        stream.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new CopeException(ex, "Failed/extract to open GameAttrib.sga!");
            }
            string rb2Path = tmpDir + RB2_PATH;
            if (!File.Exists(rb2Path))
                throw new CopeException("Extraction successful but still cannot find the RB2 file! Searched at: " + rb2Path);
            string flbPath = tmpDir + FLB_PATH;
            if (!File.Exists(flbPath))
                throw new CopeException("Extraction successful but still cannot find the FLB file! Searched at: " + flbPath);
            FieldNameFile flb;
            RB2FileExtractor extractor;
            try
            {
                flb = new FieldNameFile(flbPath);
                flb.ReadData();
                extractor = new RB2FileExtractor(rb2Path, flb);
                extractor.ReadData();
            }
            catch(Exception ex)
            {
                throw new CopeException(ex, "Failed to open the FLB or RB2 file!");
            }
            extractor.ExtractAll(tmpDir + "simulation\\attrib\\", null);
            extractor.Close();
            flb.Close();
            File.Delete(rb2Path);
            try
            {
                ArchivePacker attribPacker = new ArchivePacker("GameAttrib_orig.sga", tmpDir, true);
                var packerInfo = attribPacker.PackArchive(ModManager.GameDirectory + m_sModName + "\\Archives\\");
                while (!packerInfo.IsDone())
                    Thread.Sleep(100);
            }
            catch
            {
                Directory.Delete(tmpDir, true);
                throw;
            }
            Directory.Delete(tmpDir, true);
            return true;
        }
    }
}
