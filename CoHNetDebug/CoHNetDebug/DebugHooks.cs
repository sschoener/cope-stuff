using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using cope.Debug;

namespace CoHNetDebug
{
    static class DebugHooks
    {
        private static HookManager s_hooks;
        private delegate void Hook();

        static public void Init(Process proc)
        {
            /*s_hooks = new HookManager(Main.CurrentProcess);
            Hook test = TestFunction;
            s_hooks.InstallSetRegisterHook(s_hooks.HookedProcess.GetModuleByName("WW2Mod.dll"), 0x2C54D2, R32.EAX,
                                           Marshal.GetFunctionPointerForDelegate(s_testHook), 7);
            cope.Debug.RegisterHelper.GetEAX()*/
        }
    }
}
