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
using ModTool.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace ModTool.FE
{
    public partial class ModSettingsForm : Form
    {
        public ModSettingsForm()
        {
            InitializeComponent();
            var globalSection = ModManager.ModuleFile["global"] as ModuleFile.ModuleSectionKeyValue;
            m_tbxDescription.Text = globalSection["Description"];
            m_tbxModFolder.Text = globalSection["ModFolder"];
            m_tbxModName.Text = globalSection["Name"];
            m_tbxUiName.Text = globalSection["UIName"];
            m_tbxVersion.Text = globalSection["ModVersion"];
            if (globalSection.KeyExists("UCSBaseIndex"))
                m_nupUcsBaseIndex.Value = decimal.Parse(globalSection["UCSBaseIndex"]);
            foreach (ModuleFile.ModuleSection ms in ModManager.ModuleFile)
            {
                if (ms == globalSection || !(ms is ModuleFile.ModuleSectionFileList))
                    continue;
                m_lbxSections.Items.Add(ms);
            }
        }

        private void LbxSectionsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lbxSections.SelectedItem == null)
                return;
            m_lbxSgas.Items.Clear();
            foreach (string s in (m_lbxSections.SelectedItem as ModuleFile.ModuleSectionFileList).Archives)
                m_lbxSgas.Items.Add(s);
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            ModManager.ModName = m_tbxModName.Text;
            var globalSection = ModManager.ModuleFile["global"] as ModuleFile.ModuleSectionKeyValue;
            globalSection["Description"] = m_tbxDescription.Text;
            globalSection["ModFolder"] = m_tbxModFolder.Text;
            globalSection["Name"] = m_tbxModName.Text;
            globalSection["UIName"] = m_tbxUiName.Text;
            globalSection["ModVersion"] = m_tbxVersion.Text;
            globalSection["UCSBaseIndex"] = m_nupUcsBaseIndex.Value.ToString();
            try
            {
                LoggingManager.SendMessage("ModSettings - Trying to save module file...");
                ModManager.ModuleFile.WriteDataTo(ModManager.ModuleFile.FilePath);
            }
            catch (Exception ex)
            {
                LoggingManager.SendMessage("ModSettings - Could not save module file!");
                LoggingManager.HandleException(ex);
                 UIHelper.ShowError("Could not save module file! See logfile for further information.");
                ModManager.ModuleFile.Close();
                return;
            }
            LoggingManager.SendMessage("ModSettings - Module file successfully saved!");
            ModManager.ModuleFile.Close();
            Close();
        }

        private void BtnAddSGAClick(object sender, EventArgs e)
        {
            if (m_lbxSections.SelectedItem == null)
                return;
            m_dlgAddSGA.InitialDirectory = ModManager.GameDirectory;
            if (m_dlgAddSGA.ShowDialog() != DialogResult.OK)
                return;

            var current = m_lbxSections.SelectedItem as ModuleFile.ModuleSectionFileList;
            foreach (string s in m_dlgAddSGA.FileNames)
            {
                string fname = s.SubstringAfterLast('\\');
                string archivePath = ModManager.ModFolder + "\\Archives\\" + fname;
                if (current.Exists("archive", archivePath))
                    continue;
                File.Copy(s, ModManager.GameDirectory + archivePath, true);
                current.AddArchive(archivePath, false);
                m_lbxSgas.Items.Add(archivePath);
            }
            UIHelper.ShowMessage("Success", "Finished adding archive(s)!");
        }
    }
}