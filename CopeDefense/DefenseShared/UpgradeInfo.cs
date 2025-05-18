using System;

namespace DefenseShared
{
    /// <summary>
    /// Information about upgrades.
    /// </summary>
    public class UpgradeInfo
    {
        /// <summary>
        /// The id of this upgrade.
        /// </summary>
        public int Id;

        /// <summary>
        /// The type of this upgrade.
        /// </summary>
        public UpgradeType UpgradeType;
        
        /// <summary>
        /// Blueprint of this upgrade.
        /// </summary>
        public string BlueprintPath;

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public static UpgradeType ParseUpgradeType(string type)
        {
            switch (type.ToLowerInvariant())
            {
                case "misc":
                    return UpgradeType.Misc;
                case "skill1":
                    return UpgradeType.Skill1;
                case "skill2":
                    return UpgradeType.Skill2;
                case "skill3":
                    return UpgradeType.Skill3;
                case "skill4":
                    return UpgradeType.Skill4;
                case "skill5":
                    return UpgradeType.Skill5;
                case "unit":
                    return UpgradeType.Unit;
                case "hidden":
                    return UpgradeType.Hidden;
            }
            throw new Exception("Unknown upgrade type: " + type);
        }

        public static string UpgradeTypeString(UpgradeType type)
        {
            switch(type)
            {
                case UpgradeType.Hidden:
                    return "Hidden";
                case UpgradeType.Misc:
                    return "Misc";
                case UpgradeType.Skill1:
                    return "Skill1";
                case UpgradeType.Skill2:
                    return "Skill2";
                case UpgradeType.Skill3:
                    return "Skill3";
                case UpgradeType.Skill4:
                    return "Skill4";
                case UpgradeType.Skill5:
                    return "Skill5";
                case UpgradeType.Unit:
                    return "Unit";
            }
            throw new Exception("Unknown upgrade type: " + type);
        }

        public override string ToString()
        {
            return ItemDatabases.Upgrades.GetName(Id);
        }
    }
}