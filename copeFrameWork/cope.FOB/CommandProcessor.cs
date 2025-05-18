namespace cope.FOB
{
    /// <summary>
    /// Processes commands sent to the ForwardOperationalBase.
    /// </summary>
    public static class CommandProcessor
    {
        static GenericMethod<string, string> s_cmdProc;
        static readonly object s_cmdLock = new object();

        /// <summary>
        /// Sets the command-processor. Any command sent to the FOB will be passed to this method.
        /// </summary>
        public static GenericMethod<string, string> Processor
        {
            get
            {
                return s_cmdProc;
            }
            set
            {
                lock (s_cmdLock)
                {
                    s_cmdProc = value;
                }
            }
        }

        static internal object CommandLock
        {
            get
            {
                return s_cmdLock;
            }
        }
    }
}