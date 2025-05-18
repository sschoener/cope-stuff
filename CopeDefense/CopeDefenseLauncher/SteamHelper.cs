using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopeDefenseLauncher
{
    internal static class SteamHelper
    {
        // Methods
        public static string GetSteamPath()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
            if ((key != null) && (key.GetValue("SteamPath") != null))
            {
                return key.GetValue("SteamPath").ToString();
            }
            FolderBrowserDialog dialog2 = new FolderBrowserDialog();
            dialog2.ShowNewFolderButton = false;
            dialog2.Description = "Select your Steam directory...";
            FolderBrowserDialog dialog = dialog2;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return null;
        }

        public static void StartSteam(int appid, string arguments)
        {
            Process.Start(SteamExecutable, string.Concat(new object[] { "-applaunch ", appid, ' ', arguments }));
        }

        public static string SteamExecutable { get; set; }
    }
}
