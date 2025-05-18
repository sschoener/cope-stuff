#region

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using cope;
using cope.Extensions;

#endregion

namespace CopeDefenseLauncher
{
    public partial class MainForm : Form, IForwardPortCallback
    {
        public MainForm()
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.UserName))
                m_tbxUsername.Text = Properties.Settings.Default.UserName;
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.DoW2Arguments))
                m_tbxStartArguments.Text = Properties.Settings.Default.DoW2Arguments;
        }

        #region eventhandlers

        private void StartButtonClick(object sender, EventArgs e)
        {
            if (!ValidateUser())
                return;
            DoW2Bridge.StartArguments = m_tbxStartArguments.Text;
            ThreadPool.QueueUserWorkItem(Start);
        }

        private void PlayerSetupButtonClick(object sender, EventArgs e)
        {
            if (!ValidateUser())
                return;
            var psf = new PlayerSetupForm();
            psf.ShowDialog();
        }

        #endregion

        #region IForwardPortCallback Members

        public void SendMessage(string message)
        {
            if (!InvokeRequired)
                m_rtbOutput.Text += message + '\n';
            else
                Invoke(new MethodInvoker(() => { m_rtbOutput.Text += message + '\n'; }));
            Program.LogMessage(message);
        }

        #endregion

        /// <summary>
        /// Tries to validate the given user data. Returns true on success, false otherwise.
        /// Displays an error message if validation failed.
        /// </summary>
        /// <returns></returns>
        private bool ValidateUser()
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(m_tbxPassword.Text.ToByteArray(true));
            string pwd = hash.ToHexString(false);
            ServerInterface.UserName = m_tbxUsername.Text;
            ServerInterface.HashedPassword = pwd;
            bool userValid = ServerInterface.ValidateUser();
            if (!userValid)
            {
                UIHelper.ShowError("Invalid user or password.");
                return false;
            }
            Properties.Settings.Default.UserName = m_tbxUsername.Text;
            return true;
        }

        private void Start(object o)
        {
            SendMessage("Starting Cope's Defense Mod...");
            try
            {
                if (DoW2Bridge.StartDoW2(this))
                    DoW2Bridge.SetClientUser();
                else
                    UIHelper.ShowError("Failed to start Cope's Defense Mod.");
            }
            catch (Exception ex)
            {
                SendMessage(ex.GetInfo().Aggregate(string.Empty, (s1, s2) => s1 + s2));
                UIHelper.ShowError("Failed to start Cope's Defense Mod.");
            }
        }
    }
}