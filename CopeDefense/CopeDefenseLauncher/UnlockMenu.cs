using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cope;
using cope.Extensions;
using DefenseShared;

namespace CopeDefenseLauncher
{
    public partial class UnlockMenu : Form
    {
        internal HeroInfo Hero { get; set; }

        public UnlockMenu()
        {
            InitializeComponent();
        }

        private void UpdateMoneyDisplay()
        {
            m_labMoney.Text = Hero.Money.ToString();
        }

        private void NoUnlockSelected()
        {
            m_labUnlockName.Text = string.Empty;
            m_labUnlockPrice.Text = string.Empty;
            m_labUnlockReqName.Text = string.Empty;
            m_rtbUnlockDescription.Text = string.Empty;
            m_picbxUnlock.Image = null;
        }

        private void UnlockSelected(UnlockInfo info)
        {
            m_labUnlockName.Text = ItemDatabases.Unlocks.GetName(info.Id);
            m_labUnlockPrice.Text = info.Price.ToString();
            m_labUnlockReqName.Text = info.RequiredId <= 0 ? "None" : ItemDatabases.Unlocks.GetName(info.RequiredId);
            m_rtbUnlockDescription.Text = ItemDatabases.Unlocks.GetDesc(info.Id);
            m_picbxUnlock.Image = GetUpgradeImage(info);
        }

        private UnlockInfo GetSelected()
        {
            if (m_lbxUnlocks.SelectedIndex >= 0)
                return m_lbxUnlocks.SelectedItem as UnlockInfo;
            return null;
        }

        private void UpdateUnlockDisplay()
        {
            m_lbxUnlocks.Items.Clear();
            if (m_chkbxOnlyShowAvailable.Checked)
            {
                var unlocks = Hero.AvailableUnlocks.Where(unlock => Hero.UnlockIds.Contains(unlock.RequiredId));
                m_lbxUnlocks.Items.AddRange(unlocks);
            }
            else
                m_lbxUnlocks.Items.AddRange(Hero.AvailableUnlocks);
        }

        private static Image GetUpgradeImage(UnlockInfo unlock)
        {
            string iconPath = ItemDatabases.Unlocks.GetOther(unlock.Id, "ICON_PATH");
            string path = Path.Combine(Directory.GetCurrentDirectory(), iconPath) + ".png";
            if (string.IsNullOrWhiteSpace(iconPath) || !File.Exists(path))
                return null;
            Image img;
            try
            {
                img = Image.FromFile(path);
            }
            catch
            {
                return null;
            }
            return img;
        }

        #region eventshandlers

        private void BtnPurchaseClick(object sender, EventArgs e)
        {
            var selected = GetSelected();
            if (selected == null)
            {
                UIHelper.ShowError("No unlock selected!");
                return;
            }
            if (Hero.Money < selected.Price)
            {
                UIHelper.ShowMessage("Sorry", "You can't afford this unlock.");
                return;
            }
            if (selected.RequiredId >= 0 && !Hero.UnlockIds.Contains(selected.RequiredId))
            {
                UIHelper.ShowMessage("Sorry", "You need to purchase the required unlock first: " + ItemDatabases.Unlocks.GetName(selected.RequiredId));
                return;
            }
            if (!ServerInterface.PurchaseUnlock(Hero, selected.Id))
            {
                UIHelper.ShowError("Couldn't complete unlock. Try again.");
                return;
            }
            UIHelper.ShowMessage("Success", "You got " + ItemDatabases.Unlocks.GetName(selected.Id) + ".");
            Hero.AvailableUnlocks.Remove(selected);
            Hero.UnlockIds.Add(selected.Id);
            Hero.Money -= selected.Price;
            UpdateMoneyDisplay();
            UpdateUnlockDisplay();
        }

        private void UnlockMenuShown(object sender, EventArgs e)
        {
            UpdateMoneyDisplay();
            NoUnlockSelected();
            UpdateUnlockDisplay();
        }

        private void LbxUnlocksSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = GetSelected();
            if (selected == null)
                NoUnlockSelected();
            else
                UnlockSelected(selected);
        }

        private void ChkbxOnlyShowAvailableCheckedChanged(object sender, EventArgs e)
        {
            UpdateUnlockDisplay();
        }

        #endregion
    }
}
