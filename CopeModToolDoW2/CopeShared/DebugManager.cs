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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using cope;
using cope.Debug;

namespace ModTool.Core
{
    /// <summary>
    /// Static class for debugging and modding DoW2.
    /// </summary>
    public static class DebugManager
    {
        #region fields

        private static ForwardPortClient s_client;
        private static DebugWindow s_window;
        private static readonly Stack<string> s_log = new Stack<string>();
        private static DummyReceiver s_callbackReceiver;

        // signature of GFWL memory check residing in xlive.dll
        private static readonly byte[] s_memoryCheckSignature = new byte[]
                                                                    {
                                                                        0x8B, 0xFF, 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x20,
                                                                        0x53, 0x56, 0x57, 0x8D, 0x45, 0xE0, 0x33,
                                                                        0xF6, 0x50, 0xFF, 0x75, 0x0C
                                                                    };
        // patch for the GFWL memory check
        // translates to ASM as:
        // retn     0xC;
        private static readonly byte[] s_memoryCheckPatch = new byte[] { 0xC2, 0x0C, 0x00 };

        #endregion

        /// <summary>
        /// This class handles callbacks from the ForwardOperationsBase.
        /// </summary>
        class DummyReceiver : IForwardPortCallback
        {
            /// <summary>
            /// Called by the FOB to communicate with the injector.
            /// </summary>
            /// <param name="message"></param>
            public void SendMessage(string message)
            {
                LogMessage(message);
            }
        }

        static DebugManager()
        {
            LoggingManager.SendMessage("DebugManager - Initializing DebugManager");
            ModManager.ApplicationExit += ModManagerApplicationExit;
            LoggingManager.SendMessage("DebugManager - Initialization finished");
        }

        private static void ModManagerApplicationExit()
        {
            try
            {
                if (s_client != null)
                    s_client.Close();
            }
            catch
            {
                // might throw exceptions which actually aren't important as we're shutting down anyway
            }
        }

        private static void OnGameExited(object sender, EventArgs e)
        {
            if (s_window != null)
            {
                s_window.Close();
            }
        }

        private static void ClearLog()
        {
            s_log.Clear();
            if (s_window != null && !s_window.IsDisposed)
                s_window.ClearLog();
        }

        private static void LogMessage(string s)
        {
            if (s == null)
                return;
            s_log.Push(s);
            if (s_window != null && !s_window.IsDisposed)
                s_window.Log(s);
        }

        /// <summary>
        /// Injects the FOB into DoW2.
        /// </summary>
        /// <param name="o"></param>
        private static void Inject(object o)
        {
            LogMessage("Searching for DoW2 process...");
            Process[] ps = Process.GetProcessesByName("DoW2");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                ps = Process.GetProcessesByName("DoW2");
                if (ps.Length > 0)
                    break;
            }
            if (ps.Length <= 0)
            {
                LogMessage("TIME OUT! Could not find DoW2 process 10 seconds after launch. Please try again.");
                return;
            }
            LogMessage("Process found!");
            Process dow2 = ps[0];
            // Retribution does not use GFWL anymore, no need to patch the memory
            if (!ToolSettings.IsInRetributionMode)
            {
                
                LogMessage("Preparing process...");
                try
                {
                    dow2.ReplaceSequence(s_memoryCheckSignature, s_memoryCheckPatch, "xlive.dll", 1);
                }
                catch (Exception e)
                {
                    LoggingManager.SendMessage("DebugManager - Patching failed!");
                    LoggingManager.HandleException(e);
                     UIHelper.ShowError("Error launching DoW2, please try again.");
                    return;
                }
            }
            
            LogMessage("Injecting Cope's Forward Operations Base");
            try
            {
                s_callbackReceiver = new DummyReceiver();
                s_client = dow2.InjectForwardOperationalBase(s_callbackReceiver);
            }
            catch (Exception e)
            {
                LoggingManager.SendMessage("DebugManager - Injecting FOB failed!");
                LoggingManager.HandleException(e);
                UIHelper.ShowError("Failed to start debugging system, please try again");
                return;
            }
            string currentDir = Directory.GetCurrentDirectory() + '\\';
            LogMessage("Injecting ModDebug.dll and initializing DebugManager...");
            try
            {
                //dow2.InjectDll("M:\\Steam\\steamapps\\common\\dawn of war 2\\CopeLua.dll");
                //dow2.InjectDll(currentDir + "LuaLibLoad.dll");
                s_client.LoadAssemblyAndStartMethod(currentDir + "ModDebug.dll", "ModDebug.DebugManager", "Init", true);
            }
            catch (Exception e)
            {
                LoggingManager.SendMessage("DebugManager - Initializtaion of remote DebugManager failed!");
                LoggingManager.HandleException(e);
                UIHelper.ShowError("Initialization of the remote DebugManager failed!");
                return;
            }
            LogMessage("Setup done!");
            dow2.Exited += OnGameExited;
        }

        /// <summary>
        /// Gets whether a client (= and thus DoW2) is currently available.
        /// </summary>
        public static bool HasClient
        {
            get
            {
                if (s_client == null)
                    return false;
                int ping;
                try
                {
                    ping = s_client.Ping();
                }
                catch
                {
                    return false;
                }
                // the FOB always returns 1337 as the ping-value.
                return ping == 1337;
            }
        }

        public static void ShowDebugWindow()
        {
            if (s_window == null || s_window.IsDisposed)
                s_window = new DebugWindow();
            s_window.Show();
        }

        /// <summary>
        /// Closes any open connection, searches for DoW2 and injects the ForwardOperationsBase.
        /// </summary>
        public static void StartDebugging()
        {
            LoggingManager.SendMessage("DebugManager - Advanced mode started");
            if (s_client != null)
            {
                try
                {
                    s_client.Close();
                }
                catch (Exception)
                {
                }
                s_client = null;
            }
            ClearLog();
            if (s_window == null || s_window.IsDisposed)
                s_window = new DebugWindow();
            ThreadPool.QueueUserWorkItem(Inject);
            ShowDebugWindow();
        }

        /// <summary>
        /// Sends a command to the Debug console and the client. Valid commands:
        /// clear() -- clears the console
        /// !PropertyGroupManager_GetBasePath() -- gets the base path of the property group manager of DoW2
        /// !PropertyGroupManager_ReloadGroup(string propertyGroup) -- reloads a certian property bag group
        /// !print(string) -- prints to the game console
        /// </summary>
        /// <param name="s"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string SendCommand(string s, params object[] args)
        {
            if (s == string.Empty)
                return null;
            string cmd = s;
            if (args.Length > 0)
                cmd = string.Format(s, args);
            if (cmd.Equals("clear()"))
            {
                ClearLog();
                return null;
            }
            if (!HasClient)
            {
                LogMessage("NO CONNECTION");
                return null;
            }
            try
            {
                if (cmd.StartsWith("!"))
                {
                    return s_client.ReceiveCommand(cmd.Remove(0, 1));
                }
                string answer = s_client.ReceiveCommand(cmd);
                LogMessage(answer);
                return answer;
            }
            catch (Exception ex)
            {
                LogMessage("CONNECTION LOST: " + ex.Message);
                return null;
            }
        }
    }
}