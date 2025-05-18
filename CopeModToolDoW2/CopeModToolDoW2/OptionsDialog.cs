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
using ModTool.Core;
using ModTool.FE.Properties;
using System;
using System.Windows.Forms;

namespace ModTool.FE
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
            LoadSettingsToUI();
            if (ModManager.IsModLoaded)
                _cbx_language.Enabled = false;
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveSettings();
            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSelectDoW2ExeClick(object sender, EventArgs e)
        {
            if (_dlg_selectSteamExe.ShowDialog() == DialogResult.OK)
                _tbx_steamExe.Text = _dlg_selectSteamExe.FileName;
        }

        private void LoadSettingsToUI()
        {
            _chkbx_allowOpeningTwice.Checked = Settings.Default.bAppAllowOpeningTwice;
            _chkbx_markChangedTabs.Checked = Settings.Default.bAppMarkChanged;
            _chkbx_dvHideIcons.Checked = Settings.Default.bDirViewHideIcons;
            _chkbx_dvMarkChangedDirs.Checked = Settings.Default.bDirviewMarkChanged;
            
            switch (Settings.Default.sSteamAppID)
            {
                case GameConstants.DOW2_APP_ID:
                    m_radUseDoW2.Checked = true;
                    break;
                case GameConstants.CR_APP_ID:
                    m_radUseChaosRising.Checked = true;
                    break;
                case GameConstants.RETRIBUTION_APP_ID:
                    m_radUseRetribution.Checked = true;
                    break;
            }

            _chkbx_noMovies.Checked = Settings.Default.bTestNoMovies;
            _chkbx_window.Checked = Settings.Default.bTestWindowed;
            _chkbx_debugWindow.Checked = Settings.Default.bTestDebugWin;
            _chkbx_useAdvancedMode.Checked = Settings.Default.bUseAdvancedDebug;
            _dlg_selectSteamExe.InitialDirectory = Settings.Default.sLastPath;
            _tbx_steamExe.Text = Settings.Default.sSteamExecutable;
            _tbx_params.Text = Settings.Default.sTestParams;
            _cbx_language.SelectedItem = Settings.Default.sLanguage;
        }

        private void SaveSettings()
        {
            if (MarkChangedDirs != Settings.Default.bDirviewMarkChanged ||
                HideIcons != Settings.Default.bDirViewHideIcons)
            {
                RefreshView |= true;    
            }

            Settings.Default.bDirViewHideIcons = HideIcons;
            Settings.Default.bDirviewMarkChanged = MarkChangedDirs;
            Settings.Default.sSteamExecutable = SteamExecutable;

            if (m_radUseDoW2.Checked)
                Settings.Default.sSteamAppID = GameConstants.DOW2_APP_ID;
            else if (m_radUseChaosRising.Checked)
                Settings.Default.sSteamAppID = GameConstants.CR_APP_ID;
            else
                Settings.Default.sSteamAppID = GameConstants.RETRIBUTION_APP_ID;
            
            Settings.Default.bAppMarkChanged = MarkChangedTabs;
            MainManager.SetAllowOpeningFilesTwice(AllowOpeningTwice);
            Settings.Default.sTestParams = TestParameters;
            Settings.Default.bTestDebugWin = ShowDebugWindow;
            Settings.Default.bTestWindowed = StartWindowed;
            Settings.Default.bTestNoMovies = NoMovies;
            Settings.Default.sLanguage = Language;
            if (User.IsCurrentUserAdministrator())
                Settings.Default.bUseAdvancedDebug = AdvancedDebugMode;
            else
            {
                if (AdvancedDebugMode)
                     UIHelper.ShowError(
                        "Could not activate advanced debugging because the current user is not an administrator!");
                Settings.Default.bUseAdvancedDebug = false;
            }
        }

        #region properties

        public bool AllowOpeningTwice
        {
            get { return _chkbx_allowOpeningTwice.Checked; }
        }

        public bool MarkChangedTabs
        {
            get { return _chkbx_markChangedTabs.Checked; }
        }

        public bool MarkChangedDirs
        {
            get { return _chkbx_dvMarkChangedDirs.Checked; }
        }

        public bool HideIcons
        {
            get { return _chkbx_dvHideIcons.Checked; }
        }

        public bool NoMovies
        {
            get { return _chkbx_noMovies.Checked; }
        }

        public bool ShowDebugWindow
        {
            get { return _chkbx_debugWindow.Checked; }
        }

        public bool StartWindowed
        {
            get { return _chkbx_window.Checked; }
        }

        public string SteamExecutable
        {
            get { return _tbx_steamExe.Text; }
        }

        public string TestParameters
        {
            get { return _tbx_params.Text; }
        }

        public string Language
        {
            get { return _cbx_language.SelectedItem as string; }
        }

        public bool AdvancedDebugMode
        {
            get { return _chkbx_useAdvancedMode.Checked; }
        }

        public bool RefreshView
        {
            get;
            private set;
        }

        #endregion properties
    }
}