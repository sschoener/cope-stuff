#region

using System;
using System.Linq;
using cope.Extensions;

#endregion

namespace cope
{
    public enum LogSystemMessageType
    {
        Warning,
        Error,
        Normal
    }

    public class LogSystem
    {
        public event Action<string> OnLog;

        public void SendMessage(string format, params object[] args)
        {
            if (OnLog == null)
                return;
            string time = DateTime.Now.ToString("HH':'mm':'ss");
            OnLog(time + " - " + string.Format(format, args));
        }

        public void SendError(string format, params object[] args)
        {
            if (OnLog == null)
                return;
            string time = DateTime.Now.ToString("HH':'mm':'ss");
            OnLog(time + " - ERROR - " + string.Format(format, args));
        }

        public void SendWarning(string format, params object[] args)
        {
            if (OnLog == null)
                return;
            string time = DateTime.Now.ToString("HH':'mm':'ss");
            OnLog(time + " - WARNING - " + string.Format(format, args));
        }

        public void SendMessage(LogSystemMessageType type, string format, params object[] args)
        {
            if (OnLog == null)
                return;
            string time = DateTime.Now.ToString("HH':'mm':'ss");
            switch (type)
            {
                case LogSystemMessageType.Warning:
                    OnLog(time + " - WARNING - " + string.Format(format, args));
                    break;
                case LogSystemMessageType.Error:
                    OnLog(time + " - ERROR - " + string.Format(format, args));
                    break;
                case LogSystemMessageType.Normal:
                    OnLog(time + " - " + string.Format(format, args));
                    break;
                default:
                    break;
            }
        }

        public void Send(string s)
        {
            SendMessage(s);
        }

        public void HandleException(Exception e)
        {
            if (OnLog == null)
                return;
            SendMessage(e.GetInfo().Aggregate(string.Empty, (result, s) => result + s + '\n'));
            if (e.InnerException != null)
                HandleException(e.InnerException);
        }
    }
}