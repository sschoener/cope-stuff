using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cope;
using cope.Extensions;
using DefenseShared;

namespace CopeDefenseLauncher
{
    public partial class WargearMenu : Form
    {
        internal HeroInfo Hero
        {
            get;
            set;
        }

        public WargearMenu()
        {
            InitializeComponent();
        }

        #region methods

        private int GetNumAccessorySlots()
        {
            return GameMechanics.GetNumAccessorySlots(Hero) - m_lbxEquippedAccessories.Items.Count;
        }

        private void UpdateEquippedArmorDisplay()
        {
            var wg = GetEquippedArmor();
            if (wg != null)
            {
                m_labEquippedArmorName.Text = ItemDatabases.Wargear.GetName(wg.Id);
                m_rtbEquippedArmorDesc.Text = ItemDatabases.Wargear.GetDesc(wg.Id);
                m_picbxEquippedArmor.Image = GetWargearImage(wg);
            }
            else
            {
                m_labEquippedArmorName.Text = string.Empty;
                m_rtbEquippedArmorDesc.Text = string.Empty;
                m_picbxEquippedArmor.Image = null;
            }
        }

        private void UpdateEquippedPrimaryWeaponDisplay()
        {
            var wg = GetEquippedPrimary();
            if (wg != null)
            {
                m_labEquippedPrimaryWeaponName.Text = ItemDatabases.Wargear.GetName(wg.Id);
                m_rtbEquippedPrimaryWeaponDesc.Text = ItemDatabases.Wargear.GetDesc(wg.Id);
                m_picbxEquippedPrimaryWeapon.Image = GetWargearImage(wg);
            }
            else
            {
                m_labEquippedPrimaryWeaponName.Text = string.Empty;
                m_rtbEquippedPrimaryWeaponDesc.Text = string.Empty;
                m_picbxEquippedPrimaryWeapon.Image = null;
            }
        }

        private void UpdateEquippedSecondaryWeaponDisplay()
        {
            var wg = GetEquippedSecondary();
            if (wg != null)
            {
                m_labEquippedSecondaryWeaponName.Text = ItemDatabases.Wargear.GetName(wg.Id);
                m_rtbEquippedSecondaryWeaponDesc.Text = ItemDatabases.Wargear.GetDesc(wg.Id);
                m_picbxEquippedSecondaryWeapon.Image = GetWargearImage(wg);
            }
            else
            {
                m_labEquippedSecondaryWeaponName.Text = string.Empty;
                m_rtbEquippedSecondaryWeaponDesc.Text = string.Empty;
                m_picbxEquippedSecondaryWeapon.Image = null;
            }
        }

        private void InitEquippedAccessoryDisplay()
        {
            m_lbxEquippedAccessories.Items.AddRange(GetEquippedAccessories());
            m_labNumAccessorySlots.Text = GetNumAccessorySlots().ToString();
        }

        private void UpdateEquippedAccessoryDisplay()
        {
            m_labNumAccessorySlots.Text = GetNumAccessorySlots().ToString();
        }

        private void FillArmorDisplay()
        {
            FillWargearDisplay(m_lbxArmors, WargearType.Armor);
        }

        private void FillPrimaryDisplay()
        {
            foreach (var wg in Hero.AvailableWargear)
                if (wg.WargearType == WargearType.Weapon1 || wg.WargearType == WargearType.SingleWeapon)
                    m_lbxPrimaryWeapons.Items.Add(wg);
        }

        private void FillSecondaryDisplay()
        {
            FillWargearDisplay(m_lbxSecondaryWeapons, WargearType.Weapon2);
        }

        private void FillAccessoryDisplay()
        {
            FillWargearDisplay(m_lbxAccessories, WargearType.Misc);
        }

        private void FillWargearDisplay(ListBox lbx, WargearType wgtype)
        {
            foreach (var wg in Hero.AvailableWargear)
                if (wg.WargearType == wgtype)
                    lbx.Items.Add(wg);
        }
        
        private WargearInfo GetSelectedArmor()
        {
            if (m_lbxArmors.SelectedIndex < 0)
                return null;
            return m_lbxArmors.SelectedItem as WargearInfo;
        }

        private WargearInfo GetSelectedPrimary()
        {
            if (m_lbxPrimaryWeapons.SelectedIndex < 0)
                return null;
            return m_lbxPrimaryWeapons.SelectedItem as WargearInfo;
        }

        private WargearInfo GetSelectedSecondary()
        {
            if (m_lbxSecondaryWeapons.SelectedIndex < 0)
                return null;
            return m_lbxSecondaryWeapons.SelectedItem as WargearInfo;
        }

        private WargearInfo GetSelectedAccessory()
        {
            if (m_lbxAccessories.SelectedIndex < 0)
                return null;
            return m_lbxAccessories.SelectedItem as WargearInfo;
        }

        private WargearInfo GetSelectedEquippedAccessory()
        {
            if (m_lbxEquippedAccessories.SelectedIndex < 0)
                return null;
            return m_lbxEquippedAccessories.SelectedItem as WargearInfo;
        }

        private WargearInfo GetEquippedArmor()
        {
            return Hero.Wargear.FirstOrDefault(wg => wg.WargearType == WargearType.Armor);
        }

        private WargearInfo GetEquippedPrimary()
        {
            return
                Hero.Wargear.FirstOrDefault(
                    wg => wg.WargearType == WargearType.Weapon1 || wg.WargearType == WargearType.SingleWeapon);
        }

        private WargearInfo GetEquippedSecondary()
        {
            return Hero.Wargear.FirstOrDefault(wg => wg.WargearType == WargearType.Weapon2);
        }

        private IEnumerable<WargearInfo> GetEquippedAccessories()
        {
            return Hero.Wargear.Where(wg => wg.WargearType == WargearType.Misc);
        }

        private void NoArmorSelected()
        {
            m_rtbArmorDescription.Text = string.Empty;
            m_labArmorName.Text = string.Empty;
            m_picbxArmor.Image = null;
        }

        private void ArmorSelected(WargearInfo wg)
        {
            m_rtbArmorDescription.Text = ItemDatabases.Wargear.GetDesc(wg.Id);
            m_labArmorName.Text = ItemDatabases.Wargear.GetName(wg.Id);
            m_picbxArmor.Image = GetWargearImage(wg);
        }

        private void NoPrimarySelected()
        {
            m_rtbPrimaryWeaponDescription.Text = string.Empty;
            m_labPrimaryWeaponName.Text = string.Empty;
            m_picbxPrimaryWeapon.Image = null;
        }

        private void PrimarySelected(WargearInfo wg)
        {
            m_rtbPrimaryWeaponDescription.Text = ItemDatabases.Wargear.GetDesc(wg.Id);
            m_labPrimaryWeaponName.Text = ItemDatabases.Wargear.GetName(wg.Id);
            m_picbxPrimaryWeapon.Image = GetWargearImage(wg);
        }

        private void NoSecondarySelected()
        {
            m_rtbSecondaryWeaponDescription.Text = string.Empty;
            m_labSecondaryWeaponName.Text = string.Empty;
            m_picbxSecondaryWeapon.Image = null;
        }

        private void SecondarySelected(WargearInfo wg)
        {
            m_rtbSecondaryWeaponDescription.Text = ItemDatabases.Wargear.GetDesc(wg.Id);
            m_labSecondaryWeaponName.Text = ItemDatabases.Wargear.GetName(wg.Id);
            m_picbxSecondaryWeapon.Image = GetWargearImage(wg);
        }

        private void NoAccessorySelected()
        {
            m_rtbAccessoryDescription.Text = string.Empty;
            m_labAccessoryName.Text = string.Empty;
            m_picbxAccessory.Image = null;
        }

        private void AccessorySelected(WargearInfo wg)
        {
            m_rtbAccessoryDescription.Text = ItemDatabases.Wargear.GetDesc(wg.Id);
            m_labAccessoryName.Text = ItemDatabases.Wargear.GetName(wg.Id);
            m_picbxAccessory.Image = GetWargearImage(wg);
        }

        private void NoEquippedAccessorySelected()
        {
            m_rtbCurAccessoryDescription.Text = string.Empty;
            m_picbxEquippedAccessory.Image = null;
        }

        private void EquippedAccessorySelected(WargearInfo wg)
        {
            m_rtbCurAccessoryDescription.Text = ItemDatabases.Wargear.GetDesc(wg.Id);
            m_picbxEquippedAccessory.Image = GetWargearImage(wg);
        }

        private bool EquipWargear(WargearInfo wg)
        {
            if (!ServerInterface.EquipWargear(Hero, wg.Id))
            {
                UIHelper.ShowError("Could not equip wargear. Try again.");
                return false;
            }
            return true;
        }

        private bool UnequipWargear(WargearInfo wg)
        {
            if (!ServerInterface.UnequipWargear(Hero, wg.Id))
            {
                UIHelper.ShowError("Could not unequip wargear. Try again.");
                return false;
            }
            return true;
        }

        private bool DisableSecondary()
        {
            var secondary = GetEquippedSecondary();
            if (secondary != null && !UnequipWargear(secondary))
                return false;
            if (secondary != null)
            {
                m_lbxSecondaryWeapons.Items.Add(secondary);
                Hero.Wargear.Remove(secondary);
            }
            m_btnEquipSecondaryWeapon.Enabled = false;
            return true;
        }

        private void EnableSecondary()
        {
            m_btnEquipSecondaryWeapon.Enabled = true;
        }

        private void EquipArmor(WargearInfo wg)
        {
            if (wg.WargearType != WargearType.Armor)
                return;
            if (Hero.Wargear.Any(wargear => wg.Id == wargear.Id))
                return;
            var equipped = GetEquippedArmor();
            if (equipped != null  && !UnequipWargear(equipped))
                return;
            if (!EquipWargear(wg))
                return;
            Hero.Wargear.Add(wg);
            m_lbxArmors.Items.Remove(wg);
            if (equipped != null)
            {
                Hero.Wargear.Remove(equipped);
                m_lbxArmors.Items.Add(equipped);
            }
            UpdateEquippedArmorDisplay();
        }

        private void EquipPrimary(WargearInfo wg)
        {
            if (wg.WargearType != WargearType.Weapon1 && wg.WargearType != WargearType.SingleWeapon)
                return;
            if (Hero.Wargear.Any(wargear => wg.Id == wargear.Id))
                return;
            var equipped = GetEquippedPrimary();
            if (equipped != null && !UnequipWargear(equipped))
                return;

            // is this a single weapon?
            if (wg.WargearType == WargearType.SingleWeapon)
            {
                if (!DisableSecondary())
                    return;
            }
            else
                EnableSecondary();
            if (!EquipWargear(wg))
                return;
            Hero.Wargear.Add(wg);
            m_lbxPrimaryWeapons.Items.Remove(wg);
            if (equipped != null)
            {
                Hero.Wargear.Remove(equipped);
                m_lbxPrimaryWeapons.Items.Add(equipped);
            }
            UpdateEquippedPrimaryWeaponDisplay();
            UpdateEquippedSecondaryWeaponDisplay();
        }

        private void EquipSecondary(WargearInfo wg)
        {
            if (wg.WargearType != WargearType.Weapon2)
                return;
            if (Hero.Wargear.Any(wargear => wg.Id == wargear.Id))
                return;
            var equipped = GetEquippedSecondary();
            if (equipped != null && !UnequipWargear(equipped))
                return;
            if (!EquipWargear(wg))
                return;
            Hero.Wargear.Add(wg);
            m_lbxSecondaryWeapons.Items.Remove(wg);
            if (equipped != null)
            {
                Hero.Wargear.Remove(equipped);
                m_lbxSecondaryWeapons.Items.Add(equipped);
            }
            UpdateEquippedSecondaryWeaponDisplay();
            return;
        }
        
        private void EquipAccessory(WargearInfo wg)
        {
            if (wg.WargearType != WargearType.Misc)
                return;
            if (Hero.Wargear.Any(wargear => wg.Id == wargear.Id))
                return;
            if (!EquipWargear(wg))
                return;
            Hero.Wargear.Add(wg);
            m_lbxEquippedAccessories.Items.Add(wg);
            m_lbxAccessories.Items.Remove(wg);
            UpdateEquippedAccessoryDisplay();
            return;
        }

        private void UnequipAccessory(WargearInfo wg)
        {
            if (wg.WargearType != WargearType.Misc)
                return;
            if (Hero.Wargear.All(wargear => wg.Id != wargear.Id))
                return;
            if (!UnequipWargear(wg))
                return;
            Hero.Wargear.Remove(wg);
            m_lbxAccessories.Items.Add(wg);
            m_lbxEquippedAccessories.Items.Remove(wg);
            UpdateEquippedAccessoryDisplay();
            return;
        }

        private static Image GetWargearImage(WargearInfo wg)
        {
            string iconPath = ItemDatabases.Wargear.GetOther(wg.Id, "ICON_PATH");
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

        private void LbxCurAccessoiresSelectedIndexChanged(object sender, EventArgs e)
        {
            var wg = GetSelectedEquippedAccessory();
            if (wg == null)
                NoEquippedAccessorySelected();
            else
                EquippedAccessorySelected(wg);
        }

        private void LbxAccessoriesSelectedIndexChanged(object sender, EventArgs e)
        {
            var wg = GetSelectedAccessory();
            if (wg == null)
                NoAccessorySelected();
            else
                AccessorySelected(wg);
        }

        private void LbxSecondaryWeaponsSelectedIndexChanged(object sender, EventArgs e)
        {
            var wg = GetSelectedSecondary();
            if (wg == null)
                NoSecondarySelected();
            else
                SecondarySelected(wg);
        }

        private void LbxPrimaryWeaponsSelectedIndexChanged(object sender, EventArgs e)
        {
            var wg = GetSelectedPrimary();
            if (wg == null)
                NoPrimarySelected();
            else
                PrimarySelected(wg);
        }

        private void LbxArmorsSelectedIndexChanged(object sender, EventArgs e)
        {
            var wg = GetSelectedArmor();
            if (wg == null)
                NoArmorSelected();
            else
                ArmorSelected(wg);
        }

        private void BtnEquipArmorClick(object sender, EventArgs e)
        {
            var wg = GetSelectedArmor();
            if (wg != null)
                EquipArmor(wg);
        }

        private void BtnEquipPrimaryClick(object sender, EventArgs e)
        {
            var wg = GetSelectedPrimary();
            if (wg != null)
                EquipPrimary(wg);
        }

        private void BtnEquipSecondaryClick(object sender, EventArgs e)
        {
            var wg = GetSelectedSecondary();
            if (wg != null)
                EquipSecondary(wg);
        }

        private void BtnEquipAccessoryClick(object sender, EventArgs e)
        {
            var wg = GetSelectedAccessory();
            if (wg != null)
            {
                if (GetNumAccessorySlots() <= 0)
                {
                    UIHelper.ShowMessage("Sorry", "You can't equip more accessories.");
                    return;
                }
                EquipAccessory(wg);
            }
        }

        private void BtnUnequipAccessoryClick(object sender, EventArgs e)
        {
            var wg = GetSelectedEquippedAccessory();
            if (wg != null)
                UnequipAccessory(wg);
        }

        private void WargearMenuShown(object sender, EventArgs e)
        {
            FillAccessoryDisplay();
            FillArmorDisplay();
            FillPrimaryDisplay();
            FillSecondaryDisplay();
            NoAccessorySelected();
            NoArmorSelected();
            NoPrimarySelected();
            NoSecondarySelected();
            InitEquippedAccessoryDisplay();
            UpdateEquippedArmorDisplay();
            UpdateEquippedPrimaryWeaponDisplay();
            UpdateEquippedSecondaryWeaponDisplay();

            var primWeapon = GetEquippedPrimary();
            if (primWeapon != null && primWeapon.WargearType == WargearType.SingleWeapon)
                DisableSecondary();
        }

        #endregion
    }
}

