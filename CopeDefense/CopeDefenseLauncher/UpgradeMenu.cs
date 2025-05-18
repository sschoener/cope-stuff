using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cope;
using DefenseShared;

namespace CopeDefenseLauncher
{
    public partial class UpgradeMenu : Form
    {
        internal HeroInfo Hero
        {
            get;
            set;
        }

        public UpgradeMenu()
        {
            InitializeComponent();
        }

        #region methods

        private int GetAvailableMiscSlots()
        {
            return GameMechanics.GetNumMiscUpgradeSlots(Hero) - m_lbxActiveMisc.Items.Count;
        }

        private int GetAvailableUnitSlots()
        {
            return GameMechanics.GetNumUnitSlots(Hero) - m_lbxActiveUnits.Items.Count;
        }

        private UpgradeInfo GetCurrentSkill()
        {
            if (m_chklbxSkills.SelectedIndex < 0)
                return null;
            return m_chklbxSkills.SelectedItem as UpgradeInfo;
        }

        private UpgradeInfo GetCurrentActiveUnit()
        {
            if (m_lbxActiveUnits.SelectedIndex < 0)
                return null;
            return m_lbxActiveUnits.SelectedItem as UpgradeInfo;
        }

        private UpgradeInfo GetCurrentAvailableUnit()
        {
            if (m_lbxAvailableUnits.SelectedIndex < 0)
                return null;
            return m_lbxAvailableUnits.SelectedItem as UpgradeInfo;
        }

        private UpgradeInfo GetCurrentActiveMisc()
        {
            if (m_lbxActiveMisc.SelectedIndex < 0)
                return null;
            return m_lbxActiveMisc.SelectedItem as UpgradeInfo;
        }

        private UpgradeInfo GetCurrentAvailableMisc()
        {
            if (m_lbxAvailableMisc.SelectedIndex < 0)
                return null;
            return m_lbxAvailableMisc.SelectedItem as UpgradeInfo;
        }

        private void UncheckAllSkillsBut(int idx)
        {
            for (int i = 0; i < m_chklbxSkills.Items.Count; i++)
            {
                if (i != idx)
                    m_chklbxSkills.SetItemChecked(i, false);
            }
                
        }

        private void UpdateSlotDisplay()
        {
            m_labMiscSlots.Text = GetAvailableMiscSlots().ToString();
            
            m_labUnitSlots.Text = GetAvailableUnitSlots().ToString();
        }

        private void NoSkillSelected()
        {
            m_labSkillName.Text = string.Empty;
            m_rtbSkillDescription.Text = string.Empty;
            m_labSkillEnergyCost.Text = @"0";
            m_picbxSkill.Image = null;
        }

        private void SkillSelected(UpgradeInfo skill)
        {
            m_labSkillName.Text = ItemDatabases.Upgrades.GetName(skill.Id);
            m_rtbSkillDescription.Text = ItemDatabases.Upgrades.GetDesc(skill.Id);
            m_labSkillEnergyCost.Text = GetSkillEnergyCost(skill).ToString();
            m_picbxSkill.Image = GetUpgradeImage(skill);
        }

        private void NoUnitSelected()
        {
            m_rtbUnitDescription.Text = string.Empty;
            m_labUnitName.Text = string.Empty;
            m_labUnitPopCost.Text = @"0";
            m_labUnitReqCost.Text = @"0";
            m_picbxUnit.Image = null;
        }

        private void UnitSelected(UpgradeInfo unit)
        {
            m_rtbUnitDescription.Text = ItemDatabases.Upgrades.GetDesc(unit.Id);
            m_labUnitName.Text = ItemDatabases.Upgrades.GetName(unit.Id);
            m_labUnitPopCost.Text = GetUnitPopCost(unit).ToString();
            m_labUnitReqCost.Text = GetUnitRequisitionCost(unit).ToString();
            m_picbxUnit.Image = GetUpgradeImage(unit);
        }

        private void NoMiscSelected()
        {
            m_rtbMiscDescription.Text = string.Empty;
            m_picbxMisc.Image = null;
        }

        private void MiscSelected(UpgradeInfo misc)
        {
            m_rtbMiscDescription.Text = ItemDatabases.Upgrades.GetDesc(misc.Id);
            m_picbxMisc.Image = GetUpgradeImage(misc);
        }

        private void UpdateUnitDisplay()
        {
            m_lbxActiveUnits.Items.Clear();
            m_lbxAvailableUnits.Items.Clear();
            foreach (var upgrade in Hero.Upgrades)
            {
                if (upgrade.UpgradeType == UpgradeType.Unit)
                    m_lbxActiveUnits.Items.Add(upgrade);
            }
            foreach (var upgrade in Hero.AvailableUpgrades)
            {
                if (upgrade.UpgradeType == UpgradeType.Unit)
                    m_lbxAvailableUnits.Items.Add(upgrade);
            }
        }

        private void UpdateMiscDisplay()
        {
            m_lbxActiveMisc.Items.Clear();
            m_lbxAvailableMisc.Items.Clear();
            foreach (var upgrade in Hero.Upgrades)
            {
                if (upgrade.UpgradeType == UpgradeType.Misc)
                    m_lbxActiveMisc.Items.Add(upgrade);
            }
            foreach (var upgrade in Hero.AvailableUpgrades)
            {
                if (upgrade.UpgradeType == UpgradeType.Misc)
                    m_lbxAvailableMisc.Items.Add(upgrade);
            }
        }

        private void UpdateSkillDisplay(UpgradeType skillLevel)
        {
            if (skillLevel == UpgradeType.Misc || skillLevel == UpgradeType.Unit)
                return;
            m_chklbxSkills.Items.Clear();
            foreach (var upgrade in Hero.AvailableUpgrades.Where(upgrade => upgrade.UpgradeType == skillLevel))
                m_chklbxSkills.Items.Add(upgrade, false);
            foreach (var upgrade in Hero.Upgrades.Where(upgrade => upgrade.UpgradeType == skillLevel))
                m_chklbxSkills.Items.Add(upgrade, true);
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        private UpgradeType GetSelectedSkillLevel()
        {
            string selected = m_cbxSkillLevel.SelectedItem as string;
            switch (selected.ToLowerInvariant())
            {
                case "skill level 1":
                    return UpgradeType.Skill1;
                case "skill level 5":
                    return UpgradeType.Skill2;
                case "skill level 10":
                    return UpgradeType.Skill3;
                case "skill level 15":
                    return UpgradeType.Skill4;
                case "skill level 20":
                    return UpgradeType.Skill5;
            }
            throw new Exception("Unknown skill level: " + selected);
        }

        private bool AddUpgrade(UpgradeInfo upg)
        {
            if (Hero.Upgrades.Any(upgrade => upg.Id == upgrade.Id))
                return true;
            if (!ServerInterface.AddUpgrade(Hero, upg.Id))
            {
                UIHelper.ShowError("Failed to add upgrade. Try again!");
                return false;
            }
            Hero.AvailableUpgrades.RemoveAll(upgrade => upg.Id == upgrade.Id);
            Hero.Upgrades.Add(upg);
            return true;
        }

        private bool RemoveUpgrade(UpgradeInfo upg)
        {
            if (Hero.Upgrades.All(upgrade => upg.Id != upgrade.Id))
                return true;
            if (!ServerInterface.RemoveUpgrade(Hero, upg.Id))
            {
                UIHelper.ShowError("Failed to remove upgrade. Try again");
                return false;
            }
            Hero.Upgrades.RemoveAll(upgrade => upg.Id == upgrade.Id);
            Hero.AvailableUpgrades.Add(upg);
            return true;
        }

        private bool ActivateUnit(UpgradeInfo unit)
        {
            if (!AddUpgrade(unit))
                return false;
            m_lbxAvailableUnits.Items.Remove(unit);
            m_lbxActiveUnits.Items.Add(unit);
            return true;
        }

        private bool DeactivateUnit(UpgradeInfo misc)
        {
            if (!RemoveUpgrade(misc))
                return false;
            m_lbxActiveUnits.Items.Remove(misc);
            m_lbxAvailableUnits.Items.Add(misc);
            return true;
        }

        private bool ActivateMisc(UpgradeInfo misc)
        {
            if (!AddUpgrade(misc))
                return false;
            m_lbxAvailableMisc.Items.Remove(misc);
            m_lbxActiveMisc.Items.Add(misc);
            return true;
        }

        private bool DeactivateMisc(UpgradeInfo misc)
        {
            if (!RemoveUpgrade(misc))
                return false;
            m_lbxActiveMisc.Items.Remove(misc);
            m_lbxAvailableMisc.Items.Add(misc);
            return true;
        }

        private static int GetSkillEnergyCost(UpgradeInfo skill)
        {
            string valueString = ItemDatabases.Upgrades.GetOther(skill.Id, "ENERGY_COST");
            int energyCost;
            if (int.TryParse(valueString, out energyCost))
                return energyCost;
            return 0;
        }

        private static int GetUnitPopCost(UpgradeInfo unit)
        {
            string valueString = ItemDatabases.Upgrades.GetOther(unit.Id, "POP_COST");
            int popCost;
            if (int.TryParse(valueString, out popCost))
                return popCost;
            return 0;
        }

        private static int GetUnitRequisitionCost(UpgradeInfo unit)
        {
            string valueString = ItemDatabases.Upgrades.GetOther(unit.Id, "REQUISITION_COST");
            int energyCost;
            if (int.TryParse(valueString, out energyCost))
                return energyCost;
            return 0;
        }

        private static Image GetUpgradeImage(UpgradeInfo wg)
        {
            string iconPath = ItemDatabases.Upgrades.GetOther(wg.Id, "ICON_PATH");
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

        #endregion

        #region eventhandlers

        private void UpgradeMenuShown(object sender, EventArgs e)
        {
            m_cbxSkillLevel.SelectedIndex = 0;
            UpdateUnitDisplay();
            UpdateMiscDisplay();
            UpdateSlotDisplay();
            NoUnitSelected();
            NoSkillSelected();
        }

        private void ChklbxSkillsSelectedIndexChanged(object sender, EventArgs e)
        {
            var skill = GetCurrentSkill();
            if (skill == null)
                NoSkillSelected();
            else
                SkillSelected(skill);
        }

        private void CbxSkillLevelSelectedIndexChanged(object sender, EventArgs e)
        {
            UpgradeType skillLevel = GetSelectedSkillLevel();
            UpdateSkillDisplay(skillLevel);
        }

        private void ChklbxSkillsItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == e.CurrentValue)
                return;
            if (e.NewValue == CheckState.Unchecked)
            {
                if (!RemoveUpgrade(m_chklbxSkills.Items[e.Index] as UpgradeInfo))
                    e.NewValue = CheckState.Checked;
            }
            else if (e.NewValue == CheckState.Checked)
            {
                UncheckAllSkillsBut(e.Index);
                if (!AddUpgrade(m_chklbxSkills.Items[e.Index] as UpgradeInfo))
                    e.NewValue = CheckState.Unchecked;
            }
        }

        private void LbxActiveUnitsSelectedIndexChanged(object sender, EventArgs e)
        {
            var unit = GetCurrentActiveUnit();
            if (unit == null)
                NoUnitSelected();
            else
                UnitSelected(unit);
        }

        private void LbxAvailableUnitsSelectedIndexChanged(object sender, EventArgs e)
        {
            var unit = GetCurrentAvailableUnit();
            if (unit == null)
                NoUnitSelected();
            else
                UnitSelected(unit);
        }

        private void BtnActivateUnitClick(object sender, EventArgs e)
        {
            var unit = GetCurrentAvailableUnit();
            if (unit == null)
                return;
            if (GetAvailableUnitSlots() <= 0)
            {
                UIHelper.ShowMessage("Sorry", "You can't select any more units!");
                return;
            }
            if (ActivateUnit(unit))
                UpdateSlotDisplay();
        }

        private void BtnDeactivateAllUnitsClick(object sender, EventArgs e)
        {
            if (m_lbxActiveUnits.Items.Count <= 0)
                return;
            while (m_lbxActiveUnits.Items.Count > 0)
            {
                var obj = m_lbxActiveUnits.Items[0];
                DeactivateUnit(obj as UpgradeInfo);
            }
            UpdateSlotDisplay();
        }

        private void BtnDeactivateUnitClick(object sender, EventArgs e)
        {
            var unit = GetCurrentActiveUnit();
            if (unit == null)
                return;
            if (DeactivateUnit(unit))
                UpdateSlotDisplay();
        }

        private void LbxActiveMiscSelectedIndexChanged(object sender, EventArgs e)
        {
            var misc = GetCurrentActiveMisc();
            if (misc == null)
                NoMiscSelected();
            else
                MiscSelected(misc);
        }

        private void LbxAvailableMiscSelectedIndexChanged(object sender, EventArgs e)
        {
            var misc = GetCurrentAvailableMisc();
            if (misc == null)
                NoMiscSelected();
            else
                MiscSelected(misc);
        }

        private void BtnActivateMiscClick(object sender, EventArgs e)
        {
            var misc = GetCurrentAvailableMisc();
            if (misc == null)
                return;
            if (GetAvailableMiscSlots() <= 0)
            {
                UIHelper.ShowMessage("Sorry", "You can't select any more misc upgrades!");
                return;
            }
            if (ActivateMisc(misc))
                UpdateSlotDisplay();
        }

        private void BtnDeactivateAllMiscClick(object sender, EventArgs e)
        {
            if (m_lbxActiveMisc.Items.Count <= 0)
                return;
            while (m_lbxActiveMisc.Items.Count > 0)
            {
                var obj = m_lbxActiveMisc.Items[0];
                DeactivateMisc(obj as UpgradeInfo);
            }
            UpdateSlotDisplay();
        }

        private void BtnDeactivateMiscClick(object sender, EventArgs e)
        {
            var misc = GetCurrentActiveMisc();
            if (misc == null)
                return;
            if (DeactivateMisc(misc))
                UpdateSlotDisplay();
        }

        #endregion
    }
}
