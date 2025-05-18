#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

#endregion

namespace DefenseShared
{
    /// <summary>
    /// Class providing information about a player.
    /// </summary>
    public class PlayerInfo
    {
        private PlayerInfo()
        {
            Heroes = new List<HeroInfo>();
        }

        #region properties

        /// <summary>
        /// The player's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// All the user's heroes.
        /// </summary>
        public List<HeroInfo> Heroes { get; set; }

        #endregion

        /// <summary>
        /// Parses player-info from data received from the server.
        /// </summary>
        /// <param name="webinfo"></param>
        /// <returns></returns>
        public static PlayerInfo FromString(string webinfo)
        {
            var pi = new PlayerInfo();
            if (string.IsNullOrWhiteSpace(webinfo))
                return pi;

            var jss = new JavaScriptSerializer();
            var heroesData = jss.Deserialize<Dictionary<string, dynamic>>(webinfo);
            foreach (dynamic hero in heroesData["heroes"])
            {
                HeroInfo heroInfo = new HeroInfo();
                heroInfo.Active = hero["active"] > 0  ? true : false;
                heroInfo.AttribEnergy = hero["attrib_energy"];
                heroInfo.AttribHealth = hero["attrib_health"];
                heroInfo.AttribMelee = hero["attrib_melee"];
                heroInfo.AttribRanged = hero["attrib_ranged"];
                heroInfo.BlueprintPath = hero["bp_path"];
                heroInfo.Experience = hero["experience"];
                heroInfo.HeroId = hero["id"];
                heroInfo.HeroType = hero["hero_type"];
                heroInfo.MaxWave = hero["max_wave"];
                heroInfo.Money = hero["money"];
                heroInfo.Name = hero["hero_name"];
                heroInfo.SquadsLost = hero["total_losses"];
                heroInfo.TotalKills = hero["total_kills"];

                ParseUpgrades(hero["upgrades"], heroInfo.Upgrades);
                ParseAvailableUpgrades(hero["available_upgrades"], heroInfo.AvailableUpgrades, heroInfo.Upgrades);
                ParseWargear(hero["wargear"], heroInfo.Wargear);
                ParseAvailableWargear(hero["available_wargear"], heroInfo.AvailableWargear, heroInfo.Wargear);
                foreach (var id in hero["unlocks"])
                    heroInfo.UnlockIds.Add(id);
                ParseAvailableUnlocks(hero["available_unlocks"], heroInfo.AvailableUnlocks, heroInfo.UnlockIds);
                pi.Heroes.Add(heroInfo);
            }
            return pi;
        }

        private static void ParseAvailableUnlocks(dynamic unlocks, List<UnlockInfo> list, List<int> currentUnlocks)
        {
            foreach (var unlock in unlocks)
            {
                int id = unlock["id"];
                if (currentUnlocks.Contains(id))
                    continue;
                var unlockInfo = new UnlockInfo
                {
                    Id = id,
                    Price = unlock["price"],
                    RequiredId = unlock["req_unlock_id"]
                };
                list.Add(unlockInfo);
            }
        }

        private static void ParseUpgrades(dynamic upgrades, List<UpgradeInfo> list)
        {
            foreach (var upgrade in upgrades)
            {
                var upgradeInfo = new UpgradeInfo
                {
                    BlueprintPath = upgrade["bp_path"],
                    Id = upgrade["id"],
                    UpgradeType = UpgradeInfo.ParseUpgradeType(upgrade["upgrade_type"])
                };
                list.Add(upgradeInfo);
            }
        }

        private static void ParseAvailableUpgrades(dynamic upgrades, List<UpgradeInfo> list, IEnumerable<UpgradeInfo> currentUpgrades)
        {
            foreach (var upgrade in upgrades)
            {
                int id = upgrade["id"];
                if (currentUpgrades.Any(upg => upg.Id == id))
                    continue;
                var upgradeInfo = new UpgradeInfo
                {
                    BlueprintPath = upgrade["bp_path"],
                    Id = id,
                    UpgradeType = UpgradeInfo.ParseUpgradeType(upgrade["upgrade_type"])
                };
                list.Add(upgradeInfo);
            }
        }

        private static void ParseWargear(dynamic wargears, List<WargearInfo> list)
        {
            foreach (var wargear in wargears)
            {
                var wargearInfo = new WargearInfo
                {
                    BlueprintPath = wargear["bp_path"],
                    Id = wargear["id"],
                    WargearType = WargearInfo.ParseWargearType(wargear["wargear_type"])
                };
                list.Add(wargearInfo);
            }
        }

        private static void ParseAvailableWargear(dynamic wargears, List<WargearInfo> list, IEnumerable<WargearInfo> currentWargear)
        {
            foreach (var wargear in wargears)
            {
                int id = wargear["id"];
                if (currentWargear.Any(wg => wg.Id == id))
                    continue;
                var wargearInfo = new WargearInfo
                {
                    BlueprintPath = wargear["bp_path"],
                    Id = id,
                    WargearType = WargearInfo.ParseWargearType(wargear["wargear_type"])
                };
                list.Add(wargearInfo);
            }
        }
    }
}