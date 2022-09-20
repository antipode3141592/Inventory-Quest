using System;
using System.Collections.Generic;

namespace Data.Characters
{
    public static class PlayableCharacterLeveler
    {
        public static void AddRanksToCharacterStat(PlayableCharacter character, IDictionary<CharacterStatTypes, int> purchasedRanks)
        {
            var statsDictionary = character.Stats.StatDictionary;
            foreach (var stat in purchasedRanks)
            {
                statsDictionary[stat.Key].PurchasedLevels += stat.Value;
            }
        }
    }
}
