using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using cope.Extensions;

namespace cope.Debug
{
    public static class ProcExt
    {
        private const string FORWARD_INJECTEE = "cope.ManagedLoader.dll";
        private const string FORWARD_LOAD_METHOD = "_StartRuntime@16";
        private const string FORWARD_BASE_DLL = "cope.FOB.dll";
        private const string FORWARD_TYPE_NAME = "cope.FOB.ForwardOperationalBase";
        private const string FORWARD_METHOD_NAME = "Establish";
        private const string FORWARD_METHOD_PARAM = "";
        private const string FORWARD_ENDPOINT_ADDRESS = "net.pipe://localhost/cope.ForwardOperationalBase/ForwardPort";
        // size (in bytes) of the buffer used to read from the process in FindSequence(...)
        const int FIND_BUFFER_SIZE = 0x100000;

        #region methods

        static public byte[] ReadProcessMemory(this Process process, IntPtr baseAddress, int numBytes)
        {
            var read = new byte[numBytes];
            int numRead;
            Kernel32.ReadProcessMemory(process.Handle, baseAddress, read, numBytes, out numRead);
            return read;
        }

        static public byte[] ReadProcessMemory(this Process process, ProcessModule module, int offset, int numBytes)
        {
            var read = new byte[numBytes];
            int numRead;
            Kernel32.ReadProcessMemory(process.Handle, module.BaseAddress+offset, read, numBytes, out numRead);
            return read;
        }

        static public bool ReadProcessMemory(this Process process, IntPtr baseAddress, IntPtr targetAddress, int numBytes)
        {
            int numRead;
            return Kernel32.ReadProcessMemory(process.Handle, baseAddress, targetAddress, numBytes, out numRead);
        }

        static public bool VirtualProtectEx(this Process process, IntPtr lpAddress, uint dwSize, MemoryProtection flNewProtect, out MemoryProtection lpflOldProtect)
        {
            return Kernel32.VirtualProtectEx(process.Handle, lpAddress, new UIntPtr(dwSize), flNewProtect, out lpflOldProtect);
        }

        static public bool VirtualProtectEx(this Process process, IntPtr lpAddress, uint dwSize, MemoryProtection flNewProtect)
        {
            MemoryProtection mpOld;
            return Kernel32.VirtualProtectEx(process.Handle, lpAddress, new UIntPtr(dwSize), flNewProtect, out mpOld);
        }

        static uint WaitForThreadToExit(IntPtr threadHandle)
        {
            Kernel32.WaitForSingleObject(threadHandle, unchecked((uint)-1));
            uint exitCode;
            Kernel32.GetExitCodeThread(threadHandle, out exitCode);
            return exitCode;
        }

        static public IntPtr GetProcAddress(this Process process, string name, ProcessModule m = null)
        {
            if (m == null)
                m = process.MainModule;
            return Kernel32.GetProcAddress(m.BaseAddress, name);
        }

        static public IntPtr GetProcAddress(this Process process, string name, string moduleName)
        {
            return GetProcAddress(process, name, process.GetModuleByName(moduleName));
        }

        static public IntPtr GetProcAddressSafe(this Process process, string name, string moduleName)
        {
            return GetProcAddressSafe(process, name, process.GetModuleByName(moduleName));
        }

        static public IntPtr GetProcAddressSafe(this Process process, string name, ProcessModule m = null)
        {
            if (m == null)
                m = process.MainModule;
            HandleRef handle = new HandleRef(m, m.BaseAddress);
            AnsiString str = new AnsiString(name);
            IntPtr address;
            NtDll.LdrGetProcedureAddress(handle, ref str, 0, out address);
            return address;
        }

        static public ProcessModule GetModuleByName(this Process process, string name)
        {
            return process.Modules.Cast<ProcessModule>().FirstOrDefault(m => m.ModuleName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Tries to patch the process at an offset of your choice. Returns true on success.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="offset"></param>
        /// <param name="patchContent">Theses are the bytes the method will try to patch into the process.</param>
        /// <returns></returns>
        static public bool PatchAt(this Process process, IntPtr offset, byte[] patchContent)
        {
            int i;
            MemoryProtection oldProtection;
            Kernel32.VirtualProtectEx(process.Handle, offset, (UIntPtr)patchContent.Length, MemoryProtection.ExecuteReadWrite, out oldProtection);
            bool b = Kernel32.WriteProcessMemory(process.Handle, offset, patchContent, (uint)patchContent.Length, out i);
            Kernel32.VirtualProtectEx(process.Handle, offset, (UIntPtr)patchContent.Length, oldProtection, out oldProtection);
            return b;
        }

        /// <summary>
        /// Tries to patch the process at an offset of your choice. Returns true on success.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="module"></param>
        /// <param name="relativeOffset"></param>
        /// <param name="patchContent">Theses are the bytes the method will try to patch into the process.</param>
        /// <returns></returns>
        static public bool PatchAt(this Process process, ProcessModule module, int relativeOffset, byte[] patchContent)
        {
            int i;
            MemoryProtection oldProtection;
            Kernel32.VirtualProtectEx(process.Handle, module.BaseAddress + relativeOffset, (UIntPtr)patchContent.Length, MemoryProtection.ExecuteReadWrite, out oldProtection);
            bool b = Kernel32.WriteProcessMemory(process.Handle, module.BaseAddress + relativeOffset, patchContent, (uint)patchContent.Length, out i);
            Kernel32.VirtualProtectEx(process.Handle, module.BaseAddress + relativeOffset, (UIntPtr)patchContent.Length, oldProtection, out oldProtection);
            return b;
        }

        /// <summary>
        /// Searches the process for a specific byte-pattern and replaces it with a new pattern.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="searchPattern">The pattern to search for.</param>
        /// <param name="replacementPattern">The replacement pattern.</param>
        /// <param name="module">The module to start the replacement process.</param>
        /// <param name="maxReplacements">Specifies how many instances should maximally be replaced.</param>
        /// <returns>Returns how many instances of the pattern have been found and replaced.</returns>
        /// <exception cref="ArgumentException">The length of the pattern to replace must be > 0.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="process" /> is <c>null</c>.</exception>
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        static public int ReplaceSequence(this Process process, byte[] searchPattern, byte[] replacementPattern, ProcessModule module = null, uint maxReplacements = 0u)
        {
            if (process == null) throw new ArgumentNullException("process");
            if (searchPattern == null) throw new ArgumentNullException("searchPattern");
            if (replacementPattern == null) throw new ArgumentNullException("replacementPattern");
            if (searchPattern.Length == 0) throw new ArgumentException("The length of the search pattern must be > 0.");
            if (replacementPattern.Length == 0) throw new ArgumentException("The length of the replacement pattern must be > 0.");
            int numReplacements = 0;
            if (module == null)
                module = process.MainModule;

            ulong offset = 0;
            while (true)
            {
                IntPtr currentOffset = process.FindSequence(searchPattern, module, offset);
                if (currentOffset == IntPtr.Zero)
                    return 0;
                if (process.PatchAt(currentOffset, replacementPattern))
                {
                    offset = (ulong)(currentOffset.ToInt32() - module.BaseAddress.ToInt32() + searchPattern.Length);
                    numReplacements++;
                    if (maxReplacements != 0 && numReplacements == maxReplacements)
                        return numReplacements;
                }
                else
                {
                    var excep = new CopeException("Failed to replace pattern! Patching failed!");
                    excep.Data["CurrentOffset"] = currentOffset.ToString("X8");
                    excep.Data["NumReplacements"] = numReplacements;
                    excep.Data["MaxReplacements"] = maxReplacements;
                    excep.Data["SearchPattern"] = searchPattern.ToHexString();
                    excep.Data["ReplacementPattern"] = replacementPattern.ToHexString();
                    throw excep;
                }
                    
            }
        }

        /// <summary>
        /// Searches the process for a specific byte-pattern and replaces it with a new pattern.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="searchPattern">The pattern to search for.</param>
        /// <param name="replacementPattern">The replacement pattern.</param>
        /// <param name="moduleName">The name of the module to start the replacement process.</param>
        /// <param name="maxReplacements">Specifies how many instances should maximally be replaced.</param>
        /// <returns>Returns how many instances of the pattern have been found and replaced.</returns>
        static public int ReplaceSequence(this Process process, byte[] searchPattern, byte[] replacementPattern, string moduleName, uint maxReplacements = 0u)
        {
            return ReplaceSequence(process, searchPattern, replacementPattern, process.GetModuleByName(moduleName), maxReplacements);
        }

        /// <summary>
        /// Searches for a byte-pattern in a process. You may optionally specify a module to search in and 
        /// a relative offset within that module.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="searchPattern"></param>
        /// <param name="module"></param>
        /// <param name="relativeOffset"></param>
        /// <returns></returns>
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        static public IntPtr FindSequence(this Process process, byte[] searchPattern, ProcessModule module = null, ulong relativeOffset = 0u)
        {
            if (module == null)
                module = process.MainModule;
            ulong position = (ulong)module.BaseAddress + relativeOffset;
            ulong end = (ulong)module.BaseAddress + (ulong)module.ModuleMemorySize;

            IntPtr procHandle = process.Handle;
            int searchLength = searchPattern.Length;
            int read = FIND_BUFFER_SIZE;
            var lastBytes = new byte[read];
            int currentIndex = 0;
            bool isAtEnd = false;

            while (position <= end && !isAtEnd)
            {
                if (end - position < (ulong)read)
                {
                    lastBytes = new byte[end - position];
                    read = (int)(end - position);
                    if (!Kernel32.ReadProcessMemory(procHandle, (IntPtr)position, lastBytes, read, out read))
                    {
                        var excep = new CopeException("Can't read process memory!");
                        excep.Data["ReadOffset"] = position.ToString("X8");
                        process.StoreProcessInfo(excep);
                        throw excep;
                    }
                    isAtEnd = true;
                }
                else
                {
                    if (!Kernel32.ReadProcessMemory(procHandle, (IntPtr)position, lastBytes, read, out read))
                    {
                        var excep = new CopeException("Can't read process memory!");
                        excep.Data["ReadOffset"] = position.ToString("X8");
                        process.StoreProcessInfo(excep);
                        throw excep;
                    }
                }
                for (int i = 0; i < read; i++)
                {
                    if (lastBytes[i] == searchPattern[currentIndex])
                    {
                        currentIndex++;
                        if (currentIndex == searchLength)
                            return (IntPtr)(position + (ulong)(i - currentIndex + 1));
                    }
                    else
                        currentIndex = 0;
                }
                position += (ulong)read;
            }
            return (IntPtr)null;
        }

        /// <summary>
        /// Searches for a byte-pattern in a process. You may optionally specify the name of a module to search in and 
        /// a relative offset within that module.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="searchPattern"></param>
        /// <param name="moduleName"></param>
        /// <param name="relativeOffset"></param>
        /// <returns></returns>
        static public IntPtr FindSequence(this Process process, byte[] searchPattern, string moduleName, ulong relativeOffset = 0u)
        {
            return FindSequence(process, searchPattern, process.GetModuleByName(moduleName), relativeOffset);
        }

        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="process" /> is <c>null</c>.</exception>
        static public void InjectDll(this Process process, string dllPath)
        {
            if (process == null) throw new ArgumentNullException("process");
            if (dllPath == null) throw new ArgumentNullException("dllPath");
            if (!File.Exists(dllPath))
            {
                var excep = new CopeException("Could not find the Dll to inject.");
                excep.Data["DllPath"] = dllPath;
                throw excep;
            }
            try
            {
                ProcessModule krnl32 = process.GetModuleByName("kernel32.dll");
                IntPtr hProc = process.Handle;

                // allocate memory for the name of the dll to load
                int bytesWritten; // dummy
                IntPtr dllNameOffset = Kernel32.VirtualAllocEx(hProc, (IntPtr)null, (uint)dllPath.Length, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ReadWrite);
                Kernel32.WriteProcessMemory(hProc, dllNameOffset, dllPath.ToByteArray(true), (uint)dllPath.Length, out bytesWritten);

                // load the library from the remote thread
                IntPtr threadStart = process.GetProcAddress("LoadLibraryA", krnl32);
                IntPtr newThreadHandle = Kernel32.CreateRemoteThread(hProc, (IntPtr)null, 0, threadStart, dllNameOffset, 0, (IntPtr)null);
                var baseAddress = (IntPtr)WaitForThreadToExit(newThreadHandle);
                Kernel32.CloseHandle(newThreadHandle);
                Kernel32.VirtualFreeEx(process.Handle, dllNameOffset, (uint)dllPath.Length, AllocationType.Release);

                // freeing the library to prevent memory leaks
                threadStart = process.GetProcAddress("FreeLibrary", krnl32);
                newThreadHandle = Kernel32.CreateRemoteThread(hProc, (IntPtr)null, 0, threadStart, baseAddress, 0, (IntPtr)null);
                WaitForThreadToExit(newThreadHandle);
                Kernel32.CloseHandle(newThreadHandle);
            }
            catch (Exception ex)
            {
                var excep = new CopeException(ex, "Failed to inject DLL into process! See ExceptionData.");
                excep.Data["DllPath"] = dllPath;
                process.StoreProcessInfo(excep);
                throw excep;
            }
        }

        /// <exception cref="ArgumentNullException"><paramref name="dllPath" /> is <c>null</c>.</exception>
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        static public void InjectDll(this Process process, string dllPath, string startFunction)
        {
            if (process == null) throw new ArgumentNullException("process");
            if (dllPath == null) throw new ArgumentNullException("dllPath");
            if (startFunction == null) throw new ArgumentNullException("startFunction");
            if (!File.Exists(dllPath))
            {
                var excep = new CopeException("Could not find the Dll to inject.");
                excep.Data["DllPath"] = dllPath;
                throw excep;
            }
            try
            {
                dllPath = dllPath.Replace("\\", "\\\\");
                // get addresses from kernel 32
                ProcessModule krnl32 = process.GetModuleByName("kernel32.dll");
                IntPtr getProcAddress = process.GetProcAddressSafe("GetProcAddress", krnl32);
                IntPtr loadLibrary = process.GetProcAddress("LoadLibraryA", krnl32);
                IntPtr exitThread = process.GetProcAddress("ExitThread", krnl32);

                // get us some memory in the process
                var codeLength = (uint)(dllPath.Length + startFunction.Length + 52);
                IntPtr codeOffset = Kernel32.VirtualAllocEx(process.Handle, (IntPtr)null, codeLength,
                                                         AllocationType.Commit | AllocationType.Reserve,
                                                         MemoryProtection.ExecuteReadWrite);

                // offsets of the name of the DLL to load and the start function name
                var dllNameOffset = (uint)codeOffset;
                uint funcNameOffset = (uint)(dllNameOffset + dllPath.Length + 1);
                uint getProcAddresOffset = (uint)(funcNameOffset + startFunction.Length + 1);
                uint loadLibaryOffset = getProcAddresOffset + 4;
                uint exitOffset = loadLibaryOffset + 4;

                #region byte code construction

                var code = new List<byte>(dllPath.ToByteArray(true));
                code.Add(0x00);
                code.AddRange(startFunction.ToByteArray(true));
                code.Add(0x00);
                code.AddRange(BitConverter.GetBytes((int)getProcAddress));
                code.AddRange(BitConverter.GetBytes((int)loadLibrary));
                code.AddRange(BitConverter.GetBytes((int)exitThread));

                code.AddRange(OpcodeHelper.InitStdCall(0));
                code.AddRange(OpcodeHelper.Move(R32.EAX, dllNameOffset));
                code.Add(OpcodeHelper.Push(R32.EAX));
                code.AddRange(OpcodeHelper.AbsoluteCall(loadLibaryOffset));
                code.AddRange(OpcodeHelper.Move(R32.ECX, funcNameOffset));
                code.Add(OpcodeHelper.Push(R32.ECX));
                code.Add(OpcodeHelper.Push(R32.EAX));
                code.AddRange(OpcodeHelper.AbsoluteCall(getProcAddresOffset));
                code.AddRange(OpcodeHelper.Call(R32.EAX));
                code.AddRange(OpcodeHelper.Move(R8.AL, 0));
                code.AddRange(OpcodeHelper.AbsoluteCall(exitOffset));

                #endregion

                int bytesWritten; // dummy
                Kernel32.WriteProcessMemory(process.Handle, codeOffset, code.ToArray(), codeLength, out bytesWritten);
                if (bytesWritten != codeLength)
                {
                    var excep = new CopeException("Failed to write full byte code to Process.");
                    excep.Data["CodeLengthInBytes"] = codeLength;
                    excep.Data["BytesWritten"] = bytesWritten;
                    excep.Data["WriteOffset"] = codeOffset;
                    throw excep;
                }
                    
                var threadStart = (IntPtr)(funcNameOffset + startFunction.Length + 13);

                IntPtr newThreadHandle = Kernel32.CreateRemoteThread(process.Handle, (IntPtr)null, 0, threadStart, (IntPtr)null, 0,
                                                                  (IntPtr)null);
                WaitForThreadToExit(newThreadHandle);
                Kernel32.CloseHandle(newThreadHandle);
                Kernel32.VirtualFreeEx(process.Handle, codeOffset, codeLength, AllocationType.Release);

                // freeing the library to prevent memory leaks
                threadStart = process.GetProcAddress("FreeLibrary", krnl32);
                newThreadHandle = Kernel32.CreateRemoteThread(process.Handle, (IntPtr)null, 0, threadStart, (IntPtr)dllNameOffset, 0, (IntPtr)null);
                WaitForThreadToExit(newThreadHandle);
                Kernel32.CloseHandle(newThreadHandle);
            }
            catch (Exception ex)
            {
                var excep = new CopeException(ex, "Failed to inject DLL into process! See ExceptionData.");
                process.StoreProcessInfo(excep);
                excep.Data["DllPath"] = dllPath;
                excep.Data["DllStartFunction"] = startFunction;
                throw excep;
            }
        }

        /// <exception cref="ArgumentNullException"><paramref name="startFunction" /> is <c>null</c>.</exception>
        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        static public void InjectDll(this Process process, string dllPath, string startFunction, bool unicodeParameters, params string[] parameter)
        {
            if (process == null) throw new ArgumentNullException("process");
            if (dllPath == null) throw new ArgumentNullException("dllPath");
            if (startFunction == null) throw new ArgumentNullException("startFunction");
            if (!File.Exists(dllPath))
            {
                var excep = new CopeException("Could not find the Dll to inject.");
                excep.Data["DllPath"] = dllPath;
                throw excep;
            }
            try
            {
                // get addresses from kernel 32
                ProcessModule krnl32 = process.GetModuleByName("kernel32.dll");
                IntPtr getProcAddress = process.GetProcAddressSafe("GetProcAddress", krnl32);
                IntPtr loadLibrary = process.GetProcAddress("LoadLibraryA", krnl32);
                IntPtr exitThread = process.GetProcAddress("ExitThread", krnl32);

                // get the total length of the parameters in bytes
                uint paramLength = 0;
                foreach (string t in parameter)
                {
                    if (unicodeParameters)
                        paramLength += (uint)t.Length * 2 + 2;
                    else
                        paramLength += (uint)t.Length + 1;
                }

                // get us some memory in the process
                var codeLength = (uint)(dllPath.Length + startFunction.Length + 52 + paramLength + parameter.Length * 6);
                IntPtr codeOffset = Kernel32.VirtualAllocEx(process.Handle, (IntPtr)null, codeLength,
                                                         AllocationType.Commit | AllocationType.Reserve,
                                                         MemoryProtection.ExecuteReadWrite);
                #region offset calculations

                // offsets of the name of the DLL to load and the start function name
                var dllNameOffset = (uint)codeOffset;
                uint funcNameOffset = (uint)(dllNameOffset + dllPath.Length + 1);
                var paramOffsets = new uint[parameter.Length];

                // calculate the offsets of the parameters
                uint paramBaseOffset = (uint)(funcNameOffset + startFunction.Length + 1);
                paramLength = 0;
                for (int i = 0; i < parameter.Length; i++)
                {
                    paramOffsets[i] = paramBaseOffset + paramLength;
                    if (unicodeParameters)
                        paramLength += (uint)parameter[i].Length * 2 + 2;
                    else
                        paramLength += (uint)parameter[i].Length + 1;
                }

                uint getProcessAddressOffset = paramBaseOffset + paramLength;
                uint loadLibraryOffset = getProcessAddressOffset + 4;
                uint exitOffset = loadLibraryOffset + 4;

                #endregion

                #region byte code construction

                var code = new List<byte>(dllPath.ToByteArray(true));
                code.Add(0x00);
                code.AddRange(startFunction.ToByteArray(true));
                code.Add(0x00);

                // write the parameters to memory
                foreach (string t in parameter)
                {
                    code.AddRange(t.ToByteArray(!unicodeParameters));
                    code.Add(0x00);
                    if (unicodeParameters)
                        code.Add(0x00);
                }

                code.AddRange(BitConverter.GetBytes((int)getProcAddress));
                code.AddRange(BitConverter.GetBytes((int)loadLibrary));
                code.AddRange(BitConverter.GetBytes((int)exitThread));

                code.AddRange(OpcodeHelper.InitStdCall(0));
                code.AddRange(OpcodeHelper.Move(R32.EAX, dllNameOffset));
                code.Add(OpcodeHelper.Push(R32.EAX));
                code.AddRange(OpcodeHelper.AbsoluteCall(loadLibraryOffset));
                code.AddRange(OpcodeHelper.Move(R32.ECX, funcNameOffset));
                code.Add(OpcodeHelper.Push(R32.ECX));
                code.Add(OpcodeHelper.Push(R32.EAX));
                code.AddRange(OpcodeHelper.AbsoluteCall(getProcessAddressOffset));
                // push all the parameters in reverse order
                for (int i = parameter.Length - 1; i >= 0; i--)
                {
                    code.AddRange(OpcodeHelper.Move(R32.ECX, paramOffsets[i]));
                    code.Add(OpcodeHelper.Push(R32.ECX));
                }
                code.AddRange(OpcodeHelper.Call(R32.EAX));
                code.AddRange(OpcodeHelper.Move(R8.AL, 0));
                code.AddRange(OpcodeHelper.AbsoluteCall(exitOffset));

                #endregion

                int bytesWritten; // dummy
                Kernel32.WriteProcessMemory(process.Handle, codeOffset, code.ToArray(), codeLength, out bytesWritten);
                if (bytesWritten != codeLength)
                {
                    var excep = new CopeException("Failed to write full byte code to Process.");
                    excep.Data["CodeLengthInBytes"] = codeLength;
                    excep.Data["BytesWritten"] = bytesWritten;
                    excep.Data["WriteOffset"] = codeOffset;
                    throw excep;
                }
                var threadStart = (IntPtr)(exitOffset + 4);

                var newThreadHandle = Kernel32.CreateRemoteThread(process.Handle, (IntPtr)null, 0, threadStart, (IntPtr)null, 0, (IntPtr)null);
                WaitForThreadToExit(newThreadHandle);
                Kernel32.CloseHandle(newThreadHandle);
                Kernel32.VirtualFreeEx(process.Handle, codeOffset, codeLength, AllocationType.Release);

                // free the library to prevent memory leaks
                threadStart = process.GetProcAddress("FreeLibrary", krnl32);
                newThreadHandle = Kernel32.CreateRemoteThread(process.Handle, (IntPtr)null, 0, threadStart, (IntPtr)dllNameOffset, 0, (IntPtr)null);
                WaitForThreadToExit(newThreadHandle);
                Kernel32.CloseHandle(newThreadHandle);
            }
            catch (Exception ex)
            {
                var excep = new CopeException(ex, "Failed to inject DLL into process! See ExceptionData.");
                process.StoreProcessInfo(excep);
                excep.Data["DllPath"] = dllPath;
                excep.Data["DllStartFunction"] = startFunction;
                excep.Data["UseUnicodeForParameters"] = unicodeParameters;
                if (parameter == null || parameter.Length == 0 )
                    excep.Data["DllFunctionParameters"] = "No parameters";
                else
                {
                    for (int i = 0; i < parameter.Length; i++ )
                        excep.Data["DllFunctionParameter" + i] = parameter[i];
                }
                throw excep;
            }
        }

        /// <exception cref="CopeException">Failed to inject ForwardOperationalBase.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="process" /> is <c>null</c>.</exception>
        static public ForwardPortClient InjectForwardOperationalBase(this Process process, IForwardPortCallback callbackImplementation)
        {
            if (process == null) throw new ArgumentNullException("process");
            if (callbackImplementation == null) throw new ArgumentNullException("callbackImplementation");

            string currentdir = Directory.GetCurrentDirectory() + '\\';
            string managedLoader = currentdir + FORWARD_INJECTEE;
            if (!File.Exists(managedLoader))
            {
                var excep = new CopeException("Could not find the Managed Loader Dll.");
                excep.Data["DllPath"] = managedLoader;
                throw excep;
            }
                
            string forwardBase = currentdir + FORWARD_BASE_DLL;
            if (!File.Exists(forwardBase))
            {
                var excep = new CopeException("Could not find the Forward Operations Base Dll.");
                excep.Data["DllPath"] = forwardBase;
                throw excep;
            }

            try
            {
                process.InjectDll(managedLoader, FORWARD_LOAD_METHOD, true, forwardBase, FORWARD_TYPE_NAME, FORWARD_METHOD_NAME, FORWARD_METHOD_PARAM);
            }
            catch(Exception ex)
            {
                throw new CopeException(ex, "Failed to inject Forward Operations Base.");
            }

            ForwardPortClient client;
            try
            {
                client = new ForwardPortClient(new InstanceContext(callbackImplementation));
                client.Endpoint.Address = new EndpointAddress(new Uri(FORWARD_ENDPOINT_ADDRESS),
                                                              new SpnEndpointIdentity(string.Empty));
                client.Open();
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Failed to establish connection to Forward Operations Base.");
            }
            
            return client;
        }

        #endregion methods
    }
}