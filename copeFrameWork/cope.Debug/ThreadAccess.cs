using System;

namespace cope.Debug
{
    [Flags]
    public enum ThreadAccess
    {
        /// <summary>
        /// Required to delete the object.
        /// </summary>
        DELETE = 0x10000,
        /// <summary>
        /// Required to read information in the security descriptor for the object, not including the information in the SACL.
        /// To read or write the SACL, you must request the ACCESS_SYSTEM_SECURITY access right.For more information, see SACL Access Right
        /// <see cref="http://msdn.microsoft.com/en-us/library/ms686769(v=VS.85).aspx"/>.
        /// </summary>
        READ_CONTROL = 0x20000,
        /// <summary>
        /// Required to modify the DACL in the security descriptor for the object.
        /// </summary>
        WRITE_DAC = 0x40000,
        /// <summary>
        /// Required to change the owner in the security descriptor for the object.
        /// </summary>
        WRITE_OWNER = 0x80000,
        /// <summary>
        /// The right to use the object for synchronization. This enables a thread to wait until the object is in the signaled state.
        /// </summary>
        SYNCHRONIZE = 0x100000,
        /// <summary>
        /// Required for a server thread that impersonates a client.
        /// </summary>
        THREAD_DIRECT_IMPERSONATION = 0x200,
        /// <summary>
        /// Required to read the context of a thread using GetThreadContext
        /// </summary>
        THREAD_GET_CONTEXT = 0x8,
        /// <summary>
        /// Required to use a thread's security information directly without calling it by using a communication mechanism that provides impersonation services.
        /// </summary>
        THREAD_IMPERSONATE = 0x100,
        /// <summary>
        /// Required to read certain information from the thread object, such as the exit code (see GetExitCodeThread).
        /// </summary>
        THREAD_QUERY_INFORMATION = 0x40,
        /// <summary>
        /// Required to read certain information from the thread objects (see GetProcessIdOfThread).
        /// A handle that has the THREAD_QUERY_INFORMATION access right is automatically granted THREAD_QUERY_LIMITED_INFORMATION.
        /// </summary>
        THREAD_QUERY_LIMITED_INFORMATION = 0x800,
        /// <summary>
        /// Required to write the context of a thread using SetThreadContext.
        /// </summary>
        THREAD_SET_CONTEXT = 0x10,
        /// <summary>
        /// Required to set certain information in the thread object.
        /// </summary>
        THREAD_SET_INFORMATION = 0x20,
        /// <summary>
        /// Required to set certain information in the thread object.
        /// A handle that has the THREAD_SET_INFORMATION access right is automatically granted THREAD_SET_LIMITED_INFORMATION.
        /// </summary>
        THREAD_SET_LIMITED_INFORMATION = 0x400,
        /// <summary>
        /// Required to set the impersonation token for a thread using SetThreadToken.
        /// </summary>
        THREAD_SET_THREAD_TOKEN = 0x80,
        /// <summary>
        /// Required to suspend or resume a thread (see SuspendThread and ResumeThread).
        /// </summary>
        THREAD_SUSPEND_RESUME = 0x2,
        /// <summary>
        /// Required to terminate a thread using TerminateThread.
        /// </summary>
        THREAD_TERMINATE = 0x1,
    }
}