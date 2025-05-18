using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using cope;
using DefenseShared;

namespace DefenseAdmin
{
    public partial class UpgradeManager : Form
    {
        private Upgrade m_currentUpgrade;

        public UpgradeManager()
        {
            InitializeComponent();
        }

        #region methods

        private static Unlock GetRequiredUnlock(Upgrade upg)
        {
            Unlock ul;
            if (UnlockLibrary.CurrentUnlocks.TryGetValue(upg.RequiredUnlockId, out ul))
                return ul;
            return null;
        }

        UpgradeType GetSelectedUpgradeType()
        {
            return (UpgradeType)m_cbxUpgradeType.SelectedItem;
        }

        Unlock GetSelectedRequirement()
        {
            if (m_cbxRequiredUnlock.SelectedIndex < 0)
                return null;
            return m_cbxRequiredUnlock.SelectedItem as Unlock;
        }

        void UpgradeSelected(Upgrade upg)
        {
            if (m_currentUpgrade == upg)
                return;
            if (m_currentUpgrade != null)
                UpdateUpgrade(m_currentUpgrade);
            m_currentUpgrade = upg;
            m_tbxUpgradeBlueprint.Text = upg.Blueprint;
            m_tbxUpgradeId.Text = upg.Id.ToString();
            m_tbxUpgradeName.Text = upg.Name;

            m_rtbUpgradeDescription.Text = string.Empty;
            var dict = ItemDatabases.Upgrades.GetOther(upg.Id);
            foreach (var kvp in dict)
                m_rtbUpgradeDescription.Text += @"[[" + kvp.Key + @" = " + kvp.Value + "]]\r\n";
            m_rtbUpgradeDescription.Text += upg.Description;

            var reqUnlock = GetRequiredUnlock(upg);
            if (reqUnlock != null)
                m_cbxRequiredUnlock.SelectedItem = reqUnlock;
            else
                m_cbxRequiredUnlock.SelectedIndex = -1;
            m_cbxUpgradeType.SelectedItem = upg.Type;
        }

        void NoUpgradeSelected()
        {
            if (m_currentUpgrade != null)
                UpdateUpgrade(m_currentUpgrade);
            m_currentUpgrade = null;
            m_tbxUpgradeBlueprint.Text = string.Empty;
            m_tbxUpgradeId.Text = string.Empty;
            m_tbxUpgradeName.Text = string.Empty;
            m_rtbUpgradeDescription.Text = string.Empty;
            m_cbxRequiredUnlock.SelectedIndex = -1;
            m_cbxUpgradeType.SelectedItem = UpgradeType.Misc;
        }

        Upgrade GetSelectedUpgrade()
        {
            if (m_lbxUpgrades.SelectedIndex < 0)
                return null;
            return m_lbxUpgrades.SelectedItem as Upgrade;
        }

        void GetUpgrades()
        {
            var upgradeString = ServerInterface.GetUpgrades();
            var jss = new JavaScriptSerializer();
            var upgrades = jss.Deserialize<dynamic>(upgradeString)["upgrades"];
            foreach (var upgrade in upgrades)
            {
                Upgrade upg = new Upgrade
                {
                    Id = upgrade["id"],
                    Blueprint = upgrade["bp_path"],
                    RequiredUnlockId = upgrade["req_unlock_id"],
                    Type = UpgradeInfo.ParseUpgradeType(upgrade["upgrade_type"])
                };
                m_lbxUpgrades.Items.Add(upg);
            }
            m_cbxUpgradeType.Items.Add(UpgradeType.Hidden);
            m_cbxUpgradeType.Items.Add(UpgradeType.Misc);
            m_cbxUpgradeType.Items.Add(UpgradeType.Skill1);
            m_cbxUpgradeType.Items.Add(UpgradeType.Skill2);
            m_cbxUpgradeType.Items.Add(UpgradeType.Skill3);
            m_cbxUpgradeType.Items.Add(UpgradeType.Skill4);
            m_cbxUpgradeType.Items.Add(UpgradeType.Skill5);
            m_cbxUpgradeType.Items.Add(UpgradeType.Unit);
        }

        Upgrade AddUpgrade()
        {
            int id = ServerInterface.AddUpgrade();
            if (id < 0)
            {
                UIHelper.ShowError("Failed to add upgrade to database.");
                return null;
            }
            var upg = new Upgrade
                          {
                              Id = id,
                              Blueprint = string.Empty,
                              Type = UpgradeType.Misc,
                              RequiredUnlockId = 0
                          };
            m_lbxUpgrades.Items.Add(upg);
            return upg;
        }

        bool CopyUpgrade(Upgrade upg)
        {
            Upgrade copy = AddUpgrade();
            if (copy == null)
                return false;
            if (!ServerInterface.UpdateUpgrade(copy.Id, upg.Blueprint, upg.RequiredUnlockId, upg.Type))
            {
                UIHelper.ShowError("Failed to set values of the copy.");
                return false;
            }
            copy.Blueprint = upg.Blueprint;
            copy.RequiredUnlockId = upg.RequiredUnlockId;
            copy.Type = upg.Type;
            ItemDatabases.Upgrades.Update(copy.Id, upg.Name, upg.Description);
            var others = ItemDatabases.Upgrades.GetOther(upg.Id);
            foreach (var val in others)
                ItemDatabases.Upgrades.UpdateOther(copy.Id, val.Key, val.Value);
            m_lbxUpgrades.Items.Remove(copy);
            m_lbxUpgrades.Items.Add(copy);
            return true;
        }

        bool RemoveUpgrade(Upgrade upgrade)
        {
            if (!ServerInterface.RemoveUpgrade(upgrade.Id))
            {
                UIHelper.ShowError("Failed to remove upgrade from database.");
                return false;
            }
            m_lbxUpgrades.Items.Remove(upgrade);
            return true;
        }

        bool UpdateUpgrade(Upgrade upgrade)
        {
            string bpPath = m_tbxUpgradeBlueprint.Text;
            var req = GetSelectedRequirement();
            int reqId = req == null ? 0 : req.Id;
            UpgradeType type = GetSelectedUpgradeType();
            if (!ServerInterface.UpdateUpgrade(upgrade.Id, bpPath, reqId, type))
            {
                UIHelper.ShowError("Failed to update upgrade information.");
                return false;
            }
            upgrade.RequiredUnlockId = reqId;
            upgrade.Type = type;
            upgrade.Blueprint = bpPath;
            string newName = m_tbxUpgradeName.Text;
            string newDesc = m_rtbUpgradeDescription.Text;
            string oldName = upgrade.Name;
            ItemDatabases.Upgrades.Update(upgrade.Id, newName, newDesc);
            if (newName != oldName)
            {
                m_lbxUpgrades.Items.Remove(upgrade);
                m_lbxUpgrades.Items.Add(upgrade);
            }
            return true;
        }

        #endregion

        #region eventhandlers

        private void LbxUpgradesSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = GetSelectedUpgrade();
            if (selected == null)
                NoUpgradeSelected();
            else
                UpgradeSelected(selected);
        }

        private void BtnAddUpgradeClick(object sender, EventArgs e)
        {
            AddUpgrade();
        }

        private void BtnRemoveUpgradeClick(object sender, EventArgs e)
        {
            var selected = GetSelectedUpgrade();
            if (selected != null)
                RemoveUpgrade(selected);
        }

        private void BtnCopyClick(object sender, EventArgs e)
        {
            if (m_currentUpgrade != null)
                CopyUpgrade(m_currentUpgrade);
        }

        private void UpgradeManagerFormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_currentUpgrade != null)
                UpdateUpgrade(m_currentUpgrade);
            UnlockLibrary.UnlockAdded -= OnUnlockAdded;
            UnlockLibrary.UnlockRemoved -= OnUnlockRemoved;
            UnlockLibrary.UnlockNameUpdated -= UnlockNameUpdated;
        }

        private void UpgradeManagerShown(object sender, EventArgs e)
        {
            GetUpgrades();

            foreach (var kvp in UnlockLibrary.CurrentUnlocks)
                m_cbxRequiredUnlock.Items.Add(kvp.Value);

            UnlockLibrary.UnlockAdded += OnUnlockAdded;
            UnlockLibrary.UnlockRemoved += OnUnlockRemoved;
            UnlockLibrary.UnlockNameUpdated += UnlockNameUpdated;
        }

        void UnlockNameUpdated(Unlock obj)
        {
            m_cbxRequiredUnlock.Items.Remove(obj);
            m_cbxRequiredUnlock.Items.Add(obj);
        }

        void OnUnlockRemoved(Unlock obj)
        {
            m_cbxRequiredUnlock.Items.Remove(obj);
        }

        void OnUnlockAdded(Unlock obj)
        {
            m_cbxRequiredUnlock.Items.Add(obj);
        }

        #endregion

        class Upgrade
        {
            public int Id;
            public string Blueprint;
            public int RequiredUnlockId;
            public UpgradeType Type;

            public string Name
            {
                get { return ItemDatabases.Upgrades.GetName(Id); }
            }

            public string Description
            {
                get { return ItemDatabases.Upgrades.GetDesc(Id); }
            }

            public override string ToString()
            {
                return Type + " - " + Name;
            }
        }
    }
}
