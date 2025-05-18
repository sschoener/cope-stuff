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
using cope.Extensions;

namespace DefenseAdmin
{
    public partial class HeroTypeManager : Form
    {
        private readonly Dictionary<int, HeroType> m_heroTypes = new Dictionary<int, HeroType>();
        private readonly Dictionary<int, UnlockGroup> m_unlockGroups = new Dictionary<int, UnlockGroup>();
        private HeroType m_currentHeroType;

        public HeroTypeManager()
        {
            InitializeComponent();
        }

        #region methods

        void HeroSelected(HeroType ht)
        {
            if (ht == m_currentHeroType)
                return;
            if (m_currentHeroType != null)
                UpdateHeroType(m_currentHeroType);
            m_currentHeroType = ht;
            m_tbxHeroBlueprint.Text = ht.BlueprintPath;
            m_tbxHeroTypeName.Text = ht.Name;
            m_tbxId.Text = ht.Id.ToString();
            m_nupStandardWargear1.Value = ht.Wargear1;
            m_nupStandardWargear2.Value = ht.Wargear2;
            m_nupStandardWargear3.Value = ht.Wargear3;
            m_cbxStandardUnlock.SelectedItem = ht.StandardUnlock;
        }

        void NoHeroSelected()
        {
            m_tbxHeroBlueprint.Text = string.Empty;
            m_tbxHeroTypeName.Text = string.Empty;
            m_tbxId.Text = string.Empty;
            m_cbxStandardUnlock.SelectedIndex = -1;
            m_nupStandardWargear1.Value = 0;
            m_nupStandardWargear2.Value = 0;
            m_nupStandardWargear3.Value = 0;
        }

        void UnlockGroupSelected(UnlockGroup ug)
        {
            m_lbxUnlockGroupEntries.Items.Clear();
            m_lbxUnlockGroupEntries.Items.AddRange(ug.HeroTypes);
        }

        void NoUnlockGroupSelected()
        {
            m_lbxUnlockGroupEntries.Items.Clear();
        }

        HeroType GetSelectedHeroType()
        {
            if (m_lbxHeroTypes.SelectedIndex < 0)
                return null;
            return m_lbxHeroTypes.SelectedItem as HeroType;
        }

        HeroType GetSelectedHeroTypeInUnlockGroup()
        {
            if (m_lbxUnlockGroupEntries.SelectedIndex < 0)
                return null;
            return m_lbxUnlockGroupEntries.SelectedItem as HeroType;
        }

        UnlockGroup GetSelectedUnlockGroup()
        {
            if (m_lbxUnlockGroups.SelectedIndex < 0)
                return null;
            return m_lbxUnlockGroups.SelectedItem as UnlockGroup;
        }

        Unlock GetSelectedStandardUnlock()
        {
            if (m_cbxStandardUnlock.SelectedIndex > 0)
                return m_cbxStandardUnlock.SelectedItem as Unlock;
            return null;
        }

        bool AddNewHeroType()
        {
            int id = ServerInterface.AddHeroType();
            if (id < -1)
            {
                UIHelper.ShowError("Failed to add a new hero type.");
                return false;
            }
            var ht = new HeroType {Id = id};
            m_heroTypes.Add(id, ht);
            m_lbxHeroTypes.Items.Add(ht);
            return true;
        }

        bool RemoveHeroType(HeroType type)
        {
            if (!ServerInterface.RemoveHeroType(type.Id))
            {
                UIHelper.ShowError("Failed to remove hero type.");
                return true;
            }
            if (m_currentHeroType == type)
                m_currentHeroType = null;
            m_heroTypes.Remove(type.Id);
            m_lbxUnlockGroupEntries.Items.Remove(type);
            m_lbxHeroTypes.Items.Remove(type);
            return false;
        }

        bool UpdateHeroType(HeroType type)
        {
            string bpPath = m_tbxHeroBlueprint.Text;
            string name = m_tbxHeroTypeName.Text;
            Unlock stdUnlock = GetSelectedStandardUnlock();
            int stdUnlockId = 0;
            if (stdUnlock != null)
                stdUnlockId = stdUnlock.Id;
            int wg1 = (int) m_nupStandardWargear1.Value;
            int wg2 = (int) m_nupStandardWargear2.Value;
            int wg3 = (int) m_nupStandardWargear3.Value;
            if (!ServerInterface.UpdateHeroType(type.Id, bpPath, name, stdUnlockId, wg1, wg2, wg3))
            {
                UIHelper.ShowError("Failed to update hero type information.");
                return false;
            }
            type.BlueprintPath = bpPath;
            type.Name = name;
            return true;
        }

        bool AddToUnlockGroup(HeroType type, int groupId)
        {
            UnlockGroup ug;
            bool present = m_unlockGroups.TryGetValue(groupId, out ug);
            if (present)
            {
                if (ug.HeroTypes.Contains(type))
                {
                    UIHelper.ShowError("The hero type already belongs to the specified unlock group.");
                    return false;
                }
            }
            if (!ServerInterface.AddUnlockGroupEntry(type.Id, groupId))
            {
                UIHelper.ShowError("Failed to add hero to unlock group.");
                return false;
            }
            if (!present)
            {
                ug = new UnlockGroup {Id = groupId};
                m_unlockGroups.Add(groupId, ug);
                m_lbxUnlockGroups.Items.Add(ug);
            }
            ug.HeroTypes.Add(type);
            if (GetSelectedUnlockGroup() == ug)
                m_lbxUnlockGroupEntries.Items.Add(type);
            return true;
        }

        bool RemoveFromUnlockGroup(HeroType type, int groupId)
        {
            UnlockGroup ug;
            if (!m_unlockGroups.TryGetValue(groupId, out ug))
            {
                UIHelper.ShowError("Invalid group id!");
                return false;
            }
            if (!ug.HeroTypes.Contains(type))
            {
                UIHelper.ShowError("Hero type does not belong to specified group id");
                return false;
            }
            if (!ServerInterface.RemoveUnlockGroupEntry(type.Id, groupId))
            {
                UIHelper.ShowError("Failed to remove unlock group entry.");
                return false;
            }
            ug.HeroTypes.Remove(type);
            if (GetSelectedUnlockGroup() == ug)
                m_lbxUnlockGroupEntries.Items.Remove(type);
            if (ug.HeroTypes.Count == 0)
                m_lbxUnlockGroups.Items.Remove(ug);
            return true;
        }

        void GetHeroTypes()
        {
            m_heroTypes.Clear();
            string heroTypesString = ServerInterface.GetHeroTypes();
            var jss = new JavaScriptSerializer();
            var heroTypes = jss.Deserialize<dynamic>(heroTypesString)["hero_types"];
            foreach (var ht in heroTypes)
            {
                HeroType heroType = new HeroType
                {
                    Id = ht["id"],
                    Name = ht["name"],
                    BlueprintPath = ht["bp_path"],
                    StandardUnlock = UnlockLibrary.CurrentUnlocks[ht["std_unlock"]],
                    Wargear1 = ht["std_wargear1"],
                    Wargear2 = ht["std_wargear2"],
                    Wargear3 = ht["std_wargear3"]
                };
                m_heroTypes.Add(heroType.Id, heroType);
            }
        }

        void GetUnlockGroups()
        {
            string unlockGroupString = ServerInterface.GetUnlockGroupEntries();
            var jss = new JavaScriptSerializer();
            var unlockGroupEntries = jss.Deserialize<dynamic>(unlockGroupString)["unlock_groups"];
            foreach (var entry in unlockGroupEntries)
            {
                int id = entry["unlock_group"];
                UnlockGroup group;
                if (!m_unlockGroups.TryGetValue(id, out group))
                {
                    group = new UnlockGroup {Id = id};
                    m_unlockGroups.Add(id, group);
                }
                group.HeroTypes.Add(m_heroTypes[entry["hero_type"]]);
            }
        }

        #endregion

        #region eventhandlers

        private void BtnAddHeroTypeClick(object sender, EventArgs e)
        {
            AddNewHeroType();
        }

        private void BtnRemoveHeroTypeClick(object sender, EventArgs e)
        {
            var ht = GetSelectedHeroType();
            if (ht != null)
                RemoveHeroType(ht);
        }

        private void BtnAddToUnlockGroupClick(object sender, EventArgs e)
        {
            int id = (int) m_nupUnlockGroupId.Value;
            if (m_chkbxAddToCurrentGroup.Checked)
            {
                var group = GetSelectedUnlockGroup();
                if (group == null)
                    return;
                id = group.Id;
            }
            var ht = GetSelectedHeroType();
            if (ht != null)
                AddToUnlockGroup(ht, id);
        }

        private void BtnRemoveFromUnlockGroupClick(object sender, EventArgs e)
        {
            var ht = GetSelectedHeroTypeInUnlockGroup();
            var group = GetSelectedUnlockGroup();
            if (ht != null && group != null)
                RemoveFromUnlockGroup(ht, group.Id);
        }

        private void LbxUnlockGroupsSelectedIndexChanged(object sender, EventArgs e)
        {
            var group = GetSelectedUnlockGroup();
            if (group == null)
                NoUnlockGroupSelected();
            else
                UnlockGroupSelected(group);
        }

        private void LbxHeroTypesSelectedIndexChanged(object sender, EventArgs e)
        {
            var ht = GetSelectedHeroType();
            if (ht == null)
                NoHeroSelected();
            else
                HeroSelected(ht);
        }

        private void ChkbxAddToCurrentGroupCheckedChanged(object sender, EventArgs e)
        {
            m_nupUnlockGroupId.Enabled = !m_chkbxAddToCurrentGroup.Checked;
        }

        private void HeroTypeManagerFormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_currentHeroType != null)
                UpdateHeroType(m_currentHeroType);
        }

        private void HeroTypeManagerShown(object sender, EventArgs e)
        {
            GetHeroTypes();
            foreach (var kvp in m_heroTypes)
                m_lbxHeroTypes.Items.Add(kvp.Value);
            GetUnlockGroups();
            foreach (var kvp in m_unlockGroups)
                m_lbxUnlockGroups.Items.Add(kvp.Value);
            foreach (var kvp in UnlockLibrary.CurrentUnlocks)
                m_cbxStandardUnlock.Items.Add(kvp.Value);
        }

        #endregion

        class HeroType
        {
            public int Id;
            public string Name = string.Empty;
            public string BlueprintPath = string.Empty;
            public Unlock StandardUnlock;
            public int Wargear1;
            public int Wargear2;
            public int Wargear3;

            public override string ToString()
            {
                return Id + " - " + Name;
            }
        }

        class UnlockGroup
        {
            public readonly HashSet<HeroType> HeroTypes = new HashSet<HeroType>();
            public int Id;

            public override string ToString()
            {
                return Id.ToString();
            }
        }

    }
}


