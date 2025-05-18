using System;
using System.Runtime.InteropServices;

namespace cope.Debug
{
    public sealed class FixedMemory : IDisposable
    {

        public FixedMemory(int sizeInBytes)
        {
            Memory = CreateFixedMemory(sizeInBytes);
            Size = sizeInBytes;
        }

        public void Dispose()
        {
            DeleteFixedMemory(Memory);
        }

        public IntPtr Memory
        {
            get;
            private set;
        }

        public int Size
        {
            get;
            private set;
        }

        [DllImport("cope.Hook86.dll", EntryPoint = "FixedMemory_Create")]
        [return: MarshalAs(UnmanagedType.SysInt)]
        static extern IntPtr CreateFixedMemory(int size);

        [DllImport("cope.Hook86.dll", EntryPoint = "FixedMemory_Delete")]
        [return: MarshalAs(UnmanagedType.SysInt)]
        static extern void DeleteFixedMemory(IntPtr address);
    }
}
