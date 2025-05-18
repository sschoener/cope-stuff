using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using cope.Debug;

namespace CoHNetDebug
{
    static public class CoHBridge
    {
        static LuaBridge s_luaBridge;
        static IntPtr s_worldPtr = IntPtr.Zero; // pointer to World-object used by CoH
        static readonly object s_worldLock = new object();
        delegate int LuaHandler(IntPtr ptr); // delegate for LUA functions
        static LuaHandler s_luaHandler; // the current LuaHandler; gets assigned in LuaInit()
        private static ProcessModule s_ww2Mod;

        // Test function to be called by LUA
        static int LuaTest(IntPtr state)
        {
            TimeStampedTrace("LUA CALL");
            return 1;
        }

        static int LuaWorldTest(IntPtr state)
        {
            TimeStampedTrace(s_worldPtr.ToString());
            return 1;
        }

        static int LuaDrawCollSystem(IntPtr state)
        {
            TimeStampedTrace("SimWorld at: 0x" + s_worldPtr.ToString("X8") + " " + s_worldPtr);
            World_Simulate(s_worldPtr);
            //IntPtr collSys = World_GetCollisionSystem(s_worldPtr);
            //TimeStampedTrace("CollisionSystem at: 0x" + collSys.ToString("X8") + " " + collSys.ToString());
            //CollisionSystem_ResetStats(s_worldPtr + 0x188);
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
            TimeStampedTrace("CopeDebug - Lua Handler called");
            s_luaBridge = new LuaBridge(state);
            s_luaBridge.RegisterLuaFunction(LuaTest, "CopeLua_Test");
            s_luaBridge.RegisterLuaFunction(LuaWorldTest, "CopeLua_GetSimWorld");
            s_luaBridge.RegisterLuaFunction(LuaDrawCollSystem, "CopeLua_DrawCollisionSystem");
            s_ww2Mod = Main.CurrentProcess.GetModuleByName("WW2Mod.dll");
            GetWorldPtr();
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
                s_worldPtr = s_ww2Mod.BaseAddress + 0x5D4CBC;
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
            EntryPoint = "?dbTimeStampedTracefAux@@YAXPBDZZ",
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

        // reloading RGDs
        [DllImport("SimEngine.dll", CharSet = CharSet.Ansi,
            EntryPoint = "?ReloadGroup@PropertyBagGroupManager@@QAEPBVPropertyBagGroup@@PBD@Z",
            CallingConvention = CallingConvention.ThisCall)]
        static public extern IntPtr PropertyGroupManager_ReloadPropertyGroup(IntPtr pgm, string path);

        [DllImport("SimEngine.dll", CharSet = CharSet.Ansi,
            EntryPoint = "?DebugDraw@CollisionSystem@@QAEXXZ",
            CallingConvention = CallingConvention.ThisCall)]
        static extern void CollisionSystem_DebugDraw(IntPtr collsys);

        [DllImport("SimEngine.dll", CharSet = CharSet.Ansi,
            EntryPoint = "?ResetStats@CollisionSystem@@QAEXXZ",
            CallingConvention = CallingConvention.ThisCall)]
        static extern void CollisionSystem_ResetStats(IntPtr collsys);

        [DllImport("SimEngine.dll", CharSet = CharSet.Ansi,
            EntryPoint = "?GetCollisionSystem@World@@QAEPAVCollisionSystem@@XZ",
            CallingConvention = CallingConvention.ThisCall)]
        static extern IntPtr World_GetCollisionSystem(IntPtr world);

        [DllImport("SimEngine.dll", CharSet = CharSet.Ansi,
            EntryPoint = "?Simulate@World@@UAEXXZ",
            CallingConvention = CallingConvention.ThisCall)]
        static extern IntPtr World_Simulate(IntPtr world);

        #endregion SimEngine

        #endregion imports
    }
}
