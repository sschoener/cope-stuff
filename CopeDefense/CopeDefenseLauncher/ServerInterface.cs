#region

using System;
using System.Collections.Generic;
using System.Text;
using cope;
using System.Web.Script.Serialization;
using DefenseShared;

#endregion

namespace CopeDefenseLauncher
{
    internal static class ServerInterface
    {
        private const string SERVER_URL = "http://www.s-schoener.com/cdm/game_interface.php";

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        static internal string UserName
        {
            get { return Properties.Settings.Default.UserName; }
            set
            {
                Properties.Settings.Default.UserName = value;
            }
        }

        /// <summary>
        /// Gets or sets the password-hash.
        /// </summary>
        static internal string HashedPassword
        {
            get;
            set;
        }

        private static string UserPost
        {
            get { return "user=" + UserName + "&pwd=" + HashedPassword; }
        }

        /// <summary>
        /// Returns whether the current user/password combination is valid.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateUser()
        {
            var response = Send("ValidatePlayer");
            return response == "true";
        }

        /// <summary>
        /// Returns information about the player and his heroes.
        /// </summary>
        /// <returns></returns>
        public static PlayerInfo GetPlayerInfo()
        {
            var response = Send("GetPlayer");
            try
            {
                var pi = PlayerInfo.FromString(response);
                pi.Username = UserName;
                return pi;
            }
            catch (Exception ex)
            {
                var excep = new Exception("Error while parsing player info", ex);
                excep.Data["playerstring"] = response;
                throw excep;
            }
        }

        /// <summary>
        /// Creates a new hero given a hero name and a hero type.
        /// </summary>
        /// <param name="heroName">The name for the new hero.</param>
        /// <param name="typeId">The type of the new hero.</param>
        /// <returns></returns>
        public static bool CreateHero(string heroName, int typeId)
        {
            string response = Send("CreateHero", "name=" + heroName, "heroType=" + typeId);
            return response == "true";
        }

        /// <summary>
        /// Updates a user's hero's attributes using the provided HeroInfo.
        /// </summary>
        /// <param name="hero">The HeroInfo to update the database with.</param>
        /// <returns></returns>
        public static bool UpdateHeroAttributes(HeroInfo hero)
        {
            string response = Send("UpdateHero", "attribEnergy=" + hero.AttribEnergy,
                                   "attribHealth=" + hero.AttribHealth, "attribMelee=" +
                                                                        hero.AttribMelee,
                                   "attribRanged=" + hero.AttribRanged, "heroId=" + hero.HeroId);
            return response == "true";
        }

        /// <summary>
        /// Deletes a hero.
        /// </summary>
        /// <param name="hero">The HeroInfo of the hero to delete.</param>
        /// <returns></returns>
        public static bool DeleteHero(HeroInfo hero)
        {
            string response = Send("DeleteHero", "heroId=" + hero.HeroId);
            return response == "true";
        }

        /// <summary>
        /// Sets a the specified hero to the user's active hero, disabling all other heroes.
        /// </summary>
        /// <param name="hero">HeroInfo of the hero to set active.</param>
        /// <returns></returns>
        public static bool SetHeroActive(HeroInfo hero)
        {
            string response = Send("SetHeroActive", "heroId=" + hero.HeroId);
            return response == "true";
        }

        /// <summary>
        /// Tries to purchase an unlock for the given hero.
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="unlockId"></param>
        /// <returns></returns>
        public static bool PurchaseUnlock(HeroInfo hero, int unlockId)
        {
            string response = Send("PurchaseUnlock", "heroId=" + hero.HeroId, "unlockId=" + unlockId);
            return response == "true";
        }

        /// <summary>
        /// Adds an upgrade to the selected hero.
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="upgradeId"></param>
        /// <returns></returns>
        public static bool AddUpgrade(HeroInfo hero, int upgradeId)
        {
            string response = Send("AddUpgrade", "heroId=" + hero.HeroId, "upgradeId=" + upgradeId);
            return response == "true";
        }

        /// <summary>
        /// Removes an upgrade from the specified hero.
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="upgradeId"></param>
        /// <returns></returns>
        public static bool RemoveUpgrade(HeroInfo hero, int upgradeId)
        {
            string response = Send("RemoveUpgrade", "heroId=" + hero.HeroId, "upgradeId=" + upgradeId);
            return response == "true";
        }

        /// <summary>
        /// Equips a specified piece of wargear.
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="wargearId"></param>
        /// <returns></returns>
        public static bool EquipWargear(HeroInfo hero, int wargearId)
        {
            string response = Send("EquipWargear", "heroId=" + hero.HeroId, "wargearId=" + wargearId);
            return response == "true";
        }

        /// <summary>
        /// Unequips a specified piece of wargear.
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="wargearId"></param>
        /// <returns></returns>
        public static bool UnequipWargear(HeroInfo hero, int wargearId)
        {
            string response = Send("UnequipWargear", "heroId=" + hero.HeroId, "wargearId=" + wargearId);
            return response == "true";
        }

        private static string Send(string command, params string[] post)
        {
            string postData = "cmd=" + command + '&' + UserPost;
            if (post != null)
            {
                StringBuilder sb = new StringBuilder(260);
                for (int i = 0; i < post.Length; i++)
                {
                    sb.Append('&');
                    sb.Append(post[i]);
                }
                postData += sb.ToString();

            }
            try
            {
                return WebHelper.SendData(SERVER_URL, postData);
            }
            catch (Exception ex)
            {
                var excep = new Exception("Error while sending message to server", ex);
                excep.Data["command"] = command;
                if (post != null)
                {
                    for (int i = 0; i < post.Length; i++)
                        excep.Data["param" + i] = post[i];
                }
                throw excep;
            }
        }

        /// <summary>
        /// Returns the available hero types.
        /// </summary>
        /// <returns></returns>
        public static HeroType[] GetHeroTypes()
        {
            string response = Send("GetHeroTypes");
            try
            {
                var jss = new JavaScriptSerializer();
                var heroes = jss.Deserialize<dynamic>(response)["hero_types"];
                List<HeroType> heroTypes = new List<HeroType>();
                foreach (var hero in heroes)
                {
                    HeroType type = new HeroType
                    {
                        BlueprintPath = hero["bp_path"],
                        Id = hero["id"],
                        Name = hero["name"]
                    };
                    heroTypes.Add(type);
                }
                return heroTypes.ToArray();
            }
            catch (Exception ex)
            {
                var excep = new Exception("Error while parsing hero types", ex);
                excep.Data["herostring"] = response;
                throw excep;
            }
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        internal static HeroType GetHeroType(HeroInfo hero)
        {
            var heroTypes = GetHeroTypes();
            foreach (HeroType ht in heroTypes)
                if (hero.HeroType == ht.Id)
                    return ht;
            throw new Exception("Unknown hero type: " + hero.HeroType);
        }
    }
}