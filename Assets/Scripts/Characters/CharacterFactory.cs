using Data;
using Data.Interfaces;
using System.Collections.Generic;

namespace InventoryQuest.Characters
{
    public class CharacterFactory
    {
        public static Character GetCharacter(CharacterStats characterStats, IList<IEquipable> startingEquipment = null, IList<IItem> startingInventory = null)
        {
            Character character = new Character(characterStats, startingEquipment, startingInventory);
            return character;
        }
    }
}
