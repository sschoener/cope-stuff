#region

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Script.Serialization;
using cope;
using cope.Extensions;
using cope.FOB;

#endregion

namespace CopeDefense
{
    public static class DoW2Bridge
    {
        private static LuaBridge s_luaBridge;

        // delegate for LUA functions
        private static LuaHandler s_luaHandler; // the current LuaHandler; gets assigned in LuaInit()

        #region Cope's Defense functions

        private const string COPE_DEFENSE_URL = "http://www.s-schoener.com/cdm/game_interface.php";
        private const string HERO_BP_PATH = "sbps/cope_defense/player/";
        private const string UPGRADE_BP_PATH = "upgrade/cope_defense/player/";
        private const string WARGEAR_BP_PATH = "wargear/wargear/cope_defense/player/";
        private static PlayerInfo[] s_players;
        private static MarsagliaRng s_rng;
        private static readonly object s_randLock = new object();

        // args: table w/ playernames, int playerCount
        // returns: nothing
        private static int PreloadData(IntPtr state)
        {
            DebugLog.SendMessage("PreloadData called");
            try
            {
                int playerCount = LuaManager.lua_tointeger(state, 2);
                DebugLog.SendMessage("PlayerCount: " + playerCount);
                s_players = new PlayerInfo[playerCount];

                string[] playerNames = new string[playerCount];
                StringBuilder names = new StringBuilder();
                for (int i = 0; i < playerCount; i++)
                {
                    LuaManager.lua_pushinteger(state, i + 1);
                    LuaManager.lua_gettable(state, 1);
                    string name = LuaManager.lua_tolstring(state, -1, IntPtr.Zero);
                    playerNames[i] = name;
                    names.Append(name);
                    names.Append(',');
                    LuaManager.lua_remove(state, -1); // 3 = top of the stack
                }
                names.RemoveLast(1);
                DebugLog.SendMessage("Requesting data for " + names);
                string post = UserPost + "&cmd=GetPlayers&names=" + names;
                string playerInfo = WebHelper.SendData(COPE_DEFENSE_URL, post);
                ProcessPlayerInfo(playerInfo, playerNames);
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in PreloadData");
                DebugLog.HandleException(ex);
            }

            return 0;
        }

        // arg: none
        // returns: a table with...
        //      1. heroes' bp paths
        //      2. heroes' wargear paths
        //      3. heroes' upgrade paths
        //      4. heroes' experience points
        private static int GetPlayerHeroes(IntPtr state)
        {
            DebugLog.SendMessage("GetPlayerHeroes called");
            try
            {
                #region construct table

                LuaManager.lua_newtable(state);
                for (int playerIdx = 1; playerIdx <= s_players.Length; playerIdx++)
                {
                    var player = s_players[playerIdx - 1];
                    var hero = player.Hero;

                    if (player.Name == UserName)
                    {
                        HeroId = hero.Id;
                        DebugLog.SendMessage("Local hero id set to " + hero.Id);
                    }

                    LuaManager.lua_createtable(state, 0, 9);
                    LuaManager.lua_pushstring(state, "attrib_energy");
                    LuaManager.lua_pushinteger(state, hero.AttribEnergy);
                    LuaManager.lua_rawset(state, -3);
                    LuaManager.lua_pushstring(state, "attrib_health");
                    LuaManager.lua_pushinteger(state, hero.AttribHealth);
                    LuaManager.lua_rawset(state, -3);
                    LuaManager.lua_pushstring(state, "attrib_melee");
                    LuaManager.lua_pushinteger(state, hero.AttribMelee);
                    LuaManager.lua_rawset(state, -3);
                    LuaManager.lua_pushstring(state, "attrib_ranged");
                    LuaManager.lua_pushinteger(state, hero.AttribRanged);
                    LuaManager.lua_rawset(state, -3);
                    LuaManager.lua_pushstring(state, "attrib_energy");
                    LuaManager.lua_pushinteger(state, hero.AttribEnergy);
                    LuaManager.lua_rawset(state, -3);
                    LuaManager.lua_pushstring(state, "experience");
                    LuaManager.lua_pushinteger(state, hero.Experience);
                    LuaManager.lua_rawset(state, -3);
                    LuaManager.lua_pushstring(state, "blueprint");
                    LuaManager.lua_pushstring(state, HERO_BP_PATH + hero.Blueprint);
                    LuaManager.lua_rawset(state, -3);

                    LuaManager.lua_pushstring(state, "upgrades");
                    LuaManager.lua_createtable(state, hero.Upgrades.Length, 0);
                    for (int upgradeIdx = 1; upgradeIdx <= hero.Upgrades.Length; upgradeIdx++)
                    {
                        string upgradeBp = hero.Upgrades[upgradeIdx - 1];
                        LuaManager.lua_pushinteger(state, upgradeIdx);
                        LuaManager.lua_pushstring(state, UPGRADE_BP_PATH + upgradeBp);
                        LuaManager.lua_rawset(state, -3);
                    }
                    LuaManager.lua_rawset(state, -3); // add 'upgrades'-table

                    LuaManager.lua_pushstring(state, "wargear");
                    LuaManager.lua_createtable(state, hero.Wargear.Length, 0);
                    for (int wargearIdx = 1; wargearIdx <= hero.Wargear.Length; wargearIdx++)
                    {
                        string wargearBp = hero.Wargear[wargearIdx - 1];
                        LuaManager.lua_pushinteger(state, wargearIdx);
                        LuaManager.lua_pushstring(state, WARGEAR_BP_PATH + wargearBp);
                        LuaManager.lua_rawset(state, -3);
                    }
                    LuaManager.lua_rawset(state, -3); // add 'wargear'-table
                    LuaManager.lua_rawseti(state, -2, playerIdx);
                }

                #endregion
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in GetPlayerHeroes");
                DebugLog.HandleException(ex);
            }
            return 1;
        }

        // arg: int amount
        // returns: nothing
        private static int AddMoney(IntPtr state)
        {
            DebugLog.SendMessage("AddMoney called");
            try
            {
                int amount = LuaManager.lua_tointeger(state, 1);
                string post = UserPost + "&cmd=AddMoney&heroId=" + HeroId + "&amount=" + amount;
                DebugLog.SendMessage("Request: " + post);
                string response = WebHelper.SendData(COPE_DEFENSE_URL, post);
                DebugLog.SendMessage("Response: " + response);
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in AddMoney");
                DebugLog.HandleException(ex);
            }
            return 0;
        }

        // arg: string item-identifier
        // returns: nothing
        private static int AddItem(IntPtr state)
        {
            DebugLog.SendMessage("AddItem called");
            try
            {
                string itemId = LuaManager.lua_tolstring(state, 1, IntPtr.Zero);
                string post = UserPost + "&cmd=AddItem&heroId=" + HeroId + "&itemId=" + itemId;
                WebHelper.SendData(COPE_DEFENSE_URL, post);
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in AddItem");
                DebugLog.HandleException(ex);
            }
            return 0;
        }

        // args: int currentExpPoints
        // returns: nothing
        private static int SetPlayerExperience(IntPtr state)
        {
            DebugLog.SendMessage("SetPlayerExperience called");
            try
            {
                int currentExpPoints = LuaManager.lua_tointeger(state, 1);
                string post = UserPost + "&cmd=SetExp&heroId=" + HeroId + "&amount=" + currentExpPoints;
                DebugLog.SendMessage("Request: " + post);
                string response = WebHelper.SendData(COPE_DEFENSE_URL, post);
                DebugLog.SendMessage("Response: " + response);
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in SetPlayerExperience");
                DebugLog.HandleException(ex);
            }

            return 0;
        }

        // args: int kills, int losses, int wave
        // returns: nothing
        private static int UpdateStats(IntPtr state)
        {
            DebugLog.SendMessage("UpdateStats called");
            try
            {
                int kills = LuaManager.lua_tointeger(state, 1);
                int losses = LuaManager.lua_tointeger(state, 2);
                int wave = LuaManager.lua_tointeger(state, 3);
                string post = UserPost + "&cmd=UpdateStats&heroId=" + HeroId + "&kills=" + kills + "&losses=" + losses +
                              "&wave=" + wave;
                DebugLog.SendMessage("Request: " + post);
                string response = WebHelper.SendData(COPE_DEFENSE_URL, post);
                DebugLog.SendMessage("Response: " + response);
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in UpdateStats");
                DebugLog.HandleException(ex);
            }
            return 0;
        }

        private static void ProcessPlayerInfo(string playerString, string[] players)
        {
            DebugLog.SendPlainMessage("Got player data: " + playerString);
            try
            {
                var jss = new JavaScriptSerializer();
                var playerData = jss.Deserialize<Dictionary<string, dynamic>>(playerString);

                foreach (var player in playerData["players"])
                {
                    string userName = player["user_name"];
                    int playerIdx = players.IndexOf(userName);
                    PlayerInfo playerInfo = new PlayerInfo();
                    s_players[playerIdx] = playerInfo;
                    playerInfo.Name = userName;
                    var hero = playerInfo.Hero;

                    hero.Id = player["id"];
                    hero.AttribEnergy = player["attrib_energy"];
                    hero.AttribHealth = player["attrib_health"];
                    hero.AttribMelee = player["attrib_melee"];
                    hero.AttribRanged = player["attrib_ranged"];
                    hero.Blueprint = ((string)player["bp_path"]).Replace('\\', '/');
                    hero.Experience = player["experience"];

                    var wargear = new List<string>();
                    foreach (string wg in player["wargear"])
                        wargear.Add(wg.Replace('\\', '/'));
                    hero.Wargear = wargear.ToArray();
                    var upgrades = new List<string>();
                    foreach (string upg in player["upgrades"])
                        upgrades.Add(upg.Replace('\\', '/'));
                    hero.Upgrades = upgrades.ToArray();
                }
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in ProcessPlayerInfo");
                DebugLog.HandleException(ex);
            }
        }

        // args: int seed
        // returns: nothing
        private static int InitRandom(IntPtr state)
        {
            try
            {
                uint seed =  (uint)LuaManager.lua_tointeger(state, 1);
                DebugLog.SendMessage("Random init with seed: " + seed);
                s_rng = new MarsagliaRng(seed);
                return 0;
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in InitRandom");
                DebugLog.HandleException(ex);
                return 0;
            }
        }

        // args: int lower, int upper
        // returns: a random value in the range
        private static int GetRandom(IntPtr state)
        {
            try
            {
                int lower = LuaManager.lua_tointeger(state, 1);
                int upper = LuaManager.lua_tointeger(state, 2);
                LuaManager.lua_remove(state, 1);
                LuaManager.lua_remove(state, 1);
                if (s_rng == null)
                {
                    DebugLog.SendMessage("Random value requested but no RNG: upper = " + upper + ", lower = " + lower);
                    DebugLog.SendMessage("Returning lower limit");
                    LuaManager.lua_pushinteger(state, lower);
                    return 1;
                }
                int random = lower;
                if (lower < upper)
                {
                    lock (s_randLock)
                    {
                        random = upper == lower ? upper : s_rng.GetInt(lower, upper + 1);
                    }
                }
                /*DebugLog.SendMessage("Random value requested: upper = " + upper + ", lower = " + lower + ", return: " +
                                     random);*/
                LuaManager.lua_pushinteger(state, random);
                return 1;
            }
            catch (Exception ex)
            {
                DebugLog.SendError("Error in GetRandom");
                DebugLog.HandleException(ex);
                return 0;
            }
        }

        #region Nested type: HeroInfo

        private class HeroInfo
        {
            public int AttribEnergy;
            public int AttribHealth;
            public int AttribMelee;
            public int AttribRanged;
            public string Blueprint;
            public int Experience;
            public int Id;
            public string[] Upgrades;
            public string[] Wargear;
        }

        #endregion

        #region Nested type: PlayerInfo

        private class PlayerInfo
        {
            public readonly HeroInfo Hero = new HeroInfo();
            public string Name;
        }

        #endregion

        #endregion

        private static string UserPost
        {
            get { return "user=" + UserName + "&pwd=" + Password; }
        }

        private static int HeroId { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// MD5 hash of the password
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// Function called by the injected DLL, all the functions you want to add to the LUA state go here.
        /// Init of LuaBridge happens in here.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static int LuaCallHandler(IntPtr state)
        {
            DebugLog.SendMessage("LuaCallHandler called!");
            s_luaBridge = new LuaBridge(state);
            s_luaBridge.RegisterLuaFunction(AddItem, "Cope_AddItem");
            s_luaBridge.RegisterLuaFunction(AddMoney, "Cope_AddMoney");
            s_luaBridge.RegisterLuaFunction(GetPlayerHeroes, "Cope_GetPlayers");
            s_luaBridge.RegisterLuaFunction(PreloadData, "Cope_Preload");
            s_luaBridge.RegisterLuaFunction(SetPlayerExperience, "Cope_SetExperience");
            s_luaBridge.RegisterLuaFunction(UpdateStats, "Cope_UpdateStats");
            s_luaBridge.RegisterLuaFunction(InitRandom, "Cope_InitRandom");
            s_luaBridge.RegisterLuaFunction(GetRandom, "Cope_GetRandom");
            return 0;
        }

        /// <summary>
        /// Initializes the bridge to DoW2, called by DebugManager.Init().
        /// </summary>
        public static void LuaInit()
        {
            const string initStartMsg = "CopeDefense - Lua Init Started";
            const string initEndMsg = "CopeDefense - Lua Init Finished";
            DebugLog.SendMessage(initStartMsg);
            TimeStampedTrace(initStartMsg);
            s_luaHandler = LuaCallHandler; // setup a handler for LUA calls by the injected CopeLua.dll
            try
            {
                SetLuaHandler(Marshal.GetFunctionPointerForDelegate(s_luaHandler));
            }
            catch (Exception ex)
            {
                const string failMsg = "CopeDefense - Lua Init Failed";
                DebugLog.SendError(failMsg);
                DebugLog.HandleException(ex);
                TimeStampedTrace(failMsg);
                return;
            }
            DebugLog.SendMessage(initEndMsg);
            TimeStampedTrace(initEndMsg);
        }

        #region imports

        // used for printing stuff to the DoW2 console
        [DllImport("Debug.dll", SetLastError = true,
            EntryPoint = "?TimeStampedTracef@dbInternal@@YAXPBDZZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TimeStampedTrace(string s);

        // import from native x86 DLL used for getting access to the LUA system
        [DllImport("CopeLua.dll", EntryPoint = "SetLuaHandler", CallingConvention = CallingConvention.StdCall)]
        public static extern void SetLuaHandler(IntPtr func);

        #endregion imports

        #region Nested type: LuaHandler

        private delegate int LuaHandler(IntPtr ptr);

        #endregion
    }
}