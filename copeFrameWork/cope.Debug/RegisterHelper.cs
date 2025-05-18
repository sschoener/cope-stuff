using System.Runtime.InteropServices;

namespace cope.Debug
{
    public static class RegisterHelper
    {
        [DllImport("cope.Hook86.dll", EntryPoint = "GetEAX")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetEAX();

        [DllImport("cope.Hook86.dll", EntryPoint = "GetEBX")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetEBX();

        [DllImport("cope.Hook86.dll", EntryPoint = "GetECX")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetECX();

        [DllImport("cope.Hook86.dll", EntryPoint = "GetEDX")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetEDX();

        [DllImport("cope.Hook86.dll", EntryPoint = "GetESI")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetESI();

        [DllImport("cope.Hook86.dll", EntryPoint = "GetEDI")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetEDI();

        [DllImport("cope.Hook86.dll", EntryPoint = "GetEBP")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetEBP();

        [DllImport("cope.Hook86.dll", EntryPoint = "GetESP")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetESP();

        [DllImport("cope.Hook86.dll", EntryPoint = "GetStack")]
        [return: MarshalAs(UnmanagedType.I8)]
        static public extern int GetStack(int offset);

        [DllImport("cope.Hook86.dll", EntryPoint = "SetEAX")]
        static public extern void SetEAX(int value);

        [DllImport("cope.Hook86.dll", EntryPoint = "SetEBX")]
        static public extern void SetEBX(int value);

        [DllImport("cope.Hook86.dll", EntryPoint = "SetECX")]
        static public extern void SetECX(int value);

        [DllImport("cope.Hook86.dll", EntryPoint = "SetEDX")]
        static public extern void SetEDX(int value);

        [DllImport("cope.Hook86.dll", EntryPoint = "SetESI")]
        static public extern void SetESI(int value);

        [DllImport("cope.Hook86.dll", EntryPoint = "SetEDI")]
        static public extern void SetEDI(int value);

        [DllImport("cope.Hook86.dll", EntryPoint = "SetStack")]
        static public extern void SetStack(int offset, int value);

        [DllImport("cope.Hook86.dll", EntryPoint = "GetRegisters")]
        static public extern void GetRegisters(out int[] registers);

        /*static public int[] GetRegistersFromMemory(IntPtr memory)
        {
            var registers = new int[8];
            // read process memory
            return new int[0];
        }*/
    }
}
