#region

using System;
using System.Windows.Forms;
using cope;
using DefenseShared;

#endregion

namespace CopeDefenseLauncher
{
    public partial class PlayerSetupForm : Form
    {
        #region fields

        private HeroInfo m_currentHero;
        private PlayerInfo m_player;

        #endregion

        public PlayerSetupForm()
        {
            InitializeComponent();
        }

        #region eventhandlers

        private void PlayerSetupFormShown(object sender, EventArgs e)
        {
            try
            {
                GetUpdate();
            }
            catch(Exception ex)
            {
                Program.HandleException(ex);
                UIHelper.ShowError(
                    "Sorry, there seems to be a problem. Try again -- or if happens again, wait for cope to fix the server.");
                Close();
            }
        }

        private void BtnSaveHeroClick(object sender, EventArgs e)
        {
            if (m_currentHero == null)
                return;
            m_currentHero.AttribEnergy = (int) m_nupAttributeEnergy.Value;
            m_currentHero.AttribHealth = (int) m_nupAttributeHealth.Value;
            m_currentHero.AttribMelee = (int) m_nupAttributeMelee.Value;
            m_currentHero.AttribRanged = (int) m_nupAttributeRanged.Value;

            if (!ServerInterface.UpdateHeroAttributes(m_currentHero))
                UIHelper.ShowError("Failed to save hero data! Try again.");
            else
                UIHelper.ShowMessage("Success", "Hero data saved.");
        }

        private void BtnCreateNewHeroClick(object sender, EventArgs e)
        {
            if (m_player.Heroes.Count == 10)
            {
                UIHelper.ShowError("You cannot have more than 10 heroes!");
                return;
            }
            NewHeroForm dlg = new NewHeroForm();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            if (ServerInterface.CreateHero(dlg.HeroName, dlg.HeroType.Id))
            {
                GetUpdate();
                if (m_player.Heroes.Count == 1)
                    SelectHero(m_player.Heroes[0]);
            }
                
            else
                UIHelper.ShowError("Failed to create hero! Try again!");
        }

        private void BtnDeleteHeroClick(object sender, EventArgs e)
        {
            if (m_currentHero == null)
                return;
            DeleteHeroForm dlg = new DeleteHeroForm();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            if (!ServerInterface.DeleteHero(m_currentHero))
                UIHelper.ShowError("Failed to delete hero! Try again!");
            GetUpdate();
        }

        private void BtnOpenUpgradeMenuClick(object sender, EventArgs e)
        {
            UpgradeMenu menu = new UpgradeMenu {Hero = m_currentHero};
            menu.ShowDialog();
            GetUpdate();
            UpdateHeroSelection();
        }

        private void BtnOpenWargearMenuClick(object sender, EventArgs e)
        {
            WargearMenu menu = new WargearMenu {Hero = m_currentHero};
            menu.ShowDialog();
            GetUpdate();
            UpdateHeroSelection();
        }

        private void BtnOpenUnlockMenuClick(object sender, EventArgs e)
        {
            UnlockMenu menu = new UnlockMenu {Hero = m_currentHero};
            menu.ShowDialog();
            GetUpdate();
            UpdateHeroSelection();
        }

        private void CbxCurrentHeroSelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = m_cbxCurrentHero.SelectedIndex;
            if (idx < 0)
                return;
            m_currentHero = (HeroInfo) m_cbxCurrentHero.Items[idx];
            UpdateHeroSelection();
        }

        private void ChklbxHeroesItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked && m_chklbxHeroes.Items.Count == 1)
            {
                UIHelper.ShowError("You need to have an active hero.");
                e.NewValue = e.CurrentValue;
            }
            if (e.NewValue == CheckState.Checked)
            {
                var heroInfo = (HeroInfo) m_chklbxHeroes.Items[e.Index];
                if (!heroInfo.Active)
                    SelectHero(heroInfo);
            }
                
        }

        private void AttributeValueChanged(object sender, EventArgs e)
        {
            RecalculateAttribPoints();
        }

        #endregion

        #region methods

        private void SelectHero(HeroInfo info)
        {
            int index = m_chklbxHeroes.Items.IndexOf(info);
            if (!ServerInterface.SetHeroActive(info))
            {
                UIHelper.ShowError("Failed to save hero selection! Try again.");
                return;
            }
            UncheckAllHeroesBut(index);
            UIHelper.ShowMessage("Success",
                                 "Hero selection saved. Active hero set to " + info + ".");
        }

        private void UncheckAllHeroesBut(int idx)
        {
            for (int i = 0; i < m_chklbxHeroes.Items.Count; i++)
            {
                if (i != idx)
                    m_chklbxHeroes.SetItemChecked(i, false);
            }
                
        }

        /// <summary>
        /// Gets hero data from the server.
        /// </summary>
        private void GetUpdate()
        {
            int currentlySelected = m_cbxCurrentHero.SelectedIndex;
            m_cbxCurrentHero.Items.Clear();
            m_chklbxHeroes.Items.Clear();
            m_player = ServerInterface.GetPlayerInfo();
            foreach (HeroInfo hi in m_player.Heroes)
            {
                m_cbxCurrentHero.Items.Add(hi);
                m_chklbxHeroes.Items.Add(hi, hi.Active);
            }
                
            if (m_player.Heroes.Count > 0)
            {
                m_currentHero = m_player.Heroes[0];
                if (currentlySelected > m_player.Heroes.Count || currentlySelected < 0)
                    currentlySelected = 0;
                m_cbxCurrentHero.SelectedIndex = currentlySelected;
            }
            else
            {
                m_currentHero = null;
                m_labClass.Text = string.Empty;
                m_labAttribPointsLeft.Text = string.Empty;
                m_labExperience.Text = string.Empty;
                m_labLevel.Text = string.Empty;
                m_labNextExperience.Text = string.Empty;
                m_labMaxWave.Text = string.Empty;
                m_labKills.Text = string.Empty;
                m_labLosses.Text = string.Empty;
                m_labMoney.Text = string.Empty;
                m_prgExperience.Value = 0;
                m_nupAttributeEnergy.Value = 0;
                m_nupAttributeHealth.Value = 0;
                m_nupAttributeMelee.Value = 0;
                m_nupAttributeRanged.Value = 0;
            }
        }

        private void UpdateHeroSelection()
        {
            m_nupAttributeEnergy.Maximum = 100;
            m_nupAttributeEnergy.Value = m_currentHero.AttribEnergy;
            m_nupAttributeHealth.Maximum = 100;
            m_nupAttributeHealth.Value = m_currentHero.AttribHealth;
            m_nupAttributeMelee.Maximum = 100;
            m_nupAttributeMelee.Value = m_currentHero.AttribMelee;
            m_nupAttributeRanged.Maximum = 100;
            m_nupAttributeRanged.Value = m_currentHero.AttribRanged;

            RecalculateAttribPoints();
            m_labClass.Text = ServerInterface.GetHeroType(m_currentHero).Name;
            m_labExperience.Text = m_currentHero.Experience.ToString();
            int heroLevel = GameMechanics.GetHeroLevel(m_currentHero);
            m_labNextExperience.Text = GameMechanics.GetExpForLevel(heroLevel + 1).ToString();
            m_labLevel.Text = heroLevel.ToString();
            m_prgExperience.Maximum = GameMechanics.GetExpForLevel(heroLevel + 1) -
                                      GameMechanics.GetExpForLevel(heroLevel);
            m_prgExperience.Value = m_currentHero.Experience - GameMechanics.GetExpForLevel(heroLevel);
            m_labMaxWave.Text = m_currentHero.MaxWave.ToString();
            m_labKills.Text = m_currentHero.TotalKills.ToString();
            m_labLosses.Text = m_currentHero.SquadsLost.ToString();
            m_labMoney.Text = m_currentHero.Money.ToString();
        }

        /// <summary>
        /// Updates the maximum values of the numeric up-downs according to the total number of attrib-points.
        /// </summary>
        private void RecalculateAttribPoints()
        {
            if (m_currentHero == null)
                return;
            int attribSum = (int) (m_nupAttributeEnergy.Value + m_nupAttributeHealth.Value + m_nupAttributeMelee.Value +
                                   m_nupAttributeRanged.Value);
            int pointsLeft = GameMechanics.GetNumAttributePoints(m_currentHero) - attribSum;
            m_labAttribPointsLeft.Text = pointsLeft.ToString();
            m_nupAttributeEnergy.Maximum = m_nupAttributeEnergy.Value + pointsLeft;
            m_nupAttributeHealth.Maximum = m_nupAttributeHealth.Value + pointsLeft;
            m_nupAttributeMelee.Maximum = m_nupAttributeMelee.Value + pointsLeft;
            m_nupAttributeRanged.Maximum = m_nupAttributeRanged.Value + pointsLeft;
        }

        #endregion
    }
}