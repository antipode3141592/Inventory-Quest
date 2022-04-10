using Data.Items;
using System.Collections.Generic;

namespace Data.Characters
{
    public class CharacterFactory
    {
        public static PlayableCharacter GetCharacter(CharacterStats characterStats, IList<IEquipable> startingEquipment = null, IList<IItem> startingInventory = null)
        {
            PlayableCharacter character = new PlayableCharacter(characterStats, startingEquipment, startingInventory);
            return character;
        }
    }
}
