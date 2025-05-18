using System;

namespace cope.FOB
{
    /// <summary>
    /// Proxyclass for the ForwardOperationalBase.HostedService.
    /// </summary>
    public static class DebugLog
    {
        #region logging

        public static void SendPlainMessage(string msg)
        {
            if (ForwardOperationalBase.HostedService != null)
                ForwardOperationalBase.HostedService.SendMessageToClients(msg);
        }

        public static void SendMessage(string format, params object[] args)
        {
            if (ForwardOperationalBase.HostedService != null)
                ForwardOperationalBase.HostedService.SendMessage(format, args);
        }

        public static void SendError(string format, params object[] args)
        {
            if (ForwardOperationalBase.HostedService != null)
                ForwardOperationalBase.HostedService.SendError(format, args);
        }

        public static void SendWarning(string format, params object[] args)
        {
            if (ForwardOperationalBase.HostedService != null)
                ForwardOperationalBase.HostedService.SendWarning(format, args);
        }

        public static void HandleException(Exception e)
        {
            if (ForwardOperationalBase.HostedService != null)
                ForwardOperationalBase.HostedService.HandleException(e);
        }

        #endregion
    }
}
