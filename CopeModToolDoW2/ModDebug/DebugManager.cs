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
using cope.Extensions;
using cope.FOB;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ModDebug
{
    public static class DebugManager
    {
        static Process s_currentProcess;
        static readonly Dictionary<string, GenericMethod<string, string[]>> s_cmdHandlers = new Dictionary<string, GenericMethod<string, string[]>>();

        public static void Init()
        {
            DoW2Bridge.TimeStampedTrace("CopeDebug - Establishing Cope's Forward Operations Base!");
            s_currentProcess = Process.GetCurrentProcess();
            CommandProcessor.Processor = new GenericMethod<string, string>(CmdProcessor);
            s_cmdHandlers.Add("PropertyGroupManager_ReloadGroup", PropertyGroupManagerReloadPg);
            s_cmdHandlers.Add("PropertyGroupManager_GetBasePath", PropertyGroupManagerGetBasePath);
            s_cmdHandlers.Add("PropertyGroupManager_GetGroup", PropertyGroupManagerGetGroup);
            s_cmdHandlers.Add("print", Print);
            DoW2Bridge.TimeStampedTrace("CopeDebug - Setup finished!");
            //DoW2Bridge.LuaInit();
        }

        public static Process CurrentProcess
        {
            get
            {
                return s_currentProcess;
            }
        }

        #region handlers

        static string Print(string[] args)
        {
            if (args.Length == 0)
                return "print(string message) expects one parameter, no parameters received!";
            if (!IsString(args[0]))
                return "print(string message) expects one parameter of type STRING, wrong type!";
            DoW2Bridge.TimeStampedTrace(ParseString(args[0]));
            return null;
        }

        static string PropertyGroupManagerReloadPg(string[] args)
        {
            if (args.Length == 0)
                return "PropertyGroupManager_ReloadGroup(string propertyGroup) expects one parameter, no parameters received!";
            if (!IsString(args[0]))
                return "PropertyGroupManager_ReloadGroup(string propertyGroup) expects one parameter of type STRING, wrong type!";

            string arg0 = ParseString(args[0]);
            IntPtr propertyGroupManager;
            try
            {
                propertyGroupManager = DoW2Bridge.PropertyGroupManager_Instance();
            }
            catch (Exception ex)
            {
                return "PropertyGroupManager_Instance failed: " + ex.Message;
            }
            if (propertyGroupManager == null)
                return "PropertyGroupManager_Instance failed!";

            IntPtr pg;
            try
            {
                pg = DoW2Bridge.PropertyGroupManager_ReloadPropertyGroup(propertyGroupManager, arg0);
            }
            catch (Exception ex)
            {
                return "PropertyGroupManager_ReloadPropertyGroup failed: " + ex.Message;
            }
            if (pg == null)
                return "PropertyGroupManager_ReloadPropertyGroup failed!";
            DoW2Bridge.TimeStampedTrace("CopeDebug - " + arg0 + " reloaded");
            return "PropertyGroup " + arg0 + " reloaded";
        }

        static string PropertyGroupManagerGetBasePath(string[] args)
        {
            return DoW2Bridge.PropertyGroupManagerGetBasePath();
        }

        static string PropertyGroupManagerGetGroup(string[] args)
        {
            if (args.Length == 0)
                return "PropertyGroupManager_GetGroup(string propertyGroup) expects one parameter, no parameters received!";
            if (!IsString(args[0]))
                return "PropertyGroupManager_GetGroup(string propertyGroup) expects one parameter of type STRING, wrong type!";

            IntPtr propertyGroupManager;
            try
            {
                propertyGroupManager = DoW2Bridge.PropertyGroupManager_Instance();
            }
            catch (Exception ex)
            {
                return "PropertyGroupManager_Instance failed: " + ex.Message;
            }
            if (propertyGroupManager == null)
                return "PropertyGroupManager_Instance failed!";

            IntPtr pg;
            try
            {
                pg = DoW2Bridge.PropertyGroupManager_GetGroup(propertyGroupManager, ParseString(args[0]));
            }
            catch (Exception ex)
            {
                return "PropertyGroupManager_GetGroup failed: " + ex.Message;
            }
            if (pg == null)
                return "PropertyGroupManager_GetGroup failed!";
            return "GetGroup succeeded, address: " + pg;
        }

        #endregion handlers

        static bool IsString(string arg)
        {
            if (arg.EndsWith('"') && arg.StartsWith('"'))
                return true;
            return false;
        }

        static string ParseString(string arg)
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