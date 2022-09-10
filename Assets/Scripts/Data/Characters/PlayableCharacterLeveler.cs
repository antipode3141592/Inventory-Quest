using System;
using System.Collections.Generic;

namespace Data.Characters
{
    public static class PlayableCharacterLeveler
    {
        static void AddRanksToCharacter(PlayableCharacter character, IDictionary<Type, int> purchasedRanks)
        {
            var statsDictionary = character.Stats.Stats;
            foreach (var skill in purchasedRanks)
            {
                statsDictionary[skill.Key].PurchasedLevels += skill.Value;
            }
        }
    }
}
