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
using System.Runtime.InteropServices;

namespace ModDebug
{
    static public class DoW2Bridge
    {
        static LuaBridge s_luaBridge;
        static IntPtr s_worldPtr = IntPtr.Zero; // pointer to World-object used by DoW2
        static readonly object s_worldLock = new object();
        delegate int LuaHandler(IntPtr ptr); // delegate for LUA functions
        static LuaHandler s_luaHandler; // the current LuaHandler; gets assigned in LuaInit()

        // Test function to be called by LUA
        static int LuaTest(IntPtr state)
        {
            TimeStampedTrace("LUA CALL");
            return 1;
        }

        /// <summary>
        /// Function called by the injected DLL, all the functions you want to add to the LUA state go here.
        /// Init of LuaBridge happens in here.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        static public int LuaCallHandler(IntPtr state)
        {
            TimeStampedTrace("Lua Handler called");
            s_luaBridge = new LuaBridge(state);
            s_luaBridge.RegisterLuaFunction(LuaTest, "CopeLua_Test");
            return 1;
        }

        /// <summary>
        /// Initializes the bridge to DoW2, called by DebugManager.Init().
        /// </summary>
        static public void LuaInit()
        {
            TimeStampedTrace("CopeDebug - Lua Init");
            s_luaHandler = LuaCallHandler; // setup a handler for LUA calls by the injected CopeLua.dll
            try
            {
                SetLuaHandler(Marshal.GetFunctionPointerForDelegate(s_luaHandler));
            }
            catch (Exception ex)
            {
                TimeStampedTrace("CopeDebug - Lua Init Failed");
                TimeStampedTrace(ex.Message);
                return;
            }
            TimeStampedTrace("CopeDebug - Lua Init Finished");
        }

        /// <summary>
        /// Returns the World pointer of DoW2, may be useful in some cases.
        /// </summary>
        /// <returns></returns>
        static public IntPtr GetWorldPtr()
        {
            lock (s_worldLock)
            {
                s_worldPtr = DebugManager.CurrentProcess.GetProcAddress("?g_World@@3PAVWorld@@A", "SimEngine.dll");
            }
            return s_worldPtr;
        }

        #region wrappers

        /// <summary>
        /// Returns the base path of the PropertyGroupManager of DoW2.
        /// </summary>
        /// <returns></returns>
        static public string PropertyGroupManagerGetBasePath()
        {
            try
            {
                IntPtr instance = PropertyGroupManager_Instance();
                IntPtr path = PGM_GetBasePath(instance);
                return Marshal.PtrToStringAnsi(path);
            }
            catch
            {
                return null;
            }
        }

        #endregion wrappers

        #region imports

        // used for printing stuff to the DoW2 console
        [DllImport("Debug.dll", SetLastError = true,
            EntryPoint = "?TimeStampedTracef@dbInternal@@YAXPBDZZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static public extern void TimeStampedTrace(string s);

        // import from native x86 DLL used for getting access to the LUA system
        [DllImport("CopeLua.dll", EntryPoint = "SetLuaHandler", CallingConvention = CallingConvention.StdCall)]
        static public extern void SetLuaHandler(IntPtr func);

        #region SimEngine

        // various useful functions for accessing subsystems from the game
        // GetWorldPtr() -> World_GetSimManager() -> SimManager_GetScar() -> Scar_GetState = LuaState
        [DllImport("SimEngine.dll", EntryPoint = "?GetState@Scar@@QAEPAVLuaConfig@@XZ",
            CallingConvention = CallingConvention.ThisCall)]
        static public extern IntPtr Scar_GetState(IntPtr scar);

        [DllImport("SimEngine.dll", EntryPoint = "?GetScar@SimManager@@QBEPBVScar@@XZ",
            CallingConvention = CallingConvention.ThisCall)]
        static public extern IntPtr SimManager_GetScar(IntPtr simManager);

        [DllImport("SimEngine.dll", EntryPoint = "?GetSimManager@World@@QAEPAVSimManager@@XZ",
            CallingConvention = CallingConvention.ThisCall)]
        static public extern IntPtr World_GetSimManager(IntPtr world);

        // returns a pointer to the instance of PropertyGroupManager
        [DllImport("SimEngine.dll", EntryPoint = "?Instance@PropertyBagGroupManager@@SGAAV1@XZ",
            CallingConvention = CallingConvention.StdCall)]
        static public extern IntPtr PropertyGroupManager_Instance();

        // PGM == PropertyGroupManager
        [DllImport("SimEngine.dll", CharSet = CharSet.Ansi,
            EntryPoint = "?GetBasePath@PropertyBagGroupManager@@QBEPBDXZ",
            CallingConvention = CallingConvention.ThisCall)]
        static extern IntPtr PGM_GetBasePath(IntPtr pgm);

        [DllImport("SimEngine.dll", CharSet = CharSet.Ansi,
            EntryPoint = "?GetGroup@PropertyBagGroupManager@@QAEPBVPropertyBagGroup@@PBD@Z",
            CallingConvention = CallingConvention.ThisCall)]
        static public extern IntPtr PropertyGroupManager_GetGroup(IntPtr pgm, string name);

        // reloading RBFs
        [DllImport("SimEngine.dll", CharSet = CharSet.Ansi,
            EntryPoint = "?ReloadGroup@PropertyBagGroupManager@@QAEPBVPropertyBagGroup@@PBD@Z",
            CallingConvention = CallingConvention.ThisCall)]
        static public extern IntPtr PropertyGroupManager_ReloadPropertyGroup(IntPtr pgm, string path);

        #endregion SimEngine

        #endregion imports
    }
}