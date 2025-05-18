using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using cope.Extensions;

namespace cope.Debug
{
    /// <summary>
    /// Currently only works for the CURRENT PROCESS!
    /// </summary>
    public sealed class HookManager
    {
        private readonly Dictionary<int, Hook> m_hooks;

        public HookManager(Process process)
        {
            HookedProcess = process;
            m_hooks = new Dictionary<int, Hook>();
        }

        public Process HookedProcess
        {
            get;
            private set;
        }

        public int HookCount
        {
            get { return m_hooks.Count; }
        }

        public void RemoveHook(int id)
        {
            Hook hook;
            if (m_hooks.TryGetValue(id, out hook))
            {
                m_hooks.Remove(id);
                hook.Dispose();
            }
        }

        public string GetHookDebugInfo(int id)
        {
            Hook hook;
            if (m_hooks.TryGetValue(id, out hook))
            {
                return "Hook " + id + " info:\n" + hook.GetDebugInfo();
            }
            return "No hook with id " + id;
        }

        /// <summary>
        /// Installs a hook at 'offset' which calls 'function'. 'numRelevantBytes' specifies how many bytes need to be copied at 'offset' in order to prevent damaging the original code.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="function"></param>
        /// <param name="numRelevantBytes">Must be between 5 and 24.</param>
        /// <returns></returns>
        public int InstallBasicHook(IntPtr offset, IntPtr function, uint numRelevantBytes)
        {
            m_hooks.Add(m_hooks.Count, new Hook(this, offset, function, numRelevantBytes));
            return m_hooks.Count - 1;
        }

        /// <summary>
        /// Installs a hook at module.BaseOffset+'relativeOffset' which calls 'function'.
        /// 'numRelevantBytes' specifies how many bytes need to be copied at 'offset' in order to prevent damaging the original code.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="relativeOffset"></param>
        /// <param name="function"></param>
        /// <param name="numRelevantBytes">Must be between 5 and 24.</param>
        /// <returns></returns>
        public int InstallBasicHook(ProcessModule module, Int32 relativeOffset, IntPtr function, uint numRelevantBytes)
        {
            m_hooks.Add(m_hooks.Count, new Hook(this, module.BaseAddress + relativeOffset, function, numRelevantBytes));
            return m_hooks.Count - 1;
        }

        /// <summary>
        /// Installs a hook at module.BaseOffset+'relativeOffset' which calls 'function'. The function's return value (EAX) will then be copied into the specified register.
        /// 'numRelevantBytes' specifies how many bytes need to be copied at 'offset' in order to prevent damaging the original code.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="relativeOffset"></param>
        /// <param name="register"></param>
        /// <param name="function"></param>
        /// <param name="numRelevantBytes"></param>
        /// <returns></returns>
        public int InstallSetRegisterHook(ProcessModule module, Int32 relativeOffset, R32 register, IntPtr function, uint numRelevantBytes)
        {
            m_hooks.Add(m_hooks.Count, Hook.CreateSetRegisterHook(this, module.BaseAddress + relativeOffset, register, function, numRelevantBytes));
            return m_hooks.Count - 1;
        }

        /// <summary>
        /// Installs a hook at module.BaseOffset+'relativeOffset' which calls 'function'. The function will receive the specified register as a parameter.
        /// 'numRelevantBytes' specifies how many bytes need to be copied at 'offset' in order to prevent damaging the original code.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="relativeOffset"></param>
        /// <param name="register"></param>
        /// <param name="function"></param>
        /// <param name="numRelevantBytes"></param>
        /// <returns></returns>
        public int InstallGetRegisterHook(ProcessModule module, Int32 relativeOffset, R32 register, IntPtr function, uint numRelevantBytes)
        {
            m_hooks.Add(m_hooks.Count, Hook.CreateGetRegisterHook(this, module.BaseAddress + relativeOffset, register, function, numRelevantBytes));
            return m_hooks.Count - 1;
        }

        class Hook : IDisposable
        {
            private IntPtr m_pOffset;
            private IntPtr m_hookData;
            private int m_iNumRelevantBytes;
            private HookManager m_manager;
            private MemoryProtection m_oldProtection;

            /// <exception cref="CopeException">numRelevantBytes must be >= 6 and below 15!</exception>
            static public Hook CreateSetRegisterHook(HookManager manager, IntPtr offset, R32 register, IntPtr function, uint numRelevantBytes)
            {
                if (numRelevantBytes < 6 || numRelevantBytes > 15)
                    throw new CopeException("numRelevantBytes must be >= 6 and < 15!");
                var h = new Hook
                            {
                                m_manager = manager,
                                m_iNumRelevantBytes = (int)numRelevantBytes,
                                m_pOffset = offset,
                                m_hookData = CreateHookData((offset + (int)numRelevantBytes), function)
                            };

                var trampPtr = GetTrampolinePtr(h.m_hookData);
                // read original opcodes into &trampoline + 15
                h.m_manager.HookedProcess.ReadProcessMemory(offset, trampPtr + 0x10, (int)numRelevantBytes);
                // write a jump to trampoline
                h.m_manager.HookedProcess.PatchAt(offset, OpcodeHelper.AbsoluteJump((uint) GetTrampolinePtrPtr(h.m_hookData).ToInt32()));

                // write the first 15 trampoline bytes
                var code = new byte[16];
                code[0] = OpcodeHelper.Push(R32.EDI);
                code[1] = OpcodeHelper.PushAll;
                code[2] = OpcodeHelper.PushFlags;
                code.SetValues(3, OpcodeHelper.AbsoluteCall((uint) GetFunctionPtrPtr(h.m_hookData).ToInt32()));
                code[9] = OpcodeHelper.PopFlags;
                code[10] = OpcodeHelper.Pop(R32.EDI);
                code[11] = OpcodeHelper.Push(R32.EAX);
                code[12] = OpcodeHelper.PopAll;
                code.SetValues(13, OpcodeHelper.Move(register, R32.EDI));
                code[15] = OpcodeHelper.Pop(R32.EDI);

                h.m_manager.HookedProcess.PatchAt(trampPtr, code);

                // write the next jump back from the trampoline
                h.m_manager.HookedProcess.PatchAt(trampPtr + 0x10 + (int)numRelevantBytes,
                                                OpcodeHelper.AbsoluteJump((uint) GetJumpBackPtrPtr(h.m_hookData).ToInt32()));
                h.m_manager.HookedProcess.VirtualProtectEx(trampPtr, 40, MemoryProtection.ExecuteReadWrite, out h.m_oldProtection);
                return h;
            }

            /// <exception cref="CopeException">numRelevantBytes must be >= 6 and below 22!</exception>
            static public Hook CreateGetRegisterHook(HookManager manager, IntPtr offset, R32 register, IntPtr function, uint numRelevantBytes)
            {
                if (numRelevantBytes < 6 || numRelevantBytes > 22)
                    throw new CopeException("numRelevantBytes must be >= 6 and < 22!");
                var h = new Hook
                {
                    m_manager = manager,
                    m_iNumRelevantBytes = (int)numRelevantBytes,
                    m_pOffset = offset,
                    m_hookData = CreateHookData((offset + (int)numRelevantBytes), function)
                };

                var trampPtr = GetTrampolinePtr(h.m_hookData);
                // read original opcodes into &trampoline + 11
                h.m_manager.HookedProcess.ReadProcessMemory(offset, trampPtr + 0xA, (int)numRelevantBytes);
                // write a jump to trampoline
                h.m_manager.HookedProcess.PatchAt(offset, OpcodeHelper.AbsoluteJump((uint)GetTrampolinePtrPtr(h.m_hookData).ToInt32()));

                // write the first 15 trampoline bytes
                var code = new byte[11];
                code[0] = OpcodeHelper.PushAll;
                code[1] = OpcodeHelper.PushFlags;
                code[2] = OpcodeHelper.Push(register);
                code.SetValues(3, OpcodeHelper.AbsoluteCall((uint)GetFunctionPtrPtr(h.m_hookData).ToInt32()));
                code[9] = OpcodeHelper.PopFlags;
                code[10] = OpcodeHelper.PopAll;
                
                h.m_manager.HookedProcess.PatchAt(trampPtr, code);

                // write the next jump back from the trampoline
                h.m_manager.HookedProcess.PatchAt(trampPtr + 0xA + (int)numRelevantBytes,
                                                OpcodeHelper.AbsoluteJump((uint)GetJumpBackPtrPtr(h.m_hookData).ToInt32()));
                h.m_manager.HookedProcess.VirtualProtectEx(trampPtr, 40, MemoryProtection.ExecuteReadWrite, out h.m_oldProtection);
                return h;
            }

            private Hook()
            {
            }

            /// <exception cref="CopeException">numRelevantBytes must be > 6 and below 23!</exception>
            public Hook(HookManager manager, IntPtr offset, IntPtr function, uint numRelevantBytes)
            {
                if (numRelevantBytes < 6 || numRelevantBytes > 23)
                    throw new CopeException("numRelevantBytes must be > 6 and < 23!");
                m_manager = manager;
                m_iNumRelevantBytes = (int)numRelevantBytes;
                m_pOffset = offset;
                m_hookData = CreateHookData((offset + (int)numRelevantBytes), function);

                var trampPtr = GetTrampolinePtr(m_hookData);
                // read original opcodes into &trampoline + 10
                m_manager.HookedProcess.ReadProcessMemory(offset, trampPtr + 0xA, (int)numRelevantBytes);

                // write a jump to trampoline
                m_manager.HookedProcess.PatchAt(offset, OpcodeHelper.AbsoluteJump((uint) GetTrampolinePtrPtr(m_hookData).ToInt32()));

                // write the first 10 trampoline bytes
                var code = new byte[10];
                code[0] = OpcodeHelper.PushAll;
                code[1] = OpcodeHelper.PushFlags;
                code.SetValues(2, OpcodeHelper.AbsoluteCall((uint) GetFunctionPtrPtr(m_hookData).ToInt32()));
                code[8] = OpcodeHelper.PopFlags;
                code[9] = OpcodeHelper.PopAll;
                m_manager.HookedProcess.PatchAt(trampPtr, code);

                // write the next jump back from the trampoline
                m_manager.HookedProcess.PatchAt(trampPtr + 0xA + (int)numRelevantBytes,
                                                OpcodeHelper.AbsoluteJump((uint) GetJumpBackPtrPtr(m_hookData).ToInt32()));
                m_manager.HookedProcess.VirtualProtectEx(trampPtr, 40, MemoryProtection.ExecuteReadWrite, out m_oldProtection);
            }

            public void Dispose()
            {
                if (m_hookData == IntPtr.Zero)
                    return;
                if (m_manager.HookedProcess != null && !m_manager.HookedProcess.HasExited)
                {
                    m_manager.HookedProcess.ReadProcessMemory(GetTrampolinePtr(m_hookData) + 0xA, m_pOffset,
                                                              m_iNumRelevantBytes);
                    m_manager.HookedProcess.VirtualProtectEx(GetTrampolinePtr(m_hookData), 40, m_oldProtection);
                }
                DestroyHookData(m_hookData);
            }

            public string GetDebugInfo()
            {
                string offset = "Offset: " + m_pOffset.ToString("x8") + '\n';
                string jumpBackPtr = "JumpBackPtr: " + GetJumpBackPtr(m_hookData).ToString("x8") + '\n';
                string jumpBackPtrPTr = "*JumpBackPtr: " + GetJumpBackPtrPtr(m_hookData).ToString("x8") + '\n';
                string functionPtr = "FunctionPtr: " + GetFunctionPtr(m_hookData).ToString("x8") + '\n';
                string functionPtrPtr = "*FunctionPtr: " + GetFunctionPtrPtr(m_hookData).ToString("x8") + '\n';

                IntPtr tramp = GetTrampolinePtr(m_hookData);
                string trampPtr = "TrampolinePtr: " + GetTrampolinePtr(m_hookData).ToString("x8") + '\n';
                string trampPtrPtr = "*TrampolinePtr: " + GetTrampolinePtrPtr(m_hookData).ToString("x8") + '\n';
                string trampoline = "Trampoline content: \n" + m_manager.HookedProcess.ReadProcessMemory(tramp, 40).ToHexString();
                return offset + jumpBackPtr + jumpBackPtrPTr + functionPtr + functionPtrPtr + trampPtr + trampPtrPtr + trampoline;
            }

            /// <summary>
            /// Creates unmanaged Hook data, which has a fixed address.
            /// </summary>
            /// <param name="jumpBackPtr">The offset to jump back to after the hook and the trampoline have been executed.</param>
            /// <param name="functionPtr">Pointer to the function which is to be called by the hook.</param>
            /// <returns></returns>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_CreateHookData")]
            [return: MarshalAs(UnmanagedType.SysInt)]
            static extern IntPtr CreateHookData(IntPtr jumpBackPtr, IntPtr functionPtr);

            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_DestroyHookData")]
            static extern void DestroyHookData(IntPtr hookData);

            /// <summary>
            /// Gets the offset to jump back after the hook and the trampoline have been executed.
            /// </summary>
            /// <param name="hookData"></param>
            /// <returns></returns>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_GetJumpBackPtr")]
            [return: MarshalAs(UnmanagedType.SysInt)]
            static extern IntPtr GetJumpBackPtr(IntPtr hookData);

            /// <summary>
            /// Gets the pointer to the offset to jump back after the hook and the trampoline have been executed.
            /// </summary>
            /// <param name="hookData"></param>
            /// <returns></returns>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_GetJumpBackPtrPtr")]
            [return: MarshalAs(UnmanagedType.SysInt)]
            static extern IntPtr GetJumpBackPtrPtr(IntPtr hookData);

            /// <summary>
            /// Gets address of the function pointer which is to be called by the hook.
            /// </summary>
            /// <param name="hookData"></param>
            /// <returns></returns>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_GetFunctionPtr")]
            [return: MarshalAs(UnmanagedType.SysInt)]
            private static extern IntPtr GetFunctionPtr(IntPtr hookData);

            /// <summary>
            /// Gets the pointer to the address of the function which is to be called by the hook.
            /// </summary>
            /// <param name="hookData"></param>
            /// <returns></returns>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_GetFunctionPtrPtr")]
            [return: MarshalAs(UnmanagedType.SysInt)]
            private static extern IntPtr GetFunctionPtrPtr(IntPtr hookData);

            /// <summary>
            /// Gets the offset of the trampoline of the hook.
            /// </summary>
            /// <param name="hookData"></param>
            /// <returns></returns>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_GetTrampolinePtr")]
            [return: MarshalAs(UnmanagedType.SysInt)]
            static extern IntPtr GetTrampolinePtr(IntPtr hookData);

            /// <summary>
            /// Gets a the pointer to the offset of the trampoline of the hook.
            /// </summary>
            /// <param name="hookData"></param>
            /// <returns></returns>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_GetTrampolinePtrPtr")]
            [return: MarshalAs(UnmanagedType.SysInt)]
            static extern IntPtr GetTrampolinePtrPtr(IntPtr hookData);

            /// <summary>
            /// Sets the address of the function to be called by the hook.
            /// </summary>
            /// <param name="hookData"></param>
            /// <param name="functionPtr"></param>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_SetFunctionPtr")]
            private static extern void SetFunctionPtr(IntPtr hookData, IntPtr functionPtr);

            /// <summary>
            /// Sets the contents of the trampoline. These will be copied by the unmanaged code.
            /// </summary>
            /// <param name="hookData"></param>
            /// <param name="trampoline">The byte-code for the trampoline.</param>
            /// <param name="trampolineLength">The length of the trampoline data in bytes.</param>
            [DllImport("cope.Hook86.dll", EntryPoint = "Hook_SetTrampoline")]
            private static extern void SetTrampoline(IntPtr hookData, byte[] trampoline, int trampolineLength);
        }

        private static unsafe void GetRegisters(int* registers)
        {
            // push EAX, EBX, ECX, EDX, ESI, EDI, ESP, EBP
            RegisterInfo ri;
            ri.EBP = registers[0];
            ri.ESP = registers[1];
            ri.EDI = registers[2];
            ri.ESI = registers[3];
            ri.EDX = registers[4];
            ri.ECX = registers[5];
            ri.EBX = registers[6];
            ri.EAX = registers[7];
            //s_registers = ri;
        }

        [DllImport("cope.Hook86.dll", EntryPoint = "SetRegistersCallback")]
        [return: MarshalAs(UnmanagedType.SysInt)]
        static extern void SetRegistersCallback(IntPtr callback);
    }

    public struct RegisterInfo
    {
        public int EAX;
        public int EBX;
        public int ECX;
        public int EDX;
        public int ESI;
        public int EDI;
        public int ESP;
        public int EBP;
    }
}