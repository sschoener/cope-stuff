/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using System;
using cope;

namespace ModTool.Core
{
    public delegate void LogEventHandler(string logMessage);

    static public class LoggingManager
    {
        private static readonly LogSystem s_logSystem;
        public static event LogEventHandler OnLog;

        static LoggingManager()
        {
            s_logSystem = new LogSystem();
            s_logSystem.OnLog += OnLogMessage;
        }

        static void OnLogMessage(string message)
        {
            if (OnLog != null)
                OnLog(message);
        }

        static public void SendError(string format, params object[] args)
        {
            if (OnLog == null)
                return;
            s_logSystem.SendError(format, args);
        }

        static public void SendWarning(string format, params object[] args)
        {
            if (OnLog == null)
                return;
            s_logSystem.SendWarning(format, args);
        }

        static public void SendMessage(string format, params object[] args)
        {
            if (OnLog == null)
                return;
            s_logSystem.SendMessage(format, args);
        }

        static public void SendMessage(LogSystemMessageType type, string format, params object[] args)
        {
            if (OnLog == null)
                return;
            s_logSystem.SendMessage(type, format, args);
        }

        static public void HandleException(Exception e)
        {
            if (OnLog == null)
                return;
            s_logSystem.HandleException(e);
        }
    }
}