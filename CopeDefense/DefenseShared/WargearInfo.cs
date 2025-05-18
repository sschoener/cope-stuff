using System;

namespace DefenseShared
{
    /// <summary>
    /// Information about wargear.
    /// </summary>
    public class WargearInfo
    {
        /// <summary>
        /// The Id of this piece of wargear.
        /// </summary>
        public int Id;

        /// <summary>
        /// Blueprint of this piece of wargear.
        /// </summary>
        public string BlueprintPath;

        /// <summary>
        /// The type of this piece of wargear
        /// </summary>
        public WargearType WargearType;

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        public static WargearType ParseWargearType(string type)
        {
            switch(type.ToLowerInvariant())
            {
                case "misc":
                    return WargearType.Misc;
                case "armor":
                    return WargearType.Armor;
                case "singleweapon":
                    return WargearType.SingleWeapon;
                case "weapon1":
                    return WargearType.Weapon1;
                case "weapon2":
                    return WargearType.Weapon2;
            }
            throw new Exception("Unknown wargear type: " + type);
        }

        public static string WargearTypeString(WargearType type)
        {
            switch (type)
            {
                case WargearType.Armor:
                    return "Armor";
                case WargearType.Misc:
                    return "Misc";
                case WargearType.SingleWeapon:
                    return "SingleWeapon";
                case WargearType.Weapon1:
                    return "Weapon1";
                case WargearType.Weapon2:
                    return "Weapon2";
            }
            throw new Exception("Unknown wargear type: " + type);
        }

        public override string ToString()
        {
            return ItemDatabases.Wargear.GetName(Id);
        }
    }
}