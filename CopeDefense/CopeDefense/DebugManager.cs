#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using cope.Extensions;
using cope.FOB;

#endregion

namespace CopeDefense
{
    public static class DebugManager
    {
        private static Process s_currentProcess;

        private static readonly Dictionary<string, GenericMethod<string, string[]>> s_cmdHandlers =
            new Dictionary<string, GenericMethod<string, string[]>>();

        public static Process CurrentProcess
        {
            get { return s_currentProcess; }
        }

        #region handlers

        private static string Print(string[] args)
        {
            if (args.Length == 0)
                return "Print(string message) expects one parameter, no parameters received!";
            if (!IsString(args[0]))
                return "Print(string message) expects one parameter of type STRING, wrong type!";
            DoW2Bridge.TimeStampedTrace(ParseString(args[0]));
            return null;
        }

        private static string SetPassword(string[] args)
        {
            if (args.Length == 0)
                return "SetPassword(string pwd) expects one parameter, no parameters received!";
            if (!IsString(args[0]))
                return "SetPassword(string pwd) expects one parameter of type STRING, wrong type!";
            DoW2Bridge.Password = ParseString(args[0]);
            DebugLog.SendMessage("Password set to " + DoW2Bridge.Password);
            return null;
        }

        private static string SetUser(string[] args)
        {
            if (args.Length == 0)
                return "SetUser(string user) expects one parameter, no parameters received!";
            if (!IsString(args[0]))
                return "SetUser(string user) expects one parameter of type STRING, wrong type!";
            DoW2Bridge.UserName = ParseString(args[0]);
            DebugLog.SendMessage("Username set to " + DoW2Bridge.UserName);
            return null;
        }

        #endregion handlers

        public static void Init()
        {
            const string initStartMsg = "CopeDefense - Establishing Cope's Forward Operations Base!";
            const string initEndMsg = "CopeDefense - Setup finished!";
            DebugLog.SendMessage(initStartMsg);
            DoW2Bridge.TimeStampedTrace(initStartMsg);

            s_currentProcess = Process.GetCurrentProcess();
            CommandProcessor.Processor = new GenericMethod<string, string>(CmdProcessor);
            s_cmdHandlers.Add("Print", Print);
            s_cmdHandlers.Add("SetUser", SetUser);
            s_cmdHandlers.Add("SetPassword", SetPassword);

            DebugLog.SendMessage(initEndMsg);
            DoW2Bridge.TimeStampedTrace(initEndMsg);
            DoW2Bridge.LuaInit();
        }

        private static bool IsString(string arg)
        {
            return arg.EndsWith('"') && arg.StartsWith('"');
        }

        private static string ParseString(string arg)
        {
            return arg.Remove(arg.Length - 1, 1).Remove(0, 1);
        }

        public static string CmdProcessor(string command)
        {
            string cmd = command.SubstringBeforeFirst('(');
            string[] args = command.SubstringAfterFirst('(').SubstringBeforeLast(')').Split(',');
            if (!s_cmdHandlers.ContainsKey(cmd))
                return cmd + " is not a valid command!";
            try
            {
                return s_cmdHandlers[cmd].Invoke(args);
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }
    }
}