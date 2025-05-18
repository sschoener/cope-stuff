using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cope;
using DefenseShared;

namespace DefenseAdmin
{
    static class Program
    {       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnApplicationExit;

            // read item databases
            if (!(ReadUnlockDatabase() && ReadWargearDatabase() && ReadUpgradeDatabase()))
            {
                UIHelper.ShowError("Could not load databases. They should be in the same folder as this tool.");
                return;
            }

            Application.Run(new LoginForm());
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

        static bool SafeStream<T>(string path, Func<Stream,T> streamConsumer, out T result)
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

        static void OnApplicationExit(object sender, EventArgs e)
        {
            SafeStream("unlocks.txt", ItemDatabases.Unlocks.WriteDatabase);
            SafeStream("upgrades.txt", ItemDatabases.Upgrades.WriteDatabase);
            SafeStream("wargear.txt", ItemDatabases.Wargear.WriteDatabase);
            Properties.Settings.Default.Save();
        }
    }
}
