using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using cope;
using DefenseShared;

namespace DefenseAdmin
{
    public partial class UnlockManager : Form
    {
        private Unlock m_currentUnlock;

        public UnlockManager()
        {
            InitializeComponent();
        }

        #region methods

        Unlock GetSelectedUnlock()
        {
            if (m_lbxUnlocks.SelectedIndex < 0)
                return null;
            return m_lbxUnlocks.SelectedItem as Unlock;
        }

        Unlock GetSelectedRequirement()
        {
            if (m_cbxUnlockRequirement.SelectedIndex < 0)
                return null;
            return m_cbxUnlockRequirement.SelectedItem as Unlock;
        }

        void NoUnlockSelected()
        {
            if (m_currentUnlock != null)
                UpdateUnlock(m_currentUnlock);
            m_currentUnlock = null;
            m_tbxUnlockId.Text = string.Empty;
            m_tbxUnlockName.Text = string.Empty;
            m_rtbUnlockDescription.Text = string.Empty;
            m_nupUnlockPrice.Value = 0;
            m_nupUnlockGroup.Value = 0;
            m_cbxUnlockRequirement.SelectedIndex = -1;
        }

        void UnlockSelected(Unlock unlock)
        {
            if (unlock == m_currentUnlock)
                return;
            if (m_currentUnlock != null)
                UpdateUnlock(m_currentUnlock);
            m_tbxUnlockId.Text = unlock.Id.ToString();
            m_nupUnlockPrice.Value = unlock.Price;
            m_nupUnlockGroup.Value = unlock.UnlockGroup;
            var reqUnlock = GetRequiredUnlock(unlock);
            if (reqUnlock != null)
                m_cbxUnlockRequirement.SelectedItem = reqUnlock;
            else
                m_cbxUnlockRequirement.SelectedIndex = -1;
            m_tbxUnlockName.Text = unlock.Name;

            m_rtbUnlockDescription.Text = string.Empty;
            var dict = ItemDatabases.Unlocks.GetOther(unlock.Id);
            foreach (var kvp in dict)
                m_rtbUnlockDescription.Text += @"[[" + kvp.Key + @" = " + kvp.Value + "]]\r\n";
            m_rtbUnlockDescription.Text += unlock.Description;

            m_currentUnlock = unlock;
        }

        private bool UpdateUnlock(Unlock unlock)
        {
            int price = (int) m_nupUnlockPrice.Value;
            int groupId = (int) m_nupUnlockGroup.Value;
            Unlock req = GetSelectedRequirement();
            int reqId = req == null ? 0 : req.Id;
            if (!ServerInterface.UpdateUnlock(unlock.Id, price, reqId, groupId))
            {
                UIHelper.ShowError("Failed to update unlock information.");
                return false;
            }
            string newName = m_tbxUnlockName.Text;
            string desc = m_rtbUnlockDescription.Text;
            string oldName = unlock.Name;
            unlock.Price = price;
            unlock.UnlockGroup = groupId;
            unlock.RequiredUnlockId = reqId;
            ItemDatabases.Unlocks.Update(unlock.Id, newName, desc);
            if (newName != oldName)
            {
                UnlockLibrary.InvokeUnlockNameUpdated(unlock);
            }
            return true;
        }

        private Unlock AddUnlock()
        {
            int id = ServerInterface.AddUnlock();
            if (id < 0)
            {
                UIHelper.ShowError("Failed to add new unlock.");
                return null;
            }
            var unlock = new Unlock {Id = id, Price = 0, UnlockGroup = 0, RequiredUnlockId = 0};
            UnlockLibrary.InvokeUnlockAdded(unlock);
            return unlock;
        }

        private bool CopyUnlock(Unlock unlock)
        {
            Unlock copy = AddUnlock();
            if (copy == null)
                return false;
            copy.Price = unlock.Price;
            copy.RequiredUnlockId = unlock.RequiredUnlockId;
            copy.UnlockGroup = unlock.UnlockGroup;
            ItemDatabases.Unlocks.Update(copy.Id, unlock.Name, unlock.Description);
            var others = ItemDatabases.Unlocks.GetOther(unlock.Id);
            foreach (var val in others)
                ItemDatabases.Unlocks.UpdateOther(copy.Id, val.Key, val.Value);
            UnlockLibrary.InvokeUnlockNameUpdated(copy);
            return true;
        }

        private bool RemoveUnlock(Unlock unlock)
        {
            if (!ServerInterface.RemoveUnlock(unlock.Id))
            {
                UIHelper.ShowError("Failed to remove unlock.");
                return false;
            }
            UnlockLibrary.InvokeUnlockRemoved(unlock);
            return true;
        }

        private void GetUnlocks()
        {
            var unlocks = UnlockLibrary.CurrentUnlocks;
            foreach (var kvp in unlocks)
            {
                m_lbxUnlocks.Items.Add(kvp.Value);
                m_cbxUnlockRequirement.Items.Add(kvp.Value);
            }
        }

        private static Unlock GetRequiredUnlock(Unlock unlock)
        {
            Unlock ul;
            if (UnlockLibrary.CurrentUnlocks.TryGetValue(unlock.RequiredUnlockId, out ul))
                return ul;
            return null;
        }

        #endregion

        #region eventhandlers

        private void BtnAddUnlockClick(object sender, EventArgs e)
        {
            AddUnlock();
        }

        private void BtnRemoveClick(object sender, EventArgs e)
        {
            var selected = GetSelectedUnlock();
            if (selected != null)
                RemoveUnlock(selected);
        }

        private void BtnCopyClick(object sender, EventArgs e)
        {
            if (m_currentUnlock != null)
                CopyUnlock(m_currentUnlock);
        }

        private void LbxUnlocksSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = GetSelectedUnlock();
            if (selected == null)
                NoUnlockSelected();
            else
                UnlockSelected(selected);
        }

        private void UnlockManagerFormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_currentUnlock != null)
                UpdateUnlock(m_currentUnlock);
            UnlockLibrary.UnlockAdded -= OnUnlockAdded;
            UnlockLibrary.UnlockRemoved -= OnUnlockRemoved;
            UnlockLibrary.UnlockNameUpdated -= OnUnlockNameUpdated;
        }

        private void UnlockManagerShown(object sender, EventArgs e)
        {
            GetUnlocks();
            UnlockLibrary.UnlockAdded += OnUnlockAdded;
            UnlockLibrary.UnlockRemoved += OnUnlockRemoved;
            UnlockLibrary.UnlockNameUpdated += OnUnlockNameUpdated;
        }

        void OnUnlockNameUpdated(Unlock unlock)
        {
            m_lbxUnlocks.Items.Remove(unlock);
            m_cbxUnlockRequirement.Items.Remove(unlock);
            m_lbxUnlocks.Items.Add(unlock);
            m_cbxUnlockRequirement.Items.Add(unlock);
        }

        void OnUnlockAdded(Unlock unlock)
        {
            m_lbxUnlocks.Items.Add(unlock);
            m_cbxUnlockRequirement.Items.Add(unlock);
        }

        void OnUnlockRemoved(Unlock unlock)
        {
            m_lbxUnlocks.Items.Remove(unlock);
            m_cbxUnlockRequirement.Items.Remove(unlock);
        }

        #endregion
    }
}
