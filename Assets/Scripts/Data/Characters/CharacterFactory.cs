using Data.Items;
using System.Collections.Generic;

namespace Data.Characters
{
    public class CharacterFactory
    {
        public static ICharacter GetCharacter(ICharacterStats baseStats, IList<IEquipable> startingEquipment = null, IList<IItem> startingInventory = null)
        {
            PlayableCharacter character = new (baseStats, startingEquipment, startingInventory);
            return character;
        }
    }
}
