using System;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using cope.Extensions;
using DefenseShared;

namespace DefenseAdmin
{
    public partial class StatsForm : Form
    {
        private int m_numHeroes;
        private int m_numUsers;

        public StatsForm()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            GetActivity();
            GetStats();
            if (m_numUsers > 0)
                m_labNumHeroesPerPlayer.Text = (((float) m_numHeroes)/m_numUsers).ToString();
            else
                m_labNumHeroesPerPlayer.Text = @"no players";
        }

        /// <summary>
        ///     Gets the stats from the server and displays them.
        /// </summary>
        private void GetStats()
        {
            string statString = ServerInterface.GetStats();
            var jss = new JavaScriptSerializer();
            dynamic stats = jss.Deserialize<dynamic>(statString)["stats"];
            m_labAvgEnergy.Text = stats["avgEnergy"].ToString();
            m_labAvgHealth.Text = stats["avgHealth"].ToString();
            m_labAvgKills.Text = stats["avgKills"].ToString();
            m_labAvgLevel.Text = GameMechanics.GetLevelForExp((int) stats["avgExp"]).ToString();
            m_labAvgLosses.Text = stats["avgLosses"].ToString();
            m_labAvgMelee.Text = stats["avgMelee"].ToString();
            m_labAvgMoney.Text = stats["avgMoney"].ToString();
            m_labAvgRanged.Text = stats["avgRanged"].ToString();
            m_labAvgRatio.Text = stats["avgRatio"].ToString();
            m_labAvgWave.Text = stats["avgWave"].ToString();
            m_labBestRatio.Text = stats["maxRatio"].ToString();
            m_labMaxEnergy.Text = stats["maxEnergy"].ToString();
            m_labMaxHealth.Text = stats["maxHealth"].ToString();
            m_labMaxKills.Text = stats["maxKills"].ToString();
            m_labMaxLevel.Text = GameMechanics.GetLevelForExp((int) stats["maxExp"]).ToString();
            m_labMaxLosses.Text = stats["maxLosses"].ToString();
            m_labMaxMelee.Text = stats["maxMelee"].ToString();
            m_labMaxMoney.Text = stats["maxMoney"].ToString();
            m_labMaxRanged.Text = stats["maxRanged"].ToString();
            m_labMaxWave.Text = stats["maxWave"].ToString();
            m_numHeroes = stats["numHeroes"];
            m_labNumHeroes.Text = m_numHeroes.ToString();
            m_labTotalKills.Text = stats["totalKills"].ToString();
            m_labTotalLosses.Text = stats["totalLosses"].ToString();
        }

        /// <summary>
        ///     Gets the acitivity log from the server and displays it.
        /// </summary>
        private void GetActivity()
        {
            string activityString = ServerInterface.GetActivityLog();
            var jss = new JavaScriptSerializer();
            dynamic activity = jss.Deserialize<dynamic>(activityString)["activity"];
            m_numUsers = 0;
            int activeUsers7Days = 0;
            DateTime dt = DateTime.Now - TimeSpan.FromDays(7.0);
            var timestamp = (int) dt.GetUnixTimeStamp();
            foreach (dynamic lastActivity in activity)
            {
                if (lastActivity > timestamp)
                    activeUsers7Days++;
                m_numUsers++;
            }
            m_labNumActiveUsers.Text = activeUsers7Days.ToString();
            m_labNumUsers.Text = m_numUsers.ToString();
        }
    }
}