using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefenseShared
{
    public static class GameMechanics
    {
        // Fields
        private static readonly int[] s_levelToExperience = new int[] { 
            0, 500, 2500, 8500, 17000, 26500, 38000, 51000, 64000, 78000, 93000, 108000, 125000, 142000, 162000, 185000, 
            210000, 235000, 265000, 300000
        };

        // Methods
        public static int GetExpForLevel(int level)
        {
            return s_levelToExperience[level - 1];
        }

        public static int GetHeroLevel(HeroInfo hero)
        {
            return GetLevelForExp(hero.Experience);
        }

        public static int GetLevelForExp(int exp)
        {
            for (int i = 0; i <= s_levelToExperience.Length; i++)
            {
                if (exp < s_levelToExperience[i])
                {
                    return i;
                }
            }
            return s_levelToExperience.Length;
        }

        public static int GetNumAccessorySlots(HeroInfo hero)
        {
            return (1 + (GetHeroLevel(hero) / 5));
        }

        public static int GetNumAttributePoints(HeroInfo hero)
        {
            return (GetHeroLevel(hero) * 2);
        }

        public static int GetNumMiscUpgradeSlots(HeroInfo hero)
        {
            return (2 + (GetHeroLevel(hero) / 5));
        }

        public static int GetNumUnitSlots(HeroInfo hero)
        {
            return (2 + (GetHeroLevel(hero) / 5));
        }
    }
}
