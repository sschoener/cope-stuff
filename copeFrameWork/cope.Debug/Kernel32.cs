#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace cope.Debug
{
	public static class Kernel32
	{
		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi,
			ExactSpelling = true)]
		public static extern IntPtr GetProcAddress(IntPtr module, string name);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "VirtualProtectEx")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool VirtualProtectEx(IntPtr processHandle, IntPtr lpAddress, UIntPtr dwSize,
												   MemoryProtection flNewProtect, out MemoryProtection lpflOldProtect);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "WriteProcessMemory")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WriteProcessMemory(IntPtr processHandle, IntPtr lpBaseAddress, byte[] lpBuffer,
													 uint nSize, out int lpNumberOfBytesWritten);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "ReadProcessMemory")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ReadProcessMemory(IntPtr processHandle, IntPtr lpBaseAddress, [Out] byte[] lpBuffer,
													int dwSize, out int lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "ReadProcessMemory")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ReadProcessMemory(IntPtr processHandle, IntPtr lpBaseAddress, IntPtr targetAddress,
													int dwSize, out int lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "CreateRemoteThread")]
		public static extern IntPtr CreateRemoteThread(IntPtr processHandle, IntPtr lpThreadAttributes, uint dwStackSize,
													   IntPtr lpStartAddress,
													   IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "VirtualAllocEx")]
		public static extern IntPtr VirtualAllocEx(IntPtr processHandle, IntPtr lpAddress, uint dwSize,
												   AllocationType flAllocationType, MemoryProtection flProtect);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "VirtualFreeEx")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool VirtualFreeEx(IntPtr processHandle, IntPtr lpAddress, uint dwSize,
												AllocationType dwFreeType);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "WaitForSingleObject")]
		public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

		[DllImport("kernel32.dll", EntryPoint = "GetExitCodeThread")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetExitCodeThread(IntPtr threadHandle, out uint lpExitCode);

		[DllImport("kernel32.dll", SetLastError = true, EntryPoint = "CloseHandle")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr objectHandle);

		[DllImport("kernel32.dll", EntryPoint = "OpenThread", SetLastError = true)]
		public static extern IntPtr OpenThread(ThreadAccess access, bool inheritHandle, int threadId);

		[DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", SetLastError = true)]
		public static extern int GetCurrentThreadId();

		[DllImport("kernel32.dll", EntryPoint = "SuspendThread", SetLastError = true)]
		public static extern int SuspendThread(IntPtr hThread);

		[DllImport("kernel32.dll", EntryPoint = "ResumeThread", SetLastError = true)]
		public static extern int ResumeThread(IntPtr hThread);

		[DllImport("kernel32.dll", EntryPoint = "TerminateThread", SetLastError = true)]
		public static extern int TerminateThread(IntPtr hThread, int exitCode);

		[DllImport("kernel32.dll", EntryPoint="GetProcessIdOfThread", SetLastError=true)]
		public static extern int GetProcessIdOfThread(IntPtr hThread);

		[DllImport("kernel32.dll", SetLastError=true)]
		public static extern bool GetThreadContext(IntPtr hThread, ref ThreadContext lpThreadContext);
	}
}