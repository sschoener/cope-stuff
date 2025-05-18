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
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ModDebug
{
    static class ProcessExt
    {
        static public IntPtr GetProcAddress(this Process p, string name, string moduleName)
        {
            return GetProcAddress(p, name, p.GetModuleByName(moduleName));
        }

        static public IntPtr GetProcAddress(this Process p, string name, ProcessModule m = null)
        {
            if (m == null)
                m = p.MainModule;
            return krnl32GetProcAddress(m.BaseAddress, name);
        }

        static public ProcessModule GetModuleByName(this Process p, string name)
        {
            return p.Modules.Cast<ProcessModule>().FirstOrDefault(m => m.ModuleName == name);
        }

        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi)]
        static extern IntPtr krnl32GetProcAddress(IntPtr module, string name);
    }
}
