#region

using System;
using System.Diagnostics;
using System.Text;

#endregion

namespace cope.Extensions
{
    public static class ProcessExt
    {
        /// <summary>
        /// Collects information about the process and stores it in the Data-object of the selected exception.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="exception"></param>
        /// <exception cref="ArgumentNullException"><paramref name="exception" /> is <c>null</c>.</exception>
        public static void StoreProcessInfo(this Process process, Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            exception.Data["ProcessInfo"] = GetProcessInfo(process, true, true);
        }

        /// <summary>
        /// Gathers information about the process and returns the information as a string.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="moduleInfo">Set to true to dump information about the modules in use.</param>
        /// <param name="threadInfo">Set to true to dump information about the threads.</param>
        /// <returns></returns>
        public static string GetProcessInfo(this Process process, bool moduleInfo = false, bool threadInfo = false)
        {
            if (process == null)
                return "Process is NULL.";
            StringBuilder procInfo = new StringBuilder();
            procInfo.AppendLine("Arguments: ", process.StartInfo.Arguments);
            if (process.HasExited)
            {
                procInfo.AppendLine("Exit code: ", process.ExitCode);
                procInfo.AppendLine("Exit time: ", process.ExitTime.ToProperString());
            }
            procInfo.AppendLine("File name: ", process.StartInfo.FileName);
            procInfo.AppendLine("Handle: ", process.Handle);
            procInfo.AppendLine("Has exited: ", process.HasExited);
            procInfo.AppendLine("Id: ", process.Id);
            procInfo.AppendLine("Main module: ", process.MainModule.ModuleName);
            procInfo.AppendLine("Name: ", process.ProcessName);
            procInfo.AppendLine("Responding: ", process.Responding);
            procInfo.AppendLine("Start time: ", process.StartTime.ToProperString());
            if (moduleInfo)
                procInfo.Append(GetModuleInfo(process));
            if (threadInfo)
                procInfo.Append(GetThreadInfo(process));
            return procInfo.ToString();
        }

        private static string GetThreadInfo(Process process)
        {
            StringBuilder threadInfo = new StringBuilder();
            threadInfo.AppendLine("Threads: ");
            try
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    threadInfo.AppendLine("\tThread Id: ", thread.Id.ToString("X8"));
                    threadInfo.AppendLine("\t\tStart address: ", thread.StartAddress.ToString("X8"));
                    threadInfo.AppendLine("\t\tStart time: ", thread.StartTime.ToProperString());
                    threadInfo.AppendLine("\t\tState: ", thread.ThreadState.ToString());
                    if (thread.ThreadState == ThreadState.Wait)
                        threadInfo.AppendLine("\t\tWait reason: ", thread.WaitReason.ToString());
                    threadInfo.AppendLine("\t\tTotal processor time (ms): ",
                                          thread.TotalProcessorTime.Milliseconds.ToString());
                    threadInfo.AppendLine("\t\t\tKernel mode time (ms): ",
                                          thread.PrivilegedProcessorTime.Milliseconds.ToString());
                    threadInfo.AppendLine("\t\t\tUser mode time (ms): ",
                                          thread.UserProcessorTime.Milliseconds.ToString());
                }
            }
            catch (Exception ex)
            {
                threadInfo.AppendLine("Failed to get thread information - " + ex.Message);
            }
            return threadInfo.ToString();
        }

        private static string GetModuleInfo(Process process)
        {
            StringBuilder modInfo = new StringBuilder();
            modInfo.AppendLine("Modules loaded:");
            try
            {
                foreach (ProcessModule module in process.Modules)
                {
                    modInfo.AppendLine("\t", module.ModuleName);
                    modInfo.AppendLine("\t\tFile name: ", module.FileName);
                    modInfo.AppendLine("\t\tEntry point address: ", module.EntryPointAddress.ToString("X8"));
                    modInfo.AppendLine("\t\tBase address: ", module.BaseAddress.ToString("X8"));
                    modInfo.AppendLine("\t\tSize in memory: ", module.ModuleMemorySize.ToString("X8"));
                }
            }
            catch (Exception ex)
            {
                modInfo.AppendLine("Failed to get module information - " + ex.Message);
            }
            return modInfo.ToString();
        }
    }
}