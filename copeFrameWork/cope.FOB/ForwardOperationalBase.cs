using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.ServiceModel.Description;

namespace cope.FOB
{
    public static class ForwardOperationalBase
    {
        static ServiceHost s_localHost;

        /// <summary>
        /// Hosts the FOB. This method is called from the native DLL.
        /// </summary>
        /// <param name="dummy"></param>
        /// <returns></returns>
        static public int Establish(string dummy)
        {
            Uri baseAddr = new Uri("net.pipe://localhost/cope.ForwardOperationalBase");
            try
            {
                s_localHost = new ServiceHost(new ForwardLoader(), baseAddr);
                ServiceEndpoint ep = s_localHost.AddServiceEndpoint(typeof(IForwardPort), new NetNamedPipeBinding(), "ForwardPort");
                
                /*ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.HttpGetUrl = new Uri("http://localhost:8000/cope.ForwardOperationalBase");
                localHost.Description.Behaviors.Add(smb);*/
                s_localHost.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error hosting WCF endpoint: " + e.Message);
                s_localHost.Abort();
            }
            return 0;
        }

        /// <summary>
        /// Closes the service, kills the FOB.
        /// </summary>
        static public void Kill()
        {
            s_localHost.Close();
        }

        /// <summary>
        /// Gets the instance of the hosted service.
        /// </summary>
        static public ForwardLoader HostedService
        {
            get { return (s_localHost.SingletonInstance as ForwardLoader); }
        }
    }
}