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
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ModTool.FE
{
    static class Program
    {
        static StreamWriter s_logFile;
        static readonly object s_loglock = new object();

        [STAThread]
        static void Main(string[] args)
        {
            if (!SetUpLoggingSystem())
                 UIHelper.ShowError("Could not set up logging system!");
            if (!ConfigManager.SetupConfigSystem(Application.StartupPath + "\\plugins.config"))
                 UIHelper.ShowError("Could not set up config system! Make sure '" + Application.StartupPath + "\\plugins.config' exists.");

            LoggingManager.SendMessage("Tool starting...");
            if (args.Length > 0)
            {
                var arguments = new StringBuilder();
                foreach (string arg in args)
                {
                    arguments.Append(' ');
                    arguments.Append(arg);
                }
                LoggingManager.SendMessage("Arguments: " + arguments);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += LogException;
            Application.ApplicationExit += OnApplicationExit;
            ModManager.ApplicationExitRequested += ModManagerApplicationExitRequested;

            // determine last used directory
            string lastPath = Properties.Settings.Default.sLastPath;
            string steamPath = null;
            if (string.IsNullOrEmpty(lastPath))
            {
                LoggingManager.SendMessage("Could not determine last used path, setting it to default path.");
                steamPath = MainManager.GetSteamPath();
                if (steamPath == null)
                {
                    AppEnd("User failed to provide a valid Steam path.");
                    return;
                }
                if (Directory.Exists(steamPath + GameConstants.RETRIBUTION_PATH_FROM_STEAM))
                    lastPath = steamPath + GameConstants.RETRIBUTION_PATH_FROM_STEAM;
                else if (Directory.Exists(steamPath + GameConstants.DOW2_PATH_FROM_STEAM))
                    lastPath = steamPath + GameConstants.DOW2_PATH_FROM_STEAM;
                else
                {
                    LoggingManager.SendWarning("Could neither find Retribution nor DoW2 directory; searched for " +
                                               steamPath + GameConstants.RETRIBUTION_PATH_FROM_STEAM + " and " +
                                               steamPath + GameConstants.DOW2_PATH_FROM_STEAM);
                    //AppEnd("Could neither find DoW2 nor Retribution");
                    lastPath = steamPath;
                }
            }
            ModManager.GameDirectory = lastPath;

            // get path to steam executable
            if (string.IsNullOrEmpty(Properties.Settings.Default.sSteamExecutable))
            {
                if (steamPath == null)
                {
                    steamPath = MainManager.GetSteamPath();
                    if (steamPath == null)
                    {
                        AppEnd("User failed to provide a valid Steam path.");
                        return;
                    }
                }
                Properties.Settings.Default.sSteamExecutable = steamPath + "\\steam.exe";
            }

            MainManager.SetAllowOpeningFilesTwice(Properties.Settings.Default.bAppAllowOpeningTwice);
            // create FrontEnd
            var mainDialog = new MainDialog();

            // load plugins
            // IMPORTANT: Create the FrontEnd BEFORE loading the plugins
            PluginManager.Path = Application.StartupPath + "\\plugins\\";
            PluginManager.LoadPlugins();
            PluginManager.LoadFileTypeSettings(Properties.Settings.Default.sFileTypes);

            if (string.IsNullOrEmpty(Properties.Settings.Default.sLanguage))
            {
                LoggingManager.SendMessage("No DoW2 language found, setting it to default (English)");
                MainManager.SetModLanguage("English");
            }
            else
                MainManager.SetModLanguage(Properties.Settings.Default.sLanguage);
            UCSManager.Init();

            mainDialog.HandleArgs(args);
            Application.Run(mainDialog);
        }

        #region eventhandlers

        static void OnApplicationExit(object sender, EventArgs e)
        {
            LoggingManager.SendMessage("Application shutting down");
            Properties.Settings.Default.Save();
            lock (s_loglock)
            {
                if (s_logFile != null)
                {
                    s_logFile.Flush();
                    s_logFile.Close();
                }
            }
        }

        static void ModManagerApplicationExitRequested(object sender, string t)
        {
            AppEnd(t);
        }

        static void LogException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!(e.ExceptionObject is Exception))
            {
                LoggingManager.SendError("APPCRASH - UNABLE TO PROVIDE CRASH INFORMATION");
                return;
            }
            var exp = (Exception)e.ExceptionObject;
            LoggingManager.SendError("APPCRASH");
            LoggingManager.SendMessage("PlugIns in use:\n{0}", FileTypeManager.GetPluginListing());
            LoggingManager.SendMessage("Mod info:\n{0}", ModManager.GetDebugInfo());
            LoggingManager.HandleException(exp);
            LoggingManager.SendMessage("END OF APPCRASH INFO");
             UIHelper.ShowError("Application crashed! Please post your Logfile on the RelicNews forums!");
        }

        static void OnLogMessage(string logMessage)
        {
            lock (s_loglock)
            {
                if (s_logFile == null)
                    return;
                s_logFile.WriteLine(logMessage);
                s_logFile.Flush();
            }
        }

        #endregion eventhandlers

        #region methods

        static public void AppEnd(string reason = null)
        {
            if (reason != null)
                LoggingManager.SendMessage("Application exited - reason: " + reason);
            else
                LoggingManager.SendMessage("Application exited - no reason given");
               
            UIHelper.ShowError("Application exited, see logfile for more information.");
            Application.Exit();
        }

        static bool SetUpLoggingSystem()
        {
            try
            {
                lock (s_loglock)
                {
                    s_logFile = File.CreateText(Application.StartupPath + "\\log.txt");
                }
                if (s_logFile == null)
                    return false;

                LoggingManager.OnLog += OnLogMessage;
                LoggingManager.SendMessage("LoggingManager - Logging system set up successfully!");
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion methods
    }
}