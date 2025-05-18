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
using Microsoft.Win32;
using ModTool.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ModTool.FE
{
    // Todo: give this manager a proper purpose or delete it
    /// <summary>
    /// Bridge class between Core and FE.
    /// </summary>
    static class MainManager
    {
        #region methods

        public static void SetModLanguage(string lang)
        {
            Properties.Settings.Default.sLanguage = lang;
            ModManager.Language = lang;
        }

        public static void SetAllowOpeningFilesTwice(bool value)
        {
            Properties.Settings.Default.bAppAllowOpeningTwice = value;
            FileManager.AllowOpeningFilesTwice = value;
        }

        public static void TestMod()
        {
            UCSManager.SaveUCS();
            if (Properties.Settings.Default.sSteamExecutable == null || !File.Exists(Properties.Settings.Default.sSteamExecutable))
            {
                UIHelper.ShowError("The selected path for the Steam-Executable is not valid! Set it to a valid path in the options menu!");
                return;
            }

            string modName = ModManager.ModName;
            if (modName == null)
            {
                UIHelper.ShowError("Can't acquire mod name, please ensure that there is a mod loaded.");
                return;
            }
            string param = "-applaunch ";
            param += Properties.Settings.Default.sSteamAppID;
            if (Properties.Settings.Default.sSteamAppID == string.Empty)
                param += GameConstants.DOW2_APP_ID;
            param += " -dev -modname " + modName;
            if (Properties.Settings.Default.bTestDebugWin)
                param += " -debugwindow";
            if (Properties.Settings.Default.bTestNoMovies)
                param += " -nomovies";
            if (Properties.Settings.Default.bTestWindowed)
                param += " -window";
            if (Properties.Settings.Default.sTestParams != string.Empty)
                param += " " + Properties.Settings.Default.sTestParams;

            bool advDebug = false;
            if (User.IsCurrentUserAdministrator())
                advDebug = Properties.Settings.Default.bUseAdvancedDebug;
            else
                LoggingManager.SendMessage("DebugManager - Current user has insufficient rights to use advanced debugging.");
            Process.Start(Properties.Settings.Default.sSteamExecutable, param);
            if (advDebug)
            {
                Thread.Sleep(2000);
                DebugManager.StartDebugging();
            }
        }

        public static string GetSteamPath()
        {
            LoggingManager.SendMessage("Trying to get Steam path from the registry");
            RegistryKey rkSteamPath = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");

            if (rkSteamPath == null || rkSteamPath.GetValue("SteamPath") == null)
            {
                LoggingManager.SendMessage("Failed to get Steam path from the registry");
                var folderBrowser = new FolderBrowserDialog
                                        {
                                            ShowNewFolderButton = false,
                                            Description = @"Select your Steam directory..."
                                        };
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                    return folderBrowser.SelectedPath;
                return null;
            }
            return rkSteamPath.GetValue("SteamPath").ToString();
        }

        public static void OpenLogfileDirectory()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments,
                                                        Environment.SpecialFolderOption.None);
            if (!Directory.Exists(userPath))
            {
                UIHelper.ShowError("The 'My Documents' directory does not exist!");
                return;
            }
            string path = userPath;
            if (ToolSettings.IsInRetributionMode)
                path += GameConstants.LOG_FILE_PATH_RETRIBUTION;
            else
                path += GameConstants.LOG_FILE_PATH;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Process.Start(path);
        }

        #endregion methods
    }
}