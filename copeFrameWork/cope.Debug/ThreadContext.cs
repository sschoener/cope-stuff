using System.Runtime.InteropServices;

namespace cope.Debug
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ThreadContext
    {
        public ThreadContextFlags ContextFlags; //set this to an appropriate value 
        // Retrieved by CONTEXT_DEBUG_REGISTERS
        public uint Dr0;
        public uint Dr1;
        public uint Dr2;
        public uint Dr3;
        public uint Dr6;
        public uint Dr7;
        // Retrieved by CONTEXT_FLOATING_POINT 
        public FloatingSaveArea FloatSave;
        // Retrieved by CONTEXT_SEGMENTS
        public uint SegGs;
        public uint SegFs;
        public uint SegEs;
        public uint SegDs;
        // Retrieved by CONTEXT_INTEGER
        [PrintValue(FormatString="x8", Filter=1)]
        public uint Edi;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint Esi;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint Ebx;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint Edx;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint Ecx;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint Eax;
        // Retrieved by CONTEXT_CONTROL
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint Ebp;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint Eip;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint SegCs;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint EFlags;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint Esp;
        [PrintValue(FormatString = "x8", Filter = 1)]
        public uint SegSs;
        // Retrieved by CONTEXT_EXTENDED_REGISTERS 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] ExtendedRegisters;
    }
}