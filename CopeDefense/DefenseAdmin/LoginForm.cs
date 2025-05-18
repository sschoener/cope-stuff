using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using cope.Extensions;
using cope;

namespace DefenseAdmin
{
    public partial class LoginForm : Form
    {
        bool m_bValidated;

        public LoginForm()
        {
            InitializeComponent();
        }

        private bool ValidateUser()
        {
            if (m_bValidated)
                return true;
            ServerInterface.AdminName = m_tbxAdminName.Text;
            MD5 md5 = MD5.Create();
            md5.ComputeHash(m_tbxAdminPassword.Text.ToByteArray(true));
            ServerInterface.AdminPassword = md5.Hash.ToHexString(false);

            if (ServerInterface.ValidateAdmin())
            {
                Properties.Settings.Default.AdminName = m_tbxAdminName.Text;
                UIHelper.ShowMessage("Success", "Connection established.");
                m_bValidated = true;
                return true;
            }
            UIHelper.ShowError("Wrong user or password / server does not answer.");
            return false;
        }

        private static void EnsureUnlocksPresent()
        {
            if (!UnlockLibrary.UnlocksFetched)
                UnlockLibrary.GetUnlocks();
        }

        #region
        
        private void BtnConnectClick(object sender, EventArgs e)
        {
            ValidateUser();
        }

        private void BtnManageWargearClick(object sender, EventArgs e)
        {
            if (!ValidateUser())
                return;
            EnsureUnlocksPresent();
            WargearManager wgm = new WargearManager();
            wgm.Show();
        }

        private void BtnManageUpgradesClick(object sender, EventArgs e)
        {
            if (!ValidateUser())
                return;
            EnsureUnlocksPresent();
            UpgradeManager upgm = new UpgradeManager();
            upgm.Show();
        }

        private void BtnManageHeroesClick(object sender, EventArgs e)
        {
            if (!ValidateUser())
                return;
            EnsureUnlocksPresent();
            HeroTypeManager htm = new HeroTypeManager();
            htm.Show();
        }

        private void BtnManageUnlocksClick(object sender, EventArgs e)
        {
            if (!ValidateUser())
                return;
            EnsureUnlocksPresent();
            UnlockManager unm = new UnlockManager();
            unm.Show();
        }

        private void BtnOpenStatsClick(object sender, EventArgs e)
        {
            if (!ValidateUser())
                return;
            StatsForm stats = new StatsForm();
            stats.Show();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AdminName != null)
                m_tbxAdminName.Text = Properties.Settings.Default.AdminName;
        }

        private void TbxAdminNameTextChanged(object sender, EventArgs e)
        {
            m_bValidated = false;
        }

        private void TbxAdminPasswordTextChanged(object sender, EventArgs e)
        {
            m_bValidated = false;
        }

        #endregion
    }
}
