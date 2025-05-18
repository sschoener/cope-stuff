#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace CopeDefense
{
    // Helper class containing various LUA-shortcuts
    public static class LuaManager
    {
        #region Delegates

        public delegate int LuaFunction(IntPtr luaState);

        #endregion

        #region LuaIndices enum

        public enum LuaIndices
        {
            LUA_REGISTRYINDEX = -10000,
            LUA_ENVIRONINDEX = -10001,
            LUA_GLOBALSINDEX = -10002
        }

        #endregion

        #region LuaTypes enum

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

        #endregion

        // typedef int (*lua_CFunction) (lua_State *L);

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
            lua_setfield(luaState, (int) LuaIndices.LUA_GLOBALSINDEX, s);
        }

        public static void lua_newtable(IntPtr luaState)
        {
            lua_createtable(luaState, 0, 0);
        }

        //LUA_API void  (lua_pushcclosure) (lua_State *L, lua_CFunction fn, int n);
        /// <summary>
        /// Pushes a new C closure onto the stack.  When a C function is created, it is possible to associate some values with it, thus creating a C closure (see §3.4); these
        /// values are then accessible to the function whenever it is called. To associate values with a C function, first these values should be  pushed onto the stack
        /// (when there are multiple values, the first value is pushed first). Then lua_pushcclosure is called to create and push the C function onto the stack,
        /// with the argument n telling how many values should be associated with the function. lua_pushcclosure also pops these values from the stack. 
        /// The maximum value for n is 255.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="func"></param>
        /// <param name="n"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_pushcclosure@12", CharSet = CharSet.Ansi)]
        public static extern void lua_pushcclosure(IntPtr luaState, IntPtr func
                                                   /*[MarshalAs(UnmanagedType.FunctionPtr)] LuaFunction func*/, int n);

        /// <summary>
        /// Pushes a number with value n onto the stack.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="integer"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_pushinteger@8", CharSet = CharSet.Ansi)]
        public static extern void lua_pushinteger(IntPtr luaState, int integer);

        //LUA_API void  (lua_setfield) (lua_State *L, int idx, const char *k);
        /// <summary>
        /// Does the equivalent to t[k] = v, where t is the value at the given valid index and v is the value at the top of the stack. 
        /// This function pops the value from the stack. As in Lua, this function may trigger a metamethod for the "newindex" event (see §2.8).
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="idx"></param>
        /// <param name="s"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_setfield@12", CharSet = CharSet.Ansi)]
        public static extern void lua_setfield(IntPtr luaState, int idx, string s);

        /// <summary>
        /// Converts the Lua value at the given acceptable index to the signed integral type lua_Integer.
        /// The Lua value must be a number or a string convertible to a number (see §2.2.1); otherwise, lua_tointeger returns 0.
        /// If the number is not an integer, it is truncated in some non-specified way.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_tointeger@8", CharSet = CharSet.Ansi)]
        public static extern int lua_tointeger(IntPtr luaState, int idx);

        /// <summary>
        /// Converts the Lua value at the given acceptable index to a C string. If len is not NULL, it also sets *len with the string length.
        /// The Lua value must be a string or a number; otherwise, the function returns NULL.
        /// If the value is a number, then lua_tolstring also changes the actual value in the stack to a string.
        /// (This change confuses lua_next when lua_tolstring is applied to keys during a table traversal.) 
        /// lua_tolstring returns a fully aligned pointer to a string inside the Lua state. This string always has a zero ('\0') after
        /// its last character (as in C), but can contain other zeros in its body. Because Lua has garbage collection,
        /// there is no guarantee that the pointer returned by lua_tolstring will be valid after the corresponding value is removed from the stack.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="idx"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_tolstring@12", CharSet = CharSet.Ansi)]
        public static extern string lua_tolstring(IntPtr luaState, int idx, IntPtr length);

        /// <summary>
        /// Pushes onto the stack the value t[k], where t is the value at the given valid index and k is the value at the top of the stack. 
        /// This function pops the key from the stack (putting the resulting value in its place). As in Lua, this function may trigger a metamethod for the "index" event.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="idx"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_gettable@8", CharSet = CharSet.Ansi)]
        public static extern void lua_gettable(IntPtr luaState, int idx);

        /// <summary>
        /// Removes the element at the given valid index, shifting down the elements above this index to fill the gap.
        /// Cannot be called with a pseudo-index, because a pseudo-index is not an actual stack position.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="idx"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_remove@8", CharSet = CharSet.Ansi)]
        public static extern void lua_remove(IntPtr luaState, int idx);

        /// <summary>
        /// Creates a new empty table and pushes it onto the stack. The new table has space pre-allocated for narr array elements and nrec non-array elements.
        /// This pre-allocation is useful when you know exactly how many elements the table will have. Otherwise you can use the function lua_newtable.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="narr"></param>
        /// <param name="nrec"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_createtable@12", CharSet = CharSet.Ansi)]
        public static extern void lua_createtable(IntPtr luaState, int narr, int nrec);

        /// <summary>
        /// Does the equivalent to t[k] = v, where t is the value at the given valid index, v is the value at the top of the stack, and k is the value just below the top. 
        /// This function pops both the key and the value from the stack.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="index"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_rawset@8", CharSet = CharSet.Ansi)]
        public static extern void lua_rawset(IntPtr luaState, int index);

        /// <summary>
        /// Does the equivalent of t[n] = v, where t is the value at the given valid index and v is the value at the top of the stack. 
        /// This function pops the value from the stack. The assignment is raw; that is, it does not invoke metamethods.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="index"></param>
        /// <param name="n"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_rawseti@12", CharSet = CharSet.Ansi)]
        public static extern void lua_rawseti(IntPtr luaState, int index, int n);

        /// <summary>
        /// Pushes the zero-terminated string pointed to by str onto the stack. Lua makes (or reuses) an internal copy
        /// of the given string, so the memory at s can be freed or reused immediately after the function returns.
        /// The string cannot contain embedded zeros; it is assumed to end at the first zero.
        /// </summary>
        /// <param name="luaState"></param>
        /// <param name="str"></param>
        [DllImport("LuaConfig.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "_lua_pushstring@8", CharSet = CharSet.Ansi)]
        public static extern void lua_pushstring(IntPtr luaState, string str);
    }
}