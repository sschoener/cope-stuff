using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Text;

namespace cope.FOB
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ForwardLoader : IForwardPort
    {
        private readonly List<IForwardPortCallback> m_callbacks = new List<IForwardPortCallback>();

        /// <summary>
        /// Loads an assembly and creates an instance of the class with the specified type name.
        /// </summary>
        /// <param name="path">The path to the assembly to load.</param>
        /// <param name="startClass">The full qualified type name of the class type to create an instance of.</param>
        public void LoadAssembly(string path, string startClass)
        {
            try
            {
                Assembly lib = Assembly.LoadFrom(path);
                lib.CreateInstance(startClass);
            }
            catch (Exception e)
            {
                SendMessage("Failed to load assemlby!");
                HandleException(e);
            }
        }

        /// <summary>
        /// Loads an assembly, creates an instance of the class with the specified type name and calls the specified method.
        /// If the specified method is static, no instance will be created.
        /// </summary>
        /// <param name="path">The path to the assembly to load.</param>
        /// <param name="startClass">The full qualified type name of the class type to create an instance of.</param>
        /// <param name="startMethod">The name of the method to invoke. It should be a parameterless method.</param>
        /// <param name="isStatic">Set to true if the method is static.</param>
        public void LoadAssemblyAndStartMethod(string path, string startClass, string startMethod, bool isStatic)
        {
            try
            {
                Assembly lib = Assembly.LoadFrom(path);
                if (isStatic)
                {
                    lib.GetType(startClass).GetMethod(startMethod).Invoke(null, null);
                    return;
                }
                object cla = lib.CreateInstance(startClass);
                cla.GetType().GetMethod(startMethod).Invoke(cla, null);
            }
            catch (Exception e)
            {
                SendMessage("Failed to load assemlby!");
                HandleException(e);
            }
        }

        /// <summary>
        /// Sends a command to the FOB. Part of the IForwardPort interface.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string ReceiveCommand(string command)
        {
            lock (CommandProcessor.CommandLock)
            {
                if (CommandProcessor.Processor != null)
                    return CommandProcessor.Processor.Invoke(command);
                return "No CommandProcessor present!";
            }
        }

        /// <summary>
        /// Registers a client for callbacks. Part of the IForwardPort interface.
        /// </summary>
        public void RegisterCallbackClient()
        {
            var client = OperationContext.Current.GetCallbackChannel<IForwardPortCallback>();
            if (!m_callbacks.Contains(client))
                m_callbacks.Add(client);
            client.SendMessage("Client registered");
        }

        /// <summary>
        /// Unregisters a client for callbacks. Part of the IForwardPort interface.
        /// </summary>
        public void KillCallbackClient()
        {
            var client = OperationContext.Current.GetCallbackChannel<IForwardPortCallback>();
            client.SendMessage("Unregistering client");
            m_callbacks.Remove(client);
        }

        /// <summary>
        /// Gets a ping value which can be used to check whether the connection has been successfully established.
        /// Should always be 1337. Part of the IForwardPort interface.
        /// </summary>
        /// <returns></returns>
        public int Ping()
        {
            return 1337;
        }

        /// <summary>
        /// Sends a message to all connected callback clients.
        /// </summary>
        /// <param name="message"></param>
        public void SendMessageToClients(string message)
        {
            foreach (var client in m_callbacks)
                client.SendMessage(message);
        }

        #region logging

        /// <summary>
        /// Sends a message to all connected callback clients.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void SendMessage(string format, params object[] args)
        {
            string time = DateTime.Now.ToString("HH':'mm':'ss");
            if (args == null)
                SendMessageToClients(time + " - " + format);
            else
                SendMessageToClients(time + " - " + string.Format(format, args));
        }

        /// <summary>
        /// Sends an error message to all connected callback clients.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void SendError(string format, params object[] args)
        {
            string time = DateTime.Now.ToString("HH':'mm':'ss");
            if (args == null)
                SendMessageToClients(time + " - ERROR - " + format);
            else
                SendMessageToClients(time + " - ERROR - " + string.Format(format, args));
        }

        /// <summary>
        /// Sends a warning message to all connected callback clients.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void SendWarning(string format, params object[] args)
        {
            string time = DateTime.Now.ToString("HH':'mm':'ss");
            if (args == null)
                SendMessageToClients(time + " - WARNING - " + format);
            else
                SendMessageToClients(time + " - WARNING - " + string.Format(format, args));
        }

        /// <summary>
        /// Sends an exception info message to all connected callback clients.
        /// </summary>
        /// <param name="e"></param>
        public void HandleException(Exception e)
        {
            string exception = "Exception Info\n\t\tType: " + e.GetType().Name +
                               "\n\t\tMessage: " + e.Message +
                               "\n\t\tException source: " + e.Source +
                               "\n\t\tStack trace: \n" + e.StackTrace + '\n';

            if (e.Data.Count > 0)
            {
                exception += "\n\t\tAdditonal data:";
                StringBuilder additionalData = new StringBuilder(260);
                foreach (DictionaryEntry de in e.Data)
                    additionalData.Append("\n\t\t\t" + de.Key + ":" + de.Value);
                exception += additionalData;
            }
            else
                exception += "\n\t\tNo additonal data available.";

            if (e.InnerException == null)
                exception += "\n\t\tNo InnerExpcetion\n";
            else
                exception += "\n\t\tInnerException following\n";
            SendMessageToClients(exception);
            if (e.InnerException != null)
                HandleException(e.InnerException);
        }

        #endregion
    }

    public delegate TR GenericMethod<out TR, in TA>(TA a);
}