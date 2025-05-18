using System;
using System.Runtime.InteropServices;

namespace cope.Debug
{
    internal static class NtDll
    {
        [DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
        public static extern IntPtr LdrGetProcedureAddress([In] HandleRef ModuleHandle, [In, Optional] ref AnsiString FunctionName, [In, Optional] ushort Oridinal, [Out] out IntPtr FunctionAddress);
    }
}