using Data;

namespace InventoryQuest.Characters
{
    public class CharacterFactory
    {
        public static Character GetCharacter(CharacterStats characterStats, ContainerStats containerStats)
        {
            Container container = (Container)ItemFactory.GetItem(containerStats);
            Character character = new Character(characterStats, container);
            return character;
        }
    }
}
