using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryQuest.Characters
{
    public class CharacterFactory
    {
        public static Character GetCharacter()
        {
            CharacterStats playerStats = new CharacterStats(10f, 10f, 10f);
            
            Coor backpackSize = new Coor(r: 5, c: 10);
            ItemStats backpackStats = new ItemStats("adventure backpack",
                    weight: 2f,
                    goldValue: 5f,
                    description: "a basic adventurer's backpack");

            Container container = ContainerFactory.GetContainer(ShapeType.Square1, backpackStats, backpackSize);
            Character character = new Character(container, playerStats);

            return character;
        }
    }
}
