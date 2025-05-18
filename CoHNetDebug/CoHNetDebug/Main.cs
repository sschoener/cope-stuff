using System.Diagnostics;
using cope.Debug;

namespace CoHNetDebug
{
    public class Main
    {
        public static int Init(string dummy)
        {
            CurrentProcess = Process.GetCurrentProcess();
            CoHBridge.LuaInit();
            DebugHooks.Init(CurrentProcess);
            return 0;
        }

        public static Process CurrentProcess { get; private set; }
    }
}
