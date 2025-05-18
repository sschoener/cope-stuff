using System.Collections.Generic;

namespace DefenseShared
{
    public class HeroInfo
    {
        public HeroInfo()
        {
            Upgrades = new List<UpgradeInfo>();
            Wargear = new List<WargearInfo>();
            UnlockIds = new List<int>();
            AvailableUpgrades = new List<UpgradeInfo>();
            AvailableWargear = new List<WargearInfo>();
            AvailableUnlocks= new List<UnlockInfo>();
        }

        public string BlueprintPath;
        public string Name;
        public int Experience;
        public int AttribHealth;
        public int AttribEnergy;
        public int AttribMelee;
        public int AttribRanged;
        public int HeroId;
        public int HeroType;
        public int Money;
        public int MaxWave;
        public int TotalKills;
        public int SquadsLost;
        public bool Active;
        public List<UpgradeInfo> Upgrades;
        public List<WargearInfo> Wargear;
        public List<int> UnlockIds;
        public List<UpgradeInfo> AvailableUpgrades;
        public List<WargearInfo> AvailableWargear;
        public List<UnlockInfo> AvailableUnlocks;

        public override string ToString()
        {
            return Name;
        }
    }
}