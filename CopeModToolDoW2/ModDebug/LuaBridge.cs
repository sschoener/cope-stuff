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

namespace ModDebug
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
                DoW2Bridge.TimeStampedTrace("LUA REGISTER FAILED!");
                DoW2Bridge.TimeStampedTrace(ex.Message);
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