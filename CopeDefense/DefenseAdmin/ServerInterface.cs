using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using cope;
using DefenseShared;
using cope.Extensions;

namespace DefenseAdmin
{
    static class ServerInterface
    {
        private const string SERVER_URL = "http://www.s-schoener.com/cdm/admin_interface.php";

        /// <summary>
        /// Gets or sets the Admin name.
        /// </summary>
        public static string AdminName { get; set; }

        /// <summary>
        /// Gets or sets the md5-hash of the admin's password.
        /// </summary>
        public static string AdminPassword { get; set; }

        private static string AdminPost
        {
            get { return "adminName=" + AdminName + "&pwd=" + AdminPassword; }
        }

        /// <summary>
        /// Tries to validate the current Admin data.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateAdmin()
        {
            return Send("ValidateAdmin") == "true";
        }

        /// <summary>
        /// Requests the users' last activity field from the server.
        /// </summary>
        /// <returns></returns>
        public static string GetActivityLog()
        {
            return Send("GetActivityLog");
        }

        /// <summary>
        /// Requests the stats from the server.
        /// </summary>
        /// <returns></returns>
        public static string GetStats()
        {
            return Send("GetStats");
        }

        /// <summary>
        /// Requests the different hero types from server.
        /// </summary>
        /// <returns></returns>
        public static string GetHeroTypes()
        {
            return Send("GetHeroTypes");
        }

        /// <summary>
        /// Adds a new hero type. Returns the new hero type's id on success, -1 otherwise.
        /// </summary>
        /// <returns></returns>
        public static int AddHeroType()
        {
            int id;
            if (int.TryParse(Send("AddHeroType"), out id))
                return id;
            return -1;
        }

        /// <summary>
        /// Removes a hero type from the database. Returns true on success, false otherwise
        /// </summary>
        /// <returns></returns>
        public static bool RemoveHeroType(int id)
        {
            return "true" == Send("RemoveHeroType", "id=" + id);
        }

        /// <summary>
        /// Updates the specified hero type. Returns true on success, false otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bpPath"></param>
        /// <param name="name"></param>
        /// <param name="unlock"></param>
        /// <param name="wargear1"></param>
        /// <param name="wargear2"></param>
        /// <param name="wargear3"></param>
        /// <returns></returns>
        public static bool UpdateHeroType(int id, string bpPath, string name, int unlock, int wargear1, int wargear2, int wargear3)
        {
            var result = Send("UpdateHeroType", "id=" + id, "bp=" + bpPath.RemoveAllBut(CharType.Ascii), "name=" + name, "unlock=" + unlock,
                              "wargear1=" + wargear1, "wargear2=" + wargear2, "wargear3=" + wargear3);
            return "true" == result;
        }

        /// <summary>
        /// Returns all unlock group entries.
        /// </summary>
        /// <returns></returns>
        public static string GetUnlockGroupEntries()
        {
            return Send("GetUnlockGroupEntries");
        }

        /// <summary>
        /// Adds the specified hero type to the given unlock group. Returns true on success, false otherwise.
        /// </summary>
        /// <param name="heroType"></param>
        /// <param name="unlockGroup"></param>
        /// <returns></returns>
        public static bool AddUnlockGroupEntry(int heroType, int unlockGroup)
        {
            var result = Send("AddUnlockGroupEntry", "heroType=" + heroType, "unlockGroup=" + unlockGroup);
            return result == "true";
        }

        /// <summary>
        /// Removes the specified hero type from the given unlock group. Returns true on success, false otherwise.
        /// </summary>
        /// <param name="heroType"></param>
        /// <param name="unlockGroup"></param>
        /// <returns></returns>
        public static bool RemoveUnlockGroupEntry(int heroType, int unlockGroup)
        {
            return "true" == Send("RemoveUnlockGroupEntry", "heroType=" + heroType, "unlockGroup=" + unlockGroup);
        }

        /// <summary>
        /// Gets all unlocks from the server.
        /// </summary>
        /// <returns></returns>
        public static string GetUnlocks()
        {
            return Send("GetUnlocks");
        }

        /// <summary>
        /// Adds a new unlock to the database. Returns the unlock's id on success, -1 otherwise.
        /// </summary>
        /// <returns></returns>
        public static int AddUnlock()
        {
            int id;
            if (int.TryParse(Send("AddUnlock"), out id))
                return id;
            return -1;
        }

        /// <summary>
        /// Removes the specified unlock from the database. Returns true on success, false otherwise.
        /// </summary>
        /// <returns></returns>
        public static bool RemoveUnlock(int id)
        {
            return "true" == Send("RemoveUnlock", "id=" + id);
        }

        /// <summary>
        /// Updates the specified unlock. Returns true on success, false otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <param name="reqId"></param>
        /// <param name="unlockGroup"></param>
        /// <returns></returns>
        public static bool UpdateUnlock(int id, int price, int reqId, int unlockGroup)
        {
            return "true" ==
                   Send("UpdateUnlock", "id=" + id, "price=" + price, "reqId=" + reqId, "unlockGroup=" + unlockGroup);
        }

        /// <summary>
        /// Gets the unlocks from the server and adds them to the specified dictionary.
        /// </summary>
        /// <param name="addTo"></param>
        public static void GetAndParseUnlocks(Dictionary<int, Unlock> addTo)
        {

            var unlockString = GetUnlocks();
            var jss = new JavaScriptSerializer();
            var unlocks = jss.Deserialize<dynamic>(unlockString)["unlocks"];
            foreach (var unlock in unlocks)
            {
                Unlock ul = new Unlock
                {
                    Id = unlock["id"],
                    Price = unlock["price"],
                    RequiredUnlockId = unlock["req_unlock_id"],
                    UnlockGroup = unlock["hero_unlock_group"]
                };
                addTo.Add(ul.Id, ul);
            }
        }

        /// <summary>
        /// Requests all wargear from the server.
        /// </summary>
        /// <returns></returns>
        public static string GetWargear()
        {
            return Send("GetWargear");
        }

        /// <summary>
        /// Requests all upgrades from the server.
        /// </summary>
        /// <returns></returns>
        public static string GetUpgrades()
        {
            return Send("GetUpgrades");
        }

        /// <summary>
        /// Adds a new upgrade to the database. Returns the upgrade's id on success, -1 otherwise.
        /// </summary>
        /// <returns></returns>
        public static int AddUpgrade()
        {
            int id;
            string result = Send("AddUpgrade");
            if (int.TryParse(result, out id))
                return id;
            return -1;
        }

        /// <summary>
        /// Adds a new wargear to the database. Returns the wargear's id on success, -1 otherwise.
        /// </summary>
        /// <returns></returns>
        public static int AddWargear()
        {
            int id;
            if (int.TryParse(Send("AddWargear"), out id))
                return id;
            return -1;
        }

        /// <summary>
        /// Removes the specified upgrade from the database. Returns true on success, false otherwise.
        /// </summary>
        /// <returns></returns>
        public static bool RemoveUpgrade(int id)
        {
            return "true" == Send("RemoveUpgrade", "id=" + id);
        }

        /// <summary>
        /// Removes the specified wargear from the database. Returns true on success, false otherwise.
        /// </summary>
        /// <returns></returns>
        public static bool RemoveWargear(int id)
        {
            return "true" == Send("RemoveWargear", "id=" + id);
        }

        /// <summary>
        /// Updates the specified wargear with the given value. Returns true on success, false otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bpPath"></param>
        /// <param name="reqId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool UpdateWargear(int id, string bpPath, int reqId, WargearType type)
        {
            return "true" ==
                   Send("UpdateWargear", "id=" + id, "bp=" + bpPath.RemoveAllBut(CharType.Ascii), "reqId=" + reqId,
                        "wargearType=" + WargearInfo.WargearTypeString(type));
        }

        /// <summary>
        /// Updates the specified upgrade with the given value. Returns true on success, false otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bpPath"></param>
        /// <param name="reqId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool UpdateUpgrade(int id, string bpPath, int reqId, UpgradeType type)
        {
            return "true" == Send("UpdateUpgrade", "id=" + id, "bp=" + bpPath.RemoveAllBut(CharType.Ascii), "reqId=" + reqId,
                                  "upgradeType=" + UpgradeInfo.UpgradeTypeString(type));

        }

        private static string Send(string command, params string[] post)
        {
            string postData = "cmd=" + command + '&' + AdminPost;
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
            return WebHelper.SendData(SERVER_URL, postData);
        }
    }
}
