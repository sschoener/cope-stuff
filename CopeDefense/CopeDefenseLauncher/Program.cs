using System;
using System.IO;
using System.Windows.Forms;
using cope;
using cope.Extensions;
using System.Linq;
using DefenseShared;

namespace CopeDefenseLauncher
{
    static class Program
    {
        private static StreamWriter s_log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!SetupLogger())
                return;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.ApplicationExit += OnApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // get steam path
            string steamExec = Properties.Settings.Default.SteamExecutable;
            if (string.IsNullOrEmpty(steamExec) || !File.Exists(steamExec))
            {
                string steamPath = SteamHelper.GetSteamPath();
                if (steamPath == null)
                {
                    UIHelper.ShowError("Invalid Steam path, exiting now.");
                    return;
                }
                steamExec = steamPath + "/steam.exe";
                if (!File.Exists(steamExec))
                {
                    UIHelper.ShowError("Can't find Steam.exe, exiting now.");
                    return;
                }
                Properties.Settings.Default.SteamExecutable = steamExec;
            }
            SteamHelper.SteamExecutable = steamExec;
            DoW2Bridge.StartArguments = Properties.Settings.Default.DoW2Arguments;

            if (!ReadUnlockDatabase() || !ReadWargearDatabase() || !ReadUpgradeDatabase())
            {
                UIHelper.ShowError("Could not read item database. Try redownloading.");
                return;
            }

            var mainForm = new MainForm();
            Application.Run(mainForm);
        }

        static bool ReadUnlockDatabase()
        {
            ItemDatabases.ItemStore unlocks;
            bool result = SafeStream("unlocks.txt", ItemDatabases.ItemStore.ReadDatabase, out unlocks);
            ItemDatabases.Unlocks = unlocks;
            return result;
        }

        static bool ReadWargearDatabase()
        {
            ItemDatabases.ItemStore wargear;
            bool result = SafeStream("wargear.txt", ItemDatabases.ItemStore.ReadDatabase, out wargear);
            ItemDatabases.Wargear = wargear;
            return result;
        }

        static bool ReadUpgradeDatabase()
        {
            ItemDatabases.ItemStore upgrades;
            bool result = SafeStream("upgrades.txt", ItemDatabases.ItemStore.ReadDatabase, out upgrades);
            ItemDatabases.Upgrades = upgrades;
            return result;
        }

        static bool SafeStream<T>(string path, Func<Stream, T> streamConsumer, out T result)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                result = streamConsumer(stream);
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Flush();
                    stream.Close();
                }
            }
            return true;
        }

        static bool SafeStream(string path, Action<Stream> streamConsumer)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                streamConsumer(stream);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Flush();
                    stream.Close();
                }
            }
            return true;
        }
        
        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        static void OnApplicationExit(object sender, EventArgs e)
        {
            if (s_log != null)
            {
                s_log.Flush();
                s_log.Close();
            }
            Properties.Settings.Default.Save();
        }

        static bool SetupLogger()
        {
            try
            {
                s_log = File.CreateText(Application.StartupPath + "\\log.txt");
            }
            catch (Exception ex)
            {
                if (s_log != null)
                    s_log.Close();
                UIHelper.ShowError("Failed to setup logging system! Info: " + ex.Message);
                return false;
            }
            return true;
        }

        static public void LogMessage(string msg)
        {
            if (s_log != null)
            {
                s_log.WriteLine(msg);
                s_log.Flush();
            }
        }

        static public void HandleException(Exception ex)
        {
            if (s_log != null)
            {
                LogMessage(ex.GetInfo().Aggregate(string.Empty, (s1, s2) => s1 + s2));
            }
        }
    }
}
