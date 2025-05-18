using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using cope.Debug;
using CopeDefenseLauncher.Properties;

namespace CopeDefenseLauncher
{
    internal static class DoW2Bridge
    {
        private const string DLL_CLASS = "CopeDefense.DebugManager";
        private const string DLL_FUNC = "Init";
        private const string DLL_NAME = "cope_defense.dll";
        public const int RETRIBUTION_APPID = 0xdc50;
        private const string STD_ARGS = "-modname cope_defense -dev";
        private const int TIMEOUT_DELAY = 300;
        private static string s_sStartArguments;
        private static ForwardPortClient Client { get; set; }

        public static string StartArguments
        {
            get { return s_sStartArguments; }
            set
            {
                s_sStartArguments = value;
                Settings.Default.DoW2Arguments = value;
            }
        }

        public static void SetClientUser()
        {
            Client.ReceiveCommand("SetUser(\"" + ServerInterface.UserName + "\")");
            Client.ReceiveCommand("SetPassword(\"" + ServerInterface.HashedPassword + "\")");
        }

        public static bool StartDoW2(IForwardPortCallback callback)
        {
            TerminateClient();
            SteamHelper.StartSteam(0xdc50, "-modname cope_defense -dev " + s_sStartArguments);
            Process[] processesByName = null;
            for (int i = 0; i < 300; i++)
            {
                Thread.Sleep(1000);
                processesByName = Process.GetProcessesByName("DoW2");
                if (processesByName.Length > 0)
                {
                    break;
                }
            }
            if ((processesByName == null) || (processesByName.Length == 0))
            {
                return false;
            }
            Thread.Sleep(1000);
            Process process = processesByName[0];
            process.InjectDll(Application.StartupPath + @"\LuaLibLoad.dll");
            Client = process.InjectForwardOperationalBase(callback);
            Client.RegisterCallbackClient();
            Client.LoadAssemblyAndStartMethod(Application.StartupPath + '\\' + "cope_defense.dll",
                "CopeDefense.DebugManager", "Init", true);
            process.PatchAt(new IntPtr(0x70a820), new byte[] {0xb0, 1, 0xc3});
            return true;
        }

        private static void TerminateClient()
        {
            try
            {
                if (Client != null)
                {
                    Client.Abort();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}