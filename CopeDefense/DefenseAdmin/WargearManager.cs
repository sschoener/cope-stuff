using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using cope;
using DefenseShared;

namespace DefenseAdmin
{
    public partial class WargearManager : Form
    {
        private Wargear m_currentWargear;

        public WargearManager()
        {
            InitializeComponent();
        }

        #region methods

        private static Unlock GetRequiredUnlock(Wargear upg)
        {
            Unlock ul;
            if (UnlockLibrary.CurrentUnlocks.TryGetValue(upg.RequiredUnlockId, out ul))
                return ul;
            return null;
        }

        WargearType GetSelectedWargearType()
        {
            return (WargearType)m_cbxWargearType.SelectedItem;
        }

        Unlock GetSelectedRequirement()
        {
            if (m_cbxRequiredUnlock.SelectedIndex < 0)
                return null;
            return m_cbxRequiredUnlock.SelectedItem as Unlock;
        }

        void WargearSelected(Wargear wargear)
        {
            if (m_currentWargear == wargear)
                return;
            if (m_currentWargear != null)
                UpdateWargear(m_currentWargear);
            m_currentWargear = wargear;
            m_tbxWargearBlueprint.Text = wargear.Blueprint;
            m_tbxWargearId.Text = wargear.Id.ToString();
            m_tbxWargearName.Text = wargear.Name;

            m_rtbWargearDescription.Text = string.Empty;
            var dict = ItemDatabases.Wargear.GetOther(wargear.Id);
            foreach (var kvp in dict)
                m_rtbWargearDescription.Text += @"[[" + kvp.Key + @" = " + kvp.Value + "]]\r\n";
            m_rtbWargearDescription.Text += wargear.Description;

            var reqUnlock = GetRequiredUnlock(wargear);
            if (reqUnlock != null)
                m_cbxRequiredUnlock.SelectedItem = reqUnlock;
            else
                m_cbxRequiredUnlock.SelectedIndex = -1;
            m_cbxWargearType.SelectedItem = wargear.Type;
        }

        void NoWargearSelected()
        {
            if (m_currentWargear != null)
                UpdateWargear(m_currentWargear);
            m_currentWargear = null;
            m_tbxWargearBlueprint.Text = string.Empty;
            m_tbxWargearId.Text = string.Empty;
            m_tbxWargearName.Text = string.Empty;
            m_rtbWargearDescription.Text = string.Empty;
            m_cbxRequiredUnlock.SelectedIndex = -1;
            m_cbxWargearType.SelectedItem = WargearType.Misc;
        }

        Wargear GetSelectedWargear()
        {
            if (m_lbxWargear.SelectedIndex < 0)
                return null;
            return m_lbxWargear.SelectedItem as Wargear;
        }

        void GetWargear()
        {
            var wargearString = ServerInterface.GetWargear();
            var jss = new JavaScriptSerializer();
            var wargears = jss.Deserialize<dynamic>(wargearString)["wargear"];
            foreach (var wargear in wargears)
            {
                Wargear upg = new Wargear
                {
                    Id = wargear["id"],
                    Blueprint = wargear["bp_path"],
                    RequiredUnlockId = wargear["req_unlock_id"],
                    Type = WargearInfo.ParseWargearType(wargear["wargear_type"])
                };
                m_lbxWargear.Items.Add(upg);
            }
            m_cbxWargearType.Items.Add(WargearType.Armor);
            m_cbxWargearType.Items.Add(WargearType.Misc);
            m_cbxWargearType.Items.Add(WargearType.SingleWeapon);
            m_cbxWargearType.Items.Add(WargearType.Weapon1);
            m_cbxWargearType.Items.Add(WargearType.Weapon2);
        }

        Wargear AddWargear()
        {
            int id = ServerInterface.AddWargear();
            if (id < 0)
            {
                UIHelper.ShowError("Failed to add wargear to database.");
                return null;
            }
            var upg = new Wargear
                          {
                              Id = id,
                              Blueprint = string.Empty,
                              Type = WargearType.Misc,
                              RequiredUnlockId = 0
                          };
            m_lbxWargear.Items.Add(upg);
            return upg;
        }

        bool CopyWargear(Wargear wg)
        {
            Wargear copy = AddWargear();
            if (copy == null)
                return false;
            if (!ServerInterface.UpdateWargear(copy.Id, wg.Blueprint, wg.RequiredUnlockId, wg.Type))
            {
                UIHelper.ShowError("Failed to set values of the copy.");
                return false;
            }
            copy.Blueprint = wg.Blueprint;
            copy.RequiredUnlockId = wg.RequiredUnlockId;
            copy.Type = wg.Type;
            ItemDatabases.Wargear.Update(copy.Id, wg.Name, wg.Description);
            var others = ItemDatabases.Wargear.GetOther(wg.Id);
            foreach (var val in others)
                ItemDatabases.Wargear.UpdateOther(copy.Id, val.Key, val.Value);
            m_lbxWargear.Items.Remove(copy);
            m_lbxWargear.Items.Add(copy);
            return true;
        }

        bool RemoveWargear(Wargear wargear)
        {
            if (!ServerInterface.RemoveWargear(wargear.Id))
            {
                UIHelper.ShowError("Failed to remove wargear from database.");
                return false;
            }
            m_lbxWargear.Items.Remove(wargear);
            return true;
        }

        bool UpdateWargear(Wargear wargear)
        {
            string bpPath = m_tbxWargearBlueprint.Text;
            var req = GetSelectedRequirement();
            int reqId = req == null ? 0 : req.Id;
            WargearType type = GetSelectedWargearType();
            if (!ServerInterface.UpdateWargear(wargear.Id, bpPath, reqId, type))
            {
                UIHelper.ShowError("Failed to update wargear information.");
                return false;
            }
            wargear.RequiredUnlockId = reqId;
            wargear.Type = type;
            wargear.Blueprint = bpPath;
            string newName = m_tbxWargearName.Text;
            string newDesc = m_rtbWargearDescription.Text;
            string oldName = wargear.Name;
            ItemDatabases.Wargear.Update(wargear.Id, newName, newDesc);
            if (newName != oldName)
            {
                m_lbxWargear.Items.Remove(wargear);
                m_lbxWargear.Items.Add(wargear);
            }
            return true;
        }

        #endregion

        #region eventhandlers

        private void LbxWargearSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = GetSelectedWargear();
            if (selected == null)
                NoWargearSelected();
            else
                WargearSelected(selected);
        }

        private void BtnAddWargearClick(object sender, EventArgs e)
        {
            AddWargear();
        }

        private void BtnRemoveWargearClick(object sender, EventArgs e)
        {
            var selected = GetSelectedWargear();
            if (selected != null)
                RemoveWargear(selected);
        }

        private void BtnCopyClick(object sender, EventArgs e)
        {
            if (m_currentWargear != null)
                CopyWargear(m_currentWargear);
        }

        private void WargearManagerFormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_currentWargear != null)
                UpdateWargear(m_currentWargear);
            UnlockLibrary.UnlockAdded -= OnUnlockAdded;
            UnlockLibrary.UnlockRemoved -= OnUnlockRemoved;
            UnlockLibrary.UnlockNameUpdated -= UnlockNameUpdated;
        }

        private void WargearManagerShown(object sender, EventArgs e)
        {
            GetWargear();

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

        class Wargear
        {
            public int Id;
            public string Blueprint;
            public int RequiredUnlockId;
            public WargearType Type;

            public string Name
            {
                get { return ItemDatabases.Wargear.GetName(Id); }
            }

            public string Description
            {
                get { return ItemDatabases.Wargear.GetDesc(Id); }
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
