using System;
using System.Collections.Generic;

namespace CoHNetDebug
{
    public class LuaBridge
    {
        private readonly List<Delegate> m_refs = new List<Delegate>();
        private IntPtr m_luaState = IntPtr.Zero;

        public LuaBridge(IntPtr luaState)
        {
            m_luaState = luaState;
        }

        public bool RegisterLuaFunction(LuaManager.LuaFunction func, string luaFuncName)
        {
            if (m_luaState == IntPtr.Zero)
                return false;

            // call the lua_register API function to register a .Net function with
            // the name luaFuncName and a function pointer func
            // the function pointer is defined using the delegate shown earlier
            // when making the correct Lua function signature for .Net functions
            try
            {
                LuaManager.lua_register(m_luaState, luaFuncName, func);
            }
            catch (Exception ex)
            {
                CoHBridge.TimeStampedTrace("LUA REGISTER FAILED!");
                CoHBridge.TimeStampedTrace(ex.Message);
                m_refs.Clear();
                m_luaState = IntPtr.Zero;
                return false;
            }

            // make sure the delegate callback is not collected by the garbage collector before
            // unmanaged code has called back
            m_refs.Add(func);
            return true;
        }
    }
}
