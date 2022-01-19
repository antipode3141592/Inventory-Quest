using Data;

namespace InventoryQuest.Characters
{
    public class CharacterFactory
    {
        public static Character GetCharacter(CharacterStats characterStats, ContainerStats containerStats)
        {
            Character character = new Character(characterStats, containerStats);
            return character;
        }
    }
}
