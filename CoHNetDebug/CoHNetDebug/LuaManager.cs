using System;
using System.Runtime.InteropServices;

namespace CoHNetDebug
{
    public static class LuaManager
    {
        public enum LuaIndices
        {
            LUA_REGISTRYINDEX = -10000,
            LUA_ENVIRONINDEX = -10001,
            LUA_GLOBALSINDEX = -10002
        }
        public enum LuaTypes
        {
            NONE = -1,
            NIL = 0,
            BOOLEAN = 1,
            LIGHTUSERDATA = 2,
            NUMBER = 3,
            STRING = 4,
            TABLE = 5,
            FUNCTION = 6,
            USERDATA = 7,
            THREAD = 8,
        }

        // typedef int (*lua_CFunction) (lua_State *L);
        public delegate int LuaFunction(IntPtr luaState);

        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "?SetNumber@LuaConfig@@QAEXPBDN@Z", CharSet = CharSet.Ansi)]
        public static extern void LuaConfig_SetNumber(string name, double value);

        //#define lua_register(L,n,f) (lua_pushcfunction(L, (f)), lua_setglobal(L, (n)))
        public static void lua_register(IntPtr luaState, string n, LuaFunction func)
        {
            lua_pushcfunction(luaState, func);
            lua_setglobal(luaState, n);
        }

        //#define lua_pushcfunction(L,f) lua_pushcclosure(L, (f), 0)
        public static void lua_pushcfunction(IntPtr luaState, LuaFunction func)
        {
            lua_pushcclosure(luaState, Marshal.GetFunctionPointerForDelegate(func), 0);
        }

        //#define lua_setglobal(L,s)     lua_setfield(L, LUA_GLOBALSINDEX, (s))
        public static void lua_setglobal(IntPtr luaState, string s)
        {
            lua_setfield(luaState, (int)LuaIndices.LUA_GLOBALSINDEX, s);
        }

        //LUA_API void  (lua_pushcclosure) (lua_State *L, lua_CFunction fn, int n);
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_pushcclosure@12", CharSet = CharSet.Ansi)]
        public static extern void lua_pushcclosure(IntPtr luaState, IntPtr func/*[MarshalAs(UnmanagedType.FunctionPtr)] LuaFunction func*/, int n);

        //LUA_API void  (lua_setfield) (lua_State *L, int idx, const char *k);
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_setfield@12", CharSet = CharSet.Ansi)]
        public static extern void lua_setfield(IntPtr luaState, int idx, string s);
    }
}
