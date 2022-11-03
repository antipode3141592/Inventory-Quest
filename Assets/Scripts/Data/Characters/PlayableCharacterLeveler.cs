using System.Collections.Generic;

namespace Data.Characters
{
    public static class PlayableCharacterLeveler
    {
        public static void AddRanksToCharacterStat(ICharacter character, IDictionary<StatTypes, int> purchasedRanks)
        {
            var statsDictionary = character.StatDictionary;
            foreach (var stat in purchasedRanks)
            {
                statsDictionary[stat.Key].PurchasedLevels += stat.Value;
            }
        }
    }
}
